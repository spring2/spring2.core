using System;

namespace Spring2.Core.Types {
    /// <summary>
    /// RowVersion type for the Timestamp values in SQL Server.
    /// </summary>
    public class RowVersionType : DataType {
	public static readonly new RowVersionType DEFAULT = new RowVersionType();
	public static readonly new RowVersionType UNSET = new RowVersionType();
	
	private byte[] value = null;

	private RowVersionType() {}

	public static RowVersionType NewInstance(byte[] value) {
	    return value == null ? UNSET : new RowVersionType(value);
	}

	private RowVersionType(byte[] value) {
	    this.value = value;
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

	protected override object Value {
	    get {
		return value;
	    }
	}
    }
}

