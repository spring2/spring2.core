using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DateTypeTest {

	[Test]
	public void TestSameDateAs() {
	    Assertion.Assert("Should not be equal.", !DateType.Today.Equals(DateType.Now));
	    Assertion.Assert("Should have same dates.", DateType.Today.SameDayAs(DateType.Now));
	    Assertion.Assert("Should not have same date as UNSET.", !DateType.Today.SameDayAs(DateType.UNSET));
	    Assertion.Assert("Should not have same date as DEFAULT.", !DateType.Today.SameDayAs(DateType.DEFAULT));
	}
    }
}
