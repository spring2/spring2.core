using System;

namespace Spring2.Core.Util {
    /// <summary>
    /// Summary description for DateUtil.
    /// </summary>
    public class DateUtil {

	/// <summary>
	/// Return the last day of the year/month represented by the mmyy date string.
	/// </summary>
	/// <param name="mmyy"></param>
	/// <returns></returns>
	public static DateTime ToDateTimeFromCreditCardDate(String mmyy) {
	    if (mmyy.Length!=4) {
		throw new ArgumentOutOfRangeException("CreditCardDate string is expected to be a 4 character string in the format of mmyy");
	    }
	    Int32 mm = Int32.Parse(mmyy.Substring(0,2));
	    Int32 yy = Int32.Parse(mmyy.Substring(2,2));
	    Int32 dd = 1;

	    if (mm<12) {
		mm+=1;
	    } else {
		mm=1;
		yy+=1;
	    }

	    DateTime dt = new DateTime(yy,mm,dd);
	    return dt.AddDays(-1);
	}
    }
}
