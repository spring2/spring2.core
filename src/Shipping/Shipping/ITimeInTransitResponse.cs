using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public interface ITimeInTransitResponse {
        ArtifactAddressData FromAddress { get; }
        ArtifactAddressData ToAddresses { get; }
        BooleanType ResultSuccess { get; }
        DateTimeType PickupDateTime { get; }
        DateTimeType DropOffDate { get; }
        IntegerType TransitDays { get; }
        TimeInTransitResponseTypeEnum ResponseType { get ;}
    }
}
