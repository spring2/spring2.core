using System;
using System.Web;
using System.Text;
using Maverick.Flow;
using Spring2.Core.Maverick.Controller;
using Spring2.Core.Message;
using Spring2.Core.Types;
namespace Spring2.Core.Maverick.DataForm {
    
    /// <summary>
    /// Base data form that populates itself from the request
    /// </summary>
    public class PopulatedForm {

	//Constructor
	protected ErrorableController controller;
	protected IControllerContext controllerContext;
	private MessageList errors = new MessageList();

	public PopulatedForm(ErrorableController errorableController) {
	    this.controller = errorableController;
	    this.controllerContext = errorableController.ControllerContext;
	    controller.Populate(this, errorableController.ControllerContext.HttpContext.Request.Params);
	    Populate();
	    this.errors.AddRange(errorableController.Errors);
	}

	/// <summary>
	/// Method to handle custom population
	/// </summary>
	protected virtual void Populate() {
	}

	public IMessageFormatter MessageFormatter {
	    get { return controller.MessageFormatter; }
	}

	public MessageList Errors {
	    get { return this.errors; }
	    set { errors = value; }
	}

	/// <summary>
	/// Get a cookie for getting data. 
	/// </summary>
	/// <param name="cookieName"></param>
	/// <returns></returns>
	public HttpCookie GetCookie(String cookieName) {
		HttpCookie returnCookie = controllerContext.HttpContext.Request.Cookies[cookieName];
	    if (returnCookie == null) {
		returnCookie = new HttpCookie(cookieName);
	    }
	    return returnCookie;
	}

	/// <summary>
	/// Persists a cookie.  This can only be expected to work once for each cookie between post.
	/// </summary>
	/// <param name="cookie"></param>
	public void UpdateCookie(HttpCookie cookie) {
	    //cookie.Path = controllerContext.HttpContext.Request.ApplicationPath;
	    // TODO: set explicitly to / for problems with enrollment process
	    cookie.Path = "/";
	    controllerContext.HttpContext.Response.Cookies.Add(cookie);
	}


    }
}