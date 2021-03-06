using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.PostageService.Stamps;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService.Test {
    [TestFixture]
    public class StampsProviderTest {
	IConfigurationProvider currentProvider;
	SimpleConfigurationProvider provider;

	[SetUp]
	public void SetUp() {
	    currentProvider = ConfigurationProvider.Instance;
	    provider = new SimpleConfigurationProvider();

	    provider.Settings["PostageService.Stamps.IntegrationId"] = "36f039b2-d233-457d-86b9-54f81aafbe21";
	    provider.Settings["PostageService.Stamps.Password"] = "postage12";
	    provider.Settings["PostageService.Stamps.Username"] = "Spring2-01";
	    provider.Settings["PostageService.Stamps.PostageServerUrl"] = "https://swsim.testing.stamps.com/swsim/SwsimV52.asmx";

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
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.PRIORITYEXPRESS,
		MailpieceShape = MailpieceShapeEnum.LETTER,
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
	public void CalculatePostageRateRequest_FIRSTCLASSPACKAGEINTERNATIONSERVICE() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.FIRSTCLASSMAILINTERNATIONAL,
		MailpieceShape = MailpieceShapeEnum.LETTER,
		WeightOz = 16,
		MailpieceDimensions = new PackageDimensions { Height = 10, Width = 5, Length = 15 },
		Value = 100,
		FromPostalCode = "84070",
		ToPostalCode = "M5G 1R3",
		ToCountry = "CA",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt")
	    };

	    PostageRatesData data = postage.GetPostageRates(input);

	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void CalculatePostageRateRequest_PARCELPOST() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.STANDARDPOST,
		MailpieceShape = MailpieceShapeEnum.LARGEFLATRATEBOX,
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
	public void CalculatePostageRateRequest_FIRSTCLASSPACKAGEINTERNATIONALSERVICE() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.STANDARDPOST,
		MailpieceShape = MailpieceShapeEnum.LARGEFLATRATEBOX,
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
	public void CalculatePostageRatesRequest() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageRateInputData input = new PostageRateInputData() {
		MailpieceShape = MailpieceShapeEnum.LARGEFLATRATEBOX,
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
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageLabelInputData input = new PostageLabelInputData() {
		MailClass = MailClassEnum.PRIORITY,
		WeightOz = 20,		
		MailpieceShape = MailpieceShapeEnum.PACKAGE,
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
		ToPostalCode = "84070",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt"),
		ReferenceID = "123457"
	    };

	    PostageLabelData data = postage.GetPostageLabel(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	    Assert.IsTrue(!string.IsNullOrEmpty(data.Base64LabelImage));
	}

	[Test]
	public void InternationalPostageRequest() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostageLabelInputData input = new PostageLabelInputData() {
		Value = 1,
		MailClass = MailClassEnum.FIRSTCLASSMAILINTERNATIONAL,
		WeightOz = 5,
		MailpieceShape = MailpieceShapeEnum.THICKENVELOPE,
		FromCompany = "Spring2",
		ReturnAddress1 = "10150 S. Centennial Parkway",
		ReturnAddress2 = "Suite 210",
		FromCity = "Sandy",
		FromState = "UT",
		FromPostalCode = "84070",
		FromPhone = "2125551234", //This is required... 
		ToName = "VICTORIA STEELE",
		ToAddress1 = "306 84 AVE SE",
		ToCity = "CALGARY",
		ToState = "AB",
		ToPostalCode = "T2H 1N4",
		ToCountry = "CA",
		ToPhone = "5873511871",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt"),
		ReferenceID = "123457",
		CustomsCountry1 = "US",
		CustomsDescription1 = "Item",
		CustomsQuantity1 = 1,
		CustomsValue1 = 1,
		CustomsWeight1 = 1,
		CustomsInfo = new CustomsInput() {
		    ContentsType = "Merchandise"
		}
	    };

	    PostageLabelData data = postage.GetPostageLabel(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	    Assert.IsTrue(!string.IsNullOrEmpty(data.Base64LabelImage));
	}

	[Test]
	public void BuyPostageRequest() {
	    IPostageServiceProvider postage = new StampsProvider();

	    PostagePurchaseInputData input = new PostagePurchaseInputData() {
		RecreditAmount = "100",
		RequesterID = "100",
		RequestID = "123"
	    };

	    PurchasedPostageData data = postage.BuyPostage(input);
	    Assert.IsNotNull(data);
	}

	[Test]
	public void ResetPassword() {
	    StampsProvider postage = new StampsProvider();
	    ChangePasswordInputData input = new ChangePasswordInputData {
		NewPassword = "postage12",
		RequestId = Guid.NewGuid().ToString()
	    };
	    try {
		PasswordChangedData data = postage.ChangePassword(input);
	    } catch (Exception ex) {
		Assert.IsTrue(ex.Message.Equals("Password is repeat of previously used password."));
	    }
	}

	[Test]
	public void ShouldSupportMultipleProvidersBeingAuthenticated() {
	    IPostageServiceProvider postage1 = new StampsProvider();
	    IPostageServiceProvider postage2 = new StampsProvider();

	    DoRequest(postage1);
	    DoRequest(postage2);
	    DoRequest(postage1);
	    DoRequest(postage2);

	}

	private static void DoRequest(IPostageServiceProvider postage) {
	    PostageRateInputData input = new PostageRateInputData() {
		MailClass = MailClassEnum.PRIORITYEXPRESS,
		MailpieceShape = MailpieceShapeEnum.LETTER,
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
    }
}
