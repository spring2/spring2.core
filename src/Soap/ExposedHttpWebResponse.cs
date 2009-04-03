using System;
using System.IO;
using System.Net;

namespace Spring2.Core.Soap {
    public class ExposedHttpWebResponse : WebResponse {
	/// stores response, so it can be searchable
	private HttpWebResponse _response = null;

	private Stream _responseStream = null;

	#region properties, duplicates of HttpWebResponse 

	public override long ContentLength {
	    get { return _response.ContentLength; }
	}

	public override WebHeaderCollection Headers {
	    get { return _response.Headers; }
	}

	public override string ContentType {
	    get { return _response.ContentType; }
	}

	public override Uri ResponseUri {
	    get { return _response.ResponseUri; }
	}

	#endregion

	public ExposedHttpWebResponse(WebRequest request) : base() {
	    _response = (HttpWebResponse) request.GetResponse();
	}

	/// <summary>
	/// get response stream and populate _responseStream var, so we have access to it;
	/// </summary>
	public override Stream GetResponseStream() {
	    Stream contentStream = null;
	    Stream returnStream = null;
	    try {
		if (_response != null) {
		    contentStream = _response.GetResponseStream();
		    _responseStream = new MemoryStream();
		    returnStream = new MemoryStream();
		    // read data by bytes
		    int byte1 = 0;
		    do {
			byte1 = contentStream.ReadByte();
			if (byte1 != -1) {
			    _responseStream.WriteByte((byte) byte1);
			    returnStream.WriteByte((byte) byte1);
			}
		    } while (byte1 != -1);
		    _responseStream.Flush();
		    returnStream.Flush();
		    contentStream.Close();
		    contentStream = null;
		    _responseStream.Seek(0, SeekOrigin.Begin);
		    returnStream.Seek(0, SeekOrigin.Begin);
		}
	    } catch {
		// TODO: what to do here?
	    } finally {
		if (contentStream != null) {
		    contentStream.Close();
		}
		if (_response != null) {
		    _response.Close();
		}
	    }

	    return returnStream;
	}

	/// <summary>
	/// return content of the response;
	/// </summary>
	/// <remarks>reset position of the stream to the start</remarks>
	public string GetContent() {
	    StreamReader reader = null;
	    string responseContent = "";
	    try {
		if (_responseStream != null) {
		    _responseStream.Seek(0, SeekOrigin.Begin);
		    reader = new StreamReader(_responseStream, System.Text.Encoding.UTF8);
		    responseContent = reader.ReadToEnd();
		    // replace \r \n converted to &#xD, &#xA
		    responseContent = responseContent.Replace("&#xD;", "\n").Replace("&#xA;", "\r");
		    _responseStream.Seek(0, SeekOrigin.Begin);
		}
	    } catch {
		// TODO: what to do here?
	    } finally {
		if (reader != null) {
		    reader.Close();
		}
	    }
	    return responseContent;
	}
    }
}