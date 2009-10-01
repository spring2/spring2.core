using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.DataObject {

    /// <summary>
    /// IMailMessage generic collection
    /// </summary>
    public class MailMessageList : CollectionBase {

	[Generate]
	public static readonly MailMessageList UNSET = new MailMessageList(true);
	[Generate]
	public static readonly MailMessageList DEFAULT = new MailMessageList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private MailMessageList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public MailMessageList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IMailMessage this[int index] {
	    get {
		return (IMailMessage)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.MailMessageId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMailMessage value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.MailMessageId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, IMailMessage value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.MailMessageId] = value;
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
		    IMailMessage value = this[index];
		    keys.Remove(value.MailMessageId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IMailMessage) {
		    Add((IMailMessage)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IMailMessage");
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
	public Boolean Contains(IdType mailMessageId) {
	    return keys.Contains(mailMessageId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMailMessage this[IdType mailMessageId] {
	    get {
		return keys[mailMessageId] as IMailMessage;
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
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType mailMessageId) {
	    if(!immutable) {
		IMailMessage objectInList = this[mailMessageId];
		List.Remove(objectInList);
		keys.Remove(mailMessageId);
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
	public class MailMessageIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.MailMessageId.IsValid || !o2.MailMessageId.IsValid) {
		    return 0;
		}
		return o1.MailMessageId.CompareTo(o2.MailMessageId);
	    }
	}

	[Generate]
	public class ScheduleTimeSorter : IComparer {
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
	public class ProcessedTimeSorter : IComparer {
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
	public class FromSorter : IComparer {
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
	public class ToSorter : IComparer {
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
	public class CcSorter : IComparer {
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
	public class BccSorter : IComparer {
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
	public class SubjectSorter : IComparer {
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
	public class BodySorter : IComparer {
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
	public class ReleasedByUserIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.ReleasedByUserId.IsValid || !o2.ReleasedByUserId.IsValid) {
		    return 0;
		}
		return o1.ReleasedByUserId.CompareTo(o2.ReleasedByUserId);
	    }
	}

	[Generate]
	public class MailMessageTypeSorter : IComparer {
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
	public class NumberOfAttemptsSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.NumberOfAttempts.IsValid || !o2.NumberOfAttempts.IsValid) {
		    return 0;
		}
		return o1.NumberOfAttempts.CompareTo(o2.NumberOfAttempts);
	    }
	}

	[Generate]
	public class MessageQueueDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.MessageQueueDate.IsValid || !o2.MessageQueueDate.IsValid) {
		    return 0;
		}
		return o1.MessageQueueDate.CompareTo(o2.MessageQueueDate);
	    }
	}

	[Generate]
	public class ReferenceKeySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.ReferenceKey.IsValid || !o2.ReferenceKey.IsValid) {
		    return 0;
		}
		return o1.ReferenceKey.CompareTo(o2.ReferenceKey);
	    }
	}

	[Generate]
	public class UniqueKeySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.UniqueKey.IsValid || !o2.UniqueKey.IsValid) {
		    return 0;
		}
		return o1.UniqueKey.CompareTo(o2.UniqueKey);
	    }
	}

	[Generate]
	public class ChecksumSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Checksum.IsValid || !o2.Checksum.IsValid) {
		    return 0;
		}
		return o1.Checksum.CompareTo(o2.Checksum);
	    }
	}

	[Generate]
	public class OpenCountSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.OpenCount.IsValid || !o2.OpenCount.IsValid) {
		    return 0;
		}
		return o1.OpenCount.CompareTo(o2.OpenCount);
	    }
	}

	[Generate]
	public class BouncesSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.Bounces.IsValid || !o2.Bounces.IsValid) {
		    return 0;
		}
		return o1.Bounces.CompareTo(o2.Bounces);
	    }
	}

	[Generate]
	public class LastOpenDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.LastOpenDate.IsValid || !o2.LastOpenDate.IsValid) {
		    return 0;
		}
		return o1.LastOpenDate.CompareTo(o2.LastOpenDate);
	    }
	}

	[Generate]
	public class SmtpServerSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMailMessage o1 = (IMailMessage)a;
		IMailMessage o2 = (IMailMessage)b;

		if (o1 == null || o2 == null || !o1.SmtpServer.IsValid || !o2.SmtpServer.IsValid) {
		    return 0;
		}
		return o1.SmtpServer.CompareTo(o2.SmtpServer);
	    }
	}

    }
}
