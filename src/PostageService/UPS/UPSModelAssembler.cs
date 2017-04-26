using System;
using System.Collections.Generic;
using Spring2.Core.PostageService.Enums;
using System.Text.RegularExpressions;

namespace Spring2.Core.PostageService.UPS {
    internal class UPSModelAssembler {
	static UPSModelAssembler() {
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSWS.Ship.ShipmentRequest>()
		.ForMember(x => x.Request, o => o.UseValue(new UPSWS.Ship.RequestType() {
		    RequestOption = new String[] {"nonvalidate"}
		}))
		.ForMember(x => x.Shipment, o => o.MapFrom(src => AutoMapper.Mapper.Map<PostageLabelInputData, UPSWS.Ship.ShipmentType>(src)))
		.ForMember(x => x.LabelSpecification, o => o.UseValue(new UPSWS.Ship.LabelSpecificationType() {
		    LabelStockSize = new UPSWS.Ship.LabelStockSizeType() {
			Height = "4",
			Width = "6"
		    },
		    LabelImageFormat = new UPSWS.Ship.LabelImageFormatType() {
			Code = "GIF"
		    }
		}));
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSWS.Ship.ShipmentType>()
		.ForMember(x => x.Shipper, o => o.MapFrom(src => AutoMapper.Mapper.Map<PostageLabelInputData, UPSWS.Ship.ShipperType>(src)))
		.ForMember(x => x.PaymentInformation, o => o.MapFrom(src => src.IsInternational ?
		new UPSWS.Ship.PaymentInfoType() {
		    ShipmentCharge = new UPSWS.Ship.ShipmentChargeType[] {
			new UPSWS.Ship.ShipmentChargeType() {
			    BillShipper = new UPSWS.Ship.BillShipperType(),
			    Type = "01"
			},
			new UPSWS.Ship.ShipmentChargeType() {
			    BillShipper = new UPSWS.Ship.BillShipperType(),
			    Type = "02"
			}
		    }
		} :
		new UPSWS.Ship.PaymentInfoType() {
		    ShipmentCharge = new UPSWS.Ship.ShipmentChargeType[] {
			new UPSWS.Ship.ShipmentChargeType() {
			    BillShipper = new UPSWS.Ship.BillShipperType(),
			    Type = "01"
			}
		    }
		}))
		.ForMember(x => x.ShipFrom, o => o.MapFrom(src => new UPSWS.Ship.ShipFromType() {
		    Address = new UPSWS.Ship.ShipAddressType() {
			AddressLine = new String[] { src.ReturnAddress1, src.ReturnAddress2 },
			City = src.FromCity,
			PostalCode = src.FromPostalCode,
			StateProvinceCode = src.FromState,
			CountryCode = "US"
		    },
		    AttentionName = src.FromName,
		    Name = src.FromCompany
		}))
		.ForMember(x => x.ShipTo, o => o.MapFrom(src => new UPSWS.Ship.ShipToType() {
		    Address = new UPSWS.Ship.ShipToAddressType() {
			AddressLine = new String[] { src.ToAddress1, src.ToAddress2 },
			City = src.ToCity,
			PostalCode = src.ToPostalCode,
			StateProvinceCode = src.ToState,
			CountryCode = src.IsInternational ? src.ToCountry : "US"
		    },
		    AttentionName = src.ToName,
		    Name = src.ToName,
		    Phone = new UPSWS.Ship.ShipPhoneType() {
			Number = src.ToPhone
		    }
		}))
		.ForMember(x => x.Service, o => o.MapFrom(src => new UPSWS.Ship.ServiceType() {
		    Code = mailClassToServiceCode[src.MailClass.ToString()]
		}))
		.ForMember(x => x.USPSEndorsement, o => o.MapFrom(src => src.MailClass.Equals(MailClassEnum.UPSMIEXPEDITED) ? "4" : "1"))
		.ForMember(x => x.PackageID, o => o.UseValue(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30)))
		.ForMember(x => x.ShipmentServiceOptions, o => o.UseValue(new UPSWS.Ship.ShipmentTypeShipmentServiceOptions()))
		.ForMember(x => x.Package, o => o.MapFrom(src => new UPSWS.Ship.PackageType[] {
		    new UPSWS.Ship.PackageType() {
			PackageWeight = new UPSWS.Ship.PackageWeightType() {
			    Weight = mailClassToWeightUnitOfMeasure[src.MailClass.ToString()].Equals("LBS") ? (src.WeightOz / 16).ToString() : src.WeightOz.ToString(),
			    UnitOfMeasurement = new UPSWS.Ship.ShipUnitOfMeasurementType() {
				Code = mailClassToWeightUnitOfMeasure[src.MailClass.ToString()]
			    }
			},
			Packaging = new UPSWS.Ship.PackagingType() {
			    Code = mailpieceShapeToPackageTypeCode[src.MailpieceShape.ToString()]
			}
		    }
		}));
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSWS.Ship.ShipperType>()
		.ForMember(x => x.Address, o => o.MapFrom(src => new UPSWS.Ship.ShipAddressType() {
		    AddressLine = new String[] { src.ReturnAddress1, src.ReturnAddress2 },
		    City = src.FromCity,
		    PostalCode = src.FromPostalCode,
		    StateProvinceCode = src.FromState,
		    CountryCode = "US"
		}))
		.ForMember(x => x.Name, o => o.MapFrom(src => src.FromCompany));
	    AutoMapper.Mapper.CreateMap<UPSWS.Ship.ShipmentResponse, PostageLabelData>()
		.ForMember(x => x.Status, o => o.MapFrom(src => src.Response.ResponseStatus.Code.Equals("1") ? 1 : 0))
		.ForMember(x => x.TrackingNumber, o => o.MapFrom(src => src.ShipmentResults.PackageResults[0].TrackingNumber))
		.ForMember(x => x.Base64LabelImage, o => o.MapFrom(src => src.ShipmentResults.PackageResults[0].ShippingLabel.GraphicImage))
		.ForMember(x => x.PostagePrice, o => o.MapFrom(src => new PostageRatePrice() {
		    TotalAmount = Decimal.Parse(src.ShipmentResults.ShipmentCharges.TotalCharges.MonetaryValue),
		    MailClass = "UPSMIEXPEDITED"
		}));
	}

	public UPSWS.Ship.ShipmentRequest ToShipmentRequest(PostageLabelInputData data, string shipperNumber, string shipperName, string costCenter) {
	    UPSWS.Ship.ShipmentRequest request = AutoMapper.Mapper.Map<PostageLabelInputData, UPSWS.Ship.ShipmentRequest>(data);
	    request.Shipment.Shipper.ShipperNumber = shipperNumber;
	    request.Shipment.Shipper.Name = shipperName;
	    for (int i = 0; i < request.Shipment.PaymentInformation.ShipmentCharge.Length; i++) {
		request.Shipment.PaymentInformation.ShipmentCharge[i].BillShipper.AccountNumber = shipperNumber;
	    }
	    request.Shipment.CostCenter = costCenter;
	    return request;
	}

	public PostageLabelData ToPostageLabelData(UPSWS.Ship.ShipmentResponse response) {
	    PostageLabelData labelData = AutoMapper.Mapper.Map<UPSWS.Ship.ShipmentResponse, PostageLabelData>(response);
	    return labelData;
	}

	public PostageLabelData ToPostageLabelErrorData(string error) {
	    return new PostageLabelData() {
		ErrorMessage = error,
		Status = 0
	    };
	}

	public UPSWS.Void.VoidShipmentRequest ToVoidShipmentRequest(string trackingNumber, bool isInternational) {
	    return new UPSWS.Void.VoidShipmentRequest() {
		VoidShipment = new UPSWS.Void.VoidShipmentRequestVoidShipment() {
		    ShipmentIdentificationNumber = trackingNumber
		}
	    };
	}

	public RefundRequestData ToRefundRequestData(UPSWS.Void.VoidShipmentResponse data) {
	    return new RefundRequestData() {
		IsApproved = data.Response.ResponseStatus.Code == "1",
		ErrorMsg = data.Response.ResponseStatus.Description,
	    };
	}

	public static Dictionary<string, string> mailClassToServiceCode = new Dictionary<string, string>() {
	    { MailClassEnum.UPSNEXTDAY.ToString(), "01" },
	    { MailClassEnum.UPSSECONDDAY.ToString(), "02" },
	    { MailClassEnum.UPSGROUND.ToString(), "03" },
	    { MailClassEnum.FIRST.ToString(), "M2" },
	    { MailClassEnum.UPSMIEXPEDITED.ToString(), "M4" }
	};
	
	public static Dictionary<string, string> mailpieceShapeToPackageTypeCode = new Dictionary<string, string>() {
	    { MailpieceShapeEnum.LETTER.ToString(), "01" },
	    { MailpieceShapeEnum.PACKAGE.ToString(), "02" },
	    { MailpieceShapeEnum.PARCEL.ToString(), "57" },
	    { MailpieceShapeEnum.THICKENVELOPE.ToString(), "59" },
	    { MailpieceShapeEnum.IRREGULARPARCEL.ToString(), "62" }
	};

	public static Dictionary<string, string> mailClassToWeightUnitOfMeasure = new Dictionary<string, string>() {
	    { MailClassEnum.UPSNEXTDAY.ToString(), "LBS" },
	    { MailClassEnum.UPSSECONDDAY.ToString(), "LBS" },
	    { MailClassEnum.UPSGROUND.ToString(), "LBS" },
	    { MailClassEnum.FIRST.ToString(), "OZS" },
	    { MailClassEnum.UPSMIEXPEDITED.ToString(), "OZS" }
	};
    }
}
