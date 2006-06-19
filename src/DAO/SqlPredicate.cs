using System;
using System.Data;

namespace Spring2.Core.DAO {
	
    /// <summary>
    /// Summary description for SqlPredicate.
    /// </summary>
    public abstract class SqlPredicate : IDatabaseExpression {
    	
    	public abstract string Expression {
    	    get;
    	}

    	public abstract IDataParameterCollection Parameters {
    	    get;
    	}
    	
    }
}
