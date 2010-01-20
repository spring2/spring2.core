using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Types {

    /// <summary>
    /// CountryCodeEnum generic collection
    /// </summary>
    public class CountryCodeEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly CountryCodeEnumList UNSET = new CountryCodeEnumList(true);
	[Generate]
	public static readonly CountryCodeEnumList DEFAULT = new CountryCodeEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private CountryCodeEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public CountryCodeEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public CountryCodeEnum this[int index] {
	    get { return (CountryCodeEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(CountryCodeEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(CountryCodeEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(CountryCodeEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, CountryCodeEnum value) {
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
	public void Remove(CountryCodeEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is CountryCodeEnum) {
		    Add((CountryCodeEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to CountryCodeEnum");
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
