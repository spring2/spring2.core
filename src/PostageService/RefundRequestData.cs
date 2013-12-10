using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class RefundRequestData {
	public String FormNumber { get; set; }

	public String PICNumber { get; set; }

	public Boolean IsApproved { get; set; }

	public String ErrorMsg { get; set; }
    }
}
