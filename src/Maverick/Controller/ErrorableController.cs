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
	protected override void HandleParseErrorsAndMissingRequiredPropertyErrors(MessageList errors) {
	    Errors.AddRange(errors);
	}

	/// <summary>
	/// Persist Errors in the HttpContext
	/// </summary> 
	public void PersistErrorsInHttp(MessageList messageList){
	    this.ControllerContext.HttpContext.Items.Add(VALIDATION_ERRORS, messageList);
	}
    }
}
