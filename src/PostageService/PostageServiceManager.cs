using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Configuration;

namespace Spring2.Core.PostageService {
    public class PostageServiceManager {
	static IPostageServiceProvider instance = new NullPostageServiceProvider();

	public static IPostageServiceProvider Instance {
	    get {
		lock (instance) {
		    if (instance is NullPostageServiceProvider) {
			instance = CreateNewInstance();
		    }
		    return instance;
		}
	    }
	}

	private static IPostageServiceProvider CreateNewInstance() {
	    return CreateNewInstance(string.Empty);
	}
	
	private static IPostageServiceProvider CreateNewInstance(string specificProvider) {
	    String clazz = null;
	    if (specificProvider == null || specificProvider == String.Empty) {
		clazz = ConfigurationProvider.Instance.Settings["PostageServiceProvider.Class"];
	    } else {
		clazz = ConfigurationProvider.Instance.Settings["PostageServiceProvider." + specificProvider + ".Class"];
	    }
	    if (clazz == null || clazz.Trim().Length == 0) {
		throw new PostageServiceException("PostageServiceProvider.Class setting not set");
	    }

	    Type t = Type.GetType(clazz);
	    if (t == null) {
		throw new PostageServiceException(clazz + " could not be created");
	    }

	    Object o = Activator.CreateInstance(t);
	    if (o is IPostageServiceProvider) {
		return o as IPostageServiceProvider;
	    } else {
		throw new PostageServiceException(clazz + " does not support IPostageServiceProvider");
	    }
	}

	public static void Reset() {
	    lock (instance) {
		instance = new NullPostageServiceProvider();
	    }
	}
    }
}
