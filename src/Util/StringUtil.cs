using System;

namespace Spring2.Core.Util {
    /// <summary>
    /// Collection of miscellaneous String functions
    /// </summary>
    public class StringUtil {
	private StringUtil() { }

	/// <summary>
	/// Remove trailing Environment.NewLine from end of string.  Will remove multiples if they exist.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static String RemoveTrailingNewLine(String source) {
	    return RemoveTrailingString(source, Environment.NewLine);
	}

	/// <summary>
	/// Remove trailing string from end of a string.  Will remove multiples if they exist.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static String RemoveTrailingString(String source, String trailer) {
	    while (source.EndsWith(trailer)) {
		source = source.Substring(0, source.Length - trailer.Length);
	    }
	    return source;
	}

	/// <summary>
	/// Remove trailing Environment.NewLine from end of string.  Spaces are trimmed at end and between Environment.NewLine.
	/// </summary>
	/// <param name="s"></param>
	/// <returns></returns>
	public static String RemoveTrailingBlankLines(String source) {
	    source = source.TrimEnd();
	    while (source.EndsWith(Environment.NewLine)) {
		source = RemoveTrailingString(source, Environment.NewLine).TrimEnd();
	    }
	    return source;
	}


    }
}
