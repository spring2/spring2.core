using System;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Dss.Payment.PayflowPro {
    /// <summary>
    /// Summary description for VoidCommand.
    /// </summary>
    public class VoidCommand : PayflowProCommand {

	private String transactionId = String.Empty;
		
	public VoidCommand(StringType transactionId) {
	    this.transactionId = transactionId;
	}

		
	public override string CommandText {
	    get {
		StringBuilder command = new StringBuilder();
		AppendCommand(command, "TENDER", "C", 1);
		AppendCommand(command, "TRXTYPE", "V", 1);
		AppendCommand(command, "ORIGID", transactionId, 17);    		
		return command.ToString();
	    }
	}

    }
}
