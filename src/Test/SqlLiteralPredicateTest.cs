using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for SqlBetweenPredicateTest.
    /// </summary>
    [TestFixture]
    public class SqlLiteralPredicateTest {

	[Test]
	public void WithString() {
	    SqlLiteralPredicate predicate = new SqlLiteralPredicate("foo='bar'");
	    Assert.AreEqual(" foo='bar'", predicate.Expression);
	    Assert.AreEqual(0, predicate.Parameters.Count);
	}
    
    }
}
