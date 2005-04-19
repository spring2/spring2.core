using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// IdType generic collection
    /// </summary>
    public class IdTypeList : System.Collections.CollectionBase {
	
	public static readonly IdTypeList UNSET = new IdTypeList(true);
	public static readonly IdTypeList DEFAULT = new IdTypeList(true);

	private Boolean immutable = false;
	
	private IdTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public IdTypeList() {
	}

	// Indexer implementation.
	public IdType this[int index] {
	    get { return (IdType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(IdType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(IdType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(IdType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, IdType value) {
	    if (!immutable) {
		List.Insert(index, value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

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

	public void Remove(IdType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IdType) {
		    Add((IdType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IdType");
		}
	    }
	}
	
	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}
	
	public Boolean IsValid {
	    get {
		return !(IsDefault || IsUnset);
	    }
	}

    }
}
