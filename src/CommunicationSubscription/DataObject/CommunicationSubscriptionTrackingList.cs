using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.CommunicationSubscription.DataObject {

    /// <summary>
    /// ICommunicationSubscriptionTracking generic collection
    /// </summary>
    public class CommunicationSubscriptionTrackingList : CollectionBase {

	[Generate]
	public static readonly CommunicationSubscriptionTrackingList UNSET = new CommunicationSubscriptionTrackingList(true);
	[Generate]
	public static readonly CommunicationSubscriptionTrackingList DEFAULT = new CommunicationSubscriptionTrackingList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private CommunicationSubscriptionTrackingList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public CommunicationSubscriptionTrackingList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public ICommunicationSubscriptionTracking this[int index] {
	    get {
		return (ICommunicationSubscriptionTracking)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.CommunicationPrimaryKeyId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ICommunicationSubscriptionTracking value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.CommunicationPrimaryKeyId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, ICommunicationSubscriptionTracking value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.CommunicationPrimaryKeyId] = value;
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
		    ICommunicationSubscriptionTracking value = this[index];
		    keys.Remove(value.CommunicationPrimaryKeyId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is ICommunicationSubscriptionTracking) {
		    Add((ICommunicationSubscriptionTracking)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to ICommunicationSubscriptionTracking");
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
	public Boolean Contains(IdType communicationPrimaryKeyId) {
	    return keys.Contains(communicationPrimaryKeyId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public ICommunicationSubscriptionTracking this[IdType communicationPrimaryKeyId] {
	    get {
		return keys[communicationPrimaryKeyId] as ICommunicationSubscriptionTracking;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public CommunicationSubscriptionTrackingList RetainAll(CommunicationSubscriptionTrackingList list) {
	    CommunicationSubscriptionTrackingList result = new CommunicationSubscriptionTrackingList();

	    foreach(ICommunicationSubscriptionTracking data in List) {
		if (list.Contains(data.CommunicationPrimaryKeyId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public CommunicationSubscriptionTrackingList RemoveAll(CommunicationSubscriptionTrackingList list) {
	    CommunicationSubscriptionTrackingList result = new CommunicationSubscriptionTrackingList();

	    foreach(ICommunicationSubscriptionTracking data in List) {
		if (!list.Contains(data.CommunicationPrimaryKeyId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType communicationPrimaryKeyId) {
	    if(!immutable) {
		ICommunicationSubscriptionTracking objectInList = this[communicationPrimaryKeyId];
		List.Remove(objectInList);
		keys.Remove(communicationPrimaryKeyId);
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
	public class CommunicationPrimaryKeyIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.CommunicationPrimaryKeyId.IsValid || !o2.CommunicationPrimaryKeyId.IsValid) {
		    return 0;
		}
		return o1.CommunicationPrimaryKeyId.CompareTo(o2.CommunicationPrimaryKeyId);
	    }
	}

	[Generate]
	public class CommunicationSubscriptionTypeIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.CommunicationSubscriptionTypeId.IsValid || !o2.CommunicationSubscriptionTypeId.IsValid) {
		    return 0;
		}
		return o1.CommunicationSubscriptionTypeId.CompareTo(o2.CommunicationSubscriptionTypeId);
	    }
	}

	[Generate]
	public class CreateDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.CreateDate.IsValid || !o2.CreateDate.IsValid) {
		    return 0;
		}
		return o1.CreateDate.CompareTo(o2.CreateDate);
	    }
	}

	[Generate]
	public class CreateUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.CreateUserId.IsValid || !o2.CreateUserId.IsValid) {
		    return 0;
		}
		return o1.CreateUserId.CompareTo(o2.CreateUserId);
	    }
	}

	[Generate]
	public class LastModifiedUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.LastModifiedUserId.IsValid || !o2.LastModifiedUserId.IsValid) {
		    return 0;
		}
		return o1.LastModifiedUserId.CompareTo(o2.LastModifiedUserId);
	    }
	}

	[Generate]
	public class LastModifiedDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICommunicationSubscriptionTracking o1 = (ICommunicationSubscriptionTracking)a;
		ICommunicationSubscriptionTracking o2 = (ICommunicationSubscriptionTracking)b;

		if (o1 == null || o2 == null || !o1.LastModifiedDate.IsValid || !o2.LastModifiedDate.IsValid) {
		    return 0;
		}
		return o1.LastModifiedDate.CompareTo(o2.LastModifiedDate);
	    }
	}

    }
}
