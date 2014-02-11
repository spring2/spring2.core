using System;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Geocode;
using Spring2.Core.Geocode.WebService;
using Spring2.Core.Geocode.Types;
using Spring2.Core.Configuration;
using Spring2.Core.Geocode.TomTom;

namespace Spring2.Core.Test {
    /// <summary>
    /// Tests for TomTomProvider
    /// </summary>
    [TestFixture]
    public class TomTomProviderTest {

	IGeocodeProvider current = null;

	[SetUp]
	public void Setup() {
	    current = GeocodeProvider.Instance;
	    GeocodeProvider.SetProvider(new TomTomProvider());
	}

	[TearDown]
	public void TearDown() {
	    if (current != null) {
		GeocodeProvider.SetProvider(current);
	    }
	}

	[Test]
	[Ignore]
	public void GetGeocodeForStandardAddress() {
	    StringType street = "10150 S. Centennial Parkway";
	    StringType city = "Sandy";
	    StringType zip = "84070";
	    StringType state = "UT";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, city, state, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.567043), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.895732), geocode.MatchLongitude);
	    Assert.AreEqual("10150 S CENTENNIAL PKWY", geocode.StdAddress.ToString().Trim().ToUpper());
	    Assert.AreEqual("SANDY", geocode.StdCity.ToString().Trim().ToUpper());
	    Assert.AreEqual("84070-4103", geocode.StdZipCode.ToString().Trim().ToUpper());
	    Assert.AreEqual("UTAH", geocode.StdState.ToString().Trim().ToUpper());
	}

	[Test]
	[Ignore]
	public void ShouldFallThroughToSubLocality() {
	    StringType street = "109 Front St";
	    StringType zip = "11201";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, StringType.UNSET, StringType.UNSET, zip, StringType.UNSET, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.7025076), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-73.9890285), geocode.MatchLongitude);
	    Assert.AreEqual("109 FRONT ST", geocode.StdAddress.ToString().Trim().ToUpper());
	    Assert.AreEqual("BROOKLYN", geocode.StdCity.ToString().Trim().ToUpper());
	    Assert.AreEqual("11201-1007", geocode.StdZipCode.ToString().Trim().ToUpper());
	    Assert.AreEqual("NEW YORK", geocode.StdState.ToString().Trim().ToUpper());
	}

	[Test]
	[Ignore]
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
	[Ignore]
	public void GetGeocodeForZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, StringType.Parse("84070"), StringType.EMPTY, CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual(new DecimalType(40.57491020), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.88666830), geocode.MatchLongitude);
	}

	[Test]
	[Ignore]
	public void GetGeocodeFromWebServiceByFullAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("5892 South Acheron Avenue", "Boise", "ID", "", "", CountryCodeEnum.UNITED_STATES);
	    Assert.AreEqual("5892 S ACHERON AVE", geocode.StdAddress.ToString().ToUpper());
	    Assert.AreEqual("BOISE", geocode.StdCity.ToString().ToUpper());
	    Assert.AreEqual("IDAHO", geocode.StdState.ToString().ToUpper());
	    Assert.AreEqual("83709-5769", geocode.StdZipCode.ToString().ToUpper());
	}

	[Test]
	[Ignore]
	public void GetGeocodeFromWebServiceByZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.GetCityAndStateOfZipCode("84128");
	    //Assert.AreEqual("SALT LAKE CITY", geocode.StdCity.ToUpper());
	    Assert.AreEqual("UT", geocode.StdState.ToUpper());
	}


	[Test]
	[Ignore]
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
	[Ignore]
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
	[Ignore]
	public void GetGeocodeFromWebServiceByFullCanadianAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("1644 Rue de Champigny", "Montreal", "QC", "H4E 1M1", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("1644 RUE DE CHAMPIGNY", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("MONTRÉAL", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("QUÉBEC", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("H4E", geocode.MatchZipCode.ToString().ToUpper());
	}

	[Test]
	[Ignore]
	public void ShouldBeAbleToGeocodeCanadianAddressWithoutMatch() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("575 Broadway Boulvard", "Grand Falls", "NB", "E3Z 2L2", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("575 BROADWAY BLVD W", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("GRAND-SAULT/GRAND FALLS", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("NEW BRUNSWICK", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("E3Z", geocode.MatchZipCode.ToString().ToUpper());
	}

	[Test]
	[Ignore]
	public void ShouldBeAbleToGeocodeCanadianAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("5400 Robinson St", "Niagara Falls", "ON", "L2G 2A6", "", CountryCodeEnum.CANADA);
	    Assert.AreEqual("5400 ROBINSON ST", geocode.MatchAddress.ToString().ToUpper());
	    Assert.AreEqual("NIAGARA FALLS", geocode.MatchCity.ToString().ToUpper());
	    Assert.AreEqual("ONTARIO", geocode.MatchState.ToString().ToUpper());
	    Assert.AreEqual("L2G", geocode.MatchZipCode.ToString().ToUpper());
	}
    }
}
