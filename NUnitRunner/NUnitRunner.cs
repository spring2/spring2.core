using System;
using System.Collections;

using Spring2.Core.Test;
namespace NUnitRunner
{
    /// <summary>
    /// Summary description for NUnitRunner.
    /// </summary>
    public class NUnitRunner
    {
	public static void Main(String[] args) 
	{ 
	    // NUnit.TextUI.TestRunner.Run(DataObjectTest.Suite);
	    NUnit.GUI.Top.Main(args);
	} 

    }
}
