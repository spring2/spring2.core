using System;
using Spring2.Core.Types;

namespace Spring2.Dss.Tax {
    public class TaxSoapException : TaxException {
	private StringType ex = StringType.DEFAULT;

	public TaxSoapException(StringType ex) : base(String.Format("Tax Soap Exception: '{0}' ", ex)) {
	    this.ex = this.ex;
	}

	public StringType Ex {
	    get { return this.ex; }
	}
    }
}