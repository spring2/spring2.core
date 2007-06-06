using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class TimeTypeTest {

	[Test]
	public void CreateInstanceFromTimeSpan() {
	    TimeSpan span = new TimeSpan(1, 1, 1);
	    TimeType time = new TimeType(span);
	    Assert.AreEqual(span.Ticks, time.Ticks);
	}

	[Test]
	public void CreateInstanceFromHoursMinutesSeconds() {
	    TimeSpan span = new TimeSpan(1, 1, 1);
	    TimeType time = new TimeType(span.Hours,  span.Minutes, span.Seconds);
	    Assert.AreEqual(span.Ticks, time.Ticks);
	}

	[Test]
	public void CreateInstanceFromTicks() {
	    TimeSpan span = new TimeSpan(1, 1, 1);
	    TimeType time = new TimeType(span.Ticks);
	    Assert.AreEqual(span.Ticks, time.Ticks);
	}

	[Test]
	public void AddToTimeType() {
	    TimeType time = new TimeType(1, 1, 1);
	    time = time.Add(new TimeType(1, 1, 1));
	    Assert.AreEqual(new TimeType(2, 2, 2), time);
	}

	[Test]
	public void SubtractTimeType() {
	    TimeType time = new TimeType(2, 2, 2);
	    time = time.Subtract(new TimeType(1, 1, 1));
	    Assert.AreEqual(new TimeType(1, 1, 1), time);
	}

	[Test]
	public void ShouldBeAbleToConvertBackToTimeSpan() {
	    TimeSpan span = new TimeSpan(3, 3, 3);
	    TimeType time = new TimeType(span);
	    Assert.AreEqual(span, time.ToTimeSpan());
	}

	[Test]
	public void ShouldBeAbleToParseTimeIn24HourFormat() {
	    TimeType time = TimeType.Parse("16:07");
	    Assert.AreEqual(new TimeType(16, 7, 0), time);
	}

	[Test]
	public void ShouldBeAbleToParseTimesWithAMorPM() {
	    TimeType time = TimeType.Parse("3:09 PM");
	    Assert.AreEqual(new TimeType(15, 9, 0), time);
	}

	[Test]
	public void ShouldNotAllowTimeTypeLargerThen24Hours() {
	    try {
		TimeType time = new TimeType(24, 0, 1);
		Assert.Fail("Should not have allowed creation of TimeType greater then 24 hours");
	    } catch (ArgumentOutOfRangeException) {
		//Expected
	    }
	}

	[Test]
	public void ShouldNotAllowParseOfNewTimeTypeLargerThen24Hours() {
	    try {
		TimeType time = TimeType.Parse("24:00:01");
		Assert.Fail("Should not have allowed creation of TimeType greater then 24 hours");
	    } catch (FormatException) {
		//Expected
	    }
	}

    }
}
