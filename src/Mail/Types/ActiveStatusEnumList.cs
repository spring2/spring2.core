using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// ActiveStatusEnum generic collection
    /// </summary>
    public class ActiveStatusEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly ActiveStatusEnumList UNSET = new ActiveStatusEnumList(true);
	[Generate]
	public static readonly ActiveStatusEnumList DEFAULT = new ActiveStatusEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private ActiveStatusEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public ActiveStatusEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public ActiveStatusEnum this[int index] {
	    get { return (ActiveStatusEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ActiveStatusEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(ActiveStatusEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(ActiveStatusEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, ActiveStatusEnum value) {
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
	public void Remove(ActiveStatusEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is ActiveStatusEnum) {
		    Add((ActiveStatusEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to ActiveStatusEnum");
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
