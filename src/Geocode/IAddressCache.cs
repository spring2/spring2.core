using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Geocode {

    public class AddressCacheFields {
	private AddressCacheFields() {}
	public static readonly String ADDRESSID = "AddressId";
	public static readonly String ADDRESS1 = "Address1";
	public static readonly String CITY = "City";
	public static readonly String REGION = "Region";
	public static readonly String POSTALCODE = "PostalCode";
	public static readonly String LATITUDE = "Latitude";
	public static readonly String LONGITUDE = "Longitude";
	public static readonly String RESULT = "Result";
	public static readonly String STATUS = "Status";
    }

    public interface IAddressCache : IBusinessEntity {
	IdType AddressId {
	    get;
	}
	StringType Address1 {
	    get;
	}
	StringType City {
	    get;
	}
	StringType Region {
	    get;
	}
	StringType PostalCode {
	    get;
	}
	DecimalType Latitude {
	    get;
	}
	DecimalType Longitude {
	    get;
	}
	StringType Result {
	    get;
	}
	GeocodeStatusEnum Status {
	    get;
	}
    }
}
