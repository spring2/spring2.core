using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Publication.DataObject {

    /// <summary>
    /// IPublicationType generic collection
    /// </summary>
    public class PublicationTypeList : CollectionBase {

	[Generate]
	public static readonly PublicationTypeList UNSET = new PublicationTypeList(true);
	[Generate]
	public static readonly PublicationTypeList DEFAULT = new PublicationTypeList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private PublicationTypeList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public PublicationTypeList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IPublicationType this[int index] {
	    get {
		return (IPublicationType)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.PublicationTypeId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IPublicationType value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.PublicationTypeId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, IPublicationType value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.PublicationTypeId] = value;
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
		    IPublicationType value = this[index];
		    keys.Remove(value.PublicationTypeId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IPublicationType) {
		    Add((IPublicationType)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IPublicationType");
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
	public Boolean Contains(IdType publicationTypeId) {
	    return keys.Contains(publicationTypeId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IPublicationType this[IdType publicationTypeId] {
	    get {
		return keys[publicationTypeId] as IPublicationType;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public PublicationTypeList RetainAll(PublicationTypeList list) {
	    PublicationTypeList result = new PublicationTypeList();

	    foreach(IPublicationType data in List) {
		if (list.Contains(data.PublicationTypeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public PublicationTypeList RemoveAll(PublicationTypeList list) {
	    PublicationTypeList result = new PublicationTypeList();

	    foreach(IPublicationType data in List) {
		if (!list.Contains(data.PublicationTypeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType publicationTypeId) {
	    if(!immutable) {
		IPublicationType objectInList = this[publicationTypeId];
		List.Remove(objectInList);
		keys.Remove(publicationTypeId);
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
	public class PublicationTypeIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.PublicationTypeId.IsValid || !o2.PublicationTypeId.IsValid) {
		    return 0;
		}
		return o1.PublicationTypeId.CompareTo(o2.PublicationTypeId);
	    }
	}

	[Generate]
	public class NameSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.Name.IsValid || !o2.Name.IsValid) {
		    return 0;
		}
		return o1.Name.CompareTo(o2.Name);
	    }
	}

	[Generate]
	public class DescriptionSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.Description.IsValid || !o2.Description.IsValid) {
		    return 0;
		}
		return o1.Description.CompareTo(o2.Description);
	    }
	}

	[Generate]
	public class MailMessageTypeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.MailMessageType.IsValid || !o2.MailMessageType.IsValid) {
		    return 0;
		}
		return o1.MailMessageType.CompareTo(o2.MailMessageType);
	    }
	}

	[Generate]
	public class LastSentDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.LastSentDate.IsValid || !o2.LastSentDate.IsValid) {
		    return 0;
		}
		return o1.LastSentDate.CompareTo(o2.LastSentDate);
	    }
	}

	[Generate]
	public class FrequencyInMinutesSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.FrequencyInMinutes.IsValid || !o2.FrequencyInMinutes.IsValid) {
		    return 0;
		}
		return o1.FrequencyInMinutes.CompareTo(o2.FrequencyInMinutes);
	    }
	}

	[Generate]
	public class ProviderNameSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.ProviderName.IsValid || !o2.ProviderName.IsValid) {
		    return 0;
		}
		return o1.ProviderName.CompareTo(o2.ProviderName);
	    }
	}

	[Generate]
	public class EffectiveDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.EffectiveDate.IsValid || !o2.EffectiveDate.IsValid) {
		    return 0;
		}
		return o1.EffectiveDate.CompareTo(o2.EffectiveDate);
	    }
	}

	[Generate]
	public class ExpirationDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.ExpirationDate.IsValid || !o2.ExpirationDate.IsValid) {
		    return 0;
		}
		return o1.ExpirationDate.CompareTo(o2.ExpirationDate);
	    }
	}

	[Generate]
	public class CreateDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.CreateDate.IsValid || !o2.CreateDate.IsValid) {
		    return 0;
		}
		return o1.CreateDate.CompareTo(o2.CreateDate);
	    }
	}

	[Generate]
	public class CreateUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.CreateUserId.IsValid || !o2.CreateUserId.IsValid) {
		    return 0;
		}
		return o1.CreateUserId.CompareTo(o2.CreateUserId);
	    }
	}

	[Generate]
	public class LastModifiedUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.LastModifiedUserId.IsValid || !o2.LastModifiedUserId.IsValid) {
		    return 0;
		}
		return o1.LastModifiedUserId.CompareTo(o2.LastModifiedUserId);
	    }
	}

	[Generate]
	public class LastModifiedDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IPublicationType o1 = (IPublicationType)a;
		IPublicationType o2 = (IPublicationType)b;

		if (o1 == null || o2 == null || !o1.LastModifiedDate.IsValid || !o2.LastModifiedDate.IsValid) {
		    return 0;
		}
		return o1.LastModifiedDate.CompareTo(o2.LastModifiedDate);
	    }
	}

    }
}
