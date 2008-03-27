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
    public class EvaluateCheckedCommand : Command {
	private Boolean isChecked = false;

	public static readonly EvaluateCheckedCommand Instance = new EvaluateCheckedCommand();

	public EvaluateCheckedCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) { }
	protected EvaluateCheckedCommand() { }

	protected override String Execute() {
	    String status = "success";

            JsonAjaxUtility util = new JsonAjaxUtility();
            Hashtable response = new Hashtable();
            response.Add("status", status);
            response.Add("message", isChecked);
            return util.SerializeResponse(this.responseHandlerId, response);
	}

	public Boolean IsChecked {
	    set { isChecked = value; }
	}
    }
}
