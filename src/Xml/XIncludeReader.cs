using System;
using System.IO;
using System.Xml;

using Spring2.Core.IO;

namespace Spring2.Core.Xml {
    /// <summary>
    /// XmlTextReader that supports syntaxt for XInclude.
    /// Must include a namespace in the root element, like:
    /// xmlns:xinc="http://www.w3.org/1999/XML/xinclude" 
    /// 
    /// use like:
    /// &lt;sqltypes-ref xinc:href="sqltypes.xml.inc" /&gt;
    /// 
    /// Element name can be anything, namespace on href must match namespace included in root element.
    /// Included file can have any name and extention.
    /// </summary>
    public class XIncludeReader : XmlTextReader {
	// nested reader for processing XInclude elements
	XIncludeReader m_NestedReader;
	String root = String.Empty;
	String filename = String.Empty;
  
	public XIncludeReader(String sURI) : base(sURI) {
	    // TODO: hack - what if the uri is not a file uri?
	    FileInfo f = new FileInfo(sURI);
	    this.root = f.DirectoryName;
	    this.filename = sURI;
	    m_NestedReader=null;
	}

	public XIncludeReader(Stream stream) : base(stream) {
	    m_NestedReader=null;
	}

	/// <summary>
	/// custom implementation of Read: if a nested reader exists, delegate
	/// otherwise, Read the next node and check for xinc:href
	/// if one exists create another nested reader and continue
	/// </summary>
	/// <returns></returns>
	public override bool Read() {
	    bool bMore;

	    // if nested reader exists, delegate
	    if (m_NestedReader != null) {
		    bMore = m_NestedReader.Read();
		    // if done with nested reader, free resources & reset state
		    if (!bMore) {
			m_NestedReader.Close();
			m_NestedReader = null;
			// re-use this read implementation, just in case there are multiple include statements after each other.
			return this.Read();
		    } else {
			return true;
		    }
	    } else {
		try {
		    // read the next node and check for xinc:href
		    bMore = base.Read();
		    String href = base.GetAttribute("href", "http://www.w3.org/1999/XML/xinclude");
		    // if found, create a new nested reader and move to the first node
		    if (href != null) {
			// TODO: use ResourceLocator to get stream and use that constructor
			// use the root if the include file can be found based on the root
			if (File.Exists(Path.Combine(root, href))) {
			    href = Path.Combine(root, href);
			    m_NestedReader = new XIncludeReader(href);
			} else {
			    ResourceLocator rl = new ResourceLocator(href);
			    m_NestedReader = new XIncludeReader(rl.OpenRead());
			}
			m_NestedReader.Read();
			// move past XmlDeclaration if present
			if (m_NestedReader.NodeType == XmlNodeType.XmlDeclaration)
			    m_NestedReader.Read();
			return true;
		    } else {
			return bMore;
		    }
		} catch (XmlException ex) {
		    // try to report back the filename if possible
		    throw new XmlException("Error while reading from " + this.filename + " :: " + ex.Message, ex);
		}
	    }
	}

	// override all of XmlTextReader's methods to delegate
	// to nested reader if one exists
	public override string Name {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.Name;
		} else {
		    return base.Name;
		}
	    }
	}

	public override XmlNodeType NodeType {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.NodeType;
		} else {
		    return base.NodeType;
		}
	    }
	}
  
	public override string NamespaceURI {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.NamespaceURI;
		} else {
		    return base.NamespaceURI;
		}
	    }
	}

	public override string LocalName {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.LocalName;
		} else {
		    return base.LocalName;
		}
	    }
	}

	public override string Prefix {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.Prefix;
		} else {
		    return base.Prefix;
		}
	    }
	}
  
	public override string Value {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.Value;
		} else {
		    return base.Value;
		}
	    }
	}

	public override int AttributeCount {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.AttributeCount;
		} else {
		    return base.AttributeCount;
		}
	    }
	}

	public override bool MoveToNextAttribute() {
	    if (m_NestedReader != null) {
		return m_NestedReader.MoveToNextAttribute();
	    } else {
		return base.MoveToNextAttribute();
	    }
	}

	public override bool MoveToElement() {
	    if (m_NestedReader != null) {
		return m_NestedReader.MoveToElement();
	    } else {
		return base.MoveToElement();
	    }
	}

	public override bool HasAttributes {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.HasAttributes;
		} else {
		    return base.HasAttributes;
		}
	    }
	}

	public override String ReadInnerXml() {
	    if (m_NestedReader != null) {
		return m_NestedReader.ReadInnerXml();
	    } else {
		return base.ReadInnerXml();
	    }
	}

	public override String ReadString() {
	    if (m_NestedReader != null) {
		return m_NestedReader.ReadString();
	    } else {
		return base.ReadString();
	    }
	}
  
	public override bool IsEmptyElement {
	    get {
		if (m_NestedReader != null) {
		    return m_NestedReader.IsEmptyElement;
		} else {
		    return base.IsEmptyElement;
		}
	    }
	}

	// TODO: make sure that all methods that actually read get overridden.  Methods needed by this implementation have been done.


	public static void SerializeNode(XmlReader reader, XmlWriter w) {
	    switch (reader.NodeType) {
		case XmlNodeType.Element:
		    w.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
		    if (reader.HasAttributes) {
			while (reader.MoveToNextAttribute()) {
			    w.WriteStartAttribute(reader.Prefix, reader.LocalName, reader.NamespaceURI);
			    w.WriteString(reader.Value);
			    w.WriteEndAttribute();
			}
			reader.MoveToElement();
		    }

		    if (reader.IsEmptyElement) {
			w.WriteEndElement();
		    }

		    break;
		case XmlNodeType.Text:
		    w.WriteString(reader.Value);
		    break;
		case XmlNodeType.CDATA:
		    w.WriteCData(reader.Value);
		    break;
		case XmlNodeType.ProcessingInstruction:
		    w.WriteProcessingInstruction(reader.Name, reader.Value);
		    break;
		case XmlNodeType.Comment:
		    w.WriteComment(reader.Value);
		    break;
		case XmlNodeType.Whitespace:
		    //w.WriteWhitespace(reader.Value);
		    break;
		case XmlNodeType.SignificantWhitespace:
		    //w.WriteWhitespace(reader.Value);
		    break;
		case XmlNodeType.EndElement:
		    w.WriteEndElement();
		    break;
	    }
	}

	public static Stream GetStream(String filename) {
	    MemoryStream ms = new MemoryStream();

	    XIncludeReader r = new XIncludeReader(filename);
	    r.WhitespaceHandling = WhitespaceHandling.None;
	    XmlTextWriter tw = new XmlTextWriter(ms, System.Text.Encoding.Default);
	    tw.Formatting = Formatting.Indented;
	    tw.WriteStartDocument();
	    while (r.Read()) {
		XIncludeReader.SerializeNode(r, tw);
	    }
	    tw.WriteEndDocument();
	    tw.Flush();
	    ms.Position = 0;

	    return ms;
	}

	public static XmlTextReader GetXmlTextReader(String filename) {
	    XIncludeReader r = new XIncludeReader(filename);
	    return GetXmlTextReader(r);    
	}

	public static XmlTextReader GetXmlTextReader(Stream stream) {
	    XIncludeReader r = new XIncludeReader(stream);
	    return GetXmlTextReader(r);    
	}

	private static XmlTextReader GetXmlTextReader(XIncludeReader r) {
	    MemoryStream ms = new MemoryStream();
	    r.WhitespaceHandling = WhitespaceHandling.None;
	    XmlTextWriter tw = new XmlTextWriter(ms, System.Text.Encoding.Default);
	    tw.Formatting = Formatting.Indented;
	    tw.WriteStartDocument();
	    while (r.Read()) {
		XIncludeReader.SerializeNode(r, tw);
	    }
	    tw.WriteEndDocument();
	    tw.Flush();
	    ms.Position = 0;

	    return new XmlTextReader(ms);
	}

    }
 
}  

