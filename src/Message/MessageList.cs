using System;
using System.Collections;

namespace Spring2.Core.Message {

    public class MessageList : CollectionBase {

	public MessageList() {
	}

	public Message this[int index] {
	    get { return (Message)List[index]; }
	    set { List[index] = value; }
	}

	public void Add(Message value) {
	    List.Add(value);
	}

	public void AddRange(IList messages) {
	    foreach (Message message in  messages) {
		Add(message);
	    }
	}
    }
}
