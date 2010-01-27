using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Shipping {
    class ITimeInTransitRequest {
        ArtifactAddressData FromAddress { get; set; }
        ArtifactAddressData ToAddress { get; set; }
    }
}
