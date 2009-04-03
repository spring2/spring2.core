using System;
using Spring2.Core.Geocode.WebService;

namespace Spring2.Core.Geocode.TeleAtlasExe {
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class TeleAtlasCmd {
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    String address = args[0];
	    String[] addressParts = address.Split('|');
	    if (addressParts.Length != 5) {
		throw new ArgumentException("Expected to get address as first argument in format of street|city|state|postalCode|plus4");
	    }
	    String street = addressParts[0];
	    String city = addressParts[1];
	    String state = addressParts[2];
	    String postalCode = addressParts[3];
	    String plus4 = addressParts[4];
	    GeocodeData result;
	    try {
		GeocodeWebServiceWrapper wrapper = new GeocodeWebServiceWrapper();
		result = wrapper.DoGeocode(street, city, state, postalCode, plus4);
	    } catch (Exception ex) {
		result = new GeocodeData();
		result.MatchCount = 0;
		result.MatchType = 0;
		result.OutPut = ex.ToString();
	    }

	    System.Console.Out.Write(result.ToDelimitedString());
	}
    }
}
