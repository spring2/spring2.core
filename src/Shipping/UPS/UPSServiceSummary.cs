using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.UPS {
    public class UPSServiceSummary : IServiceSummary {
        private StringType serviceCode = StringType.EMPTY;
        private StringType serviceDescription = StringType.EMPTY;
        private StringType guaranteedCode = StringType.EMPTY;
        private DateTimeType pickupDateTime = DateTimeType.UNSET;
        private DateTimeType arrivalDateTime = DateTimeType.UNSET;
        private IntegerType transitDays = IntegerType.UNSET;

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

        public DateTimeType PickupDateTime {
            get { return pickupDateTime; }
            set { pickupDateTime = value; }
        }

        public DateTimeType ArrivalDateTime {
            get { return arrivalDateTime; }
            set { arrivalDateTime = value; }
        }

        public IntegerType TransitDays {
            get { return transitDays; }
            set { transitDays = value; }
        }

        public void ParseServiceSummary(XmlNode serviceSummaryXml) {    
            serviceCode = StringType.Parse(serviceSummaryXml.SelectSingleNode("Service/Code").InnerText);
            serviceDescription = StringType.Parse(serviceSummaryXml.SelectSingleNode("Service/Description").InnerText);
            guaranteedCode = StringType.Parse(serviceSummaryXml.SelectSingleNode("Guaranteed/Code").InnerText);
            pickupDateTime = DateTimeType.Parse(serviceSummaryXml.SelectSingleNode("EstimatedArrival/PickupDate").InnerText + " " + serviceSummaryXml.SelectSingleNode("EstimatedArrival/PickupTime").InnerText);
            arrivalDateTime = DateTimeType.Parse(serviceSummaryXml.SelectSingleNode("EstimatedArrival/Date").InnerText + " " + serviceSummaryXml.SelectSingleNode("EstimatedArrival/Time").InnerText);
            transitDays = IntegerType.Parse(serviceSummaryXml.SelectSingleNode("EstimatedArrival/BusinessTransitDays").InnerText);
        }
    }
}
