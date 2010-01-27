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
        ServiceSummaryList responseSummaries = new ServiceSummaryList();
        private IntegerType hollidayCount = IntegerType.UNSET;
        private IntegerType delayCount = IntegerType.UNSET;
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

        public ServiceSummaryList ServiceSummaries {
            get { return responseSummaries; }
            set { responseSummaries = value; }
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

       
        public IntegerType HollidayCount {
            get { return hollidayCount; }
            set { hollidayCount = value; }
        }

        public IntegerType DelayCount {
            get { return delayCount; }
            set { delayCount = value; }
        }

        public TimeInTransitResponseTypeEnum ResponseType { get { return responseType; } }

        private void ParseTimeInTransitReponseType( XmlDocument responseDocument) {
            if (responseDocument.SelectSingleNode("TimeInTransitResponse/Response/Error") != null) {
                responseType = TimeInTransitResponseTypeEnum.INVALID;
            } else {
                responseType = TimeInTransitResponseTypeEnum.VALID;
            }
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
                fromAddress.PostcodePrimaryLow = fromNode.SelectSingleNode("PostcodePrimaryLow").InnerText + ((fromNode.SelectSingleNode("PostcodeExtendedLow") != null) ? "-" + fromNode.SelectSingleNode("PostcodeExtendedLow").InnerText : "");
                fromAddress.CountryCode = fromNode.SelectSingleNode("CountryCode").InnerText;

                //Add the ToAddress Stuff from the response
                XmlNode toNode = responseXml.SelectSingleNode("TimeInTransitResponse/TransitResponse/TransitTo/AddressArtifactFormat");
                toAddress.PoliticalDivision2 = toNode.SelectSingleNode("PoliticalDivision2").InnerText;
                toAddress.PoliticalDivision1 = toNode.SelectSingleNode("PoliticalDivision1").InnerText;
                toAddress.PostcodePrimaryLow = toNode.SelectSingleNode("PostcodePrimaryLow").InnerText + ((toNode.SelectSingleNode("PostcodeExtendedLow") != null) ? "-" + toNode.SelectSingleNode("PostcodeExtendedLow").InnerText : "");
                toAddress.CountryCode = toNode.SelectSingleNode("CountryCode").InnerText;

                //add the Arival Time Stuff from response.
                XmlNodeList serviceSummaries = responseXml.SelectNodes("TimeInTransitResponse/TransitResponse/ServiceSummary");
                responseSummaries = new ServiceSummaryList();
                foreach (XmlNode currentSummary in serviceSummaries) { 
                    UPSServiceSummary serviceSummary = new UPSServiceSummary();
                    serviceSummary.ParseServiceSummary(currentSummary);
                    responseSummaries.Add((IServiceSummary)serviceSummary);
                }
                
            }
            
        }
    }
}
