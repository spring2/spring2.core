using System;

using NUnit.Framework;

using Spring2.Core.Message;

namespace Spring2.Core.Test {

    [TestFixture]
    public class MessageTest {

	[Test]
	public void TestMissingFieldMessage() {
	    TestMessage message = new TestMessage("Param1", "Param2");
	    IMessageFormatter formatter = new SimpleFormatter();
	    Assertion.AssertEquals("Message not properly formatted.", "First parameter is Param1. Second parameter is Param2.", formatter.Format(message));
	}

	[Test] 
	public void TestMessageListException() {
	    MessageList messages = new MessageList();
	    for (Int32 i = 0; i < 3; i++) {
		messages.Add(new TestMessage("Param1", "Param2"));
	    }
	    MessageListException ex = new MessageListException(messages);
	    Assertion.AssertEquals("Should have three messages.", 3, ex.Messages.Count);
	    IMessageFormatter formatter = new SimpleFormatter();
	    for (Int32 i = 0; i < 3; i++) {
		Assertion.AssertEquals("Message not properly formatted.", "First parameter is Param1. Second parameter is Param2.", formatter.Format(ex.Messages[i]));
	    }
	}
    }
}
