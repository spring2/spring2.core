using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Spring2.Core.DAO {
    /// <summary>
    /// Summary description for SqlFilter.
    /// </summary>
    public class SqlFilter : DatabaseFilter, IDatabaseExpression {
    	
    	private struct LogicalExpression {
    	    public LogicalOperatorEnum Operator;
    	    public IDatabaseExpression Expression;
    		
    	    public LogicalExpression(IDatabaseExpression expression) {
    	    	Expression = expression;
    		Operator = LogicalOperatorEnum.None;
    	    }
	    public LogicalExpression(LogicalOperatorEnum op, IDatabaseExpression expression) {
		Expression = expression;
		Operator = op;
	    }
	}

    	private ArrayList expressions = new ArrayList();
    	
	public SqlFilter() {
	}
    	
	public SqlFilter(IDatabaseExpression expression) {
	    expressions.Add(new LogicalExpression(expression));
	}

	public override String Statement {
	    get {
		// TODO:  this should probably be done a better way, this will cause the building of the expression twice
		if (Expression.Length == 0) {
		    return String.Empty;
		}

		return " WHERE " + Expression;
	    }
	}
	
	#region old code
	public override string InnerStatement {
	    get { throw new NotImplementedException(); }
	}

	
	//	public override String InnerStatement {
	//	    get {
	//		if (Fragments.Count == 0) {
	//			return String.Empty;
	//		}
	//
	//		StringBuilder sb = new StringBuilder();
	//		foreach (FilterClause fragment in Fragments) {
	//		    if (sb.Length > 0) {
	//			switch (fragment.Conjunction) {
	//			    case FilterConjunction.And :
	//			    case FilterConjunction.AndEquals :
	//			    case FilterConjunction.AndNotEquals :
	//			    case FilterConjunction.AndBetween :
	//			    case FilterConjunction.AndNotBetween :
	//			    case FilterConjunction.AndIn :
	//			    case FilterConjunction.AndNotIn :
	//			    case FilterConjunction.AndLike :
	//			    case FilterConjunction.AndNotLike :
	//				sb.Append(" AND ");
	//				break;
	//			    case FilterConjunction.Or :
	//			    case FilterConjunction.OrEquals :
	//			    case FilterConjunction.OrNotEquals :
	//			    case FilterConjunction.OrBetween :
	//			    case FilterConjunction.OrNotBetween :
	//			    case FilterConjunction.OrIn :
	//			    case FilterConjunction.OrNotIn :
	//			    case FilterConjunction.OrLike :
	//			    case FilterConjunction.OrNotLike :
	//				sb.Append(" OR ");
	//				break;
	//			    default :
	//				throw new ArgumentException("Unexpected filter conjunction");
	//			}
	//		    }
	//
	//		    if (!(fragment.Value is DatabaseFilter || fragment.Key == null)) {
	//			sb.Append(fragment.Key);
	//		    
	//			switch (fragment.Conjunction) {
	//			    case FilterConjunction.AndEquals :
	//			    case FilterConjunction.OrEquals :
	//				if (fragment.Value != null) {
	//				    sb.Append("=");
	//				} else {
	//				    sb.Append(" IS NULL");
	//				}
	//				break;
	//			    case FilterConjunction.AndNotEquals :
	//			    case FilterConjunction.OrNotEquals :
	//				if (fragment.Value != null) {
	//				    sb.Append("<>");
	//				} else {
	//				    sb.Append(" IS NOT NULL");
	//				}
	//				break;
	//			    case FilterConjunction.AndBetween :
	//			    case FilterConjunction.OrBetween :
	//				sb.Append(" BETWEEN ");
	//				break;
	//			    case FilterConjunction.AndNotBetween :
	//			    case FilterConjunction.OrNotBetween :
	//				sb.Append(" NOT BETWEEN ");
	//				break;
	//			    case FilterConjunction.AndIn :
	//			    case FilterConjunction.OrIn :
	//				sb.Append(" IN ");
	//				break;
	//			    case FilterConjunction.AndNotIn :
	//			    case FilterConjunction.OrNotIn :
	//				sb.Append(" NOT IN ");
	//				break;
	//			    case FilterConjunction.And :
	//			    case FilterConjunction.Or :
	//				break;
	//			    case FilterConjunction.AndLike :
	//			    case FilterConjunction.OrLike :
	//				sb.Append(" LIKE ");
	//				break;
	//			    case FilterConjunction.AndNotLike :
	//			    case FilterConjunction.OrNotLike :
	//				sb.Append(" NOT LIKE ");
	//				break;
	//			    default :
	//				throw new ArgumentException("Unexpected filter conjunction");
	//			}
	//		    }
	//
	//		    if (IsBetweenConjunction(fragment.Conjunction)) {
	//			sb.Append("@").Append(fragment.Key).Append("1 AND @").Append(fragment.Key).Append("2");		    	
	//		    } else if (IsInConjunction(fragment.Conjunction)) {
	//		    	if (fragment.Values.Length > 0) {
	//		    	    sb.Append("(");
	//		    	    for(Int32 i=0; i < fragment.Values.Length; i++) {
	//		    	    	Object value = fragment.Values[i];
	//		    	    	if (i > 0) {
	//		    	    	    sb.Append(", ");
	//		    	    	}
	//		    	    	sb.Append("@").Append(fragment.Key).Append((i + 1).ToString());
	//		    	    }
	//		    	    sb.Append(")");
	//		    	}
	//		    } else if (fragment.Value is DatabaseFilter) {
	//			sb.Append("(").Append(((DatabaseFilter)fragment.Value).InnerStatement).Append(")");
	//		    } else if (fragment.Key == null) {
	//			sb.Append("(").Append(fragment.Value).Append(")");
	//		    } else {
	//			if (fragment.Value != null) {
	//			    sb.Append("@").Append(fragment.Key);
	//			}
	//		    }
	//		}
	//
	//		return sb.ToString();
	//	    }
	//	}
	//
	//	public override IDataParameterCollection Parameters {
	//	    get {
	//	    	SqlCommand command = new SqlCommand();
	//	    	AddParameters(command.Parameters, Fragments);
	//	    	return command.Parameters;
	//	    }
	//	}
	//    	
	//    	private void AddParameters(SqlParameterCollection parameters, IList fragments) {
	//	    foreach(FilterClause fragment in fragments) {
	//		if (fragment.Value is DatabaseFilter) {
	//		    AddParameters(parameters, ((DatabaseFilter)fragment.Value).Fragments);
	//		} else if (fragment.Key == null || fragment.Key.Length == 0) {
	//		    // must be a constant expression
	//		} else {
	//		    if (IsBetweenConjunction(fragment.Conjunction)) {
	//			parameters.Add(new SqlParameter("@" + fragment.Key + "1", fragment.Values[0]));
	//		    	parameters.Add(new SqlParameter("@" + fragment.Key + "2", fragment.Values[1]));
	//		    } else if (IsInConjunction(fragment.Conjunction)) {
	//			for(Int32 i=0; i < fragment.Values.Length; i++) {
	//			    parameters.Add(new SqlParameter("@" + fragment.Key + (i+1).ToString(), fragment.Values[i]));
	//			}
	//		    } else if (fragment.Value != null) {
	//		    	parameters.Add(new SqlParameter("@" + fragment.Key, fragment.Value));
	//		    }
	//		}
	//	    }
	//	}
	#endregion

	public SqlFilter And(IDatabaseExpression expression) {
	    if (expressions.Count==0) {
	    	expressions.Add(new LogicalExpression(expression));
	    } else {
		expressions.Add(new LogicalExpression(LogicalOperatorEnum.And, expression));
	    }
	    return this;
	}

	public SqlFilter Or(IDatabaseExpression expression) {
	    if (expressions.Count==0) {
		expressions.Add(new LogicalExpression(expression));
	    } else {
		expressions.Add(new LogicalExpression(LogicalOperatorEnum.Or, expression));
	    }
	    return this;
	}

	public SqlFilter Not(IDatabaseExpression expression) {
	    if (expressions.Count==0) {
		expressions.Add(new LogicalExpression(expression));
	    } else {
		expressions.Add(new LogicalExpression(LogicalOperatorEnum.Not, expression));
	    }
	    return this;
	}
	
	public String Expression {
	    get {
	    	if (expressions.Count ==0) {
	    	    return String.Empty;
	    	}
	    	
		StringBuilder sb = new StringBuilder();
		if (expressions.Count > 1) {
		    sb.Append("(");
		}
		foreach(LogicalExpression expression in expressions) {
		    switch (expression.Operator) {
			case LogicalOperatorEnum.And :
			    sb.Append(" AND ");
			    break;
			case LogicalOperatorEnum.Or :
			    sb.Append(" OR ");
			    break;
			case LogicalOperatorEnum.Not :
			    sb.Append(" NOT ");
			    break;
			case LogicalOperatorEnum.None :
			    break;
			default:
			    throw new ArgumentOutOfRangeException();
		    }
		    sb.Append(expression.Expression.Expression);
		}
		if (expressions.Count > 1) {
		    sb.Append(")");
		}

		return sb.ToString();
	    }
	}

	public override IDataParameterCollection Parameters {
	    get {
		SqlParameterList parameters = new SqlParameterList();
		foreach(LogicalExpression expression in expressions) {
		    parameters.AddRange(expression.Expression.Parameters);
		}
	    	return parameters;
	    }
	}

	/// <summary>
	/// Returns true if nothing has been added to the clause; otherwise false.
	/// </summary>
	public override Boolean IsEmpty {
	    get { return expressions.Count==0; }
	}

    }
}
