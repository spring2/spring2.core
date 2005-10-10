using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.Types;


using Spring2.Core.ResourceManager.Types;


namespace Spring2.Core.ResourceManager.DataObject {

    public class ResourceData : Spring2.Core.DataObject.DataObject {

	public static readonly ResourceData DEFAULT = new ResourceData();

	private StringType entityName = StringType.DEFAULT;
	private StringType propertyName = StringType.DEFAULT;
	private IdType identity = IdType.DEFAULT;

	public StringType EntityName {
	    get { return this.entityName; }
	    set { this.entityName = value; }
	}

	public StringType PropertyName {
	    get { return this.propertyName; }
	    set { this.propertyName = value; }
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
