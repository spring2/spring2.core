using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;

using Microsoft.Win32;

namespace Spring2.Core.DAO {

    public abstract class BaseEntityDAO {

	private static StringDictionary connectionStrings = new StringDictionary();

	protected IDataReader GetListReader(String key, String viewName) { 
	    return GetListReader(key, viewName, null, null); 
	} 
		 
	protected IDataReader GetListReader(String key, String viewName, IWhere whereClause, IOrderBy orderByClause) { 
	    return GetListReader(key, viewName, whereClause, orderByClause, -1);
	}

	protected IDataReader GetListReader(String key, String viewName, IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) {
	    return GetListReader(key, viewName, whereClause, orderByClause, maxRows, -1);
	}

	protected IDataReader GetListReader(String key, String viewName, IWhere whereClause, IOrderBy orderByClause, Int32 maxRows, Int32 commandTimeout) {
	    String sql = "";
	    if (maxRows < 1) {
		sql = "select * from " + viewName;
	    }
	    else {
		sql = "select top " + maxRows.ToString() + " * from " + viewName;
	    }
	    if (whereClause != null) { 
		sql = sql + whereClause.FormatSql(); 
	    } 
	    if (orderByClause != null) { 
		sql = sql + orderByClause.FormatSql(); 
	    } 
			 
	    return ExecuteReader(key, sql, commandTimeout);
	}

	protected IDataReader ExecuteReader(String key, String sql) {
	    return ExecuteReader(key, sql, -1);
	}

	protected IDataReader ExecuteReader(String key, String sql, Int32 commandTimeout) {
	    IDbCommand cmd = GetDbCommand(key);
	    cmd.CommandType = CommandType.Text;
	    cmd.CommandText = sql;
	    if (commandTimeout > 0) {
		cmd.CommandTimeout = commandTimeout;
	    }
			 
	    try { 
		IDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		return reader;
	    } catch (Exception ex) { 
		throw new DaoException(ex.Message + " [running statement: " + sql + "]", ex);
	    }
	}

	protected abstract IDbCommand CreateCommand();

	protected IDbCommand GetDbCommand(String key) {
	    IDbConnection conn = GetDbConnection(key);
	    IDbCommand cmd = CreateCommand();
	    cmd.Connection = conn;	    
	    return cmd;
	}


	protected abstract IDbCommand GetDbCommand(String key, IDbTransaction transaction);

	protected IDbCommand GetDbCommand(String key, String commandText, CommandType commandType) {
	    IDbCommand cmd = GetDbCommand(key);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    return cmd;
	}

	protected IDbCommand GetDbCommand(String key, String commandText, CommandType commandType, Int32 commandTimeout) {
	    IDbCommand cmd = GetDbCommand(key);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    cmd.CommandTimeout = commandTimeout;
	    return cmd;
	}

	protected IDbCommand GetDbCommand(String key, String commandText, CommandType commandType, Int32 commandTimeout, IDbTransaction transaction) {
	    IDbCommand cmd = GetDbCommand(key, transaction);
	    cmd.CommandText = commandText;
	    cmd.CommandType = commandType;
	    cmd.CommandTimeout = commandTimeout;
	    return cmd;
	}

	protected abstract IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction, Object value);

	protected abstract IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction);

//	protected abstract IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, Int32 size, ParameterDirection direction, Boolean isNullable, Byte precision, Byte scale, String sourceColumn, DataRowVersion sourceVersion, Object value);

	protected IDbConnection GetDbConnection(String key) {
	    String connectionString = GetConnectionString(key);
	    IDbConnection conn = CreateConnection(connectionString);
	    conn.Open();
	    return conn;
	}

	protected abstract IDbConnection CreateConnection(String connectionString);

	protected IDbConnection GetDbConnection() {
	    return GetDbConnection(ConnectionStringKey);
	}

	protected DaoTransaction GetDbTransaction(String key) {
	    IDbConnection conn = GetDbConnection(key);
	    return new DaoTransaction(conn.BeginTransaction());
	}

	public DaoTransaction GetDbTransaction() {
	    return GetDbTransaction(ConnectionStringKey);
	}

	protected abstract String ConnectionStringKey {
	    get;
	}

	private static String GetConnectionString(String key) {
	    String connectionString;

	    // Try cache first
	    if (connectionStrings[key]!= null) {
		return connectionStrings[key];
	    }

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

	    // Cache the connection string by key for fast lookup later.
	    connectionStrings.Add(key, connectionString);

	    return connectionString;
	}

	protected static String ProcessExpression(Hashtable propertyToSqlMap, String expression) {
	    String checkExpression = expression;
	    String retVal = String.Empty;
	    Int32 leftBrace = 0;
	    Int32 startPos = 0;
	    for (leftBrace = checkExpression.IndexOf("{", startPos); startPos >= 0; leftBrace = checkExpression.IndexOf("{", startPos)) {
		if (leftBrace == -1) {
		    // No more strings to replace.
		    retVal += checkExpression.Substring(startPos, checkExpression.Length - startPos);
		    break;
		} else {
		    // Concatenate portion of string without embedded references.
		    retVal += checkExpression.Substring(startPos, leftBrace - startPos);
		}
		Int32 rightBrace = checkExpression.IndexOf("}", leftBrace);
		if (rightBrace == -1) {
		    throw new ApplicationException("Where clause contains a left brace ({) with no corresponding right brace(}).");
		}

		// Isolate the property reference and concatenate it's expansion.
		String expressionReference = checkExpression.Substring(leftBrace+1, rightBrace - leftBrace - 1);
		retVal += GetPropertyMapping(propertyToSqlMap, expressionReference);
		
		// On to the next reference.
		startPos = rightBrace + 1;
	    }

	    return retVal;
	}

	protected static String GetPropertyMapping(Hashtable propertyToSqlMap, String propertyName) {
	    if (!propertyToSqlMap.ContainsKey(propertyName)) {
		throw new ApplicationException("Property " + propertyName + " not found for where clause.");
	    }
	    return "(" + (String)(propertyToSqlMap[propertyName]) + ")";
	}
    }
}
