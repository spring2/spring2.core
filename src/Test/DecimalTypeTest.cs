using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class DecimalTypeTest {

	[Test]
	public void Format() {
	    Assert.AreEqual("3.00", new DecimalType(3).ToString("F2"));
	    Assert.AreEqual("6.60 %", new DecimalType(0.066).ToString("P2"));
	}
    }
}
