using System;
using System.IO;
using System.Xml;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Xml;

namespace Spring2.Core.Test {
    /// <summary>
    /// Test for Xml namespace
    /// </summary>
    [TestFixture]
    public class XmlTest {

	/// <summary>
	/// Test to make sure that xml errors are reported with line information
	/// </summary>
	[Test]
	public void OpenXml() {
	    String filename=@"C:\Data\work\seamlessweb\manhattan\src\DataTierGenerator.config.xml";
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		throw new FileNotFoundException("Could not load config file", filename);
	    }

	    XmlDocument doc = new XmlDocument();
	    Stream filestream = XIncludeReader.GetStream(filename);
	    doc.Load(filestream);
	    filestream.Close();	
	}
    }
}
