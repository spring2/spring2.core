using System;
using System.Collections;

using NUnit.Framework;

using Spring2.Core.DataObject;
using Spring2.Core.Types;

namespace Spring2.Core.Test {
    /// <summary>
    /// Test suite for data objects.
    /// </summary>
    [TestFixture]
    public class DataObjectTest {

	private static readonly StringType PROP1_BEFORE_CHANGE = StringType.Parse("before change");
	private static readonly StringType PROP1_AFTER_CHANGE = StringType.Parse("after change");
	private static readonly IdType ID_VALUE = new IdType(1);
	private static readonly StringType CONTAINED_PROP1_BEFORE_CHANGE = StringType.Parse("contained before change");
	private static readonly StringType CONTAINED_PROP1_AFTER_CHANGE =  StringType.Parse("contained aftger change");
	private static readonly StringType ARRAY_1_VALUE = StringType.Parse("Array #1");
	private static readonly StringType ARRAY_2_VALUE = StringType.Parse("Array #2");
	private static readonly StringType ARRAY_3_VALUE = StringType.Parse("Array #3");
	private static readonly StringType ARRAY_CHANGE_ATTEMPT = StringType.Parse("Shouldn't See");


	/// <summary>
	/// Test the Update method
	/// </summary>
	[Test]
	public void TestUpdate() {
	    TestData data = GetTestData();

	    TestData updateData = new TestData();
	    updateData.Prop1 = PROP1_AFTER_CHANGE;
	    updateData.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    Test2Data temp = new Test2Data();
	    temp.Prop1 = ARRAY_CHANGE_ATTEMPT;
	    updateData.TestCollection1.Add(temp);

	    data.Update(updateData);

	    Assertion.Assert("Update - Id changed when shouldn't", data.Id.Equals(ID_VALUE));
	    Assertion.Assert("Property 1 in TestData did not change", data.Prop1.Equals(PROP1_AFTER_CHANGE));
	    Assertion.Assert("Property 1 in Test2Data did not change", data.DataObject1.Prop1.Equals(CONTAINED_PROP1_AFTER_CHANGE));
	    IEnumerator il = data.TestCollection1.GetEnumerator();
	    Assertion.Assert("First collection entry not found", il.MoveNext());
	    Assertion.Assert("First array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_1_VALUE));
	    Assertion.Assert("Second collection entry not found", il.MoveNext());
	    Assertion.Assert("Second array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_2_VALUE));
	    Assertion.Assert("Third collection entry not found", il.MoveNext());
	    Assertion.Assert("Third array value changed.", ((Test2Data)(il.Current)).Prop1.Equals(ARRAY_3_VALUE));
	    Assertion.Assert("added 4th collection entry.", !(il.MoveNext()));
	}

	/// <summary>
	/// Test the Equals method
	/// </summary>
	[Test]
	public void TestEquals() {
	    TestData data = GetTestData();
	    Assertion.Assert("Object does not equal itself", data.Equals(data));
	    TestData data2 = GetTestData();
	    Assertion.Assert("Equal objects not equal", data.Equals(data2));
	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    Assertion.Assert("Equals doesn't notice datatype in top level dataobject change", !data.Equals(data2));
	    data2.Prop1 = data.Prop1;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    Assertion.Assert("Equals doesn't notice datatype in contained dataobject change", !data.Equals(data2));
	    data2.DataObject1.Prop1 = data.DataObject1.Prop1;
	    IEnumerator il = data2.TestCollection1.GetEnumerator();
	    il.MoveNext();
	    il.MoveNext();
	    ((Test2Data)(il.Current)).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    Assertion.Assert("Equals doesn't notice datatype in list object change", !data.Equals(data2));
	}

	/// <summary>
	/// Test the Compare method
	/// </summary>
	[Test]
	public void TestCompare() {
	    TestData data = GetTestData();
	    DataObjectCompareList list = data.Compare(data);
	    Assertion.Assert("Object does not equal itself\n" + list.ToString(), list.Count == 0);
	    TestData data2 = GetTestData();
	    list = data.Compare(data2);
	    Assertion.Assert("Equal objects not equal", list.Count == 0);

	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    list = data.Compare(data2);
	    Assertion.Assert("Only change in one top level object - saw none\n" + list.ToString(), list.Count != 0);
	    Assertion.Assert("Only change in one top level object - saw more\n" + list.ToString(), list.Count == 1);
	    Assertion.Assert("One Change in top level object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.Prop1")
		&& list[0].Value1.Equals(PROP1_BEFORE_CHANGE) 
		&& list[0].Value2.Equals(PROP1_AFTER_CHANGE));

	    data2.Prop1 = data.Prop1;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    list = data.Compare(data2);
	    Assertion.Assert("Only change in one contained object - saw none\n" + list.ToString(), list.Count != 0);
	    Assertion.Assert("Only change in one contained object - saw more\n" + list.ToString(), list.Count == 1);
	    Assertion.Assert("One Change in contained object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.DataObject1.Prop1")
		&& list[0].Value1.Equals(CONTAINED_PROP1_BEFORE_CHANGE) 
		&& list[0].Value2.Equals(CONTAINED_PROP1_AFTER_CHANGE));

	    data2.DataObject1.Prop1 = data.DataObject1.Prop1;
	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    list = data.Compare(data2);
	    Assertion.Assert("Only change in one array object - saw none\n" + list.ToString(), list.Count != 0);
	    Assertion.Assert("Only change in one array object - saw more\n" + list.ToString(), list.Count == 1);
	    Assertion.Assert("One Change in array object - properties incorrect\n" + list.ToString(), 
		list[0].PropertyName.Equals("TestData.TestCollection1[1].Prop1")
		&& list[0].Value1.Equals(ARRAY_2_VALUE) 
		&& list[0].Value2.Equals(ARRAY_CHANGE_ATTEMPT));

	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = 
		((Test2Data)(data.TestCollection1[1])).Prop1;
	    data2.Prop1 = PROP1_AFTER_CHANGE;
	    data2.DataObject1.Prop1 = CONTAINED_PROP1_AFTER_CHANGE;
	    ((Test2Data)(data2.TestCollection1[1])).Prop1 = ARRAY_CHANGE_ATTEMPT;
	    list = data.Compare(data2);
	    Assertion.Assert("Expected 3 changes did not get them\n" + list.ToString(),
		list.Count == 3);
	    bool foundTop = false;
	    bool foundContained = false;
	    bool foundArray = false;
	    for (int i=0;i<3;i++) {
		if (list[i].PropertyName.Equals("TestData.Prop1")
		    && list[i].Value1.Equals(PROP1_BEFORE_CHANGE) 
		    && list[i].Value2.Equals(PROP1_AFTER_CHANGE)) {
		    foundTop = true;
		}
		if (list[i].PropertyName.Equals("TestData.TestCollection1[1].Prop1")
		    && list[i].Value1.Equals(ARRAY_2_VALUE) 
		    && list[i].Value2.Equals(ARRAY_CHANGE_ATTEMPT)) {
		    foundArray = true;
		}
		if (list[i].PropertyName.Equals("TestData.DataObject1.Prop1")
		    && list[i].Value1.Equals(CONTAINED_PROP1_BEFORE_CHANGE) 
		    && list[i].Value2.Equals(CONTAINED_PROP1_AFTER_CHANGE)) {
		    foundContained = true;
		}
	    }
	    
	    Assertion.Assert("Top level change in multi change not found\n" + list.ToString(), foundTop);
	    Assertion.Assert("Contained change in multi change not found\n" + list.ToString(), foundContained);
	    Assertion.Assert("Array change in multi change not found\n" + list.ToString(), foundArray);

	    data = GetTestData();
	    data2 = GetTestData();
	    data.Prop1 = StringType.DEFAULT;
	    data2.Prop1 = StringType.UNSET;
	    list = data.Compare(data2);
	    Assertion.Assert("Default Matched Unset on COMPARE_ALL\n" + list.ToString(), 
		list.Count != 0);

	    list = data.Compare(data2, DataObjectCompareOptionEnum.DEFAULT_EQUALS_UNSET);
	    Assertion.Assert("Default didn't match Unset on DEFAULT_EQUALS_UNSET\n" + list.ToString(),
		list.Count == 0);

	    data2.Prop1 = PROP1_BEFORE_CHANGE;
	    list = data.Compare(data2, DataObjectCompareOptionEnum.IGNORE_DEFAULT);
	    Assertion.Assert("Default didn't match value on IgnoreDefault\n" + list.ToString(),
		list.Count == 0);
	}

	/// <summary>
	/// Get a generic test data item with values set.
	/// </summary>
	/// <returns>Item to test against.</returns>
	private TestData GetTestData() {
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
