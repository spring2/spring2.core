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
	    Assert.AreEqual(new DateTime(2002,4,7,2,0,0,0), mt.GetDaylightChanges(2002).Start);
	    Assert.AreEqual(new DateTime(2002,10,27,2,0,0,0), mt.GetDaylightChanges(2002).End);
	    Assert.AreEqual(new DateTime(2002,4,7,2,0,0,0), et.GetDaylightChanges(2002).Start);
	    Assert.AreEqual(new DateTime(2002,10,27,2,0,0,0), et.GetDaylightChanges(2002).End);
	    Assert.AreEqual(new DateTime(2002,3,31,1,0,0,0), we.GetDaylightChanges(2002).Start);
	    Assert.AreEqual(new DateTime(2002,10,27,1,0,0,0), we.GetDaylightChanges(2002).End);
	    Assert.AreEqual(new DateTime(2002,3,31,0,0,0,0), ce.GetDaylightChanges(2002).Start);
	    Assert.AreEqual(new DateTime(2002,10,27,0,0,0,0), ce.GetDaylightChanges(2002).End);

	    // test IsDaylightSavingTime
	    Assert.IsTrue(!mt.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.IsTrue(mt.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.IsTrue(!et.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.IsTrue(et.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.IsTrue(!we.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.IsTrue(we.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.IsTrue(!ce.IsDaylightSavingTime(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.IsTrue(ce.IsDaylightSavingTime(new DateTime(2002,8,13,7,36,16,0)));

	    // test GetUtcOffset
	    Assert.AreEqual(new TimeSpan(-7,0,0), mt.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(-6,0,0), mt.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(-5,0,0), et.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(-4,0,0), et.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(0,0,0), we.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(1,0,0), we.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(1,0,0), ce.GetUtcOffset(new DateTime(2002,11,13,7,36,16,0)));
	    Assert.AreEqual(new TimeSpan(2,0,0), ce.GetUtcOffset(new DateTime(2002,8,13,7,36,16,0)));

	    // to local and universal time
	    DateTime time = new DateTime(2002,11,13,20,28,49,0);
	    Assert.AreEqual(new DateTime(2002,11,14,3,28,49,0), mt.ToUniversalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,14,1,28,49,0), et.ToUniversalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,13,20,28,49,0), we.ToUniversalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,13,19,28,49,0), ce.ToUniversalTime(time));

	    Assert.AreEqual(new DateTime(2002,11,13,13,28,49,0), mt.ToLocalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,13,15,28,49,0), et.ToLocalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,13,20,28,49,0), we.ToLocalTime(time));
	    Assert.AreEqual(new DateTime(2002,11,13,21,28,49,0), ce.ToLocalTime(time));

	    // to local from another time zone
	    Assert.AreEqual(new DateTime(2002,11,13,22,28,49,0), ((RegionalTimeZone)et).ToLocalTime(time, mt));
	    Assert.AreEqual(new DateTime(2002,11,13,18,28,49,0), ((RegionalTimeZone)mt).ToLocalTime(time, et));
	}

	
    }
}
