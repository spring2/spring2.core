using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode.DataObject {

    /// <summary>
    /// IAddressCache generic collection
    /// </summary>
    public class AddressCacheList : CollectionBase {

	[Generate]
	public static readonly AddressCacheList UNSET = new AddressCacheList(true);
	[Generate]
	public static readonly AddressCacheList DEFAULT = new AddressCacheList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private AddressCacheList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public AddressCacheList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IAddressCache this[int index] {
	    get {
		return (IAddressCache)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.AddressId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IAddressCache value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.AddressId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, IAddressCache value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.AddressId] = value;
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
		    IAddressCache value = this[index];
		    keys.Remove(value.AddressId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IAddressCache) {
		    Add((IAddressCache)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IAddressCache");
		}
	    }
	}

	[Generate]
	public Boolean IsDefault {
	    get {
		return ReferenceEquals(this, DEFAULT);
	    }
	}

	[Generate]
	public Boolean IsUnset {
	    get {
		return ReferenceEquals(this, UNSET);
	    }
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
	    return keys.Contains(addressId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IAddressCache this[IdType addressId] {
	    get {
		return keys[addressId] as IAddressCache;
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
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType addressId) {
	    if(!immutable) {
		IAddressCache objectInList = this[addressId];
		List.Remove(objectInList);
		keys.Remove(addressId);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(IComparer comparer) {
	    InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    IComparer comparer = Activator.CreateInstance(type) as IComparer;
	    if (comparer == null) {
		throw new ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    InnerList.Sort(comparer);
	}

	[Generate]
	public class AddressIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.AddressId.IsValid || !o2.AddressId.IsValid) {
		    return 0;
		}
		return o1.AddressId.CompareTo(o2.AddressId);
	    }
	}

	[Generate]
	public class Address1Sorter : IComparer {
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
	public class CitySorter : IComparer {
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
	public class RegionSorter : IComparer {
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
	public class PostalCodeSorter : IComparer {
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
	public class LatitudeSorter : IComparer {
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
	public class LongitudeSorter : IComparer {
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
	public class ResultSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.Result.IsValid || !o2.Result.IsValid) {
		    return 0;
		}
		return o1.Result.CompareTo(o2.Result);
	    }
	}

	[Generate]
	public class StdAddress1Sorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.StdAddress1.IsValid || !o2.StdAddress1.IsValid) {
		    return 0;
		}
		return o1.StdAddress1.CompareTo(o2.StdAddress1);
	    }
	}

	[Generate]
	public class StdCitySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.StdCity.IsValid || !o2.StdCity.IsValid) {
		    return 0;
		}
		return o1.StdCity.CompareTo(o2.StdCity);
	    }
	}

	[Generate]
	public class StdRegionSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.StdRegion.IsValid || !o2.StdRegion.IsValid) {
		    return 0;
		}
		return o1.StdRegion.CompareTo(o2.StdRegion);
	    }
	}

	[Generate]
	public class StdPostalCodeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.StdPostalCode.IsValid || !o2.StdPostalCode.IsValid) {
		    return 0;
		}
		return o1.StdPostalCode.CompareTo(o2.StdPostalCode);
	    }
	}

	[Generate]
	public class StdPlus4Sorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.StdPlus4.IsValid || !o2.StdPlus4.IsValid) {
		    return 0;
		}
		return o1.StdPlus4.CompareTo(o2.StdPlus4);
	    }
	}

	[Generate]
	public class MatAddress1Sorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.MatAddress1.IsValid || !o2.MatAddress1.IsValid) {
		    return 0;
		}
		return o1.MatAddress1.CompareTo(o2.MatAddress1);
	    }
	}

	[Generate]
	public class MatCitySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.MatCity.IsValid || !o2.MatCity.IsValid) {
		    return 0;
		}
		return o1.MatCity.CompareTo(o2.MatCity);
	    }
	}

	[Generate]
	public class MatRegionSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.MatRegion.IsValid || !o2.MatRegion.IsValid) {
		    return 0;
		}
		return o1.MatRegion.CompareTo(o2.MatRegion);
	    }
	}

	[Generate]
	public class MatPostalCodeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.MatPostalCode.IsValid || !o2.MatPostalCode.IsValid) {
		    return 0;
		}
		return o1.MatPostalCode.CompareTo(o2.MatPostalCode);
	    }
	}

	[Generate]
	public class MatchTypeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IAddressCache o1 = (IAddressCache)a;
		IAddressCache o2 = (IAddressCache)b;

		if (o1 == null || o2 == null || !o1.MatchType.IsValid || !o2.MatchType.IsValid) {
		    return 0;
		}
		return o1.MatchType.CompareTo(o2.MatchType);
	    }
	}

    }
}
