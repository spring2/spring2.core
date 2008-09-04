using System;
using System.IO;
using System.Xml;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

    // This is identical to ChargeCardCommand, except that more values are optional and the trnasType is "05" rather than "04"
    class AuthorizeCommand : ProPayCommand {

	CurrencyType chargeAmount = CurrencyType.DEFAULT;
	StringType cardholderAddress = StringType.DEFAULT;	    // optional
	StringType cardholderApartmentNumber = StringType.DEFAULT;  // optional
	StringType cardholderPostalCode = StringType.DEFAULT;	    // optional
	StringType sourceEmail_or_AccountNumber = StringType.DEFAULT; // ProPay prefers we use AccountNumber, although either will be OK; email/accountnumber refer to the merchant, not the customer!
	StringType creditCardNumber = StringType.DEFAULT;
	StringType creditCardExpiration = StringType.DEFAULT;
	StringType creditCardCVV2 = StringType.DEFAULT;	    // optional
	StringType invNumber = StringType.DEFAULT;	    // optional
	StringType creditCardTrack1 = StringType.DEFAULT;   // in place of ccNum, expDate, CVV2
	StringType creditCardTrack2 = StringType.DEFAULT;   // optional

	public AuthorizeCommand(CurrencyType amount, StringType cardholderAddress, StringType cardholderApartmentNumber, StringType cardholderPostalCode,
				 StringType sourceEmail_or_accountNumber, StringType creditCardNumber, StringType creditCardExpirationDate, StringType creditCardCVV2,
				 StringType invNumber) {
	    init(amount, cardholderAddress, cardholderApartmentNumber, cardholderPostalCode,
		 sourceEmail_or_accountNumber, creditCardNumber, creditCardExpirationDate, creditCardCVV2,
		 invNumber, StringType.DEFAULT, StringType.DEFAULT);
	}

	public AuthorizeCommand(CurrencyType amount, StringType cardholderAddress, StringType cardholderApartmentNumber, StringType cardholderPostalCode,
				 StringType sourceEmail_or_accountNumber,
				 StringType invNumber, StringType creditCardTrack1, StringType creditCardTrack2) {
	    init(amount, cardholderAddress, cardholderApartmentNumber, cardholderPostalCode,
		 sourceEmail_or_accountNumber, StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT,
		 invNumber, creditCardTrack1, creditCardTrack2);
	}

	protected void init(CurrencyType amount, StringType cardholderAddress, StringType cardholderApartmentNumber, StringType cardholderPostalCode,
			    StringType sourceEmail_or_accountNumber, StringType creditCardNumber, StringType creditCardExpirationDate, StringType creditCardCVV2,
			    StringType invNumber, StringType creditCardTrack1, StringType creditCardTrack2) {
	    this.chargeAmount = amount;
	    this.cardholderAddress = cardholderAddress;
	    this.cardholderApartmentNumber = cardholderApartmentNumber;
	    this.cardholderPostalCode = cardholderPostalCode;
	    this.sourceEmail_or_AccountNumber = sourceEmail_or_accountNumber;
	    this.creditCardNumber = creditCardNumber;
	    this.creditCardExpiration = creditCardExpirationDate;
	    this.creditCardCVV2 = creditCardCVV2;
	    this.invNumber = invNumber;
	    this.creditCardTrack1 = creditCardTrack1;
	    this.creditCardTrack2 = creditCardTrack2;
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
	    xmltextWriter.WriteElementString("transType", "05");
	    if (chargeAmount.IsValid) {
		Decimal chargeAsDecimal = chargeAmount.ToDecimal();
		Int32 chargeAsPennies = ( Int32 )(chargeAsDecimal * 100M);
		xmltextWriter.WriteElementString("amount", "" + chargeAsPennies);
	    }
	    if (cardholderAddress.IsValid) {
		xmltextWriter.WriteElementString("addr", cardholderAddress.ToString());// optional
	    }
	    if (cardholderApartmentNumber.IsValid) {
		xmltextWriter.WriteElementString("aptNum", cardholderApartmentNumber.ToString());// optional
	    }
	    if (cardholderPostalCode.IsValid) {
		xmltextWriter.WriteElementString("zip", cardholderPostalCode.ToString());// optional
	    }
	    if (sourceEmail_or_AccountNumber.IsValid) {
		if (sourceEmail_or_AccountNumber.LastIndexOf("@") != -1) {
		    xmltextWriter.WriteElementString("sourceEmail", sourceEmail_or_AccountNumber.ToString());// may be used in place of accountNum
		} else {
		    xmltextWriter.WriteElementString("accountNum", sourceEmail_or_AccountNumber.ToString());// may be used in place of sourceEmail
		}
	    }
	    if (creditCardNumber.IsValid) {
		xmltextWriter.WriteElementString("ccNum", creditCardNumber.ToString());
	    }
	    if (creditCardExpiration.IsValid) {
		xmltextWriter.WriteElementString("expDate", creditCardExpiration.ToString());
	    }
	    if (creditCardCVV2.IsValid) {
		xmltextWriter.WriteElementString("CVV2", creditCardCVV2.ToString());	    // optional
	    }
	    if (invNumber.IsValid) {
		xmltextWriter.WriteElementString("invNum", invNumber.ToString());		    // optional
	    }
	    if (creditCardTrack1.IsValid) {
		xmltextWriter.WriteElementString("Track1", creditCardTrack1.ToString());// may be used in place of ccNum, expDate, and CVV2
	    }
	    if (creditCardTrack2.IsValid) {
		xmltextWriter.WriteElementString("Track2", creditCardTrack2.ToString());// optional
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