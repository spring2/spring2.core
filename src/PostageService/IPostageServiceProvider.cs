using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Spring2.Core.PostageService {
    public interface IPostageServiceProvider {
	PostageRateData GetPostageRate(PostageRateInputData data);
	PostageRatesData GetPostageRates(PostageRateInputData data);
	PurchasedPostageData BuyPostage(PostagePurchaseInputData data);
	PostageLabelData GetPostageLabel(PostageLabelInputData data);
    }
}
