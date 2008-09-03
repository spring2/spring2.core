using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    // This executes an actual payment if a transaction has been authorized previously
    class SettlementCommand : ProPayCommand {

	StringType sourceEmail_or_AccountNumber = StringType.DEFAULT; // ProPay prefers we use AccountNumber, although either will be OK; email/accountnumber refer to the merchant, not the customer!
	StringType transactionNumber = StringType.DEFAULT; // this value was obtained from the host's response when an AuthorizationCommand is executed

	public SettlementCommand(StringType sourceEmail_or_accountNumber, StringType transactionNumber) {
	    this.sourceEmail_or_AccountNumber = sourceEmail_or_accountNumber;
	    this.transactionNumber = transactionNumber;
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
	    xmltextWriter.WriteElementString("transType", "06");
	    if (sourceEmail_or_AccountNumber.LastIndexOf("@") != -1) {
		xmltextWriter.WriteElementString("sourceEmail", sourceEmail_or_AccountNumber.IsValid ? sourceEmail_or_AccountNumber.ToString() : "");// may be used in place of accountNum
	    } else {
		xmltextWriter.WriteElementString("accountNum", sourceEmail_or_AccountNumber.IsValid ? sourceEmail_or_AccountNumber.ToString() : "");// may be used in place of sourceEmail
	    }
	    xmltextWriter.WriteElementString("transNum", transactionNumber.IsValid ? transactionNumber.ToString() : "");
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