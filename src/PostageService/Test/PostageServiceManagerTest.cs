using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Spring2.Core.Configuration;

namespace Spring2.Core.PostageService.Test {
    [TestFixture]
    public class PostageServiceManagerTest {
	IConfigurationProvider currentProvider;
	SimpleConfigurationProvider provider;

	[SetUp]
	public void SetUp() {
	    currentProvider = ConfigurationProvider.Instance;
	    provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(currentProvider.Settings);
	    ConfigurationProvider.SetProvider(provider);
	}

	[TearDown]
	public void TearDown() {
	    ConfigurationProvider.SetProvider(currentProvider);
	    PostageServiceManager.Reset();
	}

	[Test]
	public void GetProvider() {
	    // change the provider to cause connection failure
	    provider.Settings["PostageServiceProvider.Class"] = "Spring2.Core.PostageService.Test.TestPostageServiceProvider,Spring2.Core.PostageService";
	    PostageServiceManager.Reset();

	    Assert.IsTrue(PostageServiceManager.Instance is TestPostageServiceProvider);
	}
    }
}
