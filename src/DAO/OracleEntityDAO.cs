using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace Spring2.Core.DAO {
 
    public abstract class OracleEntityDAO { 

	protected static IDataReader GetListReader(String key, String viewName) { 
	    return GetListReader(key, viewName, null, null); 
	} 
		 
	protected static IDataReader GetListReader(String key, String viewName, IWhere whereClause, IOrderBy orderByClause) { 

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

	protected static IDataReader ExecuteReader(String key, String sql) { 

	    IDbCommand cmd = GetCommand(key);			 
	    try { 
		cmd.CommandType = CommandType.Text;
		cmd.CommandText = sql;
		return cmd.ExecuteReader(CommandBehavior.CloseConnection);
	    } catch (Exception ex) { 
		// log exception
		throw ex;
	    } 
	}

	protected static IDbCommand GetCommand(String key) {

	    String connectionString = ConfigurationSettings.AppSettings[key];
	    if (connectionString == null) {
		throw new ConfigurationException("No app setting found for key: " + key + ".  Check Web.config for this value.");
	    }
	    OracleConnection conn = new OracleConnection(connectionString);
	    conn.Open();
	    OracleCommand cmd = new OracleCommand();
	    cmd.Connection = conn;

	    return cmd;
	}

	protected static IDbCommand GetCommand(String key, String commandText, CommandType commandType) {

	    IDbCommand cmd = GetCommand(key);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    return cmd;
	}
    }
}
