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
    }
}
