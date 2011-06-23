using System;
using Spring2.Core.Types;

using Spring2.Core.Geocode.Types;

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

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, CountryCodeEnum ccode) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, StringType.EMPTY, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, StringType plus4, CountryCodeEnum ccode) {
	    TeleAtlasCmdWrapper wrapper = new TeleAtlasCmdWrapper();
	    return wrapper.DoGeocode(street, city, state, postalCode, plus4, ccode);
	}

	public Int32 GetAvailableGeocodeCount() {
	    throw new NotSupportedException();
	}
    }
}
