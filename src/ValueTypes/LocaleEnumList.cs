using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// LocaleEnum generic collection
    /// </summary>
    public class LocaleEnumList : System.Collections.CollectionBase {
	
	
	public static readonly LocaleEnumList UNSET = new LocaleEnumList(true);
	
	public static readonly LocaleEnumList DEFAULT = new LocaleEnumList(true);

	
	private Boolean immutable = false;
	
	
	private LocaleEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	
	public LocaleEnumList() {
	}

	// Indexer implementation.
	
	public LocaleEnum this[int index] {
	    get { return (LocaleEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	
	public void Add(LocaleEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	
	public Boolean Contains(LocaleEnum value) {
	    return List.Contains(value);
	}
	
	
	public Int32 IndexOf(LocaleEnum value) {
	    return List.IndexOf(value);
	}
	
	
	public void Insert(Int32 index, LocaleEnum value) {
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

	
	public void Remove(LocaleEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is LocaleEnum) {
		    Add((LocaleEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to LocaleEnum");
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
