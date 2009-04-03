using System;
using System.Collections;
using System.Data;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Tax {
    /// <summary>
    /// IOrderItemLine generic collection
    /// </summary>
    public class TaxResultLineList : CollectionBase {
	[Generate]
	public static readonly TaxResultLineList UNSET = new TaxResultLineList(true);

	[Generate]
	public static readonly TaxResultLineList DEFAULT = new TaxResultLineList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TaxResultLineList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public TaxResultLineList() {
	}

	// Indexer implementation.
	[Generate]
	public TaxResultLine this[int index] {
	    get { return (TaxResultLine) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(TaxResultLine value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(TaxResultLine value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TaxResultLine value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TaxResultLine value) {
	    if (!immutable) {
		List.Insert(index, value);
	    } else {
		throw new ReadOnlyException();
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
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(TaxResultLine value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach (Object o in list) {
		if (o is TaxResultLine) {
		    Add((TaxResultLine) o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IOrderItemLine");
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
	    get { return !(IsDefault || IsUnset); }
	}

	/// <summary>
	/// See if the list contains an instance by identity
	/// </summary>
	[Generate]
	public Boolean Contains(IdType orderLineId) {
	    foreach (TaxResultLine o in List) {
		if (o.OrderLineId.Equals(orderLineId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public TaxResultLine this[IdType orderLineId] {
	    get {
		foreach (TaxResultLine o in List) {
		    if (o.OrderLineId.Equals(orderLineId)) {
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
	public TaxResultLineList RetainAll(TaxResultLineList list) {
		TaxResultLineList result = new TaxResultLineList();

	    foreach (TaxResultLine data in List) {
		if (list.Contains(data.OrderLineId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public TaxResultLineList RemoveAll(TaxResultLineList list) {
		TaxResultLineList result = new TaxResultLineList();

	    foreach (TaxResultLine data in List) {
		if (!list.Contains(data.OrderLineId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(IComparer comparer) {
	    this.InnerList.Sort(comparer);
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

	    this.InnerList.Sort(comparer);
	}

	[Generate]
	    public class PriceSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
	    	TaxResultLine o1 = (TaxResultLine) a;
	    	TaxResultLine o2 = (TaxResultLine) b;

		if (o1 == null || o2 == null || !o1.Price.IsValid || !o2.Price.IsValid) {
		    return 0;
		}
		return o1.Price.CompareTo(o2.Price);
	    }
	}

	[Generate]
	    public class DiscountAmountSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
	    	TaxResultLine o1 = (TaxResultLine) a;
	    	TaxResultLine o2 = (TaxResultLine) b;

		if (o1 == null || o2 == null || !o1.DiscountAmount.IsValid || !o2.DiscountAmount.IsValid) {
		    return 0;
		}
		return o1.DiscountAmount.CompareTo(o2.DiscountAmount);
	    }
	}

	[Generate]
	    public class ExtendedPriceSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
	    	TaxResultLine o1 = (TaxResultLine) a;
	    	TaxResultLine o2 = (TaxResultLine) b;

		if (o1 == null || o2 == null || !o1.ExtendedPrice.IsValid || !o2.ExtendedPrice.IsValid) {
		    return 0;
		}
		return o1.ExtendedPrice.CompareTo(o2.ExtendedPrice);
	    }
	}

    }
}