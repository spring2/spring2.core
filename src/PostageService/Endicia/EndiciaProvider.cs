using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using AutoMapper;
using Spring2.Core.Configuration;
using Spring2.Core.EwsLabelService;
using EW = Spring2.Core.EwsLabelService;
using S2 = Spring2.Core.PostageService;
using Spring2.Core.Types;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService.Endicia {
    public class EndiciaProvider : IPostageServiceProvider {
	EwsLabelServiceSoapClient client;
	CertifiedIntermediary credentials;
	string accountId; //accountId
	string password;
	string partnerId;
	string url;
	string testUrl = "https://www.envmgr.com/LabelService/EwsLabelService.asmx"; //This is their test server.

	public EndiciaProvider() {
	    SetCredentials();
	}

	public EndiciaProvider(string accountId, string password, string partnerId, string postageServerUrl) {
	    SetCredentials(accountId, password, partnerId, postageServerUrl);
	}

	#region initializations
	private void MapObjects() {
            Mapper.CreateMap<PostageRateInputData, PostageRateRequest>()
		.ForMember(x => x.RequesterID, o => o.UseValue(partnerId))
		.ForMember(x => x.CertifiedIntermediary, o => o.UseValue(credentials));
	    Mapper.CreateMap<PostageRateResponse, PostageRateData>();
	    Mapper.CreateMap<PostageRateInputData, PostageRatesRequest>()
		.ForMember(x => x.RequesterID, o => o.UseValue(partnerId))
		.ForMember(x => x.CertifiedIntermediary, o => o.UseValue(credentials));
	    Mapper.CreateMap<PostageRatesResponse, PostageRatesData>();
	    Mapper.CreateMap<PostagePurchaseInputData, RecreditRequest>()
		.ForMember(x => x.RequesterID, o => o.UseValue(partnerId))
		.ForMember(x => x.CertifiedIntermediary, o => o.UseValue(credentials));
	    Mapper.CreateMap<RecreditRequestResponse, PurchasedPostageData>();
	    Mapper.CreateMap<PostageLabelInputData, LabelRequest>()
		.ForMember(x => x.RequesterID, o => o.UseValue(partnerId))
		.ForMember(x => x.AccountID, o => o.UseValue(accountId))
		.ForMember(x => x.PassPhrase, o => o.UseValue(password));
	    Mapper.CreateMap<LabelRequestResponse, PostageLabelData>();
	    Mapper.CreateMap<ChangePasswordInputData, ChangePassPhraseRequest>()
		.ForMember(x => x.NewPassPhrase, o => o.MapFrom(src => src.NewPassword))
		.ForMember(x => x.RequesterID, o => o.UseValue(partnerId))
		.ForMember(x => x.CertifiedIntermediary, o => o.UseValue(credentials));
	    Mapper.CreateMap<ChangePassPhraseRequestResponse, PasswordChangedData>();
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

	    MapEnums();
	}

	private void MapEnums() {
	    Mapper.CreateMap<ContentTypeEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<EntryFacilityEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<InsuredMailEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<IntegratedFormTypeEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<MailClassEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<MailpieceShapeEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<NonDeliveryOptionEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<OnOffEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<PackageTypeIndicatorEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<PricingEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<RestrictionTypeEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<SortTypeEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);
	    Mapper.CreateMap<SundayHolidayDeliveryEnum, string>().ConvertUsing(x => (x == null) ? null : x.Code);

	    Mapper.CreateMap<string, ContentTypeEnum>().ConvertUsing(x => ContentTypeEnum.GetInstance(x));
	    Mapper.CreateMap<string, EntryFacilityEnum>().ConvertUsing(x => EntryFacilityEnum.GetInstance(x));
	    Mapper.CreateMap<string, InsuredMailEnum>().ConvertUsing(x => InsuredMailEnum.GetInstance(x));
	    Mapper.CreateMap<string, IntegratedFormTypeEnum>().ConvertUsing(x => IntegratedFormTypeEnum.GetInstance(x));
	    Mapper.CreateMap<string, MailClassEnum>().ConvertUsing(x => MailClassEnum.GetInstance(x));
	    Mapper.CreateMap<string, MailpieceShapeEnum>().ConvertUsing(x => MailpieceShapeEnum.GetInstance(x));
	    Mapper.CreateMap<string, NonDeliveryOptionEnum>().ConvertUsing(x => NonDeliveryOptionEnum.GetInstance(x));
	    Mapper.CreateMap<string, OnOffEnum>().ConvertUsing(x => OnOffEnum.GetInstance(x));
	    Mapper.CreateMap<string, PackageTypeIndicatorEnum>().ConvertUsing(x => PackageTypeIndicatorEnum.GetInstance(x));
	    Mapper.CreateMap<string, PricingEnum>().ConvertUsing(x => PricingEnum.GetInstance(x));
	    Mapper.CreateMap<string, RestrictionTypeEnum>().ConvertUsing(x => RestrictionTypeEnum.GetInstance(x));
	    Mapper.CreateMap<string, SortTypeEnum>().ConvertUsing(x => SortTypeEnum.GetInstance(x));
	    Mapper.CreateMap<string, SundayHolidayDeliveryEnum>().ConvertUsing(x => SundayHolidayDeliveryEnum.GetInstance(x));
	}
	private void InitializeWSClient() {
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

	    EndpointAddress endpointAddress = new EndpointAddress(url);
	    client = new EwsLabelServiceSoapClient(binding, endpointAddress);
	}
	private void SetCredentials() {
	    SetCredentials(ConfigurationProvider.Instance.Settings["PostageService.Endicia.AccountId"],
		ConfigurationProvider.Instance.Settings["PostageService.Endicia.Password"],
		ConfigurationProvider.Instance.Settings["PostageService.Endicia.PartnerId"],
		ConfigurationProvider.Instance.Settings["PostageService.Endicia.PostageServerUrl"]);
	}
	#endregion
	public void SetCredentials(string accountId, string password, string partnerId, string postageServerUrl) {
	    this.url = string.IsNullOrEmpty(postageServerUrl) ? testUrl : postageServerUrl;
	    this.accountId = accountId;
	    this.password = password;
	    this.partnerId = partnerId;
	    this.credentials = new CertifiedIntermediary {
		AccountID = accountId, PassPhrase = password
	    };
	    MapObjects();
	    InitializeWSClient();
	}

	public PasswordChangedData ChangePassword(ChangePasswordInputData data) {
	    ChangePassPhraseRequest request = Mapper.Map<ChangePasswordInputData, ChangePassPhraseRequest>(data);
	    ChangePassPhraseRequestResponse response = client.ChangePassPhrase(request);
	    return Mapper.Map<ChangePassPhraseRequestResponse, PasswordChangedData>(response);
	}

	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    PostageRateRequest request = Mapper.Map<PostageRateInputData, PostageRateRequest>(data);
	    PostageRateResponse response = client.CalculatePostageRate(request);
	    return Mapper.Map<PostageRateResponse, PostageRateData>(response);
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    PostageRatesRequest request = Mapper.Map<PostageRateInputData, PostageRatesRequest>(data);
	    PostageRatesResponse response = client.CalculatePostageRates(request);
	    return Mapper.Map<PostageRatesResponse, PostageRatesData>(response);
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    RecreditRequest request = Mapper.Map<PostagePurchaseInputData, RecreditRequest>(data);
	    RecreditRequestResponse response = client.BuyPostage(request);
	    return Mapper.Map<RecreditRequestResponse, PurchasedPostageData>(response);
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    LabelRequest request = Mapper.Map<PostageLabelInputData, LabelRequest>(data);
	    LabelRequestResponse response = client.GetPostageLabel(request);
	    return Mapper.Map<LabelRequestResponse, PostageLabelData>(response);
	}
    }
}

