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
	
	public static readonly ColumnMetaData ADDRESSID = new ColumnMetaData("AddressId", "AddressId", DbType.Int32, SqlDbType.Int, 0, 10, 0);
	public static readonly ColumnMetaData ADDRESS1 = new ColumnMetaData("Address1", "Address1", DbType.AnsiString, SqlDbType.VarChar, 80, 0, 0);
	public static readonly ColumnMetaData CITY = new ColumnMetaData("City", "City", DbType.AnsiString, SqlDbType.VarChar, 40, 0, 0);
	public static readonly ColumnMetaData REGION = new ColumnMetaData("Region", "Region", DbType.AnsiStringFixedLength, SqlDbType.Char, 2, 0, 0);
	public static readonly ColumnMetaData POSTALCODE = new ColumnMetaData("PostalCode", "PostalCode", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData LATITUDE = new ColumnMetaData("Latitude", "Latitude", DbType.Decimal, SqlDbType.Decimal, 0, 18, 8);
	public static readonly ColumnMetaData LONGITUDE = new ColumnMetaData("Longitude", "Longitude", DbType.Decimal, SqlDbType.Decimal, 0, 18, 8);
	public static readonly ColumnMetaData RESULT = new ColumnMetaData("Result", "Result", DbType.AnsiString, SqlDbType.VarChar, 1000, 0, 0);
	public static readonly ColumnMetaData STATUS = new ColumnMetaData("Status", "Status", DbType.AnsiString, SqlDbType.VarChar, 20, 0, 0);
	public static readonly ColumnMetaData STDADDRESS1 = new ColumnMetaData("StdAddress1", "StdAddress1", DbType.AnsiString, SqlDbType.VarChar, 80, 0, 0);
	public static readonly ColumnMetaData STDCITY = new ColumnMetaData("StdCity", "StdCity", DbType.AnsiString, SqlDbType.VarChar, 40, 0, 0);
	public static readonly ColumnMetaData STDREGION = new ColumnMetaData("StdRegion", "StdRegion", DbType.AnsiStringFixedLength, SqlDbType.Char, 2, 0, 0);
	public static readonly ColumnMetaData STDPOSTALCODE = new ColumnMetaData("StdPostalCode", "StdPostalCode", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData STDPLUS4 = new ColumnMetaData("StdPlus4", "StdPlus4", DbType.AnsiStringFixedLength, SqlDbType.Char, 4, 0, 0);
	public static readonly ColumnMetaData MATADDRESS1 = new ColumnMetaData("MatAddress1", "MatAddress1", DbType.AnsiString, SqlDbType.VarChar, 80, 0, 0);
	public static readonly ColumnMetaData MATCITY = new ColumnMetaData("MatCity", "MatCity", DbType.AnsiString, SqlDbType.VarChar, 40, 0, 0);
	public static readonly ColumnMetaData MATREGION = new ColumnMetaData("MatRegion", "MatRegion", DbType.AnsiStringFixedLength, SqlDbType.Char, 2, 0, 0);
	public static readonly ColumnMetaData MATPOSTALCODE = new ColumnMetaData("MatPostalCode", "MatPostalCode", DbType.AnsiString, SqlDbType.VarChar, 10, 0, 0);
	public static readonly ColumnMetaData MATCHTYPE = new ColumnMetaData("MatchType", "MatchType", DbType.Int32, SqlDbType.Int, 0, 10, 0);
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
