using System;
using Spring2.Core.Configuration;
using Spring2.Core.AddressValidation.Test;

namespace Spring2.Core.AddressValidation {

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class AddressValidationManager {
 
	private static IAddressValidationProvider instance = new NullAddressValidationProvider();
	private static IAddressValidationProvider upsInstance = new NullAddressValidationProvider();
	private static IAddressValidationProvider uspsInstance = new NullAddressValidationProvider();

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

	public static IAddressValidationProvider UPSInstance {
	    get {
		lock (upsInstance) {
		    if (upsInstance is NullAddressValidationProvider) {
			upsInstance = CreateNewUPSInstance();
		    }
		    return upsInstance;
		}
	    }
	}

	public static IAddressValidationProvider USPSInstance {
	    get {
		lock (uspsInstance) {
		    if (uspsInstance is NullAddressValidationProvider) {
			uspsInstance = CreateNewUSPSInstance();
		    }
		    return uspsInstance;
		}
	    }
	}

	private static IAddressValidationProvider CreateNewInstance() {
	    return CreateNewInstance(String.Empty);
	}

	private static IAddressValidationProvider CreateNewInstance(String specificProvider) {
	    String clazz = null;
	    if (specificProvider == null || specificProvider == String.Empty) {
		clazz = ConfigurationProvider.Instance.Settings["AddressValidationProvider.Class"];
	    } else {
		clazz = ConfigurationProvider.Instance.Settings["AddressValidationProvider." + specificProvider + ".Class"];
	    }
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

	private static IAddressValidationProvider CreateNewUPSInstance() {
	    return CreateNewInstance("UPS");
	}

	private static IAddressValidationProvider CreateNewUSPSInstance() {
	    return CreateNewInstance("USPS");
	}

	public static void Reset() {
	    lock(instance) {
		instance = new NullAddressValidationProvider();
	    }
	}
    
    }
}
