using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Spring2.Core.AddressValidation.Stamps {
    [Serializable]
    [XmlRoot(ElementName = "VERIFYADDRESS")]
    public class DialAZipRequest {
	public string COMMAND { get; set; }
	public string SERIALNO { get; set; }
	public string PASSWORD { get; set; }
	public string USER { get; set; }
	public string ADDRESS0 { get; set; }
	public string ADDRESS1 { get; set; }
	public string ADDRESS2 { get; set; }
	public string ADDRESS3 { get; set; }
    }
}
