using System;
using System.Collections;
using System.Configuration;
using NUnit.Framework;
using NUnit.Runner;

namespace Spring2.Core.Test {

    public class AllTests {

	public static ITest Suite {
	    get {
		PrintConfigSettings();

		TestSuite suite = new TestSuite(typeof(AllTests).FullName);

		// Facade tests
		suite.AddTestSuite(typeof(DataObjectTest));

		return suite;
	    }
	}


	private static void PrintConfigSettings() {
	    Console.Out.WriteLine(String.Empty.PadLeft(40,'-'));
	    Console.Out.WriteLine("Config App Settings");
	    Console.Out.WriteLine(String.Empty.PadLeft(40,'-'));
	    foreach(String key in ConfigurationSettings.AppSettings.Keys) {
		Console.Out.WriteLine(key + " = " + ConfigurationSettings.AppSettings[key]);
	    }
	    Console.Out.WriteLine(String.Empty.PadLeft(40,'-'));
	}

    }
}
