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

    }
}
