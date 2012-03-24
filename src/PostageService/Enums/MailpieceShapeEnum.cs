using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService.Enums {
    public enum MailpieceShapeEnum {
	Card,
	Letter,
	Flat,
	Parcel,

	LargeParcel,
	IrregularParcel,
	OversizedParcel,

	FlatRateEnvelope,
	FlatRateLegalEnvelope,
	FlatRatePaddedEnvelope,
	FlatRateGiftCardEnvelope,
	FlatRateWindowEnvelope,
	FlatRateCardboardEnvelope,
	SmallFlatRateEnvelope,

	SmallFlatRateBox,
	MediumFlatRateBox,
	LargeFlatRateBox,
	DVDFlatRateBox,
	LargeVideoFlatRateBox,

	RegionalRateBoxA,
	RegionalRateBoxB,
	RegionalRateBoxC
    }
}
