using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;
using Spring2.Core.Types;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Publication.BusinessLogic;

namespace Spring2.Core.Publication.PublicationService {
    public class PublicationService {
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	static void Main(string[] args) {
	    MDC.Set("hostname", Environment.MachineName);

	    log.Info("starting");
	    PublicationTypeList publications = PublicationType.GetActivePublications();
	    foreach (PublicationType publication in publications) {
		try {
		    IPublicationServiceProvider provider = GetProvider(publication.ProviderName);
		    provider.Process();
		    publication.Update(new PublicationTypeData() { LastSentDate = DateTimeType.Now });

		    log.Info(string.Format("PublicationType, {0}, successfully processed.", publication.Name.Display()));
		} catch (Exception ex) {
		    log.Error(string.Format("An error occurred while attempting to proccess publication, {1}.{0}The exception message is: {0}{2}",
			Environment.NewLine, publication.Name.Display(), ex.Message));
		}
	    }
	    log.Info("finished");
	}

	public static IPublicationServiceProvider GetProvider(StringType providerName) {
	    if (providerName == null || providerName.IsEmpty) {
		throw new ArgumentException("The ProviderName for a PublicationType cannot be empty.");
	    }

	    Type t = Type.GetType(providerName.Display());
	    if (t == null) {
		throw new PublicationTypeException("Cannot get the Type of the class based on the ProviderName string.");
	    }

	    Object provider = Activator.CreateInstance(t);
	    if (!(provider is IPublicationServiceProvider)) {
		throw new InvalidTypeException("IPublicationServiceProvider");
	    }

	    return provider as IPublicationServiceProvider;
	}
    }
}
