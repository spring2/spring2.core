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

	/// <summary>
	/// Constructs a new StringType object from a String.
	/// This constructor is private to avoid creating
	/// StringTypes with a null internal value.  Use 
	/// NewInstance instead.
	/// </summary>
	/// <param name="value">the internal value of the new 
	/// object.</param>
	private StringType(String value) {
	    this.value = value;
	}

	protected override Object Value {
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

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (value == null || !(o is StringType)) {
		return false;
	    } else {
		return value.Equals(((StringType)o).value);
	    }
	}

	public override int GetHashCode() {
	    return value == null ? 0 : value.GetHashCode();
	}
    }
}
