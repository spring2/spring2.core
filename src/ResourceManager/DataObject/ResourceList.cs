using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.ResourceManager.DataObject {

    /// <summary>
    /// IResource generic collection
    /// </summary>
    public class ResourceList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly ResourceList UNSET = new ResourceList(true);
	[Generate]
	public static readonly ResourceList DEFAULT = new ResourceList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private ResourceList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public ResourceList() {
	}

	// Indexer implementation.
	[Generate]
	public IResource this[int index] {
	    get { return (IResource) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IResource value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IResource value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IResource value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IResource value) {
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
	public void Remove(IResource value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IResource) {
		    Add((IResource)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IResource");
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
	public Boolean Contains(IdType resourceId) {
	    foreach(IResource o in List) {
		if (o.ResourceId.Equals(resourceId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IResource this[IdType resourceId] {
	    get { 
		foreach(IResource o in List) {
		    if (o.ResourceId.Equals(resourceId)) {
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
	public ResourceList RetainAll(ResourceList list) {
	    ResourceList result = new ResourceList();

	    foreach(IResource data in List) {
		if (list.Contains(data.ResourceId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public ResourceList RemoveAll(ResourceList list) {
	    ResourceList result = new ResourceList();

	    foreach(IResource data in List) {
		if (!list.Contains(data.ResourceId)) {
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
	public class ContextSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IResource o1 = (IResource)a;
		IResource o2 = (IResource)b;

		if (o1 == null || o2 == null || !o1.Context.IsValid || !o2.Context.IsValid) {
		    return 0;
		}
		return o1.Context.CompareTo(o2.Context);
	    }
	}

	[Generate]
	public class FieldSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IResource o1 = (IResource)a;
		IResource o2 = (IResource)b;

		if (o1 == null || o2 == null || !o1.Field.IsValid || !o2.Field.IsValid) {
		    return 0;
		}
		return o1.Field.CompareTo(o2.Field);
	    }
	}

    }
}
