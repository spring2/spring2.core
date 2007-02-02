using System;
using System.Reflection;
using System.Text;

using log4net;

using PayFlowPro;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.PayflowPro {
    /// <summary>
    /// Summary description for PayflowProCommand.
    /// </summary>
    public abstract class PayflowProCommand {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    	public virtual PaymentResult Execute() {
	    PayflowProProviderConfiguration config = new PayflowProProviderConfiguration();
	    PNComClass payflowPro = new PNComClass();
	    Guid correlationId = Guid.NewGuid();
	    int context = 0;
	    try {
		log.Info(string.Format("{0} ~ Submitting payment: {1} ~ RemoteAddress: {2}:{3}", correlationId, CommandText, config.HostAddress, config.HostPort));
		context = payflowPro.CreateContext(config.HostAddress, config.HostPort, config.Timeout, config.ProxyAddress, config.ProxyPort, config.ProxyLogon, config.ProxyPassword);
		string response = payflowPro.SubmitTransaction(context, config.ConnectionString + CommandText, CommandText.Length);
		log.Info(string.Format("{0} ~ ResultMessage: {1} ~ RemoteAddress: {2}:{3}", correlationId, response, config.HostAddress, config.HostPort));

		PaymentResult result = ParseResult(response);
		if (Int32.Parse(result.ResultCode) < 0) {
		    throw new PaymentConnectionException(result.ResultMessage);
		}

		// 109 = timeout
		// 101 = timeout value too small -- interpreted as timeout for purposes of tests
		if (result.ResultCode == "109" || result.ResultCode == "101") {
		    throw new PaymentConnectionException("Timeout while connecting to the payment processing subsystem (" + result.ResultCode + ")");
		}

		// handle stupid test account that keeps failing with 127:Fraud Service Internal Error-- go to security tab to rerun transactions through filters.
		if (result.ResultCode == "127" && config.HostAddress=="test-payflow.verisign.com") {
		    result.ResultCode = "0";
		}
	    	
		return result;
	    } catch (Exception ex) {
		log.Error(string.Format("{0} ~ Exception while submitting payment.", correlationId), ex);
		throw;
	    } finally {
		if (context != 0) {
		    payflowPro.DestroyContext(context);
		}
	    }
    	}
    	
    	public abstract String CommandText {
    	    get;
    	}
    	
	protected void AppendCommand(StringBuilder sb, StringType command, StringType value, Int32 maxLength) {
	    if (value.IsValid) {
		if (value.Length > maxLength) {
		    value = value.Substring(0, maxLength);
		}
		sb.Append("&").Append(command).Append("=").Append(value);
	    }
	}
    	
	private PaymentResult ParseResult(string resultString) {
	    PaymentResult result = new PaymentResult();
	    String[] resultParameters = resultString.Split('&');
	    if (resultParameters.Length <= 1) {
		throw new PaymentConnectionException("ResultMessage not correctly formatted.");
	    }

	    foreach (String connectionParameter in resultParameters) {
		String[] parameter = connectionParameter.Split(new char[] {'='}, 2);
		if (parameter.Length != 2) {
		    continue;
		}
		String value = parameter[1].Trim();

		switch (parameter[0].Trim().ToUpper()) {
		    case "PNREF": {
			result.TransactionId = value;
			break;
		    }
		    case "RESULT": {
			if (value.Length > 0) {
			    result.ResultCode = new IntegerType(int.Parse(value)).ToString();
			} else {
			    result.ResultCode = IntegerType.ZERO.ToString();
			}
			break;
		    }
		    case "CVV2MATCH": {
			result.CVVResponseCode = value;
			switch (value) {
			    case "Y" : {
				result.ValidCvv = BooleanType.TRUE;
				break;
			    }
			    case "N" : {
				result.ValidCvv = BooleanType.FALSE;
				break;
			    }
			    case "X" : {
				result.ValidCvv = BooleanType.DEFAULT;
				break;
			    }
			}
			break;
		    }
		    case "RESPMSG": {
			result.ResultMessage = value;
			break;
		    }
		    case "AUTHCODE": {
			result.AuthorizationNumber = value;
			break;
		    }
		    case "AVSADDR": {
			switch (value) {
			    case "Y" : {
				result.ValidAddress = BooleanType.TRUE;
				break;
			    }
			    case "N" : {
				result.ValidAddress = BooleanType.FALSE;
				break;
			    }
			    case "X" : {
				result.ValidAddress = BooleanType.DEFAULT;
				break;
			    }
			}
			break;
		    }
		    case "AVSZIP": {
			switch (value) {
			    case "Y" : {
				result.ValidPostalCode = BooleanType.TRUE;
				break;
			    }
			    case "N" : {
				result.ValidPostalCode = BooleanType.FALSE;
				break;
			    }
			    case "X" : {
				result.ValidPostalCode = BooleanType.DEFAULT;
				break;
			    }
			}
			break;
		    }
		    case "IAVS": {
			// is an international address
			break;
		    }
		    case "PREFPSMSG" : {
			// ignore
			break;
		    }
		    case "POSTFPSMSG" : {
			// ignore
			break;
		    }
		    default: {
			// Unexpected value.  Ignore.
			System.Diagnostics.Debug.WriteLine(connectionParameter);
			break;
		    }
		}
	    }
	    
	    // calculate AVSResponseCode
	    if (!result.ValidAddress.IsValid && !result.ValidPostalCode.IsValid) {
		result.AVSResponseCode = "X";
	    } else if (result.ValidAddress.IsTrue && result.ValidPostalCode.IsTrue) {
		result.AVSResponseCode = "Y";
	    } else if (result.ValidAddress.IsFalse && result.ValidPostalCode.IsFalse) {
		result.AVSResponseCode = "N";
	    } else {
		result.AVSResponseCode = result.ValidAddress.IsValid ? result.ValidAddress.IsTrue ? "Y" : "N" : "X";
		result.AVSResponseCode += result.ValidPostalCode.IsValid ? result.ValidPostalCode.IsTrue ? "Y" : "N" : "X";
	    }
	    
	    return result;
	}

	protected void CheckForFailures(StringType address, StringType postalCode, PaymentResult result) {
	    PayflowProProviderConfiguration config = new PayflowProProviderConfiguration();
    		
	    if(result.ResultCode != "0" && result.ResultCode != "126") {
		throw new PaymentFailureException(result);
	    }

	    // Check AVS responses
	    if (!config.AllowAddressMismatch || !config.AllowPostalCodeMismatch) {
		if (address.Length > 0 && !config.AllowAddressMismatch) {
		    if (result.ValidAddress.IsFalse) {
			VoidCommand command = new VoidCommand(result.TransactionId);
			command.Execute();
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
		if (postalCode.Length > 0 && !config.AllowPostalCodeMismatch) {
		    if (result.ValidPostalCode.IsFalse) {
			VoidCommand command = new VoidCommand(result.TransactionId);
			command.Execute();
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
	    }

	    // Check CVV responses
	    if (!config.AllowCvvMismatch) {
		if (result.CVVResponseCode == "N") {
		    VoidCommand command = new VoidCommand(result.TransactionId);
		    command.Execute();
		    throw new CvvValidationException(result, result.TransactionId);
		}
	    }
	}
    	
    }
}
