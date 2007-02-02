using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Dss.Tax;
using Spring2.Dss.Tax.Test;

namespace Spring2.Dss.Test {
	
    /// <summary>
    /// Summary description for TaxManagerTest.
    /// </summary>
    [TestFixture]
    public class TaxManagerTest {
    	
	[Test]
	public void GetProvider() {
	    IConfigurationProvider currentProvider = ConfigurationProvider.Instance;
	    try {
		TaxManager.Reset();
		// change the provider to cause connection failure
		SimpleConfigurationProvider provider = new SimpleConfigurationProvider();
		provider.Settings.Add(currentProvider.Settings);
		provider.Settings["TaxProvider.Class"] = "Spring2.Dss.Tax.Test.TestTaxProvider,Spring2.Dss.Tax";
		ConfigurationProvider.SetProvider(provider);
		TaxManager.Reset();
	    	
		Assert.AreEqual(typeof (TestTaxProvider), TaxManager.GetProvider(Spring2.Core.Types.StringType.UNSET).GetType());
	    } finally {
		ConfigurationProvider.SetProvider(currentProvider);
		TaxManager.Reset();
	    }
    		
	}
    }
}
