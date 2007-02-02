using System;
using System.Data;
using System.Data.OracleClient;

namespace Spring2.Core.DAO {
 
    public abstract class OracleEntityDAO : BaseEntityDAO { 

	protected override IDbCommand GetDbCommand(String key, IDbTransaction transaction) {

	    if (transaction == null) {
		return GetDbCommand(key);
	    } else {
		IDbCommand cmd = new OracleCommand();
		cmd.Connection = transaction.Connection;
		cmd.Transaction = transaction;
		return cmd;
	    }
	}

	protected override IDbCommand CreateCommand() {
	    return new OracleCommand();
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction, Object value) {
	    OracleParameter param = new OracleParameter(parameterName, value);
	    param.DbType = dbType;
	    param.Direction = direction;
	    return param;
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction) {
	    OracleParameter param = new OracleParameter(parameterName, OracleType.NVarChar);
	    param.DbType = dbType;
	    param.Direction = direction;
	    return param;
	}

	protected override IDbConnection CreateConnection(String connectionString) {
	    IDbConnection connection = new OracleConnection(connectionString);
	    connection.Open();
	    return connection;
	}
    }
}
