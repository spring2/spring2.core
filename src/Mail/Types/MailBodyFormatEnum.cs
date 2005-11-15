using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class MailBodyFormatEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new MailBodyFormatEnum DEFAULT = new MailBodyFormatEnum();
	public static readonly new MailBodyFormatEnum UNSET = new MailBodyFormatEnum();

	public static readonly MailBodyFormatEnum HTML = new MailBodyFormatEnum("Html", "Html");
	public static readonly MailBodyFormatEnum TEXT = new MailBodyFormatEnum("Text", "Text");

	public static MailBodyFormatEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailBodyFormatEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailBodyFormatEnum() {}

	private MailBodyFormatEnum(String code, String name) {
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
