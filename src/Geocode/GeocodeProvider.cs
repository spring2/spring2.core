using System;
using Spring2.Core;
using Spring2.Core.Configuration;


namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GeocodeProvider.
    /// </summary>
    public class GeocodeProvider {

	public static readonly string DEFAULT_PROVIDER = "Spring2.Core.Geocode.TeleAtlasProvider";

	private static IGeocodeProvider instance = null;

	private GeocodeProvider() {
	}

	public static IGeocodeProvider Instance {
	    get {
		if (instance == null) {
		    string providerClass = DEFAULT_PROVIDER;
		    
		    if (ConfigurationProvider.Instance.Settings["GeocodeProvider"] != null && 
			ConfigurationProvider.Instance.Settings["GeocodeProvider"] != String.Empty) {
			providerClass = ConfigurationProvider.Instance.Settings["GeocodeProvider"];
		    }

		    Type clazz = Type.GetType(providerClass);
		    Object o = System.Activator.CreateInstance(clazz);
		    if (o is IGeocodeProvider) {
			instance = o as IGeocodeProvider;
		    } else {
			throw new Exception(providerClass + " does not support IGeocodeProvider");
		    }
		}
		return instance;
	    }	
	}

	public static void SetProvider(IGeocodeProvider provider) {
	    instance = provider;
	}

	public static IGeocodeProvider CreateProvider(String className) {
	    Type clazz = Type.GetType(className);
	    Object o = System.Activator.CreateInstance(clazz);
	    if (o is IGeocodeProvider) {
		return o as IGeocodeProvider;
	    } else {
		throw new Exception(className + " does not support IGeocodeProvider");
	    }
	}
    }
}
