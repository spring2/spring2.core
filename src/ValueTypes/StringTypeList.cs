using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Spring2.Core.Types {

    /// <summary>
    /// StringType generic collection
    /// </summary>
	[Serializable]
    public class StringTypeList : System.Collections.CollectionBase, ISerializable {
	
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

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        StringTypeList(SerializationInfo info, StreamingContext context) {
            immutable = (Boolean)info.GetValue("immutable", typeof(Boolean));
            int listCount = (int)info.GetValue("listCount", typeof(int));
			for(int i = 0;i < listCount; i++) {
				List.Add((StringType)info.GetValue("v" + i.ToString(), typeof(StringType)));
			}
        }

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) {
            if (this.Equals(DEFAULT)) {
                info.SetType(typeof(StringTypeList_DEFAULT));
            } else if (this.Equals(UNSET)) {
                info.SetType(typeof(StringTypeList_UNSET));
            } else {
                info.SetType(typeof(StringTypeList));
                info.AddValue("immutable", immutable);
				info.AddValue("listCount", List.Count);
				for(int i = 0;i < List.Count; i++) {
					info.AddValue("v" + i.ToString(), List[i]);
				}
            }
        }

    }

    [Serializable]
    public class StringTypeList_DEFAULT : IObjectReference {
        public object GetRealObject(StreamingContext context) {
            return StringTypeList.DEFAULT;
        }
    }

    [Serializable]
    public class StringTypeList_UNSET : IObjectReference {
        public object GetRealObject(StreamingContext context) {
            return StringTypeList.UNSET;
        }
    }
}
