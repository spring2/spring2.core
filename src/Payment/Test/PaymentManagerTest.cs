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
	IConfigurationProvider currentProvider;

	[SetUp()]
	public void SetUp() {
	    currentProvider = ConfigurationProvider.Instance;
	}

	[TearDown()]
	public void TearDown() {
	    ConfigurationProvider.SetProvider(currentProvider);
	    PaymentManager.Reset();
	}

	[Test]
	public void Instance() {
	    SetSimpleConfigurationProvider("PaymentProvider.Class", "Spring2.Core.Payment.Test.TestPaymentProvider,Spring2.Core.Payment");
	    PaymentManager.Reset();

	    Assert.AreEqual(typeof(TestPaymentProvider), PaymentManager.Instance.GetType());
	}

	[Test()]
	public void ShouldBeAbleToGetNewInstanceOfPaymentProvider() {
	    Object provider = PaymentManager.GetNewInstance("Spring2.Core.Payment.Test.TestPaymentProvider,Spring2.Core.Payment");
	    Assert.IsTrue(provider is IPaymentProvider);
	    Assert.IsTrue(provider is TestPaymentProvider);

	    SetSimpleConfigurationProvider("PaymentProvider.Class", "Spring2.Core.Payment.Test.TestPaymentProvider,Spring2.Core.Payment");
	    PaymentManager.Reset();

	    Assert.AreNotSame(PaymentManager.Instance, provider);
	}

	[Test()]
	public void ShouldBeAbleToGetNewSplitInstanceOfPaymentProvider() {
	    Object provider = PaymentManager.GetNewInstance("Spring2.Core.Payment.Test.TestSplitPaymentProvider,Spring2.Core.Payment");
	    Assert.IsTrue(provider is ISplitPaymentProvider);
	    Assert.IsTrue(provider is TestSplitPaymentProvider);

	    SetSimpleConfigurationProvider("SplitPaymentProvider.Class", "Spring2.Core.Payment.Test.TestSplitPaymentProvider,Spring2.Core.Payment");
	    PaymentManager.Reset();

	    Assert.AreNotSame(PaymentManager.SplitInstance, provider);
	}

	private void SetSimpleConfigurationProvider(string paymentProviderKey, string paymentProviderValue) {
	    SimpleConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(currentProvider.Settings);
	    provider.Settings[paymentProviderKey] = paymentProviderValue;
	    ConfigurationProvider.SetProvider(provider);
	}
    }
}

