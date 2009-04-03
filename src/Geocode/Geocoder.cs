using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for Geocode.
    /// </summary>
    public class Geocoder {

	private Geocode geocode = null;
	public Geocoder() {
	    geocode = new GeocodeWrapper();
	}

	public GeocodeData performGeocode(StringType street, StringType city,StringType state,StringType zipCode,StringType path){
	    return geocode.DoGeocode(street, city, state, zipCode,path);
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode){
	    return geocode.DoGeocode(StringType.EMPTY,StringType.EMPTY, StringType.EMPTY, zipCode,StringType.DEFAULT);
	}
    }
}
