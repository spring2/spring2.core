using System;

namespace Spring2.Core.Message {

    public class MessageListException : ApplicationException {

	MessageList messages = new MessageList();

	public MessageListException(MessageList messages)  {
	    this.messages = messages == null ? new MessageList() : messages;
	}

	public MessageList Messages {
	    get {
		return messages;
	    }
	}
    }
}
