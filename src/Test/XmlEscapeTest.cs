using System;

using NUnit.Framework;
using Spring2.Core.Xml;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for escaping/encoding string for use in Xml
    /// </summary>
    [TestFixture]
    public class XmlEscapeTest {

	[Test]
	public void XmlEscape() {
	    Assert.AreEqual("&lt;", Escape.GetText("<"));
	    Assert.AreEqual("&gt;", Escape.GetText(">"));
	    Assert.AreEqual("&amp;", Escape.GetText("&"));
	    Assert.AreEqual("&quot;", Escape.GetText("\""));
	    Assert.AreEqual("My name is Inigo Montoya", Escape.GetText("My name is Inigo Montoya"));
	}
    }
}
