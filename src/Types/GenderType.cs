using System;
using System.Collections;

namespace Spring2.Core.Types {

    public class GenderType : EnumDataType {

	private static readonly IList OPTIONS = new ArrayList();

	public static readonly new GenderType DEFAULT = new GenderType();
	public static readonly new GenderType UNSET = new GenderType();

	public static readonly GenderType MALE = new GenderType("M", "Male");
	public static readonly GenderType FEMALE = new GenderType("F", "Female");

	public static GenderType GetInstance(Object value) {
	    if (value is String) {
		foreach (GenderType g in OPTIONS) {
		    if (g.Value.Equals(value)) {
			return g;
		    }
		}
	    }

	    return UNSET;
	}

	private GenderType() {
	}

	private GenderType(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset {
	    get {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}

	public static IList Options {
	    get { return OPTIONS; }
	}
    }
}
