using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public interface IShippingProvider {
        ITimeInTransitResponse TimeInTransit(ArtifactAddressData fromLocation, ArtifactAddressData toLocation, DateTimeType pickupDateTime, DecimalType shipmentWeight, UnitOfMeasurementEnum unitOfMeasurment, CurrencyCodeEnum currencyCode, CurrencyType monetaryValue);
    }
}
