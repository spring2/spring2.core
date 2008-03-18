using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Spring2.Core.Ajax.Json {

    class Command {
        public Int32 commandKey = 0;
        public Int32 responseHandlerId = 0;
        public Dictionary<String, String> parameters = new Dictionary<String, String>();
    }


    class Commands {
        public Dictionary<String, String> fullyQualifiedNames = new Dictionary<String, String>();
        public List<Command> AjaxCommands = new List<Command>();
    }

    public class Response {
        public Int32 responseHandlerId; 
        public Hashtable response;
    }


    public class JsonAjaxUtility {
        private String jsonText =String.Empty;
        private Commands commands = null;
        private Int32 commandIndex = 0;

        public JsonAjaxUtility() {
        }

        public JsonAjaxUtility(String jsonText) {
            try {
                commands = (Commands)JavaScriptConvert.DeserializeObject(jsonText, typeof(Commands));
            } catch (Exception ex) {
                throw ex; 
            }
        }

        public bool NextCommand(ref String fullyQualifiedName, ref Int32 responseHandlerId, ref NameValueCollection formVariables ) {
            try {
                Command nextCommand = commands.AjaxCommands[commandIndex];
                fullyQualifiedName = commands.fullyQualifiedNames[nextCommand.commandKey.ToString()];
                responseHandlerId = nextCommand.responseHandlerId;
                formVariables = new NameValueCollection();
                foreach (KeyValuePair<String, String> kvp in nextCommand.parameters) {
                    formVariables.Add(kvp.Key, kvp.Value);
                }
                commandIndex++;
                return true;

            } catch {
                return false;
            }            
        }

        public String SerializeResponse(Int32 responseHandlerId,  Hashtable values) {
            Response r = new Response();
            r.responseHandlerId = responseHandlerId;
            r.response = values;
            return JavaScriptConvert.SerializeObject(r);
	}

	public Response DeserializeResponse(String jSON) {
	    return JavaScriptConvert.DeserializeObject(jSON, typeof(Response)) as Response;
	}
    }
}
