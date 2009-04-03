using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Maverick.Controller;
using Spring2.Core.Message;


namespace Spring2.Core.Ajax.SampleController {
    public class AjaxController : ErrorableController {
        private String ajaxRequest;
	private NameValueCollection formVariables;
	private Int32 responseHandlerId;
	private String commandName = String.Empty;

	public NameValueCollection FormVariables {
            get { return formVariables; }
            set { formVariables = value; }
        }	

        public String AjaxRequest {
            set { ajaxRequest = value; }
	}

	public override string SafePerform() {
	    String json = processCommands();
	    this.ControllerContext.HttpContext.Response.ContentType = "text/plain";
	    this.ControllerContext.Model = json;
	    return "ajax";
	}

        public String processCommands() {
            String json = String.Empty;
            String jsonWrapper = String.Empty;
            String result = String.Empty;
            jsonWrapper = "{commandResponses:[";
            JsonAjaxUtility util = null;

	    util = new JsonAjaxUtility(this.ajaxRequest);
	    while(util.NextCommand(ref commandName, ref responseHandlerId, ref formVariables)) {
		if(json.Length == 0) {
		    json += GetCommand().RunCommand();
		} else {
		    json += "," + GetCommand().RunCommand();
		}
	    }

            jsonWrapper += json;
            jsonWrapper += "]}";
            return jsonWrapper;
        }

        private Command GetCommand() {
	    Type clazz = Type.GetType(commandName);
	    object[] args = { this.responseHandlerId, this.formVariables, this.MessageFormatter, this.Errors, this.Request.Cookies };
            Object o = System.Activator.CreateInstance(clazz, args);
            return (Command)o;
        }


	protected override IMessageFormatter GetMessageFormatter() {
	    return new SimpleFormatter();
	}
    }
}
