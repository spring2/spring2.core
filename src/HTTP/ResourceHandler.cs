using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Web;
using System.Drawing.Imaging;
using System.Configuration;

namespace Spring2.Core.HTTP {
    public class ResourceHandler : IHttpHandler {
	private String contentType = String.Empty;
	private ImageFormat imageFormat = null;
	private const String APPSETTING = "Spring2.Core.HTTP.ResourceHandler_ResourceDll";

	public void ProcessRequest(HttpContext context) {
	    String resource = context.Request.Params["d"];
	    SetTypeAndImageFormat(resource);
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_");

	    Assembly asm = Assembly.Load(ConfigurationSettings.AppSettings[APPSETTING]);
	    System.Resources.ResourceManager rm = new System.Resources.ResourceManager(resourceObject, asm);

	    if(imageFormat != null) {
		Bitmap bitmap = (Bitmap)rm.GetObject(resourceProperty);
		bitmap.Save(context.Response.OutputStream, imageFormat);
	    } else {
		context.Response.Write(rm.GetString(resourceProperty));
	    }
	    context.Response.ContentType = contentType;
	    context.Response.End();
	}

	public bool IsReusable {
	    get {
		return false;
	    }
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
		    contentType = "image/jpg";
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
		default:
		    contentType = "text/plain";
		    break;
	    }
	}
    }
}
