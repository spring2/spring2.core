using System;
using System.Configuration;

namespace Spring2.Core.Configuration {
    /// <summary>
    /// Summary description for AppSettingsProvider.
    /// </summary>
    public class AppSettingsProvider : IConfigurationProvider {

	public AppSettingsProvider() {
	}
	
	public System.Collections.Specialized.NameValueCollection Settings {
	    get {
		return ConfigurationSettings.AppSettings;
	    }
	}

    }
}
