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

	public StringType StringTypeProperty {
	    get { return propertyValue; }
	    set { propertyValue = value; }
	}
    }
}
