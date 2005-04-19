using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// StringType generic collection
    /// </summary>
    public class StringTypeList : System.Collections.CollectionBase {
	
	public static readonly StringTypeList UNSET = new StringTypeList(true);
	public static readonly StringTypeList DEFAULT = new StringTypeList(true);

	private Boolean immutable = false;
	
	private StringTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public StringTypeList() {
	}

	// Indexer implementation.
	public StringType this[int index] {
	    get { return (StringType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(StringType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(StringType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(StringType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, StringType value) {
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

	public void Remove(StringType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is StringType) {
		    Add((StringType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to StringType");
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
