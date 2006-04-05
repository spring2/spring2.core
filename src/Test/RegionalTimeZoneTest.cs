using System;
using System.Globalization;
using NUnit.Framework;
using Spring2.Core.Globalization;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class RegionalTimeZoneTest {

	[Test]
	public void ParseSystemTimeFromLocalTime() {
	    DateTime date = new DateTime(2006, 4, 1, 20, 0, 0);
	    Assert.AreEqual(date, ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.R_US, "en-US"));

	    date = new DateTime(2006, 4, 2, 2, 0, 0);
	    Assert.AreEqual(date, ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.R_US, "en-US"));

	    date = new DateTime(2006, 4, 1, 21, 0, 0);
	    Assert.AreEqual(date, ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.R_US, "en-US"));
	}

	[Test]
	public void ParseSystemTimeFromLocalTime_BehindLocal() {
	    DateTime date = new DateTime(2006, 4, 1, 20, 0, 0);
	    Assert.AreEqual(date.AddHours(2), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.T_US, "en-US"));

	    date = new DateTime(2006, 4, 2, 2, 0, 0);
	    Assert.AreEqual(date.AddHours(2), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.T_US, "en-US"));

	    date = new DateTime(2006, 4, 1, 21, 0, 0);
	    Assert.AreEqual(date.AddHours(2), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.R_US, RegionalTimeZone.T_US, "en-US"));
	}

	[Test]
	public void ParseSystemTimeFromLocalTime_AheadOfLocal() {
	    DateTime date = new DateTime(2006, 4, 1, 20, 0, 0);
	    Assert.AreEqual(date.AddHours(-1), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.S_US, RegionalTimeZone.R_US, "en-US"));

	    date = new DateTime(2006, 4, 2, 2, 0, 0);
	    Assert.AreEqual(date.AddHours(-1), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.S_US, RegionalTimeZone.R_US, "en-US"));

	    date = new DateTime(2006, 4, 1, 21, 0, 0);
	    Assert.AreEqual(date.AddHours(-1), ParseSystemTimeFromLocalTime(date.ToString(), RegionalTimeZone.S_US, RegionalTimeZone.R_US, "en-US"));
	}

	private DateTime ParseSystemTimeFromLocalTime(String datetime, RegionalTimeZone systemTimeZone, RegionalTimeZone localTimeZone, String cultureCode) {
	    CultureInfo culture = new CultureInfo(cultureCode);
	    DateTimeFormatInfo format = DateTimeFormatInfo.GetInstance(culture);
	    DateTime localTime = DateTime.Parse(datetime, format);
	    DateTime systemTime = systemTimeZone.ToLocalTime(localTime, localTimeZone);
	    return systemTime;
	}

    }
}
