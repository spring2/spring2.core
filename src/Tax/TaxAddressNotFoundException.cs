using Spring2.Core.Message;

namespace Spring2.Core.Tax {
    public class TaxAddressNotFoundException : TaxException {
	public TaxAddressNotFoundException() : base("Unable to get Tax Rate with the City/Region/Zip Code entered.  Please verify and re-enter.") {
	}

    }
}