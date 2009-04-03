using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// DecimalType generic collection
    /// </summary>
    public class DecimalTypeList : System.Collections.CollectionBase {
	
	public static readonly DecimalTypeList UNSET = new DecimalTypeList(true);
	public static readonly DecimalTypeList DEFAULT = new DecimalTypeList(true);

	private Boolean immutable = false;
	
	private DecimalTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public DecimalTypeList() {
	}

	// Indexer implementation.
	public DecimalType this[int index] {
	    get { return (DecimalType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(DecimalType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(DecimalType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(DecimalType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, DecimalType value) {
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

	public void Remove(DecimalType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is DecimalType) {
		    Add((DecimalType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to DecimalType");
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
