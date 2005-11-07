using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Geocode {
    
    
    public class AddressCache : Spring2.Core.BusinessEntity.BusinessEntity, IAddressCache {
        
        [Generate()]
        private IdType addressId = IdType.DEFAULT;
        
        [Generate()]
        private StringType address1 = StringType.DEFAULT;
        
        [Generate()]
        private StringType city = StringType.DEFAULT;
        
        [Generate()]
        private StringType region = StringType.DEFAULT;
        
        [Generate()]
        private StringType postalCode = StringType.DEFAULT;
        
        [Generate()]
        private DecimalType latitude = DecimalType.DEFAULT;
        
        [Generate()]
        private DecimalType longitude = DecimalType.DEFAULT;
        
        [Generate()]
        private StringType result = StringType.DEFAULT;
        
        [Generate()]
        private GeocodeStatusEnum status = GeocodeStatusEnum.DEFAULT;
        
        [Generate()]
        internal AddressCache() {
            
        }
        
        [Generate()]
        internal AddressCache(Boolean isNew) {
            this.isNew = isNew;
        }
        
        [Generate()]
        public IdType AddressId {
            get {
                return this.addressId;
            }
            set {
                this.addressId = value;
            }
        }
        
        [Generate()]
        public StringType Address1 {
            get {
                return this.address1;
            }
            set {
                this.address1 = value;
            }
        }
        
        [Generate()]
        public StringType City {
            get {
                return this.city;
            }
            set {
                this.city = value;
            }
        }
        
        [Generate()]
        public StringType Region {
            get {
                return this.region;
            }
            set {
                this.region = value;
            }
        }
        
        [Generate()]
        public StringType PostalCode {
            get {
                return this.postalCode;
            }
            set {
                this.postalCode = value;
            }
        }
        
        [Generate()]
        public DecimalType Latitude {
            get {
                return this.latitude;
            }
            set {
                this.latitude = value;
            }
        }
        
        [Generate()]
        public DecimalType Longitude {
            get {
                return this.longitude;
            }
            set {
                this.longitude = value;
            }
        }
        
        [Generate()]
        public StringType Result {
            get {
                return this.result;
            }
            set {
                this.result = value;
            }
        }
        
        [Generate()]
        public GeocodeStatusEnum Status {
            get {
                return this.status;
            }
            set {
                this.status = value;
            }
        }
        
        [Generate()]
        public static AddressCache NewInstance() {
            return new AddressCache();
        }
        
        [Generate()]
        public static AddressCache GetInstance(IdType addressId) {
            return AddressCacheDAO.DAO.Load(addressId);
        }
        
        [Generate()]
        public void Update(AddressCacheData data) {
            addressId = data.AddressId.IsDefault ? addressId : data.AddressId;
	    address1 = data.Address1.IsDefault ? address1 : data.Address1;
	    city = data.City.IsDefault ? city : data.City;
	    region = data.Region.IsDefault ? region : data.Region;
	    postalCode = data.PostalCode.IsDefault ? postalCode : data.PostalCode;
	    latitude = data.Latitude.IsDefault ? latitude : data.Latitude;
	    longitude = data.Longitude.IsDefault ? longitude : data.Longitude;
	    result = data.Result.IsDefault ? result : data.Result;
	    status = data.Status.IsDefault ? status : data.Status;
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
		this.AddressId = AddressCacheDAO.DAO.Insert(this);
		isNew = false;
            } else {
		AddressCacheDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            AddressCacheDAO.DAO.Reload(this);
        }
    }
}