using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DateTypeTest {

	[Test]
	public void TestSameDateAs() {
	    Assert.IsTrue(!DateType.Today.Equals(DateType.Now), "Should not be equal.");
	    Assert.IsTrue(DateType.Today.SameDayAs(DateType.Now), "Should have same dates.");
	    Assert.IsTrue(!DateType.Today.SameDayAs(DateType.UNSET), "Should not have same date as UNSET.");
	    Assert.IsTrue(!DateType.Today.SameDayAs(DateType.DEFAULT), "Should not have same date as DEFAULT.");
	}
    }
}
