using System;

using NUnit.Framework;
using Spring2.Core.DAO;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for IWhere functionality
    /// </summary>
    [TestFixture]
    public class WhereClauseTest {

	[Test]
	public void WhereClause() {
	    // make sure an empty object does not emits an empty clause
	    WhereClause filter = new WhereClause();
	    Assertion.Assert(filter.FormatSql(), filter.FormatSql().Equals(String.Empty));
	    
	    // make sure that apostrophe is properly escaped
	    filter = new WhereClause("column", "foo'bar");
	    Assertion.AssertEquals(" WHERE column='foo''bar'", filter.FormatSql());

	    // Check building where clause from string
	    filter = new WhereClause("c1 = 57 and [user].[plan] = 'goodone'");
	    Assertion.Assert(filter.FormatSql(), filter.FormatSql().Equals(" WHERE c1 = 57 and [user].[plan] = 'goodone'"));

	    // Check building where clause from where clause
	    WhereClause filter1 = new WhereClause(filter);
	    Assertion.Assert(filter.FormatSql() + " doesn't equal " + filter1.FormatSql(), filter1.FormatSql().Equals(" WHERE (c1 = 57 and [user].[plan] = 'goodone') "));
	}

	[Test]
	public void WhereClause_And() {
	    // make sure that apostrophe is properly escaped
	    WhereClause filter = new WhereClause("foo", "bar");
	    filter.And("column", "foo'bar");
	    Assertion.AssertEquals(" WHERE foo='bar' AND column='foo''bar'", filter.FormatSql());
	}
	
	[Test]
	public void WhereClause_Or() {
	    // make sure that apostrophe is properly escaped
	    WhereClause filter = new WhereClause("foo", "bar");
	    filter.Or("column", "foo'bar");
	    Assertion.AssertEquals(" WHERE foo='bar' OR column='foo''bar'", filter.FormatSql());
	}

	[Test]
	public void Constructors() {
	    WhereClause filter = new WhereClause();
	    Assertion.Assert(filter.IsEmpty == true);
	    
	    filter = new WhereClause("columnA", 1);
	    filter.And("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());

	    filter = new WhereClause("columnA", 1);
	    filter.AndEquals("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());

	    filter = new WhereClause("columnA", "foo'bar");
	    Assertion.AssertEquals(" WHERE columnA='foo''bar'", filter.FormatSql());
	}

	[Test]
	public void And() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", 1);
	    filter.And("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());

	    filter = new WhereClause("columnA", "foo'bar");
	    filter.And("columnB", "fee'fie");
	    Assertion.AssertEquals(" WHERE columnA='foo''bar' AND columnB='fee''fie'", filter.FormatSql());
	}

	[Test]
	public void AndEquals() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", 1);
	    filter.AndEquals("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());

	    filter = new WhereClause("columnA", "foo'bar");
	    filter.AndEquals("columnB", "fee'fie");
	    Assertion.AssertEquals(" WHERE columnA='foo''bar' AND columnB='fee''fie'", filter.FormatSql());
	}

	[Test]
	public void AndLike() 
	{
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", "a'%");
	    filter.AndLike("columnB", "b'%");
	    Assertion.AssertEquals(" WHERE columnA='a''%' AND columnB like 'b''%'", filter.FormatSql());
	}

	[Test]
	public void OrLike() 
	{
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", "a'%");
	    filter.OrLike("columnB", "b'%");
	    Assertion.AssertEquals(" WHERE columnA='a''%' OR columnB like 'b''%'", filter.FormatSql());
	}


	[Test]
	public void AndNotLike() 
	{
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", "a'%");
	    filter.AndNotLike("columnB", "b'%");
	    Assertion.AssertEquals(" WHERE columnA='a''%' AND columnB not like 'b''%'", filter.FormatSql());
	}


	[Test]
	public void OrNotLike() 
	{
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", "a'%");
	    filter.OrNotLike("columnB", "b'%");
	    Assertion.AssertEquals(" WHERE columnA='a''%' OR columnB not like 'b''%'", filter.FormatSql());
	}

	[Test]
	public void AndBetween() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause();
	    filter.AndBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE columnA between 1 and 2", filter.FormatSql());

	    filter = new WhereClause();
	    filter.AndBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE columnA between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause();
	    filter.AndBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE columnA between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());


	    filter = new WhereClause("foo", "bar");
	    filter.AndBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA between 1 and 2", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.AndBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.AndBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());
	}

	[Test]
	public void AndNotBetween() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause();
	    filter.AndNotBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE columnA NOT between 1 and 2", filter.FormatSql());

	    filter = new WhereClause();
	    filter.AndNotBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE columnA NOT between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause();
	    filter.AndNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE columnA NOT between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.AndNotBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA NOT between 1 and 2", filter.FormatSql());

	    filter = new WhereClause("foo", "foo'bar");
	    filter.AndNotBetween("columnA", "fee'fie", "fo'fum");
	    Assertion.AssertEquals(" WHERE foo='foo''bar' AND columnA NOT between 'fee''fie' and 'fo''fum'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.AndNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA NOT between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());
	}

	[Test]
	public void OrBetween() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause();
	    filter.OrBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE columnA between 1 and 2", filter.FormatSql());

	    filter = new WhereClause();
	    filter.OrBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE columnA between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause();
	    filter.OrBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE columnA between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA between 1 and 2", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrBetween("columnA", "fee'fie", "fo'fum");
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA between 'fee''fie' and 'fo''fum'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());
	}

	[Test]
	public void OrNotBetween() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause();
	    filter.OrNotBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE columnA NOT between 1 and 2", filter.FormatSql());

	    filter = new WhereClause();
	    filter.OrNotBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE columnA NOT between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause();
	    filter.OrNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE columnA NOT between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrNotBetween("columnA", 1, 2);
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA NOT between 1 and 2", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrNotBetween("columnA", "fee'fie", "fo'fum");
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA NOT between 'fee''fie' and 'fo''fum'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA NOT between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());
	}


    }
}
