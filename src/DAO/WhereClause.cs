using System;

namespace Spring2.Core.DAO {
    public class WhereClause : IWhere {

	private String sql = String.Empty;

	public WhereClause() {
	}

	public WhereClause(String field, Int32 value) {
	    sql = field + "=" + value.ToString();
	}

	public WhereClause(String field, String value) {
	    sql = field + "='" + value + "'";
	}

	public WhereClause(String field, DateTime value) {
	    sql = field + "='" + value.ToString() + "'";
	}

	public WhereClause(String sql) {
	    this.sql = sql;
	}

	public String FormatSql() {
	    if (!sql.Equals(String.Empty)) {
		return " where " + sql;
	    } else {
		return String.Empty;
	    }
	}

	public void And(String field, Int32 value) {
	    if (!sql.Equals(String.Empty)) {
		sql += " and ";
	    }
	    sql += field + "=" + value.ToString();
	}

	public void And(String field, String value) {
	    if (!sql.Equals(String.Empty)) {
		sql += " and ";
	    }
	    sql += field + "='" + value + "'";
	}

	public void And(String field, DateTime value) {
	    if (!sql.Equals(String.Empty)) {
		sql += " and ";
	    }
	    sql += field + "='" + value.ToString() + "'";
	}

	public void And(String field, Object value) {
	    if (value is Int32) {
		And(field, (Int32)value);
	    } else if (value is DateTime) {
		And(field, (DateTime)value);
	    } else {
		And(field, value.ToString());
	    }
	}

    }
}
