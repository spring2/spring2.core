using System;
using System.Collections;

using Spring2.Core.DataObject;
using Spring2.Core.Types;

namespace Spring2.Core.Test {
    /// <summary>
    /// This is a class derived from DataObject to be used for testing DataObject
    /// </summary>
    public class TestData : Spring2.Core.DataObject.DataObject {
	private IdType id = IdType.DEFAULT;
	private StringType prop1 = StringType.DEFAULT;
	private Test2Data dataObject1 = new Test2Data();
	private IList testCollection1 = new ArrayList();

	public IdType Id {
	    get { return this.id; }
	    set { this.id = value; }
	}

	public StringType Prop1 {
	    get { return this.prop1; }
	    set { this.prop1 = value; }
	}

	public Test2Data DataObject1 {
	    get { return this.dataObject1; }
	    set { this.dataObject1 = value; }
	}

	public IList TestCollection1 {
	    get { return this.testCollection1; }
	    set { this.testCollection1 = value; }
	}
    }
}
