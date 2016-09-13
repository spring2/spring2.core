using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService.UPS {
    internal class UPSModelAssembler {
	static UPSModelAssembler() {
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSMI.Ship.ShipmentRequest>()
		.ForMember(x => x.Request, o => o.UseValue(new UPSMI.Ship.RequestType() {
		    RequestOption = new String[] {"nonvalidate"}
		}))
		.ForMember(x => x.Shipment, o => o.MapFrom(src => AutoMapper.Mapper.Map<PostageLabelInputData, UPSMI.Ship.ShipmentType>(src)))
		.ForMember(x => x.LabelSpecification, o => o.UseValue(new UPSMI.Ship.LabelSpecificationType() {
		    LabelStockSize = new UPSMI.Ship.LabelStockSizeType() {
			Height = "6",
			Width = "4"
		    },
		    LabelImageFormat = new UPSMI.Ship.LabelImageFormatType() {
			Code = "GIF"
		    }
		}));
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSMI.Ship.ShipmentType>()
		.ForMember(x => x.Shipper, o => o.MapFrom(src => AutoMapper.Mapper.Map<PostageLabelInputData, UPSMI.Ship.ShipperType>(src)))
		.ForMember(x => x.PaymentInformation, o => o.MapFrom(src => src.IsInternational ?
		new UPSMI.Ship.PaymentInfoType() {
		    ShipmentCharge = new UPSMI.Ship.ShipmentChargeType[] {
			new UPSMI.Ship.ShipmentChargeType() {
			    BillShipper = new UPSMI.Ship.BillShipperType(),
			    Type = "01"
			},
			new UPSMI.Ship.ShipmentChargeType() {
			    BillShipper = new UPSMI.Ship.BillShipperType(),
			    Type = "02"
			}
		    }
		} :
		new UPSMI.Ship.PaymentInfoType() {
		    ShipmentCharge = new UPSMI.Ship.ShipmentChargeType[] {
			new UPSMI.Ship.ShipmentChargeType() {
			    BillShipper = new UPSMI.Ship.BillShipperType(),
			    Type = "01"
			}
		    }
		}))
		.ForMember(x => x.ShipFrom, o => o.MapFrom(src => new UPSMI.Ship.ShipFromType() {
		    Address = new UPSMI.Ship.ShipAddressType() {
			AddressLine = new String[] { src.ReturnAddress1, src.ReturnAddress2 },
			City = src.FromCity,
			PostalCode = src.FromPostalCode,
			StateProvinceCode = src.FromState,
			CountryCode = src.FromCountry
		    },
		    AttentionName = src.FromName,
		    Name = src.FromCompany
		}))
		.ForMember(x => x.ShipTo, o => o.MapFrom(src => new UPSMI.Ship.ShipToType() {
		    Address = new UPSMI.Ship.ShipToAddressType() {
			AddressLine = new String[] { src.ToAddress1, src.ToAddress2 },
			City = src.ToCity,
			PostalCode = src.ToPostalCode,
			StateProvinceCode = src.ToState,
			CountryCode = src.ToCountryCode
		    },
		    AttentionName = src.ToName,
		    Name = src.ToCompany,
		    Phone = new UPSMI.Ship.ShipPhoneType() {
			Number = src.ToPhone
		    }
		}))
		.ForMember(x => x.Service, o => o.UseValue(new UPSMI.Ship.ServiceType() {
		    Code = "2"
		}))
		.ForMember(x => x.USPSEndorsement, o => o.UseValue("1"))
		.ForMember(x => x.PackageID, o => o.UseValue(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30)))
		.ForMember(x => x.ShipmentServiceOptions, o => o.UseValue(new UPSMI.Ship.ShipmentTypeShipmentServiceOptions()))
		.ForMember(x => x.Package, o => o.MapFrom(src => new UPSMI.Ship.PackageType[] {
		    new UPSMI.Ship.PackageType() {
			PackageWeight = new UPSMI.Ship.PackageWeightType() {
			    Weight = src.WeightOz.ToString(),
			    UnitOfMeasurement = new UPSMI.Ship.ShipUnitOfMeasurementType() {
				Code = "OZS"
			    }
			},
			Packaging = new UPSMI.Ship.PackagingType() {
			    Code = "59"
			}
		    }
		}));
	    AutoMapper.Mapper.CreateMap<PostageLabelInputData, UPSMI.Ship.ShipperType>()
		.ForMember(x => x.Address, o => o.MapFrom(src => new UPSMI.Ship.ShipAddressType() {
		    AddressLine = new String[] { src.ReturnAddress1, src.ReturnAddress2 },
		    City = src.FromCity,
		    PostalCode = src.FromPostalCode,
		    StateProvinceCode = src.FromState,
		    CountryCode = src.FromCountry
		}));
	    AutoMapper.Mapper.CreateMap<UPSMI.Ship.ShipmentResponse, PostageLabelData>();
	}

	public UPSMI.Ship.ShipmentRequest ToShipmentRequest(PostageLabelInputData data, string shipperNumber, string costCenter) {
	    UPSMI.Ship.ShipmentRequest request = AutoMapper.Mapper.Map<PostageLabelInputData, UPSMI.Ship.ShipmentRequest>(data);
	    request.Shipment.Shipper.ShipperNumber = shipperNumber;
	    for (int i = 0; i < request.Shipment.PaymentInformation.ShipmentCharge.Length; i++) {
		request.Shipment.PaymentInformation.ShipmentCharge[i].BillShipper.AccountNumber = shipperNumber;
	    }
	    request.Shipment.CostCenter = costCenter;
	    return request;
	}

	public PostageLabelData ToPostageLabelData(UPSMI.Ship.ShipmentResponse response) {
	    PostageLabelData labelData = AutoMapper.Mapper.Map<UPSMI.Ship.ShipmentResponse, PostageLabelData>(response);

	    return labelData;
	}
    }
}
