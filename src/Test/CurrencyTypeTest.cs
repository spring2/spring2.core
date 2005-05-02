using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class CurrencyTypeTest {

	[Test]
	public void ToStringTest() {
	    CurrencyType currency = new CurrencyType(1.45);
	    Assert.AreEqual("$1.45", currency.ToString());
	}
    }
}
