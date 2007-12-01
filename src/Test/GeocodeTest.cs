using System;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Geocode;
using Spring2.Core.Geocode.WebService;
using Spring2.Core.Configuration;

namespace Spring2.Core.Test {
    /// <summary>
    /// Summary description for GeocodeTest.
    /// </summary>
    [TestFixture]
    public class GeocodeTest {
	[Test]
	[Ignore("Requires a tele atlas login to run.")]
	public void GetGeocodeForStandardAddress() {
	    StringType street = "10150 S. Centennial Parkway";
	    StringType city = "Sandy";
	    StringType zip = "84070";
	    StringType state = "UT";
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(street, city, state, zip, StringType.UNSET);
	    Assert.AreEqual(new DecimalType(40.566941), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.895122), geocode.MatchLongitude);
	    Assert.AreEqual("10150 CENTENNIAL PKWY", geocode.StdAddress.ToString().Trim());
	    Assert.AreEqual("SANDY", geocode.StdCity.ToString().Trim());
	    Assert.AreEqual("84070", geocode.StdZipCode.ToString().Trim());
	    Assert.AreEqual("UT", geocode.StdState.ToString().Trim());
	    Assert.AreEqual("4103", geocode.StdZipCodePlus4.ToString().Trim());
	}

	[Test]
	[Ignore("Requires a tele atlas login to run.")]
	public void GetGeocodeForZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, StringType.Parse("84070"), StringType.EMPTY);
	    Assert.AreEqual(new DecimalType(40.577445), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.885068), geocode.MatchLongitude);
	}

	[Test]
	[Ignore("Requires a tele atlas login to run.")]
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
	[Ignore("Requires a tele atlas login to run.")]
	public void GetGeocodeFromWebServiceByFullAddress() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode("5892 South Acheron Avenue","Boise","ID","", "");
	    Assert.AreEqual("5892 S ACHERON AVE", geocode.StdAddress.ToString());
	    Assert.AreEqual("BOISE", geocode.StdCity.ToString());
	    Assert.AreEqual("ID", geocode.StdState.ToString());
	    Assert.AreEqual("83709", geocode.StdZipCode.ToString());
	}

	[Test]
	[Ignore("Requires a tele atlas login to run.")]
	public void GetGeocodeFromWebServiceByZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.GetCityAndStateOfZipCode("84128-3877");
	    Assert.AreEqual("SALT LAKE CITY", geocode.StdCity);
	    Assert.AreEqual("UT", geocode.StdState);
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

    }
}
