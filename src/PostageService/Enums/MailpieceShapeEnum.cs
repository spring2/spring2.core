using System;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class MailpieceShapeEnum : EnumDataType  {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly MailpieceShapeEnum DEFAULT = new MailpieceShapeEnum();
	new public static readonly MailpieceShapeEnum UNSET = new MailpieceShapeEnum();

	public static readonly MailpieceShapeEnum CARD = new MailpieceShapeEnum("Card", "Card");
	public static readonly MailpieceShapeEnum LETTER = new MailpieceShapeEnum("Letter", "Letter");
	public static readonly MailpieceShapeEnum FLAT = new MailpieceShapeEnum("Flat", "Flat");
	public static readonly MailpieceShapeEnum PARCEL = new MailpieceShapeEnum("Parcel", "Parcel");
        public static readonly MailpieceShapeEnum PACKAGE = new MailpieceShapeEnum("Package", "Package");
        public static readonly MailpieceShapeEnum LARGEPARCEL = new MailpieceShapeEnum("LargeParcel", "Large Parcel");
	public static readonly MailpieceShapeEnum IRREGULARPARCEL = new MailpieceShapeEnum("IrregularParcel", "Irregular Parcel");
	public static readonly MailpieceShapeEnum OVERSIZEDPARCEL = new MailpieceShapeEnum("OversizedParcel", "Oversized Parcel");
	public static readonly MailpieceShapeEnum FLATRATEENVELOPE = new MailpieceShapeEnum("FlatRateEnvelope", "Flat Rate Envelope");
	public static readonly MailpieceShapeEnum FLATRATELEGALENVELOPE = new MailpieceShapeEnum("FlatRateLegalEnvelope", "Flat Rate Legal Envelope");
	public static readonly MailpieceShapeEnum FLATRATEPADDEDENVELOPE = new MailpieceShapeEnum("FlatRatePaddedEnvelope", "Flat Rate Padded Envelope");
	public static readonly MailpieceShapeEnum FLATRATEGIFTCARDENVELOPE = new MailpieceShapeEnum("FlatRateGiftCardEnvelope", "Flat Rate Gift Card Envelope");
	public static readonly MailpieceShapeEnum FLATRATEWINDOWENVELOPE = new MailpieceShapeEnum("FlatRateWindowEnvelope", "Flat Rate Window Envelope");
	public static readonly MailpieceShapeEnum FLATRATECARDBOARDENVELOPE = new MailpieceShapeEnum("FlatRateCardboardEnvelope", "Flat Rate Cardboard Envelope");
	public static readonly MailpieceShapeEnum SMALLFLATRATEENVELOPE = new MailpieceShapeEnum("SmallFlatRateEnvelope", "Small Flat Rate Envelope");
	public static readonly MailpieceShapeEnum SMALLFLATRATEBOX = new MailpieceShapeEnum("SmallFlatRateBox", "Small Flat Rate Box");
	public static readonly MailpieceShapeEnum MEDIUMFLATRATEBOX = new MailpieceShapeEnum("MediumFlatRateBox", "Medium Flat Rate Box");
	public static readonly MailpieceShapeEnum LARGEFLATRATEBOX = new MailpieceShapeEnum("LargeFlatRateBox", "Large Flat Rate Box");
	public static readonly MailpieceShapeEnum DVDFLATRATEBOX = new MailpieceShapeEnum("DVDFlatRateBox", "DVD Flat Rate Box");
	public static readonly MailpieceShapeEnum LARGEVIDEOFLATRATEBOX = new MailpieceShapeEnum("LargeVideoFlatRateBox", "Large Video Flat Rate Box");
	public static readonly MailpieceShapeEnum REGIONALRATEBOXA = new MailpieceShapeEnum("RegionalRateBoxA", "Regional Rate Box A");
	public static readonly MailpieceShapeEnum REGIONALRATEBOXB = new MailpieceShapeEnum("RegionalRateBoxB", "Regional Rate Box B");
	public static readonly MailpieceShapeEnum REGIONALRATEBOXC = new MailpieceShapeEnum("RegionalRateBoxC", "Regional Rate Box C");
	public static readonly MailpieceShapeEnum THICKENVELOPE = new MailpieceShapeEnum("ThickEnvelope", "Thick Envelope");


	public static MailpieceShapeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailpieceShapeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailpieceShapeEnum() {
	}

	private MailpieceShapeEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static EnumDataTypeList Options {
	    get { return OPTIONS; }
	}
    }
}
