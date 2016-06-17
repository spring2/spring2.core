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
	DateTime lastLoginTime;
	bool clearCredential;
	string loginBannerText;
	bool passwordExpired;
	bool codewordsSet;

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
	public RefundRequestData RefundRequest(String trackingNumber, bool isInternational) {
	    SWSIMV52.CancelIndiciumRequest request = new SWSIMV52.CancelIndiciumRequest(credentials, trackingNumber);
	    SWSIMV52.CancelIndiciumResponse response = new SWSIMV52.CancelIndiciumResponse(client.CancelIndicium(request.Item, request.Item1));
	    return AutoMapper.Mapper.Map<SWSIMV52.CancelIndiciumResponse, RefundRequestData>(response);
	}

	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    // no SWS implementation
	    throw new NotImplementedException();
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    SWSIMV52.RateV19 rate = assembler.ToRate(data);
	    SWSIMV52.GetRatesResponse response = new SWSIMV52.GetRatesResponse();
	    client.GetRates(credentials, rate, out response.Rates);
	    return assembler.ToPostageRatesData(response);
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    SWSIMV52.CreateIndiciumRequest request = assembler.ToCreateIndiciumRequest(data, credentials);
	    SWSIMV52.CreateIndiciumResponse response = new SWSIMV52.CreateIndiciumResponse();
	    client.CreateIndicium(request.Item, ref request.IntegratorTxID, ref response.TrackingNumber, ref request.Rate, request.From, request.To, 
				    request.CustomerID, request.Customs, request.SampleOnly, request.PostageMode, request.ImageType, request.EltronPrinterDPIType, 
				    request.memo, request.cost_code_id, request.deliveryNotification, request.ShipmentNotification, request.rotationDegrees,
				    request.horizontalOffset, request.verticalOffset, request.printDensity, request.printMemo, request.printInstructions,
				    request.requestPostageHash, request.nonDeliveryOption, request.RedirectTo, request.OriginalPostageHash, request.ReturnImageData,
				    request.InternalTransactionNumber, request.PaperSize, request.EmailLabelTo, request.PayOnPrint, request.ReturnLabelExpirationDays,
				    request.ImageDpi, request.RateToken, request.OrderId, out response.StampsTxID, out response.URL, out response.PostageBalance, 
				    out response.Mac, out response.PostageHash, out response.ImageData);
	    return assembler.ToPostageLabelData(response, accountInfo);
	}

	public PasswordChangedData ChangePassword(ChangePasswordInputData data) {
	    SWSIMV52.ChangePasswordRequest request = assembler.ToChangePasswordRequest(data, credentials);
	    SWSIMV52.ChangePasswordResponse response = new SWSIMV52.ChangePasswordResponse(client.ChangePassword(request.Item, request.OldPassword, request.NewPassword));
	    return AutoMapper.Mapper.Map<SWSIMV52.ChangePasswordResponse, PasswordChangedData>(response);
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    SWSIMV52.PurchasePostageRequest request = assembler.ToPurchasePostageRequest(data, credentials, accountInfo);
	    SWSIMV52.PurchasePostageResponse response = new SWSIMV52.PurchasePostageResponse();
	    client.PurchasePostage(request.Item, request.PurchaseAmount, request.ControlTotal, request.MI, request.IntegratorTxID, 
				    out response.PurchaseStatus, out response.TransactionID, out response.PostageBalance,
				    out response.RejectionReason, out response.MIRequired);
	    return AutoMapper.Mapper.Map<SWSIMV52.PurchasePostageResponse, PurchasedPostageData>(response);
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
	    client.AuthenticateUser(credentials, out lastLoginTime, out clearCredential, out loginBannerText, out passwordExpired, out codewordsSet);
	}

	private void SetAccountInfo() {
	    SWSIMV52.GetAccountInfoRequest request = new SWSIMV52.GetAccountInfoRequest();
	    client.GetAccountInfo(credentials, out accountInfo, out address, out email);
	}
    }
}
