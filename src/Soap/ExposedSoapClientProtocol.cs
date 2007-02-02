using System;
using System.Net;

namespace Spring2.Dss.Soap {
    public class ExposedSoapClientProtocol : System.Web.Services.Protocols.SoapHttpClientProtocol {
	private WebResponse _lastResponse;

	protected override WebRequest GetWebRequest(Uri uri) {
	    HttpWebRequest webRequest = (HttpWebRequest) base.GetWebRequest(uri);
	    //	    webRequest.KeepAlive = false;
	    //	    webRequest.ProtocolVersion=HttpVersion.Version10;
	    return webRequest;
	}

    	protected override WebResponse GetWebResponse(WebRequest request) {
	    _lastResponse = new ExposedHttpWebResponse(request as HttpWebRequest);
	    //StreamReader sr = new StreamReader(request.GetRequestStream());
	    //Console.Out.WriteLine(sr.ReadToEnd());
	    return _lastResponse;
	}

	/// <summary>
	/// return the whole response xml, including soap envelope
	/// </summary>
	public string GetResponseXML() {
	    string result = "";
	    try {
		ExposedHttpWebResponse exposedLastResponse = _lastResponse as ExposedHttpWebResponse;
		if (exposedLastResponse != null) {
		    result = exposedLastResponse.GetContent();
		}
	    } catch {
		// TODO: what to do here?
	    }
	    return result;
	}
    	
    	public string GetRequestXml() {
	    return "";
    	}
    }
}