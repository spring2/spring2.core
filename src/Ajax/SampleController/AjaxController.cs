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
        //private bool gotAjaxRequest = false;
        private String ajaxRequest;
	private String commandName = String.Empty;
	private Int32 responseHandlerId = 0;
        private NameValueCollection formVariables;

        public NameValueCollection FormVariables {
            get { return formVariables; }
            set { formVariables = value; }
        }	

        public String AjaxRequest {
            set {
                //gotAjaxRequest = true;
                ajaxRequest = value;
            }
	}

	public Int32 ResponseHandlerId {
	    get { return responseHandlerId; }
	    set { responseHandlerId = value; }
	}

        public String CommandName {
            get { return commandName; }
            set { commandName = value; }
        }

	public override string SafePerform() {
            //if (gotAjaxRequest) {
		//CreateCommandMap();
                String json = String.Empty;

                ajaxRequest = @"{""fullyQualifiedNames"":{""0"":""Spring2.Core.Ajax.SampleController.SampleCommand.HelloCommand,Spring2.Core.Ajax.SampleController""},""AjaxCommands"":[";
                ajaxRequest += @"{""commandkey"":0,""responseHandlerId"":0,""parameters"":{""X"":""MyX"",""Y"":""MyY""}}";
                ajaxRequest += "]}";
 
                //{'ResponseHandlerId':0, 'Response':{'car':'BMW','Truck':'Dodge','House':'Big'}}


                json = processCommands(ajaxRequest);
                this.ControllerContext.HttpContext.Response.ContentType = "text/xml";
                this.ControllerContext.Model = json;
                return "ajax";
            //}
            //return SUCCESS;
        }

        public String processCommands(String ajaxCommands) {
            String json = String.Empty;
            String jsonWrapper = String.Empty;
            String result = String.Empty;
            jsonWrapper = "{command:Responses[";
            JsonAjaxUtility util = null;
            try {
                util = new JsonAjaxUtility(ajaxCommands);
            } catch (Exception ex ) {
                json += ex.Message;
            }

            while (util.NextCommand(ref commandName, ref responseHandlerId, ref formVariables)) {
                if (json.Length == 0) {
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
	    object[] args = { this.ResponseHandlerId, this.FormVariables, this.MessageFormatter, this.Errors, this.Request.Cookies };
            Object o = System.Activator.CreateInstance(clazz, args);
            return (Command)o;
        }


	protected override IMessageFormatter GetMessageFormatter() {
	    return new SimpleFormatter();
	}
    }
}
