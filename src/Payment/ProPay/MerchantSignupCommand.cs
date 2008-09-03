using System;
using System.Text;
using System.Xml;
using System.IO;

using Spring2.Core.Types;

namespace Spring2.Core.Payment.ProPay {

/*
 Example:
    <?xml version='1.0'?>
    <!DOCTYPE Request.dtd>
    <XMLRequest>
    <certStr>UpperCaseLivingTestCertString1</certStr>
    <class>partner</class>
    <XMLTrans>
    <transType>01</transType>
    <country>CAN</country>
    <sourceEmail>anyone@anywhere.com</sourceEmail>
    <firstName>John</firstName>
    <mInitial></mInitial>
    <lastName>Doe</lastName>
    <addr>200 West Main Street</addr>
    <aptNum></aptNum>
    <city>Anytown</city>
    <state>UT</state>
    <zip>84057</zip>
    <dayPhone>4464464464</dayPhone>
    <evenPhone>4464464464</evenPhone>
    <ssn>000000000</ssn>
    <dob>12-15-1961</dob>
    <ccNum>4747474747474747</ccNum>
    <expDate>0309</expDate>
    <tier>Premium</tier>
    </XMLTrans>
    </XMLRequest>
*/
    /// <summary>
    /// Summary description for MerchantSignupCommand.
    /// </summary>
    public class MerchantSignupCommand : ProPayCommand {

	StringType country = StringType.DEFAULT; // optional
	StringType email = StringType.DEFAULT;
	StringType givenName = StringType.DEFAULT;
	StringType middleInitial = StringType.DEFAULT;
	StringType surname = StringType.DEFAULT;
	StringType address = StringType.DEFAULT;
	StringType apartmentNumber = StringType.DEFAULT;
	StringType city = StringType.DEFAULT;
	StringType state = StringType.DEFAULT;
	StringType postalCode = StringType.DEFAULT;
	PhoneNumberType dayPhone = PhoneNumberType.DEFAULT;
	PhoneNumberType eveningPhone = PhoneNumberType.DEFAULT;
	StringType socialSecurityNumber = StringType.DEFAULT; // optional in Canada and for CO accounts
	DateTimeType birthday = DateTimeType.DEFAULT; // MM-dd-yyyy
	StringType creditCardNumber = StringType.DEFAULT; // optional ... for testing, use 4747474747474747
	StringType creditCardExpiration = StringType.DEFAULT; // optional ... for testing, use 0309

	public MerchantSignupCommand(StringType country, StringType email, StringType firstName, StringType mi, StringType lastName,
				     StringType address, StringType apartmentNumber, StringType city, StringType state, StringType postalCode,
				     PhoneNumberType dayPhone, PhoneNumberType eveningPhone, StringType ssn, DateTimeType birthday,
				     StringType creditCardNumber, StringType creditCardExpiration) {
	    this.country = country;
	    this.email = email;
	    this.givenName = firstName;
	    this.middleInitial = mi;
	    this.surname = lastName;
	    this.address = address;
	    this.apartmentNumber = apartmentNumber;
	    this.city = city;
	    this.state = state;
	    this.postalCode = postalCode;
	    this.dayPhone = dayPhone;
	    this.eveningPhone = eveningPhone;
	    this.socialSecurityNumber = ssn;
	    this.birthday = birthday;
	    this.creditCardNumber = creditCardNumber;
	    this.creditCardExpiration = creditCardExpiration;

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

	    xmltextWriter.WriteElementString("certStr",config.SignupCertString);

	    xmltextWriter.WriteElementString("class", config.BusinessClass); //always "partner" (legacy from ProPay)

	    xmltextWriter.WriteStartElement("XMLTrans");
	    xmltextWriter.WriteElementString("transType", "01");
	    xmltextWriter.WriteElementString("country", country.IsValid ? country.ToString() : ""); // optional
	    xmltextWriter.WriteElementString("sourceEmail", email.IsValid ? email.ToString() : "");
	    xmltextWriter.WriteElementString("firstName", givenName.IsValid ? givenName.ToString() : "");
	    xmltextWriter.WriteElementString("mInitial", middleInitial.IsValid ? middleInitial.ToString() : "");
	    xmltextWriter.WriteElementString("lastName", surname.IsValid ? surname.ToString() : "");
	    xmltextWriter.WriteElementString("addr", address.IsValid ? address.ToString() : "");
	    xmltextWriter.WriteElementString("aptNum", apartmentNumber.IsValid ? apartmentNumber.ToString() : "");
	    xmltextWriter.WriteElementString("city", city.IsValid ? city.ToString() : "");
	    xmltextWriter.WriteElementString("state", state.IsValid ? state.ToString() : "");
	    xmltextWriter.WriteElementString("zip", postalCode.IsValid ? postalCode.ToString() : "");
	    xmltextWriter.WriteElementString("dayPhone", dayPhone.IsValid ? dayPhone.DBValue.ToString() : "");
	    xmltextWriter.WriteElementString("evenPhone", eveningPhone.IsValid ? eveningPhone.DBValue.ToString() : "");
	    xmltextWriter.WriteElementString("ssn", socialSecurityNumber.IsValid ? socialSecurityNumber.ToString() : ""); // optional in Canada and for CO accounts
	    xmltextWriter.WriteElementString("dob", birthday.IsValid ? birthday.ToString("MM-dd-yyyy") : ""); // MM-dd-yyyy
	    xmltextWriter.WriteElementString("ccNum", creditCardNumber.IsValid ? creditCardNumber.ToString() : ""); // optional ... for testing, use 4747474747474747
	    xmltextWriter.WriteElementString("expDate", creditCardExpiration.IsValid ? creditCardExpiration.ToString() : ""); // optional ... for testing, use 0309
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
