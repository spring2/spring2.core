using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.DataObject {

    /// <summary>
    /// IMailMessageRoute generic collection
    /// </summary>
    public class MailMessageRouteList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailMessageRouteList UNSET = new MailMessageRouteList(true);
	[Generate]
	public static readonly MailMessageRouteList DEFAULT = new MailMessageRouteList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailMessageRouteList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailMessageRouteList() {
	}

	// Indexer implementation.
	[Generate]
	public IMailMessageRoute this[int index] {
	    get { return (IMailMessageRoute) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMailMessageRoute value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMailMessageRoute value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IMailMessageRoute value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IMailMessageRoute value) {
	    if (!immutable) {
	    	List.Insert(index, value);
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
		    List.RemoveAt(index); 
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(IMailMessageRoute value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IMailMessageRoute) {
		    Add((IMailMessageRoute)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IMailMessageRoute");
		}
	    }
	}
	
	[Generate]
	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	[Generate]
	public Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
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
	    foreach(IMailMessageRoute o in List) {
		if (o.MailMessageRouteId.Equals(mailMessageRouteId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMailMessageRoute this[IdType mailMessageRouteId] {
	    get { 
		foreach(IMailMessageRoute o in List) {
		    if (o.MailMessageRouteId.Equals(mailMessageRouteId)) {
			return o;
		    }
		}

		// not found
		return null;
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
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(System.Collections.IComparer comparer) {
	    this.InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new System.ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    System.Collections.IComparer comparer = Activator.CreateInstance(type) as System.Collections.IComparer;
	    if (comparer == null) {
		throw new System.ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    this.InnerList.Sort(comparer);
	}

	[Generate]
	public class MailMessageSorter : System.Collections.IComparer {
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
	public class EmailAddressSorter : System.Collections.IComparer {
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
