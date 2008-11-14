using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;

namespace Spring2.Core.Ajax.SampleController2.SampleCommand {
    public class RegurgitationCommand : Command {
	private String text = "";

	public static readonly RegurgitationCommand Instance = new RegurgitationCommand();

	public RegurgitationCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected RegurgitationCommand() { }

	protected override String Execute() {
	    String result = text;
	    String status = "success";

            JsonAjaxUtility util = new JsonAjaxUtility();
            Hashtable response = new Hashtable();
            response.Add("status", status);
            response.Add("message", result);
            return util.SerializeResponse(this.responseHandlerId, response);
	}

	public String Text {
	    get { return text; }
	    set { text = value; }
	}
    }
}
