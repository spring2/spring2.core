using System;
using System.Data;
using System.Data.SqlClient;

namespace Spring2.Core.DAO {
    /// <summary>
    /// Summary description for SqlBetweenPredicate.
    /// </summary>
    public class SqlBetweenPredicate : SqlPredicate {
    	
    	private String columnName;
    	private Object value1;
    	private Object value2;
    	private Boolean not = false;
    	private SqlDbType dbType;
    	
	public SqlBetweenPredicate(String columnName, Int64 value1, Int64 value2) {
	    this.columnName = columnName;
	    this.value1 = value1;
	    this.value2 = value2;
	    dbType = SqlDbType.BigInt;
	}
    	
	public SqlBetweenPredicate(String columnName, Int64 value1, Int64 value2, Boolean not) {
	    this.columnName = columnName;
	    this.value1 = value1;
	    this.value2 = value2;
	    this.not = not;
	    dbType = SqlDbType.BigInt;
	}

    	public override String Expression {
	    get {
		return String.Format("({0} {1}BETWEEN @{0}1 AND @{0}2)", columnName, not ? "NOT " : String.Empty);
	    }
	}

	public override IDataParameterCollection Parameters {
	    get {
		SqlParameterList parameters = new SqlParameterList();
		parameters.Add("@" + columnName + "1", dbType, value1);
		parameters.Add("@" + columnName + "2", dbType, value2);
		return parameters;
	    }
	}

    }
}
