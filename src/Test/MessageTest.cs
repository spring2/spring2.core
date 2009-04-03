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
	    Assert.AreEqual("First parameter is Param1. Second parameter is Param2.", formatter.Format(message), "Message not properly formatted.");
	}

	[Test] 
	public void TestMessageListException() {
	    MessageList messages = new MessageList();
	    for (Int32 i = 0; i < 3; i++) {
		messages.Add(new TestMessage("Param1", "Param2"));
	    }
	    MessageListException ex = new MessageListException(messages);
	    Assert.AreEqual(3, ex.Messages.Count, "Should have three messages.");
	    IMessageFormatter formatter = new SimpleFormatter();
	    for (Int32 i = 0; i < 3; i++) {
		Assert.AreEqual("First parameter is Param1. Second parameter is Param2.", formatter.Format(ex.Messages[i]), "Message not properly formatted.");
	    }
	}
    }
}
