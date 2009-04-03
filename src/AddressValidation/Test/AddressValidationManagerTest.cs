using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.AddressValidation;
using Spring2.Core.AddressValidation.Test;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for AddressValidationManagerTest.
    /// </summary>
    [TestFixture]
    public class AddressValidationManagerTest {
    	
	[Test]
	public void GetProvider() {
	    IConfigurationProvider currentProvider = ConfigurationProvider.Instance;
	    try {
		AddressValidationManager.Reset();
		// change the provider to cause connection failure
		SimpleConfigurationProvider provider = new SimpleConfigurationProvider();
		provider.Settings.Add(currentProvider.Settings);
		provider.Settings["AddressValidationProvider.Class"] = "Spring2.Core.AddressValidation.Test.TestAddressValidationProvider,Spring2.Core.AddressValidation";
		ConfigurationProvider.SetProvider(provider);
		AddressValidationManager.Reset();
	    	
		Assert.AreEqual(typeof (TestAddressValidationProvider), AddressValidationManager.Instance.GetType());
	    } finally {
		ConfigurationProvider.SetProvider(currentProvider);
		AddressValidationManager.Reset();
	    }
    		
	}
    }
}
