using System;


namespace Spring2.Core.Types {

    /// <summary>
    /// LanguageEnum generic collection
    /// </summary>
    public class LanguageEnumList : System.Collections.CollectionBase {
	
	
	public static readonly LanguageEnumList UNSET = new LanguageEnumList(true);
	
	public static readonly LanguageEnumList DEFAULT = new LanguageEnumList(true);

	
	private Boolean immutable = false;
	
	
	private LanguageEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	
	public LanguageEnumList() {
	}

	// Indexer implementation.
	
	public LanguageEnum this[int index] {
	    get { return (LanguageEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	
	public void Add(LanguageEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	
	public Boolean Contains(LanguageEnum value) {
	    return List.Contains(value);
	}
	
	
	public Int32 IndexOf(LanguageEnum value) {
	    return List.IndexOf(value);
	}
	
	
	public void Insert(Int32 index, LanguageEnum value) {
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

	
	public void Remove(LanguageEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is LanguageEnum) {
		    Add((LanguageEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to LanguageEnum");
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
