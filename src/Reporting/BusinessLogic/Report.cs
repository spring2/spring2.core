using System;
using System.Collections;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

namespace Spring2.Reporting.Common {
    public class Report {
	private Report() {
	}

	public static IList GetISqlReports(StringType prefix) {
	    return SqlSchemaDAO.GetSqlObjects(new WhereClause("type='P' and name like '" + prefix.ToString() + "%'"), new OrderByClause("Name"));
	}

	public static IList GetISqlReportArguments(StringType sqlObject) {
	    return SqlSchemaDAO.GetSqlColumns(new WhereClause("SqlObject", sqlObject.ToString()), new OrderByClause("OrdinalPosition"));
	}

	public static SqlDataReader ExecuteISqlReport(StringType sqlObject, Hashtable args) {
	    String sql = String.Empty;
	    bool isNumeric;
	    decimal d;

	    foreach(DictionaryEntry arg in args) {
		if (!sql.Equals(String.Empty)) {
		    sql += ", ";
		}

		try {
		    isNumeric = true;
		    d = Convert.ToDecimal(arg.Value.ToString());
		} catch (System.FormatException){
		    isNumeric = false;
		}

		if(!isNumeric) {
		    sql += arg.Key.ToString() + "='" + arg.Value.ToString() + "'";
		} else {
		    sql += arg.Key.ToString() + "=" + arg.Value.ToString();
		}
	    }
	
	    sql = "exec " + sqlObject + " " + sql;
	    return SqlSchemaDAO.ExecuteReader(sql);
	}

    }
}
