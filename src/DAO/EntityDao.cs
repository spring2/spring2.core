using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace Spring2.Core.DAO {
 
    public abstract class EntityDAO { 

	protected static SqlDataReader GetListReader(String key, String viewName) { 
	    return GetListReader(key, viewName, null, null); 
	} 
		 
	protected static SqlDataReader GetListReader(String key, String viewName, IWhere whereClause, IOrderBy orderByClause) { 
	    try { 
		String sql = "select * from " + viewName;
		if (whereClause != null) { 
		    sql = sql + whereClause.FormatSql(); 
		} 
		if (orderByClause != null) { 
		    sql = sql + orderByClause.FormatSql(); 
		} 
			 
		return ExecuteReader(key, sql);
	    } catch (Exception ex) { 
		// log exception
		throw ex;
	    } 
	}

	protected static SqlDataReader ExecuteReader(String key, String sql) { 
	    SqlCommand cmd = GetSqlCommand(key);
			 
	    try { 
		cmd.CommandType = CommandType.Text;
		cmd.CommandText = sql;
		return cmd.ExecuteReader(CommandBehavior.CloseConnection);
	    } catch (Exception ex) { 
		// log exception
		throw ex;
	    } 
	}

	protected static SqlCommand GetSqlCommand(String key) {
	    SqlConnection conn = new SqlConnection(ConfigurationSettings.AppSettings[key]);
	    conn.Open();

	    SqlCommand cmd = new SqlCommand();
	    cmd.Connection = conn;

	    return cmd;
	}

	protected static SqlCommand GetSqlCommand(String key, String commandText, CommandType commandType) {
	    SqlCommand cmd = GetSqlCommand(key);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    return cmd;
	}

    }
}
