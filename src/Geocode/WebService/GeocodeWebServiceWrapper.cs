using System; 
using System.IO;
using System.Text; 
using System.Diagnostics; 
using System.Threading; 
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.BusinessLogic;
using Spring2.Core.Geocode.Dao;
using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode.WebService {
    /// <summary>
    /// Summary description for GecodeWebServiceWrapper.
    /// </summary>
    public class GeocodeWebServiceWrapper {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	protected StringType street;
	protected StringType city;
	protected StringType state;
	protected StringType zipCode;
	protected StringType plus4;
	protected StringType password;
	protected StringType userName;
	protected StringType host;
	protected StringType serviceName;

	// Tele Atlas specific variables
	//authentication credential token
	private IntegerType credential = IntegerType.UNSET;
	
	/// <summary>
	/// Creates the credential it uses to authenticate against the Tele Atlas Servers
	/// </summary>
	private IntegerType Credential {
	    get {
		if (!credential.IsValid) {
		    this.password = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodePassword"]);
		    this.userName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeUserName"]);
		    this.host = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeHost"]);
		    int retval = TeleAtlasAuthenticator.Instance.Authenticate(this.userName, this.password, this.host);
		    this.credential = new IntegerType(retval);
		}
		return this.credential;
	    }
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType plus4, CountryCodeEnum ccode){
	    TeleAtlasGeocodeWebServiceData teleAtlasgeocodeData = new TeleAtlasGeocodeWebServiceData();
	    GeocodeData geocodeData = new GeocodeData();
	    AddressCacheList list = null;
	    AddressCacheData addressCacheData;
	    AddressCache addressCache;

	    if (ccode.Equals(CountryCodeEnum.UNITED_STATES)) {
		this.serviceName = "USA_Geo_002";
	    } else if (ccode.Equals(CountryCodeEnum.CANADA)) {
		this.serviceName = "CAN_Geo_001";
	    } else {
		throw new Exception(string.Format("DoGeocode() received an invalid country code: {0}", ccode.Name));
	    }

	    if(postalCode.ToString().Length > 0){
		list = AddressCacheDAO.DAO.FindAddressByStreetAndPostalCode(street,postalCode);		
	    }else {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndCityAndState(street,city,state);
	    }

	    foreach (IAddressCache cache in list) {
		if((cache.Status.Equals(GeocodeStatusEnum.VALID))){
		    teleAtlasgeocodeData.DBOutPut = cache.Result; // the DBOutPut property will call the conversion method, if necessary

		    // if the string in the Result column of AddressCache is in the legacy format
		    // then go throw the motions of converting it
		    if (cache.Result.StartsWith("Match Count")) {
			geocodeData.OutPut = StringType.Parse(teleAtlasgeocodeData.DBOutPut);
			geocodeData.MatchCount = 1;
			geocodeData.MatchType = teleAtlasgeocodeData.MatchType;
			geocodeData.StdAddress = StringType.Parse(teleAtlasgeocodeData.StdAddress);
			geocodeData.StdCity = StringType.Parse(teleAtlasgeocodeData.StdCity);
			geocodeData.StdState = StringType.Parse(teleAtlasgeocodeData.StdState);
			geocodeData.StdZipCode = StringType.Parse(teleAtlasgeocodeData.StdZipCode);
			geocodeData.StdZipCodePlus4 = teleAtlasgeocodeData.StdZipCodePlus4.Length > 0 ? StringType.Parse(teleAtlasgeocodeData.StdZipCodePlus4) : StringType.EMPTY;
			geocodeData.MatchAddress = StringType.Parse(teleAtlasgeocodeData.MatchAddress);
			geocodeData.MatchCity = StringType.Parse(teleAtlasgeocodeData.MatchAddress);
			geocodeData.MatchState = StringType.Parse(teleAtlasgeocodeData.MatchState);
			geocodeData.MatchZipCode = StringType.Parse(teleAtlasgeocodeData.MatchZipCode);
			geocodeData.MatchLatitude = teleAtlasgeocodeData.MatchLatitude;
			geocodeData.MatchLongitude = teleAtlasgeocodeData.MatchLongitude;

			addressCacheData = new AddressCacheData(); 

			addressCacheData.Address1 = street;
			addressCacheData.City = city;
			addressCacheData.Region = state;
			addressCacheData.PostalCode = postalCode;
			addressCacheData.Result = teleAtlasgeocodeData.DBOutPut;
			addressCacheData.Latitude = teleAtlasgeocodeData.MatchLatitude;
			addressCacheData.Longitude = teleAtlasgeocodeData.MatchLongitude;
			addressCacheData.StdAddress1 = teleAtlasgeocodeData.StdAddress;
			addressCacheData.StdCity = teleAtlasgeocodeData.StdCity;
			addressCacheData.StdRegion = teleAtlasgeocodeData.StdState;
			addressCacheData.StdPostalCode = teleAtlasgeocodeData.StdZipCode;
			addressCacheData.StdPlus4 = teleAtlasgeocodeData.StdZipCodePlus4;
			addressCacheData.MatchType = teleAtlasgeocodeData.MatchType;
			addressCacheData.MatAddress1 = teleAtlasgeocodeData.MatchAddress;
			addressCacheData.MatCity = teleAtlasgeocodeData.MatchCity;
			addressCacheData.MatRegion = teleAtlasgeocodeData.MatchState;
			addressCacheData.MatPostalCode = teleAtlasgeocodeData.MatchZipCode;

			addressCache = AddressCache.GetInstance(cache.AddressId);
			addressCache.Update(addressCacheData);
		    } else { // The contents of the Result column are not in the legacy format...use the db columns
			geocodeData.OutPut = StringType.Parse(teleAtlasgeocodeData.DBOutPut);
			geocodeData.MatchCount = 1;
			geocodeData.MatchType = cache.MatchType;
			geocodeData.StdAddress = cache.StdAddress1;
			geocodeData.StdCity = cache.StdCity;
			geocodeData.StdState = cache.StdRegion;
			geocodeData.StdZipCode = cache.StdPostalCode;
			geocodeData.StdZipCodePlus4 = cache.StdPlus4;
			geocodeData.MatchAddress = cache.MatAddress1;
			geocodeData.MatchCity = cache.MatCity;
			geocodeData.MatchState = cache.MatRegion;
			geocodeData.MatchZipCode = cache.MatPostalCode;
			geocodeData.MatchLatitude = cache.Latitude;
			geocodeData.MatchLongitude = cache.Longitude;
		    }

		    return geocodeData;
		}
	    }

	    this.street = street;
	    this.city = city;
	    this.state = state;
	    this.zipCode = postalCode;
	    this.plus4 = plus4;

	    teleAtlasgeocodeData = ExecuteWebServiceCommand();

	    addressCacheData = new AddressCacheData(); 

	    addressCacheData.Address1 = street;
	    addressCacheData.City = city;
	    addressCacheData.Region = state;
	    addressCacheData.PostalCode = postalCode;
	    addressCacheData.Result = teleAtlasgeocodeData.DBOutPut;
	    addressCacheData.Latitude = teleAtlasgeocodeData.MatchLatitude;
	    addressCacheData.Longitude = teleAtlasgeocodeData.MatchLongitude;
	    addressCacheData.StdAddress1 = teleAtlasgeocodeData.StdAddress;
	    addressCacheData.StdCity = teleAtlasgeocodeData.StdCity;
	    addressCacheData.StdRegion = teleAtlasgeocodeData.StdState;
	    addressCacheData.StdPostalCode = teleAtlasgeocodeData.StdZipCode;
	    addressCacheData.StdPlus4 = teleAtlasgeocodeData.StdZipCodePlus4;
	    addressCacheData.MatAddress1 = teleAtlasgeocodeData.MatchAddress;
	    addressCacheData.MatCity = teleAtlasgeocodeData.MatchCity;
	    addressCacheData.MatRegion = teleAtlasgeocodeData.MatchState;
	    addressCacheData.MatPostalCode = teleAtlasgeocodeData.MatchZipCode;
	    addressCacheData.MatchType = teleAtlasgeocodeData.MatchType;

	    if (list.Count > 0) {
		addressCache = AddressCache.GetInstance(list[0].AddressId);
	    } else {
		addressCache = new AddressCache();
	    }

	    // Match Codes 10-17 and -1, -10 are Non Match/Errors codes
	    switch (teleAtlasgeocodeData.MatchType) {
		case 1:
		case 2:
		case 3:
		    // Tele Atlas spec does not specify a Match Code 4
		case 5:
		case 6:
		case 7:
		    addressCacheData.Status = GeocodeStatusEnum.VALID;
		    break;
		default: 
		    addressCacheData.Status = GeocodeStatusEnum.INVALID; 
		    break;
	    }

	    addressCache.Update(addressCacheData);

	    geocodeData.OutPut = StringType.Parse(teleAtlasgeocodeData.DBOutPut);
	    if (addressCacheData.Status.Equals(GeocodeStatusEnum.VALID)) {
	    	geocodeData.MatchCount = 1;
	    } else {
	    	geocodeData.MatchCount = 0;
	    }
	    geocodeData.MatchType = teleAtlasgeocodeData.MatchType;
	    geocodeData.StdAddress = StringType.Parse(teleAtlasgeocodeData.StdAddress);
	    geocodeData.StdCity = StringType.Parse(teleAtlasgeocodeData.StdCity);
	    geocodeData.StdState = StringType.Parse(teleAtlasgeocodeData.StdState);
	    geocodeData.StdZipCode = StringType.Parse(teleAtlasgeocodeData.StdZipCode);
	    geocodeData.StdZipCodePlus4 = StringType.Parse(teleAtlasgeocodeData.StdZipCodePlus4);
	    geocodeData.MatchAddress = StringType.Parse(teleAtlasgeocodeData.MatchAddress);
	    geocodeData.MatchCity = StringType.Parse(teleAtlasgeocodeData.MatchCity);
	    geocodeData.MatchState = StringType.Parse(teleAtlasgeocodeData.MatchState);
	    geocodeData.MatchZipCode = StringType.Parse(teleAtlasgeocodeData.MatchZipCode);
	    geocodeData.MatchLatitude = teleAtlasgeocodeData.MatchLatitude;
	    geocodeData.MatchLongitude = teleAtlasgeocodeData.MatchLongitude;

	    return geocodeData;
	}

	/// <summary>
	/// Calls the Tele Atlas web service function to validate the address
	/// </summary>
	/// <returns>TeleAtlasGeocodeData</returns>
	private TeleAtlasGeocodeWebServiceData ExecuteWebServiceCommand() {
	    TeleAtlasGeocodeWebServiceData data = new TeleAtlasGeocodeWebServiceData();

	    com.teleatlas.na.ezlocate.geocoding.NameValue[] address = MakeGeocodeParamString();
	    log.Info("Geocode input: " + address.ToString());

	    if (Credential != 0) {
		com.teleatlas.na.ezlocate.geocoding.Geocode returnedGeocode;
		com.teleatlas.na.ezlocate.geocoding.Geocoding geoCoder = new Spring2.Core.com.teleatlas.na.ezlocate.geocoding.Geocoding();
		geoCoder.Url = "http://" + this.host + "/axis/services/Geocoding";
		long retval = geoCoder.findAddress(this.serviceName, address, Credential.ToInt32(), out returnedGeocode);
		if (retval == 0) {
		    if (returnedGeocode.resultCode == 0) {
			for (int i = 0; i < returnedGeocode.mAttributes.Length; i++) {
			    // this piece of code is looping through the Tele Atlas Name/Value pair
			    // return object and putting it into a hash table for easy access
			    data.OutPut.Add(returnedGeocode.mAttributes[i].name, returnedGeocode.mAttributes[i].value);
			}
			data.ParseData(); // this tells the class to take action with the hashed data
		    }
		} else { 
		    log.Info("The webservice call to findAddress failed.  The return value was " + retval.ToString() + ".");
		}
	    } else {
		log.Info("An error occurred authenticating with the Tele Atlas server.");
	    }

	    return data;
	}


	/// <summary>
	/// Creates the input string passed to Tele Atlas for verification
	/// </summary>
	/// <returns>com.teleatlas.na.ezlocate.geocoding.NameValue[]</returns>
	private com.teleatlas.na.ezlocate.geocoding.NameValue[] MakeGeocodeParamString(){
	    com.teleatlas.na.ezlocate.geocoding.NameValue[] retval = new com.teleatlas.na.ezlocate.geocoding.NameValue[5];
	    retval[0] = NewNamedPair("Addr", street.ToString());
	    retval[1] = NewNamedPair("City", city.ToString());

	    if (this.serviceName == "USA_Geo_002") {
		retval[2] = NewNamedPair("State", state.ToString());
		retval[3] = NewNamedPair("ZIP", zipCode.ToString());
		retval[4] = NewNamedPair("Plus4", plus4.ToString());
	    } else if (this.serviceName == "CAN_Geo_001") {
		retval[2] = NewNamedPair("Prov", state.ToString());
		retval[3] = NewNamedPair("FSA", zipCode.ToString());
		retval[4] = NewNamedPair("LDU", String.Empty);
	    } else {
		throw new Exception(string.Format("MakeGeocodeParamString() received an invalid service name: {0}", this.serviceName));
	    }

	    return retval;
	}

	/// <summary>
	/// creates the name value pair used as input to the Tele Atlas web service
	/// 	/// </summary>
	/// <param name="name"></param>
	/// <param name="data"></param>
	/// <returns>com.teleatlas.na.ezlocate.geocoding.NameValue</returns>
	private com.teleatlas.na.ezlocate.geocoding.NameValue NewNamedPair(string name, string data) {
	    com.teleatlas.na.ezlocate.geocoding.NameValue returnValue = new com.teleatlas.na.ezlocate.geocoding.NameValue();
	    returnValue.name = name;
	    returnValue.value = data;
	    return returnValue;
	}
    }
}
