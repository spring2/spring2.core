using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class Postage {
	public string MailService { set; get; }

	public string Zone { set; get; }

	public string IntraBMC { set; get; }

	public string Pricing { set; get; }

	public decimal TotalAmount { set; get; }
    }
}
