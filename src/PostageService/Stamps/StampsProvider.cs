using System;
using Spring2.Core.Configuration;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;

namespace Spring2.Core.PostageService.Stamps {
    public class StampsProvider : IPostageServiceProvider {
	string integrationId;
	string password;
	string username;
	string url;
	SWSIMV52.Credentials credentials;
	SWSIMV52.SwsimV52SoapClient client;
	SWSIMV52.AccountInfo accountInfo;
	SWSIMV52.Address address;
	string email;
	StampsModelAssembler assembler;
	string authenticator;
	DateTime lastLoginTime;
	bool clearCredential;
	string loginBannerText;
	bool passwordExpired;
	bool codewordsSet;

	public string Authenticator {
	    get {
		return authenticator;
	    }
	}

	public DateTime LastLoginTime {
	    get {
		return lastLoginTime;
	    }
	}

	public bool ClearCredential {
	    get {
		return clearCredential;
	    }
	}

	public string LoginBannerText {
	    get {
		return loginBannerText;
	    }
	}

	public bool PasswordExpired {
	    get {
		return passwordExpired;
	    }
	}

	public bool CodewordsSet {
	    get {
		return codewordsSet;
	    }
	}

	public StampsProvider() {
	    SetCredentials();
	    InitializeSWSClient();
	    AuthenticateUser();
	    SetAccountInfo();
	    assembler = new StampsModelAssembler();
	}

	public StampsProvider(string integrationId, string password, string username, string postageServerUrl) {
	    SetCredentials(integrationId, password, username, postageServerUrl);	    
	    InitializeSWSClient();
	    AuthenticateUser();
	    SetAccountInfo();
	    assembler = new StampsModelAssembler();
	}

	public SWSIMV52.GetAccountInfoResponse GetAccountInfo() {
	    SWSIMV52.GetAccountInfoRequest request = new SWSIMV52.GetAccountInfoRequest(Authenticator);
	    SWSIMV52.GetAccountInfoResponse response = new SWSIMV52.GetAccountInfoResponse();
	    authenticator = client.GetAccountInfo(request.Item, out response.AccountInfo, out response.Address, out response.CustomerEmail);
	    accountInfo = response.AccountInfo;
	    address = response.Address;
	    email = response.CustomerEmail;
	    return response;
	}

	public SWSIMV52.CleanseAddressResponse CleanseAddress(SWSIMV52.Address address, string fromZipCode) {
	    SWSIMV52.CleanseAddressRequest request = new SWSIMV52.CleanseAddressRequest(Authenticator, address, fromZipCode);
	    SWSIMV52.CleanseAddressResponse response = new SWSIMV52.CleanseAddressResponse();
	    authenticator = client.CleanseAddress(request.Item, ref request.Address, request.FromZIPCode, out response.AddressMatch, out response.CityStateZipOK, out response.ResidentialDeliveryIndicator,
				    out response.IsPOBox, out response.CandidateAddresses, out response.StatusCodes, out response.Rates);
	    response.Address = request.Address;
	    return response;
	}
	public RefundRequestData RefundRequest(String trackingNumber, bool isInternational) {
	    SWSIMV52.CancelIndiciumRequest request = new SWSIMV52.CancelIndiciumRequest(Authenticator, trackingNumber);
	    authenticator = client.CancelIndicium(request.Item, request.Item1);
	    SWSIMV52.CancelIndiciumResponse response = new SWSIMV52.CancelIndiciumResponse(Authenticator);
	    return AutoMapper.Mapper.Map<SWSIMV52.CancelIndiciumResponse, RefundRequestData>(response);
	}

	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    // no SWS implementation
	    throw new NotImplementedException();
	}

	public SWSIMV52.RateV19 GetPostageRate(SWSIMV52.RateV19 rate, string insured) {
	    SWSIMV52.GetRatesResponse response = new SWSIMV52.GetRatesResponse();
	    authenticator = client.GetRates(Authenticator, rate, out response.Rates);
	    SWSIMV52.RateV19 returnRate = response.Rates[0];
	    returnRate.AddOns = assembler.ToRateAddons(returnRate.AddOns, returnRate.RequiresAllOf, insured);
	    return returnRate;
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    SWSIMV52.RateV19 rate = assembler.ToRate(data);
	    SWSIMV52.GetRatesResponse response = new SWSIMV52.GetRatesResponse();
	    authenticator = client.GetRates(Authenticator, rate, out response.Rates);
	    return assembler.ToPostageRatesData(response);
	}

	public SWSIMV52.RateV19[] GetPostageRates(SWSIMV52.RateV19 rate) {
	    SWSIMV52.GetRatesResponse response = new SWSIMV52.GetRatesResponse();
	    authenticator = client.GetRates(Authenticator, rate, out response.Rates);
	    return response.Rates;
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    SWSIMV52.CreateIndiciumRequest request = assembler.ToCreateIndiciumRequest(data);
	    request.Rate = GetPostageRate(request.Rate, data.Services == null ? null : data.Services.InsuredMail);
	    request.Item = Authenticator;
	    SWSIMV52.CreateIndiciumResponse response = new SWSIMV52.CreateIndiciumResponse();
	    authenticator = client.CreateIndicium(request.Item, ref request.IntegratorTxID, ref response.TrackingNumber, ref request.Rate, request.From, request.To, 
				    request.CustomerID, request.Customs, request.SampleOnly, request.PostageMode, request.ImageType, request.EltronPrinterDPIType, 
				    request.memo, request.cost_code_id, request.deliveryNotification, request.ShipmentNotification, request.rotationDegrees,
				    request.horizontalOffset, request.verticalOffset, request.printDensity, request.printMemo, request.printInstructions,
				    request.requestPostageHash, request.nonDeliveryOption, request.RedirectTo, request.OriginalPostageHash, request.ReturnImageData,
				    request.InternalTransactionNumber, request.PaperSize, request.EmailLabelTo, request.PayOnPrint, request.ReturnLabelExpirationDays,
				    request.ImageDpi, request.RateToken, request.OrderId, out response.StampsTxID, out response.URL, out response.PostageBalance, 
				    out response.Mac, out response.PostageHash, out response.ImageData);
	    response.Rate = request.Rate;
	    return assembler.ToPostageLabelData(response, accountInfo);
	}

	public PasswordChangedData ChangePassword(ChangePasswordInputData data) {
	    SWSIMV52.ChangePasswordRequest request = assembler.ToChangePasswordRequest(data, Authenticator, credentials.Password);
	    authenticator = client.ChangePassword(request.Item, request.OldPassword, request.NewPassword);
	    SWSIMV52.ChangePasswordResponse response = new SWSIMV52.ChangePasswordResponse(Authenticator);
	    return AutoMapper.Mapper.Map<SWSIMV52.ChangePasswordResponse, PasswordChangedData>(response);
	}

	public SWSIMV52.CreateScanFormResponse GetScanForm(string[] integratorTxIDs, SWSIMV52.Address address) {
	    Guid[] integratorTxIDGuids = new Guid[integratorTxIDs.Length];
	    for (int i = 0; i < integratorTxIDs.Length; i++) {
		integratorTxIDGuids[i] = new Guid(integratorTxIDs[i]);
	    }
	    SWSIMV52.CreateScanFormRequest request = new SWSIMV52.CreateScanFormRequest(Authenticator, integratorTxIDGuids, address, SWSIMV52.ImageType.Pdf, true, SWSIMV52.Carrier.Usps, null);
	    SWSIMV52.CreateScanFormResponse response = new SWSIMV52.CreateScanFormResponse();
	    authenticator = client.CreateScanForm(request.Item, request.StampsTxIDs, request.FromAddress, request.ImageType, request.PrintInstructions, request.Carrier, request.ShipDate, out response.ScanFormId, out response.Url);
	    return response;
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    SWSIMV52.PurchasePostageRequest request = assembler.ToPurchasePostageRequest(data, Authenticator, accountInfo);
	    SWSIMV52.PurchasePostageResponse response = new SWSIMV52.PurchasePostageResponse();
	    authenticator = client.PurchasePostage(request.Item, request.PurchaseAmount, request.ControlTotal, request.MI, request.IntegratorTxID, 
				    out response.PurchaseStatus, out response.TransactionID, out response.PostageBalance,
				    out response.RejectionReason, out response.MIRequired);
	    return AutoMapper.Mapper.Map<SWSIMV52.PurchasePostageResponse, PurchasedPostageData>(response);
	}

	public SWSIMV52.GetPurchaseStatusResponse GetPurchaseStatus(int transactionId) {
	    SWSIMV52.GetPurchaseStatusResponse response  = new SWSIMV52.GetPurchaseStatusResponse();
	    authenticator = client.GetPurchaseStatus(authenticator, transactionId, out response.PurchaseStatus, out response.PostageBalance, out response.RejectionReason, out response.MIRequired);
	    return response;
	}

	private void SetCredentials() {
	    SetCredentials(ConfigurationProvider.Instance.Settings["PostageService.Stamps.IntegrationId"],
		ConfigurationProvider.Instance.Settings["PostageService.Stamps.Password"],
		ConfigurationProvider.Instance.Settings["PostageService.Stamps.Username"],
		ConfigurationProvider.Instance.Settings["PostageService.Stamps.PostageServerUrl"]);
	}

	public void SetCredentials(string integrationId, string password, string username, string url) {
	    this.integrationId = integrationId;
	    this.password = password;
	    this.username = username;
	    this.url = url;
	    this.credentials = new SWSIMV52.Credentials {
		IntegrationID = new Guid(integrationId),
		Password = password,
		Username = username
	    };
	}

	private void InitializeSWSClient() {
	    BasicHttpBinding binding = new BasicHttpBinding();
	    binding.Name = "StampsBinding";
	    binding.CloseTimeout = new TimeSpan(0, 1, 0);
	    binding.OpenTimeout = new TimeSpan(0, 1, 0);
	    binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
	    binding.SendTimeout = new TimeSpan(0, 1, 0);
	    binding.AllowCookies = false;
	    binding.BypassProxyOnLocal = false;
	    binding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
	    binding.MaxBufferSize = 1000000;
	    binding.MaxBufferPoolSize = 524288;
	    binding.MaxReceivedMessageSize = 1000000;
	    binding.MessageEncoding = WSMessageEncoding.Text;
	    binding.TextEncoding = Encoding.UTF8;
	    binding.TransferMode = TransferMode.Buffered;
	    binding.UseDefaultWebProxy = true;

	    binding.ReaderQuotas.MaxDepth = 32;
	    binding.ReaderQuotas.MaxStringContentLength = 1000000;
	    binding.ReaderQuotas.MaxArrayLength = 16384;
	    binding.ReaderQuotas.MaxBytesPerRead = 4096;
	    binding.ReaderQuotas.MaxNameTableCharCount = 16384;

	    binding.Security.Mode = BasicHttpSecurityMode.Transport;
	    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
	    binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
	    binding.Security.Transport.Realm = "";
	    binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
	    binding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;

	    EndpointAddress endpointAddress = new EndpointAddress(url);
	    client = new SWSIMV52.SwsimV52SoapClient(binding, endpointAddress);
	}

	public void AuthenticateUser() {
	    authenticator = client.AuthenticateUser(credentials, out lastLoginTime, out clearCredential, out loginBannerText, out passwordExpired, out codewordsSet);
	}

	private void SetAccountInfo() {
	    SWSIMV52.GetAccountInfoRequest request = new SWSIMV52.GetAccountInfoRequest() { Item = Authenticator };
	    authenticator = client.GetAccountInfo(request.Item, out accountInfo, out address, out email);
	}
    }
}
