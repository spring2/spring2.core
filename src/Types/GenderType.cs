using System;
using System.Collections;

namespace Spring2.Core.Types {

    public class GenderType : DataType	{

	private static readonly IList OPTIONS = new ArrayList();

	public static readonly new GenderType DEFAULT = new GenderType();
	public static readonly new GenderType UNSET = new GenderType();

	public static readonly GenderType MALE = new GenderType("M");
	public static readonly GenderType FEMALE = new GenderType("F");

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

	private String code;

	private GenderType() {
	}

	private GenderType(String code) {
	    this.code = code;
	    OPTIONS.Add(this);
	}

	protected override Object Value {
	    get {
		return code;
	    }
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
    }
}
