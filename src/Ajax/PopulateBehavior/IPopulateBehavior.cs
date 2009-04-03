using System;
using System.Collections.Specialized;

using Spring2.Core.Message;

namespace Spring2.Core.Ajax.PopulateBehavior {
    public interface IPopulateBehavior {
	MessageList Populate(Object target, NameValueCollection data);
    }
}
