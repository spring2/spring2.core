using System;
using System.Collections;
using System.Data;

namespace Spring2.Core.DAO {

    /// <summary>
    /// Summary description for Filter.
    /// </summary>
    public abstract class DatabaseFilter {

	/// <summary>
	/// This is how individual fragments are joined together
	/// </summary>
	/// <remarks>And and Or are added to support nested Filters</remarks>
	internal enum FilterConjunction {
	    And,
	    Or,
	    AndEquals,
	    AndNotEquals,
	    OrEquals,
	    OrNotEquals,
	    AndBetween,
	    AndNotBetween,
	    OrBetween,
	    OrNotBetween,
	    AndIn,
	    AndNotIn,
	    OrIn,
	    OrNotIn,
	    AndLike,
	    AndNotLike,
            OrLike,
	    OrNotLike
	}

	/// <summary>
	/// Structure to hold filter fragments
	/// </summary>
	internal class FilterClause {
	    private FilterConjunction conjunction;
	    private String key;
	    private Object value;
	    private Object[] values = new Object[0];

	    public FilterClause(FilterConjunction conjunction, String key, Object value) {
		this.conjunction = conjunction;
		this.key = key;
		if (value is DBNull) {
	    	    this.value = null;
		} else {
		    this.value = value;
		}
	    }

	    public FilterClause(FilterConjunction conjunction, String key, Object value1, Object value2) {
		this.conjunction = conjunction;
		this.key = key;
	    	this.values = new Object[2] {value1, value2};
	    }

	    public FilterClause(FilterConjunction conjunction, String key, Object[] values) {
		this.conjunction = conjunction;
		this.key = key;
		this.values = values;
	    }

	    public FilterConjunction Conjunction {
	    	get { return conjunction; }
	    }

	    public String Key {
		get { return key; }
	    }
	    
	    public Object Value {
		get { return value; }
	    }

	    public Object[] Values {
		get { return values; }
	    }
	}

	private ArrayList fragments = new ArrayList();
    	
	#region Constructors

	public DatabaseFilter() {
	}

	public DatabaseFilter(String expression) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, null, expression));
	}
    	
    	public DatabaseFilter(DatabaseFilter filter) {
    	    fragments.AddRange(filter.Fragments);
    	}

    	public DatabaseFilter(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value));
	}

//	public Filter(String key, DataType value) {
//	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value.DBValue));
//	}
	#endregion
    	
	#region And
	public void AndEquals(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value));
	}

	public void AndNotEquals(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndNotEquals, key, value));
	}
	
//	public void AndEquals(String key, DataType value) {
//	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value.DBValue));
//	}

    	public void And(DatabaseFilter filter) {
	    fragments.Add(new FilterClause(FilterConjunction.And, null, filter));
	}

	public void And(String value) {
	    fragments.Add(new FilterClause(FilterConjunction.And, null, value));
	}
	#endregion

	#region Or
	public void OrEquals(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.OrEquals, key, value));
	}
	#endregion
    	
	#region Like
	public void AndLike(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndLike, key, value));
	}
	public void OrLike(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.OrLike, key, value));
	}

	public void AndNotLike(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndNotLike, key, value));
	}
	public void OrNotLike(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.OrNotLike, key, value));
	}
	#endregion
    	
	#region Between
	public void AndBetween(String key, Object value1, Object value2) {
	    fragments.Add(new FilterClause(FilterConjunction.AndBetween, key, value1, value2));
	}
	public void OrBetween(String key, Object value1, Object value2) {
	    fragments.Add(new FilterClause(FilterConjunction.OrBetween, key, value1, value2));
	}

	public void AndNotBetween(String key, Object value1, Object value2) {
	    fragments.Add(new FilterClause(FilterConjunction.AndNotBetween, key, value1, value2));
	}
	public void OrNotBetween(String key, Object value1, Object value2) {
	    fragments.Add(new FilterClause(FilterConjunction.OrNotBetween, key, value1, value2));
	}
	#endregion
    	
	#region In
	public void AndIn(String key, Object[] values) {
	    if (values.Length==0) {
		throw new ArgumentException("Values must contain at least 1 value", "values");
	    }
	    fragments.Add(new FilterClause(FilterConjunction.AndIn, key, values));
	}
	public void OrIn(String key, Object[] values) {
	    if (values.Length==0) {
		throw new ArgumentException("Values must contain at least 1 value", "values");
	    }
	    fragments.Add(new FilterClause(FilterConjunction.OrIn, key, values));
	}

	public void AndNotIn(String key, Object[] values) {
	    if (values.Length==0) {
		throw new ArgumentException("Values must contain at least 1 value", "values");
	    }
	    fragments.Add(new FilterClause(FilterConjunction.AndNotIn, key, values));
	}
	public void OrNotIn(String key, Object[] values) {
	    if (values.Length==0) {
		throw new ArgumentException("Values must contain at least 1 value", "values");
	    }
	    fragments.Add(new FilterClause(FilterConjunction.OrNotIn, key, values));
	}
	#endregion
    	
	#region Abstract
	public abstract String InnerStatement {
	    get;
	}

	public abstract String Statement {
	    get;
	}

	public abstract IDataParameterCollection Parameters {
	    get;
	}
	#endregion
    	
	internal ArrayList Fragments {
	    get { return fragments; }	
	}
    	
	/// <summary>
	/// Returns true if nothing has been added to the clause; otherwise false.
	/// </summary>
	public virtual Boolean IsEmpty {
	    get { return fragments.Count==0; }
	}

    	internal Boolean IsBetweenConjunction(FilterConjunction conjunction) {
	    switch (conjunction) {
		case FilterConjunction.AndBetween :
		case FilterConjunction.OrBetween :
		case FilterConjunction.AndNotBetween :
		case FilterConjunction.OrNotBetween :
		    return true;
		default :
		    return false;
	    }
    	}

	internal Boolean IsInConjunction(FilterConjunction conjunction) {
	    switch (conjunction) {
		case FilterConjunction.AndIn :
		case FilterConjunction.OrIn :
		case FilterConjunction.AndNotIn :
		case FilterConjunction.OrNotIn :
		    return true;
		default :
		    return false;
	    }
	}
    }
}
