using System;
using System.Globalization;
using NUnit.Framework;
using Spring2.Core.Globalization;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for DaylightTimeRule
    /// </summary>
    [TestFixture]
    public class DaylightTimeRuleTest {

    	// For reference, check see the folowing:
    	// http://en.wikipedia.org/wiki/Daylight_saving_time
    	// http://www.infoplease.com/spot/daylight1.html
	//# In 2005, daylight time begins on April 3 and ends on October 30.
	//# In 2006, daylight time begins on April 2 and ends on October 29.
	//# In 2007, daylight time begins on March 11 and ends on November 4. [New law goes into effect.]
    	
	[Test]
	public void USDaylightStartDateFor2005() {
	    IDaylightTimeRule rule = DaylightTimeRule.UNITED_STATES;
	    Assert.AreEqual("United States", rule.Name);
	    DaylightTime daylightTime = rule.GetDaylightTime(2005, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2005,4,3, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2005,10,30, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}
    	
	[Test]
	public void USDaylightStartDateFor2006() {
	    IDaylightTimeRule rule = DaylightTimeRule.UNITED_STATES;
	    Assert.AreEqual("United States", rule.Name);
	    DaylightTime daylightTime = rule.GetDaylightTime(2006, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2006,4,2, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2006,10,29, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}

	[Test]
	public void USDaylightStartDateFor2007() {
	    IDaylightTimeRule rule = DaylightTimeRule.UNITED_STATES;
	    Assert.AreEqual("United States", rule.Name);		
	    DaylightTime daylightTime = rule.GetDaylightTime(2007, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2007,3,11, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2007,11,4, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}

	[Test]
	public void NorthAmericaDaylightStartDateFor2005() {
	    IDaylightTimeRule rule = DaylightTimeRule.NORTH_AMERICA;
	    Assert.AreEqual("North America", rule.Name);
	    DaylightTime daylightTime = rule.GetDaylightTime(2005, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2005,4,3, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2005,10,30, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}
    	
	[Test]
	public void NorthAmericaDaylightStartDateFor2006() {
	    IDaylightTimeRule rule = DaylightTimeRule.NORTH_AMERICA;
	    Assert.AreEqual("North America", rule.Name);
	    DaylightTime daylightTime = rule.GetDaylightTime(2006, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2006,4,2, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2006,10,29, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}

	[Test]
	public void NorthAmericaDaylightStartDateFor2007() {
	    IDaylightTimeRule rule = DaylightTimeRule.NORTH_AMERICA;
	    Assert.AreEqual("North America", rule.Name);
	    DaylightTime daylightTime = rule.GetDaylightTime(2007, new TimeSpan(0, 0, 0));
	    Assert.AreEqual(new DateTime(2007,4,1, 2, 0, 0), daylightTime.Start);
	    Assert.AreEqual(new DateTime(2007,10,28, 2, 0, 0), daylightTime.End);
	    Assert.AreEqual(new TimeSpan(1,0,0), daylightTime.Delta);
	}
    	
    	[Test]
	public void UnknownXmlDaylightTimeRule() {
	    try {
	    	IDaylightTimeRule rule = new XmlDaylightTimeRule("foo");
	    	Assert.Fail();
	    } catch (ArgumentOutOfRangeException) {
	    	// this is expected
	    }
    	}

	[Test]
	public void UnknownDaylightTimeRuleYear() {
	    IDaylightTimeRule rule = new XmlDaylightTimeRule("Test");
	    try {
	    	DaylightTime daylightTime = rule.GetDaylightTime(1899, new TimeSpan(0, 0, 0));
	    } catch (ArgumentOutOfRangeException) {
		// this is expected
	    }
	}
    
    }
}
