using System;
using System.Collections;

namespace Spring2.Core.WebControl
{
    /// <summary>
    /// Summary description for MenuItemCollection.
    /// </summary>
    public class MenuItemCollection : CollectionBase {

	public MenuItem this[Int32 index] {
	    get {
		return (MenuItem)InnerList[index];
	    }
	}

	public void CopyTo(MenuItem[] array, Int32 index) {
	    InnerList.CopyTo(array, index);
	}

	public Int32 Add(MenuItem item) {
	    return InnerList.Add(item);
	}

	public Boolean Contains(MenuItem item) {
	    return InnerList.Contains(item);
	}

	public Int32 IndexOf(MenuItem item) {
	    return InnerList.IndexOf(item);
	}

	public void Insert(Int32 index, MenuItem item) {
	    InnerList.Insert(index, item);
	}

	public void Remove(MenuItem item) {
	    InnerList.Remove(item);
	}
    }
}
