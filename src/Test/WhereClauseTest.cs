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
	    Assertion.Assert(filter.FormatSql(), filter.FormatSql().Equals(" where column='foo''bar'"));
	}

	[Test]
	public void WhereClause_And() {
	    // make sure that apostrophe is properly escaped
	    WhereClause filter = new WhereClause("foo", "bar");
	    filter.And("column", "foo'bar");
	    Assertion.Assert(filter.FormatSql(), filter.FormatSql().Equals(" where foo='bar' and column='foo''bar'"));
	}
	
	[Test]
	public void WhereClause_Or() {
	    // make sure that apostrophe is properly escaped
	    WhereClause filter = new WhereClause("foo", "bar");
	    filter.Or("column", "foo'bar");
	    Assertion.Assert(filter.FormatSql(), filter.FormatSql().Equals(" where foo='bar' or column='foo''bar'"));
	}

    }
}
