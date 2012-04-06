using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class OnOffEnum  : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly OnOffEnum DEFAULT = new OnOffEnum();
	new public static readonly OnOffEnum UNSET = new OnOffEnum();

	public static readonly OnOffEnum ON = new OnOffEnum("On", "On");
	public static readonly OnOffEnum OFF = new OnOffEnum("Off", "Off");

	public static OnOffEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (OnOffEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private OnOffEnum() {
	}

	private OnOffEnum(String code, String name) {
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
