using System;
using Spring2.Core.Types;

using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for IGeocodeProvider.
    /// </summary>
    public interface IGeocodeProvider {
	GeocodeData GetCityAndStateOfZipCode(StringType postalCode);
	GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode);
	IntegerType GetAvailableGeocodeCount();
    }
}
