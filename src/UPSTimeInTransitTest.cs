using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Shipping;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.Test {
    [TestFixture]
    class UPSTimeInTransitTest {
        [Test]
        public void AbleToSubmitToTimeInTransitService() {
            ShippingManager manager = new ShippingManager(StringType.Parse("Spring2.Core.Shipping.UPS.UPSShippingProvider"));
            ArtifactAddressData toAddress = new ArtifactAddressData();
            ArtifactAddressData fromAddress = new ArtifactAddressData();
            toAddress.Consignee = StringType.Parse("Ben Berneki");
            toAddress.CountryCode = StringType.Parse("US");
            toAddress.PostcodePrimaryLow = StringType.Parse("02205");
            toAddress.StreetNumberLow = StringType.Parse("600");
            toAddress.StreetName = StringType.Parse("Atlantic");
            toAddress.StreetType = StringType.Parse("Avenue");
            toAddress.PoliticalDivision2 = StringType.Parse("Boston");
            toAddress.PoliticalDivision1 = StringType.Parse("MA");

            fromAddress.Consignee = StringType.Parse("Timmy Gieter");
            fromAddress.CountryCode = StringType.Parse("US");
            fromAddress.PostcodePrimaryLow = StringType.Parse("40511");
            fromAddress.StreetNumberLow = StringType.Parse("3301");
            fromAddress.StreetName = StringType.Parse("Leestown");
            fromAddress.StreetType = StringType.Parse("Road");
            fromAddress.PoliticalDivision2 = StringType.Parse("Lexington");
            fromAddress.PoliticalDivision1 = StringType.Parse("KY");

            ITimeInTransitResponse respones = manager.ShippingProvider.TimeInTransit(fromAddress, toAddress, DateTimeType.Now, 1m, StringType.Parse("LBS"), StringType.Parse("USD"), new CurrencyType(1.50m));

            Assert.IsNotNull(respones);

        }
    }
}
