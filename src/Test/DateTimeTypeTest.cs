using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DateTimeTypeTest {

	[Test]
	public void TestCompareNoSeconds() {
	    DateTimeType d1 = DateTimeType.Now;
	    DateTimeType d2 = d1;
	    Assert.AreEqual(0, d1.CompareNoSeconds(d2));
	    d2 = DateTimeType.Now.AddSeconds(50);
	    Assert.AreEqual(0, d1.CompareNoSeconds(d2));
	    d2 = DateTimeType.Now.AddSeconds(120);
	    Assert.AreEqual(-1, d1.CompareNoSeconds(d2));
	    d2 = DateTimeType.Now.AddSeconds(-240);
	    Assert.AreEqual(1, d1.CompareNoSeconds(d2));
	}

	[Test]
	public void ShouldBeAbleToCreateWithTicks() {
	    DateTimeType date = new DateTimeType(630823790450060000);
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(6, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDay() {
	    DateTimeType date = new DateTimeType(2000, 1, 2);
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(0, date.Hour);
	    Assert.AreEqual(0, date.Minute);
	    Assert.AreEqual(0, date.Second);
	    Assert.AreEqual(0, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDayCalendar() {
	    DateTimeType date = new DateTimeType(2000, 1, 2, new System.Globalization.GregorianCalendar());
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(0, date.Hour);
	    Assert.AreEqual(0, date.Minute);
	    Assert.AreEqual(0, date.Second);
	    Assert.AreEqual(0, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDayHourMinutesSeconds() {
	    DateTimeType date = new DateTimeType(2000, 1, 2, 3, 4, 5);
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(0, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDayHourMinutesSecondsCalendar() {
	    DateTimeType date = new DateTimeType(2000, 1, 2, 3, 4, 5, new System.Globalization.GregorianCalendar());
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(0, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDayHourMinutesSecondsMilliseconds() {
	    DateTimeType date = new DateTimeType(2000, 1, 2, 3, 4, 5, 6);
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(6, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateWithYearMonthDayHourMinutesSecondsMillisecondsCalendar() {
	    DateTimeType date = new DateTimeType(2000, 1, 2, 3, 4, 5, 6, new System.Globalization.GregorianCalendar());
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(6, date.Millisecond);
	}

	[Test]
	public void ShouldBeAbleToCreateFromDateTime() {
	    DateTimeType date = new DateTimeType(new DateTime(2000, 1, 2, 3, 4, 5, 6));
	    Assert.AreEqual(2000, date.Year);
	    Assert.AreEqual(1, date.Month);
	    Assert.AreEqual(2, date.Day);
	    Assert.AreEqual(3, date.Hour);
	    Assert.AreEqual(4, date.Minute);
	    Assert.AreEqual(5, date.Second);
	    Assert.AreEqual(6, date.Millisecond);
	}
    	
    	[Test]
	public void ShouldCalculateBeginningOfDay() {
    	    DateTimeType date = DateTimeType.Now;
    	    Assert.AreEqual(DateTimeType.Today, date.BeginningOfDay);
    	}

    	[Test]
	public void ShouldCalculateEndOfDay() {
	    DateTimeType date = DateTimeType.Now;
    	    DateTimeType endOfDay = DateTimeType.Today.AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(997);
	    Assert.AreEqual(endOfDay, date.EndOfDay);
	}
		[Test]
	public void ShoulCalculateEndOfMonth() 
	{
		DateTimeType date = new DateTimeType(2007, 1, 1);
		DateTimeType endOfMonth = new DateTimeType(date.Year, date.Month, DateTimeType.DaysInMonth(date.Year, date.Month));
		Assert.AreEqual(endOfMonth, date.EndOfMonth);
	}
    }
}
