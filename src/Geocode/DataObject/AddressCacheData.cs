using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Types;
using Spring2.Core.Types;



namespace Spring2.Core.Geocode.DataObject {

    public class AddressCacheData : Spring2.Core.DataObject.DataObject {

	public static readonly AddressCacheData DEFAULT = new AddressCacheData();

	private IdType addressId = IdType.DEFAULT;
	private StringType address1 = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType region = StringType.DEFAULT;
	private StringType postalCode = StringType.DEFAULT;
	private DecimalType latitude = DecimalType.DEFAULT;
	private DecimalType longitude = DecimalType.DEFAULT;
	private StringType result = StringType.DEFAULT;
	private GeocodeStatusEnum status = GeocodeStatusEnum.DEFAULT;
	private StringType stdAddress1 = StringType.DEFAULT;
	private StringType stdCity = StringType.DEFAULT;
	private StringType stdRegion = StringType.DEFAULT;
	private StringType stdPostalCode = StringType.DEFAULT;
	private StringType stdPlus4 = StringType.DEFAULT;
	private IntegerType matchType = IntegerType.DEFAULT;

	public IdType AddressId {
	    get { return this.addressId; }
	    set { this.addressId = value; }
	}

	public StringType Address1 {
	    get { return this.address1; }
	    set { this.address1 = value; }
	}

	public StringType City {
	    get { return this.city; }
	    set { this.city = value; }
	}

	public StringType Region {
	    get { return this.region; }
	    set { this.region = value; }
	}

	public StringType PostalCode {
	    get { return this.postalCode; }
	    set { this.postalCode = value; }
	}

	public DecimalType Latitude {
	    get { return this.latitude; }
	    set { this.latitude = value; }
	}

	public DecimalType Longitude {
	    get { return this.longitude; }
	    set { this.longitude = value; }
	}

	public StringType Result {
	    get { return this.result; }
	    set { this.result = value; }
	}

	public GeocodeStatusEnum Status {
	    get { return this.status; }
	    set { this.status = value; }
	}

	public StringType StdAddress1 {
	    get { return this.stdAddress1; }
	    set { this.stdAddress1 = value; }
	}

	public StringType StdCity {
	    get { return this.stdCity; }
	    set { this.stdCity = value; }
	}

	public StringType StdRegion {
	    get { return this.stdRegion; }
	    set { this.stdRegion = value; }
	}

	public StringType StdPostalCode {
	    get { return this.stdPostalCode; }
	    set { this.stdPostalCode = value; }
	}

	public StringType StdPlus4 {
	    get { return this.stdPlus4; }
	    set { this.stdPlus4 = value; }
	}

	public IntegerType MatchType {
	    get { return this.matchType; }
	    set { this.matchType = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
