using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;
using Spring2.Core.Types;



namespace Spring2.Core.ResourceManager.DataObject {

    public class LocalizedResourceData : Spring2.Core.DataObject.DataObject {

	public static readonly LocalizedResourceData DEFAULT = new LocalizedResourceData();

	private StringType content = StringType.DEFAULT;

	public StringType Content {
	    get { return this.content; }
	    set { this.content = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
