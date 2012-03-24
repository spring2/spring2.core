using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PostagePurchaseInputData {
	public string RequesterID { set; get; }

	public string RequestID { set; get; }

	public Credentials CertifiedIntermediary { set; get; }

	public string RecreditAmount { set; get; }
    }
}
