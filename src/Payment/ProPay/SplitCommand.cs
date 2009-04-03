using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    class SplitCommand : ProPayCommand {

	StringType sourceAccountNumber = StringType.DEFAULT;
	StringType destAccountNumber = StringType.DEFAULT;
	CurrencyType transferAmount = CurrencyType.DEFAULT;
	StringType transactionNumber = StringType.DEFAULT;

	public SplitCommand(StringType sourceAccountNumber, StringType destAccountNumber, CurrencyType amount, StringType transactionNumber) {
	    this.transferAmount = amount;
	    this.sourceAccountNumber = sourceAccountNumber;
	    this.destAccountNumber = destAccountNumber;
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
	    xmltextWriter.WriteElementString("transType", "16");
	    if (transferAmount.IsValid) {
		Decimal transferAsDecimal = transferAmount.ToDecimal();
		Int32 transferAsPennies = ( Int32 )(transferAsDecimal * 100M);
		xmltextWriter.WriteElementString("Amount", "" + transferAsPennies);
	    }
	    if (sourceAccountNumber.IsValid) {
		xmltextWriter.WriteElementString("accountNum", sourceAccountNumber);
	    }
	    if (destAccountNumber.IsValid) {
		xmltextWriter.WriteElementString("recAccntNum", destAccountNumber.ToString());
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

	/* This implements Pull from a ProPay Account (Spendback), 3.11 from the documentation
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
	}*/

    }
}