using System;
using Spring2.Core.Types;
using NUnit.Framework;

namespace Spring2.Core.Test {
    /// <summary>
    /// Summary description for DateType.
    /// </summary>
    [TestFixture]
    public class DateTypeTest2 {

	[Test]
	public void ParseNoFormat() {
	    DateType d = DateType.Parse("4/5/2006 3:00 PM");
	    Assert.AreEqual(new DateTime(2006,4,5,15,0,0), d.ToDateTime());
	}

	[Test]
	public void ParseUSENFormat() {
	    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-US");
	    System.Globalization.DateTimeFormatInfo format = System.Globalization.DateTimeFormatInfo.GetInstance(culture);
	    DateType d = DateType.Parse("4/5/2006 3:00 PM", format);
	    Assert.AreEqual(new DateTime(2006,4,5,15,0,0), d.ToDateTime());

	}

	[Test]
	public void ParseENGBFormat() {
	    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-GB");
	    System.Globalization.DateTimeFormatInfo format = System.Globalization.DateTimeFormatInfo.GetInstance(culture);
	    DateType d = DateType.Parse("4/5/2006 3:00 PM", format);
	    Assert.AreEqual(new DateTime(2006,5,4,15,0,0), d.ToDateTime());
	}

	[Test]
	public void ParseNoFormatNoDate() {
	    DateType d = DateType.Parse("3:00 PM");
	    Assert.AreEqual(DateTime.Today.AddHours(15), d.ToDateTime());

	}

	[Test]
	public void ParseUSENFormatNoDate() {
	    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-US");
	    System.Globalization.DateTimeFormatInfo format = System.Globalization.DateTimeFormatInfo.GetInstance(culture);
	    DateType d = DateType.Parse("3:00 PM", format);
	    Assert.AreEqual(DateTime.Today.AddHours(15), d.ToDateTime());

	}

	[Test]
	public void ParseENGBFormatNoDate() {
	    System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("EN-GB");
	    System.Globalization.DateTimeFormatInfo format = System.Globalization.DateTimeFormatInfo.GetInstance(culture);
	    DateType d = DateType.Parse("3:00 PM", format);
	    Assert.AreEqual(DateTime.Today.AddHours(15), d.ToDateTime());
	}
    
    }
}
