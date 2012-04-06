using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class SundayHolidayDeliveryEnum: EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly SundayHolidayDeliveryEnum DEFAULT = new SundayHolidayDeliveryEnum();
	new public static readonly SundayHolidayDeliveryEnum UNSET = new SundayHolidayDeliveryEnum();

	public static readonly SundayHolidayDeliveryEnum TRUE = new SundayHolidayDeliveryEnum("TRUE", "True");
	public static readonly SundayHolidayDeliveryEnum FALSE = new SundayHolidayDeliveryEnum("FALSE", "False");
	public static readonly SundayHolidayDeliveryEnum SUNDAY = new SundayHolidayDeliveryEnum("SUNDAY", "Sunday");
	public static readonly SundayHolidayDeliveryEnum HOLIDAY = new SundayHolidayDeliveryEnum("HOLIDAY", "Holiday");

	public static SundayHolidayDeliveryEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (SundayHolidayDeliveryEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private SundayHolidayDeliveryEnum() {
	}

	private SundayHolidayDeliveryEnum(String code, String name) {
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
