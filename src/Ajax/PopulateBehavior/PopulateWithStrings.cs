using System;
using System.Collections.Specialized;

using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;

namespace Spring2.Core.Ajax.PopulateBehavior {
    public class PopulateWithStrings : IPopulateBehavior {
	public static PopulateWithStrings Instance = new PopulateWithStrings();

	private PopulateWithStrings() {}

	public MessageList Populate(Object target, NameValueCollection data) {
	    return Populator.Instance.Populate(target, data);
	}
    }
}
