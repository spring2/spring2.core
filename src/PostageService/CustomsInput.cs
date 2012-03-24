using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    //CustomsInfo
    public class CustomsInput {

	public string ContentsType { set; get; }

	public string ContentsExplanation { set; get; }

	public string RestrictionType { set; get; }

	public string RestrictionComments { set; get; }

	public string SendersCustomsReference { set; get; }

	public string ImportersCustomsReference { set; get; }

	public string LicenseNumber { set; get; }

	public string CertificateNumber { set; get; }

	public string InvoiceNumber { set; get; }

	public string NonDeliveryOption { set; get; }

	public string InsuredNumber { set; get; }

	public string EelPfc { set; get; }

	public CustomsItem[] CustomsItems { set; get; }
    }
}
