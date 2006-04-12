using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class IdTypeTest {

	[Test]
	public void ImplicitIntOperator() {
	    IdType id = 1;
	    Assert.AreEqual(new IdType(1), id);
	}

	[Test]
	public void OperatorEquals() {
	    IdType id1 = new IdType(5);
	    IdType id2 = new IdType(5);
	    Assert.AreEqual(id1, id2);
	    Assert.IsTrue(id1.Equals(id2));
	    Assert.IsTrue(id1 == id2);
	}
    }
}
