using System;
using System.Collections.Specialized;

namespace Spring2.Core.Configuration {
    
    /// <summary>
    /// Summary description for IConfigurationProvider.
    /// </summary>
    public interface IConfigurationProvider {

	NameValueCollection Settings {
	    get;
	}
    }
}
