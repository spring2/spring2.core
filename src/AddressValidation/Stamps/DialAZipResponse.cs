using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Spring2.Core.AddressValidation.Stamps {
    [Serializable]
    [XmlRoot(ElementName = "Dial-A-ZIP_Response")]
    public class DialAZipResponse {
	public string User { get; set; }
	public string Command { get; set; }
	public int ReturnCode { get; set; }
	public string ZIP5 { get; set; }
	public string Plus4 { get; set; }
	public string Crt { get; set; }
	public string AddrLine1 { get; set; }
	public string AddrLine2 { get; set; }
	public string AddrLine3 { get; set; }
	public string AddrLineLast { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string DPDigits { get; set; }
	public string CountryNo { get; set; }
	public string LOTSeq { get; set; }
	public string LOTAscend { get; set; }
	public string CongressDist { get; set; }
	public string AddrExists { get; set; }
	public string AMSDate { get; set; }
	public string AMSVersion { get; set; }
	public string RecType { get; set; }
	public string LACSStatus { get; set; }
	public string LACSIndicator { get; set; }
	public string LACSReturnCode { get; set; }
	public string HSA { get; set; }
	public string HSC { get; set; }
	public string DPVFootNote { get; set; }
	public string PRUrban { get; set; }
	public string USPSFirmName { get; set; }
	public string CountyName { get; set; }
	public string RDI { get; set; }
	public string UPSRural { get; set; }
	public string ZIPLookupCount { get; set; }
	public string AMSOpenRC { get; set; }
	public string ComputerName { get; set; }
	public string TimeStamp { get; set; }

	// Hidden Element for Errors
	public string Status { get; set; }
    }
}
