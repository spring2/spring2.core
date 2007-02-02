using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Dss.Payment;
using Spring2.Dss.Payment.Test;

namespace Spring2.Dss.Test {
	
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
		provider.Settings["PaymentProvider.Class"] = "Spring2.Dss.Payment.Test.TestPaymentProvider,Spring2.Dss.Payment";
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

