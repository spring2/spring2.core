using System;
using System.Collections;

namespace Spring2.Core.Types {

    public enum GenderCode {
	Male,
	Female
    }

    public struct GenderType : IComparable, IDataType {
	private GenderCode myValue;
	private TypeState  myState;

	public static readonly GenderType DEFAULT = new GenderType(TypeState.DEFAULT);
	public static readonly GenderType UNSET = new GenderType(TypeState.UNSET);

	public static readonly GenderType MALE = new GenderType(GenderCode.Male);
	public static readonly GenderType FEMALE = new GenderType(GenderCode.Female);

	public static readonly string MaleString   = "Male";
	public static readonly string FemaleString = "Female";

	#region	State management
	public bool IsValid {
	    get	{return myState	== TypeState.VALID;}
	}

	public void SetValid() {
	    myState = TypeState.VALID;
	}

	public bool IsDefault {
	    get	{return myState	== TypeState.DEFAULT;}
	}

	public void SetDefault() {
	    myState = TypeState.DEFAULT;
	}

	public bool IsUnset {
	    get	{return myState	== TypeState.UNSET;}
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
	private GenderType(TypeState state) {
	    myState = state;
	    myValue = GenderCode.Male;
	}

	private GenderType(GenderCode code) {
	    myValue = code;
	    myState = TypeState.VALID;
	}
	#endregion

	#region Accessors
	public GenderCode Code {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue;
	    }
	    set {
		myValue = value;
		myState = TypeState.VALID;
	    }
	}

	public string Name {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}

		if (myValue == GenderCode.Male) {
		    return MaleString;
		}

		return FemaleString;
	    }
	}

	public bool IsMale {
	    get {
		if (!IsValid) {
		    return false;
		}
		return myValue == GenderCode.Male;
	    }
	}

	public bool IsFemale {
	    get {
		if (!IsValid) {
		    return false;
		}
		return myValue == GenderCode.Female;
	    }
	}
	#endregion

	#region Parse and ToString methods
	public static GenderType Parse(string valueToParse) {
	    if (valueToParse != "M" && valueToParse != "F") {
		throw new InvalidValueException(string.Format("GenderType.Parse() value must be 'M' or 'F', not '{0}'", valueToParse));
	    }

	    if (valueToParse == "M") {
		return new GenderType(GenderCode.Male);
	    }
	    
	    return new GenderType(GenderCode.Female);	    
	}

	public string ToString(bool returnName) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    if (myValue == GenderCode.Male) {
		if (returnName) {
		    return MaleString;
		}
		return "M";
	    }

	    if (returnName) {
		return FemaleString;
	    }
	    return "F";
	}

	public override string ToString() {
	    return ToString(true);
	}
	#endregion

	#region Equality operators and methods
	public static int Compare(GenderType leftHand, GenderType rightHand) {
	    if (leftHand.myState == TypeState.VALID && rightHand.myState == TypeState.VALID) {
		if (leftHand.myValue == GenderCode.Male && rightHand.myValue == GenderCode.Female) {
		    return -1;
		}

		if (leftHand.myValue == rightHand.myValue) {
		    return 0;
		}

		if (leftHand.myValue == GenderCode.Female && rightHand.myValue == GenderCode.Male) {
		    return -1;
		}
	    }

	    if (leftHand.myState == TypeState.UNSET) {
		if (rightHand.myState == TypeState.DEFAULT || rightHand.myState == TypeState.VALID) {
		    return -1;
		}

		if (rightHand.myState == TypeState.UNSET) {
		    return 0;
		}
	    }

	    if (rightHand.myState == TypeState.DEFAULT) {
		if (leftHand.myState == TypeState.DEFAULT) {
		    return 0;
		}

		if (leftHand.myState == TypeState.UNSET) {
		    return 1;
		}

		return -1;
	    }

	    //should this throw an exception?
	    return 0;
	}

	public static bool operator ==(GenderType leftHand, GenderType  rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator !=(GenderType leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
	}

	public static bool operator <(GenderType  leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
	}

	public static bool operator <=(GenderType leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}

	public static bool operator >(GenderType leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
	}

	public static bool operator >=(GenderType leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) >= 0;
	}
	#endregion

	#region Object Support, IComparable and other stuff
	int IComparable.CompareTo(Object value) {
	    if (!(value is GenderType)) {
		throw new InvalidTypeException("GenderType");
	    }

	    if (value == null) {
		throw new InvalidArgumentException("value");
	    }

	    GenderType compareTo = (GenderType) value;

	    return Compare(this, compareTo);
	}

	public int CompareTo(GenderType value) {
	    return Compare(this, value);
	}
    	
	public override bool Equals(Object value) {
	    if (value is GenderType) {
		return Compare(this, (GenderType) value) == 0;
	    }

	    return false;
	}

	public static bool Equals(GenderType leftHand, GenderType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}
	public override int GetHashCode() {
	    return myValue.GetHashCode();
	}
    
	public TypeCode GetTypeCode() {
	    //i have NO idea what this should be
	    return TypeCode.Decimal;
	}

	#endregion
    }
}
