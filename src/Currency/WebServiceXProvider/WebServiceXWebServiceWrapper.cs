using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core.net.webservicex.www;

namespace Spring2.Core.Currency {
    public class WebServiceXWebServiceWrapper {

	public double GetConversionRate(string fromCurrency, string toCurrency) {
	    double retval = 0;

	    try {
		CurrencyConvertor cc = new CurrencyConvertor();

		object from = Enum.Parse(typeof(net.webservicex.www.Currency), fromCurrency);
		object to = Enum.Parse(typeof(net.webservicex.www.Currency), toCurrency);

		retval = cc.ConversionRate((net.webservicex.www.Currency)from, (net.webservicex.www.Currency)to);
	    } catch (Exception ex) {
		throw ex;
	    }

	    return retval;
	}
    }
}
