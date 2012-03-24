using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class NullPostageServiceProvider : IPostageServiceProvider {
	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    throw new PostageServiceException("NullPostageServiceProvider is not a valid provider.");
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    throw new PostageServiceException("NullPostageServiceProvider is not a valid provider.");
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    throw new PostageServiceException("NullPostageServiceProvider is not a valid provider.");
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    throw new PostageServiceException("NullPostageServiceProvider is not a valid provider.");
	}
    }
}
