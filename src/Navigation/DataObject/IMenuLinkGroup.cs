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

    public class MenuLinkGroupFields {
	private MenuLinkGroupFields() {}
	public static readonly String ENTITY_NAME = "MenuLinkGroup";
	
	public static readonly String MENULINKGROUPID = "MenuLinkGroupId";
	public static readonly String NAME = "Name";
	public static readonly String MENULINKS = "MenuLinks";
    }

    public interface IMenuLinkGroup : IBusinessEntity {
	IdType MenuLinkGroupId {
	    get;
	}
	StringType Name {
	    get;
	}
	MenuLinkList MenuLinks {
	    get;
	}

	#region Custom Code
	IMenuLink GetHighestActiveChild();
	#endregion
    }
}
