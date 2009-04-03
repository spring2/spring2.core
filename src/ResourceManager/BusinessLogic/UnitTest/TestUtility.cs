using System;
using System.Text;

using Spring2.Core.Types;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Test {
    public class TestUtility {

	public const String TEST_CONTEXT = "TestContextForTests";
	Random random = new Random(DateTime.Now.Millisecond);
	private ILocale exampleILocale = Spring2.Core.Types.LocaleEnum.UNSET;
	private ILanguage exampleILanguage = Spring2.Core.Types.LanguageEnum.UNSET;
	
	public void CleanupTestResources() {
	    ResourceList list = ResourceDAO.DAO.FindByContext(StringType.Parse(TEST_CONTEXT));
	    foreach(IResource resource in list) {
		DeleteLocalizedResources(resource.ResourceId);
		ResourceDAO.DAO.Delete(resource.ResourceId);
	    }
	}

	public Resource CreateTestResource() {
	    return Resource.Create(StringType.Parse(TEST_CONTEXT), StringType.Parse(DateTime.Now.ToShortTimeString()), IdType.Parse(random.Next().ToString()));
	}

	public void DeleteLocalizedResources(IdType resourceId) {
	    LocalizedResourceList list = LocalizedResourceDAO.DAO.FindByResourceId(resourceId);
	    foreach(ILocalizedResource localizedResource in list) {
		LocalizedResourceDAO.DAO.Delete(localizedResource.LocalizedResourceId);
	    }
	}

	public void CleanupTestLocalizedResources() {
	    this.CleanupTestResources();
	}

	public LocalizedResource CreateTestLocalizedResource() {
	    Resource resource = this.CreateTestResource();
	    return LocalizedResource.Create(resource.ResourceId, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH, "Create Test Localized Resource From TestUtility");
	}

    }
}
