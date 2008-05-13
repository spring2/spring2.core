using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Spring2.Core.Configuration {
    
    
    public class ConfigurationSetting : Spring2.Core.BusinessEntity.BusinessEntity, IConfigurationSetting {
        
        [Generate()]
        private IdType configurationSettingId = IdType.DEFAULT;
        
        [Generate()]
        private StringType key = StringType.DEFAULT;
        
        [Generate()]
        private StringType value = StringType.DEFAULT;
        
        [Generate()]
        private DateTimeType lastModifiedDate = DateTimeType.DEFAULT;
        
        [Generate()]
        private IdType lastModifiedUserId = IdType.DEFAULT;

        [Generate()]
        private DateTimeType effectiveDate = DateTimeType.DEFAULT;
        
        [Generate()]
        internal ConfigurationSetting() {
            
        }
        
        [Generate()]
        internal ConfigurationSetting(Boolean isNew) {
            this.isNew = isNew;
        }
        
        [Generate()]
        public IdType ConfigurationSettingId {
            get {
                return this.configurationSettingId;
            }
            set {
                this.configurationSettingId = value;
            }
        }
        
        [Generate()]
        public StringType Key {
            get {
                return this.key;
            }
            set {
                this.key = value;
            }
        }
        
        [Generate()]
        public StringType Value {
            get {
                return this.value;
            }
            set {
                this.value = value;
            }
        }
        
        [Generate()]
        public DateTimeType LastModifiedDate {
            get {
                return this.lastModifiedDate;
            }
            set {
                this.lastModifiedDate = value;
            }
        }

        [Generate()]
        public DateTimeType EffectiveDate {
            get {
                return this.effectiveDate;
            }
            set {
                this.effectiveDate = value;
            }
        }
        
        [Generate()]
        public IdType LastModifiedUserId {
            get {
                return this.lastModifiedUserId;
            }
            set {
                this.lastModifiedUserId = value;
            }
        }
        
        [Generate()]
        public static ConfigurationSetting NewInstance() {
            return new ConfigurationSetting();
        }
        
        [Generate()]
        public static ConfigurationSetting GetInstance(IdType configurationSettingId) {
            return ConfigurationSettingDAO.DAO.Load(configurationSettingId);
        }
        
        [Generate()]
        public void Update(ConfigurationSettingData data) {
            configurationSettingId = data.ConfigurationSettingId.IsDefault ? configurationSettingId : data.ConfigurationSettingId;
	    key = data.Key.IsDefault ? key : data.Key;
	    value = data.Value.IsDefault ? value : data.Value;
	    lastModifiedDate = data.LastModifiedDate.IsDefault ? lastModifiedDate : data.LastModifiedDate;
	    lastModifiedUserId = data.LastModifiedUserId.IsDefault ? lastModifiedUserId : data.LastModifiedUserId;
            effectiveDate = data.EffectiveDate.IsDefault ? effectiveDate : data.EffectiveDate;
            Store();
        }
        
        [Generate()]
        public void Store() {
            MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
            }

	    if (isNew) {
		this.ConfigurationSettingId = ConfigurationSettingDAO.DAO.Insert(this);
		isNew = false;
            } else {
		ConfigurationSettingDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            ConfigurationSettingDAO.DAO.Reload(this);
        }
    }
}