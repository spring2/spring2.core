using System;
using System.Configuration;
using System.Collections.Specialized;

namespace Spring2.Core.Configuration {
    /// <summary>
    /// Summary description for ChainedConfigurationProvider.
    /// </summary>
    public class ChainedConfigurationProvider : IConfigurationProvider {

	public ChainedConfigurationProvider() {
	}
	
	public NameValueCollection Settings {
	    get {
		NameValueCollection settings = (new SqlConfigurationProvider()).Settings;
		NameValueCollection appSettings = (new AppSettingsProvider()).Settings;

		foreach (String key in appSettings.Keys) {
		    if (settings[key] == null) {
			settings.Add(key, appSettings[key]);
		    } else {
			settings[key] = appSettings[key];
		    }
		}

		return settings;
	    }
	}

    }
}