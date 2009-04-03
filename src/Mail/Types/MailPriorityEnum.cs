using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class MailPriorityEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new MailPriorityEnum DEFAULT = new MailPriorityEnum();
	public static readonly new MailPriorityEnum UNSET = new MailPriorityEnum();

	public static readonly MailPriorityEnum HIGH = new MailPriorityEnum("High", "High");
	public static readonly MailPriorityEnum LOW = new MailPriorityEnum("Low", "Low");
	public static readonly MailPriorityEnum NORMAL = new MailPriorityEnum("Normal", "Normal");

	public static MailPriorityEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailPriorityEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailPriorityEnum() {}

	private MailPriorityEnum(String code, String name) {
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
