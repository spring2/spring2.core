using System;
using System.Globalization;
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

	[Test]
	public void ParseWithFormatProvider() {
	    CultureInfo culture = new CultureInfo("en-GB");
	    CurrencyType currency = new CurrencyType(5);
	    String s = currency.ToString(culture.NumberFormat);
	    Assert.AreEqual("£5.00", s);
	    CurrencyType c2 = CurrencyType.Parse(s, culture.NumberFormat);
	    Assert.AreEqual(currency, c2);
	}

	#region Double
    	[Test]
	public void ImplicitConvertionFromDouble() {
    	    Double doubleDollars = 12.34;
    	    CurrencyType dollars = doubleDollars;
    	    Assert.AreEqual(new CurrencyType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
    	}

	[Test]
	public void MultiplyByDouble() {
	    Double doubleDollars = 12.34;
	    CurrencyType dollars1 = 100;
	    CurrencyType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars2);
	    Assert.AreEqual(1234D, dollars2.ToDouble());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByDouble() {
	    Double doubleDollars = 12.34;
	    CurrencyType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars);
	    Assert.AreEqual(1234D, dollars.ToDouble());    		
	}
	#endregion
    	
	#region Decimal
	[Test]
	public void ImplicitConvertionFromDecimal() {
	    Decimal doubleDollars = 12.34M;
	    CurrencyType dollars = doubleDollars;
	    Assert.AreEqual(new CurrencyType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByDecimal() {
	    Decimal doubleDollars = 12.34M;
	    CurrencyType dollars1 = 100;
	    CurrencyType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars2);
	    Assert.AreEqual(1234M, dollars2.ToDouble());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByDecimal() {
	    Decimal doubleDollars = 12.34M;
	    CurrencyType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars);
	    Assert.AreEqual(1234M, dollars.ToDouble());    		
	}
	#endregion
    	
	#region Int32
	[Test]
	public void ImplicitConvertionFromInt32() {
	    Int32 doubleDollars = 12;
	    CurrencyType dollars = doubleDollars;
	    Assert.AreEqual(new CurrencyType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByInt32() {
	    Int32 doubleDollars = 12;
	    CurrencyType dollars1 = 100;
	    CurrencyType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new CurrencyType(1200), dollars2);
	    Assert.AreEqual(1200, dollars2.ToInt32());    		

	    dollars2 = doubleDollars * dollars1;
	    Assert.AreEqual(new CurrencyType(1200), dollars2);
	    Assert.AreEqual(1200, dollars2.ToInt32());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByInt32() {
	    Int32 doubleDollars = 12;
	    CurrencyType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new CurrencyType(1200), dollars);
	    Assert.AreEqual(1200, dollars.ToInt32());    		
	}
	#endregion

	#region Int64
	[Test]
	public void ImplicitConvertionFromInt64() {
	    Int64 doubleDollars = 12;
	    CurrencyType dollars = doubleDollars;
	    Assert.AreEqual(new CurrencyType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByInt64() {
	    Int64 doubleDollars = 12;
	    CurrencyType dollars1 = 100;
	    CurrencyType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new CurrencyType(1200), dollars2);
	    Assert.AreEqual(1200, dollars2.ToInt64());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByInt64() {
	    Int64 doubleDollars = 12;
	    CurrencyType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new CurrencyType(1200), dollars);
	    Assert.AreEqual(1200, dollars.ToInt64());    		
	}
	#endregion
    	
	#region DecimalType
	[Test]
	public void ImplicitConvertionFromDecimalType() {
	    DecimalType doubleDollars = 12.34M;
	    CurrencyType dollars = doubleDollars;
	    Assert.AreEqual(new CurrencyType(doubleDollars), dollars);
	    Assert.AreEqual(doubleDollars, dollars.ToDouble());    		
	}

	[Test]
	public void MultiplyByDecimalType() {
	    DecimalType doubleDollars = 12.34M;
	    CurrencyType dollars1 = 100;
	    CurrencyType dollars2 = dollars1 * doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars2);
	    Assert.AreEqual(1234M, dollars2.ToDouble());    		

	    dollars2 = doubleDollars * dollars1;
	    Assert.AreEqual(new CurrencyType(1234), dollars2);
	    Assert.AreEqual(1234M, dollars2.ToDouble());    		
	}
    	
	[Test]
	public void MultiplyAssignmentByDecimalType() {
	    DecimalType doubleDollars = 12.34M;
	    CurrencyType dollars = 100;
	    dollars *= doubleDollars;
	    Assert.AreEqual(new CurrencyType(1234), dollars);
	    Assert.AreEqual(1234M, dollars.ToDouble());    		
	}
	#endregion
    	
    }
}
