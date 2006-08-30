using System;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Geocode;

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
	}

	[Test]
	[Ignore("Requires a tele atlas login to run.")]
	public void GetGeocodeForZipCode() {
	    GeocodeData geocode = GeocodeProvider.Instance.DoGeocode(StringType.EMPTY, StringType.EMPTY, StringType.EMPTY, StringType.Parse("84070"), StringType.EMPTY);
	    Assert.AreEqual(new DecimalType(40.577445), geocode.MatchLatitude);
	    Assert.AreEqual(new DecimalType(-111.885068), geocode.MatchLongitude);
	}
    }
}
