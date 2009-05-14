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
	private String contentType = String.Empty;
	private ImageFormat imageFormat = null;
	private const String APPSETTING_RESOURCEDLL = "Spring2.Core.HTTP.ResourceHandler_ResourceDll";
	private const String APPSETTING_RESOUREFILEPREFIX = "Spring2.Core.HTTP.ResourceHandler_ResourceFilePrefix";
	private const String APPSETTING_RESOURCETYPE = "Spring2.Core.HTTP.ResourceHandler_Type";

	public void ProcessRequest(HttpContext context) {
	    String resource = context.Request.Params["d"];
	    SetTypeAndImageFormat(resource);

	    String value = ConfigurationSettings.AppSettings[APPSETTING_RESOURCETYPE] != null ? ConfigurationSettings.AppSettings[APPSETTING_RESOURCETYPE].ToUpper() : String.Empty;
	    switch(value) {
		case "FILESYSTEM":
		    this.ProcessResourceFromFile(context, resource);
		    break;
		case "RESOURCEFILE":
		    this.ProcessResourcesFromResourceFile(context, resource);
		    break;
		case "RESOURCEASSEMBLY":
		    this.ProcessResourceFromAssembly(context, resource);
		    break;
		default:
		    throw new FormatException("'" + APPSETTING_RESOURCETYPE + "' app setting needs to be set to 'FileSystem', 'ResourceFile', or 'ResourceAssembly'");
	    }

	    context.Response.ContentType = contentType;
	    context.Response.End();
	}

	public bool IsReusable {
	    get {
		return false;
	    }
	}

	private void ProcessResourceFromAssembly(HttpContext context, String resource) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_").Replace("/", @"\");

	    Assembly asm = Assembly.Load(ConfigurationSettings.AppSettings[APPSETTING_RESOURCEDLL]);
	    ResourceManager rm = new ResourceManager(resourceObject, asm);

	    if(imageFormat != null) {
		Bitmap bitmap = (Bitmap)rm.GetObject(resourceProperty);
		bitmap.Save(context.Response.OutputStream, imageFormat);
	    } else {
		context.Response.Write(rm.GetString(resourceProperty));
	    }
	}

	private void ProcessResourcesFromResourceFile(HttpContext context, String resource) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_").Replace("/", @"\");

	    ResourceManager rm = ResourceManager.CreateFileBasedResourceManager(ConfigurationSettings.AppSettings[APPSETTING_RESOUREFILEPREFIX] + resourceObject, context.Server.MapPath("bin").Replace(@"\~", ""), null);

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
		StreamReader sr = File.OpenText(fileName);
		context.Response.Write(sr.ReadToEnd());
		sr.Close();
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
