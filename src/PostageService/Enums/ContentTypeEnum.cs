using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.PostageService.Enums {
    public class ContentTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly ContentTypeEnum DEFAULT = new ContentTypeEnum();
	new public static readonly ContentTypeEnum UNSET = new ContentTypeEnum();

	public static readonly ContentTypeEnum DOCUMENTS = new ContentTypeEnum("Documents", "Documents");
	public static readonly ContentTypeEnum GIFT = new ContentTypeEnum("Gift", "Gift");
	public static readonly ContentTypeEnum MERCHANDISE = new ContentTypeEnum("Merchandise", "Merchandise");
	public static readonly ContentTypeEnum OTHER = new ContentTypeEnum("Other", "Other");
	public static readonly ContentTypeEnum RETURNEDGOODS = new ContentTypeEnum("ReturnedGoods", "ReturnedGoods");
	public static readonly ContentTypeEnum SAMPLE = new ContentTypeEnum("Sample", "Sample");

	public static ContentTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (ContentTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private ContentTypeEnum() {
	}

	private ContentTypeEnum(String code, String name) {
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
