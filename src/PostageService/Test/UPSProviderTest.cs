using System;
using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.PostageService.UPS;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService.Test {
    [TestFixture]
    public class UPSProviderTest {
	IConfigurationProvider currentProvider;
	SimpleConfigurationProvider provider;

	[SetUp]
	public void SetUp() {
	    currentProvider = ConfigurationProvider.Instance;
	    provider = new SimpleConfigurationProvider();

	    provider.Settings["PostageService.UPS.AccessLicenseNumber"] = "CD15284140419C09";
	    provider.Settings["PostageService.UPS.Username"] = "premiumdist";
	    provider.Settings["PostageService.UPS.Password"] = "Password#1";
	    provider.Settings["PostageService.UPS.ShipperNumber"] = "05984W";
	    provider.Settings["PostageService.UPS.ShipperName"] = "Premium Distribution";
	    provider.Settings["PostageService.UPS.costCenter"] = "PremiumDistribution";
	    provider.Settings["PostageService.UPS.PostageServerUrl"] = "https://wwwcie.ups.com/webservices/Ship";

	    provider.Settings.Add(currentProvider.Settings);
	    ConfigurationProvider.SetProvider(provider);
	}

	[TearDown]
	public void TearDown() {
	    ConfigurationProvider.SetProvider(currentProvider);
	    PostageServiceManager.Reset();
	}

	[Test]
	public void PostageLabelRequest() {
	    IPostageServiceProvider postage = new UPSProvider();

	    PostageLabelInputData input = new PostageLabelInputData() {
		MailClass = MailClassEnum.FIRST,
		WeightOz = 5,		
		MailpieceShape = MailpieceShapeEnum.THICKENVELOPE,
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
		ToPhone = "8015555555",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt"),
		ReferenceID = "123457"
	    };

	    PostageLabelData data = postage.GetPostageLabel(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}

	[Test]
	public void MIPostageLabelRequest() {
	    IPostageServiceProvider postage = new UPSProvider();

	    PostageLabelInputData input = new PostageLabelInputData() {
		MailClass = MailClassEnum.UPSMIEXPEDITED,
		WeightOz = 7,
		MailpieceShape = MailpieceShapeEnum.IRREGULARPARCEL,
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
		ToPhone = "8015555555",
		ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		ShipTime = DateTime.Now.ToString("hh:mm tt"),
		ReferenceID = "123457"
	    };

	    PostageLabelData data = postage.GetPostageLabel(input);
	    Assert.IsNotNull(data);
	    Console.WriteLine(data.ErrorMessage);
	    Assert.IsTrue(string.IsNullOrEmpty(data.ErrorMessage));
	}
    }
}
