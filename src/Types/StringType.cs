using System;
using System.Data.SqlTypes;

namespace Spring2.Core.Types {
    
    /// <summary>
    /// Summary description for StringType.
    /// </summary>
    public class StringType : DataType {

	public static readonly new StringType DEFAULT = new StringType();
	public static readonly new StringType UNSET = new StringType();

	public static StringType NewInstance(Object value) {

	    if (value is String) {
		return new StringType((String)value);
	    } else {
		return UNSET;
	    }
	}

	private String value;

	private StringType() {}

	private StringType(String value) {
	    this.value = value;
	}

	protected override Object DBValue {
	    get {
		return value;
	    }
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}
	
	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}
    }
}
