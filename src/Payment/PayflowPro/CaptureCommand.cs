using System;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.PayflowPro {
    /// <summary>
    /// Summary description for SaleCommand.
    /// </summary>
    public class CaptureCommand : PayflowProCommand {

	private StringType transactionId;
	private CurrencyType amount; 
		
	public override PaymentResult Execute() {
	    PaymentResult result = base.Execute ();
	    CheckForFailures("", "", result);
		
	    return result;
	}

	public CaptureCommand(StringType transactionId, CurrencyType amount) {
	    this.transactionId = transactionId;
	    this.amount = amount;
	}

		
	public override string CommandText {
	    get {
		StringBuilder command = new StringBuilder();
		AppendCommand(command, "TRXTYPE", "D", 1);
		AppendCommand(command, "TENDER", "C", 1);
		AppendCommand(command, "ORIGID", transactionId, 12);
		AppendCommand(command, "AMT", amount.ToString("######0.00"), 10);
		return command.ToString();
	    }
	}

    }
}
