using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Geocode {

    /// <summary>
    /// GeocodeStatusEnum generic collection
    /// </summary>
    public class GeocodeStatusEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly GeocodeStatusEnumList UNSET = new GeocodeStatusEnumList(true);
	[Generate]
	public static readonly GeocodeStatusEnumList DEFAULT = new GeocodeStatusEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private GeocodeStatusEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public GeocodeStatusEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public GeocodeStatusEnum this[int index] {
	    get { return (GeocodeStatusEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(GeocodeStatusEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(GeocodeStatusEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(GeocodeStatusEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, GeocodeStatusEnum value) {
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
	public void Remove(GeocodeStatusEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is GeocodeStatusEnum) {
		    Add((GeocodeStatusEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to GeocodeStatusEnum");
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
