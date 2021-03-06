﻿using System;
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
	[Ignore]
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
	[Ignore]
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

	[Test]
	public void ShouldBeAbleToGetRateByDateAndCurrencyCode() {
	    try {
		CurrencyExchangeData data = new CurrencyExchangeData() {
		    CurrencyCode = "TEST",
		    EffectiveDate = DateTimeType.Now.AddDays(-200),
		    Rate = 200
		};
		CurrencyExchange one = TestUtilities.CreateCurrencyExchange(data);
		data.EffectiveDate = DateTimeType.Now.AddDays(-100);
		data.Rate = 100;
		CurrencyExchange two = TestUtilities.CreateCurrencyExchange(data);
		data.EffectiveDate = DateTimeType.Now.AddDays(1);
		data.Rate = 10;
		CurrencyExchange three = TestUtilities.CreateCurrencyExchange(data);
		ICurrencyExchange result = CurrencyExchange.GetRateByCodeAndDate("TEST", DateTimeType.Now.AddDays(-80));
		Assert.AreEqual(two.CurrencyExchangeId, result.CurrencyExchangeId);
		result = CurrencyExchange.GetRateByCodeAndDate("TEST", DateTimeType.Now.AddDays(-180));
		Assert.AreEqual(one.CurrencyExchangeId, result.CurrencyExchangeId);
	    } finally {
		TestUtilities.DeleteObjects();
	    }
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
