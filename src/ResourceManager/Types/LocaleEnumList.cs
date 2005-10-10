using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.ResourceManager.Types {

    /// <summary>
    /// LocaleEnum generic collection
    /// </summary>
    public class LocaleEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly LocaleEnumList UNSET = new LocaleEnumList(true);
	[Generate]
	public static readonly LocaleEnumList DEFAULT = new LocaleEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private LocaleEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public LocaleEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public LocaleEnum this[int index] {
	    get { return (LocaleEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(LocaleEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(LocaleEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(LocaleEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, LocaleEnum value) {
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
	public void Remove(LocaleEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is LocaleEnum) {
		    Add((LocaleEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to LocaleEnum");
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


    }
}
