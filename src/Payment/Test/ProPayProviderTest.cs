using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Spring2.Dss.Payment;
using Spring2.Core.Types;
using Spring2.Core.Payment;
using Spring2.Core.Payment.ProPay;
using Spring2.Core.Configuration;

namespace Spring2.Core.Test {
    /// <summary>
    /// Summary description for ProPayProviderTest.
    /// </summary>
    [TestFixture]
    public class ProPayProviderTest {

	// valid test card data
	private const String testCardNumber = "4747474747474747"; // This is the only card number accepted by the ProPay test server
	private const String testCardCVV = "999"; // This is the only CVV2 that will not decline
	private const String testCardExpMonth = "12"; // just making this up
	private const String testCardExpYear  = "13"; // just making this up
	private const String testCardholderName = "Fred"; // just making this up
	private const String testCardholderAddress = "FredBurger"; // just making this up
	private const String testCardholderPostalCode = "83204"; // "AVS -> "Address Match" (A)

	private const String bogusCardNumber = "1234567890123456"; 

	private const String masterAccountEmail  = "commissions@uppercaseliving.com";
	private const String masterAccountNumber = "1036174";

	private const String testAccountEmail  = "test1@uppercaseliving.com";
	private const String testAccountNumber = "1036173";

	private CurrencyType declineAmount = new CurrencyType(113); // the test system will automatically fail with this value, as per the ProPay documentation
	private const String invalidMasterAccount = "1122345"; // just making this up

	public ProPayProviderTest() {
	}

	#region Success charge scenario for all card types
	[Test()]
	public void SuccessfulPayment() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
	#endregion
    	
	#region Failure scenarios
	[Test()]
	[Ignore("The apparent limit of $20 mentioned in the documentation appears to be incorrect")]
	public void OverLimitPayment() {
	    // Documentation says that the test system has a value of $20 maximum
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(35.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("FINDME", result, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
    	
	[Test]
	public void InvalidCreditCardNumber() {
	    ProPayProvider provider = new ProPayProvider();
	    try {
		PaymentResult result = provider.Charge("orderId-" + DateTime.Now.Ticks, new CurrencyType(5.00), bogusCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		Assert.Fail();
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("48", ex.Result.ResultCode, "Invalid ccNum");
	    }
	}

	[Test]
	public void Timeout() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings["ProPay.Timeout"] = "1";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		Assert.Fail();
	    } catch (WebException) {
		// this is expected
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }	
	}
	#endregion
    	
	#region AVS
	[Test()]
	public void ValidAvsVerification1() {
	    ProPayProvider provider = new ProPayProvider();
	    StringType zipMatchValue = testCardholderPostalCode; // by ProPay docs, this should return 'A', Address Match
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("A", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification2() {
	    StringType zipMatchValue = new StringType("85284"); // by ProPay docs, this should return 'Z', Zip Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("Z", result.AVSResponseCode, "Address zip should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification3() {
	    StringType zipMatchValue = new StringType("832085284"); // by ProPay docs, this should return 'Y', Exact Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("Y", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification4() {
	    StringType zipMatchValue = new StringType("999970001"); // by ProPay docs, this should return 'B', Address Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("B", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification5() {
	    StringType zipMatchValue = new StringType("999970003"); // by ProPay docs, this should return 'D', Exact Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("D", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification6() {
	    StringType zipMatchValue = new StringType("999970005"); // by ProPay docs, this should return 'M', Exact Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("M", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification7() {
	    StringType zipMatchValue = new StringType("999970006"); // by ProPay docs, this should return 'P', Zip Match
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("P", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification1() {
	    StringType zipMatchValue = new StringType("99994"); // by ProPay docs, this should return 'U', Ver Unavailable
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("U", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification2() {
	    StringType zipMatchValue = new StringType("99998"); // by ProPay docs, this should return 'G', Ver Unavailable
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("G", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification3() {
	    StringType zipMatchValue = new StringType("999970002"); // by ProPay docs, this should return 'C', Serv Unavailable
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("C", result.AVSResponseCode, "Serv unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification4() {
	    StringType zipMatchValue = new StringType("999970004"); // by ProPay docs, this should return 'I', Ver Unavailable
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("I", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification5() {
	    StringType zipMatchValue = new StringType("999970009"); // by ProPay docs, this should return 'S', Service Not supported
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("S", result.AVSResponseCode, "Service Not supported");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification6() {
	    StringType zipMatchValue = new StringType("999970010"); // by ProPay docs, this should return 'R', Issuer system unavailable
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    Assert.AreEqual("R", result.AVSResponseCode, "Issuer system unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void GarbageAvsVerification() {
	    StringType zipMatchValue = new StringType("8320 85284"); // something about the space causes error 32, even on their testing setup, which is fine because it gives us another thing we can test
	    ProPayProvider provider = new ProPayProvider();
	    bool paymentFailureExceptionThrown = false;
	    PaymentResult result = null;
	    try {
		result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, zipMatchValue, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		paymentFailureExceptionThrown = true;
		result = ex.Result;
	    }
	    Assert.IsNotNull(result);
	    Assert.IsTrue(paymentFailureExceptionThrown);
	    Assert.AreEqual(result.ResultCode, "32", "Invalid billZip"); 
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	#endregion
    	
	#region CVV
	[Test()]
	public void ValidCvv2Verification() {
	    StringType cvvMatchValue = testCardCVV;
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, cvvMatchValue, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("M", result.CVVResponseCode, "CVV2 should be valid.");
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidCvv);
	}

	[Test()]
	public void InvalidCvv2Verification1() {
	    StringType cvvMatchValue = new StringType("888"); // ProPay testing server only accepts '999' for successful transactions
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = null;
	    bool paymentFailureExceptionThrown = false;
	    try {
		provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, cvvMatchValue, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		paymentFailureExceptionThrown = true;
		result = ex.Result;
	    }
	    Assert.IsNotNull(result);
	    Assert.IsTrue(paymentFailureExceptionThrown);
	    Assert.AreEqual("N", result.CVVResponseCode, "CVV2 should not be valid.");
	    Assert.AreEqual("58", result.ResultCode); // Credit card declined
	    Assert.AreEqual(BooleanType.FALSE, result.ValidCvv);
	}

	[Test()]
	public void InvalidCvv2Verification2() {
	    StringType cvvMatchValue = new StringType("88 78"); // ProPay testing server only accepts '999' for successful transactions
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = null;
	    bool paymentFailureExceptionThrown = false;
	    try {
		provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, cvvMatchValue, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		paymentFailureExceptionThrown = true;
		result = ex.Result;
	    }
	    Assert.IsNotNull(result);
	    Assert.IsTrue(paymentFailureExceptionThrown);
	    Assert.AreEqual("50", result.ResultCode); // Invalid cvv2
	    Assert.AreEqual(BooleanType.FALSE, result.ValidCvv);
	}

	#endregion		

	#region Primary API Scenarios
	[Test]
    	public void Charge() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", result.ResultCode);
	}
    
	[Test]
	public void Authorize() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult result = null;
	    try {
		result = provider.Authorize(masterAccountNumber, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch(Exception ex) {
		String errorMessage = ex.Message;
		Assert.Fail();
	    }
	    if (result != null) {
		Assert.AreEqual("00", result.ResultCode);
	    } else {
		Assert.Fail();
	    }
	}
		
	[Test]
	public void Refund() {
	    ProPayProvider provider = new ProPayProvider();
	    CurrencyType originalTransactionAmount = new CurrencyType(5.00);
	    PaymentResult chargeResult = provider.Charge("orderId-"+DateTime.Now.Ticks, originalTransactionAmount, testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", chargeResult.ResultCode);
	    PaymentResult refundResult = provider.Refund(masterAccountNumber, new CurrencyType(2.00), chargeResult.TransactionId, originalTransactionAmount);
	    Assert.AreEqual("00", refundResult.ResultCode);
	}
		
	[Test]
	public void Credit() {
	    // not implemented 
	}
	
	[Test]
	public void Settle() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult authorizeResult = provider.Authorize(masterAccountNumber, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", authorizeResult.ResultCode);
	    PaymentResult settlementResult = provider.Settle(masterAccountNumber, CurrencyType.UNSET, authorizeResult.TransactionId, CurrencyType.UNSET);
	    Assert.AreEqual("00", settlementResult.ResultCode);
	}

	[Test]
	public void Split() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult splitResult = null;
	    try {
		splitResult = provider.Split(new CurrencyType(1.50), testAccountNumber); // the account was initially seeded with $100K, so that should last a very long time
		Assert.AreEqual("00", splitResult.ResultCode);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("63", ex.Result.ResultCode, "In the case of failure here, verify that it is due to 'insufficient funds' in the master account");
	    }
	}

	[Test()]
	public void ShouldHandleAmpersandInCommandText() {
	    ChargeCardCommand command = new ChargeCardCommand(new CurrencyType(5.00), "Corner of 5th & Vine", "", testCardholderPostalCode, testAccountNumber, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "");
	    Assert.IsTrue(command.CommandText.Length > 0);

	    String commandText = command.CommandText;
	   	
   	    // verify
   	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("<addr>") + 20, 5),"&amp;");
	}

	#endregion

	#region Secondary API Scenarios
	public void UseBadMasterAccount() {
	    ProPayProvider provider = new ProPayProvider();
	    try {
		provider.Charge(invalidMasterAccount, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("47", ex.Result.ResultCode, "Invalid accntNum");
	    }
	}

	public void ChargeWithDecline() {
	    ProPayProvider provider = new ProPayProvider();
	    try {
		provider.Charge("orderId-"+DateTime.Now.Ticks, declineAmount, testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		Assert.Fail("ProPay documentation says this amount should fail, so why are we here?");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("58", ex.Result.ResultCode, "Credit card declined");
	    }
	}

	public void UsePastExpDate() {
	    ProPayProvider provider = new ProPayProvider();
	    try {
		provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, "01", testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		Assert.Fail("Both 1901 and 2001 were a long time ago");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("49", ex.Result.ResultCode, "Invalid expDate");
	    }
	}

	/*
	[Test]
	public void AuthorizeWithBadMasterAccount() {
	    ProPayProvider provider = new ProPayProvider();
	    try {
		provider.Authorize(invalidMasterAccount, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("47", ex.Result.ResultCode, "Invalid accntNum");
	    }
	}

	[Test]
	public void Refund() {
	    ProPayProvider provider = new ProPayProvider();
	    CurrencyType originalTransactionAmount = new CurrencyType(5.00);
	    PaymentResult chargeResult = provider.Charge("orderId-"+DateTime.Now.Ticks, originalTransactionAmount, testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", chargeResult.ResultCode);
	    PaymentResult refundResult = provider.Refund(masterAccountNumber, new CurrencyType(2.00), chargeResult.TransactionId, originalTransactionAmount);
	    Assert.AreEqual("00", refundResult.ResultCode);
	}

	[Test]
	public void Credit() {
	    // not implemented 
	}

	[Test]
	public void Settle() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult authorizeResult = provider.Authorize(masterAccountNumber, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", authorizeResult.ResultCode);
	    PaymentResult settlementResult = provider.Settle(masterAccountNumber, CurrencyType.UNSET, authorizeResult.TransactionId, CurrencyType.UNSET);
	    Assert.AreEqual("00", settlementResult.ResultCode);
	}

	[Test]
	public void Split() {
	    ProPayProvider provider = new ProPayProvider();
	    PaymentResult splitResult = null;
	    try {
		splitResult = provider.Split(new CurrencyType(2.50), testAccountNumber);
		Assert.AreEqual("00", splitResult.ResultCode);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("63", ex.Result.ResultCode, "In the case of failure here, verify that it is due to 'insufficient funds' in the master account");
	    }
	}

	[Test()]
	public void ShouldHandleAmpersandInCommandText() {
	    ChargeCardCommand command = new ChargeCardCommand(new CurrencyType(5.00), "Corner of 5th & Vine", "", testCardholderPostalCode, testAccountNumber, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "");
	    Assert.IsTrue(command.CommandText.Length > 0);

	    String commandText = command.CommandText;

	    // verify
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("<addr>") + 20, 5), "&amp;");
	}*/

	#endregion

	#region Invalid configuration
	[Test]
	public void MissingHostURL() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.HOST_URL_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.HOST_URL_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingSpecificXMLEncoding() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.XMLENCODING_KEY] = "";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.XMLENCODING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingBusinessClass() {
	    // The BUSINESSCLASS_KEY allowed us to set a value that is for legacy purposes at ProPay.  They have since told us to always use "partner" as the value, so there is no need to read it from properties

	    //IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    //try {
	//	// change the provider to cause connection failure
	//	SimpleConfigurationProvider config = new SimpleConfigurationProvider();
	//	config.Settings.Add(currentConfig.Settings);
	//	config.Settings[ProPayProviderConfiguration.BUSINESSCLASS_KEY] = "";
	//	ConfigurationProvider.SetProvider(config);

	//	ProPayProvider provider = new ProPayProvider();
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	//	    Assert.Fail("Should throw PaymentConnectionException");
	//	} catch (Exception ex) {
	//	    Assert.IsTrue(ex is PaymentException);
	//	    Assert.IsTrue(ex is PaymentConfigurationException);
	//	    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.BUSINESSCLASS_KEY) >= 0);
	//	}
	  //  } finally {
	//	ConfigurationProvider.SetProvider(currentConfig);
	  //  }
	}

	[Test]
	public void MissingCommissionProcessingCertString() {
	    // (Value not used right now)

	    //IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    //try {
		// change the provider to cause connection failure
	//	SimpleConfigurationProvider config = new SimpleConfigurationProvider();
	//	config.Settings.Add(currentConfig.Settings);
	//	config.Settings[ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY] = "";
	//	ConfigurationProvider.SetProvider(config);

	//	ProPayProvider provider = new ProPayProvider();
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	//	    Assert.Fail("Should throw PaymentConnectionException");
	//	} catch (Exception ex) {
	//	    Assert.IsTrue(ex is PaymentException);
	//	    Assert.IsTrue(ex is PaymentConfigurationException);
	//	    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY) >= 0);
	//	}
	  //  } finally {
	//	ConfigurationProvider.SetProvider(currentConfig);
	  //  }
	}

	[Test]
	public void MissingRealTimeProcessingCertString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.CARDPROCESSING_CERTSTRING_KEY] = "";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.CARDPROCESSING_CERTSTRING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingSignupCertString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.SIGNUP_CERTSTRING_KEY] = "";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.SIGNUP_CERTSTRING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingTimeout() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.TIMEOUT_KEY] = "";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.TIMEOUT_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void InvalidConnectionString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.HOST_URL_KEY] = "http://localhoost/nada/";
		ConfigurationProvider.SetProvider(config);
	    	
		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is WebException);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void InvalidBusinessClassString() {
	    // It turns out that this is legacy on ProPay's part.  Here is an email I received on the topic:
	    // "That tag is a carry over from an earlier time.  Just use partner for all transactions."

	    //IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    //try {
	//	// change the provider to cause connection failure
	//	SimpleConfigurationProvider config = new SimpleConfigurationProvider();
	//	config.Settings.Add(currentConfig.Settings);
	//	config.Settings[ProPayProviderConfiguration.BUSINESSCLASS_KEY] = "superduper";
	//	ConfigurationProvider.SetProvider(config);

	//	ProPayProvider provider = new ProPayProvider();
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	//	    Assert.Fail("Should throw PaymentConnectionException");
	//	} catch (Exception ex) {
	//	    // TODO: these are probably wrong
	//	    Assert.IsTrue(ex is PaymentException);
	//	    Assert.IsTrue(ex is PaymentConfigurationException);
	//	    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.BUSINESSCLASS_KEY) >= 0);
	//	}
	  //  } finally {
	//	ConfigurationProvider.SetProvider(currentConfig);
	  //  }
	}

	[Test]
	public void InvalidSignupAccessCertString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.SIGNUP_CERTSTRING_KEY] = "superduper";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		PaymentResult result = null;
		try {
		    String uniqueValue = "" + DateTime.Now.Ticks;
		    result = provider.SignupAccount("US","ULTest" + uniqueValue + "@uppercaseliving.com","Joe","J","Wilbur" + uniqueValue,"" + uniqueValue + " Very Long Rd.","","Paris","ID","83204",
			PhoneNumberType.Parse("801-555-1243"),PhoneNumberType.Parse("801-555-1232"),"123456789",new DateTimeType(1980,1,1),"4747474747474747", "0815");
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentFailureException);
		    Assert.AreEqual((( PaymentFailureException )ex).Result.ResultCode, "59", "unable to authenticate"); 
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void InvalidRealTimeProcessingCertString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.CARDPROCESSING_CERTSTRING_KEY] = "superduper";
		ConfigurationProvider.SetProvider(config);

		ProPayProvider provider = new ProPayProvider();
		try {
		    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentFailureException);
		    Assert.AreEqual((( PaymentFailureException )ex).Result.ResultCode, "59", "unable to authenticate"); 
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void InvalidCommissionProcessingCertString() {
	    //(Value is not used right now)

	    //IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    //try {
	//	// change the provider to cause connection failure
	//	SimpleConfigurationProvider config = new SimpleConfigurationProvider();
	//	config.Settings.Add(currentConfig.Settings);
	//	config.Settings[ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY] = "superduper";
	//	ConfigurationProvider.SetProvider(config);

	//	ProPayProvider provider = new ProPayProvider();
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	//	    Assert.Fail("Should throw PaymentConnectionException");
	//	} catch (Exception ex) {
	//	    // TODO: these are probably wrong
	//	    Assert.IsTrue(ex is PaymentException);
	//	    Assert.IsTrue(ex is PaymentConfigurationException);
	//	    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY) >= 0);
	//	}
	//    } finally {
	//	ConfigurationProvider.SetProvider(currentConfig);
	//    }
	}
	#endregion

    }
}
