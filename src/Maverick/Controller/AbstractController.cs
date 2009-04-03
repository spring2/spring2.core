using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;
using Spring2.Core.Types;
using Maverick.Ctl;

namespace Spring2.Core.Maverick.Controller {

    /// <summary>
    /// Abstract base controller
    /// </summary>
    public abstract class AbstractController : Throwaway {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private NameValueCollection requestParamsWithControllerContextParams = new NameValueCollection();

	public HttpResponse Response {
	    get { return this.ControllerContext.HttpContext.Response; }
	}

	public HttpRequest Request {
	    get { return this.ControllerContext.HttpContext.Request; }
	}

	public HttpContext Context {
	    get { return this.ControllerContext.HttpContext; }
	}

	public HttpServerUtility Server {
	    get { return this.ControllerContext.HttpContext.Server; }
	}
	
	public NameValueCollection RequestParamsWithControllerContextParams {
	    get {
		if(this.requestParamsWithControllerContextParams.Count == 0) {
		    this.requestParamsWithControllerContextParams = new NameValueCollection(this.ControllerContext.HttpContext.Request.Params);
		    if(this.ControllerContext != null && this.ControllerContext.Params != null && this.ControllerContext.Params.Count > 0) {
			foreach(String key in this.ControllerContext.Params.Keys) {
			    this.requestParamsWithControllerContextParams.Set(key, this.ControllerContext.Params[key].ToString());
			}
		    }
		}
		return this.requestParamsWithControllerContextParams;
	    }
	}
	    
	/// <summary>
	/// This is the method you should override to implement application logic.
	/// Default implementation just returns "success".
	/// </summary>
	/// <returns></returns>
	public virtual string Perform() {
	    return SUCCESS;
	}

	public override sealed string Go() {
	    // if the ControllerContext params collection exists and has anything in it, it was should be 
	    // mapped to http request params.  If a forward is used, what used to be passed along in the query string 
	    // could be here.
	    Populate(this, this.RequestParamsWithControllerContextParams);
	    this.ControllerContext.Model = this;
	    return Perform();
	}

	protected virtual void Populate(Object target, NameValueCollection data) {
	    HandleParseErrorsAndMissingRequiredPropertyErrors(Populator.Instance.Populate(target, data));
	}


	/// <summary>
	/// Handle parsing errors and missing required property errors returned from property populator
	/// </summary>
	protected virtual void HandleParseErrorsAndMissingRequiredPropertyErrors(MessageList errors) {
	}

	/// <summary>
	/// Get a cookie for getting data. 
	/// </summary>
	/// <param name="cookieName"></param>
	/// <returns></returns>
	public HttpCookie GetCookie(String cookieName) {
	    HttpCookie returnCookie = ControllerContext.HttpContext.Request.Cookies[cookieName];
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
	    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
	}


    }
}
