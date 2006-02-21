using System;
using System.Globalization;
using System.Reflection;
using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// CurrencyType tests
    /// </summary>
    [TestFixture]
    public class CurrencyTypeTest2 {

	[Test]
	public void ParseWithFormatProvider() {
	    CultureInfo culture = new CultureInfo("en-GB");
	    CurrencyType currency = new CurrencyType(5);
	    String s = currency.ToString(culture.NumberFormat);
	    Assert.AreEqual("£5.00", s);
	    CurrencyType c2 = CurrencyType.Parse(s, culture.NumberFormat);
	    Assert.AreEqual(currency, c2);
	}

    }
}
