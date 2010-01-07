using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Currency {
    public class WebServiceXProvider : ICurrencyProvider {

	public double GetConversionRate(string fromCurrency, string toCurrency) {
	    WebServiceXWebServiceWrapper wrapper = new WebServiceXWebServiceWrapper();
	    return wrapper.GetConversionRate(fromCurrency, toCurrency);
	}
    }
}
