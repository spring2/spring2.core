using System;
using System.Configuration;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GeocodeProvider.
    /// </summary>
    public class GeocodeProvider {

	public static readonly String DEFAULT_PROVIDER = "Spring2.Core.Geocode.TeleAtlasProvider";

	private static IGeocodeProvider instance = null;

	private GeocodeProvider() {
	}

	public static IGeocodeProvider Instance {
	    get {
		if (instance==null) {
		    String providerClass = DEFAULT_PROVIDER;
		    if (ConfigurationSettings.AppSettings["GeocodeProvider"] != null && ConfigurationSettings.AppSettings["GeocodeProvider"] != String.Empty) {
			providerClass = ConfigurationSettings.AppSettings["GeocodeProvider"];
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
