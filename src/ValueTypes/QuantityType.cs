using System;


namespace Spring2.Core.Types {

    public struct QuantityType : IComparable, IDataType {
	DecimalType myValue;

	public static readonly QuantityType DEFAULT = new QuantityType(TypeState.DEFAULT);
	public static readonly QuantityType UNSET   = new QuantityType(TypeState.UNSET);

	#region State management
	public bool IsValid {
	    get {return myValue.IsValid;}
	}

	public bool IsDefault {
	    get {return myValue.IsDefault;}
	}

	public bool IsUnset {
	    get {return myValue.IsUnset;}
	}

	#endregion

	#region Constructors
	private QuantityType(TypeState state) {
	    if (state == TypeState.DEFAULT) {
		myValue = DecimalType.DEFAULT;
	    } else if (state == TypeState.UNSET) {
		myValue = DecimalType.UNSET;
	    } else {
		myValue = new DecimalType(0);
	    }
	}

	public QuantityType(IntegerType value) {
	    myValue = (DecimalType) value;
	}

	public QuantityType(Int32 value) {
	    myValue = value;
	}

	public QuantityType(Decimal value) {
	    myValue = (DecimalType) value;
	}

	public QuantityType(Double value) {
	    myValue = (DecimalType) value;
	}
	#endregion

	#region Parse and ToString()
	public static QuantityType Parse(String value) {
	    if (value == null) {
		return UNSET;
	    }

	    return new QuantityType(Decimal.Parse(value));
	}

	//what should we do for ToString here?
	public override string ToString() {
	    return myValue.ToString();
	}

	public string Display() {
	    return myValue.ToString();
	}
	#endregion

	#region Equality operators and methods
	public static int Compare(QuantityType leftHand, QuantityType rightHand) {
	    return DecimalType.Compare(leftHand.myValue, rightHand.myValue);
	}

	public static bool operator ==(QuantityType leftHand, QuantityType  rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator !=(QuantityType leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
	}

	public static bool operator <(QuantityType  leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
	}

	public static bool operator <=(QuantityType leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}

	public static bool operator >(QuantityType leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
	}

	public static bool operator >=(QuantityType leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) >= 0;
	}
	#endregion

	#region Object Support, IComparable and other stuff
	int IComparable.CompareTo(Object value) {
	    if (!(value is QuantityType)) {
		throw new InvalidTypeException("QuantityType");
	    }

	    if (value == null) {
		throw new InvalidArgumentException("value");
	    }

	    QuantityType compareTo = (QuantityType) value;

	    return Compare(this, compareTo);
	}

	public int CompareTo(QuantityType value) {
	    return Compare(this, value);
	}
    	
	public override bool Equals(Object value) {
	    if (value is QuantityType) {
		return Compare(this, (QuantityType) value) == 0;
	    }

	    return false;
	}

	public static bool Equals(QuantityType leftHand, QuantityType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}
	public override int GetHashCode() {
	    return myValue.GetHashCode();
	}
    
	public TypeCode GetTypeCode() {
	    return TypeCode.Decimal;
	}
	#endregion
    }
}
