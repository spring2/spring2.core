using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment {
    public class FundsUnavailableException : Exception {
	public FundsUnavailableException() : 
	    base("Insufficient funds") {
	}
	public FundsUnavailableException(CurrencyType amountLacking) : 
	    base("Insufficient funds (" + amountLacking + ")"){
	}
    }
}
