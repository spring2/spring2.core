using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class IntegerTypeTest {

	[Test]
	public void ImplicitIntOperator() {
	    IntegerType id = 1;
	    Assert.AreEqual(new IntegerType(1), id);
	}

	[Test()]
	public void ExplicitCastToInt32() {
	    Int32 i32 = 1111111;
	    IntegerType i = i32;
	    Assert.AreEqual(i32, i.ToInt32());
	    Assert.AreEqual(i32, (int) i);
	}

    }
}
