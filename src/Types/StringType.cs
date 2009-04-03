using System;
using System.Globalization;
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

	/// <summary>
	/// Read only property to get the length of the underlying string.
	/// Returns -1 if the instance is not valid.
	/// </summary>
	public Int32 Length {
	    get { return IsValid ? value.Length : -1; }
	}

	/// <summary>
	/// Removes leading and trailing whitespace from the string.
	/// </summary>
	/// <returns>a new StringType with leading and trailing whitespace
	/// characters removed.</returns>
	public StringType Trim() {
	    return IsValid ? new StringType(value.Trim()) : this;
	}

	/// <summary>
	/// Removes all occurrences of a set of characters specified in an array from the end of this instance.
	/// </summary>
	/// <param name="trimChars">An array of Unicode characters to be removed or a null reference </param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// The String that remains after all occurrences of the characters in trimChars are removed from the end. If trimChars is a null reference,
	///  white space characters are removed instead</returns>
	public StringType TrimEnd(params char[] trimChars) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string trimmedString = value.TrimEnd(trimChars);
	    return new StringType(trimmedString);
	}

	/// <summary>
	/// Removes all occurrences of a set of characters specified in an array from the beginning of this instance.
	/// </summary>
	/// <param name="trimChars">An array of Unicode characters to be removed or a null reference.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// The StringType that remains after all occurrences of characters in trimChars are removed from the beginning.
	/// If trimChars is a null reference, white space characters are removed instead.</returns>
	public StringType TrimStart(params char[] trimChars) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string trimmedString = value.TrimStart(trimChars);
	    return new StringType(trimmedString);
	}

	/// <summary>
	/// Right-aligns the characters in this instance, padding with spaces on the left for a specified total length.
	/// </summary>
	/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original
	/// characters plus any additional padding characters.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A new StringType that is equivalent to this instance, but right-aligned and padded on the left with as many spaces as needed to create a length of totalWidth.
	/// -or-
	/// If totalWidth is less than the length of this instance, a new StringType that is identical to this instance.
	/// </returns>
	public StringType PadLeft(int totalWidth) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string paddedString = value.PadLeft(totalWidth);

	    return new StringType(paddedString);
	}

	/// <summary>
	/// Right-aligns the characters in this instance, padding on the left with a specified Unicode character for a specified total length.
	/// </summary>
	/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
	/// <param name="paddingChar">A Unicode padding character.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A new StringType that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters as needed to create a length of totalWidth. 
	/// -or- 
	/// If totalWidth is less than the length of this instance, a new StringType that is identical to this instance.</returns>
	public StringType PadLeft(int totalWidth, char paddingChar) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string paddedString = value.PadLeft(totalWidth, paddingChar);

	    return new StringType(paddedString);
	}

	/// <summary>
	/// Left-aligns the characters in this string, padding with spaces on the right, for a specified total length.
	/// </summary>
	/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A new StringType that is equivalent to this instance, but left-aligned and padded on the right with as many spaces as needed to create a length of totalWidth.
	/// -or-
	/// If totalWidth is less than the length of this instance, a new StringType that is identical to this instance.</returns>
	public StringType PadRight(int totalWidth) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string paddedString = value.PadRight(totalWidth);

	    return new StringType(paddedString);
	}

	/// <summary>
	/// Left-aligns the characters in this string, padding on the right with a specified Unicode character, for a specified total length.
	/// </summary>
	/// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.</param>
	/// <param name="paddingChar">A Unicode padding character.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A new StringType that is equivalent to this instance, but left-aligned and padded on the right with as many paddingChar characters as needed to create a length of totalWidth.
	/// -or-
	/// If totalWidth is less than the length of this instance, a new StringType that is identical to this instance.</returns>
	public StringType PadRight(int totalWidth, char paddingChar) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string paddedString = value.PadRight(totalWidth, paddingChar);

	    return new StringType(paddedString);
	}

	/// <summary>
	/// Retrieves a substring from this instance. The substring starts at a specified character position.
	/// </summary>
	/// <param name="startIndex">The starting character position of a substring in this instance.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A StringType equivalent to the substring that begins at startIndex in this instance.
	/// -or-
	/// an empty StringType if startIndex is equal to the length of this instance.</returns>
	public StringType Substring(int startIndex) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string substring = value.Substring(startIndex);

	    return new StringType(substring);
	}

	/// <summary>
	/// Retrieves a substring from this instance. The substring starts at a specified character position and has a specified length.
	/// </summary>
	/// <param name="startIndex">The index of the start of the substring.</param>
	/// <param name="length">The number of characters in the substring.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A StringType equivalent to the substring of length length that begins at startIndex in this instance.
	/// -or-
	/// an empty StringType if startIndex is equal to the length of this instance and length is zero.</returns>
	public StringType Substring(int startIndex, int length) {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    string substring = value.Substring(startIndex, length);

	    return new StringType(substring);
	}

	/// <summary>
	/// Returns a copy of this StringType in lowercase, using the casing rules of the current culture.
	/// </summary>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A StringType in lowercase.</returns>
        public StringType ToLower() {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    return new StringType(value.ToLower());
        }

	/// <summary>
	/// Returns a copy of this StringType in uppercase, using the casing rules of the current culture.
	/// </summary>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A StringType in uppercase.</returns>
        public StringType ToUpper() {
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    return new StringType(value.ToUpper());
        }

	/// <summary>
	/// Returns a copy of this StringType in uppercase, using the casing rules of the specified culture.
	/// </summary>
	/// <param name="culture">A CultureInfo object that supplies culture-specific casing rules.</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// -or-
	/// A StringType in uppercase.</returns>
	public StringType ToUpper(CultureInfo culture)
	{
	    if (!IsValid) {
		return StringType.UNSET;
	    }

	    return new StringType(value.ToUpper(culture));
	}

	/// <summary>
	/// Conversion from StringType to string.
	/// </summary>
	/// <param name="wrappedString">The StringType to convert (unwrap) from</param>
	/// <returns>If this instance is not valid (IsValid == false) String.Empty is returned.
	/// Otherwise the native string. </returns>
	public static explicit operator string(StringType wrappedString)
	{
	    if (!wrappedString.IsValid) {
		return String.Empty;
	    }

	    return wrappedString.value;
	}

	/// <summary>
	/// Conversion from string to StringType.
	/// </summary>
	/// <param name="unwrappedString">The native string to wrap in a StringType</param>
	/// <returns>If this instance is not valid (IsValid == false) StringType.UNSET is returned.
	/// Otherwise the wrapped StringType. </returns>
	public static implicit operator StringType(string unwrappedString)
	{
	    return new StringType(unwrappedString);
	}

    }
}
