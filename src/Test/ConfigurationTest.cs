using System;
using System.Collections;
using System.IO;
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
        public void ChainedEffectiveDate() {
            ConfigurationProvider.SetProvider(new ChainedConfigurationProvider());
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key old"), DateTimeType.Now.AddDays(-3));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key current"), DateTimeType.Now.AddDays(-1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("SQLKey"), StringType.Parse("SQL Key future"), DateTimeType.Now.AddDays(1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key old"), DateTimeType.Now.AddDays(-3));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key current"), DateTimeType.Now.AddDays(-1));
            TestUtilities.CreateConfigurationSetting(StringType.Parse("ConfigTest"), StringType.Parse("Config Key future"), DateTimeType.Now.AddDays(1));
            Assert.AreEqual("SQL Key current", ConfigurationProvider.Instance.Settings["SQLKey"]);
            Assert.AreEqual("From File", ConfigurationProvider.Instance.Settings["ConfigTest"]);
        }
    }
}
