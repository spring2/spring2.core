using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.ResourceManager.DataObject {

    public class ResourceData : Spring2.Core.DataObject.DataObject {

	public static readonly ResourceData DEFAULT = new ResourceData();

	private StringType context = StringType.DEFAULT;
	private StringType field = StringType.DEFAULT;
	private IdType identity = IdType.DEFAULT;

	public StringType Context {
	    get { return this.context; }
	    set { this.context = value; }
	}

	public StringType Field {
	    get { return this.field; }
	    set { this.field = value; }
	}

	public IdType Identity {
	    get { return this.identity; }
	    set { this.identity = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
