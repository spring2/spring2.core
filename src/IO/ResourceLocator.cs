using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace Spring2.Core.IO {

    /// <summary>
    /// Locates a resource (file) in the file system or as a resource in an assembly.
    /// </summary>
    public class ResourceLocator {

	private String filename = String.Empty;
	private FileInfo file = null;
	private Boolean isFile = false;
	private Boolean isResource = false;
	private IList assemblies = new ArrayList();
	private Assembly assembly = null;

	public ResourceLocator(String filename) {
	    this.filename = filename;

	    file = new FileInfo(this.filename);
	    if (file.Exists) {
		this.filename = file.FullName;
		isFile = true;
	    } else {
		foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
		    GetReferencedAssemblies(assembly, assemblies);
		}
		GetReferencedAssemblies(System.Reflection.Assembly.GetExecutingAssembly(), assemblies);
		if (System.Reflection.Assembly.GetEntryAssembly() != null) {
		    GetReferencedAssemblies(System.Reflection.Assembly.GetEntryAssembly(), assemblies);
		}
		if (System.Reflection.Assembly.GetCallingAssembly() != null) {
		    GetReferencedAssemblies(System.Reflection.Assembly.GetCallingAssembly(), assemblies);
		}

		String fn = filename.ToLower().Replace("\\", ".");
		foreach(Assembly a in assemblies) {
//Console.Out.WriteLine(a.FullName);
		    String prefix = a.FullName.Substring(0,a.FullName.IndexOf(",")).ToLower();
		    String[] names = a.GetManifestResourceNames();
		    foreach(String s in names) {
//Console.Out.WriteLine(prefix + " :: " + s.ToLower() + " :: " + fn);
			if (s.ToLower().Equals(fn) || s.ToLower().Equals(prefix + "." + fn)) {
			    this.filename = s;
			    assembly = a;
			    isResource = true;
			}
		    }
		}
	    }
	}

	public static void GetReferencedAssemblies(Assembly a, IList list) {
	    String s = a.GetName().FullName;
	    foreach(AssemblyName an in a.GetReferencedAssemblies()) {
		Assembly c = Assembly.Load(an);
		if (!list.Contains(c)) {
		    list.Add(c);
		    GetReferencedAssemblies(c, list);
		}
	    }
	    if (!list.Contains(a)) {
		list.Add(a);
	    }
	}

	public Stream OpenRead() {
	    if (isFile) {
		return file.OpenRead();
	    } else if (isResource) {
		return assembly.GetManifestResourceStream(filename);
	    } else {
		throw new FileNotFoundException("Resource could not be found", filename);
	    }
	}

	public String FullName {
	    get { return this.filename; }
	}

	public Boolean Exists {
	    get { return (isFile || isResource); }
	}


    }
}
