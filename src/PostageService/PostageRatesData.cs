using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class PostageRatesData {
	public int Status { set; get; }

	public string ErrorMessage { set; get; }

	public IList<PostageRatePrice> PostagePrice { set; get; }
    }
}
