using System;
using System.Collections;
using System.Data;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Tax {
    /// <summary>
    /// IOrderItemLine generic collection
    /// </summary>
    public class TaxOrderLineList : CollectionBase {
	[Generate]
	public static readonly TaxOrderLineList UNSET = new TaxOrderLineList(true);

	[Generate]
	public static readonly TaxOrderLineList DEFAULT = new TaxOrderLineList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TaxOrderLineList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public TaxOrderLineList() {
	}

	// Indexer implementation.
	[Generate]
	public TaxOrderLine this[int index] {
	    get { return (TaxOrderLine) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(TaxOrderLine value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(TaxOrderLine value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TaxOrderLine value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TaxOrderLine value) {
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
	public void Remove(TaxOrderLine value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach (Object o in list) {
		if (o is TaxOrderLine) {
		    Add((TaxOrderLine) o);
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
	    foreach (TaxOrderLine o in List) {
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
	public TaxOrderLine this[IdType orderLineId] {
	    get {
		foreach (TaxOrderLine o in List) {
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
	public TaxOrderLineList RetainAll(TaxOrderLineList list) {
		TaxOrderLineList result = new TaxOrderLineList();

	    foreach (TaxOrderLine data in List) {
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
	public TaxOrderLineList RemoveAll(TaxOrderLineList list) {
		TaxOrderLineList result = new TaxOrderLineList();

	    foreach (TaxOrderLine data in List) {
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
	    	TaxOrderLine o1 = (TaxOrderLine) a;
	    	TaxOrderLine o2 = (TaxOrderLine) b;

		if (o1 == null || o2 == null || !o1.Price.IsValid || !o2.Price.IsValid) {
		    return 0;
		}
		return o1.Price.CompareTo(o2.Price);
	    }
	}

	[Generate]
	    public class DiscountAmountSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
	    	TaxOrderLine o1 = (TaxOrderLine) a;
	    	TaxOrderLine o2 = (TaxOrderLine) b;

		if (o1 == null || o2 == null || !o1.DiscountAmount.IsValid || !o2.DiscountAmount.IsValid) {
		    return 0;
		}
		return o1.DiscountAmount.CompareTo(o2.DiscountAmount);
	    }
	}

	[Generate]
	    public class ExtendedPriceSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
	    	TaxOrderLine o1 = (TaxOrderLine) a;
	    	TaxOrderLine o2 = (TaxOrderLine) b;

		if (o1 == null || o2 == null || !o1.ExtendedPrice.IsValid || !o2.ExtendedPrice.IsValid) {
		    return 0;
		}
		return o1.ExtendedPrice.CompareTo(o2.ExtendedPrice);
	    }
	}

    }
}