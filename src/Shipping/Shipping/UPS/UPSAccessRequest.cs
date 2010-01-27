using System;
using System.Collections.Generic;
using System.Text;

using Spring2.Core.Types;

namespace Spring2.Core.Shipping.UPS {
    class UPSAccessRequest {
        public static StringType AccessRequestXml( StringType accessKey, StringType userId, StringType password)  {
                     return string.Format("<?xml version=\"1.0\"?>" +
                    "<AccessRequest xml:lang=\"en-us\">" +
                    "<AccessLicenseNumber>{0}</AccessLicenseNumber>" +
                    "<UserId>{1}</UserId>" +
                    "<Password>{2}</Password>" +
                    "</AccessRequest>", accessKey.ToString(), userId.ToString(), password.ToString());
        }
    }
}
