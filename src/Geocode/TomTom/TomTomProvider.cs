using System;
using Spring2.Core.Types;

using Spring2.Core.Geocode.Types;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Dao;
using Spring2.Core.Geocode.BusinessLogic;
using System.IO;
using Newtonsoft.Json;
using Google.Api.Maps.Service;
using Google.Api.Maps.Service.Geocoding;
using Spring2.Core.Configuration;

namespace Spring2.Core.Geocode.TomTom {
    /// <summary>
    /// Summary description for TeleAtlasProvider.
    /// </summary>
    public class TomTomProvider : IGeocodeProvider {
	private String apiKey=String.Empty;

	public TomTomProvider() {
	    apiKey = ConfigurationProvider.Instance.Settings["TomTom.APIKey"];
	}

	public Int32 GetAvailableGeocodeCount() {
	    return -1;
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    Address a = new Address();
	    a.ZIP = zipCode.ToString();
	    a.Country = "USA";

	    GeocodeData geocodeData = DoGeocode(a);
	    return geocodeData;
	}

	private GeocodeData DoGeocode(Address addr) {
	    GeocodeData geocodeData = new GeocodeData();
	    AddressCacheList list = null;

	    if (addr.ZIP.Length > 0) {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndPostalCode(addr.Number + " " + addr.Street, addr.ZIP);
	    } else {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndCityAndState(addr.Number + " " + addr.Street, addr.City, addr.State);
	    }

	    foreach (IAddressCache cache in list) {
		if ((cache.Status.Equals(GeocodeStatusEnum.VALID))) {
		    geocodeData.OutPut = cache.Result;

		    geocodeData.MatchType = cache.MatchType;
		    geocodeData.StdAddress = StringType.Parse(cache.StdAddress1);
		    geocodeData.StdCity = StringType.Parse(cache.StdCity);
		    geocodeData.StdState = StringType.Parse(cache.StdRegion);
		    geocodeData.StdZipCode = StringType.Parse(cache.StdPostalCode);
		    geocodeData.StdZipCodePlus4 = StringType.Parse(cache.StdPlus4);
		    geocodeData.MatchAddress = StringType.Parse(cache.MatAddress1);
		    geocodeData.MatchCity = StringType.Parse(cache.MatCity);
		    geocodeData.MatchState = StringType.Parse(cache.MatRegion);
		    geocodeData.MatchZipCode = StringType.Parse(cache.MatPostalCode);
		    geocodeData.MatchLatitude = cache.Latitude;
		    geocodeData.MatchLongitude = cache.Longitude;

		    if (geocodeData.OutPut.Length>0) {
			if (geocodeData.OutPut.StartsWith("{")) {
			    ParseJsonResult(geocodeData);
			} else {
			    ParseTeleAtlasResult(geocodeData);
			}
		    }
		    return geocodeData;
		}
	    }


	    TomTomClient gc = new TomTomClient(apiKey);
	    var aLocations = gc.CodeAddress(addr, 1);

	    geocodeData.MatchCount = aLocations.Count;
	    Boolean isValid = false;
	    if (aLocations.Count >= 1) {
		var a = aLocations[0];

		geocodeData.StdAddress = a.Number + " " + a.Street;
		geocodeData.StdCity = a.City;
		geocodeData.StdState = GetStateByName(a.State);
		geocodeData.StdZipCode = a.ZIP;
		geocodeData.StdZipCodePlus4 = a.ZIP;

		geocodeData.MatchAddress = a.Number + " " + a.Street;
		geocodeData.MatchCity = a.City;

		
		geocodeData.MatchState = GetStateByName(a.State);
		geocodeData.MatchZipCode = a.ZIP;

		geocodeData.MatchLatitude = a.Position.Latitude;
		geocodeData.MatchLongitude = a.Position.Longitude;
		isValid = true;
	    } else {
		geocodeData.StdAddress = StringType.EMPTY;
		geocodeData.StdCity = StringType.EMPTY;
		geocodeData.StdState = StringType.EMPTY;
		geocodeData.StdZipCode = StringType.EMPTY;
		geocodeData.StdZipCodePlus4 = StringType.EMPTY;

		geocodeData.MatchAddress = StringType.EMPTY;
		geocodeData.MatchCity = StringType.EMPTY;
		geocodeData.MatchState = StringType.EMPTY;
		geocodeData.MatchZipCode = StringType.EMPTY;
	    }

	    StoreResult(addr.Number + " " + addr.Street, addr.City, addr.State, addr.ZIP, geocodeData, isValid);
	    return geocodeData;
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, CountryCodeEnum ccode) {
	    return DoGeocode(street, city, state, postalCode, String.Empty, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode) {
	    Address a = new Address();
	    a.Street = street;
	    a.City = city;
	    a.State = state;
	    a.ZIP = postalCode.ToString();
	    a.Country = ccode.Code;

	    GeocodeData geocodeData = DoGeocode(a);
	    return geocodeData;
	}

	private static void StoreResult(StringType street, StringType city, StringType state, StringType postalCode, GeocodeData geocodeData, Boolean isValid) {
	    AddressCacheData addressCacheData = new AddressCacheData();

	    addressCacheData.Address1 = street;
	    addressCacheData.City = city;
	    addressCacheData.Region = state;
	    addressCacheData.PostalCode = postalCode;
	    addressCacheData.Result = geocodeData.OutPut;
	    addressCacheData.Latitude = geocodeData.MatchLatitude;
	    addressCacheData.Longitude = geocodeData.MatchLongitude;

	    if (isValid) {
		addressCacheData.MatAddress1 = geocodeData.MatchAddress;
		addressCacheData.MatCity = geocodeData.MatchCity;
		addressCacheData.MatRegion = geocodeData.MatchState;
		addressCacheData.MatPostalCode = geocodeData.MatchZipCode;
		addressCacheData.StdAddress1 = geocodeData.StdAddress;
		addressCacheData.StdCity = geocodeData.StdCity;
		addressCacheData.StdRegion = geocodeData.StdState;
		addressCacheData.StdPostalCode = geocodeData.StdZipCode;
	    }

	    AddressCache addressCache = new AddressCache();

	    if (!isValid) {
		addressCacheData.Status = GeocodeStatusEnum.INVALID;
		addressCache.Update(addressCacheData);
	    } else {
		addressCacheData.Status = GeocodeStatusEnum.VALID;
		addressCache.Update(addressCacheData);
	    }
	}

	private Boolean ParseJsonResult(GeocodeData geocodeData) {
	    Boolean isValid = false;

	    using (var stringReader = new StringReader(geocodeData.OutPut)) {
		var jsonReader = new JsonTextReader(stringReader);
		var serializer = new JsonSerializer();
		serializer.Converters.Add(new JsonEnumTypeConverter());
		GeocodingResponse response = serializer.Deserialize<GeocodingResponse>(jsonReader);

		isValid = ConvertGoogleResponse(geocodeData, response, isValid);
	    }

	    return isValid;
	}

	private static Boolean ConvertGoogleResponse(GeocodeData geocodeData, GeocodingResponse response, Boolean isValid) {
	    isValid = response.Status == ServiceResponseStatus.Ok;


	    geocodeData.StdAddress = StringType.EMPTY;
	    geocodeData.StdCity = StringType.EMPTY;
	    geocodeData.StdState = StringType.EMPTY;
	    geocodeData.StdZipCode = StringType.EMPTY;
	    geocodeData.StdZipCodePlus4 = StringType.EMPTY;
	    geocodeData.StdDPBC = StringType.EMPTY;
	    geocodeData.StdCarrier = StringType.EMPTY;
	    geocodeData.MatchAddress = StringType.EMPTY;
	    geocodeData.MatchCity = StringType.EMPTY;
	    geocodeData.MatchState = StringType.EMPTY;
	    geocodeData.MatchZipCode = StringType.EMPTY;

	    if (isValid) {
		geocodeData.MatchLatitude = response.Results[0].Geometry.Location.Latitude;
		geocodeData.MatchLongitude = response.Results[0].Geometry.Location.Longitude;
		foreach (var c in response.Results[0].Components) {
		    if (ContainsAddressType(c.Types, AddressType.StreetNumber)) {
			geocodeData.MatchAddress = c.LongName;
			geocodeData.StdAddress = geocodeData.MatchAddress;
		    }
		    if (ContainsAddressType(c.Types, AddressType.Route)) {
			geocodeData.MatchAddress += " " + c.LongName;
			geocodeData.StdAddress = geocodeData.MatchAddress;
		    }
		    //google returns the components from smallest to largest, so locality would override sub and neighborhood, and sub would override neighborhood.
		    if (ContainsAddressType(c.Types, AddressType.Locality)) {
			geocodeData.MatchCity = c.LongName;
			geocodeData.StdCity = geocodeData.MatchCity;
		    } else if (ContainsAddressType(c.Types, AddressType.Sublocality)) {
			geocodeData.MatchCity = c.LongName;
			geocodeData.StdCity = geocodeData.MatchCity;
		    } else if (ContainsAddressType(c.Types, AddressType.Neighborhood)) {
			geocodeData.MatchCity = c.LongName;
			geocodeData.StdCity = geocodeData.MatchCity;
		    }
		    if (ContainsAddressType(c.Types, AddressType.AdministrativeAreaLevel1)) {
			geocodeData.MatchState = c.ShortName;
			geocodeData.StdState = geocodeData.MatchState;
		    }
		    if (ContainsAddressType(c.Types, AddressType.PostalCode)) {
			geocodeData.MatchZipCode = c.LongName;
			geocodeData.StdZipCode = geocodeData.MatchZipCode;
		    }
		}
	    }

	    return isValid;
	}

	private static Boolean ContainsAddressType(AddressType[] types, AddressType type) {
	    foreach (var t in types) {
		if (t == type) {
		    return true;
		}
	    }
	    return false;
	}

	private void ParseTeleAtlasResult(GeocodeData geocodeData) {
	    TeleAtlasGeocodeData teleAtlasgeocodeData = new TeleAtlasGeocodeData();
	    teleAtlasgeocodeData.OutPut = geocodeData.OutPut;

	    geocodeData.MatchCount = teleAtlasgeocodeData.MatchCount;
	    geocodeData.MatchType = teleAtlasgeocodeData.MatchType;
	    geocodeData.MatchDB = StringType.Parse(teleAtlasgeocodeData.MatchDB);
	    geocodeData.StdAddress = StringType.Parse(teleAtlasgeocodeData.StdAddress);
	    geocodeData.StdCity = StringType.Parse(teleAtlasgeocodeData.StdCity);
	    geocodeData.StdState = StringType.Parse(teleAtlasgeocodeData.StdState);
	    geocodeData.StdZipCode = StringType.Parse(teleAtlasgeocodeData.StdZipCode);
	    geocodeData.StdZipCodePlus4 = StringType.Parse(teleAtlasgeocodeData.StdZipCodePlus4);
	    geocodeData.StdDPBC = StringType.Parse(teleAtlasgeocodeData.StdDPBC);
	    geocodeData.StdCarrier = StringType.Parse(teleAtlasgeocodeData.StdCarrier);
	    geocodeData.MatchAddress = StringType.Parse(teleAtlasgeocodeData.MatchAddress);
	    geocodeData.MatchCity = StringType.Parse(teleAtlasgeocodeData.MatchAddress);
	    geocodeData.MatchState = StringType.Parse(teleAtlasgeocodeData.MatchState);
	    geocodeData.MatchZipCode = StringType.Parse(teleAtlasgeocodeData.MatchZipCode);
	    geocodeData.MatchLatitude = teleAtlasgeocodeData.MatchLatitude;
	    geocodeData.MatchLongitude = teleAtlasgeocodeData.MatchLongitude;
	}

	public String GetStateByName(string name) {
	    switch (name.ToUpper()) {
		case "ALABAMA":
		    return "AL";

		case "ALASKA":
		    return "AK";

		case "AMERICAN SAMOA":
		    return "AS";

		case "ARIZONA":
		    return "AZ";

		case "ARKANSAS":
		    return "AR";

		case "CALIFORNIA":
		    return "CA";

		case "COLORADO":
		    return "CO";

		case "CONNECTICUT":
		    return "CT";

		case "DELAWARE":
		    return "DE";

		case "DISTRICT OF COLUMBIA":
		    return "DC";

		case "FEDERATED STATES OF MICRONESIA":
		    return "FM";

		case "FLORIDA":
		    return "FL";

		case "GEORGIA":
		    return "GA";

		case "GUAM":
		    return "GU";

		case "HAWAII":
		    return "HI";

		case "IDAHO":
		    return "ID";

		case "ILLINOIS":
		    return "IL";

		case "INDIANA":
		    return "IN";

		case "IOWA":
		    return "IA";

		case "KANSAS":
		    return "KS";

		case "KENTUCKY":
		    return "KY";

		case "LOUISIANA":
		    return "LA";

		case "MAINE":
		    return "ME";

		case "MARSHALL ISLANDS":
		    return "MH";

		case "MARYLAND":
		    return "MD";

		case "MASSACHUSETTS":
		    return "MA";

		case "MICHIGAN":
		    return "MI";

		case "MINNESOTA":
		    return "MN";

		case "MISSISSIPPI":
		    return "MS";

		case "MISSOURI":
		    return "MO";

		case "MONTANA":
		    return "MT";

		case "NEBRASKA":
		    return "NE";

		case "NEVADA":
		    return "NV";

		case "NEW HAMPSHIRE":
		    return "NH";

		case "NEW JERSEY":
		    return "NJ";

		case "NEW MEXICO":
		    return "NM";

		case "NEW YORK":
		    return "NY";

		case "NORTH CAROLINA":
		    return "NC";

		case "NORTH DAKOTA":
		    return "ND";

		case "NORTHERN MARIANA ISLANDS":
		    return "MP";

		case "OHIO":
		    return "OH";

		case "OKLAHOMA":
		    return "OK";

		case "OREGON":
		    return "OR";

		case "PALAU":
		    return "PW";

		case "PENNSYLVANIA":
		    return "PA";

		case "PUERTO RICO":
		    return "PR";

		case "RHODE ISLAND":
		    return "RI";

		case "SOUTH CAROLINA":
		    return "SC";

		case "SOUTH DAKOTA":
		    return "SD";

		case "TENNESSEE":
		    return "TN";

		case "TEXAS":
		    return "TX";

		case "UTAH":
		    return "UT";

		case "VERMONT":
		    return "VT";

		case "VIRGIN ISLANDS":
		    return "VI";

		case "VIRGINIA":
		    return "VA";

		case "WASHINGTON":
		    return "WA";

		case "WEST VIRGINIA":
		    return "WV";

		case "WISCONSIN":
		    return "WI";

		case "WYOMING":
		    return "WY";
	    }

	    return name;
	}


    }
}
