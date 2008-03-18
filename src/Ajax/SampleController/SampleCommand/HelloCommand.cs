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
    public class HelloCommand : Command {
	private String helloText = "";

	public static readonly HelloCommand Instance = new HelloCommand();

	public HelloCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected HelloCommand() { }

	protected override String Execute() {
	    String result = helloText;
	    String status = "success";

            JsonAjaxUtility util = new JsonAjaxUtility();
            Hashtable response = new Hashtable();
            response.Add("status", status);
            response.Add("message", result);
            return util.SerializeResponse(this.responseHandlerId, response);
	}

	public String HelloText {
	    get { return helloText; }
	    set { helloText = value; }
	}
    }
}
