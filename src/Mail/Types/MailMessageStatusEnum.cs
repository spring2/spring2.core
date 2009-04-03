using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class MailMessageStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new MailMessageStatusEnum DEFAULT = new MailMessageStatusEnum();
	public static readonly new MailMessageStatusEnum UNSET = new MailMessageStatusEnum();

	public static readonly MailMessageStatusEnum UNPROCESSED = new MailMessageStatusEnum("Unprocessed", "Unprocessed");
	public static readonly MailMessageStatusEnum SENT = new MailMessageStatusEnum("Sent", "Sent");
	public static readonly MailMessageStatusEnum NOT_ALLOWED_FROM_TEST_SERVER = new MailMessageStatusEnum("Not Allowed From Test Server", "Not Allowed From Test Server");
	public static readonly MailMessageStatusEnum HELD = new MailMessageStatusEnum("Held", "Held");
	public static readonly MailMessageStatusEnum RELEASED = new MailMessageStatusEnum("Released", "Released");
	public static readonly MailMessageStatusEnum REJECTED = new MailMessageStatusEnum("Rejected", "Rejected");
	public static readonly MailMessageStatusEnum FAILED = new MailMessageStatusEnum("Failed", "Failed");

	public static MailMessageStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailMessageStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailMessageStatusEnum() {}

	private MailMessageStatusEnum(String code, String name) {
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
