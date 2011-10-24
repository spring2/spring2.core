using System;
using System.Net;
using System.Xml.XPath;

using Spring2.Core.Configuration;
using Spring2.Core.Types;

using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Dao;
using Spring2.Core.Geocode.BusinessLogic;
using Spring2.Core.Geocode.Types;
using System.Web;
using System.IO;
using Google.Api.Maps.Service.Geocoding;
using Google.Api.Maps.Service;
using Newtonsoft.Json;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for TeleAtlasProvider.
    /// </summary>
    public class GeocodeV3 : IGeocodeProvider {

	public GeocodeV3() {
	}

	public Int32 GetAvailableGeocodeCount() {
	    return -1;
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType zipCode) {
	    return DoGeocode(StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, zipCode, StringType.DEFAULT, CountryCodeEnum.UNSET);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, CountryCodeEnum ccode) {
	    return DoGeocode(street, city, state, postalCode, String.Empty, ccode);
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode) {
	    GeocodeData geocodeData = new GeocodeData();
	    AddressCacheList list = null;

	    if (postalCode.ToString().Length > 0) {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndPostalCode(street, postalCode);
	    } else {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndCityAndState(street, city, state);
	    }

	    foreach (IAddressCache cache in list) {
		if ((cache.Status.Equals(GeocodeStatusEnum.VALID))) {
		    geocodeData.OutPut = cache.Result;

		    if (geocodeData.OutPut.StartsWith("{")) {
			ParseJsonResult(geocodeData);
		    } else {
			ParseTeleAtlasResult(geocodeData);
		    }
		    return geocodeData;
		}
	    }

	    string address = street + ", " + city + ", " + state + ", " + postalCode;
	    if (ccode.IsValid) {
		address += ", " + ccode.Name;
	    }

	    GeocodingRequest request = new GeocodingRequest();
	    request.Address = address;
	    request.Sensor = "false";
	    geocodeData.OutPut = GeocodingService.GetResponseAsString(request);

	    Boolean isValid = ParseJsonResult(geocodeData);

	    StoreResult(street, city, state, postalCode, geocodeData, isValid);

	    return geocodeData;
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
		    if (c.Types.Contains(AddressType.StreetNumber)) {
			geocodeData.MatchAddress = c.LongName;
			geocodeData.StdAddress = geocodeData.MatchAddress;
		    }
		    if (c.Types.Contains(AddressType.Route)) {
			geocodeData.MatchAddress += " " + c.LongName;
			geocodeData.StdAddress = geocodeData.MatchAddress;
		    }
		    if (c.Types.Contains(AddressType.Locality)) {
			geocodeData.MatchCity = c.LongName;
			geocodeData.StdCity = geocodeData.MatchCity;
		    }
		    if (c.Types.Contains(AddressType.AdministrativeAreaLevel1)) {
			geocodeData.MatchState = c.ShortName;
			geocodeData.StdState = geocodeData.MatchState;
		    }
		    if (c.Types.Contains(AddressType.PostalCode)) {
			geocodeData.MatchZipCode = c.LongName;
			geocodeData.StdZipCode = geocodeData.MatchZipCode;
		    }
		}
	    }

	    return isValid;
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



    }
}

