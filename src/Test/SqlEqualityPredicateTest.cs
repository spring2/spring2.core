using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for SqlBetweenPredicateTest.
    /// </summary>
    [TestFixture]
    public class SqlEqualityPredicateTest {

	[Test]
	public void EqualInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.Equal, Int64.MaxValue);
	    Assert.AreEqual("(foo = @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}
    
	[Test]
	public void NotEqualInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.NotEqual, Int64.MaxValue);
	    Assert.AreEqual("(foo <> @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}

	[Test]
	public void LessThanInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.LessThan, Int64.MaxValue);
	    Assert.AreEqual("(foo < @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}
    
	[Test]
	public void LessThanOrEqualInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.LessThanOrEqual, Int64.MaxValue);
	    Assert.AreEqual("(foo <= @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}
    
	[Test]
	public void GreaterThanInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.GreaterThan, Int64.MaxValue);
	    Assert.AreEqual("(foo > @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}
    	
	[Test]
	public void GreaterThanOrEqualInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.GreaterThanOrEqual, Int64.MaxValue);
	    Assert.AreEqual("(foo >= @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}
    	
	[Test]
	public void LikeInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.Like, Int64.MaxValue);
	    Assert.AreEqual("(foo LIKE @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}

	[Test]
	public void NotLikeInt64() {
	    SqlEqualityPredicate predicate = new SqlEqualityPredicate("foo", EqualityOperatorEnum.NotLike, Int64.MaxValue);
	    Assert.AreEqual("(foo NOT LIKE @foo)", predicate.Expression);
	    Assert.AreEqual(1, predicate.Parameters.Count);
	    Assert.AreEqual(Int64.MaxValue, ((SqlParameter)predicate.Parameters["@foo"]).Value);
	}


    }
}
