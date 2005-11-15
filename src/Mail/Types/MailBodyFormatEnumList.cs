using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// MailBodyFormatEnum generic collection
    /// </summary>
    public class MailBodyFormatEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly MailBodyFormatEnumList UNSET = new MailBodyFormatEnumList(true);
	[Generate]
	public static readonly MailBodyFormatEnumList DEFAULT = new MailBodyFormatEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private MailBodyFormatEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public MailBodyFormatEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public MailBodyFormatEnum this[int index] {
	    get { return (MailBodyFormatEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(MailBodyFormatEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(MailBodyFormatEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(MailBodyFormatEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, MailBodyFormatEnum value) {
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
	public void Remove(MailBodyFormatEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is MailBodyFormatEnum) {
		    Add((MailBodyFormatEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to MailBodyFormatEnum");
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
