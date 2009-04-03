using System;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {
    public class TaxJurisdictionTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly TaxJurisdictionTypeEnum DEFAULT = new TaxJurisdictionTypeEnum();
	new public static readonly TaxJurisdictionTypeEnum UNSET = new TaxJurisdictionTypeEnum();

	public static readonly TaxJurisdictionTypeEnum COUNTRY = new TaxJurisdictionTypeEnum("Country", "Country");
	public static readonly TaxJurisdictionTypeEnum CITY = new TaxJurisdictionTypeEnum("City", "City");
	public static readonly TaxJurisdictionTypeEnum COUNTY = new TaxJurisdictionTypeEnum("County", "County");
	public static readonly TaxJurisdictionTypeEnum STATE = new TaxJurisdictionTypeEnum("State", "State");
	public static readonly TaxJurisdictionTypeEnum DISTRICT = new TaxJurisdictionTypeEnum("District", "District");
	public static readonly TaxJurisdictionTypeEnum OTHER = new TaxJurisdictionTypeEnum("Other", "Other");
	public static readonly TaxJurisdictionTypeEnum LOCAL = new TaxJurisdictionTypeEnum("Local", "Local");

	public static TaxJurisdictionTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (TaxJurisdictionTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TaxJurisdictionTypeEnum() {
	}

	private TaxJurisdictionTypeEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static EnumDataTypeList Options {
	    get { return OPTIONS; }
	}
    }
}