using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Payment;
using Spring2.Core.Payment.Test;

namespace Spring2.Core.Test {
	
    /// <summary>
    /// Summary description for PaymentManagerTest.
    /// </summary>
    [TestFixture]
    public class PaymentManagerTest {
    	
	[Test]
	public void Instance() {
	    IConfigurationProvider currentProvider = ConfigurationProvider.Instance;
	    try {
		PaymentManager.Reset();
		// change the provider to cause connection failure
		SimpleConfigurationProvider provider = new SimpleConfigurationProvider();
		provider.Settings.Add(currentProvider.Settings);
		provider.Settings["PaymentProvider.Class"] = "Spring2.Core.Payment.Test.TestPaymentProvider,Spring2.Core.Payment";
		ConfigurationProvider.SetProvider(provider);
		PaymentManager.Reset();
	    	
		Assert.AreEqual(typeof (TestPaymentProvider), PaymentManager.Instance.GetType());
	    } finally {
		ConfigurationProvider.SetProvider(currentProvider);
		PaymentManager.Reset();
	    }
    		
	}
    }
}

