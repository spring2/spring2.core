using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spring2.Core.WebControl {
    /// <summary>
    /// Summary description for MenuItem.
    /// </summary>
//    [DefaultProperty("Text"), ToolboxData("<{0}:MenuItem runat=server></{0}:MenuItem>")]
    [ParseChildren(true, "Items")]
    public class MenuItem : System.Web.UI.WebControls.WebControl, IMenu {

	private String label;
	private String navigateUrl;
	private Boolean selected;
	private Boolean expanded;
	private IList items = new ArrayList();
	private MenuItem selectedItem;
	private IMenu parentMenu;
	
	//	[Bindable(true), Category("Appearance"), DefaultValue("")] 
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

	public IList Items {
	    get { return items; }
	}

	public IMenu ParentMenu {
	    get { return parentMenu; }
	    set { parentMenu = value; }
	}

	public MenuItem SelectedItem {
	    get { return selectedItem; }
	    set { selectedItem = value; }
	}

	public MenuItem NextItem {
	    get { return selectedItem; }
	}

	public MenuItem PreviousItem {
	    get { return selectedItem; }
	}

	public String SelectedItemImageUrl {
	    get { return ParentMenu.SelectedItemImageUrl; }
	}

	public Unit Indent {
	    get { return ParentMenu.Indent; }
	}

	public Unit Space {
	    get { return ParentMenu.Space; }
	}

	public Int32 Depth {
	    get {
		Int32 maxDepth = 0, depth = 0;
		foreach(MenuItem item in items) {
		    item.ParentMenu = this;
		    depth = item.Depth;
		    if (depth > maxDepth) {
			maxDepth = depth;
		    }
		}

		return maxDepth + 1;
	    }
	}

	public void AddItem(MenuItem item) {
	    this.Items.Add(item);
	}

	public void CreateItemControls(Table table, Unit indent, Unit space, Int32 level, Int32 depth) {

	    TableRow row = new TableRow();

	    for (Int32 i = 1; i < level; i++) {
		TableCell indentCell = new TableCell();
		indentCell.Width = indent;
		row.Cells.Add(indentCell);
	    }

	    TableCell cell = new TableCell();
	    if (Selected && !String.Empty.Equals(SelectedItemImageUrl)) {
		Image img = new Image();
		img.ImageUrl = SelectedItemImageUrl;
		cell.Width = 0;
		cell.Controls.Add(img);
	    } else {
		cell.Width = indent;
	    }

	    row.Cells.Add(cell);

	    cell = new TableCell();
	    cell.Text = this.Label;
	    cell.CssClass = this.CssClass;
	    cell.ColumnSpan = depth - level + 1;
	    cell.Width = Unit.Percentage(100);
	    row.Cells.Add(cell);
	    row.Height = space;

	    table.Rows.Add(row);

	    if (expanded) {
		foreach (MenuItem item in Items) {
		    if (item.Visible) {
			item.CreateItemControls(table, indent, space, level + 1, depth);
		    }
		}
	    }
	}

//	/// <summary> 
//	/// Render this control to the output parameter specified.
//	/// </summary>
//	/// <param name="output"> The HTML writer to write out to </param>
//	protected override void Render(HtmlTextWriter output) {
//	    output.Write(Text);
//	}
    }
}
