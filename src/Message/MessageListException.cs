using System;

namespace Spring2.Core.Message {

    public class MessageListException : ApplicationException {

	MessageList messages = new MessageList();

	public MessageListException(MessageList messages) {
	    this.messages = messages == null ? new MessageList() : messages;
	}

	public MessageList Messages {
	    get {
		return messages;
	    }
	}

	public string Messag e{
	    get {
		if (messages.Count > 0) {
		    SimpleFormatter formatter = new SimpleFormatter();
		    StringBuilder sb = new StringBuilder();
		    foreach (Message m in messages) {
			sb.Append(formatter.Format(m) + Environment.NewLine);
		    }
		    return sb.ToString();
		} else {
		    return base.Message;
		}
	    }
	}

    }
}
