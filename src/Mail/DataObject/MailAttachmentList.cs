using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.DataObject {

    /// <summary>
    /// IMailAttachment generic collection
    /// </summary>
    public class MailAttachmentList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailAttachmentList UNSET = new MailAttachmentList(true);
	[Generate]
	public static readonly MailAttachmentList DEFAULT = new MailAttachmentList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailAttachmentList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailAttachmentList() {
	}

	// Indexer implementation.
	[Generate]
	public IMailAttachment this[int index] {
	    get { return (IMailAttachment) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMailAttachment value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMailAttachment value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IMailAttachment value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IMailAttachment value) {
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
	public void Remove(IMailAttachment value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IMailAttachment) {
		    Add((IMailAttachment)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IMailAttachment");
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
	public Boolean Contains(IdType mailAttachmentId) {
	    foreach(IMailAttachment o in List) {
		if (o.MailAttachmentId.Equals(mailAttachmentId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMailAttachment this[IdType mailAttachmentId] {
	    get { 
		foreach(IMailAttachment o in List) {
		    if (o.MailAttachmentId.Equals(mailAttachmentId)) {
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
	public MailAttachmentList RetainAll(MailAttachmentList list) {
	    MailAttachmentList result = new MailAttachmentList();

	    foreach(IMailAttachment data in List) {
		if (list.Contains(data.MailAttachmentId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public MailAttachmentList RemoveAll(MailAttachmentList list) {
	    MailAttachmentList result = new MailAttachmentList();

	    foreach(IMailAttachment data in List) {
		if (!list.Contains(data.MailAttachmentId)) {
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
	public class FilenameSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailAttachment o1 = (IMailAttachment)a;
		IMailAttachment o2 = (IMailAttachment)b;

		if (o1 == null || o2 == null || !o1.Filename.IsValid || !o2.Filename.IsValid) {
		    return 0;
		}
		return o1.Filename.CompareTo(o2.Filename);
	    }
	}

    }
}
