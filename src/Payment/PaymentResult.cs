using System;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.Payment {
    /// <summary>
    /// Summary description for PaymentResponse.
    /// </summary>
    public class PaymentResult {

	private String resultCode = String.Empty;
	private String resultMessage = String.Empty;
	private Int32 responseCode = 0;
	private String transactionId = String.Empty;
	private String aVSResponseCode = String.Empty;
	private String cVVResponseCode = String.Empty;
	private String approvalNumber = String.Empty;
	private String authorizationNumber = String.Empty;
	private DateTime transactionDateTime = new DateTime();
	private String referenceNumber = String.Empty;
	private String accountNumber = String.Empty;
	private CurrencyType transactionAmount = CurrencyType.DEFAULT;
    	private BooleanType validAddress = BooleanType.DEFAULT;
    	private BooleanType validPostalCode = BooleanType.DEFAULT;
    	private BooleanType validCvv = BooleanType.DEFAULT;
    	
	public String ResultCode {
	    get { return resultCode; }
	    set { resultCode = value; }
	}

	public String ResultMessage {
	    get { return resultMessage; }
	    set { resultMessage = value; }
	}

	public Int32 ResponseCode {
	    get { return responseCode; }
	    set { responseCode = value; }
	}

	public string TransactionId {
	    get { return transactionId; }
	    set { transactionId = value; }
	}

	public string AVSResponseCode {
	    get { return aVSResponseCode; }
	    set { aVSResponseCode = value; }
	}

	public string CVVResponseCode {
	    get { return cVVResponseCode; }
	    set { cVVResponseCode = value; }
	}

	public string ApprovalNumber {
	    get { return approvalNumber; }
	    set { approvalNumber = value; }
	}

	public string AuthorizationNumber {
	    get { return authorizationNumber; }
	    set { authorizationNumber = value; }
	}

	public DateTime TransactionDateTime {
	    get { return transactionDateTime; }
	    set { transactionDateTime = value; }
	}

	public string ReferenceNumber {
	    get { return referenceNumber; }
	    set { referenceNumber = value; }
	}

	public string AccountNumber {
	    get { return accountNumber; }
	    set { accountNumber = value; }
	}

	public CurrencyType TransactionAmount {
	    get { return transactionAmount; }
	    set { transactionAmount = value; }
	}

    	public BooleanType ValidAddress {
    		get { return validAddress; }
    		set { validAddress = value; }
    	}

    	public BooleanType ValidPostalCode {
    		get { return validPostalCode; }
    		set { validPostalCode = value; }
    	}

    	public BooleanType ValidCvv {
    		get { return validCvv; }
    		set { validCvv = value; }
    	}


    	public override string ToString() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("ResultCode=").Append(ResultCode).Append(Environment.NewLine);
	    sb.Append("ResultMessage=").Append(ResultMessage).Append(Environment.NewLine);
	    sb.Append("ResponseCode=").Append(ResponseCode).Append(Environment.NewLine);
	    sb.Append("TransactionId=").Append(TransactionId).Append(Environment.NewLine);
	    sb.Append("AVSResponseCode=").Append(AVSResponseCode).Append(Environment.NewLine);
	    sb.Append("CVVResponseCode=").Append(CVVResponseCode).Append(Environment.NewLine);
	    sb.Append("ApprovalNumber=").Append(ApprovalNumber).Append(Environment.NewLine);
	    sb.Append("AuthorizationNumber=").Append(AuthorizationNumber).Append(Environment.NewLine);
	    sb.Append("TransactionDate=").Append(TransactionDateTime).Append(Environment.NewLine);
	    sb.Append("ReferenceNumber=").Append(ReferenceNumber).Append(Environment.NewLine);
	    sb.Append("AccountNumber=").Append(AccountNumber).Append(Environment.NewLine);
	    sb.Append("TransactionAmount=").Append(TransactionAmount).Append(Environment.NewLine);
	    return sb.ToString();
	}

    }
}
