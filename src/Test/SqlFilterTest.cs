using System;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for IWhere functionality
    /// </summary>
    [TestFixture]
    public class SqlFilterTest {

	#region Old version (kept for reference until these test are converted to new version as appropriate)
//	[Test]
//	public void SqlFilter() {
//	    // make sure an empty object does not emits an empty clause
//	    SqlFilter filter = new SqlFilter();
//	    Assert.AreEqual(String.Empty, filter.Statement);
//	    
//	    // make sure that apostrophe is properly escaped
//	    filter = new SqlFilter("column", "foo'bar");
//	    Assert.AreEqual(" WHERE column=@column", filter.Statement);
//
//	    filter.AndEquals("column2", 1);
//	    Assert.AreEqual(" WHERE column=@column AND column2=@column2", filter.Statement);
//
//	    SqlFilter nestedFilter = new SqlFilter("nested1", 1234.56);
//	    nestedFilter.AndEquals("nested2", DateTime.Today);
//	    filter.And(nestedFilter);
//	    filter.And("foo=bar");
//	    Assert.AreEqual(" WHERE column=@column AND column2=@column2 AND (nested1=@nested1 AND nested2=@nested2) AND (foo=bar)", filter.Statement);
//	    Assert.AreEqual(4, filter.Parameters.Count);
//	    Assert.IsTrue(filter.Parameters.Contains("@column"));
//	    Assert.IsTrue(filter.Parameters.Contains("@column2"));
//	    Assert.IsTrue(filter.Parameters.Contains("@nested1"));
//	    Assert.IsTrue(filter.Parameters.Contains("@nested2"));
//	
//	    Assert.AreEqual("foo'bar", ((SqlParameter)filter.Parameters["@column"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter)filter.Parameters["@column2"]).Value);
//	    Assert.AreEqual(1234.56, ((SqlParameter)filter.Parameters["@nested1"]).Value);
//	    Assert.AreEqual(DateTime.Today, ((SqlParameter)filter.Parameters["@nested2"]).Value);
//
//	    // Check building where clause from string
//	    filter = new SqlFilter("c1 = 57 AND [user].[plan] = 'goodone'");
//	    Assert.AreEqual(" WHERE (c1 = 57 AND [user].[plan] = 'goodone')", filter.Statement);
//	    
//	    // Check building where clause from where clause
//	    SqlFilter filter1 = new SqlFilter(filter);
//	    Assert.AreEqual(" WHERE (c1 = 57 AND [user].[plan] = 'goodone')", filter1.Statement);
//	}
//    	
//	[Test]
//	public void HandlesNullValueWithIsNull() {
//	    SqlFilter filter = new SqlFilter("CompanyId", null);
//	    Assert.AreEqual(" WHERE CompanyId IS NULL", filter.Statement);
//	    Assert.AreEqual(0, filter.Parameters.Count);
//	}
//
//	[Test]
//	public void HandlesNullValueWithIsNullWithOtherValues() {
//	    SqlFilter filter = new SqlFilter("CompanyId", null);
//	    filter.AndEquals("AccountId", 1);
//	    Assert.AreEqual(" WHERE CompanyId IS NULL AND AccountId=@AccountId", filter.Statement);
//	    Assert.AreEqual(1, filter.Parameters.Count);
//	}
//    	
//	[Test]
//	public void HandlesDBNullValueWithIsNull() {
//	    SqlFilter filter = new SqlFilter("CompanyId", DBNull.Value);
//	    Assert.AreEqual(" WHERE CompanyId IS NULL", filter.Statement);
//	    Assert.AreEqual(0, filter.Parameters.Count);
//	}
//	
//	[Test]
//	public void AndNotHandlesNullValueWithIsNullWithOtherValues() {
//	    SqlFilter filter = new SqlFilter("AccountId", 1);
//	    filter.AndNotEquals("CompanyId", null);
//	    Assert.AreEqual(" WHERE AccountId=@AccountId AND CompanyId IS NOT NULL", filter.Statement);
//	    Assert.AreEqual(1, filter.Parameters.Count);
//	}
//    	
//	[Test]
//	public void AndNotHandlesDBNullValueWithIsNull() {
//	    SqlFilter filter = new SqlFilter("AccountId", 1);
//	    filter.AndNotEquals("CompanyId", DBNull.Value);
//	    Assert.AreEqual(" WHERE AccountId=@AccountId AND CompanyId IS NOT NULL", filter.Statement);
//	    Assert.AreEqual(1, filter.Parameters.Count);
//	}
//
//	[Test]
//	public void SqlFilter_And() {
//	    // make sure that apostrophe is properly escaped
//	    SqlFilter filter = new SqlFilter("foo", "bar");
//	    filter.AndEquals("column", "foo'bar");
//	    Assert.AreEqual(" WHERE foo=@foo AND column=@column", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//		
//	[Test]
//	public void SqlFilter_Or() {
//	    // make sure that apostrophe is properly escaped
//	    SqlFilter filter = new SqlFilter("foo", "bar");
//	    filter.OrEquals("column", "foo'bar");
//	    Assert.AreEqual(" WHERE foo=@foo OR column=@column", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	
//	[Test]
//	public void Constructors() {
//	    SqlFilter filter = new SqlFilter();
//	    Assert.IsTrue(filter.IsEmpty);
//		    
//	    filter = new SqlFilter("columnA", 1);
//	    filter.AndEquals("columnB", 2);
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//		
//	    filter = new SqlFilter("columnA", 1);
//	    filter.AndEquals("columnB", 2);
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//		
//	    filter = new SqlFilter("columnA", "foo'bar");
//	    Assert.AreEqual(" WHERE columnA=@columnA", filter.Statement);
//	    Assert.AreEqual(1, filter.Parameters.Count);
//	}
//
//	#region And
//	[Test]
//	public void And() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", 1);
//	    filter.AndEquals("columnB", 2);
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	
//	    filter = new SqlFilter("columnA", "foo'bar");
//	    filter.AndEquals("columnB", "fee'fie");
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	
//	[Test]
//	public void AndEquals() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", 1);
//	    filter.AndEquals("columnB", 2);
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//		
//	    filter = new SqlFilter("columnA", "foo'bar");
//	    filter.AndEquals("columnB", "fee'fie");
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB=@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//
//	[Test]
//	public void AndNotEquals() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", 1);
//	    filter.AndNotEquals("columnB", 2);
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB<>@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//		
//	    filter = new SqlFilter("columnA", "foo'bar");
//	    filter.AndNotEquals("columnB", "fee'fie");
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB<>@columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	#endregion
//
//	#region Like
//	[Test]
//	public void AndLike() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", "a'%");
//	    filter.AndLike("columnB", "b'%");
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB LIKE @columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	
//	[Test]
//	public void OrLike() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", "a'%");
//	    filter.OrLike("columnB", "b'%");
//	    Assert.AreEqual(" WHERE columnA=@columnA OR columnB LIKE @columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	
//	
//	[Test]
//	public void AndNotLike() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", "a'%");
//	    filter.AndNotLike("columnB", "b'%");
//	    Assert.AreEqual(" WHERE columnA=@columnA AND columnB NOT LIKE @columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	
//	
//	[Test]
//	public void OrNotLike() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter = new SqlFilter("columnA", "a'%");
//	    filter.OrNotLike("columnB", "b'%");
//	    Assert.AreEqual(" WHERE columnA=@columnA OR columnB NOT LIKE @columnB", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	}
//	#endregion
//	
//	#region Between
//	[Test]
//	public void AndBetween() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.AndBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.AndBetween("columnA", "1", "2");
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.AndBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndBetween("columnA", "fee'fie", "fo'fum");
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	}
//	
//	[Test]
//	public void AndNotBetween() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.AndNotBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.AndNotBetween("columnA", "1", "2");
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.AndNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotBetween("columnA", "fee'fie", "fo'fum");
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	}
//	
//	[Test]
//	public void OrBetween() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.OrBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.OrBetween("columnA", "1", "2");
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.OrBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrBetween("columnA", "fee'fie", "fo'fum");
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	}
//	
//	[Test]
//	public void OrNotBetween() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.OrNotBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.OrNotBetween("columnA", "1", "2");
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.OrNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotBetween("columnA", 1, 2);
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotBetween("columnA", "fee'fie", "fo'fum");
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT BETWEEN @columnA1 AND @columnA2", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	}
//	#endregion
//    	
//	#region In
//	[Test]
//	public void AndIn() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.AndIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.AndIn("columnA", new Object[] {"1", "2"});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.AndIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndIn("columnA", new Object[] {"fee'fie", "fo'fum"});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//
//	    filter = new SqlFilter("foo", "bar");
//	    try {
//	    	filter.AndIn("columnA", new Object[] {});
//	    	Assert.Fail();
//	    } catch (ArgumentException) {
//	    	// this is expected
//	    }
//	}
//	
//	[Test]
//	public void AndNotIn() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.AndNotIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.AndNotIn("columnA", new Object[] {"1", "2"});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.AndNotIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotIn("columnA", new Object[] {"fee'fie", "fo'fum"});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.AndNotIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE foo=@foo AND columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//
//	    filter = new SqlFilter("foo", "bar");
//	    try {
//		filter.AndNotIn("columnA", new Object[] {});
//		Assert.Fail();
//	    } catch (ArgumentException) {
//		// this is expected
//	    }
//	}
//	
//	[Test]
//	public void OrIn() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.OrIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.OrIn("columnA", new Object[] {"1", "2"});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.OrIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrIn("columnA", new Object[] {"fee'fie", "fo'fum"});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//
//	    filter = new SqlFilter("foo", "bar");
//	    try {
//		filter.OrIn("columnA", new Object[] {});
//		Assert.Fail();
//	    } catch (ArgumentException) {
//		// this is expected
//	    }
//	}
//	
//	[Test]
//	public void OrNotIn() {
//	    SqlFilter filter = new SqlFilter();
//	
//	    filter.OrNotIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//		
//	    filter = new SqlFilter();
//	    filter.OrNotIn("columnA", new Object[] {"1", "2"});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual("1", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("2", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter();
//	    filter.OrNotIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(2, filter.Parameters.Count);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotIn("columnA", new Object[] {1, 2});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(1, ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(2, ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotIn("columnA", new Object[] {"fee'fie", "fo'fum"});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual("fee'fie", ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual("fo'fum", ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//	
//	    filter = new SqlFilter("foo", "bar");
//	    filter.OrNotIn("columnA", new Object[] {new DateTime(1969, 12, 18), new DateTime(2000, 2, 8)});
//	    Assert.AreEqual(" WHERE foo=@foo OR columnA NOT IN (@columnA1, @columnA2)", filter.Statement);
//	    Assert.AreEqual(3, filter.Parameters.Count);
//	    Assert.AreEqual("bar", ((SqlParameter) filter.Parameters["@foo"]).Value);
//	    Assert.AreEqual(new DateTime(1969, 12, 18), ((SqlParameter) filter.Parameters["@columnA1"]).Value);
//	    Assert.AreEqual(new DateTime(2000, 2, 8), ((SqlParameter) filter.Parameters["@columnA2"]).Value);
//
//	    filter = new SqlFilter("foo", "bar");
//	    try {
//		filter.OrNotIn("columnA", new Object[] {});
//		Assert.Fail();
//	    } catch (ArgumentException) {
//		// this is expected
//	    }
//	}
//    	
//	#endregion
	#endregion    	
    	
    	[Test] 
	public void NewSqlFilterWithPredicate() {
    	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate("foo", EqualityOperatorEnum.Equal, "bar"));
    	    Assert.AreEqual(" WHERE (foo = @foo)", filter.Statement);
    	    Assert.AreEqual(1, filter.Parameters.Count);
	    Assert.IsTrue(filter.Parameters.Contains("@foo"));
	}

    	[Test] 
	public void SqlFilterWithPredicateAndSqlFilter() {
    	    SqlFilter innerFilter = new SqlFilter(new SqlEqualityPredicate("x", EqualityOperatorEnum.Equal, "y"));
    	    innerFilter.And(new SqlBetweenPredicate("tweener", 1, 100));
	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate("foo", EqualityOperatorEnum.Equal, "bar"));
    	    filter.And(innerFilter);
	    Assert.AreEqual(" WHERE ((foo = @foo) AND ((x = @x) AND (tweener BETWEEN @tweener1 AND @tweener2)))", filter.Statement);
	    Assert.AreEqual(4, filter.Parameters.Count);
	    Assert.IsTrue(filter.Parameters.Contains("@foo"));
    	    Assert.IsTrue(filter.Parameters.Contains("@x"));    		
	    Assert.IsTrue(filter.Parameters.Contains("@tweener1"));
	    Assert.IsTrue(filter.Parameters.Contains("@tweener2"));    		
	}
    	
    	[Test]
	public void MultipleAnds() {
	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate("foo", EqualityOperatorEnum.Equal, "bar"));
	    filter.And(new SqlEqualityPredicate("x", EqualityOperatorEnum.Equal, "y"));
    	    filter.And(new SqlBetweenPredicate("tweener", 1, 100));
	    Assert.AreEqual(" WHERE ((foo = @foo) AND (x = @x) AND (tweener BETWEEN @tweener1 AND @tweener2))", filter.Statement);
	    Assert.AreEqual(4, filter.Parameters.Count);
	    Assert.IsTrue(filter.Parameters.Contains("@foo"));
	    Assert.IsTrue(filter.Parameters.Contains("@x"));    		
	    Assert.IsTrue(filter.Parameters.Contains("@tweener1"));
	    Assert.IsTrue(filter.Parameters.Contains("@tweener2"));    		
    	}
    	
    	[Test]
    	[Ignore ("Not yet implemented")]
	public void ShouldHandleMultipleUsesOfSameColumnName() {
    	    // TODO: this test needs to show that 2 values can be added to an expressions, that may be contained
	    // in deeper contained expressions and be referenced correctly and uniquely.
    	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate("foo", EqualityOperatorEnum.GreaterThanOrEqual, DateTime.Today.AddDays(-1)));
    	    filter.And(new SqlEqualityPredicate("foo", EqualityOperatorEnum.LessThanOrEqual, DateTime.Today.AddDays(1)));
	    Assert.AreEqual(" WHERE ((foo >= @a_foo) AND (foo <= @b_foo))", filter.Statement);
	    Assert.AreEqual(2, filter.Parameters.Count);
	    Assert.IsTrue(filter.Parameters.Contains("@a_foo"));
	    Assert.IsTrue(filter.Parameters.Contains("@b_foo"));
	}
    	
    }
}
