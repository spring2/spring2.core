using System;
using System.Collections;
using System.Reflection;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.DataObject {
    
    /// <summary>
    /// Abstract base class for data objects.
    /// </summary>
    [Serializable()]
    public abstract class DataObject {

	public override String ToString() {

	    StringBuilder sb = new StringBuilder();
	    foreach (PropertyInfo p in GetType().GetProperties()) {
		
		Object value = p.GetValue(this, null);
		if (value is ICollection) {
		    ICollection collection = (ICollection)value;
		    foreach (Object item in collection) {
			sb.Append("\tItem: ").Append(item.ToString());
		    }
		}

		sb.Append(p.Name + ": ")
		    .Append(p.GetValue(this, null))
		    .Append(Environment.NewLine);
	    }

	    return sb.ToString();
	}

	public void Update(DataObject that) {

	    if (GetType().IsInstanceOfType(that)) {
		foreach (PropertyInfo p in GetType().GetProperties()) {
		    Object value = p.GetValue(that, null);
		    if (value is DataType) {
			if (!((DataType)value).IsDefault) {
			    p.SetValue(this, value, null);
			}
		    } 
		}
	    }
	}
    }
}
