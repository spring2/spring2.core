using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {
    /// <summary>
    /// Summary description for DataTypeTest.
    /// </summary>
    [TestFixture]
    public class DataTypeTest {

	[Test]
	public void TestParse() {

	    // Currency type tests.
	    CurrencyType c1 = new CurrencyType(100);
	    String s1 = c1.ToString();
	    CurrencyType c2 = CurrencyType.Parse(s1);
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("100");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("$100");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("100.");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("$100.");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("$100.  ");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("   $100.  ");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("   $100.000  ");
	    Assertion.AssertEquals(c1, c2);
	    c2 = CurrencyType.Parse("   $ 100.000  ");
	    Assertion.AssertEquals(c1, c2);

	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
	    s1 = c1.ToString();
	    c2 = CurrencyType.Parse(s1);
	    Assertion.AssertEquals(c1, c2);

	    // Integer type tests.
	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	    IntegerType integer1 = new IntegerType(3000);
	    String IntegerString = integer1.ToString("N");
	    IntegerType integer2 = IntegerType.Parse(IntegerString);
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse("3000");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse("3,000");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.00");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.000");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse(" 3000 ");
	    Assertion.AssertEquals(integer1, integer2);
	    integer2 = IntegerType.Parse(" 3,000.000 ");
	    Assertion.AssertEquals(integer1, integer2);

	    // Id type tests.
	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	    IdType id1 = new IdType(3000);
	    String idString = id1.ToString();
	    IdType id2 = IdType.Parse(idString);
	    Assertion.AssertEquals(id1, id2);
	    id2 = IdType.Parse("3000");
	    Assertion.AssertEquals(id1, id2);
	    id2 = IdType.Parse(" 3000 ");
	    Assertion.AssertEquals(id1, id2);

	    // DateType tests.
	    DateType date1 = new DateType(new DateTime(2001, 11, 1));
	    String dateString = date1.ToString();
	    DateType date2 = DateType.Parse(dateString);
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("11/01/01");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("11/01/2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("1 Nov 2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("1 November 2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("November 1, 2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("Nov, 2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("November, 2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("11-01-2001");
	    Assertion.AssertEquals(date1, date2);
	    date2 = DateType.Parse("11-01-01");
	    Assertion.AssertEquals(date1, date2);
	}

	[Test]
	public void TestEquals() {

	    // BooleanType tests.
	    Assertion.AssertEquals(BooleanType.UNSET, BooleanType.UNSET);
	    Assertion.AssertEquals(BooleanType.DEFAULT, BooleanType.DEFAULT);
	    Assertion.AssertEquals(BooleanType.TRUE, BooleanType.TRUE);
	    Assertion.AssertEquals(BooleanType.FALSE, BooleanType.FALSE);

	    Assertion.Assert(!BooleanType.UNSET.Equals(BooleanType.DEFAULT));
	    Assertion.Assert(!BooleanType.UNSET.Equals(BooleanType.TRUE));
	    Assertion.Assert(!BooleanType.UNSET.Equals(BooleanType.FALSE));

	    Assertion.Assert(!BooleanType.DEFAULT.Equals(BooleanType.UNSET));
	    Assertion.Assert(!BooleanType.DEFAULT.Equals(BooleanType.TRUE));
	    Assertion.Assert(!BooleanType.DEFAULT.Equals(BooleanType.FALSE));

	    Assertion.Assert(!BooleanType.TRUE.Equals(BooleanType.UNSET));
	    Assertion.Assert(!BooleanType.TRUE.Equals(BooleanType.DEFAULT));
	    Assertion.Assert(!BooleanType.TRUE.Equals(BooleanType.FALSE));

	    Assertion.Assert(!BooleanType.FALSE.Equals(BooleanType.UNSET));
	    Assertion.Assert(!BooleanType.FALSE.Equals(BooleanType.DEFAULT));
	    Assertion.Assert(!BooleanType.FALSE.Equals(BooleanType.TRUE));

	    Assertion.Assert(!BooleanType.TRUE.Equals(null));
	    Assertion.Assert(!BooleanType.TRUE.Equals(new DateType()));

	    // Currency type tests.
	    CurrencyType c1 = new CurrencyType(100);
	    CurrencyType c2 = new CurrencyType(100);
	    CurrencyType c3 = new CurrencyType(50);

	    Assertion.AssertEquals(CurrencyType.UNSET, CurrencyType.UNSET);
	    Assertion.AssertEquals(CurrencyType.DEFAULT, CurrencyType.DEFAULT);
	    Assertion.Assert(!CurrencyType.UNSET.Equals(CurrencyType.DEFAULT));
	    Assertion.Assert(!CurrencyType.DEFAULT.Equals(CurrencyType.UNSET));

	    Assertion.Assert(!c1.Equals(CurrencyType.UNSET));
	    Assertion.Assert(!c1.Equals(CurrencyType.DEFAULT));
	    Assertion.Assert(!CurrencyType.UNSET.Equals(c1));
	    Assertion.Assert(!CurrencyType.DEFAULT.Equals(c1));
	    Assertion.AssertEquals(c1, c1);
	    Assertion.AssertEquals(c1, c2);
	    Assertion.AssertEquals(c2, c1);
	    Assertion.Assert(!c1.Equals(c3));
	    Assertion.Assert(!c3.Equals(c1));

	    Assertion.Assert(!c1.Equals(null));

	    // Quantity type tests.
	    QuantityType q1 = new QuantityType(100);
	    QuantityType q2 = new QuantityType(100);
	    QuantityType q3 = new QuantityType(50);

	    Assertion.AssertEquals(QuantityType.UNSET, QuantityType.UNSET);
	    Assertion.AssertEquals(QuantityType.DEFAULT, QuantityType.DEFAULT);
	    Assertion.Assert(!QuantityType.UNSET.Equals(QuantityType.DEFAULT));
	    Assertion.Assert(!QuantityType.DEFAULT.Equals(QuantityType.UNSET));

	    Assertion.Assert(!q1.Equals(QuantityType.UNSET));
	    Assertion.Assert(!q1.Equals(QuantityType.DEFAULT));
	    Assertion.Assert(!QuantityType.UNSET.Equals(q1));
	    Assertion.Assert(!QuantityType.DEFAULT.Equals(q1));
	    Assertion.AssertEquals(q1, q1);
	    Assertion.AssertEquals(q1, q2);
	    Assertion.AssertEquals(q2, q1);
	    Assertion.Assert(!q1.Equals(q3));
	    Assertion.Assert(!q3.Equals(q1));

	    Assertion.Assert(!q1.Equals(null));

	    // Decimal type tests.
	    DecimalType d1 = new DecimalType(100);
	    DecimalType d2 = new DecimalType(100);
	    DecimalType d3 = new DecimalType(50);

	    Assertion.AssertEquals(DecimalType.UNSET, DecimalType.UNSET);
	    Assertion.AssertEquals(DecimalType.DEFAULT, DecimalType.DEFAULT);
	    Assertion.Assert(!DecimalType.UNSET.Equals(DecimalType.DEFAULT));
	    Assertion.Assert(!DecimalType.DEFAULT.Equals(DecimalType.UNSET));

	    Assertion.Assert(!d1.Equals(DecimalType.UNSET));
	    Assertion.Assert(!d1.Equals(DecimalType.DEFAULT));
	    Assertion.Assert(!DecimalType.UNSET.Equals(d1));
	    Assertion.Assert(!DecimalType.DEFAULT.Equals(d1));
	    Assertion.AssertEquals(d1, d1);
	    Assertion.AssertEquals(d1, d2);
	    Assertion.AssertEquals(d2, d1);
	    Assertion.Assert(!d1.Equals(d3));
	    Assertion.Assert(!d3.Equals(d1));

	    Assertion.Assert(!d1.Equals(null));

	    // Compare decimal to currency.
	    Assertion.Assert(!CurrencyType.UNSET.Equals(DecimalType.UNSET));
	    Assertion.Assert(!CurrencyType.DEFAULT.Equals(DecimalType.DEFAULT));
	    Assertion.Assert(!c1.Equals(d1));

	    // Compare decimal to quantity.
	    Assertion.Assert(!QuantityType.UNSET.Equals(DecimalType.UNSET));
	    Assertion.Assert(!QuantityType.DEFAULT.Equals(DecimalType.DEFAULT));
	    Assertion.Assert(!q1.Equals(d1));

	    // Compare currency to quantity.
	    Assertion.Assert(!QuantityType.UNSET.Equals(CurrencyType.UNSET));
	    Assertion.Assert(!QuantityType.DEFAULT.Equals(CurrencyType.DEFAULT));
	    Assertion.Assert(!q1.Equals(c1));

	    // Id type tests.
	    IdType id1 = new IdType(100);
	    IdType id2 = new IdType(100);
	    IdType id3 = new IdType(50);

	    Assertion.AssertEquals(IdType.UNSET, IdType.UNSET);
	    Assertion.AssertEquals(IdType.DEFAULT, IdType.DEFAULT);
	    Assertion.Assert(!IdType.UNSET.Equals(IdType.DEFAULT));
	    Assertion.Assert(!IdType.DEFAULT.Equals(IdType.UNSET));

	    Assertion.Assert(!id1.Equals(IdType.UNSET));
	    Assertion.Assert(!id1.Equals(IdType.DEFAULT));
	    Assertion.Assert(!IdType.UNSET.Equals(id1));
	    Assertion.Assert(!IdType.DEFAULT.Equals(id1));
	    Assertion.AssertEquals(id1, id1);
	    Assertion.AssertEquals(id1, id2);
	    Assertion.AssertEquals(id2, id1);
	    Assertion.Assert(!id1.Equals(id3));
	    Assertion.Assert(!id3.Equals(id1));

	    Assertion.Assert(!id1.Equals(null));

	    // Integer type tests.
	    IntegerType integer1 = new IntegerType(100);
	    IntegerType integer2 = new IntegerType(100);
	    IntegerType integer3 = new IntegerType(50);

	    Assertion.AssertEquals(IntegerType.UNSET, IntegerType.UNSET);
	    Assertion.AssertEquals(IntegerType.DEFAULT, IntegerType.DEFAULT);
	    Assertion.Assert(!IntegerType.UNSET.Equals(IntegerType.DEFAULT));
	    Assertion.Assert(!IntegerType.DEFAULT.Equals(IntegerType.UNSET));

	    Assertion.Assert(!integer1.Equals(IntegerType.UNSET));
	    Assertion.Assert(!integer1.Equals(IntegerType.DEFAULT));
	    Assertion.Assert(!IntegerType.UNSET.Equals(integer1));
	    Assertion.Assert(!IntegerType.DEFAULT.Equals(integer1));
	    Assertion.AssertEquals(integer1, integer1);
	    Assertion.AssertEquals(integer1, integer2);
	    Assertion.AssertEquals(integer2, integer1);
	    Assertion.Assert(!integer1.Equals(integer3));
	    Assertion.Assert(!integer3.Equals(integer1));

	    Assertion.Assert(!integer1.Equals(null));

	    // Compare Integer to id.
	    Assertion.Assert(!IntegerType.UNSET.Equals(IdType.UNSET));
	    Assertion.Assert(!IntegerType.DEFAULT.Equals(IdType.DEFAULT));
	    Assertion.Assert(!id1.Equals(integer1));

	    // StringType tests.
	    StringType s1 = StringType.Parse("foo");
	    StringType s2 = StringType.Parse("foo");
	    StringType s3 = StringType.Parse("bar");
	    StringType s4 = StringType.Parse("");

	    Assertion.AssertEquals(StringType.UNSET, StringType.UNSET);
	    Assertion.AssertEquals(StringType.DEFAULT, StringType.DEFAULT);
	    Assertion.AssertEquals(StringType.EMPTY, StringType.EMPTY);
	    Assertion.Assert(!StringType.UNSET.Equals(StringType.DEFAULT));
	    Assertion.Assert(!StringType.DEFAULT.Equals(StringType.UNSET));
	    Assertion.Assert(!StringType.EMPTY.Equals(StringType.DEFAULT));
	    Assertion.Assert(!StringType.DEFAULT.Equals(StringType.EMPTY));
	    Assertion.Assert(!StringType.EMPTY.Equals(StringType.UNSET));
	    Assertion.Assert(!StringType.UNSET.Equals(StringType.EMPTY));

	    Assertion.AssertEquals(s4, StringType.EMPTY);
	    Assertion.AssertEquals(StringType.EMPTY, s4);
	    Assertion.Assert(!s1.Equals(StringType.UNSET));
	    Assertion.Assert(!s1.Equals(StringType.DEFAULT));
	    Assertion.Assert(!StringType.UNSET.Equals(s1));
	    Assertion.Assert(!StringType.DEFAULT.Equals(s1));
	    Assertion.AssertEquals(s1, s1);
	    Assertion.AssertEquals(s1, s2);
	    Assertion.AssertEquals(s2, s1);
	    Assertion.Assert(!s1.Equals(s3));
	    Assertion.Assert(!s3.Equals(s1));

	    Assertion.Assert(!s1.Equals(null));

	    // DateType tests.
	    DateType date1 = new DateType(new DateTime(20000));
	    DateType date2 = new DateType(new DateTime(20000));
	    DateType date3 = new DateType(new DateTime(10000));
	    DateType date4 = new DateType(new DateTime(30000));

	    Assertion.AssertEquals(DateType.UNSET, DateType.UNSET);
	    Assertion.AssertEquals(DateType.DEFAULT, DateType.DEFAULT);
	    Assertion.Assert(!DateType.UNSET.Equals(DateType.DEFAULT));
	    Assertion.Assert(!DateType.DEFAULT.Equals(DateType.UNSET));

	    Assertion.Assert(!date1.Equals(DateType.UNSET));
	    Assertion.Assert(!date1.Equals(DateType.DEFAULT));
	    Assertion.Assert(!DateType.UNSET.Equals(date1));
	    Assertion.Assert(!DateType.DEFAULT.Equals(date1));
	    Assertion.AssertEquals(date1, date1);
	    Assertion.AssertEquals(date1, date2);
	    Assertion.AssertEquals(date2, date1);
	    Assertion.Assert(!date1.Equals(date3));
	    Assertion.Assert(!date3.Equals(date1));
	    Assertion.Assert(!date1.Equals(date4));

	    Assertion.Assert(!date1.Equals(null));
	}

	[Test]
	public void TestEnumEquals() {
	    Assertion.AssertEquals(GenderType.FEMALE, GenderType.FEMALE);
	    Assertion.AssertEquals(GenderType.MALE, GenderType.MALE);
	    Assertion.AssertEquals(GenderType.DEFAULT, GenderType.DEFAULT);
	    Assertion.AssertEquals(GenderType.UNSET, GenderType.UNSET);
	    Assertion.Assert(!GenderType.MALE.Equals(GenderType.FEMALE));
	    Assertion.Assert(!GenderType.FEMALE.Equals(GenderType.MALE));
	}

	[Test]
	public void TestCompare() {
	    IntegerType int1 = new IntegerType(10);
	    IntegerType int2 = new IntegerType(20);
	    IntegerType int3 = new IntegerType(30);
	    Assertion.Assert(int2.CompareTo(int1) > 0);
	    Assertion.Assert(int2.CompareTo(int2) == 0);
	    Assertion.Assert(int2.CompareTo(int3) < 0);
	    Assertion.Assert(int2.CompareTo(IntegerType.UNSET) > 0);
	    Assertion.Assert(int2.CompareTo(IntegerType.DEFAULT) > 0);
	    Assertion.Assert(IntegerType.DEFAULT.CompareTo(int2) < 0);
	    Assertion.Assert(IntegerType.UNSET.CompareTo(int2) < 0);
	    Assertion.Assert(IntegerType.UNSET.CompareTo(IntegerType.UNSET) == 0);
	    Assertion.Assert(IntegerType.DEFAULT.CompareTo(IntegerType.DEFAULT) == 0);
	    Assertion.Assert(IntegerType.DEFAULT.CompareTo(IntegerType.UNSET) < 0);
	    Assertion.Assert(IntegerType.UNSET.CompareTo(IntegerType.DEFAULT) > 0);

	    DecimalType decimal1 = new DecimalType(10.1);
	    DecimalType decimal2 = new DecimalType(20.2);
	    DecimalType decimal3 = new DecimalType(30.3);
	    Assertion.Assert(decimal2.CompareTo(decimal1) > 0);
	    Assertion.Assert(decimal2.CompareTo(decimal2) == 0);
	    Assertion.Assert(decimal2.CompareTo(decimal3) < 0);
	    Assertion.Assert(decimal2.CompareTo(DecimalType.UNSET) > 0);
	    Assertion.Assert(decimal2.CompareTo(DecimalType.DEFAULT) > 0);
	    Assertion.Assert(DecimalType.DEFAULT.CompareTo(decimal2) < 0);
	    Assertion.Assert(DecimalType.UNSET.CompareTo(decimal2) < 0);
	    Assertion.Assert(DecimalType.UNSET.CompareTo(DecimalType.UNSET) == 0);
	    Assertion.Assert(DecimalType.DEFAULT.CompareTo(DecimalType.DEFAULT) == 0);
	    Assertion.Assert(DecimalType.DEFAULT.CompareTo(DecimalType.UNSET) < 0);
	    Assertion.Assert(DecimalType.UNSET.CompareTo(DecimalType.DEFAULT) > 0);

	    StringType string1 = StringType.Parse("bar");
	    StringType string2 = StringType.Parse("foo");
	    StringType string3 = StringType.Parse("fubar");
	    Assertion.Assert(string2.CompareTo(string1) > 0);
	    Assertion.Assert(string2.CompareTo(string2) == 0);
	    Assertion.Assert(string2.CompareTo(string3) < 0);
	    Assertion.Assert(string2.CompareTo(StringType.UNSET) > 0);
	    Assertion.Assert(string2.CompareTo(StringType.DEFAULT) > 0);
	    Assertion.Assert(StringType.DEFAULT.CompareTo(string2) < 0);
	    Assertion.Assert(StringType.UNSET.CompareTo(string2) < 0);
	    Assertion.Assert(StringType.UNSET.CompareTo(StringType.UNSET) == 0);
	    Assertion.Assert(StringType.DEFAULT.CompareTo(StringType.DEFAULT) == 0);
	    Assertion.Assert(StringType.DEFAULT.CompareTo(StringType.UNSET) < 0);
	    Assertion.Assert(StringType.UNSET.CompareTo(StringType.DEFAULT) > 0);

	    DateType date1 = new DateType(new DateTime(1969,12,18));
	    DateType date2 = new DateType(new DateTime(2001,11,01));
	    DateType date3 = new DateType(new DateTime(2003,5,9));
	    Assertion.Assert(date2.CompareTo(date1) > 0);
	    Assertion.Assert(date2.CompareTo(date2) == 0);
	    Assertion.Assert(date2.CompareTo(date3) < 0);
	    Assertion.Assert(date2.CompareTo(DateType.UNSET) > 0);
	    Assertion.Assert(date2.CompareTo(DateType.DEFAULT) > 0);
	    Assertion.Assert(DateType.DEFAULT.CompareTo(date2) < 0);
	    Assertion.Assert(DateType.UNSET.CompareTo(date2) < 0);
	    Assertion.Assert(DateType.UNSET.CompareTo(DateType.UNSET) == 0);
	    Assertion.Assert(DateType.DEFAULT.CompareTo(DateType.DEFAULT) == 0);
	    Assertion.Assert(DateType.DEFAULT.CompareTo(DateType.UNSET) < 0);
	    Assertion.Assert(DateType.UNSET.CompareTo(DateType.DEFAULT) > 0);
	}

	[Test]
	public void TestEndOfQuarter() {

	    DateType date = DateType.Parse("2/28/2002");
	    DateType lastQuarterEnd = date.EndOfPreviousQuarter;
	    DateType thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assertion.Assert(DateTime.Parse("12/31/2001").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assertion.Assert(DateTime.Parse("3/31/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));
	    
	    date = DateType.Parse("6/1/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assertion.Assert(DateTime.Parse("3/31/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assertion.Assert(DateTime.Parse("6/30/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));

	    date = DateType.Parse("7/1/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assertion.Assert(DateTime.Parse("6/30/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assertion.Assert(DateTime.Parse("9/30/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));

	    date = DateType.Parse("12/31/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assertion.Assert(DateTime.Parse("9/30/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assertion.Assert(DateTime.Parse("12/31/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));
	}
    }
}
