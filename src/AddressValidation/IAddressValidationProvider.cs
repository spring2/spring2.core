using Spring2.Core.Types;

namespace Spring2.Core.AddressValidation {
    public interface IAddressValidationProvider {
	AddressValidationResult Validate(StringType street, StringType city, StringType state, StringType postalCode, StringType countryCode);
	AddressValidationResult Validate(StringType street1, StringType street2, StringType city, StringType state, StringType postalCode, StringType countryCode);
	AddressValidationResult Validate(StringType street1, StringType street2, StringType street3, StringType city, StringType state, StringType postalCode, StringType countryCode);
    }
}