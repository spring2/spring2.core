using System;
using System.Collections.Specialized;
using System.Web;

using Spring2.Core.Message;
using Spring2.Core.Types;

namespace Spring2.Core.Maverick.DataForm {
    /// <summary>
    /// Summary description for LocalizedForm.
    /// </summary>
    public class LocalizedForm : PopulatedForm{
	protected ILocale locale;
	protected ILanguage language;

	public LocalizedForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors, HttpCookieCollection cookies, ILocale locale, ILanguage language) : base(formatter, values, errors, cookies) {
	    this.locale = locale;
	    this.language = language;
	}
	public LocalizedForm(IMessageFormatter formatter, NameValueCollection values, HttpCookieCollection cookies, ILocale locale, ILanguage language) : base(formatter, values, cookies) {
	    this.locale = locale;
	    this.language = language;
	}
	public LocalizedForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors, ILocale locale, ILanguage language) : base(formatter, values, errors) {
	    this.locale = locale;
	    this.language = language;
	}
	public LocalizedForm(IMessageFormatter formatter, NameValueCollection values, ILocale locale, ILanguage language) : base(formatter, values) {
	    this.locale = locale;
	    this.language = language;
	}

	public ILocale Locale{
	    get{ return this.locale;}
	}

	public ILanguage Language{
	    get{ return this.language;}
	}

    }
}
