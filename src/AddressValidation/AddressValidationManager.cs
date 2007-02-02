using System;
using Spring2.Core.Configuration;
using Spring2.Dss.AddressValidation.Test;

namespace Spring2.Dss.AddressValidation {

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class AddressValidationManager {
 
	private static IAddressValidationProvider instance = new NullAddressValidationProvider();

	public static IAddressValidationProvider Instance {
	    get {
		lock(instance) {
		    if (instance is NullAddressValidationProvider) {
			instance = CreateNewInstance();
		    }
		    return instance;
		}
	    }
	}

	private static IAddressValidationProvider CreateNewInstance() {
	    String clazz = ConfigurationProvider.Instance.Settings["AddressValidationProvider.Class"];
	    if (clazz == null || clazz.Trim().Length==0) {
		throw new AddressValidationConfigurationException("AddressValidationProvider.Class setting not set");
	    }
	    
	    Type t = Type.GetType(clazz);
	    if (t == null) {
		throw new AddressValidationConfigurationException(clazz + " could not be created");
	    }

	    Object o = System.Activator.CreateInstance(t);
	    if (o is IAddressValidationProvider) {
		return o as IAddressValidationProvider;
	    } else {
		throw new AddressValidationConfigurationException(clazz + " does not support IAddressValidationProvider");
	    }
	}

	public static void Reset() {
	    lock(instance) {
		instance = new NullAddressValidationProvider();
	    }
	}
    
    }
}
