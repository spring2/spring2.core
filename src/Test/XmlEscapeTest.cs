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
	    Assertion.AssertEquals("&lt;", Escape.GetText("<"));
	    Assertion.AssertEquals("&gt;", Escape.GetText(">"));
	    Assertion.AssertEquals("&amp;", Escape.GetText("&"));
	    Assertion.AssertEquals("&quot;", Escape.GetText("\""));
	    Assertion.AssertEquals("My name is Inigo Montoya", Escape.GetText("My name is Inigo Montoya"));
	}
    }
}
