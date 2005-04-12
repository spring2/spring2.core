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
	    StringType s1 = "foo";
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
