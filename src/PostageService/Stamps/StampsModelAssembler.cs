using System;
using System.Collections.Generic;
using Spring2.Core.PostageService.Enums;
using System.Text.RegularExpressions;

namespace Spring2.Core.PostageService.Stamps {
    internal class StampsModelAssembler {
	
	static StampsModelAssembler() {
	    AutoMapper.Mapper.CreateMap<PostageRateInputData, SWSIMV52.RateV19>()
		.ForMember(x => x.FromZIPCode, o => o.MapFrom(src => src.FromPostalCode))
		.ForMember(x => x.ToZIPCode, o => o.MapFrom(src => src.ToPostalCode))
		.ForMember(x => x.Length, o => o.MapFrom(src => src.MailpieceDimensions.Length))
		.ForMember(x => x.Height, o => o.MapFrom(src => src.MailpieceDimensions.Height))
		.ForMember(x => x.Width, o => o.MapFrom(src => src.MailpieceDimensions.Width))
		.ForMember(x => x.ShipDate, o => o.MapFrom(src => (src.ShipDate == null) ? DateTime.Parse("0001-01-01") : DateTime.Parse(src.ShipDate)))
		.ForMember(x => x.RegisteredValue, o => o.MapFrom(src => src.RegisteredMailValue))
		.ForMember(x => x.CODValue, o => o.MapFrom(src => src.CODAmount))
		.ForMember(x => x.NonMachinable, o => o.MapFrom(src => String.IsNullOrWhiteSpace(src.Machinable) ? false : !Convert.ToBoolean(src.Machinable)));

	    AutoMapper.Mapper.CreateMap<PostagePurchaseInputData, SWSIMV52.PurchasePostageRequest>()
		.ForMember(x => x.PurchaseAmount, o => o.MapFrom(src => decimal.Parse(src.RecreditAmount)));

	    AutoMapper.Mapper.CreateMap<ChangePasswordInputData, SWSIMV52.ChangePasswordRequest>();

	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, SWSIMV52.CreateIndiciumRequest>()
		.ForMember(x => x.Rate, o => o.MapFrom(src => AutoMapper.Mapper.Map<PostageLabelInputData, SWSIMV52.RateV19>(src)))
		.ForMember(x => x.To, o => o.MapFrom(src => new SWSIMV52.Address() {
		    Address1 = src.ToAddress1,
		    Address2 = src.ToAddress2,
		    Address3 = src.ToAddress3,
		    City = src.ToCity,
		    Country = (src.CustomsInfo != null && !String.IsNullOrWhiteSpace(src.CustomsInfo.ContentsType)) ? src.ToCountry : null,
		    ZIPCode = (src.ToPostalCode.Length > 5) ? null : src.ToPostalCode,
		    State = src.ToState,
		    PostalCode = (src.CustomsInfo != null && !String.IsNullOrWhiteSpace(src.CustomsInfo.ContentsType)) ? src.ToPostalCode : null,
		    Province = (src.CustomsInfo != null && !String.IsNullOrWhiteSpace(src.CustomsInfo.ContentsType)) ? src.ToState : null,
		    Company = src.ToCompany,
		    FullName = src.ToName,
		    PhoneNumber = src.ToPhone
		}))
		.ForMember(x => x.From, o => o.MapFrom(src => new SWSIMV52.Address() {
		    Address1 = src.ReturnAddress1,
		    Address2 = src.ReturnAddress2,
		    Address3 = src.ReturnAddress3,
		    City = src.FromCity,
		    Country = src.FromCountry,
		    ZIPCode = src.FromPostalCode,
		    State = src.FromState,
		    Company = src.FromCompany,
		    FullName = src.FromName,
		    PhoneNumber = src.FromPhone
		}))
		.ForMember(x => x.Customs, o => o.MapFrom(src => new SWSIMV52.CustomsV4() {
		    ContentType = String.IsNullOrWhiteSpace(src.CustomsInfo.ContentsType) ? SWSIMV52.ContentTypeV2.Other : AutoMapper.Mapper.Map<string, SWSIMV52.ContentTypeV2>(src.CustomsInfo.ContentsType),
		    Comments = src.CustomsInfo.RestrictionComments,
		    LicenseNumber = src.CustomsInfo.LicenseNumber,
		    CertificateNumber = src.CustomsInfo.CertificateNumber,
		    InvoiceNumber = src.CustomsInfo.InvoiceNumber,
		    CustomsSigner = src.CustomsSigner
		}))
		.ForMember(x => x.ImageType, o => o.UseValue(SWSIMV52.ImageType.Png))
		.ForMember(x => x.rotationDegrees, o => o.MapFrom(src => String.IsNullOrWhiteSpace(src.ImageRotation) ? 0 : int.Parse(src.ImageRotation)))
		.ForMember(x => x.nonDeliveryOption, o => o.MapFrom(src => String.IsNullOrWhiteSpace(src.NonDeliveryOption) ? SWSIMV52.NonDeliveryOption.Undefined : AutoMapper.Mapper.Map<string, SWSIMV52.NonDeliveryOption>(src.NonDeliveryOption)))
		.ForMember(x => x.TrackingNumber, o => o.UseValue(String.Empty))
		.ForMember(x => x.ReturnImageData, o => o.UseValue(true))
		.ForMember(x => x.PaperSize, o => o.UseValue(SWSIMV52.PaperSizeV1.LabelSize))
		.ForMember(x => x.EltronPrinterDPIType, o => o.UseValue(SWSIMV52.EltronPrinterDPIType.High))
		.ForMember(x => x.ImageDpi, o => o.UseValue(SWSIMV52.ImageDpi.ImageDpi300))
		.ForMember(x => x.SampleOnly, o => o.MapFrom(src => src.Test))
        .ForMember(x => x.printInstructions, o => o.UseValue(false));

	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, SWSIMV52.RateV19>()
		.ForMember(x => x.FromZIPCode, o => o.MapFrom(src => src.FromPostalCode))
		.ForMember(x => x.ToZIPCode, o => o.MapFrom(src => src.ToPostalCode))
		.ForMember(x => x.Length, o => o.MapFrom(src => src.MailpieceDimensions.Length))
		.ForMember(x => x.Height, o => o.MapFrom(src => src.MailpieceDimensions.Height))
		.ForMember(x => x.Width, o => o.MapFrom(src => src.MailpieceDimensions.Width))
		.ForMember(x => x.ShipDate, o => o.MapFrom(src => (src.ShipDate == null) ? DateTime.Parse("0001-01-01") : DateTime.Parse(src.ShipDate)))
		.ForMember(x => x.RegisteredValue, o => o.MapFrom(src => src.RegisteredMailValue))
		.ForMember(x => x.CODValue, o => o.MapFrom(src => src.CODAmount))
		.ForMember(x => x.NonMachinable, o => o.MapFrom(src => String.IsNullOrWhiteSpace(src.Machinable) ? false : !Convert.ToBoolean(src.Machinable)))
		.ForMember(x => x.PrintLayout, o => o.UseValue("Normal"))
		.ForMember(x => x.DeclaredValue, o => o.MapFrom(src => (decimal)src.Value))
		.ForMember(x => x.InsuredValue, o => o.MapFrom(src => (decimal)src.Value));
	    
	    AutoMapper.Mapper.CreateMap<SWSIMV52.CancelIndiciumResponse, RefundRequestData>();
	    AutoMapper.Mapper.CreateMap<SWSIMV52.CreateIndiciumResponse, PostageLabelData>()
		.ForMember(x => x.PostageBalance, o => o.Ignore())
		.ForMember(x => x.Base64LabelImage, o => o.MapFrom(src => Convert.ToBase64String(src.ImageData[0])))
		.ForMember(x => x.PostagePrice, o => o.MapFrom(src => new PostageRatePrice() {
		    TotalAmount = src.Rate.Amount,
		    MailClass = src.Rate.ServiceType.ToString()
		}))
		.ForMember(x => x.ReferenceID, o => o.MapFrom(src => src.StampsTxID.ToString()));
	    AutoMapper.Mapper.CreateMap<SWSIMV52.PurchasePostageResponse, PurchasedPostageData>()
		.ForMember(x => x.ErrorMessage, o => o.MapFrom(src => src.RejectionReason))
		.ForMember(x => x.RequestID, o => o.MapFrom(src => src.TransactionID.ToString()))
		.ForMember(x => x.Status, o => o.MapFrom(src => src.PurchaseStatus.ToString()))
		.ForMember(x => x.CertifiedIntermediary, o => o.MapFrom(src => new AccountInfo() {
		    PostageBalance = src.PostageBalance.AvailablePostage,
		    AscendingBalance = src.PostageBalance.ControlTotal
		}));
	    AutoMapper.Mapper.CreateMap<SWSIMV52.ChangePasswordResponse, PasswordChangedData>();

	    // Enums
	    AutoMapper.Mapper.CreateMap<SWSIMV52.ServiceType, string>().ConvertUsing(x => x.ToString());
	    AutoMapper.Mapper.CreateMap<string, SWSIMV52.ContentTypeV2>().ConvertUsing(x => (SWSIMV52.ContentTypeV2)Enum.Parse(typeof(SWSIMV52.ContentTypeV2), x, true));
	    AutoMapper.Mapper.CreateMap<string, SWSIMV52.ImageType>().ConvertUsing(x => (SWSIMV52.ImageType)Enum.Parse(typeof(SWSIMV52.ImageType), x, true));
	    AutoMapper.Mapper.CreateMap<string, SWSIMV52.NonDeliveryOption>().ConvertUsing(x => (SWSIMV52.NonDeliveryOption)Enum.Parse(typeof(SWSIMV52.NonDeliveryOption), x, true));
	}

	public SWSIMV52.RateV19 ToRate(PostageRateInputData data) {
	    SWSIMV52.RateV19 rate = AutoMapper.Mapper.Map<PostageRateInputData, SWSIMV52.RateV19>(data);
	    rate.ServiceType = ToServiceType(data.MailClass);
	    rate.PackageType = ToPackageType(data.MailpieceShape);
	    return rate;
	}

	public SWSIMV52.PurchasePostageRequest ToPurchasePostageRequest(PostagePurchaseInputData data, string authenticator, SWSIMV52.AccountInfo accountInfo) {
	    SWSIMV52.PurchasePostageRequest request = AutoMapper.Mapper.Map<PostagePurchaseInputData, SWSIMV52.PurchasePostageRequest>(data);
	    request.Item = authenticator;
	    request.ControlTotal = accountInfo.PostageBalance.ControlTotal;
	    request.IntegratorTxID = Guid.NewGuid().ToString();
	    return request;
	}

	public SWSIMV52.ChangePasswordRequest ToChangePasswordRequest(ChangePasswordInputData data, string authenticator, string oldPassword) {
	    SWSIMV52.ChangePasswordRequest request = AutoMapper.Mapper.Map<ChangePasswordInputData, SWSIMV52.ChangePasswordRequest>(data);
	    request.Item = authenticator;
	    request.OldPassword = oldPassword;
	    return request;
	}

	public SWSIMV52.CreateIndiciumRequest ToCreateIndiciumRequest(PostageLabelInputData data) {
	    SWSIMV52.CreateIndiciumRequest request = AutoMapper.Mapper.Map<PostageLabelInputData, SWSIMV52.CreateIndiciumRequest>(data);
	    request.Rate.ServiceType = ToServiceType(data.MailClass);
	    request.Rate.PackageType = ToPackageType(data.MailpieceShape);
	    request.IntegratorTxID = Guid.NewGuid().ToString();
	    if (data.CustomsInfo != null && !String.IsNullOrWhiteSpace(data.CustomsInfo.ContentsType)) {
		List<SWSIMV52.CustomsLine> customsLines = new List<SWSIMV52.CustomsLine>();
		customsLines.Add(new SWSIMV52.CustomsLine() {
		    CountryOfOrigin = data.CustomsCountry1,
		    Description = data.CustomsDescription1,
		    Quantity = data.CustomsQuantity1,
		    Value = decimal.Parse(data.CustomsValue1.ToString()),
		    WeightOz = double.Parse(data.CustomsWeight1.ToString())
		});
		if (!String.IsNullOrWhiteSpace(data.CustomsDescription2)) {
		    customsLines.Add(new SWSIMV52.CustomsLine() {
			CountryOfOrigin = data.CustomsCountry2,
			Description = data.CustomsDescription2,
			Quantity = data.CustomsQuantity2,
			Value = decimal.Parse(data.CustomsValue2.ToString()),
			WeightOz = double.Parse(data.CustomsWeight2.ToString())
		    });
		}
		if (!String.IsNullOrWhiteSpace(data.CustomsDescription3)) {
		    customsLines.Add(new SWSIMV52.CustomsLine() {
			CountryOfOrigin = data.CustomsCountry3,
			Description = data.CustomsDescription3,
			Quantity = data.CustomsQuantity3,
			Value = decimal.Parse(data.CustomsValue3.ToString()),
			WeightOz = double.Parse(data.CustomsWeight3.ToString())
		    });
		}
		if (!String.IsNullOrWhiteSpace(data.CustomsDescription4)) {
		    customsLines.Add(new SWSIMV52.CustomsLine() {
			CountryOfOrigin = data.CustomsCountry4,
			Description = data.CustomsDescription4,
			Quantity = data.CustomsQuantity4,
			Value = decimal.Parse(data.CustomsValue4.ToString()),
			WeightOz = double.Parse(data.CustomsWeight4.ToString())
		    });
		}
		if (!String.IsNullOrWhiteSpace(data.CustomsDescription5)) {
		    customsLines.Add(new SWSIMV52.CustomsLine() {
			CountryOfOrigin = data.CustomsCountry5,
			Description = data.CustomsDescription5,
			Quantity = data.CustomsQuantity5,
			Value = decimal.Parse(data.CustomsValue5.ToString()),
			WeightOz = double.Parse(data.CustomsWeight5.ToString())
		    });
		}
		request.Customs.CustomsLines = customsLines.ToArray();
		request.Customs.ContentType = (SWSIMV52.ContentTypeV2)Enum.Parse(typeof(SWSIMV52.ContentTypeV2), data.CustomsInfo.ContentsType);
	    }
	    return request;
	}

	public PostageRatesData ToPostageRatesData(SWSIMV52.GetRatesResponse response) {
	    PostageRatesData rates = new PostageRatesData();
	    if (response.Rates != null && response.Rates.Length > 0) {
		foreach (SWSIMV52.RateV19 rate in response.Rates) {
		    rates.PostagePrice = new List<PostageRatePrice>();
		    rates.PostagePrice.Add(new PostageRatePrice() {
			TotalAmount = rate.Amount,
			MailClass = rate.ServiceType.ToString()
		    });
		}
	    }
	    return rates;
	}

	public PostageLabelData ToPostageLabelData(SWSIMV52.CreateIndiciumResponse response, SWSIMV52.AccountInfo accountInfo) {
	    PostageLabelData labelData = AutoMapper.Mapper.Map<SWSIMV52.CreateIndiciumResponse, PostageLabelData>(response);
	    labelData.PostageBalance = accountInfo.PostageBalance.AvailablePostage;
	    return labelData;
	}

	public SWSIMV52.PackageTypeV6 ToPackageType(MailpieceShapeEnum packageType) {
	    foreach(SWSIMV52.PackageTypeV6 swsPackageType in Enum.GetValues(typeof(SWSIMV52.PackageTypeV6))) {
		if (Regex.Replace(packageType.ToString().ToUpper(), @"\s+", "").Equals(Regex.Replace(swsPackageType.ToString().ToUpper(), @"\s+", ""))) {
		    return swsPackageType;
		}
	    }
	    return SWSIMV52.PackageTypeV6.Unknown;
	}

	public SWSIMV52.ServiceType ToServiceType(MailClassEnum mailClass) {
	    try {
		string mailClassString = mailClassToServiceType[Regex.Replace(mailClass.ToString(), @"\s+", "")];
		foreach(SWSIMV52.ServiceType swsServiceType in Enum.GetValues(typeof(SWSIMV52.ServiceType))) {
		    if (mailClassString.Equals(swsServiceType.ToString())) {
			return swsServiceType;
		    }
		}
		return SWSIMV52.ServiceType.Unknown;
		    }
	    catch {
		return SWSIMV52.ServiceType.Unknown;
	    }
	}

	public SWSIMV52.AddOnV7[] ToRateAddons(SWSIMV52.AddOnV7[] availableAddons, SWSIMV52.AddOnTypeV7[][] requiredAddons, string insured) {
	    bool isInsured = !String.IsNullOrWhiteSpace(insured);
	    if ((requiredAddons == null && !isInsured) || availableAddons == null) {
		return null;
	    } else {
		List<SWSIMV52.AddOnV7> rateAddons = new List<SWSIMV52.AddOnV7>();
		for (int i = 0; i < availableAddons.Length; i++) {
		    if (availableAddons[i].AddOnType == SWSIMV52.AddOnTypeV7.SCAINS && isInsured && InsuredMailEnum.GetInstance(insured) == InsuredMailEnum.STAMPS) {
			rateAddons.Add(availableAddons[i]);
		    } else if (availableAddons[i].AddOnType == SWSIMV52.AddOnTypeV7.USAINS && isInsured && InsuredMailEnum.GetInstance(insured) == InsuredMailEnum.USPSONLINE) {
			rateAddons.Add(availableAddons[i]);
		    } else if (requiredAddons != null) {
			int requiresPosition = Array.IndexOf(requiredAddons, availableAddons[i].AddOnType);
			if (requiresPosition > -1) {
			    rateAddons.Add(availableAddons[i]);
			}
		    }
		}
		return rateAddons.ToArray();
	    }
	}

	private static readonly Dictionary<string, string> mailClassToServiceType = new Dictionary<string, string>() {
	    { "First", "USFC"},
	    { "Priority", "USPM" },
	    { "Express", "USXM" },
	    { "MediaMail", "USMM" },
	    { "LibraryMail", "USLM" },
	    { "ExpressMailInternational", "USEMI" },
	    { "PriorityMailInternational", "USPMI" },
	    { "FirstClassMailInternational", "USFCI" },
	    { "CriticalMail", "USCM" },
	    { "ParcelSelect", "USPS" },
	    { "PriorityExpress", "USXM" },
	    { "DHLGMSMParcelsExpedited", "DHLPE" },
	    { "DHLGMSMParcelsGround", "DHLPG" },
	    { "DHLGMSMParcelPlusExpedited", "DHLPPE" },
	    { "DHLGMSMBPMExpedited", "DHLBPME" },
	    { "DHLGMSMBPMGround", "DHLBPMG" },
	    { "IPA", "ASIPA" },
	    { "ISAL", "ASISAL" }
	};
    }
}
