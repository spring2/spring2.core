using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core.Types;
using Spring2.Core.Shipping;

namespace Spring2.Core.Shipping.UPS {
    class UPSTimeInTransitRequest : ITimeInTransitRequest {
        ArtifactAddressData fromLocation;
        ArtifactAddressData toLocation;
        DateTimeType pickupDateTime;
        DecimalType shipmentWeight;
        UnitOfMeasurementEnum unitOfMeasurment;
        CurrencyCodeEnum currencyCode;
        CurrencyType monetaryValue;

        public UPSTimeInTransitRequest() {
            fromLocation = new ArtifactAddressData();
            toLocation = new ArtifactAddressData();
        }

        public UPSTimeInTransitRequest(ArtifactAddressData fromLocation, ArtifactAddressData toLocation, DateTimeType pickupDateTime, DecimalType shipmentWeight, UnitOfMeasurementEnum unitOfMeasurment, CurrencyCodeEnum currencyCode, CurrencyType monetaryValue) {
            this.fromLocation = fromLocation;
            this.toLocation = toLocation;
            this.pickupDateTime = pickupDateTime;
            this.shipmentWeight = shipmentWeight;
            this.unitOfMeasurment = unitOfMeasurment;
            this.currencyCode = currencyCode;
            this.monetaryValue = monetaryValue;
        }

        public ArtifactAddressData FromLocation {
            get { return fromLocation; }
            set { fromLocation = value; }
        }

        public ArtifactAddressData ToLocation {
            get { return toLocation; }
            set { toLocation = value; }
        }

        public DateTimeType PickupDateTime {
            get { return pickupDateTime; }
            set { pickupDateTime = value; }
        }

        public DecimalType ShipmentWeight {
            get { return shipmentWeight; }
            set { shipmentWeight = value; }
        }

        public UnitOfMeasurementEnum UnitOfMeasurment {
            get { return unitOfMeasurment; }
            set { unitOfMeasurment = value; }
        }

        public CurrencyCodeEnum CurrencyCode {
            get { return currencyCode; }
            set { currencyCode = value; }
        }

        public CurrencyType MonetaryValue {
            get { return monetaryValue; }
            set { monetaryValue = value; }
        }

        public string GetRequestXml(ArtifactAddressData fromLocation, ArtifactAddressData toLocation, DateTimeType pickupDateTime, DecimalType shipmentWeight, UnitOfMeasurementEnum unitOfMeasurment, CurrencyCodeEnum currencyCode, CurrencyType monetaryValue) {
            this.fromLocation = fromLocation;
            this.toLocation = toLocation;
            this.pickupDateTime = pickupDateTime;
            this.shipmentWeight = shipmentWeight;
            this.unitOfMeasurment = unitOfMeasurment;
            this.currencyCode = currencyCode;
            this.monetaryValue = monetaryValue;
            return GetRequestXml();
        }

        public string GetRequestXml() {
            StringBuilder buffer = new StringBuilder();
            buffer.Append(@"<?xml version=""1.0""?>" + Environment.NewLine);
            buffer.Append(@"<TimeInTransitRequest xml:lang=""en-US"">" + Environment.NewLine);
            buffer.Append(@"<Request>" + Environment.NewLine);
            buffer.Append(@"<TransactionReference>" + Environment.NewLine);
            buffer.Append(@"<CustomerContext>TC1000201</CustomerContext>" + Environment.NewLine);
            buffer.Append(@"<XpciVersion>1.0002</XpciVersion>" + Environment.NewLine);
            buffer.Append(@"</TransactionReference>" + Environment.NewLine);
            buffer.Append(@"<RequestAction>TimeInTransit</RequestAction>" + Environment.NewLine);
            buffer.Append(@"</Request>" + Environment.NewLine);
            buffer.Append(@"<TransitFrom>" + Environment.NewLine);
            buffer.Append(@"<AddressArtifactFormat>" + Environment.NewLine);
            buffer.Append(@"<Consignee>" + fromLocation.Consignee.ToString() + "</Consignee>" + Environment.NewLine);
            buffer.Append(@"<StreetNumberLow>" + fromLocation.StreetNumberLow.ToString() + "</StreetNumberLow>" + Environment.NewLine);
            buffer.Append(@"<StreetName>" + fromLocation.StreetName.ToString() + "</StreetName>" + Environment.NewLine);
            buffer.Append(@"<StreetType>" + fromLocation.StreetType.ToString() + "</StreetType>" + Environment.NewLine);
            buffer.Append(@"<PoliticalDivision2>" + fromLocation.PoliticalDivision2.ToString() + "</PoliticalDivision2>" + Environment.NewLine);
            buffer.Append(@"<PoliticalDivision1>" + fromLocation.PoliticalDivision1.ToString() + "</PoliticalDivision1>" + Environment.NewLine);
            buffer.Append(@"<PostcodePrimaryLow>" + fromLocation.PostcodePrimaryLow.ToString() + "</PostcodePrimaryLow>" + Environment.NewLine);
            buffer.Append(@"<PostcodeExtendedLow>" + fromLocation.PostcodeExtendedLow.ToString() + "</PostcodeExtendedLow>" + Environment.NewLine);
            buffer.Append(@"<CountryCode>" + fromLocation.CountryCode.ToString() + "</CountryCode>" + Environment.NewLine);
            buffer.Append(@"</AddressArtifactFormat>" + Environment.NewLine);
            buffer.Append(@"</TransitFrom>" + Environment.NewLine);
            buffer.Append(@"<TransitTo>" + Environment.NewLine);
            buffer.Append(@"<AddressArtifactFormat>" + Environment.NewLine);
            buffer.Append(@"<Consignee>" + toLocation.Consignee.ToString() + "</Consignee>" + Environment.NewLine);
            buffer.Append(@"<StreetNumberLow>" + toLocation.StreetNumberLow.ToString() + "</StreetNumberLow>" + Environment.NewLine);
            buffer.Append(@"<StreetName>" + toLocation.StreetName.ToString() + "</StreetName>" + Environment.NewLine);
            buffer.Append(@"<StreetType>" + toLocation.StreetType.ToString() + "</StreetType>" + Environment.NewLine);
            buffer.Append(@"<PoliticalDivision2>" + toLocation.PoliticalDivision2.ToString() + "</PoliticalDivision2>" + Environment.NewLine);
            buffer.Append(@"<PoliticalDivision1>" + toLocation.PoliticalDivision1.ToString() + "</PoliticalDivision1>" + Environment.NewLine);
            buffer.Append(@"<PostcodePrimaryLow>" + toLocation.PostcodePrimaryLow.ToString() + "</PostcodePrimaryLow>" + Environment.NewLine);
            buffer.Append(@"<PostcodeExtendedLow>" + toLocation.PostcodeExtendedLow.ToString() + "</PostcodeExtendedLow>" + Environment.NewLine);
            buffer.Append(@"<CountryCode>" + toLocation.CountryCode.ToString() + "</CountryCode>" + Environment.NewLine);
            buffer.Append(@"</AddressArtifactFormat>" + Environment.NewLine);
            buffer.Append(@"</TransitTo>" + Environment.NewLine);
            buffer.Append(@"<PickupDate>" + pickupDateTime.ToString("yyyyMMdd") + "</PickupDate>" + Environment.NewLine);
            buffer.Append(@"<MaximumListSize>10</MaximumListSize>" + Environment.NewLine);
            buffer.Append(@"<InvoiceLineTotal>" + Environment.NewLine);
            buffer.Append(@"<CurrencyCode>" + currencyCode.Code.ToString() + "</CurrencyCode>" + Environment.NewLine);
            buffer.Append(@"<MonetaryValue>" + monetaryValue.ToDecimal().ToString() + "</MonetaryValue>" + Environment.NewLine);
            buffer.Append(@"</InvoiceLineTotal>" + Environment.NewLine);
            buffer.Append(@"<ShipmentWeight>" + Environment.NewLine);
            buffer.Append(@"<UnitOfMeasurement>" + Environment.NewLine);
            buffer.Append(@"<Code>" + unitOfMeasurment.Code.ToString() + "</Code>" + Environment.NewLine);
            buffer.Append(@"<Description>" + unitOfMeasurment.Name.ToString() + "</Description>" + Environment.NewLine);
            buffer.Append(@"</UnitOfMeasurement>" + Environment.NewLine);
            buffer.Append(@"<Weight>" + shipmentWeight.ToString() + "</Weight>" + Environment.NewLine);
            buffer.Append(@"</ShipmentWeight>" + Environment.NewLine);
            buffer.Append(@"</TimeInTransitRequest>" + Environment.NewLine);
            return buffer.ToString();
        }
    }
}
