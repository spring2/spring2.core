using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.PostageService.Endicia;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService.Test {
    [TestFixture]
    public class EndiciaProviderTest {
	IConfigurationProvider currentProvider;
	SimpleConfigurationProvider provider;

	[SetUp]
	public void SetUp() {
	    currentProvider = ConfigurationProvider.Instance;
	    provider = new SimpleConfigurationProvider();

	    provider.Settings["PostageService.Endicia.AccountId"] = "123123";
	    provider.Settings["PostageService.Endicia.Password"] = "password";
	    provider.Settings["PostageService.Endicia.PartnerId"] = "123123";

	    provider.Settings.Add(currentProvider.Settings);
	    ConfigurationProvider.SetProvider(provider);
	}

	[TearDown]
	public void TearDown() {
	    ConfigurationProvider.SetProvider(currentProvider);
	    PostageServiceManager.Reset();
	}

	[Test]
	public void CalculatePostageRateRequest() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.PRIORITYEXPRESS,
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "11375",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRateData data = postage.GetPostageRate(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void CalculatePostageRateRequest_FIRSTCLASSPACKAGEINTERNATIONSERVICE() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.FIRSTCLASSMAILINTERNATIONAL,
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "M5G 1R3",
		ToCountry = "Canada",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRateData data = postage.GetPostageRate(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void CalculatePostageRateRequest_PARCELPOST() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.STANDARDPOST,
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "11375",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRateData data = postage.GetPostageRate(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void CalculatePostageRateRequest_FIRSTCLASSPACKAGEINTERNATIONALSERVICE() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.STANDARDPOST,
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "11375",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRateData data = postage.GetPostageRate(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void CalculatePostageRatesRequest() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		MailClass = MailClassEnum.DOMESTIC,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "11375",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRatesData data = postage.GetPostageRates(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void PostageLabelRequest() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostageLabelInputData input = new PostageLabelInputData() {
		Test = true,
		MailClass = MailClassEnum.PRIORITYEXPRESS,
		WeightOz = 50,
		MailpieceShape = MailpieceShapeEnum.PARCEL,
		ReferenceID = "abc", // This should be the orderId
		FromCompany = "Spring2",
		ReturnAddress1 = "10150 S. Centennial Parkway",
		ReturnAddress2 = "Suite 210",
		FromCity = "Sandy",
		FromState = "UT",
		FromPostalCode = "84070",
		FromPhone = "2125551234", //This is required... 
		ToName = "asdf asdf",
		ToAddress1 = "10150 S. Centennial Parkway",
		ToAddress2 = "Suite 310",
		ToCity = "Sandy",
		ToState = "UT",
		ToPostalCode ="84070",
		PartnerCustomerID = "123123", //UserId of the logged in user
		PartnerTransactionID = "987987", //OrderId of the order being shipped.
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageLabelData data = postage.GetPostageLabel(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	    Assert.IsTrue(!string.IsNullOrEmpty(data.Base64LabelImage));
	}

	[Test]
	public void BuyPostageRequest() {
	    IPostageServiceProvider postage = new EndiciaProvider();

	    PostagePurchaseInputData input = new PostagePurchaseInputData() {
		RecreditAmount = "100",
		RequesterID = "100",
		RequestID = "123"
	    };

	    PurchasedPostageData data = postage.BuyPostage(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(data.ErrorMessage.Contains("The data for RecreditRequest XML (BuyPostage) has been validated"));
	}

	[Test]
	public void ResetPassword() {
	    EndiciaProvider postage = new EndiciaProvider();
	    ChangePasswordInputData input = new ChangePasswordInputData {
		NewPassword = "abc123",
		RequestId = Guid.NewGuid().ToString()
	    };

	    PasswordChangedData data = postage.ChangePassword(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(data.ErrorMessage.Contains("The data for ChangePassPhraseRequest XML (ChangePassPhrase) has been validated."));

	}

    }
}
