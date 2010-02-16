using System;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.DataObject;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for EnumDataTypeList
    /// </summary>
    [TestFixture]
    public class EnumDataTypeListTest {

	[Test]
	public void SortByName() {
	    USStateCodeEnum.Options.Sort(new EnumDataTypeNameSorter());
	    Assert.AreEqual("AK", USStateCodeEnum.Options[0].Name);
	}

	[Test]
	public void SortByNameDesc() {
	    USStateCodeEnum.Options.Sort(new EnumDataTypeReverseNameSorter());
	    Assert.AreEqual("WY", USStateCodeEnum.Options[0].Name);
	}

	[Test]
	public void SortByNameMethod() {
	    USStateCodeEnum.Options.Sort(new EnumDataTypeReverseNameSorter());
	    USStateCodeEnum.Options.SortByName();
	    Assert.AreEqual("AK", USStateCodeEnum.Options[0].Name);
	}

	[Test]
	public void SortByNameDescMethod() {
	    USStateCodeEnum.Options.SortByName();
	    USStateCodeEnum.Options.SortByNameDesc();
	    Assert.AreEqual("WY", USStateCodeEnum.Options[0].Name);
	}
    }
}
