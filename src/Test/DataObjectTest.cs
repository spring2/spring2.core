using System;
using System.Collections;

using NUnit.Framework;

using Spring2.Core.DataObject;
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
	private static readonly string COMPARE = "Compare";

	public DataObjectTest(string name) : base(name) {}

	
	public static ITest Suite 
	{ 
	    get 
	    {
		TestSuite suite= new TestSuite(); 
		suite.AddTest(new DataObjectTest(UPDATE)); 
		suite.AddTest(new DataObjectTest(EQUALS)); 
		suite.AddTest(new DataObjectTest(COMPARE));
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
	    if (Name.Equals(COMPARE))
	    {
		TestCompare();
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

	/// <summary>
	/// Test the Compare method
	/// </summary>
	protected void TestCompare()
	{
	    TestData data = GetTestData();
	    DataObjectCompareList list = data.Compare(data);
	    Assert("Object does not equal itself\n" + list.ToString(), list.Count == 0);
	    TestData data2 = GetTestData();
	    list = data.Compare(data2);
	    Assert("Equal objects not equal", list.Count == 0);

	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    list = data.Compare(data2);
	    Assert("Only change in one top level object - saw none\n" + list.ToString(), list.Count != 0);
	    Assert("Only change in one top level object - saw more\n" + list.ToString(), list.Count == 1);
	    Assert("One Change in top level object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.Prop1")
		&& list[0].Value1.Equals(PROP1_BEFORE_CHANGE) 
		&& list[0].Value2.Equals(PROP1_AFTER_CHANGE));

	    data2.Prop1 = data.Prop1;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    list = data.Compare(data2);
	    Assert("Only change in one contained object - saw none\n" + list.ToString(), list.Count != 0);
	    Assert("Only change in one contained object - saw more\n" + list.ToString(), list.Count == 1);
	    Assert("One Change in contained object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.DataObject1.Prop1")
		&& list[0].Value1.Equals(CONTAINED_PROP1_BEFORE_CHANGE) 
		&& list[0].Value2.Equals(CONTAINED_PROP1_AFTER_CHANGE));

	    data2.DataObject1.Prop1 = data.DataObject1.Prop1;
	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    list = data.Compare(data2);
	    Assert("Only change in one array object - saw none\n" + list.ToString(), list.Count != 0);
	    Assert("Only change in one array object - saw more\n" + list.ToString(), list.Count == 1);
	    Assert("One Change in array object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.TestCollection1[1].Prop1")
		&& list[0].Value1.Equals(ARRAY_2_VALUE) 
		&& list[0].Value2.Equals(ARRAY_CHANGE_ATTEMPT));

	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = 
		((Test2Data)(data.TestCollection1[1])).Prop1;
	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    list = data.Compare(data2);
	    Assert("Expected 3 changes did not get them\n" + list.ToString(),
		list.Count == 3);
	    bool foundTop = false;
	    bool foundContained = false;
	    bool foundArray = false;
	    for (int i=0;i<3;i++)
	    {
		if (list[i].PropertyName.Equals("TestData.Prop1")
		    && list[i].Value1.Equals(PROP1_BEFORE_CHANGE) 
		    && list[i].Value2.Equals(PROP1_AFTER_CHANGE))
		{
		    foundTop = true;
		}
		if (list[i].PropertyName.Equals("TestData.TestCollection1[1].Prop1")
		    && list[i].Value1.Equals(ARRAY_2_VALUE) 
		    && list[i].Value2.Equals(ARRAY_CHANGE_ATTEMPT))
		{
		    foundArray = true;
		}
		if (list[i].PropertyName.Equals("TestData.DataObject1.Prop1")
		    && list[i].Value1.Equals(CONTAINED_PROP1_BEFORE_CHANGE) 
		    && list[i].Value2.Equals(CONTAINED_PROP1_AFTER_CHANGE))
		{
		    foundContained = true;
		}
	    }
	    
	    Assert("Top level change in multi change not found\n" + list.ToString(), foundTop);
	    Assert("Contained change in multi change not found\n" + list.ToString(), foundContained);
	    Assert("Array change in multi change not found\n" + list.ToString(), foundArray);
	}

	/// <summary>
	/// Get a generic test data item with values set.
	/// </summary>
	/// <returns>Item to test against.</returns>
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
