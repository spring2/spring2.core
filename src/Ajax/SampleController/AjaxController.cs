using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;

using Spring2.Core.Ajax;
using Spring2.Core.Maverick.Controller;
using Spring2.Core.Message;


namespace Spring2.Core.Ajax.SampleController {
    public class AjaxController : ErrorableController {
        private bool gotAjaxCommand = false;
        private String ajaxCommands;
	private String commandName = String.Empty;
	private Int32 responseHandlerId = 0;
	private String commandMapString = String.Empty;
	private Dictionary<String, String> commandMap = new Dictionary<string, string>();  

        public String AjaxCommand {
            set {
                gotAjaxCommand = true;
                ajaxCommands = value;
            }
	}

	public Int32 ResponseHandlerId {
	    get { return responseHandlerId; }
	    set { responseHandlerId = value; }
	}

	public String CommandMapString {
	    set { commandMapString = value; }
	}

	public override string SafePerform() {
            if (gotAjaxCommand) {
		CreateCommandMap();
                String xml = String.Empty;
                xml = processCommands(ajaxCommands);
                this.ControllerContext.HttpContext.Response.ContentType = "text/xml";
                this.ControllerContext.Model = xml;
                return "ajax";
            }
            return SUCCESS;
        }

        public String processCommands(String ajaxCommands) {
            String xml = String.Empty;
            String result = String.Empty;
            xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><commands>";
            String[] exCommands = ajaxCommands.Split(',');
            foreach (String com in exCommands) {
                String[] commandAndId = com.Split('~');
                this.ResponseHandlerId = Convert.ToInt32(commandAndId[1]);

                xml += GetCommand(commandMap[commandAndId[0]], com).RunCommand();
            }
            xml += "</commands>";
            return xml;
        }

        private Command GetCommand(String commandName, String commandIdentifier) {
	    Type clazz = Type.GetType(commandName);
	    object[] args = { this.ResponseHandlerId, commandIdentifier, this.ControllerContext.HttpContext.Request.Params, this.MessageFormatter, this.Errors, this.Request.Cookies };
            Object o = System.Activator.CreateInstance(clazz, args);
            return (Command)o;
        }

	private void CreateCommandMap() {
	    String[] allMappings = this.commandMapString.Split('~');
	    foreach(String mapping in allMappings) {
		String[] mapArray = mapping.Split('^');
		commandMap.Add(mapArray[0], mapArray[1]);
	    }
	}


	protected override IMessageFormatter GetMessageFormatter() {
	    return new SimpleFormatter();
	}
    }
}
