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
	    set { parentMenu = value; }
	}

	public new String SelectedItemImageUrl {
	    get { return ParentMenu.SelectedItemImageUrl; }
	}

	public Int32 Depth {
	    get {
		Int32 maxDepth = 0, depth = 0;
		foreach(MenuItem item in Items) {
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
	    Items.Add(item);
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
		cell.Controls.Add(img);
	    } else {
		cell.Width = indent;
		cell.Text = "&nbsp;";
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

	    if (Expanded) {
		foreach (MenuItem item in Items) {
		    if (item.Visible) {
			item.CreateItemControls(table, indent, space, level + 1, depth);
		    }
		}
	    }
	}
    }
}
