using System;
using System.Collections;
using System.Globalization;

namespace Spring2.Core.Globalization {

/**
 * TODO:
 * 
 * support friendly standard and daylight names
 * need a GetUtcOffset for universal time
 */


    /// <summary>
    /// 
    /// </summary>
    public class RegionalTimeZone : TimeZone {

	private static readonly IList OPTIONS = new ArrayList();

	// Military/NATO time zones, whole hour offset, no daylight time
	public static readonly RegionalTimeZone Y = new RegionalTimeZone("Y", "Yankee", null, new TimeSpan(-12,0,0));
	public static readonly RegionalTimeZone Z = new RegionalTimeZone("Z", "Zulu", null, TimeSpan.Zero);
	public static readonly RegionalTimeZone R = new RegionalTimeZone("R", "Romeo", null, new TimeSpan(-5,0,0));
	public static readonly RegionalTimeZone T = new RegionalTimeZone("T", "Tango", null, new TimeSpan(-7,0,0));

	// Time zones that use North American daylight time rules
	public static readonly RegionalTimeZone R_US = new RegionalTimeZone("R-US", "US Eastern Time", DaylightTimeRule.NORTH_AMERICA, new TimeSpan(-5,0,0));
	public static readonly RegionalTimeZone T_US = new RegionalTimeZone("T-US", "US Mountain Time", DaylightTimeRule.NORTH_AMERICA, new TimeSpan(-7,0,0));

	// Time zones that use EU daylight time rules
	public static readonly RegionalTimeZone Z_EU = new RegionalTimeZone("Z-EU", "Europe Western Time", DaylightTimeRule.EUROPE, TimeSpan.Zero);
	public static readonly RegionalTimeZone A_EU = new RegionalTimeZone("A-EU", "Europe Central Time", DaylightTimeRule.EUROPE, new TimeSpan(1,0,0));

	protected String code;
	protected String name;
	protected DaylightTimeRule daylightTimeRule;
	protected TimeSpan utcOffset;

	private RegionalTimeZone() {}

	/// <summary>
	/// Constructor to create the limited number of TimeZone instances
	/// </summary>
	/// <param name="code">shorthand for the database</param>
	/// <param name="name">friendly, descriptive name</param>
	/// <param name="daylightTime">period of time that is "daylight savings time"</param>
	/// <param name="utcOffset">hours offset from UTC</param>
	private RegionalTimeZone(String code, String name, DaylightTimeRule daylightTimeRule, TimeSpan utcOffset) {
	    this.code = code;
	    this.name = name;
	    this.daylightTimeRule = daylightTimeRule;
	    this.utcOffset = utcOffset;
	    OPTIONS.Add(this);
	}

	/// <summary>
	/// Returns the intialized instance for the corresponding TimeZone.
	/// </summary>
	/// <param name="code"></param>
	/// <returns></returns>
	public static RegionalTimeZone GetInstance(String code) {
	    foreach (RegionalTimeZone t in OPTIONS) {
		if (t.Code.Equals(code)) {
		    return t;
		}
	    }

	    throw new ArgumentOutOfRangeException("code", code, code + " is not a valid time zone");
	}

	/// <summary>
	/// The code used to get a particular TimeZone instance
	/// </summary>
	public String Code {
	    get { return code; }
	}

	/// <summary>
	/// Name of TimeZone
	/// </summary>
	public String Name {
	    get { return name; }
	}

	/// <summary>
	/// Standard time name
	/// </summary>
	public override String StandardName {
	    get { return name; }
	}

	/// <summary>
	/// Daylight time name, empty string if daylight time is not observed
	/// </summary>
	public override String DaylightName {
	    get { return daylightTimeRule == null ? String.Empty : name; }
	}

	/// <summary>
	/// UTC offset for the specified time
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public override TimeSpan GetUtcOffset(DateTime time) {
	    if (IsDaylightSavingTime(time)) {
		return utcOffset.Add(new TimeSpan(1,0,0));
	    } else {
		return utcOffset;
	    }
	}

	/// <summary>
	/// DaylightTime for the specified year
	/// </summary>
	/// <param name="year"></param>
	/// <returns></returns>
	public override DaylightTime GetDaylightChanges(Int32 year) {
	    if (daylightTimeRule==null) {
		return null;
	    } else {
		return daylightTimeRule.GetDaylightTime(year, utcOffset);
	    }
	}

	/// <summary>
	/// Is the date specified within the daylight time.  Returns false if daylight time is not observed.
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public override Boolean IsDaylightSavingTime(DateTime time) {
	    if (daylightTimeRule == null) {
		return false;
	    }

	    DaylightTime daylight = GetDaylightChanges(time.Year);
	    if (time >= daylight.Start && time < daylight.End) {
		return true;
	    } else {
		return false;
	    }
	}

	/// <summary>
	/// Returns the local time that corresponds to the specified coordinated universal time (UTC)
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public override DateTime ToLocalTime(DateTime time) {
	    return time.Add(GetUtcOffset(time));
	}

	/// <summary>
	/// Convert local time of another time zone to local time
	/// </summary>
	/// <param name="time"></param>
	/// <param name="timezone"></param>
	/// <returns></returns>
	public virtual DateTime ToLocalTime(DateTime time, TimeZone timezone) {
	    return ToLocalTime(timezone.ToUniversalTime(time));
	}

	/// <summary>
	/// Returns the coordinated universal time (UTC) that corresponds to the specified local time
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	public override DateTime ToUniversalTime(DateTime time) {
	    return time.Subtract(GetUtcOffset(time));
	}

	/// <summary>
	/// The complete list of possible TimeZone instances
	/// </summary>
	public static IList Options {
	    get { return OPTIONS; }
	}

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (code == null || !(o is TimeZone)) {
		return false;
	    } else {
		return code.Equals(((RegionalTimeZone)o).code);
	    }
	}

	public override int GetHashCode() {
	    return code == null ? 0 : code.GetHashCode();
	}

	public override String ToString() {
	    return name;
	}



    }
}
