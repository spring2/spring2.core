using System;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.PayflowPro {
    /// <summary>
    /// Summary description for SaleCommand.
    /// </summary>
    public class SaleCommand : PayflowProCommand {

	private StringType accountNumber;
	private CurrencyType amount; 
	private StringType cvv2;
	private StringType referenceCode;
	private StringType expirationDate;
	private StringType name;
	private StringType address;
	private StringType postalCode;
	private StringType comment1;
	private StringType comment2;
	private StringType poNum;
	private StringType transactionId;
		
		
	public override PaymentResult Execute() {
	    PaymentResult result = base.Execute ();
	    CheckForFailures(address, postalCode, result);
		
	    return result;
	}

	public SaleCommand(StringType accountNumber, CurrencyType amount, StringType cvv2, StringType referenceCode, StringType expirationDate, StringType name, StringType address,
	    StringType postalCode, StringType comment1, StringType comment2, StringType poNum) {
    		
	    this.accountNumber = accountNumber;
	    this.amount = amount;
	    this.cvv2 = cvv2;
	    this.referenceCode = referenceCode;
	    this.expirationDate = expirationDate;
	    this.name = name;
	    this.address = address;
	    this.postalCode = postalCode;
	    this.comment1 = comment1;
	    this.comment2 = comment2;
	    this.poNum = poNum;
	}

	public SaleCommand(StringType accountNumber, CurrencyType amount, StringType cvv2, StringType referenceCode, StringType expirationDate, StringType name, StringType address,
	    StringType postalCode, StringType comment1, StringType comment2, StringType poNum, StringType transactionId) {

	    this.accountNumber = accountNumber;
	    this.amount = amount;
	    this.cvv2 = cvv2;
	    this.referenceCode = referenceCode;
	    this.expirationDate = expirationDate;
	    this.name = name;
	    this.address = address;
	    this.postalCode = postalCode;
	    this.comment1 = comment1;
	    this.comment2 = comment2;
	    this.poNum = poNum;
	    this.transactionId = transactionId;
	}
		
	public override string CommandText {
	    get {
		StringBuilder command = new StringBuilder();
		AppendCommand(command, "TENDER", "C", 1);
		AppendCommand(command, "ACCT", accountNumber, 19);
		AppendCommand(command, "AMT", amount.ToString("######0.00"), 10);
		AppendCommand(command, "CVV2", cvv2, 4);
		AppendCommand(command, "CUSTREF", referenceCode, 12);
		AppendCommand(command, "EXPDATE", expirationDate.IsValid ? expirationDate.Replace("/", string.Empty) : expirationDate, 19);
		AppendCommand(command, "NAME", name, 30);
		AppendCommand(command, "STREET", address, 30);
		AppendCommand(command, "ZIP", postalCode, 9);
		AppendCommand(command, "TRXTYPE", "S", 1);
		AppendCommand(command, "COMMENT1", comment1, 128);
		AppendCommand(command, "COMMENT2", comment2, 128);
		AppendCommand(command, "PONUM", poNum, 17);
		AppendCommand(command, "ORIGID", transactionId, 12);
		return command.ToString();
	    }
	}

    }
}
