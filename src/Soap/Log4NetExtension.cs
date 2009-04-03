using System;
using System.IO;
using System.Web.Services.Protocols;
using System.Xml;

namespace Spring2.Core.Soap {

    /// <summary>
    /// Log4Net soap extension for logging request and response xml
    /// </summary>
    /// <remarks>
    /// Extension based on CompressExtension example from Saurabh Nandu.
    /// </remarks>
    public class Log4NetExtension : System.Web.Services.Protocols.SoapExtension {

	//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private static log4net.ILog log = null;

    	private Stream oldStream;
	private Stream newStream;
    	private Guid correlationGuid = Guid.Empty;
    	private Log4NetExtensionAttribute attribute = null;
	private static string logCategory = string.Empty;
    	
	public Log4NetExtension() {
	    if (!log4net.LogManager.GetRepository().Configured) {
		log4net.Config.XmlConfigurator.Configure();
	    }
	}
    	
//    	public Log4NetExtension(string loggingCategory) {
//	    logCategory = loggingCategory;
//	    log = log4net.LogManager.GetLogger(logCategory);
//	    if (!log4net.LogManager.GetRepository().Configured) {
//	    	log4net.Config.XmlConfigurator.Configure();
//	    }
//    	}

	public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute) {
	    ((Log4NetExtensionAttribute) attribute).MethodInfo = methodInfo;
	    return attribute;
	}

	// Get the Type
	public override object GetInitializer(Type t) {
	    return typeof(Log4NetExtension);
	}
    
	// Get the Log4NetExtensionAttribute
	public override void Initialize(object initializer) {
	    attribute = (Log4NetExtensionAttribute) initializer;
	    correlationGuid = Guid.NewGuid();
	    logCategory = attribute.LoggingClass;
	    log = log4net.LogManager.GetLogger(logCategory);

	    return;
	}

	// Gives us the ability to get hold of the RAW SOAP message
	public override Stream ChainStream( Stream stream ) {
	    oldStream = stream;
	    newStream = new MemoryStream();
	    return newStream;
	}

	//  If the SoapMessageStage is such that the SoapRequest or
	//  SoapResponse is still in the SOAP format to be sent or received,
	//  save it out to a file.
	public override void ProcessMessage(SoapMessage message) {
	    switch (message.Stage) {
		case SoapMessageStage.BeforeSerialize:
		    break;
		case SoapMessageStage.AfterSerialize:
		    WriteOutput(message);
		    break;
		case SoapMessageStage.BeforeDeserialize:
		    WriteInput(message);
		    break;
		case SoapMessageStage.AfterDeserialize:
		    break;
		default:
		    throw new Exception("invalid stage");
	    }
	}

	public void WriteOutput(SoapMessage message) {
	    LogMessage(message, (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest");
	    Copy(newStream, oldStream);
	}

	public void WriteInput(SoapMessage message) {
	    Copy(oldStream, newStream);
	    LogMessage(message, (message is SoapServerMessage) ? "SoapRequest" : "SoapResponse");
	}

	private void LogMessage(SoapMessage message, String messageType) {
	    if (log.IsInfoEnabled) {
		newStream.Position = 0;
	    	XmlTextReader reader = new XmlTextReader(newStream);
	    	XmlDocument dom = new XmlDocument();
	    	dom.Load(reader);
	    	StringWriter writer = new StringWriter();
	    	dom.Save(writer);

		log.InfoFormat("{0} ~ {1} ~ {2} :: {3}", attribute.MethodInfo.Name, correlationGuid, messageType, writer.ToString());
	    }
	    
	    newStream.Position = 0;
	}

	// Gives us the ability to get hold of the RAW SOAP message
	private void Copy(Stream from, Stream to) {
	    TextReader reader = new StreamReader(from);
	    TextWriter writer = new StreamWriter(to);
	    writer.WriteLine(reader.ReadToEnd());
	    writer.Flush();
	}

    }
}
