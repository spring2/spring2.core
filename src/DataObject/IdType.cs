using System;

namespace Spring2.Core.Types {

    public class IdType : DataType	{

	public static readonly new IdType DEFAULT = new IdType();
	public static readonly new IdType UNSET = new IdType();

	private Int32 value;

	private IdType() {}

        public IdType(Int32 value) {
	    this.value = value;
	}

	protected override Object Value {
	    get {
		return value;
	    }
	}

	public override Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset {
	    get {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}
    }
}
