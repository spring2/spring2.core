using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;
using Spring2.Core.ResourceManager.DataObject;


using Spring2.Core.BusinessEntity;

namespace Spring2.Core.ResourceManager.DataObject {

    public class ResourceFields {
	private ResourceFields() {}
	public static readonly String RESOURCE = "Resource"; //Entity Name
	
	public static readonly String RESOURCEID = "ResourceId";
	public static readonly String CONTEXT = "Context";
	public static readonly String FIELD = "Field";
	public static readonly String IDENTITY = "Identity";
    }

    public interface IResource : IBusinessEntity {
	IdType ResourceId {
	    get;
	}
	StringType Context {
	    get;
	}
	StringType Field {
	    get;
	}
	IdType Identity {
	    get;
	}
    }
}
