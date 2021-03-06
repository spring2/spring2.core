using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

using Newtonsoft.Json;
using NUnit.Framework;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Message;
using Spring2.Core.Types;

namespace Spring2.Core.Test.Ajax {

    [TestFixture]
    public class AjaxTest {
	private JsonAjaxUtility JsonUtil = new JsonAjaxUtility();

	[Test()]
	public void ShouldBeAbleToExtendAndPopulateCommandAbstractClass() {
	    //set up data
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2IdType", "101");

	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = new SampleAjaxCommand(0, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    command.RunCommand();

	    //assert object was populated
	    Assert.AreEqual("value1", command.SystemString);
	    Assert.AreEqual(new StringType("value2"), command.Spring2StringType);
	    Assert.AreEqual(new IdType(101), command.Spring2IdType);
	}

	[Ignore(),Test()]
	public void ShouldGetBackUnhandledMessageListExcptionsInXML() {
	    //set up data
	    NameValueCollection collection = new NameValueCollection();

	    //get instance of SampleAjaxCommand
	    UnhandledMessageListExceptionAjaxCommand command = new UnhandledMessageListExceptionAjaxCommand(0, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());

	    ErrorResponse response = JsonConvert.DeserializeObject(command.RunCommand(), typeof(ErrorResponse)) as ErrorResponse;

	    Assert.AreEqual(true, response.unhandledException);
	    Assert.AreEqual("TE'ST%0aTE'ST'2%0a", response.message);
	}

	[Ignore(),Test()]
	public void ShouldGetBackUnhandledSystemExcptionsInXML() {
	    //set up data
	    NameValueCollection collection = new NameValueCollection();

	    //get instance of UnhandledSystemExceptionAjaxCommand
	    UnhandledSystemExceptionAjaxCommand command = new UnhandledSystemExceptionAjaxCommand(0, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());

	    ErrorResponse response = JsonConvert.DeserializeObject(command.RunCommand(), typeof(ErrorResponse)) as ErrorResponse;

	    Assert.AreEqual(true, response.unhandledException);
	    Assert.IsTrue(response.message.StartsWith("There+was+a+problem.%0aInform+supp"));
	}

	[Test()]
	public void ShouldHavePopulationErrorsAvailable() {
	    //set up data
	    NameValueCollection collection = new NameValueCollection();
	    collection.Add("systemString", "value1");
	    collection.Add("spring2StringType", "value2");
	    collection.Add("spring2IdType", "One0One");

	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = new SampleAjaxCommand(0, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    String returnValue = command.RunCommand();

	    //assert object was populated
	    Assert.AreEqual("value1", command.SystemString);
	    Assert.AreEqual(new StringType("value2"), command.Spring2StringType);
	    Assert.IsTrue(command.Spring2IdType.IsDefault);
	    Assert.AreEqual("Errors", returnValue);
	}
	
	[Test()]
	public void BaseCommandShouldHaveEmptyConstructorAndReturnNameAndQualifiedName() {
	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = SampleAjaxCommand.Instance;
	    
	    Assert.AreEqual("SampleAjaxCommand", command.Name);
	    Assert.AreEqual("Spring2.Core.Test.Ajax.SampleAjaxCommand,Spring2.Core.Test.Ajax", command.QualifiedName);
	}
	
	[Ignore(),Test()]
	public void ShouldGetInvalidStateExceptionWhenRunningCommandConstructedWithNoArguments() {
	    SampleAjaxCommand command = SampleAjaxCommand.Instance;

	    ErrorResponse response = JsonConvert.DeserializeObject(command.RunCommand(), typeof(ErrorResponse)) as ErrorResponse;

	    Assert.AreEqual(true, response.unhandledException);
	    Assert.IsTrue(response.message.StartsWith("There+was+a+problem.%0aInform+supp"));
	    Assert.IsTrue(response.message.IndexOf("InvalidOperationException%3a+Command+not+constructed+to+run") > 0, "Did not get right exception");
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


	public SampleAjaxCommand(Int32 responseHandlerId, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies)
	    : base(responseHandlerId, data, formatter, errors, cookies) {
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
	public UnhandledMessageListExceptionAjaxCommand(Int32 responseHandlerId, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, data, formatter, errors, cookies) { }

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
	public UnhandledSystemExceptionAjaxCommand(Int32 responseHandlerId, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, data, formatter, errors, cookies) { }

	protected override String Execute() {
	    if(test == true) {
		throw new Exception("TE'ST'2");
	    }
	    return "FAILED";
	}
    }
    #endregion Test commands

    #region Test Error ResponseObject

    public class ErrorResponse {
	public Int32 responseHandlerId;
	public Boolean unhandledException;
	public String message;
    }

    #endregion Test Error ResponseObject
}
