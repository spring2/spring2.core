using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spring2.Core.WebControl {

    /// <summary>
    /// Summary description for MenuControl.
    /// </summary>
    [ParseChildren(true, "Items")]
    [Description("Menu Control")]
    public class Menu : System.Web.UI.WebControls.WebControl, INamingContainer {
	
	private HyperLink link = new HyperLink();

	private Boolean selected;
	private Boolean expanded;
	private Boolean expandSelected = true;
	private String menuItemStyle = String.Empty;
	private String selectedItemStyle = String.Empty;
	private String completedItemStyle = String.Empty;
	private Int32 indent;
	private String navigateUrl = String.Empty;
	private Boolean active;

	private MenuItemCollection items = new MenuItemCollection();

	protected HyperLink Link {
	    get { return link; }
	}

	public String Label {
	    get { return link.Text; }
	    set { link.Text = value; }
	}

	public String NavigateUrl {
	    get { return navigateUrl; }
	    set { navigateUrl = value; }
	}

	public virtual Boolean Active {
	    get { return active; }
	    set { 
		active = value;
		link.NavigateUrl = value ? navigateUrl : String.Empty;
		foreach (MenuItem item in Items) {
		    item.Active = value;
		}
	    }
	}

	public virtual Boolean Selected {
	    get { return selected; }
	    set { selected = value; }
	}

	public Boolean Expanded {
	    get { return expanded; }
	    set { expanded = value; }
	}

	public Boolean ExpandSelected {
	    get { return expandSelected; }
	    set { expandSelected = value; }
	}

	public virtual String MenuItemStyle {
	    get { return menuItemStyle; }
	    set { menuItemStyle = value; }
	}

	public virtual String SelectedItemStyle {
	    get { return selectedItemStyle; }
	    set { selectedItemStyle = value; }
	}

	public virtual String CompletedItemStyle {
	    get { return completedItemStyle; }
	    set { completedItemStyle = value; }
	}

	public virtual Int32 Indent {
	    get { return indent; }
	    set { indent = value; }
	}

	public virtual MenuItem SelectedItem {
	    get { 
		foreach (MenuItem item in items) {
		    MenuItem selectedItem = item.SelectedItem;
		    if (!MenuItem.EMPTY.Equals(selectedItem)) {
			return selectedItem;
		    }
		}
		return MenuItem.EMPTY;
	    }
	    set {
		foreach (MenuItem item in items) {
		    item.SelectedItem = value;
		}
	    }
	}

	public virtual MenuItem SetSelectedItemByUrl(String url) {
	    if (url != null) {
		foreach (MenuItem item in items) {
		    MenuItem selectedItem = item.SetSelectedItemByUrl(url);
		    if (!MenuItem.EMPTY.Equals(selectedItem)) {	
			this.SelectedItem = selectedItem;
			return selectedItem;
		    }
		}
	    }
	    return MenuItem.EMPTY;
	}

	public MenuItem NextItem {
	    get {
		Int32 selectedItemIndex = items.IndexOf(SelectedItem);
		return ++selectedItemIndex >= items.Count ? MenuItem.EMPTY : items[selectedItemIndex];
	    }
	}

	public MenuItem PreviousItem {
	    get { 
		Int32 selectedItemIndex = items.IndexOf(SelectedItem);
		return --selectedItemIndex < 0 ? MenuItem.EMPTY : items[selectedItemIndex];
	    }
	}

	public MenuItem FirstItem {
	    get { return items.Count > 0 ? items[0] : MenuItem.EMPTY; }
	}

	public MenuItem LastItem {
	    get { return items.Count > 0 ? items[items.Count - 1] : MenuItem.EMPTY; }
	}

	public MenuItemCollection Items {
	    get { return items; }
	}

	protected override void OnLoad(EventArgs e) 
	{
	    base.OnLoad(e);
	    foreach (MenuItem item in Items) 
	    {
		item.ParentMenu = this;
	    }

	    Active = Active;
	}

	protected override void Render(HtmlTextWriter writer) {

	    if (Visible) {
		
		link.CssClass = this.CssClass;
		link.RenderControl(writer);

		foreach (MenuItem item in Items) {
		    item.Render(writer, 0);
		}
	    }
	}
    }
}
