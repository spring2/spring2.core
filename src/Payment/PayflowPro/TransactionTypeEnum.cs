using System;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.PayflowPro {
    public class TransactionTypeEnum : EnumDataType {
	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	new public static readonly TransactionTypeEnum DEFAULT = new TransactionTypeEnum();
	new public static readonly TransactionTypeEnum UNSET = new TransactionTypeEnum();

	public static readonly TransactionTypeEnum SALE = new TransactionTypeEnum("S", "Sale");
	public static readonly TransactionTypeEnum CREDIT = new TransactionTypeEnum("C", "Credit");
	public static readonly TransactionTypeEnum AUTHORIZATION = new TransactionTypeEnum("A", "Authorization");
	public static readonly TransactionTypeEnum DELAYED_CAPTURE = new TransactionTypeEnum("D", "Delayed Capture");
	public static readonly TransactionTypeEnum VOID = new TransactionTypeEnum("V", "Void");
	public static readonly TransactionTypeEnum VOICE_AUTHORIZATION = new TransactionTypeEnum("F", "Voice Authorization");
	public static readonly TransactionTypeEnum INQUIRY = new TransactionTypeEnum("I", "Inquiry");

	public static TransactionTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (TransactionTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TransactionTypeEnum() {
	}

	private TransactionTypeEnum(String code, String name) {
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