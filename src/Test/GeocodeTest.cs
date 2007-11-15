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
	    GeocodeData geocode = (GeocodeProvider.Instance as TeleAtlasProvider).GetCityAndStateOfZipCode("84128-3877");
	    Assert.AreEqual("SALT LAKE CITY", geocode.StdCity);
	    Assert.AreEqual("UT", geocode.StdState);
	}
    }
}
