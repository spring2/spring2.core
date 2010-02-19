using System;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using log4net;
using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Security;
using Spring2.Core.Types;

namespace Spring2.Core.Publication.PublicationService {

    public class PublicationService {
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	static void Main(string[] args) {
	    if (args.Length < 1) 
		throw new ArgumentException("Need a user ID passed in.");

	    AppDomain myDomain = Thread.GetDomain();
	    myDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

	    IdType userId = IdType.Parse(args[0]);
	    IUserPrincipal principal = new UserPrincipal(userId);
	    Thread.CurrentPrincipal = principal;

	    DoWork();
	}

	public static void DoWork() {
	    MDC.Set("hostname", Environment.MachineName);

	    log.Info("starting");
	    PublicationTypeList publications = PublicationType.GetActivePublicationsForProcessing();
	    foreach (PublicationType publication in publications) {
		try {
		    IPublisher provider = GetProvider(publication.ProviderName);
		    provider.Process(publication);

		    log.Info(string.Format("PublicationType, {0}, successfully processed.", publication.Name.Display()));
		} catch (Exception ex) {
		    log.Error(string.Format("An error occurred while attempting to proccess publication, {1}.{0}The exception message is: {0}{2}",
			Environment.NewLine, publication.Name.Display(), ex.Message));
		}
	    }
	    log.Info("finished");
	}

	public static IPublisher GetProvider(StringType providerName) {
	    if (providerName == null || providerName.IsEmpty) {
		throw new ArgumentException("The ProviderName for a PublicationType cannot be empty.");
	    }

	    Type t = Type.GetType(providerName.Display());
	    if (t == null) {
		throw new PublicationTypeException("Cannot get the Type of the class based on the ProviderName string.");
	    }

	    Object provider = Activator.CreateInstance(t);
	    if (!(provider is IPublisher)) {
		throw new InvalidTypeException("IPublicationServiceProvider");
	    }

	    return provider as IPublisher;
	}
    }
}
