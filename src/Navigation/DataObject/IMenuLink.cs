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

    public class MenuLinkFields {
	private MenuLinkFields() {}
	public static readonly String ENTITY_NAME = "MenuLink";
	
	public static readonly String MENULINKID = "MenuLinkId";
	public static readonly String NAME = "Name";
	public static readonly String TARGET = "Target";
	public static readonly String ACTIVE = "Active";
	public static readonly String MENULINKGROUPID = "MenuLinkGroupId";
	public static readonly String PARENTMENULINKID = "ParentMenuLinkId";
	public static readonly String CHILDMENULINKS = "ChildMenuLinks";
	public static readonly String KEYS = "Keys";
	public static readonly String EFFECTIVEDATE = "EffectiveDate";
	public static readonly String EXPIRATIONDATE = "ExpirationDate";
	public static readonly String SEQUENCE = "Sequence";
	public static readonly String TARGETWINDOW = "TargetWindow";
    }

    public interface IMenuLink : IBusinessEntity {
	IdType MenuLinkId {
	    get;
	}
	StringType Name {
	    get;
	}
	StringType Target {
	    get;
	}
	BooleanType Active {
	    get;
	}
	IdType MenuLinkGroupId {
	    get;
	}
	IdType ParentMenuLinkId {
	    get;
	}
	MenuLinkList ChildMenuLinks {
	    get;
	}
	MenuLinkKeyList Keys {
	    get;
	}
	DateTimeType EffectiveDate {
	    get;
	}
	DateTimeType ExpirationDate {
	    get;
	}
	IdType Sequence {
	    get;
	}
	StringType TargetWindow {
	    get;
	}

	#region Custome Code
	BooleanType IsSelected(StringType key);
	BooleanType CanMoveUp {get;}
	BooleanType CanMoveDown {get;}

	void MoveUp();
	void MoveDown();
	IMenuLink GetHighestActiveChild();
	#endregion
    }
}
