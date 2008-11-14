using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class UnHandledSystemExceptionCommand : Command {

	public static readonly UnHandledSystemExceptionCommand Instance = new UnHandledSystemExceptionCommand();

	public UnHandledSystemExceptionCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected UnHandledSystemExceptionCommand() { }

	protected override String Execute() {
	    throw new NotImplementedException("This is an unhandled system exception Special Char(%26 ' \" & & + + ''')");
	}
    }
}
