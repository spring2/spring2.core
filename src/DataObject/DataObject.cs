using System;
using System.Data;

namespace Spring2.Core.DataObject {
    
    /// <summary>
    /// Abstract base class for data objects.
    /// </summary>
    [Serializable()]
    public abstract class DataObject {

	public override String ToString() {
	    return this.GetType().ToString();   
	}
    }
}
