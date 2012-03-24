using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PostageRatePrice {
	public string MailClass { set; get; }

	public Postage Postage { set; get; }

	public Fees Fees { set; get; }

	public decimal TotalAmount { set; get; }
    }
}
