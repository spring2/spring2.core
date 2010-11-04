using System;
using System.Collections;
using NUnit.Framework;
using Spring2.Core.Configuration;
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

	[SetUp()]
	public void RemoveTestData() {
	    testUtil.RemoveTestData();
	}

	[TearDown()]
	public void Cleanup() {
	    testUtil.RemoveTestData();
	}

	[Test()]
	public void TestGettingLocalizedText() {
	    ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());

	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    String localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	}

	[Test]
	public void ShouldNotCacheValues() {
	    ResourceLocalizer.ResetCache();

	    IConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(ConfigurationProvider.Instance.Settings);
	    provider.Settings[ResourceLocalizer.KEY_CACHETIMEOUT] = "0";
	    ConfigurationProvider.SetProvider(provider);

	    ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());

	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    String localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");

	    //update text
	    ResourceEditor editor = new ResourceEditor(new LocaleFactory(), new LanguageFactory());
	    editor.UpdateLocalizedResource(localizedResource.LocalizedResourceId, "foo");

	    localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals("foo"), "Should have gotten same string when localized.");

	    Assert.AreEqual(0, localizer.Refreshes);
	}


	[Test]
	public void ShouldHaveCachedValue() {
	    ResourceLocalizer.ResetCache();

	    IConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(ConfigurationProvider.Instance.Settings);
	    provider.Settings[ResourceLocalizer.KEY_CACHETIMEOUT] = "5";
	    ConfigurationProvider.SetProvider(provider);

	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();

	    ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());
	    String localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	    Assert.AreEqual(1, localizer.Refreshes);

	    //update text
	    ResourceEditor editor = new ResourceEditor(new LocaleFactory(), new LanguageFactory());
	    editor.UpdateLocalizedResource(localizedResource.LocalizedResourceId, "foo");

	    localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	    Assert.AreEqual(1, localizer.Refreshes);
	}

	[Test]
	public void ShouldRefreshAfterConfiguredTimeout() {
	    ResourceLocalizer.ResetCache();

	    IConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(ConfigurationProvider.Instance.Settings);
	    provider.Settings[ResourceLocalizer.KEY_CACHETIMEOUT] = "2";
	    ConfigurationProvider.SetProvider(provider);

	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();

	    ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());
	    String localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	    Assert.AreEqual(1, localizer.Refreshes);

	    //update text
	    ResourceEditor editor = new ResourceEditor(new LocaleFactory(), new LanguageFactory());
	    editor.UpdateLocalizedResource(localizedResource.LocalizedResourceId, "foo");

	    localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.IsTrue(localText.Equals(localizedResource.Content.Display()), "Should have gotten same string when localized.");
	    Assert.AreEqual(1, localizer.Refreshes);

	    System.Threading.Thread.Sleep(3000);
	    localText = String.Empty;
	    try {
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }
	    Assert.AreEqual("foo", localText);
	    Assert.AreEqual(2, localizer.Refreshes);

	}

	[Test()]
	public void TestGettingEnUSWhenSpecificLocalizationDoesNotExist() {
	    IConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(ConfigurationProvider.Instance.Settings);
	    provider.Settings[ResourceLocalizer.KEY_CACHETIMEOUT] = "0";
	    ConfigurationProvider.SetProvider(provider);

	    ResourceLocalizer localizer = new ResourceLocalizer(new LocaleFactory(), new LanguageFactory());

	    ILocalizedResource localizedResource = testUtil.CreateTestLocalizedResource();
	    String localText = String.Empty;
	    String usEnText = String.Empty;
	    try {
		usEnText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, localizedResource.Locale, localizedResource.Language);
		// if there is ever a legitimate reason for someone to be writing localization software to the Afrikaan-speaking population of Kazakhstan, I will eat my hat.  Or not.  But maybe.  And pigs may fly (without the aid of aircraft).
		ILocale testLocale = LocaleEnum.KAZAKHSTAN;
		ILanguage testLanguage = LanguageEnum.AFRIKAANS;
		localText = localizer.Localize(localizedResource.Resource.Context, localizedResource.Resource.Field, localizedResource.Resource.Identity, testLocale, testLanguage, localizedResource.Locale, localizedResource.Language);
	    } catch {
		Assert.Fail("Should have been able to localize");
	    }

	    Assert.AreEqual(usEnText, localText);
	}

    }
}