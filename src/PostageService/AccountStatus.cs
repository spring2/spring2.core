using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    //CertifiedIntermediaryStatus
    public class AccountInfo {
	public string AccountID { set; get; }

	public int SerialNumber { set; get; }

	public decimal PostageBalance { set; get; }

	public decimal AscendingBalance { set; get; }

	public string AccountStatus { set; get; }

	public string DeviceID { set; get; }
    }
}
