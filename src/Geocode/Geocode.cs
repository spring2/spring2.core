using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for Geocode.
    /// </summary>
    public abstract class Geocode {

	protected StringType street;
	protected StringType city;
	protected StringType state;
	protected StringType zipCode;
	protected StringType password;
	protected StringType userName;

	public abstract GeocodeData DoGeocode(StringType street, StringType city,StringType state,StringType zipCode,StringType path);
	public abstract GeocodeData GetCityAndStateOfZipCode(StringType zipCode);
    }
}
