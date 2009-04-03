using System;
using System.ComponentModel;
using System.Globalization;

namespace Spring2.Core.Types {

    /// <summary>
    /// A type converter for StringType.
    /// </summary>
    public class StringTypeConverter : TypeConverter {

	/// <summary>
	/// Overrides the base class method.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="sourceType"></param>
	/// <returns>true if this type converter can convert from the given type to a StringType.</returns>
	public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
      
	    if (sourceType == typeof(string)) {
		return true;
	    }
	    return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>
	/// Overrides the base class method.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="culture"></param>
	/// <param name="value"></param>
	/// <returns>a StringType object converted from the given object.</returns>
	public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value) {
	    String s = value as String;
	    return s == null ? base.ConvertFrom(context, culture, value) : StringType.Parse(s);
	}

	public new Object ConvertFromString(String value) {
	    return StringType.Parse(value);
	}

	public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType) {  
	    StringType s = value as StringType;
	    if (s != null && destinationType == typeof(string)) {
		return s.ToString();
	    }
	    return base.ConvertTo(context, culture, value, destinationType);
	}
    }
}
