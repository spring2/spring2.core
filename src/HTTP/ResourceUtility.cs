using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;
using System.Resources;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;

namespace Spring2.Core.HTTP {
    public class ResourceUtility {
	private const String APPSETTING_RESOURCEDLL = "Spring2.Core.HTTP.ResourceHandler_ResourceDll";
	private const String APPSETTING_RESOURCETYPE = "Spring2.Core.HTTP.ResourceHandler_Type";

	/// <summary>
	/// Gets the resource stream. Assumes that this resource is a text based resource file.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="resource">The resource.</param>
	/// <returns></returns>
	public static MemoryStream GetResourceStream(HttpContext context, string resource) {
	    return GetResourceStream(context, resource, null);
	}

	/// <summary>
	/// Gets the resource stream. If desiredFormat is null, it is assumed that the resouce is text. Otherwise, it is an image.
	/// </summary>
	/// <param name="context">The context.</param>
	/// <param name="resource">The resource.</param>
	/// <param name="desiredFormat">The desired format.</param>
	/// <returns></returns>
	public static MemoryStream GetResourceStream(HttpContext context, string resource, ImageFormat desiredFormat) {
	    MemoryStream stream = null;

	    String value = ConfigurationSettings.AppSettings[APPSETTING_RESOURCETYPE] ?? String.Empty;
	    switch (value.ToUpper()) {
		case "FILESYSTEM":
		    stream = ProcessResourceFromFile(context, resource, desiredFormat);
		    break;
		case "RESOURCEFILE":
		    stream = ProcessResourcesFromResourceFile(context, resource, desiredFormat);
		    break;
		case "RESOURCEASSEMBLY":
		    stream = ProcessResourceFromAssembly(context, resource, desiredFormat);
		    break;
		default:
		    throw new FormatException("'" + APPSETTING_RESOURCETYPE + "' app setting needs to be set to 'FileSystem', 'ResourceFile', or 'ResourceAssembly'");
	    }

	    return stream;
	}

	private static MemoryStream ProcessResourceFromFile(HttpContext context, String resource, ImageFormat desiredFormat) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1);
	    String fileName = ConfigurationSettings.AppSettings[resourceObject] + @"\" + resourceProperty;

	    MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileName));
	    return ms;
	}

	private static MemoryStream ProcessResourceFromAssembly(HttpContext context, String resource, ImageFormat desiredFormat) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_").Replace("/", @"\");

	    Assembly asm = Assembly.Load(ConfigurationSettings.AppSettings[APPSETTING_RESOURCEDLL]);
	    ResourceManager rm = new ResourceManager(resourceObject, asm);

	    return GetMemoryStreamFromObject(rm.GetObject(resourceProperty), desiredFormat);
	}

	private static MemoryStream ProcessResourcesFromResourceFile(HttpContext context, String resource, ImageFormat desiredFormat) {
	    String resourceObject = resource.Substring(0, resource.IndexOf("."));
	    String resourceProperty = resource.Substring(resourceObject.Length + 1).Replace(".", "_").Replace("/", @"\");
	    ResourceManager rm = ResourceManager.CreateFileBasedResourceManager(resourceObject, context.Server.MapPath("~/Bin").Replace(@"\~", ""), null);

	    return GetMemoryStreamFromObject(rm.GetObject(resourceProperty), desiredFormat);
	}

	private static MemoryStream GetMemoryStreamFromObject(object o, ImageFormat desiredFormat) {
	    MemoryStream stream = new MemoryStream();
	    if (desiredFormat == null) { // We only expect image files and text files.
		UTF8Encoding enc = new UTF8Encoding();
		byte[] bytes = enc.GetBytes(o as string);
		stream.Write(bytes, 0, bytes.Length);
	    } else {
		using (Bitmap image = o as Bitmap) {
		    image.Save(stream, desiredFormat);
		}
	    }
	    stream.Position = 0;
	    return stream;
	}
    }
}
