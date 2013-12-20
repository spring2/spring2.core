using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml.XPath;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace Spring2.Core.Geocode.TomTom {
    public class TomTomClient {
	private string CredentialsClientUrl = "https://api.tomtom.com/lbs/geocoding/geocode?key={0}&language={1}&maxResults={2}&{3}";
	private readonly string CredentialsKey;
	private readonly string LanguageKey;

	private static Random Random = new Random();
	private static int SessionId = Random.Next();

	private const string XPATH_RESPONSESTATUS = "/*[local-name()='geoResponse']";
	private const string XPATH_RESPONSEADDRESS = "/*[local-name()='geoResponse']/*[local-name()='geoResult']";


	public TomTomClient(string apiKey, string language = "en") {
	    CredentialsKey = apiKey;
	    LanguageKey = language;
	}

	public List<Address> CodeAddress(string addressFreeform, string countryCodeIso3, int numberOfResult = 1) {
	    string request = string.Format("&q={0}&CC={1}", Uri.EscapeUriString(addressFreeform), countryCodeIso3);
	    return CodeAddressInternal(request, numberOfResult);
	}

	public List<Address> CodeAddress(Address address, int numberOfResult = 1) {
	    List<string> parts = new List<string>();
	    if (!string.IsNullOrEmpty(address.Number))
		parts.Add(string.Format("ST={0}", (string)Uri.EscapeUriString(address.Number)));
	    if (!string.IsNullOrEmpty(address.Street))
		parts.Add(string.Format("T={0}", (string)Uri.EscapeUriString(address.Street)));
	    if (!string.IsNullOrEmpty(address.City))
		parts.Add(string.Format("L={0}", (string)Uri.EscapeUriString(address.City)));
	    if (!string.IsNullOrEmpty(address.ZIP))
		parts.Add(string.Format("PC={0}", (string)Uri.EscapeUriString(address.ZIP)));
	    if (!string.IsNullOrEmpty(address.State))
		parts.Add(string.Format("AA={0}", (string)Uri.EscapeUriString(address.State)));
	    if (!string.IsNullOrEmpty(address.Country))
		parts.Add(string.Format("CC={0}", (string)Uri.EscapeUriString(address.Country)));

	    return CodeAddressInternal(string.Join("&", parts), numberOfResult);
	}


	private List<Address> CodeAddressInternal(string geocodingRequestUrl, int numberOfResult = 1) {
	    List<Address> result = new List<Address>();
	    ;

	    Stream aStream = null;
	    WebResponse response = null;
	    try {
		string requestUrlFull = string.Format(CredentialsClientUrl, CredentialsKey, LanguageKey, numberOfResult, geocodingRequestUrl);

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrlFull);
		response = request.GetResponse();

		// parse the result
		aStream = response.GetResponseStream();
		XPathDocument docNav = new XPathDocument(aStream);
		XPathNavigator nav = docNav.CreateNavigator();

		int numberOfResults = -1;
		// step 1: check response
		XPathNodeIterator NodeIter = nav.Select(XPATH_RESPONSESTATUS);
		if (NodeIter.MoveNext()) {
		    string numFound = NodeIter.Current.GetAttribute("count", "");
		    if (numFound != null && numFound.Length > 0) {
			numberOfResults = Convert.ToInt32(numFound);
		    }
		}

		// step 2: decode result addresses
		if (numberOfResults > 0) {
		    NodeIter = nav.Select(XPATH_RESPONSEADDRESS);
		    while (NodeIter.MoveNext()) {
			result.Add(ExtractReponseAddress(NodeIter.Current));
		    }
		}
	    } finally {
		if (aStream != null)
		    aStream.Close();
		if (response != null)
		    response.Close();
	    }

	    return result;
	}


	private static Address ExtractReponseAddress(XPathNavigator nav) {
	    Address address = new Address();

	    // lat long
	    NumberFormatInfo provider = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "," };

	    string lat = nav.SelectSingleNode(@"//*[local-name()='latitude']").Value;
	    if (lat != null && lat.Length > 0) {
		address.Position.Latitude = Convert.ToDouble(lat, provider);
	    }

	    string lng = nav.SelectSingleNode(@"//*[local-name()='longitude']").Value;
	    if (lng != null && lng.Length > 0) {
		address.Position.Longitude = Convert.ToDouble(lng, provider);
	    }

	    try {
		address.Street = nav.SelectSingleNode(@"//*[local-name()='street']").Value.Trim();
	    } catch (Exception) { address.Street = string.Empty; }

	    try {
		address.Number = nav.SelectSingleNode(@"//*[local-name()='houseNumber']").Value.Trim();
	    } catch (Exception) { address.Number = string.Empty; }

	    try {
		address.ZIP = nav.SelectSingleNode(@"//*[local-name()='postcode']").Value.Trim();
	    } catch (Exception) { address.ZIP = string.Empty; }

	    try {
		address.City = nav.SelectSingleNode(@"//*[local-name()='city']").Value.Trim();
	    } catch (Exception) { address.City = string.Empty; }

	    try {
		address.Country = nav.SelectSingleNode(@"//*[local-name()='country']").Value.Trim();
	    } catch (Exception) { address.Country = string.Empty; }

	    try {
		address.Geohash = nav.SelectSingleNode(@"//*[local-name()='geohash']").Value.Trim();
	    } catch (Exception) { address.Geohash = string.Empty; }

	    try {
		address.Score = nav.SelectSingleNode(@"//*[local-name()='score']").Value.Trim();
	    } catch (Exception) { address.Score = string.Empty; }
	    try {
		address.Confidence = nav.SelectSingleNode(@"//*[local-name()='confidence']").Value.Trim();
	    } catch (Exception) { address.Confidence = string.Empty; }

	    // admin areas
	    try {
		address.State = address.AdminArea1 = nav.SelectSingleNode(@"//*[local-name()='state']").Value.Trim();
	    } catch (Exception) { address.State = address.AdminArea1 = string.Empty; }

	    try {
		address.District = address.AdminArea2 = nav.SelectSingleNode(@"//*[local-name()='district']").Value.Trim();
	    } catch (Exception) { address.District = address.AdminArea2 = string.Empty; }

	    return address;
	}

    }
}
