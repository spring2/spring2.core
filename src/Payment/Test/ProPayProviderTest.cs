using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

//using Spring2.Dss.Payment;
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

	ProPayProvider provider = new ProPayProvider();

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
	private const String testAccountSSNLast4Digits = "0000";

	private CurrencyType declineAmount = new CurrencyType(113); // the test system will automatically fail with this value, as per the ProPay documentation
	private const String invalidMasterAccount = "1122345"; // just making this up

	public ProPayProviderTest() {
	}


	#region Success charge scenario for all card types
	[Test()]
	public void SuccessfulPayment() {
	    
	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("00", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
	#endregion

	#region Transaction Status
	[Test()]
	public void GetTransactionStatus() {
	    // run transaction
	    
	    // charge
	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("00", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));

	    // check status
	    String transactionStatus = provider.GetTransactionStatus(testAccountNumber, result.TransactionId);
	    Assert.AreEqual(transactionStatus, "CCDebitPending");

	    provider.Void(testAccountNumber, result.TransactionId, new CurrencyType(5.00));

	    transactionStatus = provider.GetTransactionStatus(testAccountNumber, result.TransactionId);
	    Assert.AreEqual(transactionStatus, "CCDebitVoided");
	}
	#endregion

	#region User information
	[Test()]
	public void SignupAndRetrieveUserData() {
	    bool exceptionOccurredDuringSignup = false;
	    try {
		provider.SignupAccount("US", testAccountEmail, "Test", "E", "Testerson", "45 Testing Circle", "", "Testville", "UT", testCardholderPostalCode,
		    PhoneNumberType.Parse("801-555-4321"), PhoneNumberType.Parse("801-555-4321"), "000000000", DateTimeType.Parse("1/1/1980"), testCardNumber, testCardExpMonth + testCardExpYear);
	    } catch(PaymentFailureException ex) {
		// user has already been added, this is expected, just ignore
		exceptionOccurredDuringSignup = true;
		Assert.AreEqual("87", ex.Result.ResultCode, "DuplicateUserID");
	    }
	    Assert.IsTrue(exceptionOccurredDuringSignup);
	    String jan1 = "1/1/2008";// +DateTime.Now.Year;
	    String dec31 = "12/31/2008";// +DateTime.Now.Year;
	    ProPayResult signupResult = provider.GetSignupReport(DateType.Parse(jan1), DateType.Parse(dec31));
	    StringTypeList listOfAccountNumbers = provider.GetUserSignupAccountNumbers(signupResult);
	    StringTypeList listOfAccountEmails = provider.GetUserSignupEmails(signupResult);
	    StringTypeList listOfAccountUserIds = provider.GetUserSignupIds(signupResult);
	    Assert.AreEqual(listOfAccountEmails.Count, listOfAccountNumbers.Count);
	    Assert.AreEqual(listOfAccountEmails.Count, listOfAccountUserIds.Count);
	    Assert.IsTrue(listOfAccountNumbers.Contains(testAccountNumber));
	    Assert.IsTrue(listOfAccountEmails.Contains(testAccountEmail));
	}
	#endregion

	#region Failure scenarios
	[Test()]
	public void OverLimitPayment() {
	    // Documentation says that the test system has a value of $20 maximum, but it lies
	    try {
		provider.Charge(testAccountNumber, new CurrencyType(15000), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.Fail("Should not have processed a $15,000 transaction");
	    } catch(PaymentFailureException ex) {
		Assert.AreEqual("61", ex.Result.ResultCode, "Amount exceeds single transaction limit");
	    }
	}
    	
	[Test]
	public void InvalidCreditCardNumber() {
	    
	    try {
		PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, bogusCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

		
		PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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
	    
	    StringType zipMatchValue = testCardholderPostalCode; // by ProPay docs, this should return 'A', Address Match
	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("A", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification2() {
	    StringType zipMatchValue = new StringType("85284"); // by ProPay docs, this should return 'Z', Zip Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("Z", result.AVSResponseCode, "Address zip should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification3() {
	    StringType zipMatchValue = new StringType("832085284"); // by ProPay docs, this should return 'Y', Exact Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("Y", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification4() {
	    StringType zipMatchValue = new StringType("999970001"); // by ProPay docs, this should return 'B', Address Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("B", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification5() {
	    StringType zipMatchValue = new StringType("999970003"); // by ProPay docs, this should return 'D', Exact Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("D", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification6() {
	    StringType zipMatchValue = new StringType("999970005"); // by ProPay docs, this should return 'M', Exact Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("M", result.AVSResponseCode, "Address should be an exact match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void ValidAvsVerification7() {
	    StringType zipMatchValue = new StringType("999970006"); // by ProPay docs, this should return 'P', Zip Match

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("P", result.AVSResponseCode, "Address should match.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification1() {
	    StringType zipMatchValue = new StringType("99994"); // by ProPay docs, this should return 'U', Ver Unavailable

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("U", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification2() {
	    StringType zipMatchValue = new StringType("99998"); // by ProPay docs, this should return 'G', Ver Unavailable

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("G", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification3() {
	    StringType zipMatchValue = new StringType("999970002"); // by ProPay docs, this should return 'C', Serv Unavailable

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("C", result.AVSResponseCode, "Serv unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification4() {
	    StringType zipMatchValue = new StringType("999970004"); // by ProPay docs, this should return 'I', Ver Unavailable

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("I", result.AVSResponseCode, "Ver unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification5() {
	    StringType zipMatchValue = new StringType("999970009"); // by ProPay docs, this should return 'S', Service Not supported

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("S", result.AVSResponseCode, "Service Not supported");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void InvalidAvsVerification6() {
	    StringType zipMatchValue = new StringType("999970010"); // by ProPay docs, this should return 'R', Issuer system unavailable

	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("R", result.AVSResponseCode, "Issuer system unavailable");
	    Assert.AreEqual(BooleanType.FALSE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.FALSE, result.ValidPostalCode);
	}

	[Test()]
	public void GarbageAvsVerification() {
	    StringType zipMatchValue = new StringType("8320 85284"); // something about the space causes error 32, even on their testing setup, which is fine because it gives us another thing we can test
	    
	    bool paymentFailureExceptionThrown = false;
	    PaymentResult result = null;
	    try {
		result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, zipMatchValue, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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
	    
	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, cvvMatchValue, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("M", result.CVVResponseCode, "CVV2 should be valid.");
	    Assert.AreEqual("00", result.ResultCode);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidCvv);
	}

	[Test()]
	public void InvalidCvv2Verification1() {
	    StringType cvvMatchValue = new StringType("888"); // ProPay testing server only accepts '999' for successful transactions
	    
	    PaymentResult result = null;
	    bool paymentFailureExceptionThrown = false;
	    try {
		result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, cvvMatchValue, "orderId-" + DateTime.Now.Ticks);
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
	    
	    PaymentResult result = null;
	    bool paymentFailureExceptionThrown = false;
	    try {
		provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, cvvMatchValue, "orderId-" + DateTime.Now.Ticks);
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
	    
	    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("00", result.ResultCode);
	}

	[Test]
	public void ChargeAndSplit() {
	    PaymentResult result = provider.ChargeWithSplit(testAccountNumber, new CurrencyType(5.00), new CurrencyType(4.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks, new DecimalType(0.75M));
	    Assert.AreEqual("00", result.ResultCode);
	}

	[Test]
	public void Authorize() {
	    
	    PaymentResult result = null;
	    try {
		result = provider.Authorize(new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, masterAccountNumber, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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
	public void DoARefund() {
	    
	    CurrencyType originalTransactionAmount = new CurrencyType(5.00);
	    PaymentResult chargeResult = provider.Charge(testAccountNumber, originalTransactionAmount, testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
	    Assert.AreEqual("00", chargeResult.ResultCode);
	    PaymentResult refundResult = provider.Refund(testAccountNumber, chargeResult.TransactionId, originalTransactionAmount, originalTransactionAmount, new DecimalType(0.0M));
	    Assert.AreEqual("00", refundResult.ResultCode);
	}

	public void deleteme() {
	    PaymentResult prCharge = provider.Charge("1036726", 29.74, "132 Great Western Road", "83838", "4747474747474747", "01/11", "999", "T1-101008");
	    Assert.AreEqual("00", prCharge.ResultCode);
	    PaymentResult prSplit = provider.Split("1036726", 6.98, prCharge.TransactionId);
	    Assert.AreEqual("00", prSplit.ResultCode);
	    PaymentResult prRefund = provider.Refund("1036726", prCharge.TransactionId, 29.74, 27.90, .75);
	    Assert.AreEqual("00", prRefund.ResultCode);
	}

	[Test]
	public void DoAVoid() {
	    
	    PaymentResult result = null;
	    PaymentResult refundResult = null;
	    try {
		CurrencyType amount = new CurrencyType(5.00);
		result = provider.Charge(testAccountNumber, amount, testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.AreEqual("00", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
		refundResult = provider.Void(testAccountNumber, result.TransactionId, amount);
		Assert.AreEqual("00", refundResult.ResultCode);
	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}
		
	[Test]
	public void DoACredit() {
	    
	    StringType orderId = "orderId-" + DateTime.Now.Ticks;
	    PaymentResult result = null;
	    PaymentResult refundResult = null;
	    try {
		result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, orderId);
		Assert.AreEqual("00", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
		//refundResult = provider.Credit(orderId, new CurrencyType(4.00), masterAccountNumber, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, result.TransactionId);
		refundResult = provider.Credit(testAccountNumber, new CurrencyType(5.00), String.Empty);
		Assert.AreEqual("00", refundResult.ResultCode);
	    } catch(PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}
	
	[Test]
	public void DoASettle() {
	    
	    StringType orderId = "orderId-" + DateTime.Now.Ticks;
	    PaymentResult authorizeResult = provider.Authorize(new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testAccountNumber, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, orderId);
	    Assert.AreEqual("00", authorizeResult.ResultCode);
	    PaymentResult settlementResult = provider.Settle(testAccountNumber, authorizeResult.TransactionId);
	    Assert.AreEqual("00", settlementResult.ResultCode);
	}

	[Test]
	public void DoASplit() {
	    
	    PaymentResult splitResult = null;
	    PaymentResult chargeResult = null;
	    try {
		StringType orderId = "orderId-" + DateTime.Now.Ticks;
		chargeResult = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.AreEqual("00", chargeResult.ResultCode);
		splitResult = provider.Split(testAccountNumber, masterAccountNumber, new CurrencyType(2.50), chargeResult.TransactionId); 
		Assert.AreEqual("00", splitResult.ResultCode);
	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}

	public void GetABalance() {
	    CurrencyType balanceAmount = provider.GetBalance(testAccountNumber);
	    Assert.IsTrue(balanceAmount.IsValid);
	}

	[Test]
	public void DoAChargeSplitReverseWithVoidPreSettle() {
	    
	    PaymentResult splitResult = null;
	    PaymentResult chargeResult = null;
	    PaymentResult reverseChargeResult = null;
	    try {
		StringType orderId = "orderId-" + DateTime.Now.Ticks;

		// charge the card, paying to the demonstrator
		chargeResult = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.AreEqual("00", chargeResult.ResultCode);

		// order the split, paying us our share
		splitResult = provider.Split(testAccountNumber, masterAccountNumber, new CurrencyType(2.50), chargeResult.TransactionId);
		Assert.AreEqual("00", splitResult.ResultCode);

		reverseChargeResult = provider.Void(testAccountNumber, chargeResult.TransactionId, new CurrencyType(5.00));
		Assert.AreEqual("00", reverseChargeResult.ResultCode);

	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}

	[Test]
	public void DoAChargeSplitReverseWithVoidPostSettle() {
	    // This test follows the same patter as a refund would, were the transaction status to return as "settled," which only happens in testing when ProPay forces a transaction to that status by hand, by our request to them

	    PaymentResult splitResult = null;
	    PaymentResult chargeResult = null;
	    PaymentResult reverseSplitResult = null;
	    PaymentResult reverseChargeResult = null;
	    try {
		StringType orderId = "orderId-" + DateTime.Now.Ticks;

		// charge the card, paying to the demonstrator
		chargeResult = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.AreEqual("00", chargeResult.ResultCode);

		// order the split, paying us our share
		splitResult = provider.Split(testAccountNumber, masterAccountNumber, new CurrencyType(2.50), chargeResult.TransactionId);
		Assert.AreEqual("00", splitResult.ResultCode);

		// void the split by returning our share to the demonstrator
		reverseSplitResult = provider.VoidSplit(testAccountNumber, new CurrencyType(2.50));
		Assert.AreEqual("00", reverseSplitResult.ResultCode);

		// void the transaction
		reverseChargeResult = provider.Void(testAccountNumber, chargeResult.TransactionId, new CurrencyType(5.00));
		Assert.AreEqual("00", reverseChargeResult.ResultCode);

	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}

	[Test]
	public void DoAChargeSplitReverseWithRefund1() {
	    
	    PaymentResult splitResult = null;
	    PaymentResult chargeResult = null;
	    PaymentResult reverseResult = null;
	    try {
		StringType orderId = "orderId-" + DateTime.Now.Ticks;
		CurrencyType amount = new CurrencyType(5.00);
		CurrencyType commissionableAmount = new CurrencyType(5.00);

		// charge the card, paying to the demonstrator
		chargeResult = provider.Charge(testAccountNumber, amount, testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.AreEqual("00", chargeResult.ResultCode);

		// order the split, paying us our share
		splitResult = provider.Split(testAccountNumber, masterAccountNumber, new CurrencyType(3.75), chargeResult.TransactionId);
		Assert.AreEqual("00", splitResult.ResultCode);

		String transactionStatus = provider.GetTransactionStatus(testAccountNumber, chargeResult.TransactionId);
		if (transactionStatus.EndsWith("Settled")) {
		    reverseResult = provider.Refund(testAccountNumber, chargeResult.TransactionId, amount, commissionableAmount, new DecimalType(0.75M));
		} else {
		    reverseResult = provider.Void(testAccountNumber, chargeResult.TransactionId, amount);
		}

		Assert.AreEqual("00", reverseResult.ResultCode);

	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}

	[Test]
	public void DoAChargeSplitReverseWithRefund2() {

	    PaymentResult chargeResult = null;
	    PaymentResult refundResult = null;
	    try {
		StringType orderId = "orderId-" + DateTime.Now.Ticks;
		CurrencyType amount = new CurrencyType(11.00);
		CurrencyType commissionableAmount = new CurrencyType(10.00);

		// charge the card, paying to the demonstrator and splitting 75% to us
		chargeResult = provider.ChargeWithSplit(testAccountNumber, amount, commissionableAmount, testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks, new DecimalType(0.75M));
		Assert.AreEqual("00", chargeResult.ResultCode);

		refundResult = provider.Refund(testAccountNumber, chargeResult.TransactionId, amount, commissionableAmount, new DecimalType(0.75M));

		Assert.AreEqual("00", refundResult.ResultCode);

	    } catch (PaymentFailureException ex) {
		Assert.Fail(ex.Message);
	    }
	}

	[Test()]
	public void ShouldHandleAmpersandInCommandText() {
	    ChargeCardCommand command = new ChargeCardCommand(testAccountNumber, new CurrencyType(5.00), "Corner of 5th & Vine", "", testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "");
	    Assert.IsTrue(command.CommandText.Length > 0);

	    String commandText = command.CommandText;
	   	
   	    // verify
   	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("<addr>") + 20, 5),"&amp;");
	}

	[Test()]
	public void ShouldHandleAngleBracketsInCommandText() {
	    ChargeCardCommand command = new ChargeCardCommand(testAccountNumber, new CurrencyType(5.00), "Corner of <5 and Dime>", "", testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "");
	    Assert.IsTrue(command.CommandText.Length > 0);

	    String commandText = command.CommandText;

	    // verify
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("<addr>") + 16, 4), "&lt;");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("<addr>") + 30, 4), "&gt;");
	}

	#endregion

	#region Secondary API Scenarios

	[Test]
	public void PingUser() {
	    
	    ProPayResult result = (ProPayResult)provider.PingAccount(testAccountEmail, testAccountNumber, testAccountSSNLast4Digits);
	    Assert.AreEqual("00", result.ResultCode);
	    StringType pingAccountNum = result.GetResultValue("accountNum");
	    StringType pingExpiration = result.GetResultValue("expiration");
	    StringType pingAffiliation = result.GetResultValue("affiliation");
	    StringType pingTier = result.GetResultValue("tier");

	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    String affiliationFromConfig = currentConfig.Settings[ProPayProviderConfiguration.AFFILIATION];
	    Assert.AreEqual(affiliationFromConfig, pingAffiliation.ToString());
	    Assert.AreEqual(pingAccountNum.ToString(), testAccountNumber);
	    Assert.AreNotEqual(pingExpiration, StringType.DEFAULT);
	    Assert.AreNotEqual(pingTier, StringType.DEFAULT);
	}

	[Test]
	public void UseBadMasterAccount() {
	    
	    try {
		provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("47", ex.Result.ResultCode, "Invalid accntNum");
	    }
	}

	[Test]
	public void ChargeWithDecline() {
	    
	    try {
		provider.Charge(testAccountNumber, declineAmount, testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.Fail("ProPay documentation says this amount should fail, so why are we here?");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("58", ex.Result.ResultCode, "Credit card declined");
	    }
	}
	[Test]
	public void UsePastExpDate() {
	    
	    try {
		provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, "01" + (DateTime.Now.Year - 1), testCardCVV, "orderId-" + DateTime.Now.Ticks);
		Assert.Fail("Successful charge with an invalid date");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("49", ex.Result.ResultCode, "Invalid expDate");
	    }
	}

	[Test]
	public void SplitWithBadTransactionId() {
	    
	    PaymentResult splitResult = null;
	    try {
		splitResult = provider.Split(testAccountNumber, new CurrencyType(2.50), "1");
		Assert.AreEqual("00", splitResult.ResultCode);
		Assert.Fail();
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("51", ex.Result.ResultCode, ex.Message); //{"Failed to process the payment. 51: (ProPay) 'Invalid transNum or unavailable to act on transNum due to funding'."}
	    }
	}

	[Test]
	public void ChargeWithSplitShouldNotBeAttemptedWhenFeesAreTooHigh() {
	    try {
		PaymentResult result = provider.ChargeWithSplit(testAccountNumber, new CurrencyType(5.00), new CurrencyType(0.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks, new DecimalType(0.75M));
		Assert.Fail("Total charge should have bee less than the split plus the fees");
	    } catch (InvalidArgumentException ex) {
		// this is expected
	    }
	}

	[Test]
	public void TransferFromMasterAccountToTestAccount() {
	    try {
		PaymentResult reverseSplitResult = provider.VoidSplit(testAccountNumber, new CurrencyType(2.50));
	    } catch(Exception ex) {
		// if this fails due to insufficient funds, ProPay needs to add more cash to one of the Uppercase Cert Strings (not sure which one, probably the commission cert string) account for transType 02
		Assert.Fail(ex.Message);
	    }
	}

	/*
	[Test]
	public void AuthorizeWithBadMasterAccount() {
	    
	    try {
		provider.Authorize(invalidMasterAccount, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("47", ex.Result.ResultCode, "Invalid accntNum");
	    }
	}

	[Test]
	public void Refund() {
	    
	    CurrencyType originalTransactionAmount = new CurrencyType(5.00);
	    PaymentResult chargeResult = provider.Charge("orderId-"+DateTime.Now.Ticks, originalTransactionAmount, testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, testAccountNumber/*StringType.UNSET*//*);
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
	    
	    PaymentResult authorizeResult = provider.Authorize(masterAccountNumber, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, StringType.UNSET);
	    Assert.AreEqual("00", authorizeResult.ResultCode);
	    PaymentResult settlementResult = provider.Settle(masterAccountNumber, CurrencyType.UNSET, authorizeResult.TransactionId, CurrencyType.UNSET);
	    Assert.AreEqual("00", settlementResult.ResultCode);
	}

	[Test]
	public void Split() {
	    
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
	    	
		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

	//	
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, testAccountNumber/*StringType.UNSET*/);
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
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY] = "";
		ConfigurationProvider.SetProvider(config);

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(ProPayProviderConfiguration.COMMISSIONPROCESSING_CERTSTRING_KEY) >= 0);
		}
	  } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	  }
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

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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
	public void InvalidProviderURL() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[ProPayProviderConfiguration.HOST_URL_KEY] = "http://localhoost/nada/";
		ConfigurationProvider.SetProvider(config);
	    	
		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

	//	
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, testAccountNumber/*StringType.UNSET*//*);
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

		
		try {
		    PaymentResult result = provider.Charge(testAccountNumber, new CurrencyType(5.00), testCardholderAddress, testCardholderPostalCode, testCardNumber, testCardExpMonth + testCardExpYear, testCardCVV, "orderId-" + DateTime.Now.Ticks);
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

	//	
	//	try {
	//	    PaymentResult result = provider.Charge("orderId-"+DateTime.Now.Ticks, new CurrencyType(5.00), testCardNumber, testCardExpYear, testCardExpMonth, testCardCVV, testCardholderName, testCardholderAddress, testCardholderPostalCode, testAccountNumber/*StringType.UNSET*//*);
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
