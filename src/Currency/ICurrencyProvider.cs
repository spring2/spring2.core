using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Currency {
    public interface ICurrencyProvider {
	double GetConversionRate(string fromCurrency, string toCurrency);
    }
}
