using System;
using System.Collections;

using Spring2.Core.Test;

namespace Spring2.Core.NUnitRunner {
    /// <summary>
    /// Summary description for NUnitRunner.
    /// </summary>
    public class NUnitRunner {
	public static void Main(String[] args) { 
	    NUnit.TextUI.TestRunner.Run(AllTests.Suite);
	    Console.Out.WriteLine();
	    Console.Out.WriteLine("press any key to close");
	    Console.Read();
	} 

    }
}
