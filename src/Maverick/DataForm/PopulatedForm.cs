using System;
using System.Collections.Specialized;
using System.Web;

using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;

namespace Spring2.Core.Maverick.DataForm {
    
    /// <summary>
    /// Base data form that populates itself from the request
    /// </summary>
    public class PopulatedForm {
	protected IMessageFormatter messageFormatter;
	protected NameValueCollection formValues;
	private MessageList errors = new MessageList();
	private HttpCookieCollection cookies = new HttpCookieCollection();

	//Constructor
	public PopulatedForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors, HttpCookieCollection cookies) {
	    this.messageFormatter = formatter;
	    this.formValues = values;
	    this.errors = errors;
	    this.cookies = cookies;
	    Populate();
	}
	public PopulatedForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors) {
	    this.messageFormatter = formatter;
	    this.formValues = values;
	    this.errors = errors;
	    Populate();
	}
	public PopulatedForm(IMessageFormatter formatter, NameValueCollection values, HttpCookieCollection cookies) {
	    this.messageFormatter = formatter;
	    this.formValues = values;
	    this.cookies = cookies;
	    Populate();
	}
	public PopulatedForm(IMessageFormatter formatter, NameValueCollection values) {
	    this.messageFormatter = formatter;
	    this.formValues = values;
	    Populate();
	}

	/// <summary>
	/// Method to handle population
	/// </summary>
	protected virtual void Populate() {
	    errors.AddRange(Populator.Instance.Populate(this, this.formValues));
	}

	public IMessageFormatter MessageFormatter {
	    get { return messageFormatter; }
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
	    //return this.cookies[cookieName];
	    HttpCookie returnCookie = this.cookies[cookieName];
	    if (returnCookie == null) {
		returnCookie = new HttpCookie(cookieName);
	    }
	    return returnCookie;
	}


    }
}