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

	public PaymentResult PingAccount(StringType proPayAccountEmail, StringType proPayPayAccountNumber, StringType ssn) {
	    if (ssn.Length >= 4) {
		log.Info("ProPayProvider:Ping(" + proPayAccountEmail + ", " + "***-**-" + ssn.Substring(ssn.Length - 4) + ")");
	    } else {
		log.Info("ProPayProvider:Ping(" + proPayAccountEmail + ", " + ssn + ")");
	    }

	    StringType ssnWithoutDashes = ssn.Replace("-", "");

	    ProPayResult result = null;
	    try {
		AccountPingCommand command = new AccountPingCommand(proPayAccountEmail, ssnWithoutDashes);
		result = command.Execute();

		// verify the account number
		if ((( PaymentResult )result).AccountNumber != proPayPayAccountNumber.ToString()) {
		    throw new InvalidArgumentException("Invalid ProPay account number: " + proPayPayAccountNumber);
		}

		// verify the affiliation
		ProPayProviderConfiguration config = new ProPayProviderConfiguration();
		StringType affiliation = config.Affiliation;
		StringType resultAffiliation = result.GetResultValue("affiliation");
		if (affiliation.ToString() != resultAffiliation.ToString()) {
		    throw new InvalidArgumentException("ProPay account is not affiliated with '" + affiliation + "'");
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
	    log.Info("ProPayProvider:Split(" + sourceAccount + ", " + recipientAccount + ", " + amount + ", " + transactionNumber + ")");

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
	public PaymentResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber) {
	    ProPayProviderConfiguration config = new ProPayProviderConfiguration();
	    return Split(sourceAccount, config.CorporateAccountNumber, amount, transactionNumber);
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

	//public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster) {
	public PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster) {
	    log.Info("ProPayProvider:ChargeWithSplit(" + providerAccountNumber + ", " + totalAmount + ", " + commissionableAmount + ", " + address + ", " + postalCode + ", " + cardNumber + ", " + cardExpirationDate + ", " + cvv2 + ", " + invoiceNumber + ", " + splitFractionToMaster.ToString() + ")");

	    ProPayResult chargeWithSplitResult = null;
	    try {
		DecimalType splitAmount = Math.Ceiling((100.0d * commissionableAmount.ToDouble()) * splitFractionToMaster.ToDouble()) / 100.0d;
		splitAmount = splitAmount + (totalAmount - commissionableAmount).ToDecimal(); // the amount we split to the master is the master's portion of the split PLUS the difference between the totalAmount and commissionalbleAmount

		// make certain that split + fees is less than totalAmount
		DecimalType expectedFeeForTransaction = CalcChargeFee(totalAmount.ToDecimal());
		if ((splitAmount + Math.Ceiling(expectedFeeForTransaction.ToDouble())) > totalAmount.ToDecimal()) {
		    throw new InvalidArgumentException("Split amount is not allowed to exceed the total charge less fees");
		}

		PaymentResult chargeResult = Charge(providerAccountNumber, totalAmount, address, postalCode, cardNumber, cardExpirationDate, cvv2, invoiceNumber);
		try {
		    PaymentResult splitResult = Split(providerAccountNumber, splitAmount, chargeResult.TransactionId);
		    chargeWithSplitResult = ( ProPayResult )chargeResult; // notice, NOT splitResult!
		} catch (Exception ex) {
		    log.Error("Customer has been charged, but split to master account failed!!: " + ex.ToString());
		    if(ex is PaymentFailureException) {
			throw (( PaymentFailureException )ex);
		    }
		    throw ex;
		}
	    } catch (PaymentFailureException pex) {
		chargeWithSplitResult = ( ProPayResult )(pex.Result);
		log.Error(pex.Message + "\r\n" + chargeWithSplitResult.RawResponse);
		throw pex;
	    } catch (Exception ex) {
		log.Error(ex.Message);
		throw ex;
	    }
	    return chargeWithSplitResult;
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

	public CurrencyType GetBalance(StringType providerAccountNumber) {
	    CurrencyType accountBalance = 0.00;
	    BalanceRequestCommand command = new BalanceRequestCommand(providerAccountNumber);
	    ProPayResult balanceResult = command.Execute();
	    accountBalance = balanceResult.Amount;
	    return accountBalance;
	}

	// NOTE: for ease of reading comments and variables, a 75/25 split is assumed.  The code DOES NOT assume this, but rather uses the parameter 'originalFractionToSplit' (75% was siphoned)
	public PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, CurrencyType commissionableAmount, DecimalType originalFractionToSplit) {
	    log.Info("ProPayProvider:Refund(" + providerAccountNumber + ", " + originalTransactionId + ", " + refundAmount + ", " + commissionableAmount + ", " + originalFractionToSplit.ToString() + ")");

	    ProPayResult result = null;

	    try {
		bool isTransactionSettled = IsTransactionSettled(providerAccountNumber, originalTransactionId);
		if (isTransactionSettled) {

		    // adjust to pennies to figure the 75/25
		    Decimal refundAmountInPennies = refundAmount.ToDecimal() * 100M;
		    // Assuming 75/25 split, favoring the 75 on rounding
		    Decimal originalSplitAmountPreRounding = (commissionableAmount.ToDecimal() * (originalFractionToSplit.ToDecimal())) + (refundAmount.ToDecimal() - commissionableAmount.ToDecimal());
		    Decimal originalSplitAmountInPennies = originalSplitAmountPreRounding * 100;
		    Decimal roundedSplitAmountInPennies = (Decimal)Math.Ceiling((double)originalSplitAmountInPennies);
		    Decimal originalSplitAmount = roundedSplitAmountInPennies / 100.0M;

		    // verify that the funds will be available for the refund
		    CurrencyType merchantAccountBalance = GetBalance(providerAccountNumber);
		    if(merchantAccountBalance < refundAmount) {
			// not enough funds in the demonstrator's account, throw error
			throw new FundsUnavailableException( new CurrencyType(refundAmount.ToDouble() - merchantAccountBalance.ToDouble()) );
		    }

		    // first, push back the 75% to the merchant
		    ProPayResult splitRefundResult = null;
		    if (originalSplitAmount > 0.0M) {
			splitRefundResult = VoidSplit(providerAccountNumber, originalSplitAmount);
		    }

		    try {
			// then void the transaction
			VoidChargeCommand command = new VoidChargeCommand(providerAccountNumber, originalTransactionId, refundAmount);
			ProPayResult voidRefundResult = command.Execute();
			result = voidRefundResult;
		    } catch (Exception ex) {
			log.Error("Funds have been transferred back to the merchant from the master account, but refund to customer FAILED!!: " + ex.ToString());
			if (ex is PaymentFailureException) {
			    throw (( PaymentFailureException )ex);
			}
			throw ex;
		    }
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

	public StringType GetTransactionStatus(StringType accountNumber, StringType transactionNumber) {
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
	    StringType transactionStatus = GetTransactionStatus(accountNumber, transactionId);
	    return transactionStatus.IsValid && transactionStatus.EndsWith("Settled");
	}

	private String StripSlashFromExpirationDate(String originalExpirationDate) {
	    String result = originalExpirationDate.Replace("/","");
	    return result;
	}

	private DecimalType CalcChargeFee(DecimalType totalChargeAmount) {
	    ProPayProviderConfiguration config = new ProPayProviderConfiguration();
	    Decimal minimumCharge = config.MinimumChargeFee;
	    Decimal calculatedTotalCharge = 0.0M;
	    Char[] splitChars = {','};
	    String[] feeRules = config.ChargeFees.Split(splitChars);
	    foreach (String feeRule in feeRules) {
		if(feeRule.IndexOf('%') != -1) { // String.Contains does not exist in earlier .NET versions
		    String cleanedFeeRule = feeRule.Replace("%","").Trim();
		    Decimal percentageFee = Decimal.Parse(cleanedFeeRule) * 0.01M;
		    calculatedTotalCharge += totalChargeAmount.ToDecimal() * percentageFee;
		} else if (feeRule.IndexOf('$') != -1) { // String.Contains does not exist in earlier .NET versions
		    String cleanedFeeRule = feeRule.Replace("$", "").Trim();
		    Decimal flatFee = Decimal.Parse(cleanedFeeRule);
		    calculatedTotalCharge += flatFee;
		} else {
		    throw new InvalidValueException("Fee Rules must include either a % or a $ to specify whether the fee is percentage or flat based");
		}
	    }
	    DecimalType expectedFee = Math.Max(minimumCharge, calculatedTotalCharge);
	    return expectedFee;
	}

    }
}