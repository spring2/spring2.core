using System;
using System.Text;
using System.Data;
using System.Reflection;

namespace Spring2.Core.DataObject {
    
    /// <summary>
    /// Abstract base class for data objects.
    /// </summary>
    [Serializable()]
    public abstract class DataObject {

	public override String ToString() {
	    StringBuilder sb = new StringBuilder();
	    Type t = this.GetType();
	    foreach (PropertyInfo p in t.GetProperties()) {
		sb.Append(p.Name + ": ");
		sb.Append(p.GetValue(this, null));
		sb.Append(Environment.NewLine);
	    }

	    return sb.ToString();
	}
    }
}
