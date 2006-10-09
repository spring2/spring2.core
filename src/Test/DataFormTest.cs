using System;
using System.Collections.Specialized;
using System.Web;

using NUnit.Framework;

using Spring2.Core.Maverick.DataForm;
using Spring2.Core.Message;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DataFormTest {

	[Test()]
	public void ShouldBeAbleToExtendAndPopulateLocalizedDataForm() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");

	    //get instance of SampleLocalizedDataForm
	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), collection, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);

	    //assert object was populated
	    Assert.AreEqual("value1", dataForm.SystemString);
	    Assert.AreEqual(new StringType("value2"), dataForm.Spring2StringType);

	    //assert other properties are as expected
	    Assert.AreEqual(LocaleEnum.UNITED_STATES, dataForm.Locale);
	    Assert.AreEqual(LanguageEnum.ENGLISH, dataForm.Language);
	    IMessageFormatter formatter = dataForm.MessageFormatter;
	}

	[Test()]
	public void ShouldBeAbleToGetInstanceOfFormWithErrors() {
	    MessageList errors = new MessageList();
	    errors.Add(new MissingRequiredFieldError("NoClass", "NoField"));

	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), new NameValueCollection(), errors, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);

	    Assert.IsTrue(dataForm.Errors.Count == 1);
	}

	[Test()]
	public void ShouldBeAbleToGetInstanceOfFormWithCookies() {
	    HttpCookieCollection cookies = new HttpCookieCollection();
	    cookies.Add(new HttpCookie("NoCookie", "NoValue"));

	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), new NameValueCollection(), cookies, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);

	    Assert.AreEqual("NoValue", dataForm.GetCookie("NoCookie").Value);
	}

	[Test()]
	public void ShouldBeAbleToGetInstanceOfFormThroughMostExtensiveConstructor() {
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");
	    MessageList errors = new MessageList();
	    errors.Add(new MissingRequiredFieldError("NoClass", "NoField"));
	    HttpCookieCollection cookies = new HttpCookieCollection();
	    cookies.Add(new HttpCookie("NoCookie", "NoValue"));

	    //get instance of SampleLocalizedDataForm
	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), collection, errors, cookies, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);

	    //assert object was populated
	    Assert.AreEqual("value1", dataForm.SystemString);
	    Assert.AreEqual(new StringType("value2"), dataForm.Spring2StringType);

	    //assert other properties are as expected
	    Assert.AreEqual(LocaleEnum.UNITED_STATES, dataForm.Locale);
	    Assert.AreEqual(LanguageEnum.ENGLISH, dataForm.Language);
	    IMessageFormatter formatter = dataForm.MessageFormatter;
	    Assert.AreEqual("NoValue", dataForm.GetCookie("NoCookie").Value);
	    Assert.IsTrue(dataForm.Errors.Count == 1);
	}

	[Test()]
	public void ShouldBeAbleToSetErrorsAfterInstanceOfFormCreated() {
	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), new NameValueCollection(), LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);
	    
	    MessageList errors = new MessageList();
	    errors.Add(new MissingRequiredFieldError("NoClass", "NoField"));

	    dataForm.Errors = errors;

	    Assert.IsTrue(dataForm.Errors.Count == 1);
	}

	[Test()]
	public void IfCookieNotFoundInCollectionExpectNullReturnedFromGetCookie() {
	    SampleLocalizedDataForm dataForm = new SampleLocalizedDataForm(new SimpleFormatter(), new NameValueCollection(), LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH);

	    HttpCookie cookie = dataForm.GetCookie("NoCookieWithThisName");

	    Assert.AreEqual(null as object, cookie);
	}

	public class SampleLocalizedDataForm : LocalizedForm {
	    private string systemString;
	    private StringType spring2StringType;

	    public string SystemString {
		get { return systemString; }
		set { systemString = value; }
	    }
	    public StringType Spring2StringType {
		get { return spring2StringType; }
		set { spring2StringType = value; }
	    }

	    public SampleLocalizedDataForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors, HttpCookieCollection cookies, ILocale locale, ILanguage language) : base(formatter, values, errors, cookies, locale, language) {
	    }
	    public SampleLocalizedDataForm(IMessageFormatter formatter, NameValueCollection values, HttpCookieCollection cookies, ILocale locale, ILanguage language) : base(formatter, values, cookies, locale, language) {
	    }
	    public SampleLocalizedDataForm(IMessageFormatter formatter, NameValueCollection values, MessageList errors, ILocale locale, ILanguage language) : base(formatter, values, errors, locale, language) {
	    }
	    public SampleLocalizedDataForm(IMessageFormatter formatter, NameValueCollection values, ILocale locale, ILanguage language) : base(formatter, values, locale, language) {
	    }
	}
    }
}
