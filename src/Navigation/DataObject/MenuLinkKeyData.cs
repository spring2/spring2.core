using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Navigation.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.Navigation.DataObject {

    public class MenuLinkKeyData : Spring2.Core.DataObject.DataObject {

	public static readonly MenuLinkKeyData DEFAULT = new MenuLinkKeyData();

	private IdType menuLinkId = IdType.DEFAULT;
	private StringType key = StringType.DEFAULT;

	public IdType MenuLinkId {
	    get { return this.menuLinkId; }
	    set { this.menuLinkId = value; }
	}

	public StringType Key {
	    get { return this.key; }
	    set { this.key = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
