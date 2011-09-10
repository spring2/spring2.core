using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using Spring2.Core.Types;
using Spring2.Core.Configuration;
using Spring2.Core.Payment.Web_References.com.concordebiz.efsnet;

namespace Spring2.Core.Payment.EFSNet {

    /// <summary>
    /// Concord EFSnet payment provider.
    /// </summary>
    public class EFSNetProvider : BasePaymentProvider, IPaymentProvider {
    	
	public static readonly Int32 DEFAULT_TIMEOUT = 100000;
    	
	private Int32 timeout = DEFAULT_TIMEOUT;

	public int Timeout {
	    get { return timeout; }
	    set { timeout = value; }
	}

	public PaymentResult Authorize(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment, StringType originalTransactionId) {
	    return Authorize(referenceNumber, amount, accountNumber, expirationYear, expirationMonth, cvv, name, address, postalCode, comment);
	}

	public PaymentResult Authorize(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;

	    String transactionId;
	    String avsResponseCode;
	    String cvvResponseCode; 
	    String approvalNumber;
	    String authenticationNumber;
	    String transactionDate;
	    String transactionTime;
	    try {
		responseCode = proxy.CreditCardAuthorize(storeID, storeKey, applicationId, "", referenceNumber, "", amount.ToString("F2"), "", "", accountNumber, expirationMonth, expirationYear, cvv, "", "", name, address, "", "", postalCode, "", "", "", "", "", "", "", "", "", "", "", "", out resultCode, out resultMessage, out transactionId, out avsResponseCode, out cvvResponseCode, out approvalNumber,  out authenticationNumber, out transactionDate, out transactionTime);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    result.TransactionId = transactionId;
	    result.AVSResponseCode = avsResponseCode;
	    result.CVVResponseCode = cvvResponseCode;
	    result.ApprovalNumber = approvalNumber;
	    result.AuthorizationNumber = authenticationNumber;
	    if (transactionDate.Length > 0) {
		Int32 yy = Int32.Parse(transactionDate.Substring(0, 2));
		Int32 mm = Int32.Parse(transactionDate.Substring(2, 2));
		Int32 dd = Int32.Parse(transactionDate.Substring(4, 2));
		Int32 hh = Int32.Parse(transactionTime.Substring(0, 2));
		Int32 nn = Int32.Parse(transactionTime.Substring(2, 2));
		Int32 ss = Int32.Parse(transactionTime.Substring(4, 2));
		result.TransactionDateTime = new DateTime(yy, mm, dd, hh, nn, ss);
	    }
	    result.TransactionAmount = amount;
	    result.ReferenceNumber = referenceNumber;
	    return result;
	}


	public PaymentResult Charge(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;

	    String transactionId;
	    String avsResponseCode;
	    String cvvResponseCode; 
	    String approvalNumber;
	    String authenticationNumber;
	    String transactionDate;
	    String transactionTime;
	    try {
		responseCode = proxy.CreditCardCharge(storeID, storeKey, applicationId, "", referenceNumber, "", amount.ToString("F2"), "", "", accountNumber, expirationMonth, expirationYear, cvv, "", "", name, address, "", "", postalCode, "", "", "", "", "", "", "", "", "", "", "", "", out resultCode, out resultMessage, out transactionId, out avsResponseCode, out cvvResponseCode, out approvalNumber,  out authenticationNumber, out transactionDate, out transactionTime);
	    } catch (WebException ex) {
		proxy.Timeout = DEFAULT_TIMEOUT;
		responseCode = proxy.TimeOutReversal(storeID, storeKey, applicationId, referenceNumber, amount.ToString("F2"), out resultCode, out resultMessage);
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    result.TransactionId = transactionId;
	    result.AVSResponseCode = avsResponseCode;
	    result.CVVResponseCode = cvvResponseCode;
	    result.ApprovalNumber = approvalNumber;
	    result.AuthorizationNumber = authenticationNumber;
	    if (transactionDate.Length > 0) {
		Int32 yy = Int32.Parse(transactionDate.Substring(0, 2));
		Int32 mm = Int32.Parse(transactionDate.Substring(2, 2));
		Int32 dd = Int32.Parse(transactionDate.Substring(4, 2));
		Int32 hh = Int32.Parse(transactionTime.Substring(0, 2));
		Int32 nn = Int32.Parse(transactionTime.Substring(2, 2));
		Int32 ss = Int32.Parse(transactionTime.Substring(4, 2));
		result.TransactionDateTime = new DateTime(yy, mm, dd, hh, nn, ss);
	    }
	    result.TransactionAmount = amount;
	    result.ReferenceNumber = referenceNumber;
	
	    CheckForFailures(address, postalCode, result);
	    
	    return result;
	}
    	
	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;

	    String transactionId;
	    String approvalNumber;
	    String transactionDate;
	    String transactionTime;
	    try {
		responseCode = proxy.CreditCardCredit(storeID, storeKey, applicationId, "", referenceNumber, "", amount.ToString("F2"), "", "", accountNumber, expirationMonth, expirationYear, "", "", name, address, "", "", postalCode, "", "", "", "", out resultCode, out resultMessage, out transactionId, out approvalNumber,  out transactionDate, out transactionTime);
	    } catch (WebException ex) {
		proxy.Timeout = DEFAULT_TIMEOUT;
		responseCode = proxy.TimeOutReversal(storeID, storeKey, applicationId, referenceNumber, amount.ToString("F2"), out resultCode, out resultMessage);
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    result.TransactionId = transactionId;
	    result.ApprovalNumber = approvalNumber;
	    if (transactionDate.Length > 0) {
		Int32 yy = Int32.Parse(transactionDate.Substring(0, 2));
		Int32 mm = Int32.Parse(transactionDate.Substring(2, 2));
		Int32 dd = Int32.Parse(transactionDate.Substring(4, 2));
		Int32 hh = Int32.Parse(transactionTime.Substring(0, 2));
		Int32 nn = Int32.Parse(transactionTime.Substring(2, 2));
		Int32 ss = Int32.Parse(transactionTime.Substring(4, 2));
		result.TransactionDateTime = new DateTime(yy, mm, dd, hh, nn, ss);
	    }
	    result.TransactionAmount = amount;
	    result.ReferenceNumber = referenceNumber;
	    return result;
	}
	
	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;

	    String transactionId;
	    String approvalNumber;
	    String transactionDate;
	    String transactionTime;
	    try {
		responseCode = proxy.CreditCardRefund(storeID, storeKey, applicationId, referenceNumber, "", amount.ToString("F2"), "", originalTransactionId, originalTransactionAmount.ToString("F2"), "", out resultCode, out resultMessage, out transactionId, out approvalNumber,  out transactionDate, out transactionTime);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    result.TransactionId = transactionId;
	    result.ApprovalNumber = approvalNumber;
	    if (transactionDate.Length > 0) {
		Int32 yy = Int32.Parse(transactionDate.Substring(0, 2));
		Int32 mm = Int32.Parse(transactionDate.Substring(2, 2));
		Int32 dd = Int32.Parse(transactionDate.Substring(4, 2));
		Int32 hh = Int32.Parse(transactionTime.Substring(0, 2));
		Int32 nn = Int32.Parse(transactionTime.Substring(2, 2));
		Int32 ss = Int32.Parse(transactionTime.Substring(4, 2));
		result.TransactionDateTime = new DateTime(yy, mm, dd, hh, nn, ss);
	    }
	    result.TransactionAmount = amount;
	    result.ReferenceNumber = referenceNumber;
	    return result;
	}

	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;

	    String transactionId;
	    String approvalNumber;
	    String transactionDate;
	    String transactionTime;
	    try {
		responseCode = proxy.CreditCardSettle(storeID, storeKey, applicationId, referenceNumber, "", amount.ToString("F2"), "", originalTransactionId, originalTransactionAmount.ToString("F2"), "", out resultCode, out resultMessage, out transactionId, out approvalNumber,  out transactionDate, out transactionTime);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    result.TransactionId = transactionId;
	    result.ApprovalNumber = approvalNumber;
	    if (transactionDate.Length > 0) {
		Int32 yy = Int32.Parse(transactionDate.Substring(0, 2));
		Int32 mm = Int32.Parse(transactionDate.Substring(2, 2));
		Int32 dd = Int32.Parse(transactionDate.Substring(4, 2));
		Int32 hh = Int32.Parse(transactionTime.Substring(0, 2));
		Int32 nn = Int32.Parse(transactionTime.Substring(2, 2));
		Int32 ss = Int32.Parse(transactionTime.Substring(4, 2));
		result.TransactionDateTime = new DateTime(yy, mm, dd, hh, nn, ss);
	    }
	    result.TransactionAmount = amount;
	    result.ReferenceNumber = referenceNumber;
	    return result;
	}
	
	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;
	    try {
		responseCode = proxy.VoidTransaction(storeID, storeKey, applicationId, transactionId, out resultCode, out resultMessage);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    return result;
	}

	public PaymentResult SystemCheck() {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String resultCode;
	    String resultMessage;
	    try {
		responseCode = proxy.SystemCheck(storeID, storeKey, applicationId, out resultCode, out resultMessage);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }

	    PaymentResult result = new PaymentResult();
	    result.ResponseCode = responseCode;
	    result.ResultCode = resultCode;
	    result.ResultMessage = resultMessage;
	    return result;
	}

	private EFSnet2 GetWebServiceProxy(out string applicationId, out string storeID, out string storeKey) {
	    EFSNetProviderConfiguration config = new EFSNetProviderConfiguration();
	    storeID = config.StoreId;
	    storeKey = config.StoreKey;
	    applicationId = config.ApplicationId;

	    EFSnet2 ws = new EFSnet2();
	    ws.Url = config.WebSeviceUrl;
	    // TODO: should this use timeout from config?
	    ws.Timeout = Timeout;
	    return ws;
	}

	public QueryTransactionsResultTransaction GetTransaction(StringType transactionId) {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String itemCount;
	    String queryDataSize;
	    String queryData;
	    try {
		responseCode = proxy.QueryTransactions(storeID, storeKey, applicationId, "", transactionId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", out itemCount, out queryDataSize, out queryData);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }
    		
	    // deserialize results
	    XmlSerializer s = new XmlSerializer( typeof( QueryTransactionsResult ) );
	    TextReader r = new StringReader(queryData);
	    QueryTransactionsResult result = (QueryTransactionsResult)s.Deserialize( r );
	    r.Close();    	    

	    return result.Transaction[0];
	}
    	
	public QueryTransactionsResult QueryTransactions() {
	    string applicationId;
	    string storeID;
	    string storeKey;
	    EFSnet2 proxy = GetWebServiceProxy(out applicationId, out storeID, out storeKey);
		
	    Int32 responseCode;
	    String itemCount;
	    String queryDataSize;
	    String queryData;
	    try {
		responseCode = proxy.QueryTransactions(storeID, storeKey, applicationId, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", out itemCount, out queryDataSize, out queryData);
	    } catch (WebException ex) {
		throw new PaymentConnectionException(ex.Message);
	    }
    		
	    // deserialize results
	    XmlSerializer s = new XmlSerializer( typeof( QueryTransactionsResult ) );
	    TextReader r = new StringReader(queryData);
	    QueryTransactionsResult result = (QueryTransactionsResult)s.Deserialize( r );
	    r.Close();    	    
    		
	    return result;
	}
    	
	private void CheckForFailures(StringType address, StringType postalCode, PaymentResult result) {
	    EFSNetProviderConfiguration config = new EFSNetProviderConfiguration();
    		
	    if (result.ResultCode != "00") {
		throw new PaymentFailureException(result);
	    }

	    // Check AVS responses
	    if (!config.AllowAddressMismatch || !config.AllowPostalCodeMismatch) {
		// TODO: need to get card type and host to determine what the actual response codes mean.
		if (address.Length > 0 && !config.AllowAddressMismatch) {
		    String invalidAddressCodes = "CINPWZ";
		    if (invalidAddressCodes.IndexOf(result.AVSResponseCode) >= 0) {
			result.ValidAddress = BooleanType.FALSE;
			Void(result.ReferenceNumber, result.TransactionId);
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
		if (postalCode.Length > 0 && !config.AllowPostalCodeMismatch) {
		    String invalidAddressCodes = "ABCN";
		    if (invalidAddressCodes.IndexOf(result.AVSResponseCode) >= 0) {
			Void(result.ReferenceNumber, result.TransactionId);
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
	    }

	    // TODO: always check the response and populate the result.Valid* properties
	    // TODO: thow here if result is not valid and value was entered and allow mismatch is false
	    //	    if ()    		
	    //	    Void(result.TransactionId);
	    //	    throw new AddressDoesNotMatchException(result, result.TransactionId);
    	
	    // Check CVV responses
	    if (!config.AllowCvvMismatch) {
		if (result.CVVResponseCode == "N") {
		    Void(result.ReferenceNumber, result.TransactionId);
		    throw new CvvValidationException(result, result.TransactionId);
		}
	    }
	}
    }
}
