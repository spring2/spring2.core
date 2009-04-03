using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Types;
using Spring2.Core.Types;



using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Geocode.DataObject {

    public class AddressCacheFields {
	private AddressCacheFields() {}
	public static readonly String ENTITY_NAME = "AddressCache";
	
	public static readonly String ADDRESSID = "AddressId";
	public static readonly String ADDRESS1 = "Address1";
	public static readonly String CITY = "City";
	public static readonly String REGION = "Region";
	public static readonly String POSTALCODE = "PostalCode";
	public static readonly String LATITUDE = "Latitude";
	public static readonly String LONGITUDE = "Longitude";
	public static readonly String RESULT = "Result";
	public static readonly String STATUS = "Status";
	public static readonly String STDADDRESS1 = "StdAddress1";
	public static readonly String STDCITY = "StdCity";
	public static readonly String STDREGION = "StdRegion";
	public static readonly String STDPOSTALCODE = "StdPostalCode";
	public static readonly String STDPLUS4 = "StdPlus4";
	public static readonly String MATADDRESS1 = "MatAddress1";
	public static readonly String MATCITY = "MatCity";
	public static readonly String MATREGION = "MatRegion";
	public static readonly String MATPOSTALCODE = "MatPostalCode";
	public static readonly String MATCHTYPE = "MatchType";
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
	StringType StdAddress1 {
	    get;
	}
	StringType StdCity {
	    get;
	}
	StringType StdRegion {
	    get;
	}
	StringType StdPostalCode {
	    get;
	}
	StringType StdPlus4 {
	    get;
	}
	StringType MatAddress1 {
	    get;
	}
	StringType MatCity {
	    get;
	}
	StringType MatRegion {
	    get;
	}
	StringType MatPostalCode {
	    get;
	}
	IntegerType MatchType {
	    get;
	}
    }
}
