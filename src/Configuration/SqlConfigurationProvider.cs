using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Spring2.Core.Configuration {

    /// <summary>
    /// Configuration provider that uses a sql table as a data store.
    /// </summary>
    public class SqlConfigurationProvider : IConfigurationProvider {

	//private NameValueCollection settings = null;

	public SqlConfigurationProvider() {
	}
	
	public NameValueCollection Settings {
	    get {
		//if (settings == null) {
		    NameValueCollection settings = new NameValueCollection();
		    ConfigurationSettingList list = ConfigurationSettingDAO.DAO.GetList();
		    foreach(IConfigurationSetting setting in list) {
			settings.Add(setting.Key.ToString(), setting.Value.ToString());
		    }
		//}

		// TODO:  this should probably not return a modifiable version
		return settings;
	    }
	}

    }
}
