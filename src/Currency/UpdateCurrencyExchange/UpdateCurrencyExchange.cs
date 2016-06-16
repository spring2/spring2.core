using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Types;

using Spring2.Core.Currency.BusinessLogic;
using Spring2.Core.Currency.DataObject;

namespace Spring2.Core.Currency.UpdateCurrencyExchange {
    class UpdateCurrencyExchange {
	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	static void Main(string[] args) {
	    // use MDC to set the hostname for log messages -- MDC is thread specific
	    log4net.MDC.Set("hostname", Environment.MachineName);

	    log.Info("starting");
	    try {
		StringType currency = StringType.EMPTY;
		if (args.Length == 0) {
		    log.Fatal("Missing required input parameter: currency code");
		} else {
		    currency = args[0];
		    log.Info(string.Format("Checking for new currency exchange rate for currency code {0}", currency));
		    ICurrencyExchange line = CurrencyExchange.CheckForNewRate(currency);
		    log.Info(string.Format("Exchange rate for {0} based on USD (US Dollar) is {1} on {2}", currency, line.Rate, line.EffectiveDate));
		}
	    } catch (Exception ex) {
		log.Fatal(String.Format("An error occurred checking for a new exchange rate.\nThe exception message is: {0}", ex.Message));
	    }
	    log.Info("finished");
	}
    }
}
