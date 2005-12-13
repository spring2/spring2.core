using System;
using System.Collections;
using System.Reflection;
using Spring2.Core.Message;
using Spring2.Core.Types;

namespace Spring2.Core.Maverick.Controller {

    /// <summary>
    /// Abstract base controller that will catch an unhandled exception
    /// </summary>
    public abstract class ErrorableController : AbstractController {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	public static readonly String VALIDATION_ERRORS = "validationErrors";

	protected MessageList errors = new MessageList();
	private IMessageFormatter messageFormatter = null;

	public IMessageFormatter MessageFormatter {
	    get {
	    	if (messageFormatter == null) {
		    this.messageFormatter = GetMessageFormatter();
	    	}
		return this.messageFormatter;
	    }
	}

	protected abstract IMessageFormatter GetMessageFormatter();

	/// <summary>
	/// collection of validation errors
	/// </summary>
	public MessageList Errors {
	    get { return this.errors; }
	}

	public override String Perform() {
	    try {
		// populate errors here
		if( this.ControllerContext.HttpContext.Items[VALIDATION_ERRORS] != null ){
		    Errors.AddRange( (MessageList)this.ControllerContext.HttpContext.Items[VALIDATION_ERRORS] );
		}
		return SafePerform();
	    } catch (Exception ex) {
		log.Error(ex);
		this.ControllerContext.HttpContext.Items.Add("error", ex);
		return ERROR;
	    }
	}

	public abstract String SafePerform();

	/// <summary>
	/// Handle parsing exceptions
	/// </summary>
	protected override void HandleParseException(PropertyInfo property, object val) {
	    Boolean required = property.GetCustomAttributes(typeof(RequiredAttribute), true).Length == 1;

	    if (required && typeof(string).IsInstanceOfType(val) && String.Empty.Equals(val)) {
		Errors.Add(new MissingRequiredFieldError(property.Name.ToString()));
	    } else {
		if (typeof(string).IsInstanceOfType(val) && String.Empty.Equals(val)) {
		    // do nothing - if the value is an empty string and is not required then no problem
		} else {
		    Errors.Add(new InvalidTypeFormatError(property.Name,  val.ToString()));
		}
	    }
	}

	/// <summary>
	/// Handle parsing exceptions
	/// </summary>
	protected override void HandleMissingRequiredProperty(PropertyInfo property) {
	    Errors.Add(new MissingRequiredFieldError(property.Name));
	}

	/// <summary>
	/// Persist Errors in the HttpContext
	/// </summary> 
	public void PersistErrorsInHttp(MessageList messageList){
	    this.ControllerContext.HttpContext.Items.Add(VALIDATION_ERRORS, messageList);
	}
    }
}
