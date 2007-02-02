using System;
using Spring2.Core.Types;

namespace Spring2.Dss.AddressValidation.Test {
    public class TestAddressValidationProvider: IAddressValidationProvider {
	public AddressValidationResult Validate(StringType street, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    AddressValidationResult result = new AddressValidationResult();
	    result.ResponseType = ResponseTypeEnum.VALID;
	    return result;
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    AddressValidationResult result = new AddressValidationResult();
	    result.ResponseType = ResponseTypeEnum.VALID;
	    return result;
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType street3, StringType city, StringType state, StringType postalCode, StringType countryCode) {
	    AddressValidationResult result = new AddressValidationResult();
	    result.ResponseType = ResponseTypeEnum.VALID;
	    return result;
	}
    }
}
