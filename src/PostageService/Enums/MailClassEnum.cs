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

	public static readonly MailClassEnum EXPRESS = new MailClassEnum("Express", "Express");
	public static readonly MailClassEnum FIRST = new MailClassEnum("First", "First");
	public static readonly MailClassEnum LIBRARYMAIL = new MailClassEnum("LibraryMail", "Library Mail");
	public static readonly MailClassEnum MEDIAMAIL = new MailClassEnum("MediaMail", "Media Mail");
	public static readonly MailClassEnum PARCELPOST = new MailClassEnum("ParcelPost", "Parcel Post");
	public static readonly MailClassEnum PARCELSELECT = new MailClassEnum("ParcelSelect", "Parcel Select");
	public static readonly MailClassEnum PRIORITY = new MailClassEnum("Priority", "Priority");
	public static readonly MailClassEnum CRITICALMAIL = new MailClassEnum("CriticalMail", "Critical Mail");
	public static readonly MailClassEnum STANDARDMAIL = new MailClassEnum("StandardMail", "Standard Mail");
	public static readonly MailClassEnum EXPRESSMAILINTERNATIONAL = new MailClassEnum("ExpressMailInternational", "Express Mail International");
	public static readonly MailClassEnum FIRSTCLASSMAILINTERNATIONAL = new MailClassEnum("FirstClassMailInternational", "First Class Mail International");
	public static readonly MailClassEnum PRIORITYMAILINTERNATIONAL = new MailClassEnum("PriorityMailInternational", "Priority Mail International");
	public static readonly MailClassEnum DOMESTIC = new MailClassEnum("Domestic", "Domestic");
	public static readonly MailClassEnum INTERNATIONAL = new MailClassEnum("International", "International");

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
