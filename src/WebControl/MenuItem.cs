using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spring2.Core.WebControl {

    /// <summary>
    /// Summary description for MenuItem.
    /// </summary>
    [ParseChildren(true, "Items")]
    public class MenuItem : Menu {

	public static readonly MenuItem EMPTY = new MenuItem();

	private Menu parentMenu;
	
	public Menu ParentMenu {
	    get { return parentMenu; }
	    set {
		parentMenu = value; 
		foreach (MenuItem item in Items) {
		    item.ParentMenu = this;
		}
	    }
	}

	public override Int32 Indent {
	    get { return ParentMenu.Indent; }
	}

	public override Boolean Selected {
	    get { return base.Selected; }
	    set { 
		base.Selected = value;
		ParentMenu.Expanded |= value;
	    }
	}

	public override MenuItem SelectedItem {
	    get {
		if (this.Selected) {
		    return this;
		}
		foreach (MenuItem item in Items) {
		    MenuItem selectedItem = item.SelectedItem;
		    if (!MenuItem.EMPTY.Equals(selectedItem)) {
			return selectedItem;
		    }
		}
		return MenuItem.EMPTY;
	    }
	    set {
		this.Selected = this.Equals(value);
		this.Expanded = this.ExpandSelected && this.Selected;
		foreach (MenuItem item in Items) {
		    item.SelectedItem = value;
		}
	    }
	}

	public override MenuItem SetSelectedItemByUrl(String url) {
	    if (url != null) {
		if (this.NavigateUrl.Equals(url)) {
		    return this;
		}

		foreach (MenuItem item in Items) {
		    MenuItem selectedItem = item.SetSelectedItemByUrl(url);
		    if (!MenuItem.EMPTY.Equals(selectedItem)) {		
			return selectedItem;
		    }
		}
	    }
	    return MenuItem.EMPTY;
	}

	public override String MenuItemStyle {
	    get { return ParentMenu.MenuItemStyle; }
	}

	public override String SelectedItemStyle {
	    get { return ParentMenu.SelectedItemStyle; }
	}

	public void Render(HtmlTextWriter writer, Int32 indentLevel) {

	    if (Visible) {

		// Insert a break between menu item.
		Literal lineBreak = new Literal();
		lineBreak.Text = "<br>";
		lineBreak.RenderControl(writer);

		// Indent the left margin based on the indentation size and level.  Set the 
		// style to be applied based on the selected status.  Then, render the 
		// link.
		Link.Style["margin-left"] = new Unit(Indent * indentLevel, UnitType.Pixel).ToString();
		if (this.Selected) {
		    Link.CssClass = SelectedItemStyle;
		} else {
		    Link.CssClass = MenuItemStyle;
		}
		Link.RenderControl(writer);

		// If this menu item is expanded, render its child menu items.
		if (Expanded) {
		    foreach (MenuItem item in Items) {
			item.Render(writer, indentLevel + 1);
		    }
		}
	    }
	}
    }
}
