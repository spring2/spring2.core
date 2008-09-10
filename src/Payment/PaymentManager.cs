using System;
using Spring2.Core.Configuration;
using Spring2.Core.Payment.Test;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Summary description for PaymentManager.
    /// </summary>
    public class PaymentManager {
 
	private static IPaymentProvider paymentProviderInstance = new NullPaymentProvider();
	private static ISplitPaymentProvider splitPaymentProviderInstance = new NullSplitPaymentProvider();

	public static IPaymentProvider Instance {
	    get {
		lock(paymentProviderInstance) {
		    if (paymentProviderInstance is NullPaymentProvider) {
			paymentProviderInstance = CreateNewPaymentInstance();
		    }
		    return paymentProviderInstance;
		}
	    }
	}

	public static ISplitPaymentProvider SplitInstance {
	    get {
		lock (splitPaymentProviderInstance) {
		    if (splitPaymentProviderInstance is NullPaymentProvider) {
			splitPaymentProviderInstance = CreateNewSplitPaymentInstance();
		    }
		    return splitPaymentProviderInstance;
		}
	    }
	}

	public static Object GetInstance(String key) {
		Object result = null;
		
		// "Shopping Cart" is the enum code from Uppercase Living that signifies that a split payment is necessary
	    if(key == "Shopping Cart") {
			result = SplitInstance;
		} else {
			result = Instance;
		}

	    return result;
	}

	private static IPaymentProvider CreateNewPaymentInstance() {
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

	private static ISplitPaymentProvider CreateNewSplitPaymentInstance() {
	    String clazz = ConfigurationProvider.Instance.Settings["SplitPaymentProvider.Class"];
	    if (clazz == null || clazz.Trim().Length == 0) {
		throw new PaymentConfigurationException("SplitPaymentProvider.Class setting not set");
	    }

	    Type t = Type.GetType(clazz);
	    if (t == null) {
		throw new PaymentConfigurationException(clazz + " could not be created");
	    }

	    Object o = System.Activator.CreateInstance(t);
	    if (o is IPaymentProvider) {
		return o as ISplitPaymentProvider;
	    } else {
		throw new PaymentConfigurationException(clazz + " does not support ISplitPaymentProvider");
	    }
	}

	public static void Reset() {
	    lock(paymentProviderInstance) {
		paymentProviderInstance = new NullPaymentProvider();
	    }

	    lock(splitPaymentProviderInstance) {
		splitPaymentProviderInstance = new NullSplitPaymentProvider();
	    }
	}
    
    }
}
