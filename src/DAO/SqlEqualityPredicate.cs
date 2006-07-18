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
	    SetInternalValues(columnName, _operator, value, SqlDbType.BigInt);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Int32 value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Int);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Int16 value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.SmallInt);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, DateTime value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.DateTime);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, String value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.VarChar);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Decimal value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Decimal);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Double value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Float);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Single value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Real);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Boolean value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Bit);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Byte value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.TinyInt);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Byte[] value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.Image);
	}
    	
	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Guid value) {
	    SetInternalValues(columnName, _operator, value, SqlDbType.UniqueIdentifier);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, DBNull value) {
	    SetInternalValues(columnName, _operator, null, SqlDbType.Variant);
	}

	public SqlEqualityPredicate(String columnName, EqualityOperatorEnum _operator, Object value) {
	    if (value is DBNull) {
		SetInternalValues(columnName, _operator, null, SqlDbType.Variant);
	    } else if (value is Int64) {
		SetInternalValues(columnName, _operator, value, SqlDbType.BigInt);
	    } else if (value is Int32) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Int);
	    } else if (value is Int16) {
		SetInternalValues(columnName, _operator, value, SqlDbType.SmallInt);
	    } else if (value is DateTime) {
		SetInternalValues(columnName, _operator, value, SqlDbType.DateTime);
	    } else if (value is String) {
		SetInternalValues(columnName, _operator, value, SqlDbType.VarChar);
	    } else if (value is Decimal) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Decimal);
	    } else if (value is Double) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Float);
	    } else if (value is Single) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Real);
	    } else if (value is Boolean) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Bit);
	    } else if (value is Byte) {
		SetInternalValues(columnName, _operator, value, SqlDbType.TinyInt);
	    } else if (value is Byte[]) {
		SetInternalValues(columnName, _operator, value, SqlDbType.Image);
	    } else if (value is Guid) {
		SetInternalValues(columnName, _operator, value, SqlDbType.UniqueIdentifier);
	    } else {
	    	throw new ArgumentException(value.GetType().ToString() + " is not an allowed type");
	    }
	}		
    	
    	private void SetInternalValues(String columnName, EqualityOperatorEnum _operator, Object value, SqlDbType dbType) {
	    this.columnName = columnName;
	    this._operator = _operator;
	    this.value = value;
	    this.dbType = dbType;
    	}

	public override String Expression {
	    get {
		if (_operator.Equals(EqualityOperatorEnum.Equal) && value == null) {
		    return String.Format("({0} IS NULL)", Escape(columnName));
		} else if (_operator.Equals(EqualityOperatorEnum.NotEqual) && value == null) {
		    return String.Format("({0} IS NOT NULL)", Escape(columnName));
		} else {
		    return String.Format("({0} {1} @{2})", Escape(columnName), GetSqlOperator(_operator), ReplaceSpecialCharacters(columnName).Replace(".", "_"));
		}
	    }
	}

	public override IDataParameterCollection Parameters {
	    get {
		SqlParameterList parameters = new SqlParameterList();
		if (_operator.Equals(EqualityOperatorEnum.Equal) && value == null) {
		    // do not add parameter
		} else if (_operator.Equals(EqualityOperatorEnum.NotEqual) && value == null) {
		    // do not add parameter
		} else {
		    parameters.Add("@" + ReplaceSpecialCharacters(columnName).Replace(".","_"), dbType, value);
		}
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
