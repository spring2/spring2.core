using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Currency.DataObject {

    /// <summary>
    /// ICurrencyExchange generic collection
    /// </summary>
    public class CurrencyExchangeList : CollectionBase {

	[Generate]
	public static readonly CurrencyExchangeList UNSET = new CurrencyExchangeList(true);
	[Generate]
	public static readonly CurrencyExchangeList DEFAULT = new CurrencyExchangeList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private CurrencyExchangeList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public CurrencyExchangeList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public ICurrencyExchange this[int index] {
	    get {
		return (ICurrencyExchange)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.CurrencyExchangeId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ICurrencyExchange value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.CurrencyExchangeId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Insert(Int32 index, ICurrencyExchange value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.CurrencyExchangeId] = value;
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
		    ICurrencyExchange value = this[index];
		    keys.Remove(value.CurrencyExchangeId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is ICurrencyExchange) {
		    Add((ICurrencyExchange)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to ICurrencyExchange");
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
	public Boolean Contains(IdType currencyExchangeId) {
	    return keys.Contains(currencyExchangeId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public ICurrencyExchange this[IdType currencyExchangeId] {
	    get {
		return keys[currencyExchangeId] as ICurrencyExchange;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public CurrencyExchangeList RetainAll(CurrencyExchangeList list) {
	    CurrencyExchangeList result = new CurrencyExchangeList();

	    foreach(ICurrencyExchange data in List) {
		if (list.Contains(data.CurrencyExchangeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public CurrencyExchangeList RemoveAll(CurrencyExchangeList list) {
	    CurrencyExchangeList result = new CurrencyExchangeList();

	    foreach(ICurrencyExchange data in List) {
		if (!list.Contains(data.CurrencyExchangeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// removes value by identity
	/// </summary>
	[Generate]
	public void Remove(IdType currencyExchangeId) {
	    if(!immutable) {
		ICurrencyExchange objectInList = this[currencyExchangeId];
		List.Remove(objectInList);
		keys.Remove(currencyExchangeId);
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
	public class CurrencyExchangeIdSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICurrencyExchange o1 = (ICurrencyExchange)a;
		ICurrencyExchange o2 = (ICurrencyExchange)b;

		if (o1 == null || o2 == null || !o1.CurrencyExchangeId.IsValid || !o2.CurrencyExchangeId.IsValid) {
		    return 0;
		}
		return o1.CurrencyExchangeId.CompareTo(o2.CurrencyExchangeId);
	    }
	}

	[Generate]
	public class CurrencyCodeSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICurrencyExchange o1 = (ICurrencyExchange)a;
		ICurrencyExchange o2 = (ICurrencyExchange)b;

		if (o1 == null || o2 == null || !o1.CurrencyCode.IsValid || !o2.CurrencyCode.IsValid) {
		    return 0;
		}
		return o1.CurrencyCode.CompareTo(o2.CurrencyCode);
	    }
	}

	[Generate]
	public class EffectiveDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICurrencyExchange o1 = (ICurrencyExchange)a;
		ICurrencyExchange o2 = (ICurrencyExchange)b;

		if (o1 == null || o2 == null || !o1.EffectiveDate.IsValid || !o2.EffectiveDate.IsValid) {
		    return 0;
		}
		return o1.EffectiveDate.CompareTo(o2.EffectiveDate);
	    }
	}

	[Generate]
	public class RateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		ICurrencyExchange o1 = (ICurrencyExchange)a;
		ICurrencyExchange o2 = (ICurrencyExchange)b;

		if (o1 == null || o2 == null || !o1.Rate.IsValid || !o2.Rate.IsValid) {
		    return 0;
		}
		return o1.Rate.CompareTo(o2.Rate);
	    }
	}

    }
}
