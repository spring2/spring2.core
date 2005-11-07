using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Geocode {

    /// <summary>
    /// IAddressCache generic collection
    /// </summary>
    public class AddressCacheList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly AddressCacheList UNSET = new AddressCacheList(true);
	[Generate]
	public static readonly AddressCacheList DEFAULT = new AddressCacheList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private AddressCacheList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public AddressCacheList() {
	}

	// Indexer implementation.
	[Generate]
	public IAddressCache this[int index] {
	    get { return (IAddressCache) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IAddressCache value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IAddressCache value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IAddressCache value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IAddressCache value) {
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
	public void Remove(IAddressCache value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IAddressCache) {
		    Add((IAddressCache)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IAddressCache");
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

	/// <summary>
	/// See if the list contains an instance by identity
	/// </summary>
	[Generate]
	public Boolean Contains(IdType addressId) {
	    foreach(IAddressCache o in List) {
		if (o.AddressId.Equals(addressId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IAddressCache this[IdType addressId] {
	    get { 
		foreach(IAddressCache o in List) {
		    if (o.AddressId.Equals(addressId)) {
			return o;
		    }
		}

		// not found
		return null;
	    }
	}
	
	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public AddressCacheList RetainAll(AddressCacheList list) {
	    AddressCacheList result = new AddressCacheList();

	    foreach(IAddressCache data in List) {
		if (list.Contains(data.AddressId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public AddressCacheList RemoveAll(AddressCacheList list) {
	    AddressCacheList result = new AddressCacheList();

	    foreach(IAddressCache data in List) {
		if (!list.Contains(data.AddressId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(System.Collections.IComparer comparer) {
	    this.InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new System.ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    System.Collections.IComparer comparer = Activator.CreateInstance(type) as System.Collections.IComparer;
	    if (comparer == null) {
		throw new System.ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    this.InnerList.Sort(comparer);
	}

	[Generate]
	public class Address1Sorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Address1.IsValid || !o2.Address1.IsValid) {
		    return 0;
		}
		return o1.Address1.CompareTo(o2.Address1);
	    }
	}

	[Generate]
	public class CitySorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.City.IsValid || !o2.City.IsValid) {
		    return 0;
		}
		return o1.City.CompareTo(o2.City);
	    }
	}

	[Generate]
	public class RegionSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Region.IsValid || !o2.Region.IsValid) {
		    return 0;
		}
		return o1.Region.CompareTo(o2.Region);
	    }
	}

	[Generate]
	public class PostalCodeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.PostalCode.IsValid || !o2.PostalCode.IsValid) {
		    return 0;
		}
		return o1.PostalCode.CompareTo(o2.PostalCode);
	    }
	}

	[Generate]
	public class LatitudeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Latitude.IsValid || !o2.Latitude.IsValid) {
		    return 0;
		}
		return o1.Latitude.CompareTo(o2.Latitude);
	    }
	}

	[Generate]
	public class LongitudeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Longitude.IsValid || !o2.Longitude.IsValid) {
		    return 0;
		}
		return o1.Longitude.CompareTo(o2.Longitude);
	    }
	}

	[Generate]
	public class ResultSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Result.IsValid || !o2.Result.IsValid) {
		    return 0;
		}
		return o1.Result.CompareTo(o2.Result);
	    }
	}

    }
}
