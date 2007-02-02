using System;
using System.Reflection;

using log4net;

using Moneris;

using Spring2.Core.Types;

using NotImplementedException=System.NotImplementedException;

namespace Spring2.Core.Payment.Moneris {
    /// <summary>
    /// Summary description for MonerisProvider.
    /// </summary>
    public class MonerisProvider : IPaymentProvider {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    	
	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber,
		StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name,
		StringType address, StringType postalCode, StringType comment) {
	    throw new NotImplementedException();
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear,
	    StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode,
	    StringType comment) {
	    
	    MonerisProviderConfiguration config = new MonerisProviderConfiguration();
		
	    AvsInfo avsCheck = new AvsInfo();	
	    avsCheck.SetAvsStreetNumber(GetStreetNumberFromAddress(address));
	    avsCheck.SetAvsStreetName (GetStreetNameFromAddress(address));
	    avsCheck.SetAvsZipCode (postalCode);
		
	    /****************** Card Validation Digits *************************/
	       	
	    CvdInfo cvdCheck = new CvdInfo();
	    if (cvv.Length > 0) {
	    	cvdCheck.SetCvdIndicator ("1");
	    	cvdCheck.SetCvdValue (cvv);
	    } else {
	    	cvdCheck.SetCvdIndicator ("0");
	    }
	       	
	    Purchase purchase = new Purchase(referenceNumber, amount.ToString("F2"), accountNumber.ToString(), expirationYear + expirationMonth, config.Crypt);
	    if (address.Length > 0 || postalCode.Length > 0) {
	    	purchase.SetAvsInfo (avsCheck);
	    }
	    purchase.SetCvdInfo (cvdCheck);	
		
	    HttpsPostRequest mpgReq =
		new HttpsPostRequest(config.Host, config.StoreId, config.ApiToken, purchase, true);
	
	    Receipt receipt = mpgReq.GetReceipt();
	    PaymentResult result = GetResultFromReceipt(config, receipt, referenceNumber, address, postalCode);
	    return result;
	}
    	
    	private String GetStreetNumberFromAddress(String address) {
    	    return address;
    	}

	private String GetStreetNameFromAddress(String address) {
	    return address;
	}

	private PaymentResult GetResultFromReceipt(MonerisProviderConfiguration config, Receipt receipt, StringType referenceNumber, StringType address, StringType postalCode) {
	    Console.WriteLine("CardType = " + receipt.GetCardType());
	    Console.WriteLine("TransAmount = " + receipt.GetTransAmount());
	    Console.WriteLine("TxnNumber = " + receipt.GetTxnNumber());
	    Console.WriteLine("ReceiptId = " + receipt.GetReceiptId());
	    Console.WriteLine("TransType = " + receipt.GetTransType());
	    Console.WriteLine("ReferenceNum = " + receipt.GetReferenceNum());
	    Console.WriteLine("ResponseCode = " + receipt.GetResponseCode());
	    Console.WriteLine("ISO = " + receipt.GetISO());
	    Console.WriteLine("BankTotals = " + receipt.GetBankTotals());
	    Console.WriteLine("Message = " + receipt.GetMessage());
	    Console.WriteLine("AuthCode = " + receipt.GetAuthCode());
	    Console.WriteLine("Complete = " + receipt.GetComplete());
	    Console.WriteLine("TransDate = " + receipt.GetTransDate());
	    Console.WriteLine("TransTime = " + receipt.GetTransTime());
	    Console.WriteLine("Ticket = " + receipt.GetTicket());
	    Console.WriteLine("TimedOut = " + receipt.GetTimedOut());
	    Console.WriteLine("AVS Result Code = " + receipt.GetAvsResultCode());
	    Console.WriteLine("CVD Result Code = " + receipt.GetCvdResultCode());
	    	
	    /*
	    CardType = V
	    TransAmount = 205.00
	    TxnNumber = 238590-974-0
	    ReceiptId = 96206212
	    TransType = 00
	    ReferenceNum = 660021630017149740
	    ResponseCode = 027
	    ISO = 01
	    BankTotals = 
	    Message = APPROVED           *                    =
	    AuthCode = 010280
	    Complete = true
	    TransDate = 2006-12-12
	    TransTime = 11:13:04
	    Ticket = null
	    TimedOut = false
	    
	    CardType = V
	    TransAmount = 10.40
	    TxnNumber = 57407-191-0
	    ReceiptId = 80449659
	    TransType = 00
	    ReferenceNum = 660053720010231910
	    ResponseCode = 027
	    ISO = 01
	    BankTotals = 
	    Message = APPROVED           *                    =
	    AuthCode = 645658
	    Complete = true
	    TransDate = 2006-12-27
	    TransTime = 18:29:46
	    Ticket = null
	    TimedOut = false
	    */

	    PaymentResult result = new PaymentResult();
	    if (receipt.GetComplete() == "false") {
	    	throw new PaymentConnectionException(receipt.GetReceiptId());
	    } else if (receipt.GetTimedOut() == "true") {
	    	throw new PaymentConnectionException("timeout");
	    } else {
		result.ResponseCode = Int32.Parse(receipt.GetResponseCode());
		result.ResultCode = receipt.GetResponseCode();
		result.ResultMessage = ParseMessageFromReceipt(receipt);
		result.TransactionId = receipt.GetTxnNumber();
		result.AVSResponseCode = receipt.GetAvsResultCode() == null ? "" : receipt.GetAvsResultCode();
		result.CVVResponseCode = receipt.GetCvdResultCode() == null ? "" : receipt.GetCvdResultCode();
		result.ApprovalNumber = receipt.GetAuthCode();
		result.AuthorizationNumber = receipt.GetAuthCode();
		result.TransactionDateTime = DateTime.Parse(receipt.GetTransDate() + " " + receipt.GetTransTime());
		result.TransactionAmount = CurrencyType.Parse(receipt.GetTransAmount());
		result.ReferenceNumber = referenceNumber;
		CheckForAvsFailure(config, result, address, postalCode);
		CheckForCvvFailure(config, result);
	    }
	    return result;
	}
    	
    	private void CheckForAvsFailure(MonerisProviderConfiguration config, PaymentResult result, StringType address, StringType postalCode) {
	    // Check AVS responses
	    if (result.AVSResponseCode.Length > 0 && (!config.AllowAddressMismatch || !config.AllowPostalCodeMismatch)) {
		// TODO: need to get card type and host to determine what the actual response codes mean.
		if (address.Length > 0 && !config.AllowAddressMismatch) {
		    String invalidAddressCodes = "NZ";
		    if (invalidAddressCodes.IndexOf(result.AVSResponseCode) >= 0) {
			result.ValidAddress = BooleanType.FALSE;
			Void(result.ReferenceNumber, result.TransactionId);
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
		if (postalCode.Length > 0 && !config.AllowPostalCodeMismatch) {
		    String invalidAddressCodes = "AN";
		    if (invalidAddressCodes.IndexOf(result.AVSResponseCode) >= 0) {
			Void(result.ReferenceNumber, result.TransactionId);
			throw new AvsValidationException(result, result.TransactionId);
		    }
		}
	    }

    	}
    	
	private void CheckForCvvFailure(MonerisProviderConfiguration config, PaymentResult result) {
	    // Check CVV responses
	    if (result.CVVResponseCode.Length > 0 && !config.AllowCvvMismatch) {
		if (result.CVVResponseCode == "1N") {
		    Void(result.ReferenceNumber, result.TransactionId);
		    throw new CvvValidationException(result, result.TransactionId);
		}
	    }
	}
	
    	private String ParseMessageFromReceipt(Receipt receipt) {
    	    String message = receipt.GetMessage();
    	    if (message.EndsWith("=")) {
    		message = message.Substring(0, message.Length - 1);
    	    }
    	    
    	    String[] messages = message.Split('*');
    	    message = String.Empty;
    	    foreach (String s in messages) {
    	    	if (s.Trim().Length > 0) {
    	    	    if (message.Length > 0) {
    	    		message += " ";
    	    	    }
    	    	    message += s.Trim();
    	    	}
    	    }
    	    return message;
    	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear,
	    StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode,
	    StringType comment, StringType originalTransactionId) {
	    
	    MonerisProviderConfiguration config = new MonerisProviderConfiguration();
		
	    Refund refund = new Refund(referenceNumber, amount.ToString("F2"), originalTransactionId, config.Crypt);
	    HttpsPostRequest mpgReq = new HttpsPostRequest(config.Host, config.StoreId, config.ApiToken, refund);
					
	    Receipt receipt = mpgReq.GetReceipt();
	    PaymentResult result = GetResultFromReceipt(config, receipt, referenceNumber, address, postalCode);
	    return result;
	}

	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId,
	    CurrencyType originalTransactionAmount) {
	    throw new NotImplementedException();
	}

	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId,
	    CurrencyType originalTransactionAmount) {
	    throw new NotImplementedException();
	}

	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    MonerisProviderConfiguration config = new MonerisProviderConfiguration();

		HttpsPostRequest mpgReq =
		    new HttpsPostRequest(config.Host, config.StoreId, config.ApiToken,
		new PurchaseCorrection(referenceNumber, transactionId, config.Crypt));
		
	    Receipt receipt = mpgReq.GetReceipt();
	    PaymentResult result = GetResultFromReceipt(config, receipt, "", "", "");
	    return result;
	}
    }
}