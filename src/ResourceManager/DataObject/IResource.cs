using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.Types;


using Spring2.Core.ResourceManager.Types;

using Spring2.Core.BusinessEntity;

namespace Spring2.Core.ResourceManager.DataObject {

    public class ResourceFields {
	private ResourceFields() {}
	public static readonly String RESOURCEID = "ResourceId";
	public static readonly String ENTITYNAME = "EntityName";
	public static readonly String PROPERTYNAME = "PropertyName";
	public static readonly String IDENTITY = "Identity";
    }

    public interface IResource : IBusinessEntity {
	IdType ResourceId {
	    get;
	}
	StringType EntityName {
	    get;
	}
	StringType PropertyName {
	    get;
	}
	IdType Identity {
	    get;
	}
    }
}
