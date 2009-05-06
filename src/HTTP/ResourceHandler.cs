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
	private const String APPSETTING_RESOURCEDLL = "Spring2.Core.HTTP.ResourceHandler_ResourceDll";
	private const String APPSETTING_USEFILESYSTEM = "Spring2.Core.HTTP.ResourceHandler_UseFileSystem";

	public void ProcessRequest(HttpContext context) {
	    String resource = context.Request.Params["d"];
	    SetTypeAndImageFormat(resource);
	    if(ConfigurationSettings.AppSettings[APPSETTING_USEFILESYSTEM] == null || ConfigurationSettings.AppSettings[APPSETTING_USEFILESYSTEM].ToUpper() != "TRUE") {
		this.ProcessResource(context, resource);
	    } else {
		this.ProcessResourceFromFile(context, resource);
	    }
	    context.Response.ContentType = contentType;
	    context.Response.End();
	}

	public bool IsReusable {
	    get {
		return false;
	    }
	}

	private void ProcessResource(HttpContext context, String resource) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_").Replace("/", @"\");

	    Assembly asm = Assembly.Load(ConfigurationSettings.AppSettings[APPSETTING_RESOURCEDLL]);
	    System.Resources.ResourceManager rm = new System.Resources.ResourceManager(resourceObject, asm);

	    if(imageFormat != null) {
		Bitmap bitmap = (Bitmap)rm.GetObject(resourceProperty);
		bitmap.Save(context.Response.OutputStream, imageFormat);
	    } else {
		context.Response.Write(rm.GetString(resourceProperty));
	    }
	}

	private void ProcessResourceFromFile(HttpContext context, String resource) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1);

	    String fileName = ConfigurationSettings.AppSettings[resourceObject] + @"\" + resourceProperty;

	    if(imageFormat != null) {
		Image img = Image.FromFile(fileName);
		img.Save(context.Response.OutputStream, imageFormat);
	    } else {
		context.Response.Write(System.IO.File.OpenText(fileName).ReadToEnd());
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
