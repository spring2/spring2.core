using System;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Types {

    /// <summary>
    /// RoutingTypeEnum generic collection
    /// </summary>
    public class RoutingTypeEnumList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly RoutingTypeEnumList UNSET = new RoutingTypeEnumList(true);
	[Generate]
	public static readonly RoutingTypeEnumList DEFAULT = new RoutingTypeEnumList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private RoutingTypeEnumList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public RoutingTypeEnumList() {
	}

	// Indexer implementation.
	[Generate]
	public RoutingTypeEnum this[int index] {
	    get { return (RoutingTypeEnum) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(RoutingTypeEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(RoutingTypeEnum value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(RoutingTypeEnum value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, RoutingTypeEnum value) {
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
	public void Remove(RoutingTypeEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is RoutingTypeEnum) {
		    Add((RoutingTypeEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to RoutingTypeEnum");
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
