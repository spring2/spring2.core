using System;
using System.Collections;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Spring2.Core.DAO;
using Spring2.Core.Configuration;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for MailMessage
    /// </summary>
    [TestFixture]
    public class ConfigurationTest {

        [TearDown]
        public void TearDown() {
            TestUtilities.DeleteObjects();
        }

        [Test]
        public void AppSetting() {
            ConfigurationProvider.SetProvider(new AppSettingsProvider());
            Assert.AreEqual("From File", ConfigurationProvider.Instance.Settings["ConfigTest"]);
        }

        [Test]
        public void SQL() {
            ConfigurationProvider.SetProvider(new SqlConfigurationProvider());
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key From DB"));
            Assert.AreEqual("SQL Key From DB", ConfigurationProvider.Instance.Settings["SQLKey"]);
        }

        [Test]
        public void Chained() {
            ConfigurationProvider.SetProvider(new ChainedConfigurationProvider());
            Assert.AreEqual("From File", ConfigurationProvider.Instance.Settings["ConfigTest"]);
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key From DB"));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("From DB"));
            Assert.AreEqual("SQL Key From DB", ConfigurationProvider.Instance.Settings["SQLKey"]);
            Assert.AreEqual("From File", ConfigurationProvider.Instance.Settings["ConfigTest"]);
        }

        [Test]
        public void SQLEffectiveDate() {
            ConfigurationProvider.SetProvider(new SqlConfigurationProvider());
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key old"), DateTimeType.Now.AddDays(-3));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key current"), DateTimeType.Now.AddDays(-1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key future"), DateTimeType.Now.AddDays(1));
            Assert.AreEqual("SQL Key current", ConfigurationProvider.Instance.Settings["SQLKey"]);
        }

	[Test]
	public void ShouldNotCacheValues() {
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key old"), DateTimeType.Now.AddDays(-3));
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key current"), DateTimeType.Now.AddDays(-1));
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key future"), DateTimeType.Now.AddDays(1));
	    SqlConfigurationProvider provider = new SqlConfigurationProvider(0);
	    ConfigurationProvider.SetProvider(provider);
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key new"), DateTimeType.Now.AddHours(-1));
	    Assert.AreEqual("SQL Key new", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual("SQL Key new", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual(2, provider.Refreshes);
	    Assert.AreEqual(DateTime.MinValue, provider.LastRefresh);
	    Thread.Sleep(2000);
	    Assert.AreEqual("SQL Key new", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual(3, provider.Refreshes);
	    Assert.AreEqual(DateTime.MinValue, provider.LastRefresh);
	}


	[Test]
	public void ShouldHaveCachedValue() {
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key old"), DateTimeType.Now.AddDays(-3));
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key current"), DateTimeType.Now.AddDays(-1));
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key future"), DateTimeType.Now.AddDays(1));
	    SqlConfigurationProvider provider = new SqlConfigurationProvider(2);
	    ConfigurationProvider.SetProvider(provider);
	    DateTime lastRefresh = provider.LastRefresh;
	    TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key new"), DateTimeType.Now.AddHours(-1));
	    Assert.AreEqual("SQL Key current", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual("SQL Key current", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual(lastRefresh, provider.LastRefresh);
	    Assert.AreEqual(1, provider.Refreshes);
	    Thread.Sleep(2000);
	    Assert.AreEqual("SQL Key new", ConfigurationProvider.Instance.Settings["SQLKey"]);
	    Assert.AreEqual(2, provider.Refreshes);
	    Assert.AreNotEqual(lastRefresh, provider.LastRefresh);
	}

        [Test]
        public void ChainedEffectiveDate() {
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key old"), DateTimeType.Now.AddDays(-3));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key current"), DateTimeType.Now.AddDays(-1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key future"), DateTimeType.Now.AddDays(1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key old"), DateTimeType.Now.AddDays(-3));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key current"), DateTimeType.Now.AddDays(-1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key future"), DateTimeType.Now.AddDays(1));

	    ConfigurationProvider.SetProvider(new ChainedConfigurationProvider());
            Assert.AreEqual("SQL Key current", ConfigurationProvider.Instance.Settings["SQLKey"]);
            Assert.AreEqual("From File", ConfigurationProvider.Instance.Settings["ConfigTest"]);
        }
    }
}
