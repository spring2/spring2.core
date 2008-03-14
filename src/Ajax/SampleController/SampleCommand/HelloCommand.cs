using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class HelloCommand : Command {

	public static readonly HelloCommand Instance = new HelloCommand();

	public HelloCommand(Int32 responseHandlerId, String commandIdentifier, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, commandIdentifier, values, formatter, errors, cookies) { }
	protected HelloCommand() { }

	protected override String Execute() {
	    String result = "Hello";
	    String status = "success";

	    return String.Format("<command id='{0}' status='{1}' message='{2}'></command>", responseHandlerId, status, result);
	}
    }
}
