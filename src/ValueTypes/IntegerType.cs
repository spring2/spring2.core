using System;

namespace Spring2.Types {
    [System.Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct IntegerType :	System.IComparable, System.IFormattable {
	private System.Int32 myValue;
	private TypeState    myState;

	public const int MaxValue = System.Int32.MaxValue;
	public const int MinValue = System.Int32.MinValue;

	public static readonly IntegerType DEFAULT = new IntegerType(TypeState.DEFAULT);
	public static readonly IntegerType UNSET   = new IntegerType(TypeState.UNSET);

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
	//should this stay private?
	private IntegerType(IntegerType value) {
	    myValue = value.myValue;
	    myState = value.myState;
	}

	public IntegerType(byte value) {
	    myValue = (int) value;
	    myState = TypeState.VALID;
	}

	public IntegerType(short value) {
	    myValue = value;
	    myState = TypeState.VALID;
	}

	public IntegerType(int value) {
	    myValue = value;
	    myState = TypeState.VALID;
	}
	#endregion

	#region ToString and Parsing
	public override System.String ToString() {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString();
	}
   

	public System.String ToString(System.String format) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString(format);
	}

   
	public System.String ToString(System.String format, System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString(format, formatProvider);
	}

	public static IntegerType Parse(System.String parseString) {    
	    if (parseString == null) {
		throw new InvalidArgumentException("parseString");
	    }

	    IntegerType parsedInt32;

	    parsedInt32.myValue = System.Int32.Parse(parseString);
	    parsedInt32.myState = TypeState.VALID;

	    return parsedInt32;
	}
   
	public static IntegerType Parse(System.String parseString, System.Globalization.NumberStyles style) {
	    if (parseString == null) {
		throw new InvalidArgumentException("parseString");
	    }

	    IntegerType parsedInt32;

	    parsedInt32.myValue = System.Int32.Parse(parseString, style);
	    parsedInt32.myState = TypeState.VALID;

	    return parsedInt32;
	}


	public static IntegerType Parse(System.String parseString, System.IFormatProvider formatProvider) {
	    if (parseString == null) {
		throw new InvalidArgumentException("parseString");
	    }

	    IntegerType parsedInt32;

	    parsedInt32.myValue = System.Int32.Parse(parseString, formatProvider);
	    parsedInt32.myState = TypeState.VALID;

	    return parsedInt32;
	}


	public static IntegerType Parse(System.String parseString, System.Globalization.NumberStyles style, System.IFormatProvider formatProvider) {
	    if (parseString == null) {
		throw new InvalidArgumentException("parseString");
	    }

	    IntegerType parsedInt32;
	    parsedInt32.myValue = System.Int32.Parse(parseString, style, formatProvider);
	    parsedInt32.myState = TypeState.VALID;

	    return parsedInt32;
	}


	public System.String ToString(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue.ToString(formatProvider);
	}
	#endregion

	#region To<XX> methods
	public byte ToByte(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToByte(myValue);
	}

	public short ToInt16(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToInt16(myValue);
	}

	public int ToInt32(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToInt32(myValue);
	}

	public long ToInt64(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return myValue;
	}

	public float ToSingle(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToSingle(myValue);
	}

	public double ToDouble(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToDouble(myValue);
	}

	public Decimal ToDecimal(System.IFormatProvider formatProvider) {
	    if (!IsValid) {
		throw new InvalidStateException(myState);
	    }

	    return System.Convert.ToDecimal(myValue);
	}
	#endregion

	#region Cast operators
	#region byte
	public static implicit operator IntegerType(byte castFrom) {
	    IntegerType returnType = new IntegerType(castFrom);

	    return returnType;
	}

	public static explicit operator byte(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return (byte) castFrom.myValue;
	}
	#endregion

	#region short
	public static explicit operator short(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return (short) castFrom.myValue;
	}

	public static implicit operator IntegerType(short castFrom) {
	    IntegerType returnType = new IntegerType(castFrom);

	    return returnType;
	}
	#endregion

	#region int
	public static implicit operator int(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return castFrom.myValue;
	}

	public static implicit operator IntegerType(int castFrom) {
	    IntegerType returnType = new IntegerType(castFrom);

	    return returnType;
	}
	#endregion

	#region long
	public static implicit operator long(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return (long) castFrom.myValue;
	}

	public static explicit operator IntegerType(long castFrom) {
	    IntegerType returnType = new IntegerType(castFrom);

	    return returnType;
	}
	#endregion

	#region Decimal	and DecimalType
	public static explicit operator Decimal(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return (Decimal) castFrom.myValue;
	}

	public static explicit operator IntegerType(Decimal castFrom) {
	    IntegerType returnType = new IntegerType(Convert.ToInt32(castFrom));

	    return returnType;
	}

	public static explicit operator DecimalType(IntegerType castFrom) {
	    if (!castFrom.IsValid) {
		throw new InvalidStateException(castFrom.myState);
	    }

	    return new DecimalType(castFrom.myValue);
	}

	public static explicit operator IntegerType(DecimalType castFrom) {
	    IntegerType returnType = new IntegerType(Convert.ToInt32(castFrom));

	    return returnType;
	}
	#endregion
	#endregion

	#region Equality operators and methods
	public static int Compare(IntegerType leftHand, IntegerType rightHand) {
	    if (leftHand.myState == TypeState.VALID && rightHand.myState == TypeState.VALID) {
		if (leftHand.myValue < rightHand.myValue) {
		    return -1;
		}

		if (leftHand.myValue == rightHand.myValue) {
		    return 0;
		}

		if (leftHand.myValue > rightHand.myValue) {
		    return 1;
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


	#region Equality operators
	public static bool operator == (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator != (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
	}

	public static bool operator == (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue == rightHand;
	}

	public static bool operator != (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue != rightHand;
	}
	#endregion    
	#endregion

	#region < and > operators
	public static bool operator < (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
	}

	public static bool operator > (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
	}

	public static bool operator < (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue < rightHand;
	}

	public static bool operator > (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue > rightHand;
	}
	#endregion    

	#region <= and >= operators
	public static bool operator <= (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}

	public static bool operator >= (IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) >= 0;
	}

	public static bool operator <= (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue <= rightHand;
	}

	public static bool operator >= (IntegerType leftHand, int rightHand) {
	    if (!leftHand.IsValid) {
		return false;
	    }

	    return leftHand.myValue >= rightHand;
	}
	#endregion    
	    
	#region Addition operators and methods
	public static IntegerType Add(IntegerType augend, IntegerType addend) {
	    if (!augend.IsValid || !addend.IsValid) {
		throw new InvalidStateException(augend.myState, addend.myState);
	    }

	    IntegerType result = new IntegerType(augend.myValue + addend.myValue);

	    return result;
	}

	public static IntegerType operator +(IntegerType augend, IntegerType addend) {
	    if (!augend.IsValid || !addend.IsValid) {
		throw new InvalidStateException(augend.myState, addend.myState);
	    }

	    IntegerType result = new IntegerType(augend.myValue + addend.myValue);

	    return result;
	}

	public static IntegerType operator +(IntegerType augend, int addend) {
	    if (!augend.IsValid) {
		throw new InvalidStateException(augend.myState);
	    }

	    IntegerType result = new IntegerType(augend.myValue + addend);

	    return result;
	}

	public static IntegerType operator ++(IntegerType augend) {
	    if (!augend.IsValid) {
		throw new InvalidStateException(augend.myState);
	    }

	    IntegerType result = new IntegerType(augend.myValue + 1);

	    return result;
	}

	//	public static IntegerType operator ++(int augend) {
	//	    IntegerType result = new IntegerType(augend + 1);

	//	    return result;
	//	}
	#endregion

	#region Subtraction operators and methods
	public static IntegerType operator -(IntegerType minuend, IntegerType subtrahend) {
	    if (!minuend.IsValid || !subtrahend.IsValid) {
		throw new InvalidStateException(minuend.myState, subtrahend.myState);
	    }

	    IntegerType result = new IntegerType(minuend.myValue - subtrahend.myValue);

	    return result;
	}

	public static IntegerType operator -(IntegerType minuend, int subtrahend) {
	    if (!minuend.IsValid) {
		throw new InvalidStateException(minuend.myState);
	    }

	    IntegerType result = new IntegerType(minuend.myValue - subtrahend);

	    return result;
	}

	public static IntegerType operator --(IntegerType subtrahend) {
	    if (!subtrahend.IsValid) {
		throw new InvalidStateException(subtrahend.myState);
	    }

	    IntegerType result = new IntegerType(subtrahend.myValue - 1);

	    return result;
	}

	//	public static IntegerType operator --(int subtrahend) {
	//	    IntegerType result = new IntegerType(subtrahend - 1);

	//	    return result;
	//	}

	#endregion

	#region Mutliplication operators and methods
	public static IntegerType Multiply(IntegerType multiplier, IntegerType multiplicand) {
	    if (!multiplier.IsValid || !multiplicand.IsValid) {
		throw new InvalidStateException(multiplier.myState, multiplicand.myState);
	    }

	    IntegerType result = new IntegerType(multiplier.myValue * multiplicand.myValue);

	    return result;
	}

	public static IntegerType operator *(IntegerType multiplier, IntegerType multiplicand) {
	    if (!multiplier.IsValid || !multiplicand.IsValid) {
		throw new InvalidStateException(multiplier.myState, multiplicand.myState);
	    }

	    IntegerType result = new IntegerType(multiplier.myValue * multiplicand.myValue);

	    return result;
	}

	public static IntegerType operator *(IntegerType multiplier, int multiplicand) {
	    if (!multiplier.IsValid) {
		throw new InvalidStateException(multiplier.myState);
	    }

	    IntegerType result = new IntegerType(multiplier.myValue * multiplicand);

	    return result;
	}
	#endregion

	#region Division operators and methods
	public static IntegerType Divide(IntegerType dividend, IntegerType divisor) {
	    if (!dividend.IsValid || !divisor.IsValid) {
		throw new InvalidStateException(dividend.myState, divisor.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue / divisor.myValue);

	    return result;
	}

	public static IntegerType operator /(IntegerType dividend, IntegerType divisor) {
	    if (!dividend.IsValid || !divisor.IsValid) {
		throw new InvalidStateException(dividend.myState, divisor.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue / divisor.myValue);

	    return result;
	}

	public static IntegerType operator /(IntegerType dividend, int divisor) {
	    if (!dividend.IsValid) {
		throw new InvalidStateException(dividend.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue / divisor);

	    return result;
	}
	#endregion

	#region Modulus operators and methods
	public static IntegerType Remainder(IntegerType dividend, IntegerType divisor) {
	    if (!dividend.IsValid || !divisor.IsValid) {
		throw new InvalidStateException(dividend.myState, divisor.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue % divisor.myValue);

	    return result;
	}

	public static IntegerType operator %(IntegerType dividend, IntegerType divisor) {
	    if (!dividend.IsValid || !divisor.IsValid) {
		throw new InvalidStateException(dividend.myState, divisor.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue % divisor.myValue);

	    return result;
	}

	public static IntegerType operator %(IntegerType dividend, int divisor) {
	    if (!dividend.IsValid) {
		throw new InvalidStateException(dividend.myState);
	    }

	    IntegerType result = new IntegerType(dividend.myValue % divisor);

	    return result;
	}
	#endregion

	#region IComparable method
	int IComparable.CompareTo(Object value) {
	    if (!(value is IntegerType)) {
		throw new InvalidTypeException("IntegerType");
	    }

	    if (value == null) {
		throw new InvalidArgumentException("value");
	    }

	    IntegerType compareTo = (IntegerType) value;

	    return Compare(this, compareTo);
	}
	#endregion

	#region Object support and other stuff
	public override bool Equals(Object value) {
	    if (value is IntegerType) {
		return Compare(this, (IntegerType) value) == 0;
	    }

	    return false;
	}

	public static bool Equals(IntegerType leftHand, IntegerType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public override int GetHashCode() {
	    return myValue.GetHashCode();
	}
	
	public TypeCode GetTypeCode() {
	    return TypeCode.Int32;
	}
	#endregion
    }
}
