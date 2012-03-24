using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService {
    //PostageRateRequest
    public class PostageRateInputData : CommonLabelRequest {
	public string RequesterID { set; get; }

	public Credentials CertifiedIntermediary { set; get; }

	public string AutomationRate { set; get; }

	public string Machinable { set; get; }

	public string ServiceLevel { set; get; }

	public string SortType { set; get; }

	public Services Services { set; get; }

	public float Value { set; get; }

	public double CODAmount { set; get; }

	public string InsuredValue { set; get; }

	public double RegisteredMailValue { set; get; }

	public string EntryFacility { set; get; }

	public string FromPostalCode { set; get; }

	public string ToPostalCode { set; get; }

	public string ToCountry { set; get; }

	public string ToCountryCode { set; get; }

	public string ShipDate { set; get; }

	public string ShipTime { set; get; }

	public ReturnOptions ResponseOptions { set; get; }
    }
}
