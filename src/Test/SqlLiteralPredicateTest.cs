using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;
using System.Data;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for SqlLiteralPredicateTest.
    /// </summary>
    [TestFixture]
    public class SqlLiteralPredicateTest {

	[Test]
	public void WithString() {
	    SqlLiteralPredicate predicate = new SqlLiteralPredicate("foo='bar'");
	    Assert.AreEqual(" foo='bar'", predicate.Expression);
	    Assert.AreEqual(0, predicate.Parameters.Count);
	}

	[Test]
	public void WithStringAndParameters() {
	    SqlParameterList parameters = new SqlParameterList();
	    parameters.Add("@bar", SqlDbType.VarChar, "bar");

	    SqlLiteralPredicate predicate = new SqlLiteralPredicate("foo=@bar", parameters);
	    Assert.AreEqual(" foo=@bar", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual("bar", ((SqlParameter)predicate.Parameters["@bar"]).Value);
	}
    
    }
}
