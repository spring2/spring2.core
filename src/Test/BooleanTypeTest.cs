using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for BooleanType
    /// </summary>
    [TestFixture]
    public class BooleanTypeTest {

	[Test]
	public void TestParse() {
	    Assert.IsTrue(BooleanType.FALSE.IsValid);
	    Assert.IsTrue(BooleanType.TRUE.IsValid);

	    Assert.IsFalse(BooleanType.FALSE.ToBoolean());
	    Assert.IsTrue(BooleanType.TRUE.ToBoolean());

	    Assert.IsFalse(BooleanType.UNSET.IsValid);
	    Assert.IsFalse(BooleanType.DEFAULT.IsValid);
	}
    	
    	[Test]
	public void ShouldConstructFromBoolean() {
    	    BooleanType bt = new BooleanType(true);
    	    Assert.AreEqual(true, bt.ToBoolean());
    		
	    bt = new BooleanType(false);
	    Assert.AreEqual(false, bt.ToBoolean());
    	}
    	
    	[Test]
	public void ShouldImplicitlyConvertFromBoolean() {
	    BooleanType bt = true;
	    Assert.AreEqual(true, bt.ToBoolean());
    		
	    bt = false;
	    Assert.AreEqual(false, bt.ToBoolean());
    	}

    }
}
