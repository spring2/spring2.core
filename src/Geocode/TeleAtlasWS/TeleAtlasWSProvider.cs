using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.TeleAtlasWS {
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class TeleAtlasWSProvider : IGeocodeProvider {
	public TeleAtlasWSProvider() {
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    throw new NotSupportedException();
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, StringType plus4) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, plus4);
	}

	public IntegerType GetAvailableGeocodeCount() {
	    throw new NotSupportedException();
	}
    }
}
