using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Spring2.Core.Util;

namespace Spring2.Core.Maverick {
    /// <summary>
    /// Summary description for MockHttpWorkerRequest.
	/// For more ideas, see:
	/// http://haacked.com/archive/2005/06/11/Simulating_HttpContext.aspx
    /// </summary>
    public class MockHttpWorkerRequest : HttpWorkerRequest {

	private NameValueCollection form = new NameValueCollection();
	private String queryString = String.Empty;
	private NameValueCollection cookies = new NameValueCollection();
    	private String rawUrl = "http://localhost/index.html";
	private StringDictionary headerValues = new StringDictionary();
	private String localAddress = String.Empty;
	private String uriPath = String.Empty;

	public MockHttpWorkerRequest() {
	    Initialize();
	}

	public MockHttpWorkerRequest(String rawUrl) {
	    this.rawUrl = rawUrl;
	    Initialize();
	}
	
    	public MockHttpWorkerRequest(NameValueCollection form) {
	    this.form = form;
	    Initialize();
	}

	public MockHttpWorkerRequest(String rawUrl, NameValueCollection form) {
	    this.rawUrl = rawUrl;
	    this.form = form;
	    Initialize();
	}

	/// <summary>
	/// Sets up the initial values that are to be found in the header
	/// </summary>
	private void Initialize() {
	    headerValues.Add(HttpWorkerRequest.HeaderContentLength.ToString(), (Content.Length - 2).ToString());
	    headerValues.Add(HttpWorkerRequest.HeaderContentType.ToString(), "application/x-www-form-urlencoded");
	    headerValues.Add(HttpWorkerRequest.HeaderAcceptLanguage.ToString(), "en-US");
	    if(this.rawUrl.IndexOf("?") > 0) {
		queryString = rawUrl.Substring(rawUrl.IndexOf("?") + 1);
	    }
	    localAddress = rawUrl.Substring(rawUrl.IndexOf("//")+2);
	    if(localAddress.IndexOf("/") > 0) {
		localAddress = localAddress.Substring(0, localAddress.IndexOf("/"));
	    }
	    uriPath = rawUrl.Substring(7 + localAddress.Length);
	    if(uriPath.IndexOf("?") > 0) {
		uriPath = uriPath.Remove(uriPath.IndexOf("?"), queryString.Length + 1);
	    }
	}

	public void SetHeaderValue(Int32 index, String value) {
	    headerValues[index.ToString()] = value;
	}

	public override string GetUriPath() {
	    return uriPath;
	}

	public override string GetQueryString() {
	    return queryString;
	}

	public override string GetRawUrl() {
	    return rawUrl;
	}

	public override string GetHttpVerbName() {
	    return "GET";
	}

	public override string GetHttpVersion() {
	    return "HTTP/1.1";
	}

	public override string GetRemoteAddress() {
	    return "remoteaddress";
	}

	public override int GetRemotePort() {
	    throw new NotImplementedException();
	}

	public override string GetLocalAddress() {
	    return this.localAddress;
	}

	public override int GetLocalPort() {
	    return 80;
	}

	public override void SendStatus(int statusCode, string statusDescription) {
	    throw new NotImplementedException();
	}

	public override void SendKnownResponseHeader(int index, string value) {
	    throw new NotImplementedException();
	}

	public override void SendUnknownResponseHeader(string name, string value) {
	    throw new NotImplementedException();
	}

	public override void SendResponseFromMemory(byte[] data, int length) {
	    throw new NotImplementedException();
	}

	public override void SendResponseFromFile(string filename, long offset, long length) {
	    throw new NotImplementedException();
	}

	public override void SendResponseFromFile(IntPtr handle, long offset, long length) {
	    throw new NotImplementedException();
	}

	public override void FlushResponse(bool finalFlush) {
	    throw new NotImplementedException();
	}

	public override void EndOfRequest() {
	    throw new NotImplementedException();
	}

	public override string GetKnownRequestHeader(int index) {
	    if (headerValues.ContainsKey(index.ToString())) {
	    	return headerValues[index.ToString()];
	    } else {
		//System.Diagnostics.Debug.WriteLine("GetKnownRequestHeader not implemented for index: " + index);
		return String.Empty;
	    }
	}

	public override byte[] GetPreloadedEntityBody() {
	    return Encoding.ASCII.GetBytes(StringUtil.RemoveTrailingNewLine(Content));
	}

	private String Content {
	    get {
		StringBuilder sb = new StringBuilder();
		for(Int32 i=0; i<form.Keys.Count; i++) {
		    String key = form.Keys[i];
		    String value = form[key];
		    sb.Append(key).Append("=").Append(value);
		    if (i < form.Keys.Count-1) {
			sb.Append("&");
		    }
		}
		if (form.Keys.Count>0) {
		    sb.Append("\r\n");
		}

		return sb.ToString();
	    }
	}
    }

}
