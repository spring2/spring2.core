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
	
	private String label;
	private String navigateUrl;
	private Boolean selected;
	private Boolean expanded;
	private MenuItemCollection items = new MenuItemCollection();

	private String selectedItemImageUrl;
	private String completedItemImageUrl;
	private Unit indent;
	private Unit space;

	public String Label {
	    get { return label; }
	    set { label = value; }
	}

	public String NavigateUrl {
	    get { return navigateUrl; }
	    set { navigateUrl = value; }
	}

	public Boolean Selected {
	    get { return selected; }
	    set { selected = value; }
	}

	public Boolean Expanded {
	    get { return expanded; }
	    set { expanded = value; }
	}

	public MenuItem SelectedItem {
	    get { 
		foreach (MenuItem item in items) {
		    if (item.Selected) {
			return item;
		    }
		}
		return MenuItem.EMPTY;
	    }
	    set {
		foreach (MenuItem item in items) {
		    item.Selected = item.Equals(value);
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

	public MenuItem SetSelectedItemByUrl(String url) {
	    if (url != null) {
		foreach (MenuItem item in items) {
		    if (url.Equals(item.NavigateUrl)) {
			SelectedItem = item;
			return item;
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

	public Unit Indent {
	    get { return indent; }
	    set { indent = value; }
	}

	public Unit Space {
	    get { return space; }
	    set { space = value; }
	}

	public Int32 MaxDepth {
	    get {
		Int32 depth, maxDepth = 0;
		foreach (MenuItem item in Items) {
		    item.ParentMenu = this;
		    depth = item.Depth;
		    if (depth > maxDepth) {
			maxDepth = depth;
		    }
		}
		return maxDepth;
	    }
	}

	protected override void CreateChildControls() {

	    Table table = new Table();
	    table.CellPadding = 0;
	    table.CellSpacing = 0;
	    table.Width = this.Width;

	    TableRow row = new TableRow();
	    TableHeaderCell cell = new TableHeaderCell();

	    cell.CssClass = CssClass;
	    cell.BackColor = this.BackColor;
	    cell.HorizontalAlign = HorizontalAlign.Center;
	    cell.Text = this.Label;

	    this.CssClass = String.Empty;
	    this.BackColor = System.Drawing.Color.Transparent;

	    row.Cells.Add(cell);
	    table.Rows.Add(row);

	    Int32 maxDepth = MaxDepth;
	    foreach (MenuItem item in Items) {
		if (item.Visible) {
		    item.CreateItemControls(table, Indent, Space, 1, maxDepth);
		}
	    }

	    cell.ColumnSpan = maxDepth + 1;

	    Controls.Add(table);
	}
    }
}
