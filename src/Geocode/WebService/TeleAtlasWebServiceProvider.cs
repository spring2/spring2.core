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
	    throw new Exception("Function not implemented.");
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, StringType plus4) {
	    GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, plus4);
	}

	public IntegerType GetAvailableGeocodeCount() {
	    return new IntegerType(0);
	}
    }
}
