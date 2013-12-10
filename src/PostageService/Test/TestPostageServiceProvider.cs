using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService.Test {
    public class TestPostageServiceProvider : IPostageServiceProvider {
	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    return new PostageRateData();
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    return new PostageRatesData();
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    return new PurchasedPostageData();
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    return new PostageLabelData();
	}

	public PasswordChangedData ChangePassword(ChangePasswordInputData data) {
	    return new PasswordChangedData();
	}

	public RefundRequestData RefundRequest(String trackingNumber) {
	    throw new NotImplementedException();
	}
    }
}
