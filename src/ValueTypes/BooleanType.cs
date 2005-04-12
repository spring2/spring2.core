using System;
using System.Collections;

namespace Spring2.Core.Types {
    [Serializable()] 
    public struct BooleanType : IComparable, IDataType {
	
	private Boolean myValue;
	private TypeState myState;

	private const bool True = true;
	private const bool False = false;

	public static string TrueString = Boolean.TrueString;
	public static string FalseString = Boolean.FalseString;

	public static readonly BooleanType TRUE = new BooleanType(true);
	public static readonly BooleanType FALSE = new BooleanType(false);

	public static readonly BooleanType DEFAULT = new BooleanType(TypeState.DEFAULT);
	public static readonly BooleanType UNSET   = new BooleanType(TypeState.UNSET);

	#region State management
	public bool IsValid {
	    get {return myState == TypeState.VALID;}
	}

	public void SetValid() {
	    myState = TypeState.VALID;
	}

	public bool IsDefault {
	    get {return myState == TypeState.DEFAULT;}
	}

	public void SetDefault() {
	    myState = TypeState.DEFAULT;
	}

	public bool IsUnset {
	    get {return myState == TypeState.UNSET;}
	}

	public void SetUnset() {
	    myState = TypeState.UNSET;
	}

	public TypeState State {
	    get {return myState;}
	    set {myState = value;}
	}
	#endregion

	#region Constructors
	private BooleanType(TypeState state) {
	    myValue = false;
	    myState = state;
	}

	private BooleanType(bool value) {
	    myValue = value;
	    myState = TypeState.VALID;
	}
	#endregion

	#region ToString and Parsing

	public override String ToString() {
	    if (myState != TypeState.VALID) {
		throw new InvalidValueException(myState);
	    }

	    return myValue.ToString();
	    //	    if (myValue) {
	    //		return TrueString;
	    //	    }

	    //	    return FalseString;
	}

	//	String IConvertible.ToString(IFormatProvider provider) {
	//	    return myValue.ToString(provider);
	//	}

	public static BooleanType Parse(String value) {
	    bool boolVal = bool.Parse(value);
	    return new BooleanType(boolVal);
	}

	#endregion

	#region IConvertible and other conversions
	
	int IComparable.CompareTo(Object value) {
	    if (!(value is BooleanType)) {
		throw new InvalidTypeException("BooleanType");
	    }

	    if (value == null) {
		throw new InvalidArgumentException("value");
	    }

	    BooleanType compareTo = (BooleanType) value;

	    if (IsValid && compareTo.IsValid) {
		if (myValue == compareTo.myValue) {
		    return 0;
		} else if (myValue == false) {
		    return -1;
		}

		return 1;
	    }

	    if (myState == TypeState.UNSET) {
		if (compareTo.myState == TypeState.DEFAULT || compareTo.myState == TypeState.VALID) {
		    return -1;
		}

		if (compareTo.myState == TypeState.UNSET) {
		    return 0;
		}
	    }

	    if (compareTo.myState == TypeState.DEFAULT) {
		if (myState == TypeState.DEFAULT) {
		    return 0;
		}

		if (myState == TypeState.UNSET) {
		    return 1;
		}

		return -1;
	    }

	    //should this throw an exception?
	    return 0;
	}

	
	public bool ToBoolean(IFormatProvider provider) {
	    if (myState != TypeState.VALID) {
		throw new InvalidValueException(myState);
	    }

	    return myValue;
	}
	#endregion

	#region Object support methods
	//what to do here?? we aren't really a string
	public TypeCode GetTypeCode() {
	    return TypeCode.Boolean;
	}

	//should this worry about validity?
	public override int GetHashCode() {
	    if (myValue) {
		return 1;
	    }

	    return 0;
	}
	#endregion

	public static BooleanType GetInstance(Boolean value) {
	    return value ? TRUE : FALSE;
	}

	public static BooleanType GetInstance(Int32 value) {
	    if (value == 1) {
		return TRUE;
	    }
	    if (value == 0) {
		return FALSE;
	    }
	    return UNSET;
	}

	public static BooleanType GetInstance(String value) {
	    if (value == "Y") {
		return TRUE;
	    }
	    if (value == "N") {
		return FALSE;
	    }
	    return UNSET;
	}

	public Boolean ToBoolean() {
	    if (myState == TypeState.VALID) {
		return myValue;
	    } else {
		throw new InvalidCastException("UNSET and DEFAULT cannot be cast to a Boolean.");
	    }
	}

	public Boolean IsTrue {
	    get {
		return this.Equals(TRUE);
	    }
	}

	public Boolean IsFalse {
	    get {
		return this.Equals(FALSE);
	    }
	}

    }
}