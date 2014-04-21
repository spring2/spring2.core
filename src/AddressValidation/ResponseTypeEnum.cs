using System;
using Spring2.Core.Types;

namespace Spring2.Core.AddressValidation {
    public class ResponseTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly ResponseTypeEnum DEFAULT = new ResponseTypeEnum();
	new public static readonly ResponseTypeEnum UNSET = new ResponseTypeEnum();

	public static readonly ResponseTypeEnum VALID = new ResponseTypeEnum("Valid", "Valid");
	public static readonly ResponseTypeEnum INVALID = new ResponseTypeEnum("Invalid", "Invalid");
	public static readonly ResponseTypeEnum AMBIGUOUS = new ResponseTypeEnum("Ambiguous", "Ambiguous");
	public static readonly ResponseTypeEnum VALID_CITYSTATEZIP = new ResponseTypeEnum("Valid_CityStateZip", "Valid_CityStateZip");

	public static ResponseTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (ResponseTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private ResponseTypeEnum() {
	}

	private ResponseTypeEnum(String code, String name) {
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