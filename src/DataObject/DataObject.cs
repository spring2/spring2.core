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
	public override Boolean Equals(Object obj) {
	    if (obj == null) {
		return false;
	    }

	    if (obj == this) {
		return true;
	    }

	    if (!GetType().Equals(obj.GetType())) {
		return false;
	    }

	    foreach (PropertyInfo p in GetType().GetProperties()) {
		Object value1 = p.GetValue(this, null);
		Object value2 = p.GetValue(obj, null);
		if (value1 is ICollection) {
		    ICollection collection1 = (ICollection)value1;
		    ICollection collection2 = (ICollection)value2;
		    if (collection1.Count != collection2.Count) {
			return false;
		    }
		    
		    IEnumerator enumerator1 = collection1.GetEnumerator();
		    IEnumerator enumerator2 = collection2.GetEnumerator();
		    while (enumerator1.MoveNext() && enumerator2.MoveNext()) {
			if (!enumerator1.Current.Equals(enumerator2.Current)) {
			    return false;
			}
		    }
		}
		else {
		    if (!value1.Equals(value2)) {
			return false;
		    }
		}
	    }

	    return true;
	}

	/// <summary>
	/// Does a deep compare of the current object with the passed object.
	/// </summary>
	/// <param name="obj">Object to compare current object with.</param>
	/// <returns>List of differences between the objects.</returns>
	public DataObjectCompareList Compare(DataObject obj) {
	    return Compare(obj, this.GetType().Name, DataObjectCompareOptionEnum.COMPARE_ALL);
	}

	/// <summary>
	/// Does a deep compare of the current object with the passed object.
	/// </summary>
	/// <param name="obj">Object to compare current object with.</param>
	/// <param name="option">Allows customization of how DEFAULT values are compared.</param>
	/// <returns>List of differences between the objects.</returns>
	public DataObjectCompareList Compare(DataObject obj, DataObjectCompareOptionEnum option) {
	    return Compare(obj, this.GetType().Name, option);
	}

	/// <summary>
	/// Does a deep compare of the current object with the passed object.
	/// </summary>
	/// <param name="obj">Object to compare current object with.</param>
	/// <param name="prefix">Prefix to use when identifying properties.</param>
	/// <param name="option">Allows customization of how DEFAULT values are compared.</param>
	/// <returns>List of differences between the objects.</returns>
	public DataObjectCompareList Compare(DataObject obj, String prefix,
	    DataObjectCompareOptionEnum option) {
	    DataObjectCompareList returnValue = new DataObjectCompareList();
	    if (obj == null) {
		returnValue.Add(new DataObjectCompareDetail(prefix, 
		    StringType.NewInstance("Data Object"), StringType.NewInstance("Null Data Object")));
		return returnValue;
	    }

	    if (obj == this) {
		return returnValue;
	    }

	    if (!GetType().Equals(obj.GetType())) {
		returnValue.Add(new DataObjectCompareDetail(prefix, 
		    StringType.NewInstance("different types"), 
		    StringType.NewInstance("different types")));
		return returnValue;
	    }

	    foreach (PropertyInfo p in GetType().GetProperties()) {
		Object value1 = p.GetValue(this, null);
		Object value2 = p.GetValue(obj, null);
		String valuePrefix = prefix + "." + p.Name;
		if (value1 is ICollection && value2 is ICollection) {
		    ICollection collection1 = (ICollection)value1;
		    ICollection collection2 = (ICollection)value2;
		    if (collection1.Count != collection2.Count) {
			returnValue.Add(new DataObjectCompareDetail(
			    valuePrefix
			    + ".Count", 
			    new NumberType(collection1.Count), 
			    new NumberType(collection2.Count)));
		    }
		    else {
			IEnumerator enumerator1 = collection1.GetEnumerator();
			IEnumerator enumerator2 = collection2.GetEnumerator();
			int whichOccurrence = 0;
			while (enumerator1.MoveNext() && enumerator2.MoveNext()) {
			    String collectionPrefix = valuePrefix
				+ "[" + whichOccurrence.ToString() + "]";
			    if (enumerator1.Current is DataObject && enumerator2.Current is DataObject) {
				returnValue.Append(((DataObject)(enumerator1.Current)).Compare(
				    (DataObject)(enumerator2.Current), 
				    collectionPrefix, option));
			    } 
			    else  if (enumerator1.Current is DataType 
				&& enumerator2.Current is DataType) {
				if (!CompareDataTypes((DataType)(enumerator1.Current),
				    (DataType)(enumerator2.Current),
				    option)) {
				    returnValue.Add(new DataObjectCompareDetail(collectionPrefix,
					(DataType)(enumerator1.Current),
					(DataType)(enumerator2.Current)));
				}
			    }
			    else {
				if (!enumerator1.Current.Equals(enumerator2.Current)) {
				    returnValue.Add(new DataObjectCompareDetail(
					collectionPrefix,
					StringType.NewInstance("Not DataType"),
					StringType.NewInstance("Not DataType")));
				}
			    }
			    whichOccurrence++;
			}
		    }
		}
		else if (value1 is DataObject && value2 is DataObject) {
		    returnValue.Append(((DataObject)value1).Compare(
			(DataObject)value2,
			valuePrefix, option));
		}
		else {
		    if (!value1.Equals(value2)) { 
			if (value1 is DataType && value2 is DataType) {
			    if (!CompareDataTypes((DataType)value1, (DataType)value2, option)) {
				returnValue.Add(new DataObjectCompareDetail(valuePrefix,
				    (DataType)value1,
				    (DataType)value2));
			    }
			}
			else {
			    DataType v1;
			    DataType v2;
			    if (value1 is DataType) {
				v1 = (DataType)value1;
			    }
			    else {
				v1 = StringType.NewInstance("Not DataType");
			    }
			    if (value2 is DataType) {
				v2 = (DataType)value2;
			    }
			    else {
				v2 = StringType.NewInstance("Not DataType");
			    }
			    returnValue.Add(new DataObjectCompareDetail(valuePrefix, v1, v2));
			}
		    }
		}
	    }

	    return returnValue;
	}

	/// <summary>
	/// Returns a hash code.
	/// </summary>
	/// <returns>Hash code value</returns>
	public override int GetHashCode() {
	    int hashCode = 0;
	    foreach (PropertyInfo p in GetType().GetProperties()) {
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
	public void Update(DataObject that) {

	    if (GetType().IsInstanceOfType(that)) {
		foreach (PropertyInfo p in GetType().GetProperties()) {
		    Object value = p.GetValue(that, null);
		    if (value is DataType) {
			if (!((DataType)value).IsDefault) {
			    p.SetValue(this, value, null);
			}
		    } 
		    else if (value is DataObject) {
			((DataObject)(p.GetValue(this, null))).Update((DataObject)value);
		    }
		}
	    }
	}

	/// <summary>
	/// Compares two data types based on the enum passed.
	/// </summary>
	/// <param name="v1">First Value</param>
	/// <param name="v2">Second Value</param>
	/// <param name="option">option value to use</param>
	private bool CompareDataTypes(DataType v1, DataType v2,
	    DataObjectCompareOptionEnum option) {
	    bool found = false;
	    if (v1.Equals(v2)) {
		found = true;
	    }
	    if (!found) {
		if (option.Equals(DataObjectCompareOptionEnum.IGNORE_DEFAULT)
		    && (v1.IsDefault || v2.IsDefault)) {
		    found = true;
		}
		else if (option.Equals(DataObjectCompareOptionEnum.DEFAULT_EQUALS_UNSET)
		    && ((v1.IsUnset && v2.IsDefault)
		    || (v1.IsDefault && v2.IsUnset))) {
		    found = true;
		}
	    }

	    return found;
	}
    }
}
