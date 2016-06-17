using System;

namespace Spring2.Core.PostageService {
    public interface IPostageServiceProvider {
	PostageRateData GetPostageRate(PostageRateInputData data);
	PostageRatesData GetPostageRates(PostageRateInputData data);
	PurchasedPostageData BuyPostage(PostagePurchaseInputData data);
	PostageLabelData GetPostageLabel(PostageLabelInputData data);
	PasswordChangedData ChangePassword(ChangePasswordInputData data);
	RefundRequestData RefundRequest(String trackingNumber, Boolean isInternational); 
    }
}
