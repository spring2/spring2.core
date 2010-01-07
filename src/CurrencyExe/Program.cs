using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core.Types;

using Spring2.Core.Currency.BusinessLogic;
using Spring2.Core.Currency.DataObject;

namespace Spring2.Core.Currency.CurrencyExe {
    class Program {
	static void Main(string[] args) {
	    // For now assume USD -> CAD
	    StringType currency = "CAD";
	    ICurrencyExchange line = CurrencyExchange.CheckForRateAndUpdate(currency);
	    Console.Out.Write(string.Format("Exchange rate for CAD (Canadian Dollar) based on USD (US Dollar) is {0} at {1}.", line.Rate, line.EffectiveDate));
	}
    }
}
