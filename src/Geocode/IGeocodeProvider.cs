using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for IGeocodeProvider.
    /// </summary>
    public interface IGeocodeProvider {
	Spring2.Core.Geocode.GeocodeData GetCityAndStateOfZipCode(StringType zipCode);
	GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path);
    }
}
