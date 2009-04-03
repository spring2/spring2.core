using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Mail.Types {

    public class RoutingTypeEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new RoutingTypeEnum DEFAULT = new RoutingTypeEnum();
	public static readonly new RoutingTypeEnum UNSET = new RoutingTypeEnum();

	public static readonly RoutingTypeEnum TO = new RoutingTypeEnum("To", "To");
	public static readonly RoutingTypeEnum FROM = new RoutingTypeEnum("From", "From");
	public static readonly RoutingTypeEnum CC = new RoutingTypeEnum("Cc", "Cc");
	public static readonly RoutingTypeEnum BCC = new RoutingTypeEnum("Bcc", "Bcc");

	public static RoutingTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (RoutingTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private RoutingTypeEnum() {}

	private RoutingTypeEnum(String code, String name) {
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
