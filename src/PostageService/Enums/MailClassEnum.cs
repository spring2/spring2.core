using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class MailClassEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly MailClassEnum DEFAULT = new MailClassEnum();
	new public static readonly MailClassEnum UNSET = new MailClassEnum();

	[Obsolete("USPS name change to 'PriorityExpress' Jan 2015")]
	public static readonly MailClassEnum EXPRESS = new MailClassEnum("Express", "Express");
	public static readonly MailClassEnum PRIORITYEXPRESS = new MailClassEnum("PriorityExpress", "Priority Express");
	public static readonly MailClassEnum FIRST = new MailClassEnum("First", "First");
	public static readonly MailClassEnum LIBRARYMAIL = new MailClassEnum("LibraryMail", "Library Mail");
	public static readonly MailClassEnum MEDIAMAIL = new MailClassEnum("MediaMail", "Media Mail");
	public static readonly MailClassEnum STANDARDPOST = new MailClassEnum("StandardPost", "Standard Post");
	public static readonly MailClassEnum PARCELSELECT = new MailClassEnum("ParcelSelect", "Parcel Select");
	public static readonly MailClassEnum PARCELPOST = new MailClassEnum("ParcelPost", "Parcel Post");
	public static readonly MailClassEnum PRIORITY = new MailClassEnum("Priority", "Priority");
	public static readonly MailClassEnum CRITICALMAIL = new MailClassEnum("CriticalMail", "Critical Mail");
	public static readonly MailClassEnum STANDARDMAIL = new MailClassEnum("StandardMail", "Standard Mail");
	[Obsolete("USPS name change to 'PriorityMailExpressInternational' Jan 2015")]
	public static readonly MailClassEnum EXPRESSMAILINTERNATIONAL = new MailClassEnum("ExpressMailInternational", "Express Mail International");
	public static readonly MailClassEnum PRIORITYMAILEXPRESSINTERNATIONAL = new MailClassEnum("PriorityMailExpressInternational", "Priority Mail Express International");
	public static readonly MailClassEnum FIRSTCLASSMAILINTERNATIONAL = new MailClassEnum("FirstClassMailInternational", "First Class Mail International");
	public static readonly MailClassEnum PRIORITYMAILINTERNATIONAL = new MailClassEnum("PriorityMailInternational", "Priority Mail International");
	public static readonly MailClassEnum DOMESTIC = new MailClassEnum("Domestic", "Domestic");
	public static readonly MailClassEnum INTERNATIONAL = new MailClassEnum("International", "International");

	public static readonly MailClassEnum DHLGMSMPARCELPLUSEXPEDITED = new MailClassEnum("DHLGMSMParcelPlusExpedited", "DHLGMSM Parcel Plus Expedited");
	public static readonly MailClassEnum DHLGMSMPARCELPLUSSTANDARD = new MailClassEnum("DHLGMSMParcelPlusStandard", "DHLGMSM Parcel Plus Standard");
	public static readonly MailClassEnum DHLGMSMBPMEXPEDITED = new MailClassEnum("DHLGMSMBPMExpedited", "DHLGMSM BPM Expedited");
	public static readonly MailClassEnum DHLGMSMBPMGROUND = new MailClassEnum("DHLGMSMBPMGround", "DHLGMSM BPM Ground");
	public static readonly MailClassEnum DHLGMSMCATALOGBPMEXPEDITED = new MailClassEnum("DHLGMSMCatalogBPMExpedited", "DHLGMSM Catalog BPM Expedited");
	public static readonly MailClassEnum DHLGMSMCATALOGBPMGROUND = new MailClassEnum("DHLGMSMCatalogBPMGround", "DHLGMSM Catalog BPM Ground");
	public static readonly MailClassEnum DHLGMSMMEDIAMAILGROUND = new MailClassEnum("DHLGMSMMediaMailGround", "DHLGMSM Media Mail Ground");
	public static readonly MailClassEnum DHLGMSMPARCELSEXPEDITED = new MailClassEnum("DHLGMSMParcelsExpedited", "DHLGMSM Parcels Expedited");
	public static readonly MailClassEnum DHLGMSMPARCELSGROUND = new MailClassEnum("DHLGMSMParcelsGround", "DHLGMSM Parcels Ground");
	public static readonly MailClassEnum DHLGMSMPRIORITYMAIL = new MailClassEnum("DHLGMSMPriorityMail", "DHLGMSM Priority Mail");
	public static readonly MailClassEnum DHLGMSMFIRSTCLASSPRODUCT = new MailClassEnum("DHLGMSMFirstClassProduct", "DHLGMSM First Class Product");
	public static readonly MailClassEnum DHLGMSMFIRSTCLASSPARCELS = new MailClassEnum("DHLGMSMFirstClassParcels", "DHLGMSM First Class Parcels");

	public static readonly MailClassEnum IPA = new MailClassEnum("IPA", "International");
	public static readonly MailClassEnum ISAL = new MailClassEnum("ISAL", "International");


	public static MailClassEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (MailClassEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private MailClassEnum() {
	}

	private MailClassEnum(String code, String name) {
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
