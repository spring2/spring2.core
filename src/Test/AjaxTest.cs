using System;
using System.Collections.Specialized;
using System.Web;
using System.Xml;

using NUnit.Framework;

using Spring2.Core.Ajax;
using Spring2.Core.Message;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class AjaxTest {

	[Test()]
	public void ShouldBeAbleToExtendAndPopulateCommandAbstractClass() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2IdType", "101");

	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = new SampleAjaxCommand(0, String.Empty, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    command.RunCommand();

	    //assert object was populated
	    Assert.AreEqual("value1", command.SystemString);
	    Assert.AreEqual(new StringType("value2"), command.Spring2StringType);
	    Assert.AreEqual(new IdType(101), command.Spring2IdType);
	}

	[Test]
	public void ShouldGetBackUnhandledMessageListExcptionsInXML() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();

	    //get instance of SampleAjaxCommand
	    UnhandledMessageListExceptionAjaxCommand command = new UnhandledMessageListExceptionAjaxCommand(0, String.Empty, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(command.RunCommand());

	    String unhandledException = doc.DocumentElement.GetAttribute("unhandledException");
	    Assert.AreEqual("true", unhandledException);

	    String message = doc.DocumentElement.GetAttribute("message");
	    Assert.AreEqual("TE'ST%0aTE'ST'2%0a", message);
	}

	[Test]
	public void ShouldGetBackUnhandledSystemExcptionsInXML() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();

	    //get instance of UnhandledSystemExceptionAjaxCommand
	    UnhandledSystemExceptionAjaxCommand command = new UnhandledSystemExceptionAjaxCommand(0, String.Empty, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(command.RunCommand());

	    String unhandledException = doc.DocumentElement.GetAttribute("unhandledException");
	    Assert.AreEqual("true", unhandledException);

	    String message = doc.DocumentElement.GetAttribute("message");
	    Assert.IsTrue(message.StartsWith("There+was+a+problem.%0aInform+supp"));
	}

	[Test]
	public void ShouldHavePopulationErrorsAvailable() {
	    //set up NameValueCollection
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2IdType", "One0One");

	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = new SampleAjaxCommand(0, String.Empty, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    String returnValue = command.RunCommand();

	    //assert object was populated
	    Assert.AreEqual("value1", command.SystemString);
	    Assert.AreEqual(new StringType("value2"), command.Spring2StringType);
	    Assert.IsTrue(command.Spring2IdType.IsDefault);
	    Assert.AreEqual("Errors", returnValue);
	}
	
	[Test]
	public void BaseCommandShouldHaveEmptyConstructorAndReturnNameAndQualifiedName() {
	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = SampleAjaxCommand.Instance;
	    
	    Assert.AreEqual("SampleAjaxCommand", command.Name);
	    Assert.AreEqual("Spring2.Core.Test.SampleAjaxCommand,Spring2.Core.Test", command.QualifiedName);
	}
	
	[Test]
	public void ShouldGetInvalidStateExceptionWhenRunningCommandConstructedWithNoArguments() {
	    SampleAjaxCommand command = SampleAjaxCommand.Instance;
	    
	    XmlDocument doc = new XmlDocument();
	    doc.LoadXml(command.RunCommand());

	    String unhandledException = doc.DocumentElement.GetAttribute("unhandledException");
	    Assert.AreEqual("true", unhandledException);

	    String message = doc.DocumentElement.GetAttribute("message");
	    Assert.IsTrue(message.StartsWith("There+was+a+problem.%0aInform+supp"));
	    Assert.IsTrue(message.IndexOf("InvalidOperationException%3a+Command+not+constructed+to+run") > 0, "Did not get right exception");
	}
    }
    
    #region Test commands
    public class SampleAjaxCommand : Command {
	public static readonly SampleAjaxCommand Instance = new SampleAjaxCommand();
	private string systemString;
	private StringType spring2StringType;
	private IdType spring2IdType;

	public string SystemString {
	    get { return systemString; }
	    set { systemString = value; }
	}
	public StringType Spring2StringType {
	    get { return spring2StringType; }
	    set { spring2StringType = value; }
	}
	public IdType Spring2IdType {
	    get { return spring2IdType; }
	    set { spring2IdType = value; }
	}


	public SampleAjaxCommand (Int32 responseHandlerId, String commandIdentifier, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base (responseHandlerId, commandIdentifier, data, formatter, errors, cookies) {
	}

	private SampleAjaxCommand () {}

	protected override String Execute() {
	    if(this.errors.Count > 0) {
		return "Errors";
	    }
	    return String.Empty;
	}
    }

    public class UnhandledMessageListExceptionAjaxCommand : Command {
	private Boolean test = true;
	public UnhandledMessageListExceptionAjaxCommand (Int32 responseHandlerId, String commandIdentifier, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base (responseHandlerId, commandIdentifier, data, formatter, errors, cookies) {}

	protected override String Execute() {
	    if(test == true) {
		MissingRequiredFieldError exception = new MissingRequiredFieldError("TE'ST", "TE'ST");
		MissingRequiredFieldError exception2 = new MissingRequiredFieldError("TE'ST'2", "TE'ST'2");
		MessageList errors = new MessageList();
		errors.Add(exception);
		errors.Add(exception2);
		throw new MessageListException(errors);
	    }
	    return "FAILED";
	}
    }

    public class UnhandledSystemExceptionAjaxCommand : Command {
	private Boolean test = true;
	public UnhandledSystemExceptionAjaxCommand (Int32 responseHandlerId, String commandIdentifier, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base (responseHandlerId, commandIdentifier, data, formatter, errors, cookies) {}

	protected override String Execute() {
	    if(test == true) {
		throw new Exception("TE'ST'2");
	    }
	    return "FAILED";
	}
    }
    #endregion Test commands
}
