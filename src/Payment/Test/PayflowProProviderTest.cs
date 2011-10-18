using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Payment;
using Spring2.Core.Payment.PayflowPro;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for PayflowProProviderTest.
    /// </summary>
    [TestFixture]
    public class PayflowProProviderTest {
		
	private const string accountAmericanExpress = "378282246310005";
	private const string accountDinersClub = "30569309025904";
	private const string accountDiscover = "6011111111111117";
	private const string accountJcb = "3530111333300000";
	private const string accountMasterCard = "5555555555554444";
	private const string accountVisa = "4111111111111111";

	private const string expirationMonth = "11";
	private const string expirationYear = "15";

	private Random random = null;
	private IConfigurationProvider originalProvider;

	public PayflowProProviderTest(){
	    random = new Random();
	}

	[SetUp]
	public void Setup() {
	    originalProvider = ConfigurationProvider.Instance;

	    IConfigurationProvider provider = new SimpleConfigurationProvider();
	    provider.Settings.Add(ConfigurationProvider.Instance.Settings);
	    provider.Settings[PayflowProProviderConfiguration.CONNECTIONSTRING_KEY] = "user=GCUser;vendor=Spring2;partner=VeriSign;pwd=test123;verbosity=LOW";
	    provider.Settings[PayflowProProviderConfiguration.REMOSTHOST_KEY] = "pilot-payflowpro.paypal.com:443";
	    provider.Settings[PayflowProProviderConfiguration.TIMEOUT_KEY] = "30";
	    provider.Settings[PayflowProProviderConfiguration.ALLOWPOSTALCODEMISMATCH_KEY] = "0";
	    provider.Settings[PayflowProProviderConfiguration.ALLOWADDRESSMISMATCH_KEY] = "0";
	    provider.Settings[PayflowProProviderConfiguration.ALLOWCVVMISMATCH_KEY] = "0";
	    ConfigurationProvider.SetProvider(provider);
	}

	[TearDown]
	public void TearDown() {
	    ConfigurationProvider.SetProvider(originalProvider);
	}
        
	#region Success charge scenario for all card types
	[Test()]
	public void AmericanExpressPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void DinersClubPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountDinersClub), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void DiscoverPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountDiscover), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void JcbPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountJcb), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void MasterCardPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void VisaPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
	#endregion
    	
	#region Failure scenarios
	[Test()]
	[Ignore("fails because of test payflow pro account.")]
	public void OverLimitPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(2001, 3000)), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.EMPTY);
	    Assert.AreEqual("12", result, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
    	
	[Test]
	public void InvalidCreditCardNumber() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa + "1"), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		Assert.Fail();
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("23", ex.Result.ResultCode);
		Assert.AreEqual("Invalid account number", ex.Result.ResultMessage);
	    }
	}

	[Test]
	public void Timeout() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings["PayflowPro.Timeout"] = "1";
		ConfigurationProvider.SetProvider(config);

		PayflowProProvider provider = new PayflowProProvider();
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, "Elmer Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }	
	}

	[Test]
	public void CreditNonReferencedBadAcct() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Credit(StringType.EMPTY, new CurrencyType(random.Next(999)), "1111111111111111", expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, "");
		Assert.Fail("bad account number succeeded");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("23", ex.Result.ResultCode);
	    }
	}

	[Test]
	public void CreditReferencedBadTid() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Credit(StringType.EMPTY, new CurrencyType(random.Next(999)), "", "", "", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, "XXX");
		Assert.Fail("bad account number succeeded");
	    } catch (PaymentFailureException ex) {
		Assert.AreEqual("23", ex.Result.ResultCode);
	    }
	}
	#endregion
    	
	#region AVS
	[Test()]
	public void ValidAvsVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, new StringType("24285 Elm"), new StringType("00382"), StringType.UNSET);
	    Assert.AreEqual("Y", result.AVSResponseCode, "Address should be valid.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	    
	}

	[Test()]
	public void InvalidAddressVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    StringType referenceNumber = Guid.NewGuid().ToString();
	    try {
		PaymentResult result = provider.Charge(referenceNumber, random.Next(999), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, new StringType("49354 Main"), new StringType("00382"), StringType.UNSET);
	    	Assert.Fail();
	    } catch (AvsValidationException ex) {
	    	Assert.AreEqual("NY", ex.Result.AVSResponseCode, "Address should be invalid.");
		Assert.AreEqual(BooleanType.FALSE, ex.Result.ValidAddress);
		Assert.AreEqual(BooleanType.TRUE, ex.Result.ValidPostalCode);
		// TODO: the test processor does not fail for AVS failures and does not appear to allow for rules
	    	//Assert.AreNotEqual("0", ex.Result.ResultCode);
	    	
	    	// TODO: query the transaction to verify that it is voided
	    	//InquiryCommand command = new InquiryCommand(ex.Result.ReferenceNumber, DateTimeType.DEFAULT, DateTimeType.DEFAULT);
	    	//PaymentResult result = command.Execute();
	    	//Assert.AreEqual("", result.ResultMessage);
	    }
	}

	[Test()]
	public void InvalidPostalCodeVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Charge(StringType.EMPTY, random.Next(999), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, "24285 Elm", new StringType("94303"), StringType.UNSET);
		Assert.Fail();
	    } catch (AvsValidationException ex) {
		Assert.AreEqual("YN", ex.Result.AVSResponseCode, "Address should be invalid.");
		Assert.AreEqual(BooleanType.TRUE, ex.Result.ValidAddress);
		Assert.AreEqual(BooleanType.FALSE, ex.Result.ValidPostalCode);
		// TODO: the test processor does not fail for AVS failures and does not appear to allow for rules
		//Assert.AreNotEqual("0", ex.Result.ResultCode);
	    }
	}

    	[Test()]
	public void UnknownAvsVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, "79232 Maple", "20304", StringType.UNSET);
	    Assert.AreEqual("X", result.AVSResponseCode, "Address should be unvalidated.");
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(BooleanType.DEFAULT, result.ValidAddress);
	    Assert.AreEqual(BooleanType.DEFAULT, result.ValidPostalCode);
	}
	#endregion
    	
	#region CVV
	[Test()]
	public void ValidCvv2Verification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, new StringType("100"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("Y", result.CVVResponseCode, "CVV2 should be valid.");
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidCvv);
	}

	[Test()]
	public void InvalidCvv2Verification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, new StringType("400"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    	Assert.Fail();
	    } catch (PaymentFailureException ex) {
	    	Assert.AreEqual("N", ex.Result.CVVResponseCode, "CVV2 should be invalid.");
	    	//Assert.AreNotEqual("0", ex.Result.ResultCode); --> taken from PayflowPro_Guide.pdf, page 51:  The issuing bank may decline the transaction if
																										//	there is a mismatch. In other cases, the
																										//	transaction may be approved despite a mismatch.
	    	Assert.AreEqual(BooleanType.FALSE, ex.Result.ValidCvv);
	    }
	}

	[Test()]
	public void UnknownCvv2Verification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), expirationYear, expirationMonth, new StringType("700"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("X", result.CVVResponseCode, "CVV2 should be unvalidated.");
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(BooleanType.DEFAULT, result.ValidCvv);
	}
	#endregion		

	#region Success scenarios
	[Test]
    	public void Charge() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	}

	[Test]
	public void ChargeTransactionId() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Authorize(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);

	    PaymentResult result2 = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), "", expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, result.TransactionId);
	    Assert.AreEqual("0", result2.ResultCode);
	    Assert.AreNotEqual(result.TransactionId, result2.TransactionId);
	    Assert.AreEqual(12, result2.TransactionId.Length);
	}
    
	[Test]
	public void Authorize() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Authorize(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);
	}
		
	[Test]
	public void Refund() {
		
	}
		
	[Test]
	public void CreditNonReferenced() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Credit(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, "");
	    Assert.AreEqual("0", result.ResultCode);
	}

	[Test]
	public void CreditReferenced() {
	    PayflowProProvider provider = new PayflowProProvider();

	    CurrencyType amt = random.Next(999);

	    PaymentResult result = provider.Charge(StringType.EMPTY, amt, new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);

	    result = provider.Credit(StringType.EMPTY, amt, "", "", "", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, result.TransactionId);
	    Assert.AreEqual("0", result.ResultCode);
	}

	[Test]
	public void CreditReferencedFromReferenced() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Authorize(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    PaymentResult result2 = provider.Charge(StringType.EMPTY, 100, "", expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, result.TransactionId);
	    PaymentResult result3 = provider.Credit(StringType.EMPTY, 100, "", "", "", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, result2.TransactionId);
	    Assert.AreEqual("0", result3.ResultCode);
	}

	[Test]
	public void CreditNonReferencedBadAcctWithTid() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result0 = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    PaymentResult result = provider.Credit(StringType.EMPTY, new CurrencyType(random.Next(999)), "4111XXXXXXXXX1111", expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, result0.TransactionId);
	    Assert.AreEqual("0", result.ResultCode);
	}

	[Test]
	public void Settle() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Authorize(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);

	    PaymentResult result2 = provider.Settle(StringType.EMPTY, new CurrencyType(random.Next(999)), result.TransactionId, result.TransactionAmount);
	    Assert.AreEqual("0", result2.ResultCode);
	    Assert.AreEqual(12, result2.TransactionId.Length);
	    Assert.AreNotEqual(result.TransactionId, result2.TransactionId);
	}

	[Test()]
	public void ShouldEscapeValuesWhenGettingCommandText() {
	    SaleCommand command = new SaleCommand("Elmer & Fudd", new CurrencyType(random.Next(999)), "El&F", "Elmer&Fudd", "Elmer & Fudd", "Elmer & Fudd", "Elmer & Fudd", "Elm & Fud", "Elmer & Fudd", "Elmer & Fudd", "Elmer & Fudd");
	    Assert.IsTrue(command.CommandText.Length > 0);

	    String commandText = command.CommandText;
	   	
	   	// verify the commands
	   	Assert.AreEqual(commandText.Substring(commandText.IndexOf("&ACCT"), 10),"&ACCT[12]=");
	   	Assert.AreEqual(commandText.Substring(commandText.IndexOf("&CVV2"), 9), "&CVV2[4]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&CUSTREF"), 13), "&CUSTREF[10]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&EXPDATE"), 13), "&EXPDATE[12]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&NAME"), 10), "&NAME[12]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&STREET"), 12), "&STREET[12]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&ZIP"), 8), "&ZIP[9]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&COMMENT1"), 14), "&COMMENT1[12]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&COMMENT2"), 14), "&COMMENT2[12]=");
	    Assert.AreEqual(commandText.Substring(commandText.IndexOf("&PONUM"), 11), "&PONUM[12]=");
	    
	    // verify the values
	   	Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&ACCT") + 10, 12)); //&ACCT[12]=
	    Assert.AreEqual("El&F", commandText.Substring(commandText.IndexOf("&CVV2") + 9, 4));//&CVV2[4]=
	    Assert.AreEqual("Elmer&Fudd", commandText.Substring(commandText.IndexOf("&CUSTREF") + 13, 10));//&CUSTREF[10]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&EXPDATE") + 13, 12));//&EXPDATE[12]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&NAME") + 10, 12));//&NAME[12]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&STREET") + 12, 12));//&STREET[12]=
	    Assert.AreEqual("Elm & Fud", commandText.Substring(commandText.IndexOf("&ZIP") + 8, 9));//&ZIP[9]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&COMMENT1") + 14, 12));//&COMMENT1[12]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&COMMENT2") + 14, 12));//&COMMENT2[12]=
	    Assert.AreEqual("Elmer & Fudd", commandText.Substring(commandText.IndexOf("&PONUM") + 11, 12));//&PONUM[12]=
	}

	[Test()]
	public void ShouldHandleAmpersandInName() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, "Elmer & Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	}

	[Test]
	public void ShouldHandleSimultaneousReferenceTransaction() {
	    PayflowProProvider provider = new PayflowProProvider();

	    CurrencyType authAmount = new CurrencyType(random.Next(999));
	    PaymentResult result = provider.Authorize(StringType.EMPTY, authAmount, new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);
	    string transactionId = result.TransactionId;

	    CurrencyType authAmount2 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId2 = result.TransactionId;

	    CurrencyType authAmount3 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId2);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId3 = result.TransactionId;

	    PaymentResult settleResult = provider.Settle(StringType.EMPTY, authAmount3 + 10, transactionId3, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId2, settleResult.TransactionId);

	    settleResult = provider.Settle(StringType.EMPTY, authAmount2 + 10, transactionId2, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId2, settleResult.TransactionId);

	    settleResult = provider.Settle(StringType.EMPTY, authAmount + 10, transactionId, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId, settleResult.TransactionId);
	}

	[Test]
	public void ContinuousAuthorizations() {
	    PayflowProProvider provider = new PayflowProProvider();

	    CurrencyType authAmount = new CurrencyType(random.Next(999));
	    PaymentResult result = provider.Authorize(StringType.EMPTY, authAmount, new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);
	    string transactionId = result.TransactionId;

	    PaymentResult settleResult = provider.Settle(StringType.EMPTY, authAmount + 10, transactionId, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId, settleResult.TransactionId);

	    CurrencyType authAmount2 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId2 = result.TransactionId;

	    settleResult = provider.Settle(StringType.EMPTY, authAmount2 + 10, transactionId2, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId2, settleResult.TransactionId);

	    CurrencyType authAmount3 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId2);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId3 = result.TransactionId;

	    settleResult = provider.Settle(StringType.EMPTY, authAmount3 + 10, transactionId3, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId3, settleResult.TransactionId);
	}

	[Test]
	public void ContinuousAuthorizationsWithInvalidCardData() {
	    PayflowProProvider provider = new PayflowProProvider();

	    CurrencyType authAmount = new CurrencyType(random.Next(999));
	    PaymentResult result = provider.Authorize(StringType.EMPTY, authAmount, new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);
	    string transactionId = result.TransactionId;

	    PaymentResult settleResult = provider.Settle(StringType.EMPTY, authAmount + 10, transactionId, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId, settleResult.TransactionId);

	    CurrencyType authAmount2 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId2 = result.TransactionId;

	    settleResult = provider.Settle(StringType.EMPTY, authAmount2 + 10, transactionId2, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId2, settleResult.TransactionId);

	    CurrencyType authAmount3 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId2);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId.Length);
	    string transactionId3 = result.TransactionId;

	    settleResult = provider.Settle(StringType.EMPTY, authAmount3 + 10, transactionId3, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId3, settleResult.TransactionId);
	} 

	[Test]
	public void VerifyAndAuth() {
	    PayflowProProvider provider = new PayflowProProvider();
	    CurrencyType authAmount = 0;
	    PaymentResult result = provider.Authorize(StringType.EMPTY, authAmount, new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(12, result.TransactionId.Length);
	    string transactionId = result.TransactionId;

	    CurrencyType authAmount2 = new CurrencyType(random.Next(999));
	    result = provider.Authorize(StringType.EMPTY, authAmount2, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, transactionId);
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreNotEqual(transactionId, result.TransactionId);
	    string transactionId2 = result.TransactionId;

	    PaymentResult settleResult = provider.Settle(StringType.EMPTY, authAmount2 + 10, transactionId2, result.TransactionAmount);
	    Assert.AreEqual("0", settleResult.ResultCode);
	    Assert.AreEqual(12, settleResult.TransactionId.Length);
	    Assert.AreNotEqual(transactionId2, settleResult.TransactionId);
	}
	#endregion
    	    	
	#region Invalid configuration
	[Test]
	public void MissingConnectionString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[PayflowProProviderConfiguration.CONNECTIONSTRING_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProvider provider = new PayflowProProvider();
		try {
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.CONNECTIONSTRING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void MissingRemostHost() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[PayflowProProviderConfiguration.REMOSTHOST_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProvider provider = new PayflowProProvider();
		try {
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.REMOSTHOST_KEY) >= 0);
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
		config.Settings[PayflowProProviderConfiguration.TIMEOUT_KEY] = "";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProvider provider = new PayflowProProvider();
		try {
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.TIMEOUT_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}
    	
    	[Test]
	public void ProxyParsing() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[PayflowProProviderConfiguration.PROXY_KEY] = "host:1234:user:password";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProviderConfiguration c = new PayflowProProviderConfiguration();
		Assert.AreEqual("host", c.ProxyAddress);
	    	Assert.AreEqual(1234, c.ProxyPort);
		Assert.AreEqual("user", c.ProxyLogon);
		Assert.AreEqual("password", c.ProxyPassword);
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
    	}

	[Test]
	public void NullConectionString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings.Remove(PayflowProProviderConfiguration.CONNECTIONSTRING_KEY);
		ConfigurationProvider.SetProvider(config);
	    	
		try {
		    PayflowProProviderConfiguration c = new PayflowProProviderConfiguration();
		    Assert.Fail();
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.CONNECTIONSTRING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}
    	
	[Test]
	public void NullRemoteHost() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings.Remove(PayflowProProviderConfiguration.REMOSTHOST_KEY);
		ConfigurationProvider.SetProvider(config);
	    	
		try {
		    PayflowProProviderConfiguration c = new PayflowProProviderConfiguration();
		    Assert.Fail();
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.REMOSTHOST_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void NullTimeout() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings.Remove(PayflowProProviderConfiguration.TIMEOUT_KEY);
		ConfigurationProvider.SetProvider(config);
	    	
		try {
		    PayflowProProviderConfiguration c = new PayflowProProviderConfiguration();
		    Assert.Fail();
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.TIMEOUT_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}

	[Test]
	public void ShortConnectionString() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
		// change the provider to cause connection failure
		SimpleConfigurationProvider config = new SimpleConfigurationProvider();
		config.Settings.Add(currentConfig.Settings);
		config.Settings[PayflowProProviderConfiguration.CONNECTIONSTRING_KEY] = "a";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProvider provider = new PayflowProProvider();
		try {
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.CONNECTIONSTRING_KEY) >= 0);
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
		config.Settings[PayflowProProviderConfiguration.CONNECTIONSTRING_KEY] = "user:GCUser;vendor:Spring2;partner:VeriSign;pwd:test123;verbosity:LOW";
		ConfigurationProvider.SetProvider(config);
	    	
		PayflowProProvider provider = new PayflowProProvider();
		try {
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), expirationYear, expirationMonth, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
		    Assert.Fail("Should throw PaymentConnectionException");
		} catch (Exception ex) {
		    Assert.IsTrue(ex is PaymentException);
		    Assert.IsTrue(ex is PaymentConfigurationException);
		    Assert.IsTrue(ex.Message.IndexOf(PayflowProProviderConfiguration.CONNECTIONSTRING_KEY) >= 0);
		}
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }
	}
    	
    	[Test]
	public void InvalidHostName() {
	    IConfigurationProvider currentConfig = ConfigurationProvider.Instance;
	    try {
	    	// change the provider to cause connection failure
	    	SimpleConfigurationProvider config = new SimpleConfigurationProvider();
	    	config.Settings.Add(currentConfig.Settings);
	    	config.Settings["PayflowPro.RemoteHost"] = "foo."+currentConfig.Settings["PayflowPro.RemoteHost"];
	    	ConfigurationProvider.SetProvider(config);

	    	PayflowProProvider provider = new PayflowProProvider();
	    	PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), expirationYear, expirationMonth, StringType.UNSET, "Elmer Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    	Assert.Fail();
	    } catch (PaymentConnectionException) {
	    	// this is expected
	    } finally {
	    	ConfigurationProvider.SetProvider(currentConfig);
	    }	
    	}
	#endregion
    	
    	
    }
}
