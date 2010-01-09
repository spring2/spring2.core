using System;
using Spring2.Core.Types;

using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for TeleAtlasProvider.
    /// </summary>
    public class TeleAtlasProvider : IGeocodeProvider {
	public TeleAtlasProvider() {
	}

	public IntegerType GetAvailableGeocodeCount() {
	    GeocodeWrapper wrapper = new GeocodeWrapper();
	    return wrapper.GetGeocodeCount();
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    GeocodeWrapper wrapper = new GeocodeWrapper();
	    return wrapper.GetCityAndStateOfZipCode(zipCode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, CountryCodeEnum ccode) {
	    return DoGeocode (street, city, state, postalCode, String.Empty, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode) {
	    GeocodeWrapper wrapper = new GeocodeWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, path, ccode);
	}
    }
}
