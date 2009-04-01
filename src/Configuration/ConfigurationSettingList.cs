using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Configuration {

    /// <summary>
    /// IConfigurationSetting generic collection
    /// </summary>
    public class ConfigurationSettingList : System.Collections.CollectionBase {
	
	[Generate]
	public static readonly ConfigurationSettingList UNSET = new ConfigurationSettingList(true);
	[Generate]
	public static readonly ConfigurationSettingList DEFAULT = new ConfigurationSettingList(true);

	[Generate]
	private Boolean immutable = false;
	
	[Generate]
	private ConfigurationSettingList (Boolean immutable) {
	        this.immutable = immutable;
	}

	[Generate]
	public ConfigurationSettingList() {
	}

	// Indexer implementation.
	[Generate]
	public IConfigurationSetting this[int index] {
	    get { return (IConfigurationSetting) List[index]; }
	    set { 
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(IConfigurationSetting value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(IConfigurationSetting value) {
	    return List.Contains(value);
	}
	
	[Generate]
	public Int32 IndexOf(IConfigurationSetting value) {
	    return List.IndexOf(value);
	}
	
	[Generate]
	public void Insert(Int32 index, IConfigurationSetting value) {
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
	public void Remove(IConfigurationSetting value) {
	    if (!immutable) {
		List.Remove(value); 
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is IConfigurationSetting) {
		    Add((IConfigurationSetting)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IConfigurationSetting");
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

	/// <summary>
	/// See if the list contains an instance by identity
	/// </summary>
	[Generate]
	public Boolean Contains(IdType configurationSettingId) {
	    foreach(IConfigurationSetting o in List) {
		if (o.ConfigurationSettingId.Equals(configurationSettingId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public IConfigurationSetting this[IdType configurationSettingId] {
	    get { 
		foreach(IConfigurationSetting o in List) {
		    if (o.ConfigurationSettingId.Equals(configurationSettingId)) {
			return o;
		    }
		}

		// not found
		return null;
	    }
	}
	
	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public ConfigurationSettingList RetainAll(ConfigurationSettingList list) {
	    ConfigurationSettingList result = new ConfigurationSettingList();

	    foreach(IConfigurationSetting data in List) {
		if (list.Contains(data.ConfigurationSettingId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public ConfigurationSettingList RemoveAll(ConfigurationSettingList list) {
	    ConfigurationSettingList result = new ConfigurationSettingList();

	    foreach(IConfigurationSetting data in List) {
		if (!list.Contains(data.ConfigurationSettingId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(System.Collections.IComparer comparer) {
	    this.InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new System.ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    System.Collections.IComparer comparer = Activator.CreateInstance(type) as System.Collections.IComparer;
	    if (comparer == null) {
		throw new System.ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    this.InnerList.Sort(comparer);
	}

	[Generate]
	public class KeySorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IConfigurationSetting o1 = (IConfigurationSetting)a;
		IConfigurationSetting o2 = (IConfigurationSetting)b;

		if (o1 == null || o2 == null || !o1.Key.IsValid || !o2.Key.IsValid) {
		    return 0;
		}
		return o1.Key.CompareTo(o2.Key);
	    }
	}

	[Generate]
	public class ValueSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IConfigurationSetting o1 = (IConfigurationSetting)a;
		IConfigurationSetting o2 = (IConfigurationSetting)b;

		if (o1 == null || o2 == null || !o1.Value.IsValid || !o2.Value.IsValid) {
		    return 0;
		}
		return o1.Value.CompareTo(o2.Value);
	    }
	}

	[Generate]
	public class LastModifiedDateSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		IConfigurationSetting o1 = (IConfigurationSetting)a;
		IConfigurationSetting o2 = (IConfigurationSetting)b;

		if (o1 == null || o2 == null || !o1.LastModifiedDate.IsValid || !o2.LastModifiedDate.IsValid) {
		    return 0;
		}
		return o1.LastModifiedDate.CompareTo(o2.LastModifiedDate);
	    }
	}

        [Generate]
        public class EffectgiveDateSorter : System.Collections.IComparer {
            public Int32 Compare(Object a, Object b) {
                IConfigurationSetting o1 = (IConfigurationSetting)a;
                IConfigurationSetting o2 = (IConfigurationSetting)b;

                if (o1 == null || o2 == null || !o1.EffectiveDate.IsValid || !o2.EffectiveDate.IsValid) {
                    return 0;
                }
                return o1.EffectiveDate.CompareTo(o2.EffectiveDate);
            }
        }

    }
}
