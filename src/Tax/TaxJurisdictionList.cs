using System;
using System.Collections;
using System.Data;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Tax {
    /// <summary>
    /// IOrderItemLine generic collection
    /// </summary>
    public class TaxJurisdictionList : CollectionBase {
	[Generate]
	public static readonly TaxJurisdictionList UNSET = new TaxJurisdictionList(true);

	[Generate]
	public static readonly TaxJurisdictionList DEFAULT = new TaxJurisdictionList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TaxJurisdictionList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public TaxJurisdictionList() {
	}

	// Indexer implementation.
	[Generate]
	public TaxJurisdiction this[int index] {
	    get { return (TaxJurisdiction) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(TaxJurisdiction value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(TaxJurisdiction value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TaxJurisdiction value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TaxJurisdiction value) {
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
	public void Remove(TaxJurisdiction value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach (Object o in list) {
		if (o is TaxJurisdiction) {
		    Add((TaxJurisdiction) o);
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

	public TaxJurisdiction this[TaxJurisdictionTypeEnum type] {
	    get {
		foreach (TaxJurisdiction o in List) {
		    if (o.JurisdictionType.Equals(type)) {
			return o;
		    }
		}

		// not found
		return null;
	    }
	}
    }
}