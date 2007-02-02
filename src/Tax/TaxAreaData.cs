using System;
using Spring2.Core.Types;

namespace Spring2.Dss.Tax {
    public class TaxAreaData {
	public static readonly TaxAreaData DEFAULT = new TaxAreaData();

	private IdType taxAreaID = IdType.DEFAULT;
	private StringType taxArea = StringType.DEFAULT;
	private StringType country = StringType.DEFAULT;
	private StringType region = StringType.DEFAULT;
	private StringType county = StringType.DEFAULT;
	private StringType streetAddress = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType postalCode = StringType.DEFAULT;
	private StringType localDistrict1 = StringType.DEFAULT;
	private StringType localDistrict2 = StringType.DEFAULT;
	private StringType localDistrict3 = StringType.DEFAULT;
	private DecimalType countryTaxRate = DecimalType.DEFAULT;
	private DecimalType regionTaxRate = DecimalType.DEFAULT;
	private DecimalType countyTaxRate = DecimalType.DEFAULT;
	private DecimalType cityTaxRate = DecimalType.DEFAULT;
	private DecimalType localDistrict1TaxRate = DecimalType.DEFAULT;
	private DecimalType localDistrict2TaxRate = DecimalType.DEFAULT;
	private DecimalType localDistrict3TaxRate = DecimalType.DEFAULT;
	private DecimalType totalTaxRate = DecimalType.DEFAULT;
	private BooleanType isShippingTaxable = BooleanType.DEFAULT;
	private BooleanType addressValidated = BooleanType.DEFAULT;

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

	public StringType LocalDistrict1 {
	    get { return this.localDistrict1; }
	    set { this.localDistrict1 = value; }
	}

	public StringType LocalDistrict2 {
	    get { return this.localDistrict2; }
	    set { this.localDistrict2 = value; }
	}

	public StringType LocalDistrict3 {
	    get { return this.localDistrict3; }
	    set { this.localDistrict3 = value; }
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

	public DecimalType LocalDistrict1TaxRate {
	    get { return this.localDistrict1TaxRate; }
	    set { this.localDistrict1TaxRate = value; }
	}

	public DecimalType LocalDistrict2TaxRate {
	    get { return this.localDistrict2TaxRate; }
	    set { this.localDistrict2TaxRate = value; }
	}

	public DecimalType LocalDistrict3TaxRate {
	    get { return this.localDistrict3TaxRate; }
	    set { this.localDistrict3TaxRate = value; }
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
    }
}