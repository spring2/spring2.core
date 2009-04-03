using System;
using Spring2.Core.Configuration;
using Spring2.Core.Types;

namespace Spring2.Core.Test {
    /// <summary>
    /// Summary description for BaseTest.
    /// </summary>
    public abstract class BaseTest {
	public BaseTest() {
	    if (ConfigurationProvider.Instance.Settings["DatabaseMode"].ToString() != "Developement") {
		throw new Exception("Tests can only be run on a development database.");
	    }
	}

    }
}
