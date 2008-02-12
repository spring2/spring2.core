using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Navigation.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.Navigation.DataObject {

    public class MenuLinkData : Spring2.Core.DataObject.DataObject {

	public static readonly MenuLinkData DEFAULT = new MenuLinkData();

	private StringType name = StringType.DEFAULT;
	private StringType target = StringType.DEFAULT;
	private BooleanType active = BooleanType.DEFAULT;
	private IdType menuLinkGroupId = IdType.DEFAULT;
	private IdType parentMenuLinkId = IdType.DEFAULT;
	private MenuLinkList childMenuLinks = MenuLinkList.DEFAULT;
	private MenuLinkKeyList keys = MenuLinkKeyList.DEFAULT;
	private DateTimeType effectiveDate = DateTimeType.DEFAULT;
	private DateTimeType expirationDate = DateTimeType.DEFAULT;
	private IdType sequence = IdType.DEFAULT;
	private StringType targetWindow = StringType.DEFAULT;

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public StringType Target {
	    get { return this.target; }
	    set { this.target = value; }
	}

	public BooleanType Active {
	    get { return this.active; }
	    set { this.active = value; }
	}

	public IdType MenuLinkGroupId {
	    get { return this.menuLinkGroupId; }
	    set { this.menuLinkGroupId = value; }
	}

	public IdType ParentMenuLinkId {
	    get { return this.parentMenuLinkId; }
	    set { this.parentMenuLinkId = value; }
	}

	public MenuLinkList ChildMenuLinks {
	    get { return this.childMenuLinks; }
	    set { this.childMenuLinks = value; }
	}

	public MenuLinkKeyList Keys {
	    get { return this.keys; }
	    set { this.keys = value; }
	}

	public DateTimeType EffectiveDate {
	    get { return this.effectiveDate; }
	    set { this.effectiveDate = value; }
	}

	public DateTimeType ExpirationDate {
	    get { return this.expirationDate; }
	    set { this.expirationDate = value; }
	}

	public IdType Sequence {
	    get { return this.sequence; }
	    set { this.sequence = value; }
	}

	public StringType TargetWindow {
	    get { return this.targetWindow; }
	    set { this.targetWindow = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
