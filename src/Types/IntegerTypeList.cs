using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// IntegerType generic collection
    /// </summary>
    public class IntegerTypeList : System.Collections.CollectionBase {
	
	public static readonly IntegerTypeList UNSET = new IntegerTypeList(true);
	public static readonly IntegerTypeList DEFAULT = new IntegerTypeList(true);

	private Boolean immutable = false;
	
	private IntegerTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public IntegerTypeList() {
	}

	// Indexer implementation.
	public IntegerType this[int index] {
	    get { return (IntegerType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(IntegerType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(IntegerType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(IntegerType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, IntegerType value) {
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

	public void Remove(IntegerType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IntegerType) {
		    Add((IntegerType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IntegerType");
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
