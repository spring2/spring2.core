using System;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

using log4net;

//using Spring2.Dss.Payment;
using Spring2.Core.Types;
using Spring2.Core.Payment;
using Spring2.Core.Configuration;

namespace Spring2.Core.Payment.ProPay {
    /// <summary>
    /// Summary description for PayflowProCommand.
    /// </summary>
    public abstract class ProPayCommand {

	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
	ProPayProviderConfiguration config = new ProPayProviderConfiguration();

	public ProPayCommand() {
	    init();
	}

	private void init() {
	}

    	public virtual ProPayResult Execute() {
	    ProPayResult proPayResult = null;
	    //log.Info(string.Format("{0} ~ Submitting payment: {1} ~ RemoteAddress: {2}:{3}", correlationId, CommandText, config.HostAddress, config.HostPort));
	    String xmlResponse = SubmitTransaction();
	    //log.Info(string.Format("{0} ~ ResultMessage: {1} ~ RemoteAddress: {2}:{3}", correlationId, response, config.HostAddress, config.HostPort));

	    proPayResult = ParseResult(xmlResponse);
	    CheckForFailures(proPayResult);
	    return proPayResult;
    	}
    	
    	public abstract String CommandText {
    	    get;
    	}
    	

	private ProPayResult ParseResult(String resultXML) {
	    ProPayResult result = new ProPayResult();
	    result.RawResponse = resultXML;
	    if (resultXML.Length > 0) {
		StringReader reader = new StringReader(resultXML);
		XmlTextReader xmlReader = new XmlTextReader(reader);
		String lastNodeName = String.Empty;
		while (xmlReader.Read()) {
		    XmlNodeType nodeType = xmlReader.NodeType;
		    if (nodeType == XmlNodeType.Element) {
			lastNodeName = xmlReader.Name.Trim().ToLower();
		    } else if (nodeType == XmlNodeType.Text) {
			switch (lastNodeName) {
			    case "transtype":
				result.TransType = xmlReader.Value;
				break;
			    case "sourceemail":
				result.SourceEmail = xmlReader.Value;
				break;
			    case "password":
				result.Password = xmlReader.Value;
				break;
			    case "accntnum":
				result.AccntNum = xmlReader.Value;
				break;
			    case "tier":
				result.Tier = xmlReader.Value;
				break;
			    case "status":
				result.Status = xmlReader.Value;
				break;
			    case "invnum":
				result.InvNum = xmlReader.Value;
				break;
			    case "transnum":
				result.TransNum = xmlReader.Value;
				break;
			    case "authcode":
				result.AuthCode = xmlReader.Value;
				break;
			    case "avs":
				result.AVS = xmlReader.Value;
				break;
			    case "cvv2resp":
				result.CVV2Resp = xmlReader.Value;
				break;
			    case "resp":
				result.Resp = xmlReader.Value;
				break;
			    case "accountnum":
				result.AccountNum = xmlReader.Value;
				break;
			    case "amount":
				result.Amount = CurrencyType.Parse(xmlReader.Value);
				break;
			    case "":
				log.Info("Unrecognized element: " + xmlReader.Name);
				break;
			}
		    } else {
			// this node has no interesting contents, so wipe the value
			lastNodeName = String.Empty;
		    }
		}
	    }

	    return result;
	}

	protected void CheckForFailures(ProPayResult propayResult) {
	    if(!propayResult.CVV2Resp.IsValid) {
		throw new InvalidValueException("'CVV2Resp' was not set on result");
	    } else switch (propayResult.CVV2Resp) {
		    case "":
		    case "N":
		    case "P":
		    case "S":
		    case "U":
			propayResult.ValidCvv = BooleanType.FALSE;
			break;
		    default:
			propayResult.ValidCvv = BooleanType.TRUE;
			break;
	    }

	    if(!propayResult.AVS.IsValid) {
		throw new InvalidValueException("'AVS' was not set on result");
	    } else switch(propayResult.AVS) {
		    case "":
		    case "U":
		    case "G":
		    case "C":
		    case "I":
		    case "S":
		    case "R":
			propayResult.ValidAddress = BooleanType.FALSE;
			propayResult.ValidPostalCode = BooleanType.FALSE;
			break;
		    default:
			propayResult.ValidAddress = BooleanType.TRUE;
			propayResult.ValidPostalCode = BooleanType.TRUE;
			break;
	    }

	    if (!propayResult.Status.IsValid) {
		throw new InvalidValueException("'Status' was not set on result");
	    } else switch (propayResult.Status) {
		case "00": // Success
		case "69": // Duplicate invoice, but was successful on prior attempt (see the RawResult for further details)
		case "99": // Success, but account still must be paid for
		    break;
		default:
		    String errorMessage = LookupErrorResultMessage(propayResult.Status);
		    propayResult.ResultMessage = errorMessage;
		    throw new PaymentFailureException(propayResult, "Failed to process the payment. {0}: {1}.", propayResult.Status, "(ProPay) '" + errorMessage + "'");
	    }
		
	}

	protected String SubmitTransaction() {
	    String resultXML = PostCommandToHost2();
	    return resultXML;
	}

	// This version allows timeout values to be set
	// original, unmodified code found at http://p2p.wrox.com/topic.asp?TOPIC_ID=59529
	private String PostCommandToHost2() {
	    //Declare XMLResponse document
	    //XmlDocument XMLResponse = null;

	    //Declare an HTTP-specific implementation of the WebRequest class.
	    HttpWebRequest objHttpWebRequest;

	    //Declare an HTTP-specific implementation of the WebResponse class
	    HttpWebResponse objHttpWebResponse = null;

	    //Declare a generic view of a sequence of bytes
	    Stream objRequestStream = null;
	    Stream objResponseStream = null;

	    //Declare XMLReader
	    StreamReader resultReader;

	    String result = String.Empty;

	    //Creates an HttpWebRequest for the specified URL.
	    objHttpWebRequest = ( HttpWebRequest )WebRequest.Create(config.HostAddress);
	    objHttpWebRequest.Timeout = config.Timeout;

	    try {
		//---------- Start HttpRequest

		//Set HttpWebRequest properties
		byte[] bytes;
		bytes = config.XMLEncoding.GetBytes(CommandText);
		objHttpWebRequest.Method = "POST";
		objHttpWebRequest.ContentLength = bytes.Length;
		objHttpWebRequest.ContentType = "text/xml; encoding='" + ConfigurationProvider.Instance.Settings[ProPayProviderConfiguration.XMLENCODING_KEY] + "'";

		//Get Stream object
		objRequestStream = objHttpWebRequest.GetRequestStream();

		//Writes a sequence of bytes to the current stream        
		objRequestStream.Write(bytes, 0, bytes.Length);

		//Close stream
		objRequestStream.Close();

		//---------- End HttpRequest

		//Sends the HttpWebRequest, and waits for a response.
		objHttpWebResponse = ( HttpWebResponse )objHttpWebRequest.GetResponse();

		//---------- Start HttpResponse
		if (objHttpWebResponse.StatusCode == HttpStatusCode.OK) {

		    //Get response stream
		    objResponseStream = objHttpWebResponse.GetResponseStream();
		    //objResponseStream.WriteTimeout = Math.Min(objResponseStream.WriteTimeout, config.Timeout); --> functionality does not exist in early .NET versions

		    //Load response stream into XMLReader
		    resultReader = new StreamReader(objResponseStream);

		    result = resultReader.ReadToEnd();
		    
		    //Close XMLReader
		    resultReader.Close();
		}

		//Close HttpWebResponse
		objHttpWebResponse.Close();
	    } catch (WebException ex) {
		throw ex;
	    } catch (Exception) {
		throw;
	    } finally {
		//Close connections
		if (objRequestStream != null) {
		    objRequestStream.Close();
		    objRequestStream = null;
		}
		if (objResponseStream != null) {
		    objResponseStream.Close();
		    objResponseStream = null;
		}
		if (objHttpWebResponse != null) {
		    objHttpWebResponse.Close();
		    objHttpWebResponse = null;
		}

		//Release remaining objects
		resultReader = null;
		objHttpWebRequest = null;
	    }

	    //Return
	    return result;
	}

	private String PostCommandToHost() {
	    String result = String.Empty;
	    Byte[] dataFromHost = null;
	    try {
		WebClient client = new WebClient();
		
		// we can't do the following until Core doesn't compile with early .NET versions
		// not supported in earlier .NET versions --> result = client.UploadString(config.HostAddress, CommandText);
		
		// use this instead of the commented line, above, so long as Core compiles with early .NET versions
		Byte[] xmlAsBytes = config.XMLEncoding.GetBytes(CommandText);
		//Byte[] xmlAsBytes = Encoding.UTF8.GetBytes(CommandText);
		dataFromHost = client.UploadData(config.HostAddress, xmlAsBytes);
		StringBuilder sb = new StringBuilder();
		foreach(Byte b in dataFromHost) {
		    sb.Append((char)b);
		}
		result = sb.ToString();

		
	    } catch (WebException webEx) {
		log.Error(webEx.Message);
		Exception innerException = webEx.InnerException;
		if (innerException != null) {
		    log.Error(innerException.Message);
		}
		throw;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw;
	    }
	    return result;
	}

	public static String LookupAVSResultMessage(String code) {
	    switch(code) {
		case "A": return "Address Match (A)";
		case "Z": return "Zip Match (Z)";
		case "Y": return "Exact Match (Y)";
		case "U": return "Verification Unavailable (U)";
		case "G": return "Verification Unavailable (G)";
		case "B": return "Address Match (B)";
		case "C": return "Service Unavailable or offline";
		case "D": return "Exact Match (D)";
		case "I": return "Verification Unavailable";
		case "M": return "Exact Match (M)";
		case "P": return "Zip Match (P)";
		case "S": return "Service Not Supported";
		case "R": return "Issuer System Unavailable";
	    }
	    return "";
	}

	public static String LookupCVVResultMessage(String code) {
	    switch(code) {
		case "M": return "CVV2 Match";
		case "N": return "CVV2 No Match";
		case "P": return "Not Processed";
		case "S": return "Merchant has indicated that CVV2 is not present on card";
		case "U": return "Issuer is not certified and/or has not provided Visa encryption keys";
	    }
	    return "";
	}

	private static String LookupErrorResultMessage(String code) {
	    switch(code) {
		case "00": return "Success";
		case "20": return "Invalid username";
		case "21": return "Invalid transType";
		case "23": return "Invalid accountType";
		case "24": return "Invalid sourceEmail";
		case "25": return "Invalid firstName";
		case "26": return "Invalid mInitial";
		case "27": return "Invalid lastName";
		case "28": return "Invalid billAddr";
		case "29": return "Invalid aptNum";
		case "30": return "Invalid city";
		case "31": return "Invalid state";
		case "32": return "Invalid billZip";
		case "33": return "Invalid mailAddr";
		case "34": return "Invalid mailApt";
		case "35": return "Invalid mailCity";
		case "36": return "Invalid mailState";
		case "37": return "Invalid mailZip";
		case "38": return "Invalid dayPhone";
		case "39": return "Invalid evenPhone";
		case "40": return "Invalid ssn";
		case "41": return "Invalid dob";
		case "42": return "Invalid recEmail";
		case "43": return "Invalid knownAccount";
		case "44": return "Invalid amount";
		case "45": return "Invalid invNum";
		case "46": return "Invalid rtNum";
		case "47": return "Invalid accntNum";
		case "48": return "Invalid ccNum";
		case "49": return "Invalid expDate";
		case "50": return "Invalid cvv2";
		case "51": return "Invalid transNum or unavailable to act on transNum due to funding";
		case "52": return "Invalid splitNum";
		case "53": return "A ProPay account with this e-mail address already exists or User has no AccountNumber";
		case "54": return "A ProPay account with this social security number already exists";
		case "55": return "Recipient's e-mail address should have a ProPay account and doesn't";
		case "56": return "Recipient's e-mail address shouldn't have a ProPay account and does";
		case "57": return "Cannot settle transaction because it already expired";
		case "58": return "Credit card declined";
		case "59": return "User not authenticated";
		case "60": return "Credit card authorization timed out; retry at a later time";
		case "61": return "Amount exceeds single transaction limit";
		case "62": return "Amount exceeds monthly volume limit";
		case "63": return "Insufficient funds in account";
		case "64": return "Over credit card use limit";
		case "65": return "Miscellaneous error";
		case "66": return "Denied a ProPay account";
		case "67": return "Unauthorized service requested";
		case "68": return "Account not affiliated";
		case "69": return "Duplicate invoice number"; // (Transaction succeeded in a prior attempt within the previous 24 hours.  This return code should be handled as a success.  Details about the original transaction are included whenever a 69 response is returned.  These details include a repeat of the authcode, the original AVS response, and the original CVV response.)
		case "70": return "Duplicate external ID";
		case "71": return "Account previously set up, but problem affiliating it with partner";
		case "72": return "The ProPay Account has already been upgraded to a Premium Account";
		case "73": return "Invalid Destination Account";
		case "74": return "Account or Trans Error";
		case "75": return "Money already pulled";
		case "76": return "Not Premium"; // (used only for push/pull transactions)
		case "77": return "Empty results";
		case "78": return "Invalid Authentication";
		case "79": return "AddFunds requires 2 successful outgoing ACH Transactions";
		case "80": return "Invalid Password";
		case "81": return "AccountExpired";
		case "82": return "InvalidUserID";
		case "83": return "BatchTransCountError";
		case "84": return "InvalidBeginDate";
		case "85": return "InvalidEndDate";
		case "86": return "InvalidExternalID";
		case "87": return "DuplicateUserID";
		case "88": return "Invalid track 1";
		case "89": return "Invalid track 2";
		case "90": return "Transaction already refunded";
		case "91": return "Duplicate Batch ID";
		case "92": return "Duplicate Batch Transaction";
		case "93": return "Batch Transaction amount error";
		case "94": return "Unavailable Tier";
		case "99": return "Account created successfully, but still must be paid for";
	    }
	    return "";
	}

    }

    // This class allows us to set xml encoding to something other than UTF-16, which ProPay doesn't like in a drastic measure
    class VariableEncodingStringWriter : StringWriter {
	Encoding overriddenEncoding = System.Text.Encoding.UTF8;
	public VariableEncodingStringWriter(System.Text.Encoding explicitEncoding) {
	    overriddenEncoding = explicitEncoding;
	}
	public override Encoding Encoding {
	    get { return overriddenEncoding; }
	}
    }
}
