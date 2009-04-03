using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// MailMessageStatusEnum generic collection
    /// </summary>
    public class MailMessageStatusEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailMessageStatusEnumList UNSET = new MailMessageStatusEnumList(true);
	[Generate]
	public static readonly MailMessageStatusEnumList DEFAULT = new MailMessageStatusEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailMessageStatusEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailMessageStatusEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public MailMessageStatusEnum this[int index] {
	    get { return (MailMessageStatusEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(MailMessageStatusEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(MailMessageStatusEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(MailMessageStatusEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, MailMessageStatusEnum value) {
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
	public void Remove(MailMessageStatusEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is MailMessageStatusEnum) {
		    Add((MailMessageStatusEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to MailMessageStatusEnum");
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


    }
}
