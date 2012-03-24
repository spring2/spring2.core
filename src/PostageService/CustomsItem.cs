using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class CustomsItem {
	public string Description { set; get; }

	public int Quantity { set; get; }

	public decimal Weight { set; get; }

	public decimal Value { set; get; }

	public string HSTariffNumber { set; get; }

	public string CountryOfOrigin { set; get; }
    }
}
