using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

using Microsoft.Win32;

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
	    // determine where the connection string comes from and what it is
	    String connectionString;
	    if (key.ToLower().StartsWith("registry")) {
		String subkey = key.Substring(key.LastIndexOf(":")+1);
		subkey = subkey.Substring(0, subkey.LastIndexOf("\\"));
		String value = key.Substring(key.LastIndexOf("\\")+1);
		String hive = key.Substring(9,4).ToUpper();
		RegistryKey rkey;
		if (hive.Equals("HKLM")) {
		    rkey = Registry.LocalMachine.OpenSubKey(subkey);
		} else if (hive.Equals("HKCU")) {
		    rkey = Registry.CurrentUser.OpenSubKey(subkey);
		} else {
		    throw new Exception("Unable to determine hive from registry type connection key.  Hive understood: " + hive + "  Key used was: " + key);
		}
		if (rkey == null) {
		    throw new Exception("Specified subkey was not found.  Subkey: " + subkey);
		}
		connectionString = rkey.GetValue(value).ToString();
	    } else {
		connectionString = ConfigurationSettings.AppSettings[key];
	    }

	    connectionString = connectionString.ToUpper();
	    connectionString = connectionString.Replace("NETWORK=DBMSSOCN;", "Network Library=DBMSSOCN;");
	    connectionString = connectionString.Replace("PROVIDER=MSDASQL;", "");
	    connectionString = connectionString.Replace("DRIVER=SQL SERVER;", "");

	    SqlConnection conn = new SqlConnection(connectionString);
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

	protected static SqlCommand GetSqlCommand(String key, String commandText, CommandType commandType, Int32 commandTimeout) {
	    SqlCommand cmd = GetSqlCommand(key);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    cmd.CommandTimeout = commandTimeout;
	    return cmd;
	}
    }
}
