using System;

namespace Spring2.Core.Shipping {
    /// <summary>
    /// Summary description for AddressValidationConfigurationException.
    /// </summary>
    public class ShippingConfigurationException : AddressValidationException {
        public ShippingConfigurationException(String message): base(message) {
	}
    }
}
