using System;
#if (NET_1_1)
#else
using System.Collections.Generic;
#endif
using NUnit.Framework;

using ResourceEditor.ResourceManager.Facade;

using Spring2.Core.Types;

using Spring2.Core.ResourceManager.TestUtility;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Test {


    /// <summary>
    /// Summary description for ResourceEditorTest.
    /// </summary>
    [TestFixture()]
    public class ResourceEditorTest {

	TestUtil testUtil = new TestUtil();
	Spring2.Core.ResourceManager.Facade.ResourceEditor editor = new Spring2.Core.ResourceManager.Facade.ResourceEditor(new LocaleFactory(), new LanguageFactory());

	[SetUp()]
	public void RemoveTestData() {
	    testUtil.RemoveTestData();
	}

	[Test()]
	public void GetAllContexts() {
	    testUtil.CreateTestResource();
	    StringTypeList list = editor.GetContexts();
	    Assert.IsTrue(list.Count > 0, "Should have gotten at least 1 context");
	}

	[Test()]
	public void GetFeildsbyContext() {
	    IResource resource = testUtil.CreateTestResource();
	    StringTypeList list = editor.GetFields(resource.Context);
	    Assert.IsTrue(list.Count > 0, "Should have gotten at least 1 context");
	}

	[Test()]
	public void GetIdentityByContextAndField() {
	    IResource resource = testUtil.CreateTestResource();
	    IResource resource2 = testUtil.CreateTestResource();
	    IdTypeList list = editor.GetIdentities(resource.Context, resource.Field);
	    Assert.IsTrue(list.Count > 1, "Should have found at least 2 Identities");
	}

	[Test()]
	public void GetResourcesByContext() {
	    IResource resource = testUtil.CreateTestResource();
	    ResourceList list = editor.GetResources(resource.Context);
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Resource");
	}

	[Test()]
	public void GetResourcesByContextAndField() {
	    IResource resource = testUtil.CreateTestResource();
	    ResourceList list = editor.GetResources(resource.Context, resource.Field);
	    Assert.IsTrue(list.Count > 0, "Should have found at least 1 Resource");
	}

	[Test()]
	public void GetAllPossibleLocalizedResourcesByContext() {
	    IResource resource = testUtil.CreateTestResource();
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();

#if (NET_1_1)
    	    ResourceDictionary list = editor.GetPossibleLocalizedResources(resource.Context, localizedResource.Locale, localizedResource.Language);
#else    	
	    Dictionary<IResource, ILocalizedResource> list = editor.GetPossibleLocalizedResources(resource.Context, localizedResource.Locale, localizedResource.Language);
#endif

	    //check that expected resources are in list
	    foreach(IResource resourceFromKey in list.Keys) {
		if(!resourceFromKey.ResourceId.Equals(resource.ResourceId) && !resourceFromKey.ResourceId.Equals(localizedResource.ResourceId)) {
		    Assert.Fail("Did not have expected resource in list");
		}
		if(list[resourceFromKey] != null && resourceFromKey.ResourceId.Equals(resource.ResourceId)) {
		    Assert.Fail("Resouce has localized resource associated that was not expected");
		}
		if(list[resourceFromKey] == null && resourceFromKey.ResourceId.Equals(localizedResource.ResourceId)) {
		    Assert.Fail("Did not have expected localized resource in list");
		}
	    }
	}

	[Test()]
	public void GetAllPossibleLocalizedResourcesByContextAndField() {
	    IResource resource = testUtil.CreateTestResource();
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();

#if (NET_1_1)
	    ResourceDictionary list = editor.GetPossibleLocalizedResources(resource.Context, resource.Field, localizedResource.Locale, localizedResource.Language);
#else    	
	    Dictionary<IResource, ILocalizedResource> list = editor.GetPossibleLocalizedResources(resource.Context, resource.Field, localizedResource.Locale, localizedResource.Language);
#endif

	    //check that expected resources are in list
	    foreach(IResource resourceFromKey in list.Keys) {
		if(!resourceFromKey.ResourceId.Equals(resource.ResourceId) && !resourceFromKey.ResourceId.Equals(localizedResource.ResourceId)) {
		    Assert.Fail("Did not have expected resource in list");
		}
		if(list[resourceFromKey] != null && resourceFromKey.ResourceId.Equals(resource.ResourceId)) {
		    Assert.Fail("Resouce has localized resource associated that was not expected");
		}
		if(list[resourceFromKey] == null && resourceFromKey.ResourceId.Equals(localizedResource.ResourceId)) {
		    Assert.Fail("Did not have expected localized resource in list");
		}
	    }
	}

	[Test()]
	public void GetLocalizedResourceByContextFieldAndIdentity() {
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
#if (NET_1_1)
	    ResourceDictionary list = editor.GetPossibleLocalizedResources(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
#else    	
	    Dictionary<IResource, ILocalizedResource> list = editor.GetPossibleLocalizedResources(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
#endif
    
	    Assert.IsTrue(list.Count == 1, "Should have found the one localized resource");
	}

	[Test()]
	public void CreateLocalizedResource() {
	    IResource resource = testUtil.CreateTestResource();
	    try {
		editor.CreateLocalizedResource(resource.ResourceId, LocaleEnum.UNITED_STATES, LanguageEnum.ENGLISH, StringType.Parse("Created from ResourceEditorTest"));
	    } catch {
		Assert.Fail("Should have been able to create localized resource");
	    }
	}

	[Test()]
	public void UpdateLocalizedResource() {
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    try {
		editor.UpdateLocalizedResource(localizedResource, StringType.Parse("Updated from ResourceEditorTest"));
	    } catch (Exception ex) {
		Assert.Fail("Should have been able to update localized resource.  Exception Message is = " + ex.Message);
	    }
	}

	[Test()]
	public void UpdateLocalizedResourceUsingOnlyId() {
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    try {
		editor.UpdateLocalizedResource(localizedResource.LocalizedResourceId, StringType.Parse("Updated from ResourceEditorTest"));
	    } catch(Exception ex) {
		Assert.Fail("Should have been able to update localized resource.  Exception Message is = " + ex.Message);
	    }
	}

	[Test()]
	public void CopyResourceAndLocalization() {
	    Random random = new Random(DateTime.Now.Millisecond);
	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    LocalizedResourceList newLocalizedResources = new LocalizedResourceList();
	    try {
		newLocalizedResources = editor.CopyResourceAndLocalization(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, IdType.Parse(random.Next().ToString()));
	    } catch (Exception ex) {
		Assert.Fail("Should have been able to copy resource and localization.  Error was : " + ex.Message);
	    }
	    Assert.IsTrue(newLocalizedResources.Count > 0, "Should have had at least one localized resource created.");
	}

	[TearDown()]
	public void Cleanup() {
	    testUtil.RemoveTestData();
	}

    }
}