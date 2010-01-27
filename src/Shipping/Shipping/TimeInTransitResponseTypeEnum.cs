using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public class TimeInTransitResponseTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

        new public static readonly TimeInTransitResponseTypeEnum DEFAULT = new TimeInTransitResponseTypeEnum();
        new public static readonly TimeInTransitResponseTypeEnum UNSET = new TimeInTransitResponseTypeEnum();

        public static readonly TimeInTransitResponseTypeEnum VALID = new TimeInTransitResponseTypeEnum("Valid", "Valid");
        public static readonly TimeInTransitResponseTypeEnum INVALID = new TimeInTransitResponseTypeEnum("Invalid", "Invalid");
        public static readonly TimeInTransitResponseTypeEnum AMBIGUOUS = new TimeInTransitResponseTypeEnum("Ambiguous", "Ambiguous");

	public static TimeInTransitResponseTypeEnum GetInstance(Object value) {
	    if (value is String) {
                foreach (TimeInTransitResponseTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TimeInTransitResponseTypeEnum() {
	}

        private TimeInTransitResponseTypeEnum(String code, String name) {
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
