using System;
using System.Globalization;
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
    	
	[Test]
	public void ToStringTest() {
	    DecimalType currency = new DecimalType(1.45);
	    Assert.AreEqual("1.45", currency.ToString());
	}

	[Test]
	public void ParseWithFormatProvider() {
	    CultureInfo culture = new CultureInfo("en-GB");
	    DecimalType currency = new DecimalType(5);
	    String s = currency.ToString(culture.NumberFormat);
	    Assert.AreEqual("5", s);
	    DecimalType c2 = DecimalType.Parse(s, culture.NumberFormat);
	    Assert.AreEqual(currency, c2);
	}

	#region Double
	[Test]
	public void ImplicitConvertionFromDouble() {
	    Double doubleDollars = 12.34;
	    DecimalType dollars = doubleDollars;
	    Assert.AreEqual(new DecimalType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByDouble() {
	    Double doubleDollars = 12.34;
	    DecimalType dollars1 = 100;
	    DecimalType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new DecimalType(1234), dollars2);
	    Assert.AreEqual(1234D, dollars2.ToDouble());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByDouble() {
	    Double doubleDollars = 12.34;
	    DecimalType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new DecimalType(1234), dollars);
	    Assert.AreEqual(1234D, dollars.ToDouble());    		
	}
	#endregion
    	
	#region Decimal
	[Test]
	public void ImplicitConvertionFromDecimal() {
	    Decimal doubleDollars = 12.34M;
	    DecimalType dollars = doubleDollars;
	    Assert.AreEqual(new DecimalType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByDecimal() {
	    Decimal doubleDollars = 12.34M;
	    DecimalType dollars1 = 100;
	    DecimalType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new DecimalType(1234), dollars2);
	    Assert.AreEqual(1234M, dollars2.ToDouble());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByDecimal() {
	    Decimal doubleDollars = 12.34M;
	    DecimalType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new DecimalType(1234), dollars);
	    Assert.AreEqual(1234M, dollars.ToDouble());    		
	}
	#endregion
    	
	#region Int32
	[Test]
	public void ImplicitConvertionFromInt32() {
	    Int32 doubleDollars = 12;
	    DecimalType dollars = doubleDollars;
	    Assert.AreEqual(new DecimalType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByInt32() {
	    Int32 doubleDollars = 12;
	    DecimalType dollars1 = 100;
	    DecimalType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new DecimalType(1200), dollars2);
	    Assert.AreEqual(1200, dollars2.ToInt32());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByInt32() {
	    Int32 doubleDollars = 12;
	    DecimalType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new DecimalType(1200), dollars);
	    Assert.AreEqual(1200, dollars.ToInt32());    		
	}

	[Test]
	public void ToInt32ShouldReturnAnInt32() {
	    DecimalType newDecimal = 100;
	    Int32 integer = newDecimal.ToInt32();
	    Assert.AreEqual(100, integer);
	}
	#endregion

	#region Int64
	[Test]
	public void ImplicitConvertionFromInt64() {
	    Int64 doubleDollars = 12;
	    DecimalType dollars = doubleDollars;
	    Assert.AreEqual(new DecimalType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByInt64() {
	    Int64 doubleDollars = 12;
	    DecimalType dollars1 = 100;
	    DecimalType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new DecimalType(1200), dollars2);
	    Assert.AreEqual(1200, dollars2.ToInt64());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByInt64() {
	    Int64 doubleDollars = 12;
	    DecimalType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new DecimalType(1200), dollars);
	    Assert.AreEqual(1200, dollars.ToInt64());    		
	}
	#endregion
    	
    }
}
