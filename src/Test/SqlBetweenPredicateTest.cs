using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for SqlBetweenPredicateTest.
    /// </summary>
    [TestFixture]
    public class SqlBetweenPredicateTest {

	[Test]
	public void WithInt64() {
	    SqlBetweenPredicate predicate = new SqlBetweenPredicate("foo", 1, 2);
	    Assert.AreEqual("(foo BETWEEN @foo1 AND @foo2)", predicate.Expression);
	    Assert.AreEqual(2, predicate.Parameters.Count);
	    Assert.AreEqual(1, ((SqlParameter)predicate.Parameters["@foo1"]).Value);
	    Assert.AreEqual(2, ((SqlParameter)predicate.Parameters["@foo2"]).Value);
	}
    
    }
}
