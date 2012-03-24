using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    //PostageRateResponse
    public class PostageRateData {
	public int Status { set; get; }

	public string ErrorMessage { set; get; }

	public string Zone { set; get; }

	public PostageRate[] Postage { set; get; }

	public PostageRatePrice[] PostagePrice { set; get; }
    }
}
