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
    public class MenuItem : Menu 
    {

	public static readonly MenuItem EMPTY = new MenuItem();

	private Menu parentMenu;
	
	public Menu ParentMenu 
	{
	    get { return parentMenu; }
	    set 
	    {
		parentMenu = value; 
		foreach (MenuItem item in Items) 
		{
		    item.ParentMenu = this;
		}
	    }
	}

	public new String SelectedItemImageUrl 
	{
	    get { return ParentMenu.SelectedItemImageUrl; }
	}

	public new Boolean ShowSelectedImage 
	{
	    get { return ParentMenu.ShowSelectedImage; }
	}

	public override Int32 Indent 
	{
	    get { return ParentMenu.Indent; }
	}

	public override Boolean Selected 
	{
	    get { return base.Selected; }
	    set 
	    { 
		base.Selected = value;
		if (value) 
		{
		    ParentMenu.Expanded = true;
		}
	    }
	}

	public override MenuItem SelectedItem {
	    get 
	    {
		if (this.Selected) 
		{
		    return this;
		}
		foreach (MenuItem item in Items) 
		{
		    MenuItem selectedItem = item.SelectedItem;
		    if (!MenuItem.EMPTY.Equals(selectedItem)) 
		    {
			return selectedItem;
		    }
		}
		return MenuItem.EMPTY;
	    }
	    set 
	    {
		this.Selected = this.Equals(value);
		this.Expanded = this.ExpandSelected && this.Selected;
		foreach (MenuItem item in Items) 
		{
		    item.SelectedItem = value;
		}
	    }
	}

	public override MenuItem SetSelectedItemByUrl(String url) 
	{
	    if (url != null) 
	    {
		if (this.NavigateUrl.Equals(url)) {
		    return this;
		}

		foreach (MenuItem item in Items) 
		{
		    MenuItem selectedItem = item.SetSelectedItemByUrl(url);
		    if (!MenuItem.EMPTY.Equals(selectedItem)) 
		    {		
			return selectedItem;
		    }
		}
	    }
	    return MenuItem.EMPTY;
	}

	public void Render(HtmlTextWriter writer, Int32 indentLevel) {

	    if (Visible) {
		TableRow row = new TableRow();
		row.RenderBeginTag(writer);

		//	    for (Int32 i = 1; i < level; i++) {
		//		TableCell indentCell = new TableCell();
		//		indentCell.Width = indent;
		//		indentCell.Text = "&nbsp";
		//		row.Cells.Add(indentCell);
		//	    }

		//	    if (ShowSelectedImage) {
		//		TableCell imageCell = new TableCell();
		//		if (Selected && !String.Empty.Equals(SelectedItemImageUrl)) {
		//		    Image img = new Image();
		//		    img.ImageUrl = SelectedItemImageUrl;
		//		    imageCell.Controls.Add(img);
		//		} else {
		//		    imageCell.Width = indent;
		//		    imageCell.Text = "&nbsp;";
		//		}
		//
		//		row.Cells.Add(imageCell);
		//	    }

		TableCell cell = new TableCell();
		cell.RenderBeginTag(writer);

		Link.Style["margin-left"] = new Unit(Indent * indentLevel, UnitType.Pixel).ToString();
		if (this.Selected) {
		    Link.CssClass = "selectedmenuitem";
		} else {
		    Link.CssClass = "menuitem";
		}

		Link.RenderControl(writer);

		cell.RenderEndTag(writer);
		row.RenderEndTag(writer);

		if (Expanded) {
		    foreach (MenuItem item in Items) {
			item.Render(writer, indentLevel + 1);
		    }
		}
	    }
	}
    }
}
