using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment;
using Spring2.Core.Payment.EFSNet;

namespace Spring2.Core.Test {
    
    /// <summary>
    /// Provides unit tests for EFSNet payment provider class.
    /// </summary>
    [TestFixture()]
    public class EFSnetProviderTest {

	public static readonly String VALID_BILLING_ADDRESS = "1234 N. 56th St. Apt 789";
	public static readonly String VALID_BILLING_POSTAL_CODE = "12345-6789";
	public static readonly String VALID_CVV = "123";
	public static readonly String VALID_EXPIRATION_MONTH = "09";
	public static readonly String VALID_EXPIRATION_YEAR = "11";
	public static readonly String VALID_VISA_CARD_NUMBER = "4111111111111111";
    	
	protected Random random = null;

	public EFSnetProviderTest(){
	    random = new Random();
	}
	
	[Test]
	public void SystemCheck() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.SystemCheck();
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("ONLINE", result.ResultMessage);
	}

	#region Missing and invalid configuration
	[Test]
	public void MissingWebServiceUrl() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[EFSNetProviderConfiguration.WEBSERVICEURL_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		EFSNetProvider provider = new EFSNetProvider();
		try {
		    provider.SystemCheck();
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(EFSNetProviderConfiguration.WEBSERVICEURL_KEY) >= 0);
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
		config.Settings[EFSNetProviderConfiguration.STOREID_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		EFSNetProvider provider = new EFSNetProvider();
		try {
		    provider.SystemCheck();
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(EFSNetProviderConfiguration.STOREID_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingStoreKey() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[EFSNetProviderConfiguration.STOREKEY_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		EFSNetProvider provider = new EFSNetProvider();
		try {
		    provider.SystemCheck();
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(EFSNetProviderConfiguration.STOREKEY_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingApplicationId() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[EFSNetProviderConfiguration.APPLICATIONID_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		EFSNetProvider provider = new EFSNetProvider();
		try {
		    provider.SystemCheck();
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(EFSNetProviderConfiguration.APPLICATIONID_KEY) >= 0);
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
		config.Settings[EFSNetProviderConfiguration.WEBSERVICEURL_KEY] = "https://foo.bar/ws";
		ConfigurationProvider.SetProvider(config);
	    	
		EFSNetProvider provider = new EFSNetProvider();
		try {
		    provider.SystemCheck();
		    Assert.Fail("Should throw PaymentConnectionException");
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
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}

	[Test]
	public void Authorize() {
	    String referenceNumber = random.Next(99999999).ToString();
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Authorize(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}
    	
	[Test]
	public void Settle() {
	    String referenceNumber = random.Next(99999999).ToString();
	    CurrencyType amount = random.Next(999);
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Authorize(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
		
	    PaymentResult settleResult = provider.Settle(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
	    Assert.AreEqual(0, settleResult.ResponseCode);
	    Assert.AreEqual("00", settleResult.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Used", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}
    	
	[Test]
	public void Credit() {
	    String referenceNumber = random.Next(99999999).ToString();
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Credit(referenceNumber, random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here", "ignored original transactionId");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}
    
	[Test]
	public void RefundEntireAmount() {
	    String referenceNumber = random.Next(99999999).ToString();
	    CurrencyType amount = random.Next(999);
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);

	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);

	    PaymentResult settleResult = provider.Refund(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
	    Assert.AreEqual(0, settleResult.ResponseCode);
	    Assert.AreEqual("00", settleResult.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}

	[Test]
	public void RefundPartialAmount() {
	    String referenceNumber = random.Next(99999999).ToString();
	    CurrencyType amount = random.Next(999);
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);

	    PaymentResult settleResult = provider.Refund(result.ReferenceNumber, Decimal.Round(result.TransactionAmount.ToDecimal()/2,2), result.TransactionId, result.TransactionAmount);
	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
	    Assert.AreEqual(0, settleResult.ResponseCode);
	    Assert.AreEqual("00", settleResult.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
	    
	    transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}

	[Test]
	public void Void() {
	    String referenceNumber = random.Next(99999999).ToString();
	    CurrencyType amount = random.Next(999);
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(referenceNumber, amount, VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);

	    PaymentResult settleResult = provider.Void(result.ReferenceNumber, result.TransactionId);
	    Assert.AreEqual("APPROVED", settleResult.ResultMessage);
	    Assert.AreEqual(0, settleResult.ResponseCode);
	    Assert.AreEqual("00", settleResult.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Void", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}
    	
	[Test]
	public void QueryTransactions() {
	    EFSNetProvider provider = new EFSNetProvider();
	    QueryTransactionsResult results = provider.QueryTransactions();
	    Assert.AreNotEqual(0, results.Transaction.Length);
	    Assert.IsNotNull(results.Transaction[0].TransactionID);
	}
	#endregion

	#region Failure scenarios
	[Test]
	public void ChargeTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    QueryTransactionsResult results = provider.QueryTransactions();		
		
	    provider.Timeout = 250;
	    try {
		provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
		
	    provider.Timeout = EFSNetProvider.DEFAULT_TIMEOUT;
	    QueryTransactionsResult afterResults = provider.QueryTransactions();
	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
	    
	}

	[Test]
	public void SettleTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Authorize(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		provider.Timeout = 10;
		provider.Settle(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
	    
	    provider.Timeout = EFSNetProvider.DEFAULT_TIMEOUT;
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	}

	[Test]
	public void CreditTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    QueryTransactionsResult results = provider.QueryTransactions();		
		
	    provider.Timeout = 250;
	    try {
		provider.Credit(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here", "ignored original transactionId");
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
		
	    provider.Timeout = EFSNetProvider.DEFAULT_TIMEOUT;
	    QueryTransactionsResult afterResults = provider.QueryTransactions();
	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
	}
    	
	[Test]
	public void AuthorizeTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    QueryTransactionsResult results = provider.QueryTransactions();		
		
	    provider.Timeout = 250;
	    try {
		provider.Authorize(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
		
	    provider.Timeout = EFSNetProvider.DEFAULT_TIMEOUT;
	    QueryTransactionsResult afterResults = provider.QueryTransactions();
	    Assert.IsTrue(results.Transaction.Length == afterResults.Transaction.Length ||
		afterResults.Transaction[afterResults.Transaction.Length - 1].TransactionStatus == "Void");
	}
	
	[Test]
	public void RefundTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		provider.Timeout = 10;
		provider.Refund(result.ReferenceNumber, result.TransactionAmount, result.TransactionId, result.TransactionAmount);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
	    
	    provider.Timeout = EFSNetProvider.DEFAULT_TIMEOUT;
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	}
	
	[Test]
	public void VoidTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    try {
		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		provider.Timeout = 10;
		provider.Void(result.ReferenceNumber, result.TransactionId);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
	}

	[Test]
	public void GetTransactionTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    try {
		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		provider.Timeout = 10;
		provider.GetTransaction(result.TransactionId);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
	}
	
	[Test]
	public void QueryTransactionsTimeout() {
	    EFSNetProvider provider = new EFSNetProvider();
	    try {
		PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		provider.Timeout = 10;
		provider.QueryTransactions();
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    }
	}
	
	[Test]
	public void Declined() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999) + 0.56M, VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV + "0", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (PaymentFailureException) {
		// this is expected
	    }
	}

	#endregion
	
	#region AVS
	[Test]
	public void ValidAddress() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", "123", "Elmer Fudd", "1234 N. 56th St. Apt 789", "12345-6789", "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("X", result.AVSResponseCode);
	}

	[Test]
	public void InvalidAddressAndZip() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "987 Some Street", "98765", "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException ex) {
		// make sure that the transaction is voided
		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}

	[Test]
	public void ValidAddressInvalidZip() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, "98765", "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException ex) {
		// make sure that the transaction is voided
		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
    	
	[Test]
	public void Valid9DigitZipInvalidAddress() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "no address", VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException ex) {
		// make sure that the transaction is voided
		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
    	
	[Test]
	public void Valid5DigitZipInvalidAddress() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "no address", VALID_BILLING_POSTAL_CODE.Substring(0,5), "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (AvsValidationException ex) {
		// make sure that the transaction is voided
		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
	
	[Test]
	public void NoAddressNoZip() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", "", "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("N", result.AVSResponseCode);
	}
	
	[Test]
	public void NoAddressValid9DigitZip() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("W", result.AVSResponseCode);
	}
	
	[Test]
	public void NoAddressValid5DigitZip() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", "", VALID_BILLING_POSTAL_CODE.Substring(0,5), "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("Z", result.AVSResponseCode);
	}

	[Test]
	public void NoZipValidAddress() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, "", "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("A", result.AVSResponseCode);
	}
	#endregion
	
	// invalid address exception based on config value
	// invalid address for type for each card type (23 x 5)
    	
	#region CVV
	[Test]
	public void ValidCvv() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("M", result.CVVResponseCode);
	}
    	
	[Test]
	public void NoCvv() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, "", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual("", result.CVVResponseCode);
	}

	[Test]
	public void InvalidCvv() {
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Charge(random.Next(99999999).ToString(), random.Next(999), VALID_VISA_CARD_NUMBER, VALID_EXPIRATION_MONTH, VALID_EXPIRATION_YEAR, VALID_CVV + "0", "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
		Assert.Fail(result.ToString());
	    } catch (CvvValidationException ex) {
		// make sure that the transaction is voided
		QueryTransactionsResultTransaction transaction = provider.GetTransaction(ex.TransactionId);
		Assert.AreEqual("Void", transaction.TransactionStatus);
	    }
	}
	#endregion

	// invalid cvv based on config value
    	
	// valid date
	// invalid date
    	
	// TODO: null config values
    	
	[Test]
	public void ShouldChargeAmountGreaterThanOneThousand() {
	    String referenceNumber = random.Next(99999999).ToString();
	    EFSNetProvider provider = new EFSNetProvider();
	    PaymentResult result = provider.Charge(referenceNumber, random.Next(1000,99999), VALID_VISA_CARD_NUMBER, "09", "11", VALID_CVV, "Elmer Fudd", VALID_BILLING_ADDRESS, VALID_BILLING_POSTAL_CODE, "Kilroy was here");
	    Assert.AreEqual("APPROVED", result.ResultMessage);
	    Assert.AreEqual(0, result.ResponseCode);
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(referenceNumber, result.ReferenceNumber);
		
	    QueryTransactionsResultTransaction transaction = provider.GetTransaction(result.TransactionId);
	    Assert.AreEqual("Approved", transaction.TransactionStatus);
	    Assert.AreEqual(result.ReferenceNumber, transaction.ReferenceNumber);
	}

    	
    }
}
