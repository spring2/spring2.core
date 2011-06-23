using System;
using Spring2.Core.Types;

using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode.WebService {
    /// <summary>
    /// Summary description for TeleAtlasWebServiceProvider.
    /// </summary>
    public class TeleAtlasWebServiceProvider : IGeocodeProvider {
	public TeleAtlasWebServiceProvider() {
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    throw new Exception("Function not implemented.");
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, CountryCodeEnum ccode) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, StringType plus4, CountryCodeEnum ccode) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, plus4, ccode);
	}

	public Int32 GetAvailableGeocodeCount() {
	    return 0;
	}
    }
}
