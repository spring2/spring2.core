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
    public class MenuItem : Menu {

	public static readonly MenuItem EMPTY = new MenuItem();

	private Menu parentMenu;
	
	public Menu ParentMenu {
	    get { return parentMenu; }
	    set { parentMenu = value; }
	}

//	public MenuItem SelectedItem {
//	    get { 
//		foreach (MenuItem item in items) {
//		    if (item.Selected) {
//			return item;
//		    }
//		}
//		return EMPTY;
//	    }
//	    set {
//		foreach (MenuItem item in items) {
//		    item.Selected = item.Equals(value);
//		}
//	    }
//	}
//
//	public MenuItem NextItem {
//	    get {
//		Int32 selectedItemIndex = items.IndexOf(SelectedItem);
//		return ++selectedItemIndex >= items.Count ? EMPTY : items[selectedItemIndex];
//	    }
//	}
//
//	public MenuItem PreviousItem {
//	    get { 
//		Int32 selectedItemIndex = items.IndexOf(SelectedItem);
//		return --selectedItemIndex < 0 ? EMPTY : items[selectedItemIndex];
//	    }
//	}
//
//	public MenuItem FirstItem {
//	    get { return items.Count > 0 ? items[0] : EMPTY; }
//	}
//
//	public MenuItem LastItem {
//	    get { return items.Count > 0 ? items[items.Count - 1] : EMPTY; }
//	}
//
	public new String SelectedItemImageUrl {
	    get { return ParentMenu.SelectedItemImageUrl; }
	}
//
//	public MenuItem SetSelectedItemByUrl(String url) {
//	    foreach (MenuItem item in items) {
//	        if (item.NavigateUrl.Equals(url)) {
//		    SelectedItem = item;
//		    return item;
//		}
//	    }
//	    return EMPTY;
//	}
//
//	public Unit Indent {
//	    get { return ParentMenu.Indent; }
//	}
//
//	public Unit Space {
//	    get { return ParentMenu.Space; }
//	}
//
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

	//	/// <summary> 
	//	/// Render this control to the output parameter specified.
	//	/// </summary>
	//	/// <param name="output"> The HTML writer to write out to </param>
	//	protected override void Render(HtmlTextWriter output) {
	//	    output.Write(Text);
	//	}
    }
}
