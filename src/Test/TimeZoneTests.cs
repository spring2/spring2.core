using System;
using System.Globalization;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Globalization;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for TimeZoneTests
    /// </summary>
    [TestFixture]
    public class TimeZoneTests {

	[Test]
	public void GetTimeZone() {
	    TimeZone mt = RegionalTimeZone.GetInstance("T-US");
	    TimeZone et = RegionalTimeZone.GetInstance("R-US");
	    TimeZone we = RegionalTimeZone.GetInstance("Z-EU");
	    TimeZone ce = RegionalTimeZone.GetInstance("A-EU");

	    // test DaylightChanges
	    Assertion.AssertEquals(new DateTime(2002,4,7,2,0,0,0), mt.GetDaylightChanges(2002).Start);
	    Assertion.AssertEquals(new DateTime(2002,10,27,2,0,0,0), mt.GetDaylightChanges(2002).End);
	    Assertion.AssertEquals(new DateTime(2002,4,7,2,0,0,0), et.GetDaylightChanges(2002).Start);
	    Assertion.AssertEquals(new DateTime(2002,10,27,2,0,0,0), et.GetDaylightChanges(2002).End);
	    Assertion.AssertEquals(new DateTime(2002,3,31,1,0,0,0), we.GetDaylightChanges(2002).Start);
	    Assertion.AssertEquals(new DateTime(2002,10,27,1,0,0,0), we.GetDaylightChanges(2002).End);
	    Assertion.AssertEquals(new DateTime(2002,3,31,0,0,0,0), ce.GetDaylightChanges(2002).Start);
	    Assertion.AssertEquals(new DateTime(2002,10,27,0,0,0,0), ce.GetDaylightChanges(2002).End);

	    // test IsDaylightSavingTime
	    Assertion.Assert(!mt.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.Assert(mt.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.Assert(!et.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.Assert(et.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.Assert(!we.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.Assert(we.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.Assert(!ce.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.Assert(ce.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));

	    // test GetUtcOffset
	    Assertion.AssertEquals(new TimeSpan(-7,0,0), mt.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(-6,0,0), mt.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(-5,0,0), et.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(-4,0,0), et.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(0,0,0), we.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(1,0,0), we.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(1,0,0), ce.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assertion.AssertEquals(new TimeSpan(2,0,0), ce.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));

	    // to local and universal time
	    DateTime time = new DateTime(2002,11,13,20,28,49,0);
	    Assertion.AssertEquals(new DateTime(2002,11,14,3,28,49,0), mt.ToUniversalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,14,1,28,49,0), et.ToUniversalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,13,20,28,49,0), we.ToUniversalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,13,19,28,49,0), ce.ToUniversalTime(time));

	    Assertion.AssertEquals(new DateTime(2002,11,13,13,28,49,0), mt.ToLocalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,13,15,28,49,0), et.ToLocalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,13,20,28,49,0), we.ToLocalTime(time));
	    Assertion.AssertEquals(new DateTime(2002,11,13,21,28,49,0), ce.ToLocalTime(time));

	    // to local from another time zone
	    Assertion.AssertEquals(new DateTime(2002,11,13,22,28,49,0), ((RegionalTimeZone)et).ToLocalTime(time, mt));
	    Assertion.AssertEquals(new DateTime(2002,11,13,18,28,49,0), ((RegionalTimeZone)mt).ToLocalTime(time, et));
	}

	
    }
}
