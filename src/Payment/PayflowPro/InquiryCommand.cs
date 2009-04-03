using System;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.PayflowPro {
    /// <summary>
    /// Summary description for VoidCommand.
    /// </summary>
    public class InquiryCommand : PayflowProCommand {

	private StringType transactionId = StringType.EMPTY;
    	private StringType referenceNumber = StringType.EMPTY;
    	private DateTimeType startTime = DateTimeType.DEFAULT;
    	private DateTimeType endTime = DateTimeType.DEFAULT;
		
	public InquiryCommand(StringType transactionId) {
	    this.transactionId = transactionId;
	}

	public InquiryCommand(StringType referenceNumber, DateTimeType startTime, DateTimeType endTime) {
	    this.referenceNumber = referenceNumber;
	    this.startTime = startTime;
	    this.endTime = endTime;
	}
		
	public override string CommandText {
	    get {
		StringBuilder command = new StringBuilder();
		AppendCommand(command, "TENDER", "C", 1);
		AppendCommand(command, "TRXTYPE", "I", 1);
		if (!transactionId.IsEmpty) {
		    AppendCommand(command, "ORIGID", transactionId, 17);
		} else {
		    AppendCommand(command, "CUSTREF", referenceNumber, 12);
		}
		return command.ToString();
	    }
	}

    }
}
