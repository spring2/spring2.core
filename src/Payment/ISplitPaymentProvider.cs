﻿using Spring2.Core.Types;

namespace Spring2.Core.Payment {

    // This interface is highly similar to IPaymentProvider, but that interface was lacking certain aspects that were required to communicate via a provider that needs information on how to move money around in a split transaction
    public interface ISplitPaymentProvider {
	PaymentResult Authorize(CurrencyType amount, StringType address, StringType postalCode, StringType providerAccountNumber, StringType creditCardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber);

	PaymentResult Charge(StringType providerAccountNumber, CurrencyType amount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber);

	PaymentResult ChargeWithSplit(StringType providerAccountNumber, CurrencyType totalAmount, CurrencyType commissionableAmount, StringType address, StringType postalCode, StringType cardNumber, StringType cardExpirationDate, StringType cvv2, StringType invoiceNumber, DecimalType splitFractionToMaster);

	PaymentResult Credit(StringType providerAccountNumber, CurrencyType amount, StringType invoiceNumber);

	PaymentResult Refund(StringType providerAccountNumber, StringType originalTransactionId, CurrencyType refundAmount, CurrencyType commissionableAmount, DecimalType originalFractionToSplit);

	PaymentResult Settle(StringType providerAccountNumber, StringType originalTransactionId);

	PaymentResult Void(StringType referenceNumber, StringType transactionId, CurrencyType amount);

	PaymentResult Split(StringType sourceAccount, CurrencyType amount, StringType transactionNumber);
	PaymentResult Split(StringType sourceAccount, StringType recipientAccount, CurrencyType amount, StringType transactionNumber);

	PaymentResult VoidSplit(StringType recipientAccount, CurrencyType amount, StringType invoiceNumber);

	PaymentResult PingAccount(StringType proPayAccountEmail, StringType proPayPayAccountNumber, StringType ssn);

	CurrencyType GetBalance(StringType providerAccountNumber);

	StringType GetTransactionStatus(StringType accountNumber, StringType transactionNumber);
    }

}