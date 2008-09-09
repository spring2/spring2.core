using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    // This executes an actual payment if a transaction has been authorized previously
    class TransactionStatusCommand : ProPayCommand {

	StringType accountNumber = StringType.DEFAULT;
	StringType transactionNumber = StringType.DEFAULT;

	public TransactionStatusCommand(StringType accountNumber, StringType transactionNumber) {
	    this.accountNumber = accountNumber;
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
	    xmltextWriter.WriteElementString("transType", "28");
	    if (accountNumber.IsValid) {
		xmltextWriter.WriteElementString("accountNum", accountNumber);
	    }
	    if (transactionNumber.IsValid) {
		xmltextWriter.WriteElementString("transNum", transactionNumber);
	    }
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