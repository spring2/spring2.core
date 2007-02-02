using System;
using System.Collections;
using NUnit.Framework;

using Spring2.Core.DAO;
using Spring2.Core.Message;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.TestUtility;
using Spring2.Core.ResourceManager.Facade;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Test {


    /// <summary>
    /// Summary description for ResourceEditorTest.
    /// </summary>
    [TestFixture()]
    public class ResourceLocalizerTest {

	TestUtil testUtil = new TestUtil();
	ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());

	[SetUp()]
	public void RemoveTestData() {
	    testUtil.RemoveTestData();
	}

	[Test()]
	public void TestGettingLocalizedText() {
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    String localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	}

	[TearDown()]
	public void Cleanup() {
	    testUtil.RemoveTestData();
	}

    }
}