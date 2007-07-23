using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Navigation.DataObject {

    /// <summary>
    /// IMenuLink generic collection
    /// </summary>
    public class MenuLinkList : CollectionBase {

	[Generate]
	public static readonly MenuLinkList UNSET = new MenuLinkList(true);
	[Generate]
	public static readonly MenuLinkList DEFAULT = new MenuLinkList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private MenuLinkList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public MenuLinkList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IMenuLink this[int index] {
	    get {
		return (IMenuLink)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.MenuLinkId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMenuLink value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.MenuLinkId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMenuLink value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(IMenuLink value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, IMenuLink value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.MenuLinkId] = value;
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
		    IMenuLink value = this[index];
		    keys.Remove(value.MenuLinkId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(IMenuLink value) {
	    if (!immutable) {
		List.Remove(value);
		keys.Remove(value.MenuLinkId);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IMenuLink) {
		    Add((IMenuLink)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IMenuLink");
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
	public Boolean Contains(IdType menuLinkId) {
	    return keys.Contains(menuLinkId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMenuLink this[IdType menuLinkId] {
	    get {
		return keys[menuLinkId] as IMenuLink;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public MenuLinkList RetainAll(MenuLinkList list) {
	    MenuLinkList result = new MenuLinkList();

	    foreach(IMenuLink data in List) {
		if (list.Contains(data.MenuLinkId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public MenuLinkList RemoveAll(MenuLinkList list) {
	    MenuLinkList result = new MenuLinkList();

	    foreach(IMenuLink data in List) {
		if (!list.Contains(data.MenuLinkId)) {
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
	public class NameSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMenuLink o1 = (IMenuLink)a;
		IMenuLink o2 = (IMenuLink)b;

		if (o1 == null || o2 == null || !o1.Name.IsValid || !o2.Name.IsValid) {
		    return 0;
		}
		return o1.Name.CompareTo(o2.Name);
	    }
	}

	[Generate]
	public class TargetSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMenuLink o1 = (IMenuLink)a;
		IMenuLink o2 = (IMenuLink)b;

		if (o1 == null || o2 == null || !o1.Target.IsValid || !o2.Target.IsValid) {
		    return 0;
		}
		return o1.Target.CompareTo(o2.Target);
	    }
	}

	[Generate]
	public class EffectiveDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMenuLink o1 = (IMenuLink)a;
		IMenuLink o2 = (IMenuLink)b;

		if (o1 == null || o2 == null || !o1.EffectiveDate.IsValid || !o2.EffectiveDate.IsValid) {
		    return 0;
		}
		return o1.EffectiveDate.CompareTo(o2.EffectiveDate);
	    }
	}

	[Generate]
	public class ExpirationDateSorter : IComparer {
	    public Int32 Compare(Object a, Object b) {
		IMenuLink o1 = (IMenuLink)a;
		IMenuLink o2 = (IMenuLink)b;

		if (o1 == null || o2 == null || !o1.ExpirationDate.IsValid || !o2.ExpirationDate.IsValid) {
		    return 0;
		}
		return o1.ExpirationDate.CompareTo(o2.ExpirationDate);
	    }
	}

    }
}
