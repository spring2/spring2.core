using System;
using System.IO;

using Spring2.Core.DAO;
using Spring2.Core.Configuration;
using Spring2.Core.Types;

using NUnit.Framework;

namespace Spring2.Core.Test {
    /// <summary>
    /// Utilities to help in the testing process.
    /// </summary>
    public class TestUtilities {

	private static readonly int COMPAREBUFFERSIZE = 512;
        private static ConfigurationSettingList configurationSettingsToDelete = new ConfigurationSettingList();
	
	/// <summary>
	/// All methods are static.
	/// </summary>
	private TestUtilities() {
	}

	/// <summary>
	/// Asserts two files are identical
	/// </summary>
	/// <param name="filename1">File to compare.</param>
	/// <param name="filename2">File to compare.</param>
	public static void CompareFiles(string filename1, string filename2) {
	    FileStream file1 = new FileStream(filename1, FileMode.Open, FileAccess.Read);
	    FileStream file2 = new FileStream(filename2, FileMode.Open, FileAccess.Read);

	    try {
		byte[] buffer1 = new byte[COMPAREBUFFERSIZE];
		byte[] buffer2 = new byte[COMPAREBUFFERSIZE];
		int bytes1 = file1.Read(buffer1, 0, COMPAREBUFFERSIZE);
		int bytes2 = file2.Read(buffer2, 0, COMPAREBUFFERSIZE);
		while(bytes1 > 0) {
		    Assert.IsTrue(bytes1 == bytes2, filename1 + " has different length than " + filename2 + ".");
		    for(int i=0;i<bytes1;i++) {
			Assert.IsTrue(buffer1[i] == buffer2[i], filename1 + " and " + filename2 + " differ.");
		    }
		    bytes1 = file1.Read(buffer1, 0, COMPAREBUFFERSIZE);
		    bytes2 = file2.Read(buffer2, 0, COMPAREBUFFERSIZE);
		}

		Assert.IsTrue(bytes1 == bytes2, filename1 + " has different length than " + filename2 + ".");
	    }
	    finally {
		file1.Close();
		file2.Close();
	    }
	}

        public static void DeleteObjects() {
            foreach (ConfigurationSetting c in configurationSettingsToDelete) {
                ConfigurationSettingDAO.DAO.Delete(c.ConfigurationSettingId);
            }
            configurationSettingsToDelete = new ConfigurationSettingList();
        }

        public static ConfigurationSetting CreateConfigurationSetting(StringType key, StringType value) {
            return CreateConfigurationSetting(key, value, DateTimeType.DEFAULT);
        }

        public static ConfigurationSetting CreateConfigurationSetting(StringType key, StringType value, DateTimeType effectiveDate) {
            ConfigurationSetting setting = ConfigurationSetting.NewInstance();
            setting.Key = key;
            setting.Value = value;
            setting.LastModifiedDate = DateTimeType.Now;
            setting.LastModifiedUserId = new IdType(1);
            if (effectiveDate.IsValid) {
                setting.EffectiveDate = effectiveDate;
            } else {
                setting.EffectiveDate = DateTimeType.Now.AddMilliseconds(-10);
            }
            setting.Store();

            configurationSettingsToDelete.Add(setting);

            return setting;
        }
    }
}
