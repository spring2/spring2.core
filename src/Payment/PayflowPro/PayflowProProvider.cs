using System;
using System.Reflection;
using System.Text;
using log4net;
using PayFlowPro;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment.PayflowPro;

namespace Spring2.Core.Payment.PayflowPro {

    public class PayflowProProvider : IPaymentProvider {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    	
	public PaymentResult Authorize(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    throw new System.NotImplementedException();
	}

	public PaymentResult Charge(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    SaleCommand command =
	    	new SaleCommand(accountNumber, amount, cvv, referenceNumber, expirationMonth + "/" + expirationYear, name, address,
	    	                postalCode, comment, StringType.EMPTY, referenceNumber);
	    PaymentResult result = command.Execute();
	    return result;
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    CreditCommand command =
		new CreditCommand(accountNumber, amount, cvv, referenceNumber, expirationMonth + "/" + expirationYear, name, address,
		postalCode, comment, StringType.EMPTY, referenceNumber);
	    PaymentResult result = command.Execute();
	    return result;
	}
	
	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new System.NotImplementedException();
	}

	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    throw new System.NotImplementedException();
	}

    	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    VoidCommand command = new VoidCommand(transactionId);
	    PaymentResult result = command.Execute();
	    return result;
    	}

    }
}