using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Dss.AddressValidation.UPS {
    public class UPSAddressValidationProvider: IAddressValidationProvider {

	private string UPSAccessKey {
	    get { return ConfigurationProvider.Instance.Settings["UPS.AccessKey"]; }
	}

	private string UPSUserId {
	    get { return ConfigurationProvider.Instance.Settings["UPS.UserId"]; }
	}

	private string UPSPassword {
	    get { return ConfigurationProvider.Instance.Settings["UPS.Password"]; }
	}

	private string UPSServer {
	    get { return ConfigurationProvider.Instance.Settings["UPS.Server"]; }
	}

	public AddressValidationResult Validate(StringType street, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    return Validate(street, string.Empty, string.Empty, city, state, postalCode, countryCode);
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    return Validate(street1, street2, string.Empty, city, state, postalCode, countryCode);
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType street3, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    string response = string.Empty;
	    string request = AccessRequestXml + Environment.NewLine + GetRequestXml(street1, street2.IsValid ? street2.ToString() : string.Empty, street3.IsValid ? street3.ToString() : string.Empty, city, state, postalCode);

	    // Set encoding & get content Length
	    ASCIIEncoding encoding = new ASCIIEncoding();
	    byte[] data = encoding.GetBytes(request);

	    // Prepare post request
	    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(UPSServer);
	    webRequest.Method = "POST";
	    webRequest.ContentType = "application/x-www-form-urlencoded";
	    webRequest.ContentLength = data.Length;
	    Stream requestStream = webRequest.GetRequestStream();
	    // Send the data
	    requestStream.Write(data, 0, data.Length);
	    requestStream.Close();
	    // get the response
	    WebResponse webResponse = null;
	    try {
		webResponse = webRequest.GetResponse();
		using (StreamReader sr = new StreamReader(webResponse.GetResponseStream())) {
		    response = sr.ReadToEnd();
		    sr.Close();
		}
	    } finally {
		if (webResponse != null) {
		    webResponse.Close();
		}
	    }

	    return ParseResponse(response);
	}

	private AddressValidationResult ParseResponse(string response) {
	    AddressValidationResult result = new AddressValidationResult();
	    XmlDocument xml = new XmlDocument();
	    xml.LoadXml(response);
	    result.ResponseXml = response;
	    result.ResponseType = GetResponseType(xml);

	    if (!result.ResponseType.Equals(ResponseTypeEnum.INVALID)) {
		AddressList addresses = new AddressList();
		XmlNodeList addressNodes = xml.SelectNodes("AddressValidationResponse/AddressKeyFormat");
		foreach (XmlNode node in addressNodes) {
		    AddressData data = new AddressData();
		    XmlNodeList streets = node.SelectNodes("AddressLine");
		    data.Street1 = streets.Item(0).InnerText;
		    if (streets.Count > 1) {
			data.Street2 = streets.Item(1).InnerText;
			if (streets.Count > 2) {
			    data.Street3 = streets.Item(2).InnerText;
			}
		    }
		    data.City = node.SelectSingleNode("PoliticalDivision2").InnerText;
		    data.State = node.SelectSingleNode("PoliticalDivision1").InnerText;
		    data.PostalCode = node.SelectSingleNode("PostcodePrimaryLow").InnerText + "-" + node.SelectSingleNode("PostcodeExtendedLow").InnerText;
		    data.Country = node.SelectSingleNode("CountryCode").InnerText;
		    data.BlockRange = AddressIsBlockRange(data.Street1);
		    addresses.Add(data);
		}
		result.Addresses = addresses;
	    }
	    return result;
	}

	private bool AddressIsBlockRange(string street) {
	    return System.Text.RegularExpressions.Regex.IsMatch(street, @"^\d+-\d+");
	}

	private ResponseTypeEnum GetResponseType(XmlDocument xml) {
	    string responseTypeNode = xml.SelectSingleNode("AddressValidationResponse").ChildNodes[1].OuterXml.ToUpper();
	    if (responseTypeNode == "<VALIDADDRESSINDICATOR />") {
		return ResponseTypeEnum.VALID;
	    } else if (responseTypeNode == "<AMBIGUOUSADDRESSINDICATOR />") {
		return ResponseTypeEnum.AMBIGUOUS;
	    } else if (responseTypeNode == "<NOCANDIDATESINDICATOR />") {
		return ResponseTypeEnum.INVALID;
	    } else {
		return ResponseTypeEnum.UNSET;
	    }
	}

	private string AccessRequestXml {
	    get {
		return string.Format("<?xml version=\"1.0\"?>" +
		    "<AccessRequest xml:lang=\"en-us\">" +
		    "<AccessLicenseNumber>{0}</AccessLicenseNumber>" +
		    "<UserId>{1}</UserId>" +
		    "<Password>{2}</Password>" +
		    "</AccessRequest>", UPSAccessKey, UPSUserId, UPSPassword);
	    }
	}

	private string GetRequestXml(string street1, string street2, string street3, string city, string state, string postalCode) {
	    StringBuilder buffer = new StringBuilder();
	    buffer.Append(@"<?xml version=""1.0""?>" + Environment.NewLine);
	    buffer.Append(@"<AddressValidationRequest xml:lang=""en-US"">" + Environment.NewLine);
	    buffer.Append(@"   <Request>" + Environment.NewLine);
	    buffer.Append(@"      <TransactionReference>" + Environment.NewLine);
	    buffer.Append(@"         <CustomerContext>TC1000201</CustomerContext>" + Environment.NewLine);
	    buffer.Append(@"         <XpciVersion>1.0002</XpciVersion>" + Environment.NewLine);
	    buffer.Append(@"      </TransactionReference>" + Environment.NewLine);
	    buffer.Append(@"      <RequestAction>XAV</RequestAction>" + Environment.NewLine);
	    buffer.Append(@"   </Request>" + Environment.NewLine);
	    buffer.Append(@"   <AddressKeyFormat>" + Environment.NewLine);
	    buffer.Append(@"      <AddressLine>" + street1.ToLower() + "</AddressLine>" + Environment.NewLine);
	    buffer.Append(@"      <AddressLine>" + street2.ToLower() + "</AddressLine>" + Environment.NewLine);
	    buffer.Append(@"      <AddressLine>" + street3.ToLower() + "</AddressLine>" + Environment.NewLine);
	    buffer.Append(@"      <PoliticalDivision2>" + city.ToLower() + "</PoliticalDivision2>" + Environment.NewLine);
	    buffer.Append(@"      <PoliticalDivision1>" + state.ToLower() + "</PoliticalDivision1>" + Environment.NewLine);
	    buffer.Append(@"      <PostcodePrimaryLow>" + postalCode + "</PostcodePrimaryLow>" + Environment.NewLine);
	    buffer.Append(@"      <PostcodeExtendedLow></PostcodeExtendedLow>" + Environment.NewLine);
	    buffer.Append(@"      <CountryCode>US</CountryCode>" + Environment.NewLine);
	    buffer.Append(@"   </AddressKeyFormat>" + Environment.NewLine);
	    buffer.Append(@"   <MaximumListSize>10</MaximumListSize>" + Environment.NewLine);
	    buffer.Append(@"</AddressValidationRequest>" + Environment.NewLine);

	    return buffer.ToString();
	}
    }
}
