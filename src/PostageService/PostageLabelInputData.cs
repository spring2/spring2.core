using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    //LabelRequest
    public class PostageLabelInputData : CommonLabelRequest {

	public PostageLabelInputData() : base() { }

	public string RequesterID { set; get; }

	public string AccountID { set; get; }

	public string PassPhrase { set; get; }

	public string AutomationRate { set; get; }

	public string Machinable { set; get; }

	public string ServiceLevel { set; get; }

	public string SortType { set; get; }

	public string IncludePostage { set; get; }

	public string ReplyPostage { set; get; }

	public string ShowReturnAddress { set; get; }

	public string Stealth { set; get; }

	public string ValidateAddress { set; get; }

	public string SignatureWaiver { set; get; }

	public string NoWeekendDelivery { set; get; }

	public Services Services { set; get; }

	public int CostCenter { set; get; }

	public float Value { set; get; }

	public double CODAmount { set; get; }

	public double RegisteredMailValue { set; get; }

	public string Description { set; get; }

	public string IntegratedFormType { set; get; }

	public string CustomsFormType { set; get; }

	public string CustomsFormImageFormat { set; get; }

	public string CustomsFormImageResolution { set; get; }

	public string OriginCountry { set; get; }

	public string ContentsType { set; get; }

	public string ContentsExplanation { set; get; }

	public string NonDeliveryOption { set; get; }

	public string ReferenceID { set; get; }

	public string ReferenceID2 { set; get; }

	public string ReferenceID3 { set; get; }

	public string ReferenceID4 { set; get; }

	public string PartnerCustomerID { set; get; }

	public string PartnerTransactionID { set; get; }

	public string BpodClientDunsNumber { set; get; }

	public string RubberStamp1 { set; get; }

	public string RubberStamp2 { set; get; }

	public string RubberStamp3 { set; get; }

	public string EntryFacility { set; get; }

	public string POZipCode { set; get; }

	public string ShipDate { set; get; }

	public string ShipTime { set; get; }

	public CustomsInput CustomsInfo { set; get; }

	public string CustomsCertify { set; get; }

	public string CustomsSigner { set; get; }

	public string HfpEmailAddress { set; get; }

	public string HfpSMS { set; get; }

	public string HfpFacilityID { set; get; }

	public string MRSPermitNo { set; get; }

	public string MRSPermitCityStateZIP { set; get; }

	public string MRSPermitFirm { set; get; }

	public string MRSPermitStreet { set; get; }

	public string MRSRMANumber { set; get; }

	public string OpenAndDistributeFacilityType { set; get; }

	public string OpenAndDistributeFacilityName { set; get; }

	public string OpenAndDistributeTray { set; get; }

	public string OpenAndDistributeMailClassEnclosed { set; get; }

	public string OpenAndDistributeMailClassOther { set; get; }

	public string GXGFedexTrackingNumber { set; get; }

	public string GXGUSPSTrackingNumber{ set; get; }

	public string PrintConsolidatorLabel { set; get; }

	public ReturnOptions ResponseOptions { set; get; }

	public string Token { set; get; }

	public string CustomsSendersCopy { set; get; }

	public string NoDate { set; get; }

	public string MerchantID { set; get; }

	public string PrintScanBasedPaymentLabel { set; get; }

	public string SpecialContents { set; get; }

	public string EmailMiscNotes { set; get; }

	public string InsuredValue { set; get; }

	public string FromName { set; get; }

	public string FromCompany { set; get; }

	public string ReturnAddress1 { set; get; }

	public string ReturnAddress2 { set; get; }

	public string ReturnAddress3 { set; get; }

	public string ReturnAddress4 { set; get; }

	public string FromCity { set; get; }

	public string FromState { set; get; }

	public string FromPostalCode { set; get; }

	public string FromZIP4 { set; get; }

	public string FromCountry { set; get; }

	public string FromPhone { set; get; }

	public string FromEMail { set; get; }

	public string ToName { set; get; }

	public string ToCompany { set; get; }

	public string ToAddress1 { set; get; }

	public string ToAddress2 { set; get; }

	public string ToAddress3 { set; get; }

	public string ToAddress4 { set; get; }

	public string ToCity { set; get; }

	public string ToState { set; get; }

	public string ToPostalCode { set; get; }

	public string ToZIP4 { set; get; }

	public string ToDeliveryPoint { set; get; }

	public string ToCountry { set; get; }

	public string ToCountryCode { set; get; }

	public string ToPhone { set; get; }

	public string ToEMail { set; get; }

	public string CustomsCountry1 { set; get; }

	public string CustomsDescription1 { set; get; }

	public uint CustomsQuantity1 { set; get; }

	public bool CustomsQuantity1Specified { set; get; }

	public float CustomsValue1 { set; get; }

	public bool CustomsValue1Specified { set; get; }

	public uint CustomsWeight1 { set; get; }

	public bool CustomsWeight1Specified { set; get; }

	public string CustomsCountry2 { set; get; }

	public string CustomsDescription2 { set; get; }

	public uint CustomsQuantity2 { set; get; }

	public bool CustomsQuantity2Specified { set; get; }

	public float CustomsValue2 { set; get; }

	public bool CustomsValue2Specified { set; get; }

	public uint CustomsWeight2 { set; get; }

	public bool CustomsWeight2Specified { set; get; }

	public string CustomsCountry3 { set; get; }

	public string CustomsDescription3 { set; get; }

	public uint CustomsQuantity3 { set; get; }

	public bool CustomsQuantity3Specified { set; get; }

	public float CustomsValue3 { set; get; }

	public bool CustomsValue3Specified { set; get; }

	public uint CustomsWeight3 { set; get; }

	public bool CustomsWeight3Specified { set; get; }

	public string CustomsCountry4 { set; get; }

	public string CustomsDescription4 { set; get; }

	public uint CustomsQuantity4 { set; get; }

	public bool CustomsQuantity4Specified { set; get; }

	public float CustomsValue4 { set; get; }

	public bool CustomsValue4Specified { set; get; }

	public uint CustomsWeight4 { set; get; }

	public bool CustomsWeight4Specified { set; get; }

	public string CustomsCountry5 { set; get; }

	public string CustomsDescription5 { set; get; }

	public uint CustomsQuantity5 { set; get; }

	public bool CustomsQuantity5Specified { set; get; }

	public float CustomsValue5 { set; get; }

	public bool CustomsValue5Specified { set; get; }

	public uint CustomsWeight5 { set; get; }

	public bool CustomsWeight5Specified { set; get; }

	public string EelPfc { set; get; }

	public string HfpFacilityName { set; get; }

	public string HfpFacilityAddress1 { set; get; }

	public string HfpFacilityCity { set; get; }

	public string HfpFacilityState { set; get; }

	public string HfpFacilityPostalCode { set; get; }

	public string HfpFacilityZIP4 { set; get; }

	public string CostCenterAlphaNumeric { set; get; }

	public string ToCarrierRoute { set; get; }

	public string Test { set; get; }

	public string LabelType { set; get; }

	public string LabelSubtype { set; get; }

	public string LabelSize { set; get; }

	public string ImageFormat { set; get; }

	public string ImageResolution { set; get; }

	public string ImageRotation { set; get; }
    }
}
