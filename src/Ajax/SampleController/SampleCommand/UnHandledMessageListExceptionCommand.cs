using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class UnHandledMessageListExceptionCommand : Command {

	public static readonly UnHandledMessageListExceptionCommand Instance = new UnHandledMessageListExceptionCommand();

	public UnHandledMessageListExceptionCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected UnHandledMessageListExceptionCommand() { }

	protected override String Execute() {
	    MessageList errors = new MessageList();
	    errors.Add(new MissingRequiredFieldError("FieldNameHere Special Char(%26 ' \" & & + + ''')"));
	    throw new MessageListException(errors);
	}
    }
}
