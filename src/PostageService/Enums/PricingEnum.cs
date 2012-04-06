using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class PricingEnum : EnumDataType  {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly PricingEnum DEFAULT = new PricingEnum();
	new public static readonly PricingEnum UNSET = new PricingEnum();

	public static readonly PricingEnum COMMERCIALBASE = new PricingEnum("CommercialBase", "Commercial Base");
	public static readonly PricingEnum COMMERCIALPLUS = new PricingEnum("CommercialPlus", "Commercial Plus");
	public static readonly PricingEnum RETAIL = new PricingEnum("Retail", "Retail");

	public static PricingEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (PricingEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private PricingEnum() {
	}

	private PricingEnum(String code, String name) {
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
