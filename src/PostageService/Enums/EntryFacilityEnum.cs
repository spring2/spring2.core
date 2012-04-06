using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class EntryFacilityEnum : EnumDataType{
        private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly EntryFacilityEnum DEFAULT = new EntryFacilityEnum();
	new public static readonly EntryFacilityEnum UNSET = new EntryFacilityEnum();

	public static readonly EntryFacilityEnum DBMC = new EntryFacilityEnum("DBMC", "DBMC");
	public static readonly EntryFacilityEnum DDU = new EntryFacilityEnum("DDU", "DDU");
	public static readonly EntryFacilityEnum DSCF = new EntryFacilityEnum("DSCF", "DSCF");
	public static readonly EntryFacilityEnum OBMC = new EntryFacilityEnum("OBMC", "OBMC");
	public static readonly EntryFacilityEnum OTHER = new EntryFacilityEnum("Other", "Other");

	public static EntryFacilityEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (EntryFacilityEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private EntryFacilityEnum() {
	}

	private EntryFacilityEnum(String code, String name) {
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
