using System;
using System.Data;
using System.Data.SqlClient;

namespace Spring2.Core.DAO {
    /// <summary>
    /// Summary description for SqlEqualityPredicate.
    /// </summary>
    public class SqlEqualityPredicate : SqlPredicate {
    	
    	private EqualityOperatorEnum _operator;
    	private String columnName;
    	private Object value;
    	private SqlDbType dbType;
    		
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Int64 value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.BigInt;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Int32 value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Int;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Int16 value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.SmallInt;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, DateTime value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.DateTime;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, String value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.VarChar;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Decimal value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Decimal;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Double value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Float;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Single value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Real;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Boolean value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Bit;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Byte value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.TinyInt;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Byte[] value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.Image;
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Guid value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    dbType = SqlDbType.UniqueIdentifier;
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, DBNull value) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = null;
	    dbType = SqlDbType.Variant;
	}


	public override String Expression {
	    get {
		return String.Format("({0} {1} @{0})", columnName, GetSqlOperator(_operator));
	    }
	}

	public override IDataParameterCollection Parameters {
	    get {
		SqlParameterList parameters = new SqlParameterList();
		parameters.Add("@" + columnName, dbType, value);
		return parameters;
	    }
	}

	private String GetSqlOperator(EqualityOperatorEnum op) {
	    switch (op) {
	    	case EqualityOperatorEnum.Equal :
	    	    return "=";
		case EqualityOperatorEnum.NotEqual :
		    return "<>";
	    	case EqualityOperatorEnum.LessThan :
	    	    return "<";
	    	case EqualityOperatorEnum.LessThanOrEqual :
	    	    return "<=";
	    	case EqualityOperatorEnum.GreaterThan :
	    	    return ">";
	    	case EqualityOperatorEnum.GreaterThanOrEqual :
	    	    return ">=";
	    	case EqualityOperatorEnum.Like :
	    	    return "LIKE";
	    	case EqualityOperatorEnum.NotLike :
	    	    return "NOT LIKE";
		default:
		    throw new ArgumentOutOfRangeException();
	    }
	}
    }
}
