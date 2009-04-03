using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// CurrencyType generic collection
    /// </summary>
    public class CurrencyTypeList : System.Collections.CollectionBase {
	
	public static readonly CurrencyTypeList UNSET = new CurrencyTypeList(true);
	public static readonly CurrencyTypeList DEFAULT = new CurrencyTypeList(true);

	private Boolean immutable = false;
	
	private CurrencyTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public CurrencyTypeList() {
	}

	// Indexer implementation.
	public CurrencyType this[int index] {
	    get { return (CurrencyType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(CurrencyType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(CurrencyType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(CurrencyType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, CurrencyType value) {
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

	public void Remove(CurrencyType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is CurrencyType) {
		    Add((CurrencyType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to CurrencyType");
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
