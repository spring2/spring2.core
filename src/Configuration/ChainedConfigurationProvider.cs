// <copyright >
//     Copyright (c) Spring2 Technologies, Inc.  All rights reserved.
// </copyright>
using System;
using System.Collections.Specialized;

namespace Spring2.Core.Configuration {

    /// <summary>
    /// Aggregated configuration provider that uses both AppSettingsProvider and SqlConfigurationProvider.  The 
    /// settings from AppSettingsProvider override settings found from SqlConfigurationProvider.
    /// </summary>
    public class ChainedConfigurationProvider : IConfigurationProvider {

    	private readonly SqlConfigurationProvider sqlProvider;
    	private readonly AppSettingsProvider appProvider;

	public ChainedConfigurationProvider() {
	    sqlProvider = new SqlConfigurationProvider();
	    appProvider = new AppSettingsProvider();
	}
	
	/// <summary>
	/// Returns the aggregated settings with those from AppSettingsProvider taking precedence.
	/// </summary>
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