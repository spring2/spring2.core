using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Dss.Payment;
using Spring2.Dss.Payment.Moneris;

namespace Spring2.Dss.Test {
    
    /// <summary>
    /// Provides unit tests for Moneris payment provider class.
    /// </summary>
    [TestFixture()]
    public class MonerisProviderTest {

	public static readonly String VALID_BILLING_ADDRESS = "1234 N. 56th St. Apt 789";
	public static readonly String VALID_BILLING_POSTAL_CODE = "12345-6789";
	public static readonly String VALID_CVV = "123";
	public static readonly String VALID_EXPIRATION_MONTH = "09";
	public static readonly String VALID_EXPIRATION_YEAR = "11";
	public static readonly String VALID_VISA_CARD_NUMBER = "4242424242424242";
    	public static readonly String VALID_MASTERCARD_CARD_NUMBER = "5454545454545454";
    	public static readonly String VALID_AMEX_CARD_NUMBER = "373599005095005";
    	public static readonly String VALID_DINERS_CARD_NUMBER = "36462462742008";
    	
	protected Random random = null;

	public MonerisProviderTest(){
	    random = new Random();
	}
	
	#region Missing and invalid configuration
	[Test]
	public void MissingHost() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[MonerisProviderConfiguration.HOST_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		MonerisProvider provider = new MonerisProvider();
		try {
		    String referenceNumber = random.Next(99999999).ToString();
		    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(MonerisProviderConfiguration.HOST_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingStoreId() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[MonerisProviderConfiguration.STOREID_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		MonerisProvider provider = new MonerisProvider();
		try {
		    String referenceNumber = random.Next(99999999).ToString();
		    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(MonerisProviderConfiguration.STOREID_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingApiToken() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[MonerisProviderConfiguration.APITOKEN_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		MonerisProvider provider = new MonerisProvider();
		try {
		    String referenceNumber = random.Next(99999999).ToString();
		    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(MonerisProviderConfiguration.APITOKEN_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingCrypt() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[MonerisProviderConfiguration.CRYPT_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		MonerisProvider provider = new MonerisProvider();
		try {
		    String referenceNumber = random.Next(99999999).ToString();
		    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(MonerisProviderConfiguration.CRYPT_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

    	[Test]
	public void InvalidUrl() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[MonerisProviderConfiguration.HOST_KEY] = "foo.bar.ws";
		ConfigurationProvider.SetProvider(config);
	    	
		MonerisProvider provider = new MonerisProvider();
		try {
		    String referenceNumber = random.Next(99999999).ToString();
		    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (AssertionException) {
		    throw;
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConnectionException);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}
	#endregion
    	
	#region Success scenarios
	[Test]
	public void Charge() {
	    String referenceNumber = random.Next(99999999).ToString();
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = provider.Charge(referenceNumber, 10.41, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    //Assert.IsTrue(result.ResultMessage.StartsWith("APPROVED"));
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
	}

//	[Test]
//	public void Authorize() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Authorize(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
//    	
//	[Test]
//	public void Settle() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    CurrencyType amount = random.Next(999);
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Authorize(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//		
//	    PaymentResult settleResult = provider.Settle(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
//	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
//	    Assert.AreEqual(0, settleResult.ResponseCode);
//	    Assert.AreEqual("00", settleResult.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Used", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
    	
	[Test]
	public void Credit() {
	    String referenceNumber = random.Next(99999999).ToString();
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = provider.Charge(referenceNumber, 10.41, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");		
	    Assert.AreEqual("APPROVED", result.ResultMessage);
		
	    PaymentResult creditResult = provider.Credit(referenceNumber, 10.41, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here", result.TransactionId);
	    Assert.AreEqual("APPROVED", creditResult.ResultMessage);
	    Assert.AreEqual(27, creditResult.ResponseCode);
	    Assert.AreEqual("027", creditResult.ResultCode);
	    Assert.AreEqual(referenceNumber, creditResult.ReferenceNumber);
	}
    
//	[Test]
//	public void RefundEntireAmount() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    CurrencyType amount = random.Next(999);
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//
//	    PaymentResult settleResult = provider.Refund(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
//	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
//	    Assert.AreEqual(0, settleResult.ResponseCode);
//	    Assert.AreEqual("00", settleResult.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
//
//	[Test]
//	public void RefundPartialAmount() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    CurrencyType amount = random.Next(999);
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//
//	    PaymentResult settleResult = provider.Refund(result.ReferenceNumber, Decimal.Round(result.TransactionAmount.ToDecimal()/2,2), result.TransactionId, result.TransactionAmount);
//	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
//	    Assert.AreEqual(0, settleResult.ResponseCode);
//	    Assert.AreEqual("00", settleResult.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//	    
//	    transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
//
//	[Test]
//	public void Void() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    CurrencyType amount = random.Next(999);
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//
//	    PaymentResult settleResult = provider.Void(result.TransactionId);
//	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
//	    Assert.AreEqual(0, settleResult.ResponseCode);
//	    Assert.AreEqual("00", settleResult.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Void", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
//    	
//	[Test]
//	public void QueryTransactions() {
//	    MonerisProvider provider = new MonerisProvider();
//	    QueryTransactionsResult results = provider.QueryTransactions();
//	    Assert.AreNotEqual(0, results.Transaction.Length);
//	    Assert.IsNotNull(results.Transaction[0].TransactionID);
//	}
	#endregion
//
//	#region Failure scenarios
//	[Test]
//	public void ChargeTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    QueryTransactionsResult results = provider.QueryTransactions();		
//		
//	    provider.Timeout = 250;
//	    try {
//		provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//		
//	    provider.Timeout = MonerisProvider.DEFAULT_TIMEOUT;
//	    QueryTransactionsResult afterResults = provider.QueryTransactions();
//	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
//		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
//	    
//	}
//
//	[Test]
//	public void SettleTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = null;
//	    try {
//		result = provider.Authorize(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		provider.Timeout = 10;
//		provider.Settle(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//	    
//	    provider.Timeout = MonerisProvider.DEFAULT_TIMEOUT;
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	}
//
//	[Test]
//	public void CreditTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    QueryTransactionsResult results = provider.QueryTransactions();		
//		
//	    provider.Timeout = 250;
//	    try {
//		provider.Credit(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//		
//	    provider.Timeout = MonerisProvider.DEFAULT_TIMEOUT;
//	    QueryTransactionsResult afterResults = provider.QueryTransactions();
//	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
//		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
//	}
//    	
//	[Test]
//	public void AuthorizeTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    QueryTransactionsResult results = provider.QueryTransactions();		
//		
//	    provider.Timeout = 250;
//	    try {
//		provider.Authorize(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//		
//	    provider.Timeout = MonerisProvider.DEFAULT_TIMEOUT;
//	    QueryTransactionsResult afterResults = provider.QueryTransactions();
//	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
//		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
//	}
//	
//	[Test]
//	public void RefundTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = null;
//	    try {
//		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		provider.Timeout = 10;
//		provider.Refund(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//	    
//	    provider.Timeout = MonerisProvider.DEFAULT_TIMEOUT;
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	}
//	
//	[Test]
//	public void VoidTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    try {
//		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		provider.Timeout = 10;
//		provider.Void(result.TransactionId);
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//	}
//
//	[Test]
//	public void GetTransactionTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    try {
//		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		provider.Timeout = 10;
//		provider.GetTransaction(result.TransactionId);
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//	}
//	
//	[Test]
//	public void QueryTransactionsTimeout() {
//	    MonerisProvider provider = new MonerisProvider();
//	    try {
//		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		provider.Timeout = 10;
//		provider.QueryTransactions();
//		Assert.Fail();
//	    } catch (PaymentConnectionException) {
//		// this is expected
//	    }
//	}
//	
//	[Test]
//	public void Declined() {
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = null;
//	    try {
//		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999) + 0.56M, VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV + "0", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//		Assert.Fail(result.ToString());
//	    } catch (PaymentFailureException) {
//		// this is expected
//	    }
//	}
//
//	#endregion
//	
	#region AVS
	[Test]
	public void ValidAddress() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), 10.41, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("X", result.AVSResponseCode);
	}

    	[Test]
	public void InvalidAddressAndZip() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException) {
		// make sure that the transaction is voided
//		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
//		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}

	[Test]
	public void ValidAddressInvalidZip() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException) {
		// make sure that the transaction is voided
//		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
//		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
    	
	[Test]
	public void Valid9DigitZipInvalidAddress() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException) {
		// make sure that the transaction is voided
//		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
//		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
    	
	[Test]
	public void Valid5DigitZipInvalidAddress() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE.Substring(0,5), "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException) {
		// make sure that the transaction is voided
//		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
//		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
	
	[Test]
	[Ignore("Not sure how to simulate this")]
	public void NoAddressNoZip() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", "", "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("N", result.AVSResponseCode);
	}
	
	[Test]
	public void NoAddressValid9DigitZip() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("W", result.AVSResponseCode);
	}
	
	[Test]
	public void NoAddressValid5DigitZip() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", VALID_BILLING_POSTAL_CODE.Substring(0,5), "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("Z", result.AVSResponseCode);
	}

	[Test]
	public void NoZipValidAddress() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), 10.40, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, "", "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("A", result.AVSResponseCode);
	}
	#endregion
	
//	// invalid address exception based on config value
//	// invalid address for type for each card type (23 x 5)
    	
	#region CVV
	[Test]
	public void ValidCvv() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), 10.41, VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("1M", result.CVVResponseCode);
	}
    	
	[Test]
	public void NoCvv() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), 10.30, VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, "", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(27, result.ResponseCode);
	    Assert.AreEqual("027", result.ResultCode);
	    Assert.AreEqual("0P", result.CVVResponseCode);
	}

	[Test]
	public void InvalidCvv() {
	    MonerisProvider provider = new MonerisProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), 10.32, VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV + "0", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (CvvValidationException) {
		// make sure that the transaction is voided
	    }
	}
	#endregion

//	// invalid cvv based on config value
//    	
//	// valid date
//	// invalid date
//    	
//	// TODO: null config values
//    	
//	[Test]
//	public void ShouldChargeAmountGreaterThanOneThousand() {
//	    String referenceNumber = random.Next(99999999).ToString();
//	    MonerisProvider provider = new MonerisProvider();
//	    PaymentResult result = provider.Charge(referenceNumber, random.Next(1000,99999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
//	    Assert.AreEqual("APPROVED", result.ResultMessage);
//	    Assert.AreEqual(0, result.ResponseCode);
//	    Assert.AreEqual("00", result.ResultCode);
//	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
//		
//	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
//	    Assert.AreEqual("Approved", transaction.TransactionStatus);
//	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
//	}
//
    	
    }
}
