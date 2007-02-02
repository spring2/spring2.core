using System;
using Spring2.Core.Configuration;
using Spring2.Core.Payment.Test;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class PaymentManager {
 
	private static IPaymentProvider instance = new NullPaymentProvider();

	public static IPaymentProvider Instance {
	    get {
		lock(instance) {
		    if (instance is NullPaymentProvider) {
			instance = CreateNewInstance();
		    }
		    return instance;
		}
	    }
	}

	private static IPaymentProvider CreateNewInstance() {
	    String clazz = ConfigurationProvider.Instance.Settings["PaymentProvider.Class"];
	    if (clazz == null || clazz.Trim().Length==0) {
		throw new PaymentConfigurationException("PaymentProvider.Class setting not set");
	    }
	    
	    Type t = Type.GetType(clazz);
	    if (t == null) {
		throw new PaymentConfigurationException(clazz + " could not be created");
	    }

	    Object o = System.Activator.CreateInstance(t);
	    if (o is IPaymentProvider) {
		return o as IPaymentProvider;
	    } else {
		throw new PaymentConfigurationException(clazz + " does not support IPaymentProvider");
	    }
	}

	public static void Reset() {
	    lock(instance) {
		instance = new NullPaymentProvider();
	    }
	}
    
    }
}
