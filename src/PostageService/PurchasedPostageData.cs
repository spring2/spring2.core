using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PurchasedPostageData {
	public int Status { set; get; }

	public string ErrorMessage { set; get; }

	public string RequesterID { set; get; }

	public string RequestID { set; get; }

	public AccountInfo CertifiedIntermediary { set; get; }
    }
}
