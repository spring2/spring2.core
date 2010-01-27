using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.UPS {
    class UPSTimeInTransitResponse : ITimeInTransitResponse {
        private ArtifactAddressData fromAddress = null;
        private ArtifactAddressData toAddress = null;
        private StringType responseStatusCode = StringType.EMPTY;
        private StringType responseStatusDiscription = StringType.EMPTY;
        private StringType serviceCode = StringType.EMPTY;
        private StringType serviceDescription = StringType.EMPTY;
        private StringType guaranteedCode = StringType.EMPTY;
        private IntegerType businessTransitDays = IntegerType.UNSET;
        private DateTimeType pickupDateTime = DateTimeType.UNSET;
        private IntegerType hollidayCount = IntegerType.UNSET;
        private IntegerType delayCount = IntegerType.UNSET;
        private DateTimeType arrivalDateTime = DateTimeType.UNSET;
        private IntegerType transitDays = IntegerType.UNSET;
        private IntegerType restDays = IntegerType.UNSET;
        private XmlDocument responseXml = null;
        private TimeInTransitResponseTypeEnum responseType = TimeInTransitResponseTypeEnum.UNSET;

        public UPSTimeInTransitResponse(): this(StringType.EMPTY){
        }

        public UPSTimeInTransitResponse(StringType xmlResults) {
            fromAddress = new ArtifactAddressData();
            toAddress = new ArtifactAddressData();
            if (xmlResults.IsValid) {
                ParseTimeInTransitResponse(xmlResults.ToString());
            }
        }

        public BooleanType ResultSuccess {
            get { return BooleanType.TRUE; } 
        }

        public ArtifactAddressData FromAddress {
            get { return fromAddress; }
            set { fromAddress = value; }
        }

        public ArtifactAddressData ToAddresses {
            get { return toAddress; }
            set { toAddress = value; }
        }


        public IntegerType RestDays {
            get { return restDays; }
            set { restDays = value; }
        }


        public XmlDocument ResponseXml {
            get { return responseXml; }
            set { responseXml = value; }
        }


        public StringType ResponseStatusCode {
            get { return responseStatusCode; }
            set { responseStatusCode = value; }
        }

        public StringType ResponseStatusDiscription {
            get { return responseStatusDiscription; }
            set { responseStatusDiscription = value; }
        }

        public StringType ServiceCode {
            get { return serviceCode; }
            set { serviceCode = value; }
        }

        public StringType ServiceDescription {
            get { return serviceDescription; }
            set { serviceDescription = value; }
        }

        public StringType GuaranteedCode {
            get { return guaranteedCode; }
            set { guaranteedCode = value; }
        }

        public IntegerType BusinessTransitDays {
            get { return businessTransitDays; }
            set { businessTransitDays = value; }
        }

        public DateTimeType PickupDateTime {
            get { return pickupDateTime; }
            set { pickupDateTime = value; }
        }

        public IntegerType HollidayCount {
            get { return hollidayCount; }
            set { hollidayCount = value; }
        }

        public IntegerType DelayCount {
            get { return delayCount; }
            set { delayCount = value; }
        }

        public TimeInTransitResponseTypeEnum ResponseType { get { return responseType; } }

        public DateTimeType DropOffDate { get { return arrivalDateTime; } }

        public DateTimeType ArrivalDateTime {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        public IntegerType TransitDays {
            get { return transitDays; }
            set { transitDays = value; }
        }

        private void ParseTimeInTransitReponseType( XmlDocument reponseDocument) {
            responseType = TimeInTransitResponseTypeEnum.VALID; 
        }

        public void ParseTimeInTransitResponse() {
            ParseTimeInTransitResponse(String.Empty);
        }

        public void ParseTimeInTransitResponse(String response) {
            if(response != String.Empty){
                responseXml = new XmlDocument();
                responseXml.LoadXml(response);
            }

            ParseTimeInTransitReponseType(responseXml);
            
            XmlNode responseNode = responseXml.SelectSingleNode("TimeInTransitResponse/Response");
            responseStatusCode = responseNode.SelectSingleNode("ResponseStatusCode").InnerText;
            responseStatusDiscription = responseNode.SelectSingleNode("ResponseStatusDescription").InnerText;
            if (responseType == TimeInTransitResponseTypeEnum.VALID) {
                fromAddress = new ArtifactAddressData();
                toAddress = new ArtifactAddressData();
                XmlNode transitResponseNode = responseXml.SelectSingleNode("TimeInTransitResponse/TransitResponse");
                
                //Add the From Address stuff from the response.
                XmlNode fromNode = responseXml.SelectSingleNode("TimeInTransitResponse/TransitResponse/TransitFrom/AddressArtifactFormat");
                fromAddress.PoliticalDivision2 = fromNode.SelectSingleNode("PoliticalDivision2").InnerText;
                fromAddress.PoliticalDivision1 = fromNode.SelectSingleNode("PoliticalDivision1").InnerText;
                fromAddress.PostcodePrimaryLow = fromNode.SelectSingleNode("PostcodePrimaryLow").InnerText + "-" + fromNode.SelectSingleNode("PostcodeExtendedLow").InnerText;
                fromAddress.CountryCode = fromNode.SelectSingleNode("CountryCode").InnerText;

                //Add the ToAddress Stuff from the response
                XmlNode toNode = responseXml.SelectSingleNode("TimeInTransitResponse/TransitResponse/TransitTo/AddressArtifactFormat");
                toAddress.PoliticalDivision2 = toNode.SelectSingleNode("PoliticalDivision2").InnerText;
                toAddress.PoliticalDivision1 = toNode.SelectSingleNode("PoliticalDivision1").InnerText;
                toAddress.PostcodePrimaryLow = toNode.SelectSingleNode("PostcodePrimaryLow").InnerText + "-" + toNode.SelectSingleNode("PostcodeExtendedLow").InnerText;
                toAddress.CountryCode = toNode.SelectSingleNode("CountryCode").InnerText;

                //add the Arival Time Stuff from response.
                //XmlNodeList  = responseXml.SelectSingleNode("TimeInTransitResponse/TransitResponse/TransitTo/AddressArtifactFormat");
                
            }
            
        }
    }
}
