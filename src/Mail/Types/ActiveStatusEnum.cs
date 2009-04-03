using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class ActiveStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new ActiveStatusEnum DEFAULT = new ActiveStatusEnum();
	public static readonly new ActiveStatusEnum UNSET = new ActiveStatusEnum();

	public static readonly ActiveStatusEnum ACTIVE = new ActiveStatusEnum("Y", "Active");
	public static readonly ActiveStatusEnum DISABLED = new ActiveStatusEnum("N", "Disabled");

	public static ActiveStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (ActiveStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private ActiveStatusEnum() {}

	private ActiveStatusEnum(String code, String name) {
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
