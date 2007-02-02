using System;

using Spring2.Core.Types;

using Spring2.Core.AddressValidation;

namespace Spring2.Core.AddressValidation {

    public class AddressList : System.Collections.CollectionBase {
	
	public static readonly AddressList UNSET = new AddressList(true);
	
	public static readonly AddressList DEFAULT = new AddressList(true);

	private Boolean immutable = false;
	
	private AddressList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public AddressList() {
	}

	public void Add(AddressData value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(AddressData value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(AddressData value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, AddressData value) {
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

	public void Remove(AddressData value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is AddressData) {
		    Add((AddressData)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to AddressData");
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

	public AddressData this[int index] {
	    get { return (AddressData) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public AddressList PotentialAddressMatches {
	    get {
		AddressList list = new AddressList();
		foreach (AddressData data in this) {
		    if (!data.BlockRange.IsTrue) {
			list.Add(data);
		    }
		}
		return list;
	    }
	}

	public AddressList BlockRangeAddresses {
	    get {
		AddressList list = new AddressList();
		foreach (AddressData data in this) {
		    if (data.BlockRange.IsTrue) {
			list.Add(data);
		    }
		}
		return list;
	    }
	}
    }
}
