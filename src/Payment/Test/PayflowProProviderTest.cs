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

	private Random random = null;

	public PayflowProProviderTest(){
	    random = new Random();
	}
        
	#region Success charge scenario for all card types
	[Test()]
	public void AmericanExpressPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void DinersClubPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountDinersClub), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void DiscoverPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountDiscover), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void JcbPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountJcb), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void MasterCardPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}

	[Test()]
	public void VisaPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
	#endregion
    	
	#region Failure scenarios
	[Test()]
	[Ignore("fails because of test payflow pro account.")]
	public void OverLimitPayment() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(2001, 3000)), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.EMPTY);
	    Assert.AreEqual("12", result, string.Format("Unexpected result of {0}:{1}", result.ResultCode, result.ResultMessage));
	}
    	
	[Test]
	public void InvalidCreditCardNumber() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa + "1"), "11", "09", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "09", "11", StringType.UNSET, "Elmer Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
		Assert.Fail();
	    } catch (PaymentConnectionException) {
		// this is expected
	    } finally {
		ConfigurationProvider.SetProvider(currentConfig);
	    }	
	}
	#endregion
    	
	#region AVS
	[Test()]
	public void ValidAvsVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, new StringType("24285 Elm"), new StringType("00382"), StringType.UNSET);
	    Assert.AreEqual("Y", result.AVSResponseCode, "Address should be valid.");
	    Assert.AreEqual(BooleanType.TRUE, result.ValidAddress);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidPostalCode);
	    
	}

	[Test()]
	public void InvalidAddressVerification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    StringType referenceNumber = Guid.NewGuid().ToString();
	    try {
		PaymentResult result = provider.Charge(referenceNumber, random.Next(999), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, new StringType("49354 Main"), new StringType("00382"), StringType.UNSET);
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
		PaymentResult result = provider.Charge(StringType.EMPTY, random.Next(999), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, "24285 Elm", new StringType("94303"), StringType.UNSET);
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
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", StringType.UNSET, StringType.UNSET, "79232 Maple", "20304", StringType.UNSET);
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
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", new StringType("100"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("Y", result.CVVResponseCode, "CVV2 should be valid.");
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(BooleanType.TRUE, result.ValidCvv);
	}

	[Test()]
	public void InvalidCvv2Verification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    try {
		PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", new StringType("400"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    	Assert.Fail();
	    } catch (PaymentFailureException ex) {
	    	Assert.AreEqual("N", ex.Result.CVVResponseCode, "CVV2 should be invalid.");
	    	Assert.AreNotEqual("0", ex.Result.ResultCode);
	    	Assert.AreEqual(BooleanType.FALSE, ex.Result.ValidCvv);
	    }
	}

	[Test()]
	public void UnknownCvv2Verification() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountMasterCard), "09", "11", new StringType("700"), StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("X", result.CVVResponseCode, "CVV2 should be unvalidated.");
	    Assert.AreEqual("0", result.ResultCode);
	    Assert.AreEqual(BooleanType.DEFAULT, result.ValidCvv);
	}
	#endregion		

	#region Success scenarios
	[Test]
    	public void Charge() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "11", "09", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
	}
    
	[Test]
	public void Authorize() {
	    		
	}
		
	[Test]
	public void Refund() {
		
	}
		
	[Test]
	public void Credit() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Credit(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "11", "09", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, "ignored original transactionId");
	    Assert.AreEqual("0", result.ResultCode);
	}
	
	[Test]
	public void Settle() {
		
	}

	[Test()]
	public void ShouldEscapeValuesWhenGettingCommandText() {
	    SaleCommand command = new SaleCommand("Elmer & Fudd", new CurrencyType(random.Next(999)), "El&F", "Elmer&Fudd", "Elmer & Fudd", "Elmer & Fudd", "Elmer & Fudd", "Elm & Fud", "Elmer & Fudd", "Elmer & Fudd", "Elmer & Fudd");
	    Console.Write(command.CommandText);
	    Assert.IsTrue(command.CommandText.Length > 0);
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&ACCT=") + 6, 14));
	    Assert.AreEqual("El%26F", command.CommandText.Substring(command.CommandText.IndexOf("&CVV2=") + 6, 6));
	    Assert.AreEqual("Elmer%26Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&CUSTREF=") + 9, 12));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&EXPDATE=") + 9, 14));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&NAME=") + 6, 14));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&STREET=") + 8, 14));
	    Assert.AreEqual("Elm+%26+Fud", command.CommandText.Substring(command.CommandText.IndexOf("&ZIP=") + 5, 11));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&COMMENT1=") + 10, 14));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&COMMENT2=") + 10, 14));
	    Assert.AreEqual("Elmer+%26+Fudd", command.CommandText.Substring(command.CommandText.IndexOf("&PONUM=") + 7, 14));
	}

	[Test()]
	public void ShouldHandleAmpersandInName() {
	    PayflowProProvider provider = new PayflowProProvider();
	    PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "11", "09", StringType.UNSET, "Elmer & Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
	    Assert.AreEqual("0", result.ResultCode);
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
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
		    provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountAmericanExpress), "09", "11", StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
	    	PaymentResult result = provider.Charge(StringType.EMPTY, new CurrencyType(random.Next(999)), new StringType(accountVisa), "09", "11", StringType.UNSET, "Elmer Fudd", StringType.UNSET, StringType.UNSET, StringType.UNSET);
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
