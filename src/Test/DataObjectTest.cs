using System;
using System.Collections;

using NUnit.Framework;

using Spring2.Core.Types;

namespace Spring2.Core.Test
{
    /// <summary>
    /// Test suite for data objects.
    /// </summary>
    public class DataObjectTest : NUnit.Framework.TestCase
    {
	private static readonly StringType PROP1_BEFORE_CHANGE = new StringType("before change");
	private static readonly StringType PROP1_AFTER_CHANGE = new StringType("after change");
	private static readonly IdType ID_VALUE = new IdType(1);
	private static readonly StringType CONTAINED_PROP1_BEFORE_CHANGE = new StringType("contained before change");
	private static readonly StringType CONTAINED_PROP1_AFTER_CHANGE =  new StringType("contained aftger change");
	private static readonly StringType ARRAY_1_VALUE = new StringType("Array #1");
	private static readonly StringType ARRAY_2_VALUE = new StringType("Array #2");
	private static readonly StringType ARRAY_3_VALUE = new StringType("Array #3");
	private static readonly StringType ARRAY_CHANGE_ATTEMPT = new StringType("Shouldn't See");

	private static readonly string UPDATE = "Update";
	private static readonly string EQUALS = "Equals";

	public DataObjectTest(string name) : base(name) {}

	
	public static ITest Suite 
	{ 
	    get 
	    {
		TestSuite suite= new TestSuite(); 
		suite.AddTest(new DataObjectTest(UPDATE)); 
		suite.AddTest(new DataObjectTest(EQUALS)); 
		return suite;
	    }
	}
	protected override void RunTest()
	{
	    if (Name.Equals(UPDATE))
	    {
		TestUpdate();
	    }
	    if (Name.Equals(EQUALS))
	    {
		TestEquals();
	    }
	}

	/// <summary>
	/// Test the Update method
	/// </summary>
	protected void TestUpdate()
	{
	    TestData data = GetTestData();

	    TestData updateData = new TestData();
	    updateData.Prop1 = PROP1_AFTER_CHANGE;
	    updateData.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    Test2Data temp = new Test2Data();
	    temp.Prop1 = ARRAY_CHANGE_ATTEMPT;
	    updateData.TestCollection1.Add(temp);

	    data.Update(updateData);

	    Assert("Update - Id changed when shouldn't", data.Id.Equals(ID_VALUE));
	    Assert("Property 1 in TestData did not change", data.Prop1.Equals(PROP1_AFTER_CHANGE));
	    Assert("Property 1 in Test2Data did not change", data.DataObject1.Prop1.Equals(CONTAINED_PROP1_AFTER_CHANGE));
	    IEnumerator il = data.TestCollection1.GetEnumerator();
	    Assert("First collection entry not found", il.MoveNext());
	    Assert("First array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_1_VALUE));
	    Assert("Second collection entry not found", il.MoveNext());
	    Assert("Second array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_2_VALUE));
	    Assert("Third collection entry not found", il.MoveNext());
	    Assert("Third array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_3_VALUE));
	    Assert("added 4th collection entry.", !(il.MoveNext()));
	}

	/// <summary>
	/// Test the Equals method
	/// </summary>
	protected void TestEquals()
	{
	    TestData data = GetTestData();
	    Assert("Object does not equal itself", data.Equals(data));
	    TestData data2 = GetTestData();
	    Assert("Equal objects not equal", data.Equals(data2));
	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    Assert("Equals doesn't notice datatype in top level dataobject change", !data.Equals(data2));
	    data2.Prop1 = data.Prop1;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    Assert("Equals doesn't notice datatype in contained dataobject change", !data.Equals(data2));
	    data2.DataObject1.Prop1 = data.DataObject1.Prop1;
	    IEnumerator il = data2.TestCollection1.GetEnumerator();
	    il.MoveNext();
	    il.MoveNext();
	    ((Test2Data)(il.Current)).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    Assert("Equals doesn't notice datatype in list object change", !data.Equals(data2));
	}

	private TestData GetTestData()
	{
	    TestData data = new TestData();
	    data.Id = new IdType(1);
	    data.Prop1 = PROP1_BEFORE_CHANGE;
	    
	    Test2Data temp = new Test2Data();
	    temp.Prop1 = ARRAY_1_VALUE;
	    data.TestCollection1.Add(temp);
	    temp = new Test2Data();
	    temp.Prop1 = ARRAY_2_VALUE;
	    data.TestCollection1.Add(temp);
	    temp = new Test2Data();
	    temp.Prop1 = ARRAY_3_VALUE;
	    data.TestCollection1.Add(temp);

	    data.DataObject1.Prop1 = CONTAINED_PROP1_BEFORE_CHANGE;

	    return data;
	}
    }
}
