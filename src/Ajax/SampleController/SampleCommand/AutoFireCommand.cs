using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class AutoFireCommand : Command {

	public static readonly AutoFireCommand Instance = new AutoFireCommand();

	public AutoFireCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected AutoFireCommand() { }

	protected override String Execute() {
	    String result = DateTime.Now.ToString();
	    String status = "success";

            JsonAjaxUtility util = new JsonAjaxUtility();
            Hashtable response = new Hashtable();
            response.Add("status", status);
            response.Add("message", result);
            return util.SerializeResponse(this.responseHandlerId, response);
	}
    }
}
