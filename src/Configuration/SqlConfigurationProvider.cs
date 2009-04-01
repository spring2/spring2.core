using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using Spring2.Core.DAO;
using Spring2.Core.Types;

namespace Spring2.Core.Configuration {

    /// <summary>
    /// Configuration provider that uses a sql table as a data store.
    /// </summary>
    public class SqlConfigurationProvider : IConfigurationProvider {

	/// <summary>
	/// creates a logger that uses the OutputDebugPrint API - use DebugView to see what is being written
	/// </summary>
	protected static DefaultTraceListener debug = new DefaultTraceListener();

	private NameValueCollection settings = null;
    	private Int32 cacheTimeout = 0;
    	private DateTime lastRefresh = DateTime.MinValue;
    	private Int32 refreshes = 0;

	public SqlConfigurationProvider() {
	    // get the timeout for the cache, if specified
	    if (ConfigurationSettings.AppSettings["SqlConfigurationProvider.CacheTimeout"] != null && ConfigurationSettings.AppSettings["SqlConfigurationProvider.CacheTimeout"] != String.Empty) {
		cacheTimeout = Int32.Parse(ConfigurationSettings.AppSettings["SqlConfigurationProvider.CacheTimeout"]);
	    }
	    PrepopulateCache();
	}

	public SqlConfigurationProvider(Int32 cacheTimeout) {
	    this.cacheTimeout = cacheTimeout;
	    PrepopulateCache();
	}

	private void PrepopulateCache() {
	    // prepopulate the settings, if it is to be cached
	    debug.WriteLine("SqlConfigurationProvider configured with cacheTimeout of " + cacheTimeout);
	    if (cacheTimeout > 0) {
		settings = Refresh();
		lastRefresh = DateTime.Now;
	    }
	}

    	public Int32 CacheTimeout {
    	    get {
    	    	return this.cacheTimeout;
    	    }
    	}

	public Int32 Refreshes {
	    get {
		return this.refreshes;
	    }
	}

	public DateTime LastRefresh {
	    get {
		return this.lastRefresh;
	    }
	}

	public NameValueCollection Settings {
	    get {
		if (cacheTimeout > 0) {
		    if (DateTime.Now > lastRefresh.AddSeconds(cacheTimeout)) {
		    	lock (settings) {
	    		    settings = Refresh();
			    lastRefresh = DateTime.Now;
		    	}
		    }
		    return settings;
		} else {
		    return Refresh();
		}
	    }
	}

	private NameValueCollection Refresh() {
	    NameValueCollection settings = new NameValueCollection();
	    OrderByClause sort = new OrderByClause("[Key], EffectiveDate desc");
	    ConfigurationSettingList list = ConfigurationSettingDAO.DAO.GetList(sort);
	    foreach (IConfigurationSetting setting in list) {
		if (setting.EffectiveDate > DateTimeType.Now) {
		    continue;
		}
		if (settings[setting.Key.ToString()] == null) {
		    settings.Add(setting.Key.ToString(), setting.Value.ToString());
		}
	    }

	    debug.WriteLine("SqlConfigurationProvider.Refresh()");
	    refreshes++;
	    return settings;
	}
    }
}
