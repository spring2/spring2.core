using System;


namespace Spring2.Core.Shipping.UPS {

    /// <summary>
    /// CurrencyCodeEnum generic collection
    /// </summary>
    public class UnitOfMeasurementEnumList : System.Collections.CollectionBase {
	

	public static readonly UnitOfMeasurementEnumList UNSET = new UnitOfMeasurementEnumList(true);

	public static readonly UnitOfMeasurementEnumList DEFAULT = new UnitOfMeasurementEnumList(true);

	private Boolean immutable = false;

        private UnitOfMeasurementEnumList(Boolean immutable) {
	        this.immutable = immutable;
	}

	public UnitOfMeasurementEnumList() {
	}

	public UnitOfMeasurementEnum this[int index] {
            get { return (UnitOfMeasurementEnum)List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

        public void Add(UnitOfMeasurementEnum value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

        public Boolean Contains(UnitOfMeasurementEnum value) {
	    return List.Contains(value);
	}

        public Int32 IndexOf(UnitOfMeasurementEnum value) {
	    return List.IndexOf(value);
	}

        public void Insert(Int32 index, UnitOfMeasurementEnum value) {
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

        public void Remove(UnitOfMeasurementEnum value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
                if (o is UnitOfMeasurementEnum) {
                    Add((UnitOfMeasurementEnum)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to CurrencyCodeEnum");
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
