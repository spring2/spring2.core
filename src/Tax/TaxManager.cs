using System;
using System.Collections;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Tax {
    /// <summary>
    /// Summary description for TaxManager.
    /// </summary>
    public class TaxManager {
	
	private static readonly string providerKey = "TaxProvider.Class";
	private static Hashtable providers = new Hashtable();

	public static ITaxProvider GetProvider(StringType profileKey) {
	    ITaxProvider provider = providers[profileKey] as ITaxProvider;
	    if (provider == null) {
		lock(providers) {
		    provider = providers[profileKey] as ITaxProvider;
		    if (provider == null) {
			provider = CreateNewInstance(profileKey);
		    }
		    return provider;
		}
	    } else {
		return provider;
	    }
	}

	private static ITaxProvider CreateNewInstance(StringType profileKey) {
	    string keyName = string.Format("{0}.{1}", providerKey, profileKey);
	    String clazz = ConfigurationProvider.Instance.Settings[keyName];
	    if (clazz == null || clazz.Trim().Length==0) {
		clazz = ConfigurationProvider.Instance.Settings["TaxProvider.Class"];
		if (clazz == null || clazz.Trim().Length==0) {
		    throw new TaxConfigurationException("TaxProvider.Class setting not set");
		}
	    }
	    
	    Type t = Type.GetType(clazz);
	    if (t == null) {
		throw new TaxConfigurationException(clazz + " could not be created");
	    }

	    Object o;
	    if (profileKey.IsValid) {
		object[] args = new object[1]{profileKey};
		o = System.Activator.CreateInstance(t, args);
	    } else {
		o = System.Activator.CreateInstance(t);
	    }
	    
	    if (o is ITaxProvider) {
		return o as ITaxProvider;
	    } else {
		throw new TaxConfigurationException(clazz + " does not support ITaxProvider");
	    }
	}

	public static void Reset() {
	    lock(providers) {
		providers = new Hashtable();
	    }
	}
    }
}
