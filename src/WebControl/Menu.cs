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
	private String completedItemStyle = String.Empty;
	private Int32 indent;
	private String navigateUrl = String.Empty;
	private Boolean active;
	private String selectedImageUrl = String.Empty;
	private String unselectedImageUrl = String.Empty;
	private String description = String.Empty;
	private String descriptionCssClass = String.Empty;

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

	/// <summary>
	/// Gets or sets the enabled property for the menu.
	/// </summary>
	public new bool Enabled 
	{
	    get
	    {
		return base.Enabled;
	    }
	    set
	    {
		base.Enabled = value;
		foreach (MenuItem item in Items)
		{
		    item.Enabled = value;
		}
	    }
	}

	/// <summary>
	/// Gets or sets the description to be rendered beneath the menu link.
	/// </summary>
	public String Description
	{
	    get { return description; }
	    set { description = value == null?String.Empty:value; }
	}

	/// <summary>
	/// Gets or sets the css class to be used with the description.
	/// </summary>
	public String DescriptionCssClass
	{
	    get { return descriptionCssClass; }
	    set { descriptionCssClass = value == null?String.Empty:value; }
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

	protected void RenderLink(HtmlTextWriter writer)
	{
	    if (Selected || !Enabled)
	    {
		Link.NavigateUrl = String.Empty;
	    }
	    Link.RenderControl(writer);
	    if (Description != String.Empty)
	    {
		writer.Write("<div id=\"leftdescription\" ");
		if (DescriptionCssClass != String.Empty)
		{
		    writer.Write(" class=\"" + DescriptionCssClass + "\"");
		}
		writer.Write(">" + Description + "</div>");
	    }
	}

	protected override void Render(HtmlTextWriter writer) {

	    if (Visible) {
		
		writer.Write("<div id=\"left\" class=\"" + this.CssClass + "\">");

		Literal beginHeader = new Literal();
		beginHeader.Text = "<h1>";

		Literal endHeader = new Literal();
		endHeader.Text = "</h1>";

		beginHeader.RenderControl(writer);
		RenderLink(writer);
		endHeader.RenderControl(writer);

		foreach (MenuItem item in Items) {
		    item.Render(writer, 0);
		}

		writer.Write("</div>");
	    }
	}
    }
}
