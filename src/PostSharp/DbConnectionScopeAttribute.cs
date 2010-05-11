using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;
using Spring2.Core.DAO;

namespace Spring2.Core.PostSharp {

    /// <summary>
    /// Custom attribute that will create a DbConnectionScope for the attributed method.  The default option, if not specified, is Required.
    /// </summary>
    [Serializable]
    public sealed class DbConnectionScopeAttribute : OnMethodBoundaryAspect {

    	private DbConnectionScopeOption option;

	public DbConnectionScopeAttribute () {
	    this.option = DbConnectionScopeOption.Required;
	}

	public DbConnectionScopeAttribute (DbConnectionScopeOption option) {
	    this.option = option;
	}

    	public DbConnectionScopeOption DbConnectionScopeOption {
	    get { return this.option;  }
	    set { this.option = value; }
    	}

	public override void OnEntry(MethodExecutionArgs eventArgs) {
	    eventArgs.MethodExecutionTag = new DbConnectionScope(option);
	}

	public override void OnExit(MethodExecutionArgs eventArgs) {
	    DbConnectionScope scope = eventArgs.MethodExecutionTag as DbConnectionScope;
	    if (scope != null) {
		scope.Dispose();
	    }
	}
    }
}
