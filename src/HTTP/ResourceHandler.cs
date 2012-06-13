using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Web;
using System.Drawing.Imaging;
using System.Configuration;
using System.IO;
using System.Resources;

namespace Spring2.Core.HTTP {
    public class ResourceHandler : IHttpHandler {
	private const String APPSETTING_CACHELENGTH = "Spring2.Core.HTTP.ResourceHandler_CacheLengthInDays";

	private String contentType = String.Empty;
	private ImageFormat imageFormat = null;

	public void ProcessRequest(HttpContext context) {
	    Int32 days = 0;
	    try {
		days = Int32.Parse(ConfigurationManager.AppSettings[APPSETTING_CACHELENGTH] ?? "0");
	    } catch(Exception) { }
	    context.Response.Cache.SetCacheability(HttpCacheability.Public);
	    context.Response.Cache.SetExpires(DateTime.Now.AddDays(days));
	    String resource = context.Request.Params["d"];
	    if(resource != null && resource.Length > 3 && resource.Contains(".")) {
		SetTypeAndImageFormat(resource);
		try {
		    using(MemoryStream stream = ResourceUtility.GetResourceStream(context, resource, imageFormat)) {
			stream.WriteTo(context.Response.OutputStream);
		    }
		} catch(Exception ex) {
		    contentType = "text/plain";
		    context.Response.StatusCode = 404;
		    context.Response.Write("Not Found!\n");
		    context.Response.Write("Exception::\n" + ex.ToString());
		}
	    } else {
		contentType = "text/plain";
		context.Response.StatusCode = 400;
		context.Response.Write(" ");
	    }
	    context.Response.ContentType = contentType;
	    context.Response.End();
	}

	public bool IsReusable {
	    get {
		return false;
	    }
	}

	private void ImageToOutputStream(Image img, HttpContext context) {
	    if(imageFormat == ImageFormat.Png) {
		//special stuff
		MemoryStream ms = new MemoryStream();
		img.Save(ms, ImageFormat.Png);
		ms.WriteTo(context.Response.OutputStream);
		ms.Dispose();
	    } else {
		img.Save(context.Response.OutputStream, imageFormat);
	    }
	    img.Dispose();
	}

	private String GetExtension(String resource) {
	    String extension = "";
	    if(null != resource) {
		int indexSeperator = resource.LastIndexOf(".");
		if(indexSeperator > -1)
		    extension = resource.Substring(indexSeperator + 1, resource.Length - indexSeperator - 1).ToUpper();
	    }
	    return extension;
	}

	private void SetTypeAndImageFormat(String resource) {
	    switch(GetExtension(resource)) {
		case "JPG":
		case "JPEG":
		    contentType = "image/jpeg";
		    imageFormat = ImageFormat.Jpeg;
		    break;
		case "PNG":
		    contentType = "image/png";
		    imageFormat = ImageFormat.Png;
		    break;
		case "GIF":
		    contentType = "image/gif";
		    imageFormat = ImageFormat.Gif;
		    break;
		case "CSS":
		    contentType = "text/css";
		    break;
		case "XML":
		    contentType = "text/xml";
		    break;
		case "JS":
		    contentType = "text/javascript";
		    break;
		case "SWF":
		    contentType = "application/x-shockwave-flash";
		    break;
		default:
		    contentType = "text/plain";
		    break;
	    }
	}
    }
}
