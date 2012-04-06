using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class NonDeliveryOptionEnum  : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly NonDeliveryOptionEnum DEFAULT = new NonDeliveryOptionEnum();
	new public static readonly NonDeliveryOptionEnum UNSET = new NonDeliveryOptionEnum();

	public static readonly NonDeliveryOptionEnum ABANDON = new NonDeliveryOptionEnum("Abandon", "Abandon");
	public static readonly NonDeliveryOptionEnum RETURN = new NonDeliveryOptionEnum("Return", "Return");

	public static NonDeliveryOptionEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (NonDeliveryOptionEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private NonDeliveryOptionEnum() {
	}

	private NonDeliveryOptionEnum(String code, String name) {
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
