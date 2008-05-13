using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

namespace Spring2.Core.Configuration {

    public class ConfigurationSettingData : Spring2.Core.DataObject.DataObject {

	public static readonly ConfigurationSettingData DEFAULT = new ConfigurationSettingData();

	private IdType configurationSettingId = IdType.DEFAULT;
	private StringType key = StringType.DEFAULT;
	private StringType value = StringType.DEFAULT;
	private DateTimeType lastModifiedDate = DateTimeType.DEFAULT;
	private IdType lastModifiedUserId = IdType.DEFAULT;
        private DateTimeType effectiveDate = DateTimeType.DEFAULT;

	public IdType ConfigurationSettingId {
	    get { return this.configurationSettingId; }
	    set { this.configurationSettingId = value; }
	}

	public StringType Key {
	    get { return this.key; }
	    set { this.key = value; }
	}

	public StringType Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

	public DateTimeType LastModifiedDate {
	    get { return this.lastModifiedDate; }
	    set { this.lastModifiedDate = value; }
	}

	public IdType LastModifiedUserId {
	    get { return this.lastModifiedUserId; }
	    set { this.lastModifiedUserId = value; }
	}

        public DateTimeType EffectiveDate {
            get { return this.effectiveDate; }
            set { this.effectiveDate = value; }
        }

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
