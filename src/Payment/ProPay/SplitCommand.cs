using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    class SplitCommand : ProPayCommand {

	CurrencyType chargeAmount = CurrencyType.DEFAULT;
	StringType destAccountNumber = StringType.DEFAULT; // ProPay prefers we use AccountNumber, although either will be OK; email/accountnumber refer to the merchant, not the customer!

	public SplitCommand(CurrencyType amount, StringType destAccountNumber) {
	    this.chargeAmount = amount;
	    this.destAccountNumber = destAccountNumber;
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
	    xmltextWriter.WriteElementString("transType", "11");
	    if (chargeAmount.IsValid) {
		Decimal chargeAsDecimal = chargeAmount.ToDecimal();
		Int32 chargeAsPennies = ( Int32 )(chargeAsDecimal * 100M);
		xmltextWriter.WriteElementString("amount", "" + chargeAsPennies);
	    }
	    xmltextWriter.WriteElementString("accountNum", config.CorporateAccountNumber);
	    if (destAccountNumber.IsValid) {
		xmltextWriter.WriteElementString("recAccntNum", destAccountNumber.ToString());
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

	/* This implements ProPay-toProPay, 3.2 in the documentation
	 * private String GetXml() {
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
	    xmltextWriter.WriteElementString("transType", "02");
	    if (chargeAmount.IsValid) {
		Decimal chargeAsDecimal = chargeAmount.ToDecimal();
		Int32 chargeAsPennies = ( Int32 )(chargeAsDecimal * 100M);
		xmltextWriter.WriteElementString("amount", "" + chargeAsPennies);
	    }
	    if (sourceEmail_or_AccountNumber.IsValid) {
		if (sourceEmail_or_AccountNumber.LastIndexOf("@") != -1) {
		    xmltextWriter.WriteElementString("recEmail", sourceEmail_or_AccountNumber.ToString());// may be used in place of accountNum
		} else {
		    xmltextWriter.WriteElementString("recAccntNum", sourceEmail_or_AccountNumber.ToString());// may be used in place of sourceEmail
		}
	    }
	    if (invNumber.IsValid) {
		xmltextWriter.WriteElementString("invNum", invNumber.ToString()); // optional
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
	}*/

    }
}