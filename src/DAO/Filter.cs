using System;
using System.Collections;
using System.Data;
using Spring2.Core.Types;

namespace Spring2.Core.DAO {

    /// <summary>
    /// Summary description for Filter.
    /// </summary>
    public abstract class Filter {

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
	    OrNotIn
	}

	/// <summary>
	/// Structure to hold filter fragments
	/// </summary>
	internal class FilterClause {
	    private FilterConjunction conjunction;
	    private String key;
	    private Object value;

	    public FilterClause(FilterConjunction conjunction, String key, Object value) {
		this.conjunction = conjunction;
		this.key = key;
		this.value = value;
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
	}

	private ArrayList fragments = new ArrayList();

	public Filter() {
	}

	public Filter(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value));
	}

	public Filter(String key, DataType value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value.DBValue));
	}

	public void AndEquals(String key, Object value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value));
	}

	public void AndEquals(String key, DataType value) {
	    fragments.Add(new FilterClause(FilterConjunction.AndEquals, key, value.DBValue));
	}

	public void And(Filter filter) {
	    fragments.Add(new FilterClause(FilterConjunction.And, null, filter));
	}

	public void And(String value) {
	    fragments.Add(new FilterClause(FilterConjunction.And, null, value));
	}

	public abstract String InnerStatement {
	    get;
	}

	public abstract String Statement {
	    get;
	}

	public abstract IDataParameterCollection Parameters {
	    get;
	}

	protected ArrayList Fragments {
	    get { return fragments; }	
	}

    }
}
