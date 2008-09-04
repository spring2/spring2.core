using System;
using System.Text;

using Spring2.Core.Types;
using Spring2.Core.Payment;

namespace Spring2.Core.Payment.ProPay {
    public class ProPayResult : PaymentResult {

	// this is the XML received from the ProPay server
	private StringType responseXML = StringType.DEFAULT;

	private StringType transType = StringType.DEFAULT;
	private StringType sourceEmail = StringType.DEFAULT;
	private StringType password = StringType.DEFAULT;
	//private StringType accntNum = StringType.DEFAULT; --> PaymentResult.AccountNumber
	private StringType tier = StringType.DEFAULT;
	//private StringType status = StringType.DEFAULT; --> PaymentResult.ResultCode
	private StringType invNum = StringType.DEFAULT;
	//private StringType transNum = StringType.DEFAULT; --> PaymentResult.TransactionId
	//private StringType authCode = StringType.DEFAULT; --> PaymentResult.ReferenceNumber
	//private StringType avs = StringType.DEFAULT; --> PaymentResult.AVSResponseCode
	//private StringType cvv2Resp = StringType.DEFAULT; --> PaymentResult.CVVResponseCode
	//private StringType resp = StringType.DEFAULT; --> PaymentResult.ResultMessage
	//private StringType accountNum = StringType.DEFAULT; --> PaymentResult.AccountNumber
	//private CurrencyType amount = CurrencyType.DEFAULT; --> PaymentResult.TransactionAmount

	public StringType RawResponse {
	    get { return responseXML; }
	    set { responseXML = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType TransType {
	    get { return transType; }
	    set { transType = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType SourceEmail {
	    get { return sourceEmail; }
	    set { sourceEmail = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType Password {
	    get { return password; }
	    set { password = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType AccntNum {
	    get { return AccountNumber; }
	    set { AccountNumber = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType Tier {
	    get { return tier; }
	    set { tier = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType Status {
	    get { return ResultCode; }
	    set { ResultCode = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType InvNum {
	    get { return invNum; }
	    set { invNum = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType TransNum {
	    get { return TransactionId; }
	    set { TransactionId = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType AuthCode {
	    get { return ReferenceNumber; }
	    set { ReferenceNumber = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType AVS {
	    get { return AVSResponseCode; }
	    set { AVSResponseCode = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType CVV2Resp {
	    get { return CVVResponseCode; }
	    set { CVVResponseCode = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType Resp {
	    get { return ResultMessage; }
	    set { ResultMessage = value.IsValid ? value : StringType.EMPTY; }
	}

	public StringType AccountNum {
	    get { return AccountNumber; }
	    set { AccountNumber = value.IsValid ? value : StringType.EMPTY; }
	}

	public CurrencyType Amount {
	    get { return TransactionAmount; }
	    set { TransactionAmount = value; }
	}

	public override String ToString() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("transType=").Append(TransType).Append(Environment.NewLine);
	    sb.Append("sourceEmail=").Append(SourceEmail).Append(Environment.NewLine);
	    sb.Append("password=").Append(Password).Append(Environment.NewLine);
	    sb.Append("accntNum=").Append(AccntNum).Append(Environment.NewLine);
	    sb.Append("tier=").Append(Tier).Append(Environment.NewLine);
	    sb.Append("status=").Append(Status).Append(Environment.NewLine);
	    sb.Append("invNum=").Append(InvNum).Append(Environment.NewLine);
	    sb.Append("transNum=").Append(TransNum).Append(Environment.NewLine);
	    sb.Append("authCode=").Append(AuthCode).Append(Environment.NewLine);
	    sb.Append("avs=").Append(AVS).Append(Environment.NewLine);
	    sb.Append("cvv2Resp=").Append(CVV2Resp).Append(Environment.NewLine);
	    sb.Append("resp=").Append(Resp).Append(Environment.NewLine);
	    sb.Append("accountNum=").Append(AccountNum).Append(Environment.NewLine);
	    sb.Append("amount=").Append(Amount).Append(Environment.NewLine);

	    String result = sb.ToString();
	    if(result == String.Empty) {
		result = "ProPayResult.DEFAULT";
	    }

	    return result;
	}
    }
}
