using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;



namespace Spring2.Core.CommunicationSubscription.DataObject {

    /// <summary>
    /// ICommunicationSubscriptionType generic collection
    /// </summary>
    public class CommunicationSubscriptionTypeList : CollectionBase {

	[Generate]
	public static readonly CommunicationSubscriptionTypeList UNSET = new CommunicationSubscriptionTypeList(true);
	[Generate]
	public static readonly CommunicationSubscriptionTypeList DEFAULT = new CommunicationSubscriptionTypeList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private CommunicationSubscriptionTypeList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public CommunicationSubscriptionTypeList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public ICommunicationSubscriptionType this[int index] {
	    get {
		return (ICommunicationSubscriptionType)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.CommunicationSubscriptionTypeId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ICommunicationSubscriptionType value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.CommunicationSubscriptionTypeId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, ICommunicationSubscriptionType value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.CommunicationSubscriptionTypeId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(int index) {
	    if (!immutable) {
		if (index > Count - 1 || index < 0) {
		    throw new IndexOutOfRangeException();
		} else {
		    ICommunicationSubscriptionType value = this[index];
		    keys.Remove(value.CommunicationSubscriptionTypeId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is ICommunicationSubscriptionType) {
		    Add((ICommunicationSubscriptionType)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to ICommunicationSubscriptionType");
		}
	    }
	}

	[Generate]
	public Boolean IsDefault {
	    get {
		return ReferenceEquals(this, DEFAULT);
	    }
	}

	[Generate]
	public Boolean IsUnset {
	    get {
		return ReferenceEquals(this, UNSET);
	    }
	}

	[Generate]
	public Boolean IsValid {
	    get {
		return !(IsDefault || IsUnset);
	    }
	}

	/// <summary>
	/// See if the list contains an instance by identity
	/// </summary>
	[Generate]
	public Boolean Contains(IdType communicationSubscriptionTypeId) {
	    return keys.Contains(communicationSubscriptionTypeId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public ICommunicationSubscriptionType this[IdType communicationSubscriptionTypeId] {
	    get {
		return keys[communicationSubscriptionTypeId] as ICommunicationSubscriptionType;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public CommunicationSubscriptionTypeList RetainAll(CommunicationSubscriptionTypeList list) {
	    CommunicationSubscriptionTypeList result = new CommunicationSubscriptionTypeList();

	    foreach(ICommunicationSubscriptionType data in List) {
		if (list.Contains(data.CommunicationSubscriptionTypeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public CommunicationSubscriptionTypeList RemoveAll(CommunicationSubscriptionTypeList list) {
	    CommunicationSubscriptionTypeList result = new CommunicationSubscriptionTypeList();

	    foreach(ICommunicationSubscriptionType data in List) {
		if (!list.Contains(data.CommunicationSubscriptionTypeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType communicationSubscriptionTypeId) {
	    if(!immutable) {
		ICommunicationSubscriptionType objectInList = this[communicationSubscriptionTypeId];
		List.Remove(objectInList);
		keys.Remove(communicationSubscriptionTypeId);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(IComparer comparer) {
	    InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    IComparer comparer = Activator.CreateInstance(type) as IComparer;
	    if (comparer == null) {
		throw new ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    InnerList.Sort(comparer);
	}

	[Generate]
	public class CommunicationSubscriptionTypeIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.CommunicationSubscriptionTypeId.IsValid || !o2.CommunicationSubscriptionTypeId.IsValid) {
		    return 0;
		}
		return o1.CommunicationSubscriptionTypeId.CompareTo(o2.CommunicationSubscriptionTypeId);
	    }
	}

	[Generate]
	public class NameSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.Name.IsValid || !o2.Name.IsValid) {
		    return 0;
		}
		return o1.Name.CompareTo(o2.Name);
	    }
	}

	[Generate]
	public class EmailSubjectSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.EmailSubject.IsValid || !o2.EmailSubject.IsValid) {
		    return 0;
		}
		return o1.EmailSubject.CompareTo(o2.EmailSubject);
	    }
	}

	[Generate]
	public class EmailBodySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.EmailBody.IsValid || !o2.EmailBody.IsValid) {
		    return 0;
		}
		return o1.EmailBody.CompareTo(o2.EmailBody);
	    }
	}

	[Generate]
	public class EmailBodyTypeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.EmailBodyType.IsValid || !o2.EmailBodyType.IsValid) {
		    return 0;
		}
		return o1.EmailBodyType.CompareTo(o2.EmailBodyType);
	    }
	}

	[Generate]
	public class MailMessageTypeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.MailMessageType.IsValid || !o2.MailMessageType.IsValid) {
		    return 0;
		}
		return o1.MailMessageType.CompareTo(o2.MailMessageType);
	    }
	}

	[Generate]
	public class LastSentDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.LastSentDate.IsValid || !o2.LastSentDate.IsValid) {
		    return 0;
		}
		return o1.LastSentDate.CompareTo(o2.LastSentDate);
	    }
	}

	[Generate]
	public class FrequencyInMinutesSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.FrequencyInMinutes.IsValid || !o2.FrequencyInMinutes.IsValid) {
		    return 0;
		}
		return o1.FrequencyInMinutes.CompareTo(o2.FrequencyInMinutes);
	    }
	}

	[Generate]
	public class ProviderNameSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.ProviderName.IsValid || !o2.ProviderName.IsValid) {
		    return 0;
		}
		return o1.ProviderName.CompareTo(o2.ProviderName);
	    }
	}

	[Generate]
	public class EffectiveDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.EffectiveDate.IsValid || !o2.EffectiveDate.IsValid) {
		    return 0;
		}
		return o1.EffectiveDate.CompareTo(o2.EffectiveDate);
	    }
	}

	[Generate]
	public class ExpirationDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.ExpirationDate.IsValid || !o2.ExpirationDate.IsValid) {
		    return 0;
		}
		return o1.ExpirationDate.CompareTo(o2.ExpirationDate);
	    }
	}

	[Generate]
	public class CreateDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.CreateDate.IsValid || !o2.CreateDate.IsValid) {
		    return 0;
		}
		return o1.CreateDate.CompareTo(o2.CreateDate);
	    }
	}

	[Generate]
	public class CreateUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.CreateUserId.IsValid || !o2.CreateUserId.IsValid) {
		    return 0;
		}
		return o1.CreateUserId.CompareTo(o2.CreateUserId);
	    }
	}

	[Generate]
	public class LastModifiedUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.LastModifiedUserId.IsValid || !o2.LastModifiedUserId.IsValid) {
		    return 0;
		}
		return o1.LastModifiedUserId.CompareTo(o2.LastModifiedUserId);
	    }
	}

	[Generate]
	public class LastModifiedDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionType o1 = (ICommunicationSubscriptionType)a;
		ICommunicationSubscriptionType o2 = (ICommunicationSubscriptionType)b;

		if (o1 == null || o2 == null || !o1.LastModifiedDate.IsValid || !o2.LastModifiedDate.IsValid) {
		    return 0;
		}
		return o1.LastModifiedDate.CompareTo(o2.LastModifiedDate);
	    }
	}

    }
}
