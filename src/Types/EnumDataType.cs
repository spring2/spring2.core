using System;

namespace Spring2.Core.Types {
    public abstract class EnumDataType : DataType {

	protected String code;
	protected String name;
		
	protected override Object Value {
	    get { return code; }
	}
		
	public override String ToString() {
	    if (IsDefault) {
		return DEFAULT;
	    } else if (IsUnset) {
		return UNSET;
	    } else {
		return name;
	    }
	}

    }
}
