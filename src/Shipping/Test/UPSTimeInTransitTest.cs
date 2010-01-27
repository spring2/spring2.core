using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Shipping;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.Test {
    [TestFixture]
    public class UPSTimeInTransitTest {
        [Test]
        public void AbleToSubmitToTimeInTransitServiceDomestic() {
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

            ITimeInTransitResponse respones = manager.ShippingProvider.TimeInTransit(fromAddress, toAddress, DateTimeType.Now, 1m, UnitOfMeasurementEnum.POUNDS, CurrencyCodeEnum.UNITED_STATES_OF_AMERICA_DOLLARS, new CurrencyType(1.50m));

            Assert.IsNotNull(respones, "Response Not Found");
            Assert.IsTrue(respones.ServiceSummaries.Count > 0, "Service Summaries Not Found");
            Assert.IsTrue(respones.ResponseType == TimeInTransitResponseTypeEnum.VALID, "Respose returned Invalid response type");
        }

        [Test]
        public void AbleToSubmitToTimeInTransitServiceInternational() {
            ShippingManager manager = new ShippingManager(StringType.Parse("Spring2.Core.Shipping.UPS.UPSShippingProvider"));
            ArtifactAddressData toAddress = new ArtifactAddressData();
            ArtifactAddressData fromAddress = new ArtifactAddressData();
            toAddress.Consignee = StringType.Parse("Royal Onterio Museum");
            toAddress.CountryCode = StringType.Parse("CA");
            toAddress.PostcodePrimaryLow = StringType.Parse("M5S 2C6");
            toAddress.StreetNumberLow = StringType.Parse("100");
            toAddress.StreetName = StringType.Parse("Queen's Park");
            //toAddress.StreetType = StringType.Parse("");
            toAddress.PoliticalDivision2 = StringType.Parse("Toronto");
            toAddress.PoliticalDivision1 = StringType.Parse("Ontario");

            fromAddress.Consignee = StringType.Parse("Timmy Gieter");
            fromAddress.CountryCode = StringType.Parse("US");
            fromAddress.PostcodePrimaryLow = StringType.Parse("40511");
            fromAddress.StreetNumberLow = StringType.Parse("3301");
            fromAddress.StreetName = StringType.Parse("Leestown");
            fromAddress.StreetType = StringType.Parse("Road");
            fromAddress.PoliticalDivision2 = StringType.Parse("Lexington");
            fromAddress.PoliticalDivision1 = StringType.Parse("KY");

            ITimeInTransitResponse respones = manager.ShippingProvider.TimeInTransit(fromAddress, toAddress, DateTimeType.Now, 1m, UnitOfMeasurementEnum.POUNDS, CurrencyCodeEnum.UNITED_STATES_OF_AMERICA_DOLLARS, new CurrencyType(1.50m));

            Assert.IsNotNull(respones, "Response Not Found");
            Assert.IsTrue(respones.ResponseType == TimeInTransitResponseTypeEnum.VALID, "Respose returned Invalid response type");
            Assert.IsTrue(respones.ServiceSummaries.Count > 0, "Service Summaries Not Found");
             

        }
    }
}
