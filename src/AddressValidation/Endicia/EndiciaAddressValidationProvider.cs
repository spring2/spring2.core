using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.AddressValidation;
using Spring2.Core.Types;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Net;

namespace Spring2.Core.AddressValidation.Endicia {
    public class EndiciaAddressValidationProvider : IAddressValidationProvider {
	protected string dialAZipUrlBaseFormat = "http://www.dial-a-zip.com/XML-Dial-A-ZIP/DAZService.asmx/MethodZIPValidate?input={0}";
	protected const string COMMAND = "ZIP1";
	protected string user = null;
	protected string password = null;
	protected string serialNumber = null;

	protected string address1 = null;
	protected string address2 = null;
	protected string address3 = null;
	protected string city = null;
	protected string region = null;
	protected string postalCode = null;
	protected string country = null;

	private Dictionary<int, string> returnCodes = new Dictionary<int, string> {
	    { 10, "Invalid Dual Address - May indicate that more than one delivery address is detected" },
	    { 11, "Invalid City/State/ZIP Code - ZIP Code could not be found because neither a valid City, State, nor valid 5-digit ZIP Code was present" },
	    { 12, "Invalid State - The State code in the address is invalid. Note that only US State and U.S. Territories and possession abbreviations are valid" },
	    { 13, "Invalid City – The City in the address submitted is invalid. Remember, city names cannot begin with numbers" },
	    { 21, "Address Not Found – The address as submitted could not be found. Check for excessive abbreviations in the street address line or in the City name" },
	    { 22, "Multiple Responses – More than one ZIP+4 was found. Check for missing address elements, or run ZIPM request for a list of possible valid addresses" },
	    { 25, "Invalid Street - City, State and ZIP Code are valid, but street address is not a match" },
	    { 31, "Exact Match – No corrective action required" },
	    { 32, "Default Match – More information, such as an apartment or suite number, may give a more specific address" }
	};
	private int[] validReturnCodes = new int[] { 31, 32 };

	public AddressValidationResult Validate(StringType street, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    this.address1 = street;
	    this.city = city;
	    this.region = state;
	    this.postalCode = postalCode;
	    this.country = countryCode;

	    return Execute();
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    this.address1 = street1;
	    this.address2 = street2;
	    this.city = city;
	    this.region = state;
	    this.postalCode = postalCode;
	    this.country = countryCode;

	    return Execute();
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType street3, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    this.address1 = street1;
	    this.address2 = street2;
	    this.address3 = street3;
	    this.city = city;
	    this.region = state;
	    this.postalCode = postalCode;
	    this.country = countryCode;

	    return Execute();
	}

	public void SetCredentials(StringType user, StringType password, StringType serialNo) {
	    this.user = user;
	    this.password = password;
	    this.serialNumber = serialNo;
	}

	private AddressValidationResult Execute() {
	    if (string.IsNullOrEmpty(this.user) || string.IsNullOrEmpty(this.password) || string.IsNullOrEmpty(this.serialNumber)) {
		throw new NotSupportedException("The credentials supplied are incomplete. Please verify and retry.");
	    }
	    DialAZipRequest request = new DialAZipRequest();
	    request.USER = this.user;
	    request.COMMAND = COMMAND;
	    request.PASSWORD = this.password;
	    request.SERIALNO = this.serialNumber;

	    request.ADDRESS0 = "";
	    request.ADDRESS1 = this.address1;
	    request.ADDRESS2 = this.address2 ?? "";
	    request.ADDRESS3 = string.Format("{0}, {1} {2}", this.city, this.region, this.postalCode);

	    string xml = SerializeToString<DialAZipRequest>(request);
	    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format(dialAZipUrlBaseFormat, Encode(xml)));
	    webRequest.Method = "GET";

	    HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
	    string stringResponse = null;
	    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream())) {
		stringResponse = reader.ReadToEnd();
	    }

	    DialAZipResponse response = DeserializeStringToObject<DialAZipResponse>(stringResponse);
	    if (!string.IsNullOrEmpty(response.Status)) {
		throw new NotSupportedException(response.Status);
	    }

	    AddressValidationResult result = new AddressValidationResult();
	    result.ResponseXml = stringResponse;
	    if (validReturnCodes.Contains(response.ReturnCode)) {
		result.ResponseType = ResponseTypeEnum.VALID;
	    } else {
		result.ResponseType = ResponseTypeEnum.INVALID;
	    }
	    AddressList addressList = new AddressList();
	    addressList.Add(new AddressData {
		Street1 = response.AddrLine1,
		Street2 = response.AddrLine2,
		Street3 = response.AddrLine3,
		City = response.City,
		State = response.State,
		PostalCode = response.ZIP5
	    });
	    result.Addresses = addressList;
	    result.Status = returnCodes[response.ReturnCode];
	    return result;
	}

	private string SerializeToString<T>(T serializableObject) {
	    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
	    XmlSerializer serializer = new XmlSerializer(typeof(T));

	    XmlWriterSettings writerSettings = new XmlWriterSettings {
		Encoding = Encoding.UTF8,
		Indent = false,
		OmitXmlDeclaration = true
	    };

	    using (StringWriter stringWriter = new StringWriter()) {
		using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings)) {
		    serializer.Serialize(xmlWriter, serializableObject, namespaces);
		    return stringWriter.ToString();
		}
	    }
	}

	private T DeserializeStringToObject<T>(string s) {
	    XmlSerializer serializer = new XmlSerializer(typeof(T));
	    using (StringReader reader = new StringReader(s)) {
		return (T)serializer.Deserialize(reader);
	    }
	}

	private string Encode(string source) {
	    return System.Web.HttpUtility.UrlEncode(source);
	}
    }
}
