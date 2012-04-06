using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class SortTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly SortTypeEnum DEFAULT = new SortTypeEnum();
	new public static readonly SortTypeEnum UNSET = new SortTypeEnum();

	public static readonly SortTypeEnum BMC = new SortTypeEnum("BMC", "BMC");
	public static readonly SortTypeEnum FIVEDIGIT = new SortTypeEnum("FiveDigit", "Five Digit");
	public static readonly SortTypeEnum MIXEDBMC = new SortTypeEnum("MixedBMC", "Mixed BMC");
	public static readonly SortTypeEnum NONPRESORTED = new SortTypeEnum("Nonpresorted", "Nonpresorted");
	public static readonly SortTypeEnum PRESORTED = new SortTypeEnum("Presorted", "Presorted");
	public static readonly SortTypeEnum SCF = new SortTypeEnum("SCF", "SCF");
	public static readonly SortTypeEnum SINGLEPIECE = new SortTypeEnum("SinglePiece", "Single Piece");
	public static readonly SortTypeEnum THREEDIGIT = new SortTypeEnum("ThreeDigit", "Three Digit");

	public static SortTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (SortTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private SortTypeEnum() {
	}

	private SortTypeEnum(String code, String name) {
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
