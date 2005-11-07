using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Spring2.Core.Configuration {

    /// <summary>
    /// ConfigurationProvider that is manually populated.  This provider does not have a store.
    /// </summary>
    public class SimpleConfigurationProvider : IConfigurationProvider {

	private NameValueCollection settings = new NameValueCollection();

	public SimpleConfigurationProvider() {
	}
	
	public NameValueCollection Settings {
	    get {
		// TODO:  this should probably not return a modifiable version
		return settings;
	    }
	}

    }
}
