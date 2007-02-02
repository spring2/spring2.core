using System;
using System.Data;
using System.Data.SqlClient;

namespace Spring2.Core.DAO {

    public abstract class SqlEntityDAO : BaseEntityDAO {

	protected override IDbCommand GetDbCommand(String key, IDbTransaction transaction) {

	    if (transaction == null) {
		return GetDbCommand(key);
	    } else {
		// for now, since there is no functionality for enlisting a connection in a distributed transaction,
		// throw an exception if the connection string is not the same
		//TODO: this will not be the same if there is a password in it
		//		if (!transaction.Connection.ConnectionString.Equals(key)) {
		//		    throw new DaoException("Connection string from transaction (" + transaction.Connection.ConnectionString + ") does not match connection string (" + connectionString + ") retrieved by key (" + key + ")");
		//		}

		IDbCommand cmd = new SqlCommand();
		cmd.Connection = transaction.Connection;
		cmd.Transaction = transaction;
		return cmd;
	    }
	}

	protected override IDbCommand CreateCommand() {
	    return new SqlCommand();
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction, Object value) {
	    SqlParameter param = new SqlParameter(parameterName, value);
	    param.DbType = dbType;
	    param.Direction = direction;
	    return param;
	}

	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, ParameterDirection direction) {
	    SqlParameter param = new SqlParameter(parameterName, SqlDbType.NVarChar);
	    param.DbType = dbType;
	    param.Direction = direction;
	    return param;
	}

//	protected override IDbDataParameter CreateDataParameter(String parameterName, DbType dbType, Int32 size, ParameterDirection direction, Boolean isNullable, Byte precision, Byte scale, String sourceColumn, DataRowVersion sourceVersion, Object value) {
//	    SqlParameter param = new SqlParameter(parameterName, SqlDbType.NVarChar, size, direction, isNullable, precision, scale, sourceColumn, sourceVersion, value);
//	    param.DbType = dbType;
//	    return param;
//	}

	protected override IDbConnection CreateConnection(String connectionString) {
#if (NET_1_1)
	    IDbConnection connection = new SqlConnection(connectionString);
	    connection.Open();
	    return connection;
#else
	    // check to see if there is a current connection scope and pull connection from that, otherwise create and open a new one
	    if (DbConnectionScope.Current != null) {
		return DbConnectionScope.Current.GetOpenConnection(SqlClientFactory.Instance, connectionString);
	    } else {
		IDbConnection connection = new SqlConnection(connectionString);
		connection.Open();
		return connection;
	    }
#endif
	}
    }
}
