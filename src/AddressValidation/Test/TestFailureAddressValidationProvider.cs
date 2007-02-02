using System;

namespace Spring2.Core.AddressValidation.Test {
    public class TestFailureAddressValidationProvider: IAddressValidationProvider {
	public AddressValidationResult Validate(Spring2.Core.Types.StringType street, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType countryCode) {
	    throw new NotImplementedException();
	}

	AddressValidationResult Spring2.Core.AddressValidation.IAddressValidationProvider.Validate(Spring2.Core.Types.StringType street1, Spring2.Core.Types.StringType street2, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType countryCode) {
	    throw new NotImplementedException();
	}

	AddressValidationResult Spring2.Core.AddressValidation.IAddressValidationProvider.Validate(Spring2.Core.Types.StringType street1, Spring2.Core.Types.StringType street2, Spring2.Core.Types.StringType street3, Spring2.Core.Types.StringType city, Spring2.Core.Types.StringType state, Spring2.Core.Types.StringType postalCode, Spring2.Core.Types.StringType countryCode) {
	    throw new NotImplementedException();
	}
    }
}
