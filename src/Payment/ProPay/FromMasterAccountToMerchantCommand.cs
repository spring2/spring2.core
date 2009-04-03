using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    // NOTE:  it is the certStr that determines the source account for the funds being transferred

    class FromMasterAccountToMerchantCommand : ProPayCommand {

	StringType destEmail_or_AccountNumber = StringType.DEFAULT;
	CurrencyType transferAmount = CurrencyType.DEFAULT;
	StringType invoiceNumber = StringType.DEFAULT;

	public FromMasterAccountToMerchantCommand(StringType destEmail_or_AccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    this.transferAmount = amount;
	    this.destEmail_or_AccountNumber = destEmail_or_AccountNumber;
	    this.invoiceNumber = invoiceNumber;
	}

	public FromMasterAccountToMerchantCommand(StringType destEmail_or_AccountNumber, CurrencyType amount) {
	    this.transferAmount = amount;
	    this.destEmail_or_AccountNumber = destEmail_or_AccountNumber;
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

	    // it is the certStr that determines the source account for the funds
	    xmltextWriter.WriteElementString("certStr", config.CommissionProcessingCertString);

	    xmltextWriter.WriteElementString("class", config.BusinessClass); //always "partner" (legacy from ProPay)

	    xmltextWriter.WriteStartElement("XMLTrans");
	    xmltextWriter.WriteElementString("transType", "02");
	    if (transferAmount.IsValid) {
		Decimal transferAsDecimal = transferAmount.ToDecimal();
		Int32 transferAsPennies = ( Int32 )(transferAsDecimal * 100M);
		xmltextWriter.WriteElementString("amount", "" + transferAsPennies);
	    }
	    if (destEmail_or_AccountNumber.IsValid) {
		if (destEmail_or_AccountNumber.LastIndexOf("@") != -1) {
		    xmltextWriter.WriteElementString("recEmail", destEmail_or_AccountNumber.ToString());// may be used in place of accountNum
		} else {
		    xmltextWriter.WriteElementString("recAccntNum", destEmail_or_AccountNumber.ToString());// may be used in place of sourceEmail
		}
	    }
	    if (invoiceNumber.IsValid) {
		xmltextWriter.WriteElementString("invNum", invoiceNumber.ToString()); // optional
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