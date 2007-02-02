using System;
using System.Web.Services.Protocols;

namespace Spring2.Dss.Soap {

    /// <summary>
    /// Summary description for CompressionExtensionAttribute.
    /// </summary>
	
    // Make the Attribute only Applicable to Methods
    [AttributeUsage(AttributeTargets.Method)]
    public class Log4NetExtensionAttribute : System.Web.Services.Protocols.SoapExtensionAttribute {
    
	private int priority;
    	private LogicalMethodInfo methodInfo = null;
	protected string logClass = string.Empty;

	public Log4NetExtensionAttribute(string loggingClass) {
	    logClass = loggingClass;
	}

	public string LoggingClass {
	    get {
		return logClass;
	    }
	    set {
		logClass = value;
	    }
	}

	// Override the base class properties
	public override Type ExtensionType {
	get {
	    return typeof(Log4NetExtension);
	    }
	}

	public override int Priority {
	    get { 
		return priority; 
	    }
	    set { 
		priority = value; 
	    }
	}
    	
	public LogicalMethodInfo MethodInfo {
	    get {
		return methodInfo; 
	    }
	    set {
		methodInfo = value;
	    }
	}

    }
}
