using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.WebService {
    /// <summary>
    /// Summary description for TeleAtlasWebServiceProvider.
    /// </summary>
    public class TeleAtlasWebServiceProvider : IGeocodeProvider {
	public TeleAtlasWebServiceProvider() {
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.GetCityAndStateOfZipCode(zipCode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, path);
	}

	public IntegerType GetAvailableGeocodeCount() {
	    return new IntegerType(0);
	}
    }
}
