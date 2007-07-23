using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Navigation.DataObject;
using Spring2.Core.Types;



using Spring2.Core.BusinessEntity;

namespace Spring2.Core.Navigation.DataObject {

    public class MenuLinkKeyFields {
	private MenuLinkKeyFields() {}
	public static readonly String ENTITY_NAME = "MenuLinkKey";
	
	public static readonly String MENULINKID = "MenuLinkId";
	public static readonly String KEY = "Key";
    }

    public interface IMenuLinkKey : IBusinessEntity {
	IdType MenuLinkId {
	    get;
	}
	StringType Key {
	    get;
	}
    }
}
