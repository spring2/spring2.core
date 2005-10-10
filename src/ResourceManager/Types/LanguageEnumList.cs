using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.ResourceManager.Types {

    /// <summary>
    /// LanguageEnum generic collection
    /// </summary>
    public class LanguageEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly LanguageEnumList UNSET = new LanguageEnumList(true);
	[Generate]
	public static readonly LanguageEnumList DEFAULT = new LanguageEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private LanguageEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public LanguageEnumList() {
	}

	// Indexer implementation.
	[Generate]
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

	[Generate]
	public void Add(LanguageEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(LanguageEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(LanguageEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, LanguageEnum value) {
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
	public void Remove(LanguageEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is LanguageEnum) {
		    Add((LanguageEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to LanguageEnum");
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
