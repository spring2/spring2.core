using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class ExtendedHelloCommand : HelloCommand {

	public static readonly ExtendedHelloCommand Instance = new ExtendedHelloCommand();

	public ExtendedHelloCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected ExtendedHelloCommand() { }

	protected override String Execute() {
	    this.HelloText = "Extended Hello";
            return base.Execute();
	}
    }
}
