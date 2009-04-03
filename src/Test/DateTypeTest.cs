using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DateTypeTest {

	[Test]
	public void TestSameDateAs() {
	    // TODO: changed from DateType to DateTimeType -- what should be done for DateType
	    Assert.IsTrue(!DateTimeType.Today.Equals(DateTimeType.Now), "Should not be equal.");
	    Assert.IsTrue(DateTimeType.Today.SameDayAs(DateTimeType.Now), "Should have same dates.");
	    Assert.IsTrue(!DateTimeType.Today.SameDayAs(DateTimeType.UNSET), "Should not have same date as UNSET.");
	    Assert.IsTrue(!DateTimeType.Today.SameDayAs(DateTimeType.DEFAULT), "Should not have same date as DEFAULT.");
	}
    }
}
