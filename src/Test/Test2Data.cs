using System;

using Spring2.Core.DataObject;
using Spring2.Core.Types;

namespace Spring2.Core.Test {
    /// <summary>
    /// Contained data object for testing dagta objects
    /// </summary>
    public class Test2Data : Spring2.Core.DataObject.DataObject {
	private StringType prop1 = StringType.DEFAULT;

	public StringType Prop1 {
	    get { return this.prop1; }
	    set { this.prop1 = value; }
	}
    }
}
