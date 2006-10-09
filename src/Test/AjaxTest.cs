using System;
using System.Collections.Specialized;
using System.Web;

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

	    //get instance of SampleAjaxCommand
	    SampleAjaxCommand command = new SampleAjaxCommand(0, String.Empty, collection, new SimpleFormatter(), new MessageList(), new HttpCookieCollection());
	    command.Execute();

	    //assert object was populated
	    Assert.AreEqual("value1", command.SystemString);
	    Assert.AreEqual(new StringType("value2"), command.Spring2StringType);
	}

	public class SampleAjaxCommand : Command {
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

	    public SampleAjaxCommand (Int32 responseHandlerId, String commandIdentifier, NameValueCollection data, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base (responseHandlerId, commandIdentifier, data, formatter, errors, cookies) {
	    }

	    public override String Execute() {
		return String.Empty;
	    }

	}
    }
}
