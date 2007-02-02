using System;
using System.Collections;
using System.Configuration;

using NUnit.Framework;
using Spring2.Core.DAO;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Test {


    /// <summary>
    /// Summary description for AccountTest.
    /// </summary>
    [TestFixture()]
    public class LocalizedResourceTest {

	TestUtility testUtility = new TestUtility();

	/// <summary>
	/// Makes sure all test LocalizedResources and Resources are removed.
	/// </summary>
	[SetUp()]
	public void CleanTestResources() {
	    LocalizedResourceDAO.DAO.LocaleFactory = new LocaleFactory();
	    LocalizedResourceDAO.DAO.LanguageFactory = new LanguageFactory();
	    testUtility.CleanupTestLocalizedResources();
	}

	/// <summary>
	/// Tests creation of LocalizedResource
	/// </summary>
	[Test()]
	public void CreateLocalizedResourceTest() {
	    Resource resource = testUtility.CreateTestResource();
	    try {
		LocalizedResource.Create(resource.ResourceId, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH, "CreateLocalizedResourceTest");
	    } catch {
		Assert.Fail("Should have been able to create localized resource");
	    }
	}

	[Test()]
	public void GetByResourceIdLocaleAndLanguage() {
	    LocalizedResource localizedResource = testUtility.CreateTestLocalizedResource();
	    try {
		LocalizedResource.GetInstance(localizedResource.ResourceId, localizedResource.Locale, localizedResource.Language);
	    } catch(FinderException) {
		Assert.Fail("Should have found localized resource");
	    }
	}

	[Test()]
	public void UpdateTest() {
	    LocalizedResource localizedResource = testUtility.CreateTestLocalizedResource();

	    LocalizedResourceData data = new LocalizedResourceData();
	    data.Content = "Blah Blah Blah";
	    try {
		localizedResource.Update(data);
	    } catch {
		Assert.Fail("Should have been able to update localized resource content");
	    }
	}

	// Should always be the last test in the class
	[TearDown()]
	public void CleanupTestData() {
	    testUtility.CleanupTestLocalizedResources();
	}
    }
}