using System;
using NUnit.Framework;
using Spring2.Core.AddressValidation.Stamps;

namespace Spring2.Core.AddressValidation.Test {
    [TestFixture]
    public class StampsAddressValidationTest {
	string integrationId = "36f039b2-d233-457d-86b9-54f81aafbe21";
	string password = "postage12";
	string username = "Spring2-01";
	string url = "https://swsim.testing.stamps.com/swsim/SwsimV52.asmx";
	[Test]
	public void ValidateAddress() {
	    string address1 = "8220 MIDLAND RD";
	    string city = "MENTOR";
	    string state = "OH";
	    string postalCode = "44060";
	    string fullName = "THOMAS YANCHAR";
	    StampsAddressValidationProvider provider = new StampsAddressValidationProvider();
	    provider.SetCredentials(integrationId, password, username, url, fullName);
	    AddressValidationResult result = provider.Validate(address1, null, city, state, postalCode, null);
	    Assert.IsNotNull(result);
	    Assert.IsTrue(result.ResponseType == ResponseTypeEnum.VALID);
	    Assert.IsNotNull(result.Addresses[0]);
	}
    }
    
    public class StampsAddressValidationProviderTest {
	string integrationId = "36f039b2-d233-457d-86b9-54f81aafbe21";
	string password = "postage12";
	string username = "Spring2-01";
	string url = "https://swsim.testing.stamps.com/swsim/SwsimV52.asmx";
	[Test]
	public void ValidateAddress() {
	    StampsAddressValidationProvider addressProvider = new StampsAddressValidationProvider();
	    addressProvider.SetCredentials(integrationId, password, username, url, "THOMAS YANCHAR");
	    AddressValidationResult result = addressProvider.Validate("8220 MIDLAND RD", "MENTOR", "OH", "44060", "United States");
	    Assert.IsNotNull(result);
	    Assert.IsTrue(result.ResponseType == ResponseTypeEnum.VALID);
	    Assert.IsNotNull(result.Addresses[0]);
	}

	[Test]
	public void ValidateInternationalAddress() {
	    StampsAddressValidationProvider addressProvider = new StampsAddressValidationProvider();
	    addressProvider.SetCredentials(integrationId, password, username, url, "VICTORIA STEELE");
	    AddressValidationResult result = addressProvider.ValidateInternational("306 84 AVE SE", "CALGARY", "AB", "T2H", "CA", "5873511871");
	    Assert.IsNotNull(result);
	    Assert.IsTrue(result.ResponseType == ResponseTypeEnum.VALID);
	}
    }
}
