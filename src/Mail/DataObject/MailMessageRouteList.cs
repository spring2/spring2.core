using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.DataObject {

    /// <summary>
    /// IMailMessageRoute generic collection
    /// </summary>
    public class MailMessageRouteList : CollectionBase {

	[Generate]
	public static readonly MailMessageRouteList UNSET = new MailMessageRouteList(true);
	[Generate]
	public static readonly MailMessageRouteList DEFAULT = new MailMessageRouteList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private MailMessageRouteList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public MailMessageRouteList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IMailMessageRoute this[int index] {
	    get {
		return (IMailMessageRoute)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.MailMessageRouteId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMailMessageRoute value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.MailMessageRouteId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, IMailMessageRoute value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.MailMessageRouteId] = value;
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
		    IMailMessageRoute value = this[index];
		    keys.Remove(value.MailMessageRouteId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IMailMessageRoute) {
		    Add((IMailMessageRoute)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IMailMessageRoute");
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
	public Boolean Contains(IdType mailMessageRouteId) {
	    return keys.Contains(mailMessageRouteId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMailMessageRoute this[IdType mailMessageRouteId] {
	    get {
		return keys[mailMessageRouteId] as IMailMessageRoute;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public MailMessageRouteList RetainAll(MailMessageRouteList list) {
	    MailMessageRouteList result = new MailMessageRouteList();

	    foreach(IMailMessageRoute data in List) {
		if (list.Contains(data.MailMessageRouteId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public MailMessageRouteList RemoveAll(MailMessageRouteList list) {
	    MailMessageRouteList result = new MailMessageRouteList();

	    foreach(IMailMessageRoute data in List) {
		if (!list.Contains(data.MailMessageRouteId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType mailMessageRouteId) {
	    if(!immutable) {
		IMailMessageRoute objectInList = this[mailMessageRouteId];
		List.Remove(objectInList);
		keys.Remove(mailMessageRouteId);
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
	public class MailMessageRouteIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessageRoute o1 = (IMailMessageRoute)a;
		IMailMessageRoute o2 = (IMailMessageRoute)b;

		if (o1 == null || o2 == null || !o1.MailMessageRouteId.IsValid || !o2.MailMessageRouteId.IsValid) {
		    return 0;
		}
		return o1.MailMessageRouteId.CompareTo(o2.MailMessageRouteId);
	    }
	}

	[Generate]
	public class MailMessageSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessageRoute o1 = (IMailMessageRoute)a;
		IMailMessageRoute o2 = (IMailMessageRoute)b;

		if (o1 == null || o2 == null || !o1.MailMessage.IsValid || !o2.MailMessage.IsValid) {
		    return 0;
		}
		return o1.MailMessage.CompareTo(o2.MailMessage);
	    }
	}

	[Generate]
	public class EmailAddressSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessageRoute o1 = (IMailMessageRoute)a;
		IMailMessageRoute o2 = (IMailMessageRoute)b;

		if (o1 == null || o2 == null || !o1.EmailAddress.IsValid || !o2.EmailAddress.IsValid) {
		    return 0;
		}
		return o1.EmailAddress.CompareTo(o2.EmailAddress);
	    }
	}

    }
}
