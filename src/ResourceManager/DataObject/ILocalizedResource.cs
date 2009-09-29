using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.Types;


using Spring2.Core.BusinessEntity;

namespace Spring2.Core.ResourceManager.DataObject {

    public class LocalizedResourceFields {
	private LocalizedResourceFields() {}
	public static readonly String LOCALIZEDRESOURCE = "LocalizedResource"; //Entity Name
	
	public static readonly String LOCALIZEDRESOURCEID = "LocalizedResourceId";
	public static readonly String RESOURCEID = "ResourceId";
	public static readonly String LOCALE = "Locale";
	public static readonly String LANGUAGE = "Language";
	public static readonly String CONTENT = "Content";
	public static readonly String RESOURCE = "Resource";
    }

    public interface ILocalizedResource : IBusinessEntity {
	IdType LocalizedResourceId {
	    get;
	}
	IdType ResourceId {
	    get;
	}
	ILocale Locale {
	    get;
	}
	ILanguage Language {
	    get;
	}
	StringType Content {
	    get;
	}
	IResource Resource {
	    get;
	}
    }
}
