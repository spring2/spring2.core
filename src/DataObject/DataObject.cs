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

	/// <summary>
	/// Creates a string representation of the DataObject.
	/// </summary>
	/// <returns>String representing the data object.</returns>
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

	/// <summary>
	/// Does a deep compare of the current object with the passed object.
	/// </summary>
	/// <param name="obj">Object to compare current object with.</param>
	/// <returns>True if objects are equal, false otherwise.</returns>
	public override Boolean Equals(Object obj) 
	{
	    if (obj == null) 
	    {
		return false;
	    }

	    if (obj == this)
	    {
		return true;
	    }

	    if (!GetType().Equals(obj.GetType()))
	    {
		return false;
	    }

	    foreach (PropertyInfo p in GetType().GetProperties()) 
	    {
		Object value1 = p.GetValue(this, null);
		Object value2 = p.GetValue(obj, null);
		if (value1 is ICollection) 
		{
		    ICollection collection1 = (ICollection)value1;
		    ICollection collection2 = (ICollection)value2;
		    if (collection1.Count != collection2.Count)
		    {
			return false;
		    }
		    
		    IEnumerator enumerator1 = collection1.GetEnumerator();
		    IEnumerator enumerator2 = collection2.GetEnumerator();
		    while (enumerator1.MoveNext() && enumerator2.MoveNext())
		    {
			if (!enumerator1.Current.Equals(enumerator2.Current))
			{
			    return false;
			}
		    }
		}
		else
		{
		    if (!value1.Equals(value2))
		    {
			return false;
		    }
		}
	    }

	    return true;
	}

	/// <summary>
	/// Returns a hash code.
	/// </summary>
	/// <returns>Hash code value</returns>
	public override int GetHashCode() 
	{
	    int hashCode = 0;
	    foreach (PropertyInfo p in GetType().GetProperties()) 
	    {
		hashCode = (hashCode + p.GetValue(this, null).GetHashCode()) % 2000000000;
	    }

	    return hashCode;
	}

	/// <summary>
	/// Updates the current DataObject instance with values from the
	/// passed DataObject instance.  Contained DataObjects are also updated.
	/// Instance values set to Default are not copied.  Collections in the 
	/// DataObject are ignored.
	/// </summary>
	/// <param name="that">Object to copy from.</param>
	public void Update(DataObject that) 
	{

	    if (GetType().IsInstanceOfType(that)) {
		foreach (PropertyInfo p in GetType().GetProperties()) {
		    Object value = p.GetValue(that, null);
		    if (value is DataType) 
		    {
			if (!((DataType)value).IsDefault)
			{
			    p.SetValue(this, value, null);
			}
		    } 
		    else if (value is DataObject) 
		    {
			((DataObject)(p.GetValue(this, null))).Update((DataObject)value);
		    }
		}
	    }
	}

	private void SetValue(PropertyInfo p, Object value) 
	{
	}
    }
}
