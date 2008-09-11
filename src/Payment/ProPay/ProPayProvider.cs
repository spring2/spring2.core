using System;
using System.Reflection;
using System.Text;
using log4net;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment;


namespace Spring2.Core.Payment.ProPay {

    public class ProPayProvider : ISplitPaymentProvider {
    	
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	// signs up a new account
	public ProPayResult SignupAccount(StringType country, StringType email, StringType firstName, StringType mi, StringType lastName,
				     StringType address, StringType apartmentNumber, StringType city, StringType state, StringType postalCode,
				     PhoneNumberType dayPhone, PhoneNumberType eveningPhone, StringType ssn, DateTimeType birthday,
				     StringType creditCardNumber, StringType creditCardExpiration) {
	    ProPayResult result = null;
	    creditCardExpiration = StripSlashFromExpirationDate(creditCardExpiration);
	    try {
		MerchantSignupCommand command = new MerchantSignupCommand(country, email, firstName, mi, lastName, address, apartmentNumber,
									    city, state, postalCode, dayPhone, eveningPhone, ssn,
									    birthday, creditCardNumber, creditCardExpiration);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = (ProPayResult)(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public ProPayResult Ping(StringType proPayAccountEmail, StringType ssn) {
	    log.Info("ProPayProvider:Ping(" + proPayAccountEmail + ", " + "***-**-" + ssn.Substring(ssn.Length-4) + ")");

	    ProPayResult result = null;
	    try {
		AccountPingCommand command = new AccountPingCommand(proPayAccountEmail, ssn);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public StringTypeList GetUserSignupIds(ProPayResult userReportResult) {
	    return GetUserSignupValues(userReportResult, "userID");
	}

	public StringTypeList GetUserSignupEmails(ProPayResult userReportResult) {
	    return GetUserSignupValues(userReportResult, "sourceEmail");
	}

	public StringTypeList GetUserSignupAccountNumbers(ProPayResult userReportResult) {
	    return GetUserSignupValues(userReportResult, "accountNum");
	}

	protected StringTypeList GetUserSignupValues(ProPayResult userReportResult, StringType xmlElement) {
	    return userReportResult.GetResultValues(xmlElement);
	}

	// tells ProPay that part of the transaction (specified by transactionNumber) goes to recipientAccount
	public PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber) {
	    log.Info("ProPayProvider:VoidSplit(" + sourceAccount + ", " + recipientAccount + ", " + amount + ", " + transactionNumber + ")");

	    ProPayResult result = null;
	    try {
		SplitCommand command = new SplitCommand(sourceAccount, recipientAccount, amount, transactionNumber);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}
	public ProPayResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber) {
	    ProPayProviderConfiguration config = new ProPayProviderConfiguration();
	    return (ProPayResult) Split(sourceAccount, config.CorporateAccountNumber, amount, transactionNumber);
	}

	// In a higher level sense, all this method does is push money from master account to the merchant
	public PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber) {
	    log.Info("ProPayProvider:VoidSplit(" + recipientAccount+ ", " + amount + ", " + invoiceNumber + ")");

	    ProPayResult result = null;
	    try {
		FromMasterAccountToMerchantCommand command = new FromMasterAccountToMerchantCommand(recipientAccount, amount, invoiceNumber);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}
	public ProPayResult VoidSplit(StringType recipientAccount, CurrencyType amount) {
	    return (ProPayResult) VoidSplit(recipientAccount, amount, StringType.DEFAULT);
	}

	// This will max out at 500 results
	public ProPayResult GetSignupReport(DateType startDate, DateType endDate) {
	    log.Info("ProPayProvider:GetSignupReport(" + startDate + ", " + endDate + ")");

	    ProPayResult result = null;
	    try {
		GetSignupsCommand command = new GetSignupsCommand(startDate, endDate);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public StringType GetSignupReportAsXml(DateType startDate, DateType endDate) {
	    ProPayResult result = GetSignupReport(startDate, endDate);
	    return result.RawResponse;
	}

	public PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    log.Info("ProPayProvider:Authorize(" + amount + ", " + address + ", " + postalCode + ", " + providerAccountNumber + ", " + creditCardNumber + ", " + cardExpirationDate + ", " + cvv2 + ", " + invoiceNumber + ")");

	    ProPayResult result = null;
	    cardExpirationDate = StripSlashFromExpirationDate(cardExpirationDate);
	    try {
		StringType emptyApartmentNumber = StringType.DEFAULT;
		AuthorizeCommand command = new AuthorizeCommand(amount, address, emptyApartmentNumber, postalCode, providerAccountNumber, creditCardNumber, cardExpirationDate, cvv2, invoiceNumber);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber) {
	    log.Info("ProPayProvider:Charge(" + providerAccountNumber + ", " + amount+ ", " + address + ", " + postalCode + ", " + cardNumber + ", " + cardExpirationDate + ", " + cvv2 + ", " + invoiceNumber + ")");

	    ProPayResult result = null;
	    cardExpirationDate = StripSlashFromExpirationDate(cardExpirationDate);
	    try {
		StringType emptyApartmentNumber = StringType.DEFAULT;
		ChargeCardCommand command = new ChargeCardCommand(providerAccountNumber, amount, address, emptyApartmentNumber, postalCode, cardNumber, cardExpirationDate, cvv2, invoiceNumber);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber) {
	    //return Refund(providerAccountNumber, originalTransactionId, amount);

	    log.Info("ProPayProvider:Credit(" + providerAccountNumber + ", " + amount + ", " + invoiceNumber + ")");

	    ProPayResult result = null;
	    try {
		FromMasterAccountToMerchantCommand command = new FromMasterAccountToMerchantCommand(providerAccountNumber, amount, invoiceNumber);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}
	
	// This one automatically assumes that there has been a 75/25 split, checks the transaction status, and voids/moves-money as necessary
	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount) {
	    log.Info("ProPayProvider:Refund(" + providerAccountNumber + ", " + originalTransactionId + ", " + refundAmount + ")");

	    ProPayResult result = null;

	    try {
		bool assume_75_25_split = true;
		bool isTransactionSettled = IsTransactionSettled(providerAccountNumber, originalTransactionId);
		if (assume_75_25_split && isTransactionSettled) {
		    // adjust to pennies to figure the 75/25
		    refundAmount *= 100M;
		    // Assuming 75/25 split, favoring the 75 on rounding
		    double doubleRefundAmount = refundAmount.ToDouble();
		    Decimal refundFor75InPennies = System.Convert.ToDecimal(Math.Floor(doubleRefundAmount * 0.75d));
		    Decimal refundFor25InPennies = refundAmount.ToDecimal() - refundFor75InPennies;
		    Decimal refundFor75InDollars = refundFor75InPennies / 100M;
		    Decimal refundFor25InDollars = refundFor25InPennies / 100M;

		    // first, push back the 75% to the merchant
		    ProPayResult splitRefundResult = VoidSplit(providerAccountNumber, refundFor75InDollars);
		    // then void the transaction
		    VoidChargeCommand command = new VoidChargeCommand(providerAccountNumber, originalTransactionId, refundAmount);
		    result = command.Execute();
		} else {
		    VoidChargeCommand command = new VoidChargeCommand(providerAccountNumber, originalTransactionId, refundAmount);
		    result = command.Execute();
		}
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }

	    return result;
	}

	public PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId) {
	    log.Info("ProPayProvider:Settle(" + providerAccountNumber + ", " + originalTransactionId + ")");

	    ProPayResult result = null;
	    try {
		SettlementCommand command = new SettlementCommand(providerAccountNumber, originalTransactionId);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public PaymentResult Void(StringType providerAccountNumber, StringType transactionId, CurrencyType amount) {
	    log.Info("ProPayProvider:Void(" + providerAccountNumber + ", " + transactionId + ", " + amount + ")");

	    ProPayResult result = null;
	    try {
		VoidChargeCommand command = new VoidChargeCommand(providerAccountNumber, transactionId, amount);
		result = command.Execute();
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return result;
	}

	public String TransactionStatus(StringType accountNumber, StringType transactionNumber) {
	    /*Possible Transaction states:
		InvalidTransaction 
		CCDebitAuthorized
		CCDebitPending
		CCDebitSettled
		CCDebitFunded
		CCDebitDeclined
		CCDebitVoided
		CCDebitChargedBack
		CCDebitRepresented
		CCCreditAuth
		CCCreditSettled
		PPCreditSimple
		PPCreditTimedPullPending
		PPCreditTimedPullFunded
		PPCreditSpendBack
		PPDebitSimple
		PPDebitTimedPullPending
		PPDebitTimedPullFunded
		PPDebitSpendBack
		Other
	     */

	    log.Info("ProPayProvider:TransactionStatus(" + accountNumber + ", " + transactionNumber + ")");

	    String transactionStatus = String.Empty;
	    ProPayResult result = null;
	    try {
		TransactionStatusCommand command = new TransactionStatusCommand(accountNumber, transactionNumber);
		result = command.Execute();
		transactionStatus = result.GetResultValue("txnStatus");
	    } catch (PaymentFailureException pex) {
		result = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + result.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return transactionStatus;
	}

	public bool IsTransactionSettled(StringType accountNumber, StringType transactionId) {
	    StringType transactionStatus = TransactionStatus(accountNumber, transactionId);
	    return transactionStatus.IsValid && transactionStatus.EndsWith("Settled");
	}

	private String StripSlashFromExpirationDate(String originalExpirationDate) {
	    String result = originalExpirationDate.Replace("/","");
	    return result;
	}

    }
}