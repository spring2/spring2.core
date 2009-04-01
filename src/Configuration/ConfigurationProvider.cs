using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;

namespace Spring2.Core.Configuration {
    /// <summary>
    /// Summary description for ConfigurationProvider.
    /// </summary>
    public class ConfigurationProvider {

	public static readonly String DEFAULT_PROVIDER = "Spring2.Core.Configuration.AppSettingsProvider";

	private static IConfigurationProvider instance = null;

	private ConfigurationProvider() {
	}

	public static IConfigurationProvider Instance {
	    get {
		if (instance==null) {
		    String providerClass = DEFAULT_PROVIDER;
		    if (ConfigurationSettings.AppSettings["ConfigurationProvider"] != null && ConfigurationSettings.AppSettings["ConfigurationProvider"] != String.Empty) {
			providerClass = ConfigurationSettings.AppSettings["ConfigurationProvider"];
		    }
	    
		    Type clazz = Type.GetType(providerClass);
		    Object o = System.Activator.CreateInstance(clazz);
		    if (o is IConfigurationProvider) {
			instance = o as IConfigurationProvider;
			// force load of all settings, if provider caches them
		    	NameValueCollection settings = instance.Settings;
		    } else {
			throw new Exception(providerClass + " does not support IConfigurationProvider");
		    }
		}

		return instance;
	    }	
	}

	public static void SetProvider(IConfigurationProvider provider) {
	    instance = provider;
	}

	public static IConfigurationProvider CreateProvider(String className) {
	    Type clazz = Type.GetType(className);
	    Object o = System.Activator.CreateInstance(clazz);
	    if (o is IConfigurationProvider) {
		return o as IConfigurationProvider;
	    } else {
		throw new Exception(className + " does not support IConfigurationProvider");
	    }
	}
    
    }
}
