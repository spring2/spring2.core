using System;
using System.Collections;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Navigation.DataObject {

    /// <summary>
    /// IMenuLinkGroup generic collection
    /// </summary>
    public class MenuLinkGroupList : CollectionBase {

	[Generate]
	public static readonly MenuLinkGroupList UNSET = new MenuLinkGroupList(true);
	[Generate]
	public static readonly MenuLinkGroupList DEFAULT = new MenuLinkGroupList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private Hashtable keys = new Hashtable();

	[Generate]
	private MenuLinkGroupList(Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public MenuLinkGroupList() {
	}

	[Generate]
	public ICollection Keys {
	    get {
		return new ArrayList(keys.Keys);
	    }
	}

	// Indexer implementation.
	[Generate]
	public IMenuLinkGroup this[int index] {
	    get {
		return (IMenuLinkGroup)List[index];
	    }
	    set {
		if (!immutable) {
		    List[index] = value;
		    keys[value.MenuLinkGroupId] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IMenuLinkGroup value) {
	    if (!immutable) {
		List.Add(value);
		keys[value.MenuLinkGroupId] = value;
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IMenuLinkGroup value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(IMenuLinkGroup value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, IMenuLinkGroup value) {
	    if (!immutable) {
		List.Insert(index, value);
		keys[value.MenuLinkGroupId] = value;
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
		    IMenuLinkGroup value = this[index];
		    keys.Remove(value.MenuLinkGroupId);
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(IMenuLinkGroup value) {
	    if (!immutable) {
		List.Remove(value);
		keys.Remove(value.MenuLinkGroupId);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IMenuLinkGroup) {
		    Add((IMenuLinkGroup)o);
		} else {
		    throw new InvalidCastException("object in list could not be cast to IMenuLinkGroup");
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
	public Boolean Contains(IdType menuLinkGroupId) {
	    return keys.Contains(menuLinkGroupId);
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IMenuLinkGroup this[IdType menuLinkGroupId] {
	    get {
		return keys[menuLinkGroupId] as IMenuLinkGroup;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public MenuLinkGroupList RetainAll(MenuLinkGroupList list) {
	    MenuLinkGroupList result = new MenuLinkGroupList();

	    foreach(IMenuLinkGroup data in List) {
		if (list.Contains(data.MenuLinkGroupId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public MenuLinkGroupList RemoveAll(MenuLinkGroupList list) {
	    MenuLinkGroupList result = new MenuLinkGroupList();

	    foreach(IMenuLinkGroup data in List) {
		if (!list.Contains(data.MenuLinkGroupId)) {
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
		IMenuLinkGroup o1 = (IMenuLinkGroup)a;
		IMenuLinkGroup o2 = (IMenuLinkGroup)b;

		if (o1 == null || o2 == null || !o1.Name.IsValid || !o2.Name.IsValid) {
		    return 0;
		}
		return o1.Name.CompareTo(o2.Name);
	    }
	}

	#region	
	public IMenuLinkGroup this[StringType name] {
	    get {
		foreach(IMenuLinkGroup group in List) {
		    if(group.Name.Equals(name)) {
			return group;
		    }
		}
		throw new IndexOutOfRangeException();
	    }
	}
	#endregion
    }
}
