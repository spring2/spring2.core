using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// DateType generic collection
    /// </summary>
    public class DateTypeList : System.Collections.CollectionBase {
	
	public static readonly DateTypeList UNSET = new DateTypeList(true);
	public static readonly DateTypeList DEFAULT = new DateTypeList(true);

	private Boolean immutable = false;
	
	private DateTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public DateTypeList() {
	}

	// Indexer implementation.
	public DateType this[int index] {
	    get { return (DateType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(DateType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(DateType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(DateType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, DateType value) {
	    if (!immutable) {
		List.Insert(index, value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

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

	public void Remove(DateType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is DateType) {
		    Add((DateType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to DateType");
		}
	    }
	}
	
	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}
	
	public Boolean IsValid {
	    get {
		return !(IsDefault || IsUnset);
	    }
	}

    }
}
