using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class IntegratedFormTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly IntegratedFormTypeEnum DEFAULT = new IntegratedFormTypeEnum();
	new public static readonly IntegratedFormTypeEnum UNSET = new IntegratedFormTypeEnum();

	public static readonly IntegratedFormTypeEnum FORM2976 = new IntegratedFormTypeEnum("Form2976", "Form 2976");
	public static readonly IntegratedFormTypeEnum FORM2976A = new IntegratedFormTypeEnum("Form2976A", "Form 2976A");

	public static IntegratedFormTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (IntegratedFormTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private IntegratedFormTypeEnum() {
	}

	private IntegratedFormTypeEnum(String code, String name) {
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
