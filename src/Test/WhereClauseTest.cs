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
	}

	[Test]
	public void And() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", 1);
	    filter.And("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());
	}

	[Test]
	public void AndEquals() {
	    WhereClause filter = new WhereClause();

	    filter = new WhereClause("columnA", 1);
	    filter.AndEquals("columnB", 2);
	    Assertion.AssertEquals(" WHERE columnA=1 AND columnB=2", filter.FormatSql());
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

	    filter = new WhereClause("foo", "bar");
	    filter.AndNotBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE foo='bar' AND columnA NOT between '1' and '2'", filter.FormatSql());

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
	    filter.OrBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA between '1' and '2'", filter.FormatSql());

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
	    filter.OrNotBetween("columnA", "1", "2");
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA NOT between '1' and '2'", filter.FormatSql());

	    filter = new WhereClause("foo", "bar");
	    filter.OrNotBetween("columnA", new DateTime(1969, 12, 18), new DateTime(2000, 2, 8));
	    Assertion.AssertEquals(" WHERE foo='bar' OR columnA NOT between '12/18/1969 12:00:00 AM' and '2/8/2000 12:00:00 AM'", filter.FormatSql());
	}


    }
}
