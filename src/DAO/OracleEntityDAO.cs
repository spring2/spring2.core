using System;
using System.Data;
using System.Data.OracleClient;

namespace Spring2.Core.DAO {
 
    public class OracleEntityDAO : BaseEntityDAO { 

	protected override IDbCommand GetDbCommand(String key, IDbTransaction transaction) {

	    String connectionString = GetConnectionString(key);
	    
	    if (transaction == null) {
		return GetDbCommand(key);
	    } else {
		IDbCommand cmd = new OracleCommand();
		cmd.Connection = transaction.Connection;
		cmd.Transaction = transaction;
		return cmd;
	    }
	}

	protected override IDbCommand GetDbCommand(String key) {
	    String connectionString = GetConnectionString(key);	    
	    OracleConnection conn = new OracleConnection(connectionString);
	    conn.Open();
	    OracleCommand cmd = new OracleCommand();
	    cmd.Connection = conn;
	    
	    return cmd;
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, ParameterDirection direction, Object value) {
	    OracleParameter param = new OracleParameter(parameterName, value);
	    param.Direction = direction;
	    return param;
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction) {
	    OracleParameter param = new OracleParameter(parameterName, OracleType.NVarChar);
	    param.DbType = dbType;
	    param.Direction = direction;
	    return param;
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, Int32 size, ParameterDirection direction, Boolean isNullable, Byte precision, Byte scale, String sourceColumn, DataRowVersion sourceVersion, Object value) {
	    OracleParameter param = new OracleParameter(parameterName, OracleType.NVarChar, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
	    param.DbType = dbType;
	    return param;
	}

	protected override IDbConnection GetDbConnection(String connectionString) {
	    OracleConnection conn = new OracleConnection(connectionString);
	    conn.Open();
	    return conn;
	}
    }
}
