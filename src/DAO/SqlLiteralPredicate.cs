using System;
using System.Data;
using System.Data.SqlClient;

namespace Spring2.Core.DAO {
    /// <summary>
    /// Summary description for SqlLiteralPredicate.
    /// </summary>
    public class SqlLiteralPredicate : SqlPredicate {
    	
    	private String value;
    	
	public SqlLiteralPredicate(String value) {
	    this.value = value;
	}
    	
	public override String Expression {
	    get {
	    	return " " + value;
	    }
	}

	public override IDataParameterCollection Parameters {
	    get {
	    	return new SqlParameterList();
	    }
	}

    }
}
