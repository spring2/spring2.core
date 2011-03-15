using System;
using System.Collections.Specialized;
using System.Web;
using NUnit.Framework;
using Spring2.Core.Maverick;
using Spring2.Core.Maverick.Controller;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Maverick;
using Maverick.Flow;
using NotImplementedException=System.NotImplementedException;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for MockHttpWorkerRequestTest.
    /// </summary>
    [TestFixture]
    public class MockHttpWorkerRequestTest {

	[Test]
	public void FormTest() {
	    NameValueCollection form = new NameValueCollection();
	    form.Add("1", "a");
	    form.Add("2", "b");
	    form.Add("3", "c");

	    HttpWorkerRequest wr = new MockHttpWorkerRequest(form);
	    HttpContext context = new HttpContext(wr);
	    Assert.AreEqual(3, context.Request.Form.Keys.Count);

	    Assert.AreEqual("a", context.Request.Form["1"]);
	    Assert.AreEqual("b", context.Request.Form["2"]);
	    Assert.AreEqual("c", context.Request.Form["3"]);

	    Assert.AreEqual("a", context.Request["1"]);
	    Assert.AreEqual("b", context.Request["2"]);
	    Assert.AreEqual("c", context.Request["3"]);
	}
    	
	[Test]
	public void ShouldExposeQueryString() {
	    String rawUrl = "http://localhost/foo/bar.m?abc=123";
	    HttpWorkerRequest wr = new MockHttpWorkerRequest(rawUrl);
	    HttpContext context = new HttpContext(wr);
	    Assert.AreEqual(1, context.Request.QueryString.Count);
    	    
	    Assert.AreEqual(rawUrl.Replace("http://localhost", string.Empty), context.Request.RawUrl);
	}

	[Test]
	public void ShouldParseValidCultureFromAcceptLanguage() {
	    MockHttpWorkerRequest wr = new MockHttpWorkerRequest();
	    wr.SetHeaderValue(HttpWorkerRequest.HeaderAcceptLanguage, "es-ES");
	    HttpContext context = new HttpContext(wr);

	    Dispatcher dispatcher = new Dispatcher();
	    MaverickContext mctx = new MaverickContext(dispatcher, context);
	    context.Items.Add(Dispatcher.MAVERICK_CONTEXT_KEY, mctx);

	    TestController controller = new TestController();
	    String result = controller.Go(mctx);

	    Assert.AreEqual(LocaleEnum.SPAIN, controller.Locale);
	    Assert.AreEqual(LanguageEnum.SPANISH, controller.Language);
	}

	[Test]
	public void ShouldParseInvalidCultureFromAcceptLanguage() {
	    MockHttpWorkerRequest wr = new MockHttpWorkerRequest();
	    wr.SetHeaderValue(HttpWorkerRequest.HeaderAcceptLanguage, "es");
	    HttpContext context = new HttpContext(wr);

	    Dispatcher dispatcher = new Dispatcher();
	    MaverickContext mctx = new MaverickContext(dispatcher, context);
	    context.Items.Add(Dispatcher.MAVERICK_CONTEXT_KEY, mctx);

	    TestController controller = new TestController();
	    String result = controller.Go(mctx);

	    Assert.AreEqual(LocaleEnum.UNITED_STATES, controller.Locale);
	    Assert.AreEqual(LanguageEnum.SPANISH, controller.Language);
	}

	[Test]
	public void ShouldParseMultipleCulturesFromAcceptLanguage() {
	    MockHttpWorkerRequest wr = new MockHttpWorkerRequest();
	    wr.SetHeaderValue(HttpWorkerRequest.HeaderAcceptLanguage, "foo1,foo2;q=0.8,es-ES; q=0.6");
	    HttpContext context = new HttpContext(wr);

	    Dispatcher dispatcher = new Dispatcher();
	    MaverickContext mctx = new MaverickContext(dispatcher, context);
	    context.Items.Add(Dispatcher.MAVERICK_CONTEXT_KEY, mctx);

	    TestController controller = new TestController();
	    String result = controller.Go(mctx);

	    Assert.AreEqual(LocaleEnum.UNITED_STATES, controller.Locale);
	    Assert.AreEqual(LanguageEnum.ENGLISH, controller.Language);
	}

	[Test]
	public void ShouldBeAbleToSetUrlAndForm() {
	    NameValueCollection form = new NameValueCollection();
	    form.Add("1", "a");
	    form.Add("2", "b");
	    form.Add("3", "c");

	    String rawUrl = "http://test.localhost/foo/bar.m?abc=123";

	    HttpWorkerRequest wr = new MockHttpWorkerRequest(rawUrl, form);
	    HttpContext context = new HttpContext(wr);
	    Assert.AreEqual(3, context.Request.Form.Keys.Count);

	    Assert.AreEqual("a", context.Request.Form["1"]);
	    Assert.AreEqual("b", context.Request.Form["2"]);
	    Assert.AreEqual("c", context.Request.Form["3"]);

	    Assert.AreEqual("a", context.Request["1"]);
	    Assert.AreEqual("b", context.Request["2"]);
	    Assert.AreEqual("c", context.Request["3"]);

	    Assert.AreEqual(rawUrl, context.Request.Url.ToString());
	    Assert.AreEqual("test.localhost", context.Request.Url.Host);
	}
    
    }

    internal class TestController : LocalizedController {
	public override string LocalizedPerform() {
	    return "success";
	}

	protected override IMessageFormatter GetMessageFormatter() {
	    return new SimpleFormatter();
	}
    }
}
