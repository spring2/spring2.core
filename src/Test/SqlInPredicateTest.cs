using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for SqlBetweenPredicateTest.
    /// </summary>
    [TestFixture]
    public class SqlInPredicateTest {

	[Test]
	public void WithInt64() {
	    SqlInPredicate predicate = new SqlInPredicate("foo", new Int64[] {1, 2, 3});
	    Assert.AreEqual(" foo IN (@foo1, @foo2, @foo3)", predicate.Expression);
	    Assert.AreEqual(3, predicate.Parameters.Count);
	    Assert.AreEqual(1, ((SqlParameter)predicate.Parameters["@foo1"]).Value);
	    Assert.AreEqual(2, ((SqlParameter)predicate.Parameters["@foo2"]).Value);
	    Assert.AreEqual(3, ((SqlParameter)predicate.Parameters["@foo3"]).Value);
	}
    
    }
}
