using System;
using System.Collections;

namespace Spring2.Core.Types {
    public abstract class EnumDataType : DataType {

	protected static readonly IList OPTIONS = new ArrayList();

	protected String code;
	protected String name;
	
	protected override Object Value {
	    get { return code; }
	}
	
	public static IList Options {
	    get { return OPTIONS; }
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

	public override Boolean Equals(Object o) {
	    if (this == o) {
		return true;
	    } else if (code == null || !(o is StringType)) {
		return false;
	    } else {
		return code.Equals(((EnumDataType)o).code);
	    }
	}

	public override int GetHashCode() {
	    return code == null ? 0 : code.GetHashCode();
	}
    }
}
