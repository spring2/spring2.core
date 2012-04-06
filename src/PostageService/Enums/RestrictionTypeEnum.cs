using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class RestrictionTypeEnum  : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly RestrictionTypeEnum DEFAULT = new RestrictionTypeEnum();
	new public static readonly RestrictionTypeEnum UNSET = new RestrictionTypeEnum();

	public static readonly RestrictionTypeEnum NONE = new RestrictionTypeEnum("None", "None");
	public static readonly RestrictionTypeEnum OTHER = new RestrictionTypeEnum("Other", "Other");
	public static readonly RestrictionTypeEnum QUARANTINE = new RestrictionTypeEnum("Quarantine", "Quarantine");
	public static readonly RestrictionTypeEnum SANITARYPHYTOSANITARYINSPECTION = new RestrictionTypeEnum("SanitaryPhytosanitaryInspection", "Sanitary Phytosanitary Inspection");

	public static RestrictionTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (RestrictionTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private RestrictionTypeEnum() {
	}

	private RestrictionTypeEnum(String code, String name) {
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
