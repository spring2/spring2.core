using System;

namespace Spring2.Core.DAO {
	public class WhereClause : IWhere {

		private String sql;

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
			return " where " + sql;
		}
	}
}
