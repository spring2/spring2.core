using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Spring2.Core.DAO {
	public abstract class EntityDAO { 

		public EntityDAO() {
		}

		protected abstract String GetViewName();

		protected SqlDataReader GetListReader(SqlConnection connection) { 
			return GetListReader(null, null, connection); 
		} 
		 
		protected SqlDataReader GetListReader(IWhere whereClause, IOrderBy orderByClause, SqlConnection connection) { 
			try { 
				String sql = "select * from " + GetViewName();
				if (whereClause != null) { 
					sql = sql + whereClause.FormatSql(); 
				} 
				if (orderByClause != null) { 
					sql = sql + orderByClause.FormatSql(); 
				} 
			 
				return ExecuteReader(sql, connection);
			} catch (Exception ex) { 
				// log exception
				throw ex;
			} 
		}

		protected SqlDataReader ExecuteReader(String sql, SqlConnection connection) { 
			SqlCommand cmd = GetSqlCommand(connection);
			 
			try { 
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = sql;
				return cmd.ExecuteReader(CommandBehavior.CloseConnection);
			} catch (Exception ex) { 
				// log exception
				throw ex;
			} 
		}


		public ICollection GetList(SqlConnection connection) {
			return GetList(null, null, connection);
		}

		public ICollection GetList(IWhere whereClause, SqlConnection connection) {
			return GetList(whereClause, null, connection);
		}

		public abstract ICollection GetList(IWhere whereClause, IOrderBy orderByClause, SqlConnection connection);


		protected SqlCommand GetSqlCommand(SqlConnection conn) {
			SqlCommand cmd;

			// Create and open the database connection - if the one passed in was not null
			if (conn == null) {
				conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"]);
				conn.Open();
			}

			// Create and execute the command
			cmd = new SqlCommand();
			cmd.Connection = conn;

			return cmd;
		}

		protected SqlCommand GetSqlCommand(SqlConnection conn, String commandText, CommandType commandType) {
			SqlCommand cmd = GetSqlCommand(conn);
			cmd.CommandText = commandText;
			cmd.CommandType = commandType;
			return cmd;
		}

	}
}
