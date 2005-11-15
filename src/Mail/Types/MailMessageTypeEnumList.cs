using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// MailMessageEnum generic collection
    /// </summary>
    public class MailMessageTypeEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailMessageTypeEnumList UNSET = new MailMessageTypeEnumList(true);
	[Generate]
	public static readonly MailMessageTypeEnumList DEFAULT = new MailMessageTypeEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailMessageTypeEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailMessageTypeEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public MailMessageTypeEnum this[int index] {
	    get { return (MailMessageTypeEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(MailMessageTypeEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(MailMessageTypeEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(MailMessageTypeEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, MailMessageTypeEnum value) {
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
	public void Remove(MailMessageTypeEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is MailMessageTypeEnum) {
		    Add((MailMessageTypeEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to MailMessageEnum");
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
