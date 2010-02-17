using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;



namespace Spring2.Core.CommunicationSubscription.DataObject {

    /// <summary>
    /// IPublicationTracking generic collection
    /// </summary>
    public class PublicationTrackingList : CollectionBase {

	[Generate]
	public static readonly PublicationTrackingList UNSET = new PublicationTrackingList(true);
	[Generate]
	public static readonly PublicationTrackingList DEFAULT = new PublicationTrackingList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private PublicationTrackingList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public PublicationTrackingList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IPublicationTracking this[int index] {
	    get {
		return (IPublicationTracking)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.PublicationPrimaryKeyId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IPublicationTracking value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.PublicationPrimaryKeyId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, IPublicationTracking value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.PublicationPrimaryKeyId] = value;
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
		    IPublicationTracking value = this[index];
		    keys.Remove(value.PublicationPrimaryKeyId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IPublicationTracking) {
		    Add((IPublicationTracking)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IPublicationTracking");
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
	public Boolean Contains(IdType publicationPrimaryKeyId) {
	    return keys.Contains(publicationPrimaryKeyId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IPublicationTracking this[IdType publicationPrimaryKeyId] {
	    get {
		return keys[publicationPrimaryKeyId] as IPublicationTracking;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public PublicationTrackingList RetainAll(PublicationTrackingList list) {
	    PublicationTrackingList result = new PublicationTrackingList();

	    foreach(IPublicationTracking data in List) {
		if (list.Contains(data.PublicationPrimaryKeyId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public PublicationTrackingList RemoveAll(PublicationTrackingList list) {
	    PublicationTrackingList result = new PublicationTrackingList();

	    foreach(IPublicationTracking data in List) {
		if (!list.Contains(data.PublicationPrimaryKeyId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType publicationPrimaryKeyId) {
	    if(!immutable) {
		IPublicationTracking objectInList = this[publicationPrimaryKeyId];
		List.Remove(objectInList);
		keys.Remove(publicationPrimaryKeyId);
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
	public class PublicationPrimaryKeyIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.PublicationPrimaryKeyId.IsValid || !o2.PublicationPrimaryKeyId.IsValid) {
		    return 0;
		}
		return o1.PublicationPrimaryKeyId.CompareTo(o2.PublicationPrimaryKeyId);
	    }
	}

	[Generate]
	public class PublicationTypeIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.PublicationTypeId.IsValid || !o2.PublicationTypeId.IsValid) {
		    return 0;
		}
		return o1.PublicationTypeId.CompareTo(o2.PublicationTypeId);
	    }
	}

	[Generate]
	public class CreateDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.CreateDate.IsValid || !o2.CreateDate.IsValid) {
		    return 0;
		}
		return o1.CreateDate.CompareTo(o2.CreateDate);
	    }
	}

	[Generate]
	public class CreateUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.CreateUserId.IsValid || !o2.CreateUserId.IsValid) {
		    return 0;
		}
		return o1.CreateUserId.CompareTo(o2.CreateUserId);
	    }
	}

	[Generate]
	public class LastModifiedUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.LastModifiedUserId.IsValid || !o2.LastModifiedUserId.IsValid) {
		    return 0;
		}
		return o1.LastModifiedUserId.CompareTo(o2.LastModifiedUserId);
	    }
	}

	[Generate]
	public class LastModifiedDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationTracking o1 = (IPublicationTracking)a;
		IPublicationTracking o2 = (IPublicationTracking)b;

		if (o1 == null || o2 == null || !o1.LastModifiedDate.IsValid || !o2.LastModifiedDate.IsValid) {
		    return 0;
		}
		return o1.LastModifiedDate.CompareTo(o2.LastModifiedDate);
	    }
	}

    }
}
