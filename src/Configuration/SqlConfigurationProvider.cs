using System;
using System.Collections.Specialized;
using System.Configuration;

using Spring2.Core.DAO;
using Spring2.Core.Types;

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
                    OrderByClause sort = new OrderByClause("[Key], EffectiveDate desc");
		    ConfigurationSettingList list = ConfigurationSettingDAO.DAO.GetList(sort);
		    foreach(IConfigurationSetting setting in list) {
                        if (setting.EffectiveDate > DateTimeType.Now) {
                            continue;
                        }
                        if (settings[setting.Key.ToString()] == null) {
                            settings.Add(setting.Key.ToString(), setting.Value.ToString());
                        }
		    }
		//}

		// TODO:  this should probably not return a modifiable version
		return settings;
	    }
	}

    }
}
