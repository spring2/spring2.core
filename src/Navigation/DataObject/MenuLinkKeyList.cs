using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Navigation.DataObject {

    /// <summary>
    /// IMenuLinkKey generic collection
    /// </summary>
    public class MenuLinkKeyList : CollectionBase {

	[Generate]
	public static readonly MenuLinkKeyList UNSET = new MenuLinkKeyList(true);
	[Generate]
	public static readonly MenuLinkKeyList DEFAULT = new MenuLinkKeyList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private MenuLinkKeyList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public MenuLinkKeyList() {
	}

	// Indexer implementation.
	[Generate]
	public IMenuLinkKey this[int index] {
	    get {
		return (IMenuLinkKey)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMenuLinkKey value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMenuLinkKey value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(IMenuLinkKey value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, IMenuLinkKey value) {
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
	public void Remove(IMenuLinkKey value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IMenuLinkKey) {
		    Add((IMenuLinkKey)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IMenuLinkKey");
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
	public class KeySorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMenuLinkKey o1 = (IMenuLinkKey)a;
		IMenuLinkKey o2 = (IMenuLinkKey)b;

		if (o1 == null || o2 == null || !o1.Key.IsValid || !o2.Key.IsValid) {
		    return 0;
		}
		return o1.Key.CompareTo(o2.Key);
	    }
	}

    }
}
