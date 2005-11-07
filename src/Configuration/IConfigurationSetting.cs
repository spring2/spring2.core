using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Configuration {

    public class ConfigurationSettingFields {
	private ConfigurationSettingFields() {}
	public static readonly String CONFIGURATIONSETTINGID = "ConfigurationSettingId";
	public static readonly String KEY = "Key";
	public static readonly String VALUE = "Value";
	public static readonly String LASTMODIFIEDDATE = "LastModifiedDate";
	public static readonly String LASTMODIFIEDUSERID = "LastModifiedUserId";
    }

    public interface IConfigurationSetting : IBusinessEntity {
	IdType ConfigurationSettingId {
	    get;
	}
	StringType Key {
	    get;
	}
	StringType Value {
	    get;
	}
	DateTimeType LastModifiedDate {
	    get;
	}
	IdType LastModifiedUserId {
	    get;
	}
    }
}
