using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Currency {
    public class CurrencyTestProvider : ICurrencyProvider {
	public double GetConversionRate(string fromCurrency, string toCurrency) {
	    return 1.6969;
	}
    }
}
