using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Types {

    /// <summary>
    /// CurrencyCodeEnum generic collection
    /// </summary>
    public class CurrencyCodeEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly CurrencyCodeEnumList UNSET = new CurrencyCodeEnumList(true);
	[Generate]
	public static readonly CurrencyCodeEnumList DEFAULT = new CurrencyCodeEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private CurrencyCodeEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public CurrencyCodeEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public CurrencyCodeEnum this[int index] {
	    get { return (CurrencyCodeEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(CurrencyCodeEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(CurrencyCodeEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(CurrencyCodeEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, CurrencyCodeEnum value) {
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
	public void Remove(CurrencyCodeEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is CurrencyCodeEnum) {
		    Add((CurrencyCodeEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to CurrencyCodeEnum");
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
