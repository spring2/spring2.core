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
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("100");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("$100");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("100.");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("$100.");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("$100.  ");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("   $100.  ");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("   $100.000  ");
	    Assert.AreEqual(c1, c2);
	    c2 = CurrencyType.Parse("   $ 100.000  ");
	    Assert.AreEqual(c1, c2);

	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-CA");
	    s1 = c1.ToString();
	    c2 = CurrencyType.Parse(s1);
	    Assert.AreEqual(c1, c2);

	    // Integer type tests.
	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	    IntegerType integer1 = new IntegerType(3000);
	    String IntegerString = integer1.ToString("N");
	    IntegerType integer2 = IntegerType.Parse(IntegerString);
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse("3000");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse("3,000");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.00");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse("3000.000");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse(" 3000 ");
	    Assert.AreEqual(integer1, integer2);
	    integer2 = IntegerType.Parse(" 3,000.000 ");
	    Assert.AreEqual(integer1, integer2);

	    // Id type tests.
	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	    IdType id1 = new IdType(3000);
	    String idString = id1.ToString();
	    IdType id2 = IdType.Parse(idString);
	    Assert.AreEqual(id1, id2);
	    id2 = IdType.Parse("3000");
	    Assert.AreEqual(id1, id2);
	    id2 = IdType.Parse(" 3000 ");
	    Assert.AreEqual(id1, id2);

	    // DateType tests.
	    DateType date1 = new DateType(new DateTime(2001, 11, 1));
	    String dateString = date1.ToString();
	    DateType date2 = DateType.Parse(dateString);
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("11/01/01");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("11/01/2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("1 Nov 2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("1 November 2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("November 1, 2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("Nov, 2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("November, 2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("11-01-2001");
	    Assert.AreEqual(date1, date2);
	    date2 = DateType.Parse("11-01-01");
	    Assert.AreEqual(date1, date2);

	    //PhoneNumber tests
	    //test the constructor
	    PhoneNumberType phone1 = new PhoneNumberType("801", "825", "6264", String.Empty);
	    Assert.AreEqual("(801) 825-6264", phone1.ToString());
	    //test the parse method
	    PhoneNumberType phone2 = PhoneNumberType.Parse("8018256264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    phone2 = PhoneNumberType.Parse("(801)8256264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    phone2 = PhoneNumberType.Parse("(801)825-6264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    phone2 = PhoneNumberType.Parse("801.825.6264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    phone2 = PhoneNumberType.Parse("801 825 6264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    phone2 = PhoneNumberType.Parse("801.825.6264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    //test the international capabilities
	    phone1 = new PhoneNumberType("34","8256264", String.Empty);
	    phone2 = PhoneNumberType.Parse("+34 8256-264");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    //test the extension capabilities
	    phone1 = new PhoneNumberType("801","825","6264","107");
	    phone2 = PhoneNumberType.Parse("1-801-825-6264 extension 107");
	    Assert.AreEqual(phone1.ToString(), phone2.ToString());
	    Assert.AreEqual("(801) 825-6264 x107", phone2.ToString());
	    //test the dbvalue
	    Assert.AreEqual("8018256264 x107", phone2.DBValue);
	    phone2 = PhoneNumberType.Parse("+34 8256-264 ext. 107");
	    Assert.AreEqual("+34 8256264 x107", phone2.DBValue);
	    //test local number functionality
	    phone2 = PhoneNumberType.Parse("825-6264");
	    Assert.AreEqual("8256264", phone2.DBValue);
	}


	[Test]
	public void TestEquals() {

	    // BooleanType tests.
	    Assert.AreEqual(BooleanType.UNSET, BooleanType.UNSET);
	    Assert.AreEqual(BooleanType.DEFAULT, BooleanType.DEFAULT);
	    Assert.AreEqual(BooleanType.TRUE, BooleanType.TRUE);
	    Assert.AreEqual(BooleanType.FALSE, BooleanType.FALSE);

	    Assert.IsTrue(!BooleanType.UNSET.Equals(BooleanType.DEFAULT));
	    Assert.IsTrue(!BooleanType.UNSET.Equals(BooleanType.TRUE));
	    Assert.IsTrue(!BooleanType.UNSET.Equals(BooleanType.FALSE));

	    Assert.IsTrue(!BooleanType.DEFAULT.Equals(BooleanType.UNSET));
	    Assert.IsTrue(!BooleanType.DEFAULT.Equals(BooleanType.TRUE));
	    Assert.IsTrue(!BooleanType.DEFAULT.Equals(BooleanType.FALSE));

	    Assert.IsTrue(!BooleanType.TRUE.Equals(BooleanType.UNSET));
	    Assert.IsTrue(!BooleanType.TRUE.Equals(BooleanType.DEFAULT));
	    Assert.IsTrue(!BooleanType.TRUE.Equals(BooleanType.FALSE));

	    Assert.IsTrue(!BooleanType.FALSE.Equals(BooleanType.UNSET));
	    Assert.IsTrue(!BooleanType.FALSE.Equals(BooleanType.DEFAULT));
	    Assert.IsTrue(!BooleanType.FALSE.Equals(BooleanType.TRUE));

	    Assert.IsTrue(!BooleanType.TRUE.Equals(null));
	    Assert.IsTrue(!BooleanType.TRUE.Equals(new DateType()));

	    // Currency type tests.
	    CurrencyType c1 = new CurrencyType(100);
	    CurrencyType c2 = new CurrencyType(100);
	    CurrencyType c3 = new CurrencyType(50);

	    Assert.AreEqual(CurrencyType.UNSET, CurrencyType.UNSET);
	    Assert.AreEqual(CurrencyType.DEFAULT, CurrencyType.DEFAULT);
	    Assert.IsTrue(!CurrencyType.UNSET.Equals(CurrencyType.DEFAULT));
	    Assert.IsTrue(!CurrencyType.DEFAULT.Equals(CurrencyType.UNSET));

	    Assert.IsTrue(!c1.Equals(CurrencyType.UNSET));
	    Assert.IsTrue(!c1.Equals(CurrencyType.DEFAULT));
	    Assert.IsTrue(!CurrencyType.UNSET.Equals(c1));
	    Assert.IsTrue(!CurrencyType.DEFAULT.Equals(c1));
	    Assert.AreEqual(c1, c1);
	    Assert.AreEqual(c1, c2);
	    Assert.AreEqual(c2, c1);
	    Assert.IsTrue(!c1.Equals(c3));
	    Assert.IsTrue(!c3.Equals(c1));

	    Assert.IsTrue(!c1.Equals(null));

	    // Quantity type tests.
	    QuantityType q1 = new QuantityType(100);
	    QuantityType q2 = new QuantityType(100);
	    QuantityType q3 = new QuantityType(50);

	    Assert.AreEqual(QuantityType.UNSET, QuantityType.UNSET);
	    Assert.AreEqual(QuantityType.DEFAULT, QuantityType.DEFAULT);
	    Assert.IsTrue(!QuantityType.UNSET.Equals(QuantityType.DEFAULT));
	    Assert.IsTrue(!QuantityType.DEFAULT.Equals(QuantityType.UNSET));

	    Assert.IsTrue(!q1.Equals(QuantityType.UNSET));
	    Assert.IsTrue(!q1.Equals(QuantityType.DEFAULT));
	    Assert.IsTrue(!QuantityType.UNSET.Equals(q1));
	    Assert.IsTrue(!QuantityType.DEFAULT.Equals(q1));
	    Assert.AreEqual(q1, q1);
	    Assert.AreEqual(q1, q2);
	    Assert.AreEqual(q2, q1);
	    Assert.IsTrue(!q1.Equals(q3));
	    Assert.IsTrue(!q3.Equals(q1));

	    Assert.IsTrue(!q1.Equals(null));

	    // Decimal type tests.
	    DecimalType d1 = new DecimalType(100);
	    DecimalType d2 = new DecimalType(100);
	    DecimalType d3 = new DecimalType(50);

	    Assert.AreEqual(DecimalType.UNSET, DecimalType.UNSET);
	    Assert.AreEqual(DecimalType.DEFAULT, DecimalType.DEFAULT);
	    Assert.IsTrue(!DecimalType.UNSET.Equals(DecimalType.DEFAULT));
	    Assert.IsTrue(!DecimalType.DEFAULT.Equals(DecimalType.UNSET));

	    Assert.IsTrue(!d1.Equals(DecimalType.UNSET));
	    Assert.IsTrue(!d1.Equals(DecimalType.DEFAULT));
	    Assert.IsTrue(!DecimalType.UNSET.Equals(d1));
	    Assert.IsTrue(!DecimalType.DEFAULT.Equals(d1));
	    Assert.AreEqual(d1, d1);
	    Assert.AreEqual(d1, d2);
	    Assert.AreEqual(d2, d1);
	    Assert.IsTrue(!d1.Equals(d3));
	    Assert.IsTrue(!d3.Equals(d1));

	    Assert.IsTrue(!d1.Equals(null));

	    // Compare decimal to currency.
	    Assert.IsTrue(!CurrencyType.UNSET.Equals(DecimalType.UNSET));
	    Assert.IsTrue(!CurrencyType.DEFAULT.Equals(DecimalType.DEFAULT));
	    Assert.IsTrue(!c1.Equals(d1));

	    // Compare decimal to quantity.
	    Assert.IsTrue(!QuantityType.UNSET.Equals(DecimalType.UNSET));
	    Assert.IsTrue(!QuantityType.DEFAULT.Equals(DecimalType.DEFAULT));
	    Assert.IsTrue(!q1.Equals(d1));

	    // Compare currency to quantity.
	    Assert.IsTrue(!QuantityType.UNSET.Equals(CurrencyType.UNSET));
	    Assert.IsTrue(!QuantityType.DEFAULT.Equals(CurrencyType.DEFAULT));
	    Assert.IsTrue(!q1.Equals(c1));

	    // Id type tests.
	    IdType id1 = new IdType(100);
	    IdType id2 = new IdType(100);
	    IdType id3 = new IdType(50);

	    Assert.AreEqual(IdType.UNSET, IdType.UNSET);
	    Assert.AreEqual(IdType.DEFAULT, IdType.DEFAULT);
	    Assert.IsTrue(!IdType.UNSET.Equals(IdType.DEFAULT));
	    Assert.IsTrue(!IdType.DEFAULT.Equals(IdType.UNSET));

	    Assert.IsTrue(!id1.Equals(IdType.UNSET));
	    Assert.IsTrue(!id1.Equals(IdType.DEFAULT));
	    Assert.IsTrue(!IdType.UNSET.Equals(id1));
	    Assert.IsTrue(!IdType.DEFAULT.Equals(id1));
	    Assert.AreEqual(id1, id1);
	    Assert.AreEqual(id1, id2);
	    Assert.AreEqual(id2, id1);
	    Assert.IsTrue(!id1.Equals(id3));
	    Assert.IsTrue(!id3.Equals(id1));

	    Assert.IsTrue(!id1.Equals(null));

	    // Integer type tests.
	    IntegerType integer1 = new IntegerType(100);
	    IntegerType integer2 = new IntegerType(100);
	    IntegerType integer3 = new IntegerType(50);

	    Assert.AreEqual(IntegerType.UNSET, IntegerType.UNSET);
	    Assert.AreEqual(IntegerType.DEFAULT, IntegerType.DEFAULT);
	    Assert.IsTrue(!IntegerType.UNSET.Equals(IntegerType.DEFAULT));
	    Assert.IsTrue(!IntegerType.DEFAULT.Equals(IntegerType.UNSET));

	    Assert.IsTrue(!integer1.Equals(IntegerType.UNSET));
	    Assert.IsTrue(!integer1.Equals(IntegerType.DEFAULT));
	    Assert.IsTrue(!IntegerType.UNSET.Equals(integer1));
	    Assert.IsTrue(!IntegerType.DEFAULT.Equals(integer1));
	    Assert.AreEqual(integer1, integer1);
	    Assert.AreEqual(integer1, integer2);
	    Assert.AreEqual(integer2, integer1);
	    Assert.IsTrue(!integer1.Equals(integer3));
	    Assert.IsTrue(!integer3.Equals(integer1));

	    Assert.IsTrue(!integer1.Equals(null));

	    // Compare Integer to id.
	    Assert.IsTrue(!IntegerType.UNSET.Equals(IdType.UNSET));
	    Assert.IsTrue(!IntegerType.DEFAULT.Equals(IdType.DEFAULT));
	    Assert.IsTrue(!id1.Equals(integer1));

	    // StringType tests.
	    StringType s1 = StringType.Parse("foo");
	    StringType s2 = StringType.Parse("foo");
	    StringType s3 = StringType.Parse("bar");
	    StringType s4 = StringType.Parse("");

	    Assert.AreEqual(StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual(StringType.DEFAULT, StringType.DEFAULT);
	    Assert.AreEqual(StringType.EMPTY, StringType.EMPTY);
	    Assert.IsTrue(!StringType.UNSET.Equals(StringType.DEFAULT));
	    Assert.IsTrue(!StringType.DEFAULT.Equals(StringType.UNSET));
	    Assert.IsTrue(!StringType.EMPTY.Equals(StringType.DEFAULT));
	    Assert.IsTrue(!StringType.DEFAULT.Equals(StringType.EMPTY));
	    Assert.IsTrue(!StringType.EMPTY.Equals(StringType.UNSET));
	    Assert.IsTrue(!StringType.UNSET.Equals(StringType.EMPTY));

	    Assert.AreEqual(s4, StringType.EMPTY);
	    Assert.AreEqual(StringType.EMPTY, s4);
	    Assert.IsTrue(!s1.Equals(StringType.UNSET));
	    Assert.IsTrue(!s1.Equals(StringType.DEFAULT));
	    Assert.IsTrue(!StringType.UNSET.Equals(s1));
	    Assert.IsTrue(!StringType.DEFAULT.Equals(s1));
	    Assert.AreEqual(s1, s1);
	    Assert.AreEqual(s1, s2);
	    Assert.AreEqual(s2, s1);
	    Assert.IsTrue(!s1.Equals(s3));
	    Assert.IsTrue(!s3.Equals(s1));

	    Assert.IsTrue(!s1.Equals(null));

	    // TODO: below this was all DateType before, I have changed to DateTimeType as that is what they now closely match
	    // TODO: there should be tests for DateType as well as for TimeType too
	    // DateType tests.
	    DateTimeType date1 = new DateTimeType(new DateTime(20000));
	    DateTimeType date2 = new DateTimeType(new DateTime(20000));
	    DateTimeType date3 = new DateTimeType(new DateTime(10000));
	    DateTimeType date4 = new DateTimeType(new DateTime(30000));

	    Assert.AreEqual(DateTimeType.UNSET, DateTimeType.UNSET);
	    Assert.AreEqual(DateTimeType.DEFAULT, DateTimeType.DEFAULT);
	    Assert.IsTrue(!DateTimeType.UNSET.Equals(DateTimeType.DEFAULT));
	    Assert.IsTrue(!DateTimeType.DEFAULT.Equals(DateTimeType.UNSET));

	    Assert.IsTrue(!date1.Equals(DateTimeType.UNSET));
	    Assert.IsTrue(!date1.Equals(DateTimeType.DEFAULT));
	    Assert.IsTrue(!DateTimeType.UNSET.Equals(date1));
	    Assert.IsTrue(!DateTimeType.DEFAULT.Equals(date1));
	    Assert.AreEqual(date1, date1);
	    Assert.AreEqual(date1, date2);
	    Assert.AreEqual(date2, date1);
	    Assert.IsTrue(!date1.Equals(date3));
	    Assert.IsTrue(!date3.Equals(date1));
	    Assert.IsTrue(!date1.Equals(date4));

	    Assert.IsTrue(!date1.Equals(null));
	}

	[Test]
	public void TestEnumEquals() {
	    Assert.AreEqual(GenderType.FEMALE, GenderType.FEMALE);
	    Assert.AreEqual(GenderType.MALE, GenderType.MALE);
	    Assert.AreEqual(GenderType.DEFAULT, GenderType.DEFAULT);
	    Assert.AreEqual(GenderType.UNSET, GenderType.UNSET);
	    Assert.IsTrue(!GenderType.MALE.Equals(GenderType.FEMALE));
	    Assert.IsTrue(!GenderType.FEMALE.Equals(GenderType.MALE));
	}

	[Test]
	public void TestCompare() {
	    IntegerType int1 = new IntegerType(10);
	    IntegerType int2 = new IntegerType(20);
	    IntegerType int3 = new IntegerType(30);
	    Assert.IsTrue(int2.CompareTo(int1) > 0);
	    Assert.IsTrue(int2.CompareTo(int2) == 0);
	    Assert.IsTrue(int2.CompareTo(int3) < 0);
	    Assert.IsTrue(int2.CompareTo(IntegerType.UNSET) > 0);
	    Assert.IsTrue(int2.CompareTo(IntegerType.DEFAULT) > 0);
	    Assert.IsTrue(IntegerType.DEFAULT.CompareTo(int2) < 0);
	    Assert.IsTrue(IntegerType.UNSET.CompareTo(int2) < 0);
	    Assert.IsTrue(IntegerType.UNSET.CompareTo(IntegerType.UNSET) == 0);
	    Assert.IsTrue(IntegerType.DEFAULT.CompareTo(IntegerType.DEFAULT) == 0);
	    Assert.IsTrue(IntegerType.DEFAULT.CompareTo(IntegerType.UNSET) < 0);
	    Assert.IsTrue(IntegerType.UNSET.CompareTo(IntegerType.DEFAULT) > 0);

	    DecimalType decimal1 = new DecimalType(10.1);
	    DecimalType decimal2 = new DecimalType(20.2);
	    DecimalType decimal3 = new DecimalType(30.3);
	    Assert.IsTrue(decimal2.CompareTo(decimal1) > 0);
	    Assert.IsTrue(decimal2.CompareTo(decimal2) == 0);
	    Assert.IsTrue(decimal2.CompareTo(decimal3) < 0);
	    Assert.IsTrue(decimal2.CompareTo(DecimalType.UNSET) > 0);
	    Assert.IsTrue(decimal2.CompareTo(DecimalType.DEFAULT) > 0);
	    Assert.IsTrue(DecimalType.DEFAULT.CompareTo(decimal2) < 0);
	    Assert.IsTrue(DecimalType.UNSET.CompareTo(decimal2) < 0);
	    Assert.IsTrue(DecimalType.UNSET.CompareTo(DecimalType.UNSET) == 0);
	    Assert.IsTrue(DecimalType.DEFAULT.CompareTo(DecimalType.DEFAULT) == 0);
	    Assert.IsTrue(DecimalType.DEFAULT.CompareTo(DecimalType.UNSET) < 0);
	    Assert.IsTrue(DecimalType.UNSET.CompareTo(DecimalType.DEFAULT) > 0);

	    StringType string1 = StringType.Parse("bar");
	    StringType string2 = StringType.Parse("foo");
	    StringType string3 = StringType.Parse("fubar");
	    Assert.IsTrue(string2.CompareTo(string1) > 0);
	    Assert.IsTrue(string2.CompareTo(string2) == 0);
	    Assert.IsTrue(string2.CompareTo(string3) < 0);
	    Assert.IsTrue(string2.CompareTo(StringType.UNSET) > 0);
	    Assert.IsTrue(string2.CompareTo(StringType.DEFAULT) > 0);
	    Assert.IsTrue(StringType.DEFAULT.CompareTo(string2) < 0);
	    Assert.IsTrue(StringType.UNSET.CompareTo(string2) < 0);
	    Assert.IsTrue(StringType.UNSET.CompareTo(StringType.UNSET) == 0);
	    Assert.IsTrue(StringType.DEFAULT.CompareTo(StringType.DEFAULT) == 0);
	    Assert.IsTrue(StringType.DEFAULT.CompareTo(StringType.UNSET) < 0);
	    Assert.IsTrue(StringType.UNSET.CompareTo(StringType.DEFAULT) > 0);

	    DateType date1 = new DateType(new DateTime(1969,12,18));
	    DateType date2 = new DateType(new DateTime(2001,11,01));
	    DateType date3 = new DateType(new DateTime(2003,5,9));
	    Assert.IsTrue(date2.CompareTo(date1) > 0);
	    Assert.IsTrue(date2.CompareTo(date2) == 0);
	    Assert.IsTrue(date2.CompareTo(date3) < 0);
	    Assert.IsTrue(date2.CompareTo(DateType.UNSET) > 0);
	    Assert.IsTrue(date2.CompareTo(DateType.DEFAULT) > 0);
	    Assert.IsTrue(DateType.DEFAULT.CompareTo(date2) < 0);
	    Assert.IsTrue(DateType.UNSET.CompareTo(date2) < 0);
	    Assert.IsTrue(DateType.UNSET.CompareTo(DateType.UNSET) == 0);
	    Assert.IsTrue(DateType.DEFAULT.CompareTo(DateType.DEFAULT) == 0);
	    Assert.IsTrue(DateType.DEFAULT.CompareTo(DateType.UNSET) < 0);
	    Assert.IsTrue(DateType.UNSET.CompareTo(DateType.DEFAULT) > 0);
	}

	[Test]
	public void TestLessThanGreaterThan() {
	    DecimalType d1 = new DecimalType(-1);
	    DecimalType d2 = new DecimalType(1);
	    DecimalType d3 = new DecimalType(1);
	    
	    Assert.IsTrue(d1 < d2);
	    Assert.IsTrue(d2 > d1);
	    Assert.IsTrue(d2 > DecimalType.ZERO);
	    Assert.IsTrue(DecimalType.ZERO < d2);
	    Assert.IsTrue(d1 < DecimalType.ZERO);
	    Assert.IsTrue(DecimalType.ZERO > d1);
	    Assert.IsTrue(!(d2 < d3));
	    Assert.IsTrue(d2 <= d3);
	    Assert.IsTrue(d2 >= d3);

	    Assert.IsTrue(!(DecimalType.DEFAULT < DecimalType.DEFAULT));
	    Assert.IsTrue(DecimalType.DEFAULT <= DecimalType.DEFAULT);
	    Assert.IsTrue(!(DecimalType.DEFAULT > DecimalType.DEFAULT));
	    Assert.IsTrue(DecimalType.DEFAULT >= DecimalType.DEFAULT);
	    
	    Assert.IsTrue(!(DecimalType.UNSET < DecimalType.UNSET));
	    Assert.IsTrue(DecimalType.UNSET <= DecimalType.UNSET);
	    Assert.IsTrue(!(DecimalType.UNSET > DecimalType.UNSET));
	    Assert.IsTrue(DecimalType.UNSET >= DecimalType.UNSET);

	    Assert.IsTrue(DecimalType.DEFAULT < DecimalType.UNSET);
	    Assert.IsTrue(DecimalType.DEFAULT < DecimalType.ZERO);
	    Assert.IsTrue(DecimalType.DEFAULT < d1);
	    Assert.IsTrue(DecimalType.DEFAULT < d2);

	    CurrencyType c1 = new CurrencyType(-1);
	    CurrencyType c2 = new CurrencyType(1);
	    CurrencyType c3 = new CurrencyType(1);
	    
	    Assert.IsTrue(c1 < c2);
	    Assert.IsTrue(c2 > c1);
	    Assert.IsTrue(c2 > CurrencyType.ZERO);
	    Assert.IsTrue(CurrencyType.ZERO < c2);
	    Assert.IsTrue(c1 < CurrencyType.ZERO);
	    Assert.IsTrue(CurrencyType.ZERO > c1);
	    Assert.IsTrue(!(c2 < c3));
	    Assert.IsTrue(c2 <= c3);
	    Assert.IsTrue(c2 >= c3);

	    Assert.IsTrue(!(CurrencyType.DEFAULT < CurrencyType.DEFAULT));
	    Assert.IsTrue(CurrencyType.DEFAULT <= CurrencyType.DEFAULT);
	    Assert.IsTrue(!(CurrencyType.DEFAULT > CurrencyType.DEFAULT));
	    Assert.IsTrue(CurrencyType.DEFAULT >= CurrencyType.DEFAULT);
	    
	    Assert.IsTrue(!(CurrencyType.UNSET < CurrencyType.UNSET));
	    Assert.IsTrue(CurrencyType.UNSET <= CurrencyType.UNSET);
	    Assert.IsTrue(!(CurrencyType.UNSET > CurrencyType.UNSET));
	    Assert.IsTrue(CurrencyType.UNSET >= CurrencyType.UNSET);

	    Assert.IsTrue(CurrencyType.DEFAULT < CurrencyType.UNSET);
	    Assert.IsTrue(CurrencyType.DEFAULT < CurrencyType.ZERO);
	    Assert.IsTrue(CurrencyType.DEFAULT < c1);
	    Assert.IsTrue(CurrencyType.DEFAULT < c2);
	}

	[Test]
	public void TestEndOfQuarter() {

	    DateType date = DateType.Parse("2/28/2002");
	    DateType lastQuarterEnd = date.EndOfPreviousQuarter;
	    DateType thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assert.IsTrue(DateTime.Parse("12/31/2001").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assert.IsTrue(DateTime.Parse("3/31/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));
	    
	    date = DateType.Parse("6/1/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assert.IsTrue(DateTime.Parse("3/31/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assert.IsTrue(DateTime.Parse("6/30/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));

	    date = DateType.Parse("7/1/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assert.IsTrue(DateTime.Parse("6/30/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assert.IsTrue(DateTime.Parse("9/30/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));

	    date = DateType.Parse("12/31/2002");
	    lastQuarterEnd = date.EndOfPreviousQuarter;
	    thisQuarterEnd = date.EndOfCurrentQuarter;
	    Assert.IsTrue(DateTime.Parse("9/30/2002").Date.Equals(lastQuarterEnd.ToDateTime().Date));
	    Assert.IsTrue(DateTime.Parse("12/31/2002").Date.Equals(thisQuarterEnd.ToDateTime().Date));
	}
    }
}
