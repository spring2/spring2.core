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

	    // Number type tests.
	    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
	    NumberType number1 = new NumberType(3000);
	    String numberString = number1.ToString("N");
	    NumberType number2 = NumberType.Parse(numberString);
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse("3000");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse("3,000");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse("3000.");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse("3000.00");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse("3000.000");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse(" 3000 ");
	    Assertion.AssertEquals(number1, number2);
	    number2 = NumberType.Parse(" 3,000.000 ");
	    Assertion.AssertEquals(number1, number2);

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

	    // Number type tests.
	    NumberType n1 = new NumberType(100);
	    NumberType n2 = new NumberType(100);
	    NumberType n3 = new NumberType(50);

	    Assertion.AssertEquals(NumberType.UNSET, NumberType.UNSET);
	    Assertion.AssertEquals(NumberType.DEFAULT, NumberType.DEFAULT);
	    Assertion.Assert(!NumberType.UNSET.Equals(NumberType.DEFAULT));
	    Assertion.Assert(!NumberType.DEFAULT.Equals(NumberType.UNSET));

	    Assertion.Assert(!n1.Equals(NumberType.UNSET));
	    Assertion.Assert(!n1.Equals(NumberType.DEFAULT));
	    Assertion.Assert(!NumberType.UNSET.Equals(n1));
	    Assertion.Assert(!NumberType.DEFAULT.Equals(n1));
	    Assertion.AssertEquals(n1, n1);
	    Assertion.AssertEquals(n1, n2);
	    Assertion.AssertEquals(n2, n1);
	    Assertion.Assert(!n1.Equals(n3));
	    Assertion.Assert(!n3.Equals(n1));

	    Assertion.Assert(!n1.Equals(null));

	    // Compare number to id.
	    Assertion.Assert(!NumberType.UNSET.Equals(IdType.UNSET));
	    Assertion.Assert(!NumberType.DEFAULT.Equals(IdType.DEFAULT));
	    Assertion.Assert(!id1.Equals(n1));

	    // StringType tests.
	    StringType s1 = StringType.NewInstance("foo");
	    StringType s2 = StringType.NewInstance("foo");
	    StringType s3 = StringType.NewInstance("bar");
	    StringType s4 = StringType.NewInstance("");

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
    }
}
