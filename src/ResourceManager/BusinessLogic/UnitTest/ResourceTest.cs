using System;
using System.Collections;
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
    /// Summary description for CompanyTest.
    /// </summary>
    [TestFixture()]
    public class ResourceTest {

	TestUtility testUtility = new TestUtility();

	/// <summary>
	/// Makes sure all test resources are removed.
	/// </summary>
	[SetUp()]
	public void CleanTestResources() {
	    LocalizedResourceDAO.DAO.LocaleFactory = new LocaleFactory();
	    LocalizedResourceDAO.DAO.LanguageFactory = new LanguageFactory();
	    testUtility.CleanupTestResources();
	}

	/// <summary>
	/// Tests creation of resource with Context, Field, and ContextIdentity
	/// Not able to create resource without Identity
	/// </summary>
	[Test()]
	public void CreateResourceTest() {
	    try {
		Resource resource = Resource.Create(StringType.Parse(TestUtility.TEST_CONTEXT), StringType.Parse(DateTime.Now.ToShortTimeString()), IdType.Parse(DateTime.Now.Millisecond.ToString()));
	    } catch {
		Assert.Fail("Should have been able to create resource");
	    }
	}

	/// <summary>
	/// Expect that valid Identity will be passed in.
	/// </summary>
	[Test()]
	public void FailCreateWithoutIdentity() {
	    try {
	        Resource.Create(StringType.Parse(TestUtility.TEST_CONTEXT), StringType.Parse(DateTime.Now.ToShortTimeString()), IdType.UNSET);
	        Assert.Fail("Should not have been able to create resource without Identity");
	    } catch (InvalidArgumentException) {
	        //Expected
	    }
	}

	[Test()]
	public void GetContextList() {
	    testUtility.CreateTestResource();
	    StringTypeList list = Resource.GetContextList();
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Context");
	}

	[Test()]
	public void GetFieldListByContext() {
	    IResource resource = testUtility.CreateTestResource();
	    StringTypeList list = Resource.GetFieldList(resource.Context);
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Field");
	}

	[Test()]
	public void GetIdentityListByContextAndField() {
	    IResource resource = testUtility.CreateTestResource();
	    IResource resource2 = testUtility.CreateTestResource();
	    IdTypeList list = Resource.GetIdentityList(resource.Context, resource.Field);
	    Assert.IsTrue(list.Count > 1, "Should have found at least 2 Identities");
	}

	[Test()]
	public void GetResourcesByContext() {
	    testUtility.CreateTestResource();
	    ResourceList list = Resource.GetListByContext(StringType.Parse(TestUtility.TEST_CONTEXT));
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Resource");
	}

	[Test()]
	public void GetResourcesByContextAndField() {
	    IResource resource = testUtility.CreateTestResource();
	    ResourceList list = Resource.GetListByContextAndField(resource.Context, resource.Field);
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Resource");
	}

	[Test()]
	public void CopyResourceAndLocalization() {
	    Random random = new Random(DateTime.Now.Millisecond);
	    LocalizedResource localizedResource = testUtility.CreateTestLocalizedResource();
	    LocalizedResourceList newLocalizedResources = new LocalizedResourceList();
	    try {
		newLocalizedResources = localizedResource.Resource.CopyResourceAndLocalization(IdType.Parse(random.Next().ToString()));
	    } catch {
		Assert.Fail("Should have been able to copy resource and localization");
	    }
	    Assert.IsTrue(newLocalizedResources.Count > 0, "Should have had at least one localized resource created.");
	}

	[Test()]
	public void CopyResourcesAndLocalizationFailure() {
	    Resource resource1 = testUtility.CreateTestResource();
	    Resource resource2 = testUtility.CreateTestResource();
	    resource2.Field = resource1.Field;
	    resource2.Store();
	    try {
		resource1.CopyResourceAndLocalization(resource2.Identity);
		Assert.Fail("Should not have been able to create copy of resource");
	    } catch(InvalidValueException) {
		//Expected
	    }
	}

        
        // Should always be the last test in the class
        [TearDown()]
	public void CleanupTestData() {
	    testUtility.CleanupTestResources();
        }
    }
}