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
	    log4net.Config.XmlConfigurator.Configure();
	    try {
		// For now assume USD -> CAD
		StringType currency = "CAD";
		if (args.Length > 0) {
		}
		ICurrencyExchange line = CurrencyExchange.CheckForRateAndUpdate(currency);
		log.Info(string.Format("Exchange rate for CAD (Canadian Dollar) based on USD (US Dollar) is {0} on {1}.", line.Rate, line.EffectiveDate));
	    } catch (Exception ex) {
		log.Error(String.Format("An error occurred checking for a new exchange rate.\nThe exception message is: {0}", ex.Message));
	    }
	}
    }
}
