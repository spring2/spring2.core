using System;

namespace Spring2.Core.Types {

    /// <summary>
    /// EnumDataType generic collection
    /// </summary>
    public class EnumDataTypeList : System.Collections.CollectionBase {
	
	public static readonly EnumDataTypeList UNSET = new EnumDataTypeList(true);
	public static readonly EnumDataTypeList DEFAULT = new EnumDataTypeList(true);

	private Boolean immutable = false;
	
	private EnumDataTypeList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public EnumDataTypeList() {
	}

	// Indexer implementation.
	public EnumDataType this[int index] {
	    get { return (EnumDataType) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(EnumDataType value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(EnumDataType value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(EnumDataType value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, EnumDataType value) {
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

	public void Remove(EnumDataType value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is EnumDataType) {
		    Add((EnumDataType)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to EnumDataType");
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
