using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;

namespace Spring2.Core.Reporting {
    public class SqlSchemaDAO : Spring2.Core.DAO.SqlEntityDAO {

	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 300;

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
  	    }
	}

	private static SqlObjectData GetSqlObjectDataFromReader(SqlDataReader dataReader) {
	    SqlObjectData data = new SqlObjectData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Name"))) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Type"))) {
		data.Type = StringType.UNSET;
	    } else {
		data.Type = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Type")));
	    }

	    return data;
	}


	private static SqlColumnData GetSqlColumnDataFromReader(SqlDataReader dataReader) {
	    SqlColumnData data = new SqlColumnData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Name"))) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Type"))) {
		data.Type = StringType.UNSET;
	    } else {
		data.Type = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Type")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SqlObject"))) {
		data.SqlObject = StringType.UNSET;
	    } else {
		data.SqlObject = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("SqlObject")));
	    }

	    return data;
	}

	public static IList GetSqlObjects(IWhere whereClause, IOrderBy orderByClause) {
	    SqlDataReader dataReader = (SqlDataReader)(new SqlSchemaDAO().GetListReader(CONNECTION_STRING_KEY, "vwSqlObject", whereClause, orderByClause));

	    ArrayList list = new ArrayList();
	    while (dataReader.Read()) {
		list.Add(GetSqlObjectDataFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	public static IList<SqlColumnData> GetSqlColumns(IWhere whereClause, IOrderBy orderByClause) {
	    SqlDataReader dataReader = (SqlDataReader)(new SqlSchemaDAO().GetListReader(CONNECTION_STRING_KEY, "vwSqlColumn", whereClause, orderByClause));

	    IList<SqlColumnData> list = new List<SqlColumnData>();
	    while (dataReader.Read()) {
		list.Add(GetSqlColumnDataFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	public static SqlDataReader ExecuteReader(String sql) {
	    SqlDataReader reader = (SqlDataReader)(new SqlSchemaDAO().ExecuteReader(CONNECTION_STRING_KEY, sql, COMMAND_TIMEOUT));
	    return reader;
	}

    }
}