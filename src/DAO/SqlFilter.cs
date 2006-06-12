using System;
using System.Data;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.DAO {
    /// <summary>
    /// Summary description for SqlFilter.
    /// </summary>
    public class SqlFilter : Filter {

	public SqlFilter() : base() {
	}

	public SqlFilter(String key, Object value) : base(key, value) {
	}

	public SqlFilter(String key, DataType value) : base(key, value) {
	}

	public override String Statement {
	    get {
		if (Fragments.Count == 0) {
		    return String.Empty;
		}

		return " WHERE " + InnerStatement;
	    }
	}
	

	public override String InnerStatement {
	    get {
		if (Fragments.Count == 0) {
			return String.Empty;
		}

		StringBuilder sb = new StringBuilder();
		foreach (FilterClause fragment in Fragments) {
		    if (sb.Length > 0) {
			switch (fragment.Conjunction) {
			    case FilterConjunction.And :
			    case FilterConjunction.AndEquals :
			    case FilterConjunction.AndNotEquals :
			    case FilterConjunction.AndBetween :
			    case FilterConjunction.AndNotBetween :
			    case FilterConjunction.AndIn :
			    case FilterConjunction.AndNotIn :
				sb.Append(" AND ");
				break;
			    case FilterConjunction.Or :
			    case FilterConjunction.OrEquals :
			    case FilterConjunction.OrNotEquals :
			    case FilterConjunction.OrBetween :
			    case FilterConjunction.OrNotBetween :
			    case FilterConjunction.OrIn :
			    case FilterConjunction.OrNotIn :
				sb.Append(" OR ");
				break;
			    default :
				throw new ArgumentException("Unexpected filter conjunction");
			}
		    }

		    if (!(fragment.Value is Filter || fragment.Key == null)) {
			sb.Append(fragment.Key);
		    }
		    
		    switch (fragment.Conjunction) {
			case FilterConjunction.AndEquals :
			case FilterConjunction.OrEquals :
			    sb.Append("=");
			    break;
			case FilterConjunction.AndNotEquals :
			case FilterConjunction.OrNotEquals :
			    sb.Append("<>");
			    break;
			case FilterConjunction.AndBetween :
			case FilterConjunction.OrBetween :
			    sb.Append(" BETWEEN ");
			    break;
			case FilterConjunction.AndNotBetween :
			case FilterConjunction.OrNotBetween :
			    sb.Append(" NOT BETWEEN ");
			    break;
			case FilterConjunction.AndIn :
			case FilterConjunction.OrIn :
			    sb.Append(" IN ");
			    break;
			case FilterConjunction.AndNotIn :
			case FilterConjunction.OrNotIn :
			    sb.Append(" NOT IN ");
			    break;
			case FilterConjunction.And :
			case FilterConjunction.Or :
			    break;
			default :
			    throw new ArgumentException("Unexpected filter conjunction");
		    }

		    if (fragment.Value is Filter) {
			sb.Append("(").Append(((Filter)fragment.Value).InnerStatement).Append(")");
		    } else if (fragment.Key == null) {
			sb.Append("(").Append(fragment.Value).Append(")");
		    } else {
			sb.Append("@").Append(fragment.Key);
		    }
		}

		return sb.ToString();
	    }
	}

	public override IDataParameterCollection Parameters {
	    get { throw new NotImplementedException(); }
	}

    }
}
