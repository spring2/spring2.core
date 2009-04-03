using System;
using System.Collections;
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
    	
	[Test]
	public void GetInstanceForAllOptions() {
	    ArrayList timeZones = new ArrayList();
	    timeZones.AddRange(RegionalTimeZone.Options);
	    timeZones.Add(RegionalTimeZone.AKST);
	    timeZones.Add(RegionalTimeZone.EST);
	    timeZones.Add(RegionalTimeZone.CST);
	    timeZones.Add(RegionalTimeZone.MST);
	    timeZones.Add(RegionalTimeZone.PST);
	    timeZones.Add(RegionalTimeZone.GMT);
	    timeZones.Add(RegionalTimeZone.CET);
	    timeZones.Add(RegionalTimeZone.EET);
    		
	    foreach(RegionalTimeZone timeZone in timeZones) {
		RegionalTimeZone tz = RegionalTimeZone.GetInstance(timeZone.Code);
		Assert.AreEqual(timeZone, tz);
	    	Assert.AreEqual(tz.ToString(), tz.Name);
	    }
	}
    	
    	[Test]
	public void GetInstanceForAKST() {
    	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("AKST");
    	    Assert.AreEqual(RegionalTimeZone.V_US, tz);
    	}

	[Test]
	public void GetInstanceForEST() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("EST");
	    Assert.AreEqual(RegionalTimeZone.R_US, tz);
	}

	[Test]
	public void GetInstanceForCST() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("CST");
	    Assert.AreEqual(RegionalTimeZone.S_US, tz);
	}
    
	[Test]
	public void GetInstanceForMST() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("MST");
	    Assert.AreEqual(RegionalTimeZone.T_US, tz);
	}
    
	[Test]
	public void GetInstanceForPST() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("PST");
	    Assert.AreEqual(RegionalTimeZone.U_US, tz);
	}
    
	[Test]
	public void GetInstanceForGMT() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("GMT");
	    Assert.AreEqual(RegionalTimeZone.Z_EU, tz);
	}
    
	[Test]
	public void GetInstanceForCET() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("CET");
	    Assert.AreEqual(RegionalTimeZone.A_EU, tz);
	}
    
	[Test]
	public void GetInstanceForEET() {
	    RegionalTimeZone tz = RegionalTimeZone.GetInstance("EET");
	    Assert.AreEqual(RegionalTimeZone.B_EU, tz);
	}
    	
	[Test]
	public void GetInstanceForUnknown() {
	    try {
		RegionalTimeZone tz = RegionalTimeZone.GetInstance("foo");
		Assert.Fail();
	    } catch (ArgumentOutOfRangeException) {
	    	// this is expected
	    }
	}
    	
    	[Test]
	public void GetDaylightChangesForNoDaylightTimeRule() {
    	    Assert.IsNull(RegionalTimeZone.Z.GetDaylightChanges(DateTime.Today.Year));
    	}

	[Test]
	public void IsNotDaylightSavingsTimeForNoDaylightTimeRule() {
	    Assert.IsFalse(RegionalTimeZone.Z.IsDaylightSavingTime(DateTime.Now));
	}
    	
    	[Test]
	public void DaylightName() {
    	    Assert.AreEqual(RegionalTimeZone.PST.StandardName, RegionalTimeZone.PST.DaylightName);
    	    Assert.AreEqual(String.Empty, RegionalTimeZone.Z.DaylightName);
    	}
    	
    	[Test]
	public void HashcodeIsSameAsCodesHashCode() {
    	    Assert.AreEqual(RegionalTimeZone.Z.Code.GetHashCode(), RegionalTimeZone.Z.GetHashCode());
    	}
    	
    	[Test]
	public void SameInstanceEquals() {
    	    Assert.IsTrue(RegionalTimeZone.Z.Equals(RegionalTimeZone.Z));	
    	}
    	
	[Test]
	public void DifferentInstancesDoNotEqual() {
	    Assert.IsFalse(RegionalTimeZone.Z.Equals(RegionalTimeZone.Y));	
	}

	[Test]
	public void DaylightTimeRuleDoesNotEqualRegionalTimeZone() {
	    Assert.IsFalse(RegionalTimeZone.Z.Equals(DaylightTimeRule.UNITED_STATES));	
	}
    }
}
