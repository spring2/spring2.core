using System;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {
    public class TaxAreaData : Spring2.Core.DataObject.DataObject {
	public static readonly TaxAreaData DEFAULT = new TaxAreaData();

	private IdType taxAreaID = IdType.DEFAULT;
	private StringType taxArea = StringType.DEFAULT;
	private StringType country = StringType.DEFAULT;
	private StringType region = StringType.DEFAULT;
	private StringType county = StringType.DEFAULT;
	private StringType streetAddress = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType postalCode = StringType.DEFAULT;
	private DecimalType countryTaxRate = DecimalType.DEFAULT;
	private DecimalType regionTaxRate = DecimalType.DEFAULT;
	private DecimalType countyTaxRate = DecimalType.DEFAULT;
	private DecimalType cityTaxRate = DecimalType.DEFAULT;
	private DecimalType totalTaxRate = DecimalType.DEFAULT;
	private BooleanType isShippingTaxable = BooleanType.DEFAULT;
	private BooleanType addressValidated = BooleanType.DEFAULT;
	private TaxJurisdictionList taxJurisdictions = new TaxJurisdictionList();

	public IdType TaxAreaID {
	    get { return this.taxAreaID; }
	    set { this.taxAreaID = value; }
	}

	public StringType TaxArea {
	    get { return this.taxArea; }
	    set { this.taxArea = value; }
	}

	public StringType Country {
	    get { return this.country; }
	    set { this.country = value; }
	}

	public StringType Region {
	    get { return this.region; }
	    set { this.region = value; }
	}

	public StringType County {
	    get { return this.county; }
	    set { this.county = value; }
	}

	public StringType Street {
	    get { return this.streetAddress; }
	    set { this.streetAddress = value; }
	}

	public StringType City {
	    get { return this.city; }
	    set { this.city = value; }
	}

	public StringType PostalCode {
	    get { return this.postalCode; }
	    set { this.postalCode = value; }
	}

	public DecimalType CountryTaxRate {
	    get { return this.countryTaxRate; }
	    set { this.countryTaxRate = value; }
	}

	public DecimalType RegionTaxRate {
	    get { return this.regionTaxRate; }
	    set { this.regionTaxRate = value; }
	}

	public DecimalType CountyTaxRate {
	    get { return this.countyTaxRate; }
	    set { this.countyTaxRate = value; }
	}

	public DecimalType CityTaxRate {
	    get { return this.cityTaxRate; }
	    set { this.cityTaxRate = value; }
	}

	public DecimalType TotalTaxRate {
	    get { return this.totalTaxRate; }
	    set { this.totalTaxRate = value; }
	}

	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(DEFAULT, this); }
	}

	public BooleanType IsShippingTaxable {
	    get { return isShippingTaxable; }
	    set { isShippingTaxable = value; }
	}

	public BooleanType AddressValidated {
	    get { return addressValidated; }
	    set { addressValidated = value; }
	}

	public TaxJurisdictionList TaxJurisdictions {
	    get { return this.taxJurisdictions; }
	    set { this.taxJurisdictions = value; }
	}
    }
}