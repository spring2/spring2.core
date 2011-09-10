using System;
using System.Reflection;
using System.Text;
using log4net;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment.PayflowPro;

namespace Spring2.Core.Payment.PayflowPro {

    public class PayflowProProvider : IPaymentProvider {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    	
	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
	    return Authorize(referenceNumber, amount, accountNumber, expirationYear, expirationMonth, cvv, name, address, postalCode, comment, StringType.UNSET);
	}

	public PaymentResult Authorize(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    AuthorizationCommand command = new AuthorizationCommand(accountNumber, amount, cvv, referenceNumber, expirationMonth + "/" + expirationYear, name, address, postalCode, comment, StringType.EMPTY, referenceNumber, originalTransactionId);
	    PaymentResult result = command.Execute();
	    return result;
	}

	public PaymentResult Charge(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment) {
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
	    CaptureCommand command = new CaptureCommand(originalTransactionId, amount);
	    PaymentResult result = command.Execute();
	    return result;
	}

    	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    VoidCommand command = new VoidCommand(transactionId);
	    PaymentResult result = command.Execute();
	    return result;
    	}



    }
}