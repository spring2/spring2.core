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
