using System;
using System.Configuration;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.SendMailMessages {

    class SendMailMessages {

	protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	/// <summary>
	/// Schedulable task that reads from the mail queue anything has to be mailed up to now
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    // use MDC to set the hostname for log messages -- MDC is thread specific
	    log4net.MDC.Set("hostname", Environment.MachineName);

	    log.Info("starting");
	    try {
		// login as system service user
		// TODO: using a common core component, this probably wouldn't be possible, is this a concern?
		//ServiceFacade serviceFacade = new ServiceFacade();
		//serviceFacade.Login();

		MailMessageList mails = MailMessage.GetMailMessagesToSend();
		foreach(MailMessage mail in mails) {
		    try {
			mail.Send();
		    } catch (Exception ex) {
			// catch any exception, log it and keep going.  Don't want to stop sending 
			// any message because of a problem with one.
			log.Error(ex);
		    }
		}
	    } catch (Exception ex) {
		// catch all so that any exception not dealt with gets logged
		log.Fatal(ex);
	    }
	    log.Info("finished");
	}

    }
}
