using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.DataObject {

    /// <summary>
    /// IMailMessage generic collection
    /// </summary>
    public class MailMessageList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailMessageList UNSET = new MailMessageList(true);
	[Generate]
	public static readonly MailMessageList DEFAULT = new MailMessageList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailMessageList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailMessageList() {
	}

	// Indexer implementation.
	[Generate]
	public IMailMessage this[int index] {
	    get { return (IMailMessage) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMailMessage value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMailMessage value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IMailMessage value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IMailMessage value) {
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
	public void Remove(IMailMessage value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IMailMessage) {
		    Add((IMailMessage)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IMailMessage");
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
	public Boolean Contains(IdType mailMessageId) {
	    foreach(IMailMessage o in List) {
		if (o.MailMessageId.Equals(mailMessageId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMailMessage this[IdType mailMessageId] {
	    get { 
		foreach(IMailMessage o in List) {
		    if (o.MailMessageId.Equals(mailMessageId)) {
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
	public MailMessageList RetainAll(MailMessageList list) {
	    MailMessageList result = new MailMessageList();

	    foreach(IMailMessage data in List) {
		if (list.Contains(data.MailMessageId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public MailMessageList RemoveAll(MailMessageList list) {
	    MailMessageList result = new MailMessageList();

	    foreach(IMailMessage data in List) {
		if (!list.Contains(data.MailMessageId)) {
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
	public class ScheduleTimeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.ScheduleTime.IsValid || !o2.ScheduleTime.IsValid) {
		    return 0;
		}
		return o1.ScheduleTime.CompareTo(o2.ScheduleTime);
	    }
	}

	[Generate]
	public class ProcessedTimeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.ProcessedTime.IsValid || !o2.ProcessedTime.IsValid) {
		    return 0;
		}
		return o1.ProcessedTime.CompareTo(o2.ProcessedTime);
	    }
	}

	[Generate]
	public class FromSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.From.IsValid || !o2.From.IsValid) {
		    return 0;
		}
		return o1.From.CompareTo(o2.From);
	    }
	}

	[Generate]
	public class ToSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.To.IsValid || !o2.To.IsValid) {
		    return 0;
		}
		return o1.To.CompareTo(o2.To);
	    }
	}

	[Generate]
	public class CcSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Cc.IsValid || !o2.Cc.IsValid) {
		    return 0;
		}
		return o1.Cc.CompareTo(o2.Cc);
	    }
	}

	[Generate]
	public class BccSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Bcc.IsValid || !o2.Bcc.IsValid) {
		    return 0;
		}
		return o1.Bcc.CompareTo(o2.Bcc);
	    }
	}

	[Generate]
	public class SubjectSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Subject.IsValid || !o2.Subject.IsValid) {
		    return 0;
		}
		return o1.Subject.CompareTo(o2.Subject);
	    }
	}

	[Generate]
	public class BodySorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Body.IsValid || !o2.Body.IsValid) {
		    return 0;
		}
		return o1.Body.CompareTo(o2.Body);
	    }
	}

	[Generate]
	public class MailMessageTypeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.MailMessageType.IsValid || !o2.MailMessageType.IsValid) {
		    return 0;
		}
		return o1.MailMessageType.CompareTo(o2.MailMessageType);
	    }
	}

	[Generate]
	public class MessageQueueDateSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.MessageQueueDate.IsValid || !o2.MessageQueueDate.IsValid) {
		    return 0;
		}
		return o1.MessageQueueDate.CompareTo(o2.MessageQueueDate);
	    }
	}

    }
}
