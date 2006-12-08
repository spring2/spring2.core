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

    }
}
