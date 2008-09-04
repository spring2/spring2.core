using System;
using System.Reflection;
using System.Text;
using log4net;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment;

namespace Spring2.Core.Payment.ProPay {

    public class ProPayProvider : IPaymentProvider {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	// signs up a new account
	public ProPayResult SignupAccount(StringType country, StringType email, StringType firstName, StringType mi, StringType lastName,
				     StringType address, StringType apartmentNumber, StringType city, StringType state, StringType postalCode,
				     PhoneNumberType dayPhone, PhoneNumberType eveningPhone, StringType ssn, DateTimeType birthday,
				     StringType creditCardNumber, StringType creditCardExpiration) {
	    MerchantSignupCommand command = new MerchantSignupCommand(country,email,firstName,mi,lastName,address,apartmentNumber,
									city,state,postalCode,dayPhone,eveningPhone,ssn,
									birthday,creditCardNumber,creditCardExpiration);
	    ProPayResult result = command.Execute();
	    return result;
	}

	public ProPayResult Ping(StringType proPayAccountEmail, StringType ssn) {
	    AccountPingCommand command = new AccountPingCommand(proPayAccountEmail, ssn);
	    ProPayResult result = command.Execute();
	    return result;
	}

	// funnels funds from the "master" account to another account
	public ProPayResult Split(CurrencyType amount, StringType recipientAccount) {
	    SplitCommand command = new SplitCommand(amount, recipientAccount);
	    ProPayResult result = command.Execute();
	    return result;
	}

	// This will max out at 500 results
	public StringType GetSignupReport(DateType startDate, DateType endDate) {
	    GetSignupsCommand command = new GetSignupsCommand(startDate, endDate);
	    ProPayResult result = command.Execute();
	    return result.RawResponse;
	}
    	
	public PaymentResult Authorize(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    StringType emptyApartmentNumber = StringType.DEFAULT; // apartment numbers will need to be part of the combined address
	    StringType emptyInvNumber = StringType.DEFAULT; // optional value
	    StringType cardExpirationDate = new StringType(expirationMonth.ToString() + expirationYear.ToString());
	    StringType proPayAccountNumber = referenceNumber;
	    StringType creditCardNumber = accountNumber;
	    AuthorizeCommand command = new AuthorizeCommand(amount, address, emptyApartmentNumber, postalCode, proPayAccountNumber, creditCardNumber, cardExpirationDate, cvv, emptyInvNumber);
	    ProPayResult result = command.Execute();
	    return result;
	}

	public PaymentResult Charge(StringType proPayAccountNumber, CurrencyType amount, StringType cardNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType nameOnCard, StringType address, StringType postalCode) {
	    return Charge(proPayAccountNumber, amount, cardNumber, expirationYear, expirationMonth, cvv, nameOnCard, address, postalCode, StringType.DEFAULT);
	}
	public PaymentResult Charge(Spring2.Core.Types.StringType referenceNumber, Spring2.Core.Types.CurrencyType amount, Spring2.Core.Types.StringType accountNumber, Spring2.Core.Types.StringType expirationYear, Spring2.Core.Types.StringType expirationMonth, Spring2.Core.Types.StringType cvv, Spring2.Core.Types.StringType name, Spring2.Core.Types.StringType address, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType comment) {
	    StringType emptyApartmentNumber = StringType.DEFAULT; // apartment numbers will need to be part of the combined address
	    ProPayProviderConfiguration config = new ProPayProviderConfiguration();
	    StringType proPayAccountNumber = config.CorporateAccountNumber;
	    StringType cardExpirationDate = new StringType(expirationMonth.ToString() + expirationYear.ToString());
	    ChargeCardCommand command = new ChargeCardCommand(amount, address, emptyApartmentNumber, postalCode, proPayAccountNumber, accountNumber, cardExpirationDate, cvv, referenceNumber);
	    ProPayResult result = command.Execute();
	    return result;
	}

	public PaymentResult Credit(StringType referenceNumber, CurrencyType amount, StringType accountNumber, StringType expirationYear, StringType expirationMonth, StringType cvv, StringType name, StringType address, StringType postalCode, StringType comment, StringType originalTransactionId) {
	    throw new System.NotImplementedException();
	}
	
	public PaymentResult Refund(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    StringType proPayAccountNumber = referenceNumber;
	    StringType transactionNumber = originalTransactionId;
	    VoidChargeCommand command = new VoidChargeCommand(proPayAccountNumber, transactionNumber, amount);
	    ProPayResult result = command.Execute();
	    return result;
	}

	public PaymentResult Settle(StringType referenceNumber, CurrencyType amount, StringType originalTransactionId, CurrencyType originalTransactionAmount) {
	    StringType proPayAccountNumber = referenceNumber;
	    SettlementCommand command = new SettlementCommand(proPayAccountNumber, originalTransactionId);
	    ProPayResult result = command.Execute();
	    return result;
	}

    	public PaymentResult Void(StringType referenceNumber, StringType transactionId) {
	    return Refund(referenceNumber, CurrencyType.DEFAULT, transactionId, CurrencyType.DEFAULT);
	}

    }
}