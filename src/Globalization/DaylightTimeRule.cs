using System;
using System.Collections;
using System.Globalization;

namespace Spring2.Core.Globalization {

    /// <summary>
    /// Represents the rules that are used to determine the actual DaylightTime for a given year
    /// </summary>
    public class DaylightTimeRule {

	public static readonly DaylightTimeRule NORTH_AMERICA = new DaylightTimeRule(WeekOfMonth.First, DayOfWeek.Sunday, 4, WeekOfMonth.Last, DayOfWeek.Sunday, 10, 2, false, new TimeSpan(1,0,0));
	public static readonly DaylightTimeRule EUROPE = new DaylightTimeRule(WeekOfMonth.Last, DayOfWeek.Sunday, 3, WeekOfMonth.Last, DayOfWeek.Sunday, 10, 1, true, new TimeSpan(1,0,0));

	private WeekOfMonth startWeekOfMonth;
	private DayOfWeek startDayOfWeek;
	private Int32 startDay;
	private Int32 startMonth;

	private WeekOfMonth endWeekOfMonth;
	private DayOfWeek endDayOfWeek;
	private Int32 endDay;
	private Int32 endMonth;

	private Int32 hourOfDay;
	private Boolean utcHourOfDay = false;
	private TimeSpan delta = TimeSpan.Zero;

	private Hashtable years = new Hashtable();

	private DaylightTimeRule() {
	}

	private DaylightTimeRule(WeekOfMonth startWeekOfMonth, DayOfWeek startDayOfWeek, Int32 startMonth, WeekOfMonth endWeekOfMonth, DayOfWeek endDayOfWeek, Int32 endMonth, Int32 hourOfDay, Boolean utcHourOfDay, TimeSpan delta) {
	    this.startWeekOfMonth = startWeekOfMonth;
	    this.startDayOfWeek = startDayOfWeek;
	    this.startMonth = startMonth;

	    this.endWeekOfMonth = endWeekOfMonth;
	    this.endDayOfWeek = endDayOfWeek;
	    this.endMonth = endMonth;

	    this.hourOfDay = hourOfDay;
	    this.utcHourOfDay = utcHourOfDay;

	    this.delta = delta;
	}

	/// <summary>
	/// Get the DaylightTime for the year specified, adjusted by UTC offset if required (by a RegionalTimeZone)
	/// </summary>
	/// <param name="year"></param>
	/// <param name="utcOffset"></param>
	/// <returns></returns>
	public DaylightTime GetDaylightTime(Int32 year, TimeSpan utcOffset) {
	    DaylightTime daylight = (DaylightTime)years[year];

	    // if not already cached, figure it out
	    if (daylight==null) {
		DateTime start = CalculateTime(year, startWeekOfMonth, startDayOfWeek, startMonth);
		DateTime end = CalculateTime(year, endWeekOfMonth, endDayOfWeek, endMonth);
		daylight = new DaylightTime(start, end, delta);
		years[year]=daylight;
	    }

	    // if change hour is based on a UTC hour, offset by UTC offset
	    if (utcHourOfDay) {
		return new DaylightTime(daylight.Start.Subtract(utcOffset), daylight.End.Subtract(utcOffset), daylight.Delta);
	    } else {
		return daylight;
	    }

	}

	private DateTime CalculateTime(Int32 year, WeekOfMonth weekOfMonth, DayOfWeek dayOfWeek, Int32 month) {
	    DateTime time = new DateTime(year, month, 1, hourOfDay, 0, 0, 0);

	    Boolean found = false;
	    Int32 occurrence = 0;
	    DateTime last = time;
	    while (!found && time.Month==month) {
		if (time.DayOfWeek == dayOfWeek) {
		    occurrence += 1;
		    last = time;
		}
		if (occurrence == (Int32)weekOfMonth) {
		    return time;
		}
		time = time.AddDays(1);
	    }

	    if (weekOfMonth.Equals(WeekOfMonth.Last)) {
		return last;
	    }
	
	    // could not find the correct occurrence
	    throw new ArgumentOutOfRangeException("weekOfMonth", "Could not find the correct occurrence");
	}

    }
}
