using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.Types {

    public class GeocodeStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new GeocodeStatusEnum DEFAULT = new GeocodeStatusEnum();
	public static readonly new GeocodeStatusEnum UNSET = new GeocodeStatusEnum();

	public static readonly GeocodeStatusEnum VALID = new GeocodeStatusEnum("Valid", "Valid");
	public static readonly GeocodeStatusEnum INVALID = new GeocodeStatusEnum("Invalid", "Invalid");

	public static GeocodeStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (GeocodeStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private GeocodeStatusEnum() {}

	private GeocodeStatusEnum(String code, String name) {
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
