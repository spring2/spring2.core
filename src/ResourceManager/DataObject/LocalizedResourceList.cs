using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.ResourceManager.DataObject {

    /// <summary>
    /// ILocalizedResource generic collection
    /// </summary>
    public class LocalizedResourceList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly LocalizedResourceList UNSET = new LocalizedResourceList(true);
	[Generate]
	public static readonly LocalizedResourceList DEFAULT = new LocalizedResourceList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private LocalizedResourceList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public LocalizedResourceList() {
	}

	// Indexer implementation.
	[Generate]
	public ILocalizedResource this[int index] {
	    get { return (ILocalizedResource) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ILocalizedResource value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(ILocalizedResource value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(ILocalizedResource value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, ILocalizedResource value) {
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
	public void Remove(ILocalizedResource value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is ILocalizedResource) {
		    Add((ILocalizedResource)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to ILocalizedResource");
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
	public Boolean Contains(IdType localizedResourceId) {
	    foreach(ILocalizedResource o in List) {
		if (o.LocalizedResourceId.Equals(localizedResourceId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public ILocalizedResource this[IdType localizedResourceId] {
	    get { 
		foreach(ILocalizedResource o in List) {
		    if (o.LocalizedResourceId.Equals(localizedResourceId)) {
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
	public LocalizedResourceList RetainAll(LocalizedResourceList list) {
	    LocalizedResourceList result = new LocalizedResourceList();

	    foreach(ILocalizedResource data in List) {
		if (list.Contains(data.LocalizedResourceId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public LocalizedResourceList RemoveAll(LocalizedResourceList list) {
	    LocalizedResourceList result = new LocalizedResourceList();

	    foreach(ILocalizedResource data in List) {
		if (!list.Contains(data.LocalizedResourceId)) {
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
	public class ContentSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		ILocalizedResource o1 = (ILocalizedResource)a;
		ILocalizedResource o2 = (ILocalizedResource)b;

		if (o1 == null || o2 == null || !o1.Content.IsValid || !o2.Content.IsValid) {
		    return 0;
		}
		return o1.Content.CompareTo(o2.Content);
	    }
	}

    }
}
