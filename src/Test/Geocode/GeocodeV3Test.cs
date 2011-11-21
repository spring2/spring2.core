using System;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Geocode;
using Spring2.Core.Geocode.WebService;
using Spring2.Core.Geocode.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Test {
    /// <summary>
    /// Tests for Google Geocode API V3
    /// </summary>
    [TestFixture]
    public class GeocodeV3Test {

	IGeocodeProvider current = null;

	[SetUp]
	public void Setup() {
	    current = GeocodeProvider.Instance;
	    GeocodeProvider.SetProvider(new GeocodeV3());
	}

	[TearDown]
	public void TearDown() {
	    if (current != null) {
		GeocodeProvider.SetProvider(current);
	    }
	}

	[Test]
	public void GetGeocodeForStandardAddress() {
	    StringType street = "10150 S. Centennial Parkway";
	    StringType city = "Sandy";
	    StringType zip = "84070";
	    StringType state = "UT";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, city, state, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.5671180), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.8957300), geocode.MatchLongitude);
	    Assert.AreEqual("10150 CENTENNIAL PKWY", geocode.StdAddress.ToString().Trim().ToUpper());
	    Assert.AreEqual("SANDY", geocode.StdCity.ToString().Trim().ToUpper());
	    Assert.AreEqual("84070", geocode.StdZipCode.ToString().Trim().ToUpper());
	    Assert.AreEqual("UT", geocode.StdState.ToString().Trim().ToUpper());
	    //Assert.AreEqual("4103", geocode.StdZipCodePlus4.ToString().Trim().ToUpper());
	}

	[Test]
	public void ShouldFallThroughToSubLocality() {
	    StringType street = "109 Front St";
	    StringType zip = "11201";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, StringType.UNSET, StringType.UNSET, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.70252210), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-73.98929950), geocode.MatchLongitude);
	    Assert.AreEqual("109 FRONT ST", geocode.StdAddress.ToString().Trim().ToUpper());
	    Assert.AreEqual("BROOKLYN", geocode.StdCity.ToString().Trim().ToUpper());
	    Assert.AreEqual("11201", geocode.StdZipCode.ToString().Trim().ToUpper());
	    Assert.AreEqual("NY", geocode.StdState.ToString().Trim().ToUpper());
	}

	[Test]
	public void WhatIsWrongWithBrooklyn() {
	    StringType street = "133 Sterling Pl";
	    StringType zip = "11217";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, StringType.UNSET, StringType.UNSET, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.70252210), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-73.98929950), geocode.MatchLongitude);
	    Assert.AreEqual("133 STERLING PL", geocode.StdAddress.ToString().Trim().ToUpper());
	    Assert.AreEqual("BROOKLYN", geocode.StdCity.ToString().Trim().ToUpper());
	    Assert.AreEqual("11201", geocode.StdZipCode.ToString().Trim().ToUpper());
	    Assert.AreEqual("NY", geocode.StdState.ToString().Trim().ToUpper());
	}

	[Test]
	public void ShouldHandleBadGeoCodeData() {
	    StringType street = "bad address";
	    StringType city = "";
	    StringType zip = "not zip";
	    StringType state = "";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, city, state, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.IsNotNull(geocode);
	    Assert.AreEqual(string.Empty, geocode.MatchAddress.ToString());
	    Assert.AreEqual(string.Empty, geocode.MatchZipCode.ToString());
	}

	[Test]
	public void GetGeocodeForZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, StringType.Parse("84070"), StringType.EMPTY, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.57491020), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.88666830), geocode.MatchLongitude);
	}

	[Test]
	[Ignore]
	public void Authtenticate() {
	    string userName = "jeffd";
	    string password = "dimond";
	    string host = "mmezl.teleatlas.com";
	    string serviceName;
	    IntegerType credential;

	    password = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodePassword"]);
	    userName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeUserName"]);
	    host = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeHost"]);
	    serviceName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeoCodeServiceName"]);


	    credential = new IntegerType(TeleAtlasAuthenticator.Instance.Authenticate(userName,password,host));
	    Console.WriteLine("Received credential return value: " + credential.ToString());
	    Assert.AreNotEqual("true", credential.IsValid.ToString());
	}

	[Test]
	public void GetGeocodeFromWebServiceByFullAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("5892 South Acheron Avenue","Boise","ID","", "", CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual("5892 S ACHERON AVE", geocode.StdAddress.ToString().ToUpper());
	    Assert.AreEqual("BOISE", geocode.StdCity.ToString().ToUpper());
	    Assert.AreEqual("ID", geocode.StdState.ToString().ToUpper());
	    Assert.AreEqual("83709", geocode.StdZipCode.ToString().ToUpper());
	}

	[Test]
	public void GetGeocodeFromWebServiceByZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.GetCityAndStateOfZipCode("84128");
	    Assert.AreEqual("SALT LAKE CITY", geocode.StdCity.ToUpper());
	    Assert.AreEqual("UT", geocode.StdState.ToUpper());
	}


	[Test]
	public void ShouldBeAbleToPopulateGeocodeDataWithString() {
	    String delimitedString = "1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17";

	    GeocodeData data = new GeocodeData(delimitedString);
	    Assert.AreEqual(new IntegerType(1), data.MatchCount);
	    Assert.AreEqual(new IntegerType(2), data.MatchType);
	    Assert.AreEqual(new StringType("3"), data.MatchDB);
	    Assert.AreEqual(new StringType("4"), data.StdAddress);
	    Assert.AreEqual(new StringType("5"), data.StdCity);
	    Assert.AreEqual(new StringType("6"), data.StdState);
	    Assert.AreEqual(new StringType("7"), data.StdZipCode);
	    Assert.AreEqual(new StringType("8"), data.StdZipCodePlus4);
	    Assert.AreEqual(new StringType("9"), data.StdDPBC);
	    Assert.AreEqual(new StringType("10"), data.StdCarrier);
	    Assert.AreEqual(new StringType("11"), data.MatchAddress);
	    Assert.AreEqual(new StringType("12"), data.MatchCity);
	    Assert.AreEqual(new StringType("13"), data.MatchState);
	    Assert.AreEqual(new StringType("14"), data.MatchZipCode);
	    Assert.AreEqual(new DecimalType(15), data.MatchLatitude);
	    Assert.AreEqual(new DecimalType(16), data.MatchLongitude);
	    Assert.AreEqual(new StringType("17"), data.OutPut);
	}

	[Test]
	public void ShouldBeAbleToGetDelimitedStringFromGeocodeData() {
	    GeocodeData data = new GeocodeData();
	    data.MatchCount = new IntegerType(1);
	    data.MatchType = new IntegerType(2);
	    data.MatchDB = new StringType("3");
	    data.StdAddress = new StringType("4");
	    data.StdCity = new StringType("5");
	    data.StdState = new StringType("6");
	    data.StdZipCode = new StringType("7");
	    data.StdZipCodePlus4 = new StringType("8");
	    data.StdDPBC = new StringType("9");
	    data.StdCarrier = new StringType("10");
	    data.MatchAddress = new StringType("11");
	    data.MatchCity = new StringType("12");
	    data.MatchState = new StringType("13");
	    data.MatchZipCode = new StringType("14");
	    data.MatchLatitude = new DecimalType(15);
	    data.MatchLongitude = new DecimalType(16);
	    data.OutPut = new StringType("17");

	    Assert.AreEqual("1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17", data.ToDelimitedString());
	}

	[Test]
	public void GetGeocodeFromWebServiceByFullCanadianAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("1644 Rue de Champigny", "Montreal", "QC", "H4E 1M1", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("1644 RUE DE CHAMPIGNY", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("MONTREAL", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("QC", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("H4E 1M2", geocode.MatchZipCode.ToString().ToUpper());
	}

	[Test]
	public void ShouldBeAbleToGeocodeCanadianAddressWithoutMatch() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("575 Broadway Boulvard", "Grand Falls", "NB", "E3Z 2L2", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("575 BOULEVARD BROADWAY", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("GRAND FALLS (GRAND-SAULT)", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("NB", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("E3Z 1B3", geocode.MatchZipCode.ToString().ToUpper());
	}

	[Test]
	public void ShouldBeAbleToGeocodeCanadianAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("5400 Robinson St", "Niagara Falls", "ON", "L2G 2A6", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("5400 ROBINSON ST", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("NIAGARA FALLS", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("ON", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("L2G 7T8", geocode.MatchZipCode.ToString().ToUpper());
	}
    }
}
