using System;
using Spring2.Core.Configuration;

namespace Spring2.Core.Payment {

    /// <summary>
    /// Base configuration provider
    /// </summary>
    public abstract class BaseConfigurationProvider {

    	/// <summary>
    	/// Validate that configuration values exist and are not empty
    	/// </summary>
    	/// <param name="key"></param>
    	protected void CheckConfigurationValue(String key) {
	    if (ConfigurationProvider.Instance.Settings[key] == null || ConfigurationProvider.Instance.Settings[key].Length == 0) {
		throw new PaymentConfigurationException("Missing configuration value for " + key);
	    }
	}

    	/// <summary>
    	/// Returns a configuration value, or the defaultValue if it is not found or empty
    	/// </summary>
    	/// <param name="key"></param>
    	/// <param name="defaultValue"></param>
    	/// <returns></returns>
	protected String GetConfigurationValue(String key, String defaultValue) {
	    String value = ConfigurationProvider.Instance.Settings[key];
	    if (value != null && value.Length > 0) {
		return value;
	    } else {
		return defaultValue;
	    }
	}

    }
}
