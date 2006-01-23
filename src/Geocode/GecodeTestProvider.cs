using System;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GecodeTestProvider.
    /// </summary>
    public class GecodeTestProvider : IGeocodeProvider {
	public GecodeTestProvider() {
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    return DoGeocode(StringType.EMPTY,StringType.EMPTY, StringType.EMPTY, zipCode,StringType.DEFAULT);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path) {
	    GeocodeData data = new GeocodeData();
	    data.MatchAddress = street;
	    data.MatchCity = city;
	    data.MatchState = state;
	    data.MatchZipCode = postalCode;
	    data.StdAddress = street;
	    data.StdCity = city;
	    data.StdState = state;
	    data.StdZipCode = postalCode;
	    data.OutPut = StringType.EMPTY;
	    data.MatchLatitude = DecimalType.Parse(ConfigurationProvider.Instance.Settings["DefaultLatitude"]);
	    data.MatchLongitude = DecimalType.Parse(ConfigurationProvider.Instance.Settings["DefaultLongitude"]);
	    return data;
	}

    }
}
