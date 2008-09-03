using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    class AccountPingCommand : ProPayCommand {

	StringType accountEmail = StringType.DEFAULT;
	StringType ssn = StringType.DEFAULT;

	public AccountPingCommand(StringType proPayAccountEmail, StringType ssn) {
	    this.accountEmail = proPayAccountEmail;
	    this.ssn = ssn;
	}

	public override string CommandText {
	    get {
		String text = GetXml();
		return text;
	    }
	}

	private String GetXml() {
	    ProPayProviderConfiguration config = new ProPayProviderConfiguration();
	    StringWriter stringWriter = new VariableEncodingStringWriter(config.XMLEncoding);

	    XmlTextWriter xmltextWriter =
		new XmlTextWriter(stringWriter);
	    xmltextWriter.Formatting = Formatting.Indented;

	    // Start document
	    xmltextWriter.WriteStartDocument();

	    xmltextWriter.WriteStartElement("XMLRequest");

	    xmltextWriter.WriteElementString("certStr", config.CardProcessingCertString);

	    xmltextWriter.WriteElementString("class", config.BusinessClass); //always "partner" (legacy from ProPay)

	    xmltextWriter.WriteStartElement("XMLTrans");
	    xmltextWriter.WriteElementString("transType", "13");
	    xmltextWriter.WriteElementString("sourceEmail", accountEmail.IsValid ? accountEmail.ToString() : "");
	    xmltextWriter.WriteElementString("ssn", ssn.IsValid ? ssn.ToString() : "");
	    xmltextWriter.WriteElementString("accountType", "Y");
	    xmltextWriter.WriteEndElement(); // </XMLTrans>

	    // End document
	    xmltextWriter.WriteEndElement(); // </XMLRequest>

	    xmltextWriter.Flush();
	    xmltextWriter.Close();
	    stringWriter.Flush();
	    String resultXML = stringWriter.ToString();
	    stringWriter.Close();

	    return resultXML;
	}

    }
}