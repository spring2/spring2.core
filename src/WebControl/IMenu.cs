using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spring2.Core.WebControl {
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public interface IMenu {
	String SelectedItemImageUrl {
	    get;
	}

	Unit Indent {
	    get;
	}

	Unit Space {
	    get;
	}

	MenuItem SelectedItem {
	    get; set;
	}

	MenuItem NextItem {
	    get;
	}

	MenuItem PreviousItem {
	    get;
	}

	MenuItem FirstItem {
	    get;
	}

	MenuItem LastItem {
	    get;
	}

	MenuItem SetSelectedItemByUrl(String url);
    }
}
