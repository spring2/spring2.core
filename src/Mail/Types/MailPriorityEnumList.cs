using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// MailPriorityEnum generic collection
    /// </summary>
    public class MailPriorityEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailPriorityEnumList UNSET = new MailPriorityEnumList(true);
	[Generate]
	public static readonly MailPriorityEnumList DEFAULT = new MailPriorityEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailPriorityEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailPriorityEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public MailPriorityEnum this[int index] {
	    get { return (MailPriorityEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(MailPriorityEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(MailPriorityEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(MailPriorityEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, MailPriorityEnum value) {
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
	public void Remove(MailPriorityEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is MailPriorityEnum) {
		    Add((MailPriorityEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to MailPriorityEnum");
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
