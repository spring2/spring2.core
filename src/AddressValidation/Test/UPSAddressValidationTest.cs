using System;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.AddressValidation;
using Spring2.Core.AddressValidation.UPS;

namespace Spring2.Core.Test {
    [TestFixture()]
    public class UPSAddressValidationTest {
	[Test()]
	public void ShouldValidateValidAddress() {
	    UPSAddressValidationProvider provider = new UPSAddressValidationProvider();
	    Console.WriteLine(provider.GetType().ToString());
	    AddressValidationResult result = provider.Validate("10150 CENTENNIAL PARKWAY", "STE 420", "", "SANDY", "UT", "84070", "US");
	    Assert.AreEqual(ResponseTypeEnum.VALID, result.ResponseType);
	    Assert.AreEqual(1, result.Addresses.Count);
	    Assert.AreEqual("10150 CENTENNIAL PKWY", result.Addresses[0].Street1.ToString());
	    Assert.AreEqual("STE 420", result.Addresses[0].Street2.ToString());
	    Assert.AreEqual(StringType.DEFAULT, result.Addresses[0].Street3);
	    Assert.AreEqual("SANDY", result.Addresses[0].City.ToString());
	    Assert.AreEqual("UT", result.Addresses[0].State.ToString());
	    Assert.AreEqual("84070-4273", result.Addresses[0].PostalCode.ToString());
	    Assert.AreEqual("US", result.Addresses[0].Country.ToString());
	}

	[Test()]
	public void ShouldNotValidateInvalidAddress() {
	    UPSAddressValidationProvider provider = new UPSAddressValidationProvider();
	    AddressValidationResult result = provider.Validate("123 Dummy Street", "", "", "No City", "ZZ", "55555", "US");
	    Assert.AreEqual(ResponseTypeEnum.INVALID, result.ResponseType);
	    Assert.AreEqual(0, result.Addresses.Count);
	}

	[Test()]
	public void AmbiguousAddress() {
	    UPSAddressValidationProvider provider = new UPSAddressValidationProvider();
	    AddressValidationResult result = provider.Validate("8035 N.W. 5th Street", "", "", "Terrebonne", "OR", "97760", "US");
	    Assert.AreEqual(ResponseTypeEnum.AMBIGUOUS, result.ResponseType);
	    Assert.AreEqual(8, result.Addresses.Count);
	    for (int i = 0; i < result.Addresses.Count; i++) {
		if (i == 0) {
		    Assert.IsFalse(result.Addresses[i].BlockRange.ToBoolean());
		} else {
		    Assert.IsTrue(result.Addresses[i].BlockRange.ToBoolean());
		}
	    }
	}
    }
}
