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
	    string address1 = "10150 S. Centennial Parkway";
	    string address2 = "Suite 310";
	    string city = "Sandy";
	    string state = "UT";
	    string postalCode = "84070";
	    string fullName = "Elmer Fudd";
	    StampsAddressValidationProvider provider = new StampsAddressValidationProvider();
	    provider.SetCredentials(integrationId, password, username, url, fullName);
	    AddressValidationResult result = provider.Validate(address1, address2, city, state, postalCode, null);
	    Assert.IsNotNull(result);
	    Assert.IsTrue(result.ResponseType == ResponseTypeEnum.VALID_CITYSTATEZIP);
	    Assert.IsNotNull(result.Addresses[0]);
	}
    }
}
