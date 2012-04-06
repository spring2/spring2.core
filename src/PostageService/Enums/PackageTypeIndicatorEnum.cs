using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class PackageTypeIndicatorEnum  : EnumDataType {
    	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly PackageTypeIndicatorEnum DEFAULT = new PackageTypeIndicatorEnum();
	new public static readonly PackageTypeIndicatorEnum UNSET = new PackageTypeIndicatorEnum();

	public static readonly PackageTypeIndicatorEnum REGULAR = new PackageTypeIndicatorEnum("Regular", "Regular");
	public static readonly PackageTypeIndicatorEnum SOFTPACK = new PackageTypeIndicatorEnum("Softpack", "Softpack");

	public static PackageTypeIndicatorEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (PackageTypeIndicatorEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private PackageTypeIndicatorEnum() {
	}

	private PackageTypeIndicatorEnum(String code, String name) {
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
