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
    public class MenuControl : System.Web.UI.WebControls.WebControl, IMenu, INamingContainer {
	
	private IList items = new ArrayList();
	private String text;
	private String cssClass;
	private String selectedItemImageUrl;
	private String completedItemImageUrl;
	private Unit width;
	private Unit indent;
	private Unit space;

	[Bindable(true), Category("Appearance"), DefaultValue("")]
	public String Text {
	    get { return text; }
	    set { text = value; }
	}

	public String Label {
	    get { return text; }
	    set { text = value; }
	}

	public String SelectedItemImageUrl {
	    get { return selectedItemImageUrl; }
	    set { selectedItemImageUrl = value; }
	}

	public String CompletedItemImageUrl {
	    get { return completedItemImageUrl; }
	    set { completedItemImageUrl = value; }
	}

	public IList Items {
	    get { return items; }
	}

	public override String CssClass {
	    get { return cssClass; }
	    set { cssClass = value; }
	}

	public override Unit Width { 
	    get { return width; }
	    set { width = value; }
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
	    TableRow row = new TableRow();
	    TableCell cell = new TableCell();
	    table.Width = this.Width;

	    cell.CssClass = this.cssClass;
	    cell.HorizontalAlign = HorizontalAlign.Center;
	    cell.Text = this.Label;

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

//	/// <summary>
//	/// Render this control to the output parameter specified.
//	/// </summary>
//	/// <param name="output"> The HTML writer to write out to </param>
//	protected override void Render(HtmlTextWriter output) {
//	    output.Write(Text);
//	}
    }
}
