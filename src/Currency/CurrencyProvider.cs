using System;
using Spring2.Core;
using Spring2.Core.Configuration;

namespace Spring2.Core.Currency {
    public class CurrencyProvider {

	public static readonly string DEFAULT_PROVIDER = "Spring2.Core.Currency.WebServiceXProvider";

	private static ICurrencyProvider instance = null;

	private CurrencyProvider() {
	}

	public static ICurrencyProvider Instance {
	    get {
		if (instance == null) {
		    string providerClass = DEFAULT_PROVIDER;

		    if (ConfigurationProvider.Instance.Settings["CurrencyProvider"] != null &&
			ConfigurationProvider.Instance.Settings["CurrencyProvider"] != String.Empty) {
			providerClass = ConfigurationProvider.Instance.Settings["CurrencyProvider"];
		    }

		    Type clazz = Type.GetType(providerClass);
		    Object o = System.Activator.CreateInstance(clazz);
		    if (o is ICurrencyProvider) {
			instance = o as ICurrencyProvider;
		    } else {
			throw new Exception(providerClass + " does not support ICurrencyProvider");
		    }
		}
		return instance;
	    }
	}

	public static void SetProvider(ICurrencyProvider provider) {
	    instance = provider;
	}

	public static ICurrencyProvider CreateProvider(String className) {
	    Type clazz = Type.GetType(className);
	    Object o = System.Activator.CreateInstance(clazz);
	    if (o is ICurrencyProvider) {
		return o as ICurrencyProvider;
	    } else {
		throw new Exception(className + " does not support ICurrencyProvider");
	    }
	}
    }
}
