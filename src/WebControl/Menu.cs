using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spring2.Core.WebControl {

    /// <summary>
    /// Summary description for MenuControl.
    /// </summary>
//    [DefaultProperty("Text"),
//    ToolboxData("<{0}:MenuControl runat=server></{0}:MenuControl>")]
    [ParseChildren(true, "Items")]
    [Description("Menu Control")]
    public class Menu : System.Web.UI.WebControls.WebControl, INamingContainer {
	
	private HyperLink link = new HyperLink();

	private Boolean selected;
	private Boolean expanded;
	private Boolean expandSelected = true;
	private Boolean showSelectedImage;
	private HorizontalAlign horizontalAlign = HorizontalAlign.Center;
	private MenuItemCollection items = new MenuItemCollection();

	private String selectedItemImageUrl;
	private String completedItemImageUrl;
	private Int32 indent;
	private Unit space;

	protected HyperLink Link {
	    get { return link; }
	}

	public String Label {
	    get { return link.Text; }
	    set { link.Text = value; }
	}

	public String NavigateUrl {
	    get { return link.NavigateUrl; }
	    set { link.NavigateUrl = value; }
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

	public Boolean ShowSelectedImage {
	    get { return showSelectedImage; }
	    set { showSelectedImage = value; }
	}

	public HorizontalAlign HorizontalAlign {
	    get { return horizontalAlign; }
	    set { horizontalAlign = value; }
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

	public String SelectedItemImageUrl {
	    get { return selectedItemImageUrl; }
	    set { selectedItemImageUrl = value; }
	}

	public String CompletedItemImageUrl {
	    get { return completedItemImageUrl; }
	    set { completedItemImageUrl = value; }
	}

	public MenuItemCollection Items {
	    get { return items; }
	}

	public virtual Int32 Indent {
	    get { return indent; }
	    set { indent = value; }
	}

	public Unit Space {
	    get { return space; }
	    set { space = value; }
	}

	protected override void OnLoad(EventArgs e) 
	{
	    base.OnLoad(e);
	    foreach (MenuItem item in Items) 
	    {
		item.ParentMenu = this;
	    }
	}

	protected override void Render(HtmlTextWriter writer) {

	    if (Visible) {
		Table table = new Table();
		table.CellPadding = 0;
		table.CellSpacing = 0;
		table.Width = this.Width;

		table.RenderBeginTag(writer);

		TableRow row = new TableRow();
		row.RenderBeginTag(writer);

		TableCell cell = new TableCell();
		cell.BackColor = this.BackColor;
		cell.HorizontalAlign = this.HorizontalAlign;
		cell.RenderBeginTag(writer);

		if (Selected) {
		    link.CssClass = "selectedmenu";
		} else {
		    link.CssClass = "menu";
		}
		link.RenderControl(writer);

		cell.RenderEndTag(writer);
		row.RenderEndTag(writer);

		foreach (MenuItem item in Items) {
		    item.Render(writer, 0);
		}

		table.RenderEndTag(writer);
	    }
	}
    }
}
