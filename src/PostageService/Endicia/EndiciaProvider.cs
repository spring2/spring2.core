using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using AutoMapper;
using Spring2.Core.Configuration;
using Spring2.Core.EwsLabelService;
using EW = Spring2.Core.EwsLabelService;
using S2 = Spring2.Core.PostageService;

namespace Spring2.Core.PostageService.Endicia {
    public class EndiciaProvider : IPostageServiceProvider {
	EwsLabelServiceSoapClient client;
	string accountId; //accountId
	string password;
	string partnerId;

	public EndiciaProvider() {
	    InitializeWSClient();
	    MapObjects();
	    SetCredentials();
	}

	#region initializations
	private void MapObjects() {
	    Mapper.CreateMap<PostageRateInputData, PostageRateRequest>();
	    Mapper.CreateMap<PostageRateResponse, PostageRateData>();
	    Mapper.CreateMap<PostageRateInputData, PostageRatesRequest>();
	    Mapper.CreateMap<PostageRatesResponse, PostageRatesData>();
	    Mapper.CreateMap<PostagePurchaseInputData, RecreditRequest>();
	    Mapper.CreateMap<RecreditRequestResponse, PurchasedPostageData>();
	    Mapper.CreateMap<PostageLabelInputData, LabelRequest>();
	    Mapper.CreateMap<LabelRequestResponse, PostageLabelData>();
	    Mapper.CreateMap<Credentials, CertifiedIntermediary>()
		.ForMember(x => x.PassPhrase, o => o.MapFrom(src => src.Password))
		.ForMember(x => x.AccountID, o => o.MapFrom(src => src.Username));
	    Mapper.CreateMap<CertifiedIntermediary, Credentials>()
		.ForMember(x => x.Password, o => o.MapFrom(src => src.PassPhrase))
		.ForMember(x => x.Username, o => o.MapFrom(src => src.AccountID));
	    Mapper.CreateMap<PackageDimensions, Dimensions>();
	    Mapper.CreateMap<Services, SpecialServices>();
	    Mapper.CreateMap<ReturnOptions, ResponseOptions>();
	    Mapper.CreateMap<EW.PostageRate, S2.PostageRate>();
	    Mapper.CreateMap<EW.PostagePrice, S2.PostageRatePrice>();
	    Mapper.CreateMap<EW.Postage, S2.Postage>();
	    Mapper.CreateMap<EW.Fees, S2.Fees>();
	    Mapper.CreateMap<EW.CertifiedIntermediaryStatus, S2.AccountInfo>();
	    Mapper.CreateMap<S2.CustomsInput, EW.CustomsInfo>();
	    Mapper.CreateMap<S2.CustomsItem, EW.CustomsItem>();
	    Mapper.CreateMap<EW.ImageSet, S2.ImageSet>();
	    Mapper.CreateMap<EW.ImageData, S2.ImageData>();

	    Mapper.AssertConfigurationIsValid();
	}
	private void InitializeWSClient() {
	    string uri = ConfigurationProvider.Instance.Settings["PostageService.Endicia.PostageServerUrl"] ??
		"https://www.envmgr.com/LabelService/EwsLabelService.asmx"; //This is their test server.
	    BasicHttpBinding binding = new BasicHttpBinding();
	    binding.Name = "EndiciaBinding";
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

	    EndpointAddress endpointAddress = new EndpointAddress(uri);
	    client = new EwsLabelServiceSoapClient(binding, endpointAddress);
	}
	private void SetCredentials() {
	    accountId = ConfigurationProvider.Instance.Settings["PostageService.Endicia.AccountId"];
	    password = ConfigurationProvider.Instance.Settings["PostageService.Endicia.Password"];
	    partnerId = ConfigurationProvider.Instance.Settings["PostageService.Endicia.PartnerId"];
	}
	#endregion

	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    PostageRateRequest request = Mapper.Map<PostageRateInputData, PostageRateRequest>(data);
	    request.CertifiedIntermediary = new CertifiedIntermediary {
		AccountID = accountId,
		PassPhrase = password
	    };
	    PostageRateResponse response = client.CalculatePostageRate(request);
	    return Mapper.Map<PostageRateResponse, PostageRateData>(response);
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    PostageRatesRequest request = Mapper.Map<PostageRateInputData, PostageRatesRequest>(data);
	    request.CertifiedIntermediary = new CertifiedIntermediary {
		AccountID = accountId,
		PassPhrase = password
	    };
	    PostageRatesResponse response = client.CalculatePostageRates(request);
	    return Mapper.Map<PostageRatesResponse, PostageRatesData>(response);
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    RecreditRequest request = Mapper.Map<PostagePurchaseInputData, RecreditRequest>(data);
	    request.CertifiedIntermediary = new CertifiedIntermediary {
		AccountID = accountId,
		PassPhrase = password
	    };
	    RecreditRequestResponse response = client.BuyPostage(request);
	    return Mapper.Map<RecreditRequestResponse, PurchasedPostageData>(response);
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    LabelRequest request = Mapper.Map<PostageLabelInputData, LabelRequest>(data);
	    request.AccountID = accountId;
	    request.PassPhrase = password;
	    request.RequesterID = partnerId;
	    LabelRequestResponse response = client.GetPostageLabel(request);
	    return Mapper.Map<LabelRequestResponse, PostageLabelData>(response);
	}
    }
}

