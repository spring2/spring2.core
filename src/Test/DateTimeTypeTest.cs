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
    }
}
