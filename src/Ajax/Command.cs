using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web;

using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;
using Spring2.Core.Ajax.Json;

namespace Spring2.Core.Ajax {
    /// <summary>
    /// Summary description for Command.
    /// </summary>
    abstract public class Command {
	protected Int32 responseHandlerId;
	protected IMessageFormatter messageFormatter = null;
	protected NameValueCollection commandValues = null;
	protected MessageList errors = new MessageList();
	protected HttpCookieCollection cookies = null;
	private String name = String.Empty;
	private String qualifiedName = String.Empty;
	private Boolean isValid = false;

	public Command(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) {
	    this.isValid = true;
	    this.responseHandlerId = responseHandlerId;
	    this.commandValues = values;
	    this.messageFormatter = formatter;
	    this.errors = errors;
	    this.cookies = cookies;
	    this.Populate();
	}

	protected Command() {}

	public String RunCommand() {
	    try {
		if(isValid) {
		    return Execute();
		} else {
		    throw new System.InvalidOperationException("Command not constructed to run.");
		}
	    } catch(MessageListException listException) {
		this.errors.AddRange(listException.Messages);
		return String.Format("<command id='{0}' unhandledException='true' message='{1}'></command>", responseHandlerId, LocalizeAndFormatErrorsForJavaScriptAlert());
	    } catch(Exception ex) {
		return String.Format("<command id='{0}' unhandledException='true' message='{1}'></command>", responseHandlerId, this.PrepairString("There was a problem.\nInform support of the following error and refresh the page.\n\n" + ex.ToString()));
	    }
	}

	public String Name {
	    get {
		if(name.Equals(String.Empty)) {
		    name = this.GetType().Name;
		}
		return name; 
	    }
	}

	public String QualifiedName {
	    get {
		if(qualifiedName.Equals(String.Empty)) {
		    qualifiedName = this.GetType().AssemblyQualifiedName;
		    qualifiedName = qualifiedName.Substring(0, qualifiedName.IndexOf(',', qualifiedName.IndexOf(',')+1)).Replace(" ", "");
		}
		return qualifiedName;
	    }
	}

	protected virtual void Populate() {
	    errors.AddRange(Populator.Instance.Populate(this, commandValues));
	}

	abstract protected String Execute();

	internal String PrepairString(String message) {
	    return HttpUtility.UrlEncode(message.Replace("'", "&#39;"));
	}

	internal String PrepairString(StringBuilder message) {
	    return PrepairString(message.ToString());
	}

	public String LocalizeAndFormatErrorsForJavaScriptAlert() {
	    StringBuilder sb = new StringBuilder();
	    if(this.errors.Count > 0) {
		foreach(Spring2.Core.Message.Message message in this.errors) {
		    sb.Append(messageFormatter.Format(message));
		    sb.Append("\n");
		}
	    }
	    return HttpUtility.UrlEncode(sb.ToString()).Replace("'", "&apos;");
	}
    }
}
