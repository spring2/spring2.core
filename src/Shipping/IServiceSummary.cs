using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public interface IServiceSummary {
        StringType ServiceCode { get; }

        StringType ServiceDescription { get; }

        StringType GuaranteedCode { get; }

        DateTimeType PickupDateTime { get; }

        DateTimeType ArrivalDateTime { get; }

        IntegerType TransitDays { get; }

        void ParseServiceSummary(XmlNode serviceSummaryXml);

    }
}
