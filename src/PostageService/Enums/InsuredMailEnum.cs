using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class InsuredMailEnum: EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly InsuredMailEnum DEFAULT = new InsuredMailEnum();
	new public static readonly InsuredMailEnum UNSET = new InsuredMailEnum();

	public static readonly InsuredMailEnum OFF = new InsuredMailEnum("OFF", "Off");
	public static readonly InsuredMailEnum USPSONLINE = new InsuredMailEnum("UspsOnline", "USPS Online");
	public static readonly InsuredMailEnum ENDICIA = new InsuredMailEnum("Endicia", "Endicia");

	public static InsuredMailEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (InsuredMailEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private InsuredMailEnum() {
	}

	private InsuredMailEnum(String code, String name) {
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
