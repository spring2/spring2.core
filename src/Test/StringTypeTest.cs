using System;
using System.Reflection;
using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for BooleanType
    /// </summary>
    [TestFixture]
    public class StringTypeTest {

	private StringType propertyValue = StringType.DEFAULT;

	[Test]
	public void AssignmentFromString() {
	    String foo = "foo";
	    StringType s2 = StringType.Parse(foo);
	    StringType s3 = StringType.Parse(foo);
	    Assert.AreEqual(s2, s3);

	    StringType s1 = foo;
	    Assert.AreEqual(foo, s1.ToString());
	    Assert.AreEqual(s2, s1);
	    
	    // this fails -- why?
	    Assert.AreEqual(StringType.Parse("foo"), s1);
	}

	public void AssignementFromObject() {
	    StringType s1 = "foo";
	    Object o = s1 as Object;
	    
	    PropertyInfo property = this.GetType().GetProperty("StringTypeProperty");
	    property.SetValue(this, o, null);
	    Assert.AreEqual(s1, propertyValue);
	}

	/// <summary>
	/// Property for AssignementFromDataObject test
	/// </summary>
	public StringType StringTypeProperty {
	    get { return propertyValue; }
	    set { propertyValue = value; }
	}

	[Test]
	public void ToStringTest() {
	    Assert.AreEqual("UNSET", StringType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", StringType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", BooleanType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", BooleanType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", CurrencyType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", CurrencyType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", DateType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", DateType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", DecimalType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", DecimalType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", GenderType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", GenderType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", IdType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", IdType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", IntegerType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", IntegerType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", PhoneNumberType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", PhoneNumberType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", QuantityType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", QuantityType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", RowVersionType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", RowVersionType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", StringType.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", StringType.DEFAULT.ToString());

	    Assert.AreEqual("UNSET", USStateCodeEnum.UNSET.ToString());
	    Assert.AreEqual("DEFAULT", USStateCodeEnum.DEFAULT.ToString());
	}

    }
}
