using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Navigation.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.Navigation.DataObject {

    public class MenuLinkGroupData : Spring2.Core.DataObject.DataObject {

	public static readonly MenuLinkGroupData DEFAULT = new MenuLinkGroupData();

	private StringType name = StringType.DEFAULT;

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
