using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService {
    public abstract class CommonLabelRequest {
	public MailpieceShapeEnum MailpieceShape { set; get; }

	public MailClassEnum? MailClass { set; get; }

	public double WeightOz { set; get; }

	public PackageTypeIndicatorEnum? PackageTypeIndicator { set; get; }

	public PackageDimensions MailpieceDimensions { set; get; }

	public int DateAdvance { set; get; }

	public PricingEnum? Pricing { set; get; }

	public SundayHolidayDeliveryEnum SundayHolidayDelivery { set; get; }

	public string Extension { set; get; }
    }
}
