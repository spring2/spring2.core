using System;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace Spring2.Core.Types {
    
    /// <summary>
    /// Summary description for StringType.
    /// </summary>
    [TypeConverter(typeof(StringTypeConverter))]
    public class StringType : DataType, IComparable {

	public static readonly new StringType DEFAULT = new StringType();
	public static readonly new StringType UNSET = new StringType();
	public static readonly StringType EMPTY = new StringType(String.Empty);

	[Obsolete("Use Parse method instead.")]
	public static StringType NewInstance(String value) {
	    return value == null ? UNSET : new StringType(value);
	}

	public static StringType Parse(String value) {
	    return value == null ? UNSET : new StringType(value);
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

	public Boolean IsEmpty {
	    get { return !IsValid || String.Empty.Equals(value.Trim()); }
	}

	public Int32 CompareTo(Object o) {

	    StringType that = o as StringType;
	    if (that == null) {
		throw new ArgumentException("Argument must be an instance of StringType");
	    }

	    if (this.IsValid && that.IsValid) {
		return value.CompareTo(that.Value);
	    }
	    
	    return Compare(that);
	}
    }
}
