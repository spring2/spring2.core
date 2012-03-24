using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PostageLabelData {

	public int Status { set; get; }

	public string ErrorMessage { set; get; }

	public string Base64LabelImage { set; get; }

	public ImageSet Label { set; get; }

	public ImageSet CustomsForm { set; get; }

	public string PIC { set; get; }

	public string CustomsNumber { set; get; }

	public string TrackingNumber { set; get; }

	public decimal FinalPostage { set; get; }

	public int TransactionID { set; get; }

	public string TransactionDateTime { set; get; }

	public string PostmarkDate { set; get; }

	public decimal PostageBalance { set; get; }

	public string ReferenceID { set; get; }

	public int CostCenter { set; get; }

	public string HfpFacilityID { set; get; }

	public string HfpFacilityName { set; get; }

	public string HfpFacilityAddress1 { set; get; }

	public string HfpFacilityCity { set; get; }

	public string HfpFacilityState { set; get; }

	public string HfpFacilityPostalCode { set; get; }

	public string HfpFacilityZIP4 { set; get; }

	public PostageRatePrice PostagePrice { set; get; }
    }
}
