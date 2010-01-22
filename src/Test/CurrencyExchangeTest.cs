using System;
using NUnit.Framework;
using Spring2.Core;
using Spring2.Core.Types;
using Spring2.Core.Configuration;
using Spring2.Core.Currency;
using Spring2.Core.Currency.BusinessLogic;
using Spring2.Core.Currency.DataObject;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for Currency Conversion Test
    /// </summary>
    [TestFixture]
    public class CurrencyConversionTest {
	[Test]
	public void ShouldGetValidRate() {
	    double rate = 0;
	    try {
		Spring2.Core.Configuration.SimpleConfigurationProvider provider = GetTestConfigurationProvider();
		provider.Settings["CurrencyProvider"] = "Spring2.Core.Currency.WebServiceXProvider";

		rate = CurrencyProvider.Instance.GetConversionRate("USD", "CAD");
	    } catch (Exception ex) {
		Assert.Fail(ex.Message);
	    }

	    Assert.Greater(rate, 0);
	}

	[Test]
	public void ShouldGetValidRate2() {
	    ICurrencyExchange line = null;
	    try {
		Spring2.Core.Configuration.SimpleConfigurationProvider provider = GetTestConfigurationProvider();
		provider.Settings["CurrencyProvider"] = "Spring2.Core.Currency.WebServiceXProvider";

		line = CurrencyExchange.CheckForNewRate(StringType.Parse("CAD"));

		line = CurrencyExchange.GetCurrentRate(StringType.Parse("CAD"));

	    } catch (Exception ex) {
		Assert.Fail(ex.Message);
	    }

	    Assert.Greater(line.Rate, DecimalType.ZERO);
	}

	internal static SimpleConfigurationProvider GetTestConfigurationProvider() {
	    IConfigurationProvider originalProvider = ConfigurationProvider.Instance;
	    IConfigurationProvider currentProvider = ConfigurationProvider.Instance;
	    SimpleConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(currentProvider.Settings);
	    ConfigurationProvider.SetProvider(provider);
	    return provider;
	}

    }
}
