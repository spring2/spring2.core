using System;
using System.Configuration;
using System.Collections.Specialized;

namespace Spring2.Core.Configuration {
    /// <summary>
    /// Summary description for ChainedConfigurationProvider.
    /// </summary>
    public class ChainedConfigurationProvider : IConfigurationProvider {

    	private SqlConfigurationProvider sqlProvider;
    	private AppSettingsProvider appProvider;

	public ChainedConfigurationProvider() {
	    sqlProvider = new SqlConfigurationProvider();
	    appProvider = new AppSettingsProvider();
	}
	
	public NameValueCollection Settings {
	    get {
		NameValueCollection settings = sqlProvider.Settings;
		NameValueCollection appSettings = appProvider.Settings;

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