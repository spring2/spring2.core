using System.Text;
using System;

namespace Spring2.Core.Types {

    [Serializable]
    public struct TimeType : IComparable, IDataType {
	private TimeSpan  myValue;
	private TypeState myState;

	#region Constants
	//a tick is equal to 100 nanoseconds

	public const long TicksPerMillisecond = 10000;
	public const long TicksPerSecond      = TicksPerMillisecond * 1000;
	public const long TicksPerMinute      = TicksPerSecond * 60;
	public const long TicksPerHour        = TicksPerMinute * 60;
	public const long TicksPerHalfDay     = TicksPerHour * 12;
	public const long TicksPerDay         = TicksPerHalfDay * 2;

	public const int MilliSecondsPerSecond  = 1000;
	public const int MilliSecondsPerMinute  = MilliSecondsPerSecond * 60;
	public const int MilliSecondsPerHour    = MilliSecondsPerMinute * 60;
	public const int MilliSecondsPerHalfDay = MilliSecondsPerHour * 12;
	public const int MilliSecondsPerDay     = MilliSecondsPerHalfDay * 2;

	public static readonly TimeType Zero = new TimeType(0L);
	public static readonly TimeType MinValue = new TimeType(0L);
	public static readonly TimeType MaxValue = new TimeType(TicksPerDay);
	#endregion

	private static readonly long TicksMinValue = 0;
	private static readonly long TicksMaxValue = TicksPerDay;

	public static readonly TimeType DEFAULT = new TimeType(TypeState.DEFAULT);
	public static readonly TimeType UNSET   = new TimeType(TypeState.UNSET);

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
	private TimeType(TypeState state) {
	    myState = state;
	    myValue = new TimeSpan(0);
	}

	public TimeType(long ticks) {
	    if (ticks < TicksMinValue || ticks > TicksMaxValue) {
		throw new ArgumentOutOfRangeException("Arguement out of range - MinValue < ticks < MaxTicks");
	    }

	    myValue = new TimeSpan(ticks);
	    myState = TypeState.VALID;
	}

	public TimeType(TimeSpan value) {
	    if (!InRange(value)) {
		throw new ArgumentOutOfRangeException("Arguement out of range - MinValue < ticks < MaxTicks");
	    }

	    myValue = value;
	    myState = TypeState.VALID;
	}

	public TimeType(int hours, int minutes, int seconds) {
	    TimeSpan time = new TimeSpan(hours, minutes, seconds);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range - MinValue < ticks < MaxTicks");
	    }

	    myValue = time;
	    myState = TypeState.VALID;
	}
	#endregion 

	#region Time part accessors (Ticks, Hours, Minutes, ..)
	public long Ticks {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue.Ticks;
	    }
	}

	public int Hours {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue.Hours;
	    }
	}

	public int Milliseconds {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue.Milliseconds;
	    }
	}

	public int Minutes {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue.Minutes;
	    }
	}

	public int Seconds {
	    get {
		if (!IsValid) {
		    throw new InvalidStateException(myState);
		}
		return myValue.Seconds;
	    }
	}
	#endregion 

	#region Comparison Methods
	public static int Compare(TimeType leftHand, TimeType rightHand) {
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


	public int CompareTo(Object value) {
	    if (value == null) {
		throw new InvalidTypeException("CompareTo parameter must be of type TimeType");
	    }

	    return Compare(this, (TimeType) value);
	}

	#endregion 

	#region From<Hours/minutes/..> methods
	public static TimeType FromHours(double value) {
	    TimeSpan time = TimeSpan.FromHours(value);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}

	public static TimeType FromMilliseconds(double value) {
	    TimeSpan time = TimeSpan.FromMilliseconds(value);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}

	public static TimeType FromMinutes(double value) {
	    TimeSpan time = TimeSpan.FromMinutes(value);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}

	public static TimeType FromSeconds(double value) {
	    TimeSpan time = TimeSpan.FromSeconds(value);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}		    

	public static TimeType FromTicks(long value) {
	    TimeSpan time = new TimeSpan(value);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}
	#endregion 

	#region Addition and Subtraction operators and methods
	#region Addition operators and methods
	public TimeType Add(TimeType toAdd) {
	    if (!IsValid || !toAdd.IsValid) {
		throw new InvalidStateException(myState, toAdd.myState);
	    }

	    TimeSpan time = myValue;
	    time.Add(toAdd.myValue);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}

	public static TimeType operator +(TimeType leftHand, TimeType rightHand) {
	    return leftHand.Add(rightHand);
	}
	#endregion 

	#region Subtract methods and operators
	public TimeType Subtract(TimeType toSubtract) {
	    TimeSpan time = myValue.Subtract(toSubtract.myValue);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}
        
	public static TimeType operator -(TimeType leftHand, TimeType rightHand) {
	    return leftHand.Subtract(rightHand);
	}
	#endregion
	#endregion 

	#region ToString and Parse methods
	public override String ToString() {
	    return IsValid ? this.myValue.ToString() : myState.ToString();
	}

	public static TimeType Parse(String s) {
	    TimeSpan time = TimeSpan.Parse(s);

	    if (!InRange(time)) {
		throw new ArgumentOutOfRangeException("Arguement out of range");
	    }

	    return new TimeType(time);
	}
	#endregion 

	#region Equality operators and methods
	public static bool Equals(TimeType leftHand, TimeType rightHand) {
	    if (!leftHand.IsValid || !rightHand.IsValid) {
		throw new InvalidStateException(leftHand.myState, rightHand.myState);
	    }

	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator ==(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) == 0;
	}

	public static bool operator !=(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) != 0;
	}

	public static bool operator <(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) < 0;
	}

	public static bool operator <=(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}

	public static bool operator >(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) > 0;
	}

	public static bool operator >=(TimeType leftHand, TimeType rightHand) {
	    return Compare(leftHand, rightHand) <= 0;
	}
	#endregion

	#region Object support
	public override bool Equals(Object value) {
	    if (value is TimeType) {
		return Compare(this, ((TimeType) value)) == 0;
	    }

	    return false;
	}

	public override int GetHashCode() {
	    return myValue.GetHashCode();
	}
	#endregion 

	#region Support methods
	private static bool InRange(TimeType value) {
	    if (!InRange(value)) {
		return false;
	    }

	    return true;
	}

	private static bool InRange(TimeSpan value) {
	    if (value.Ticks < TicksMinValue || value.Ticks > TicksMaxValue) {
		return false;
	    }

	    return true;
	}
	#endregion 

	//sets this instance to the passed in value
	//if it is invalid. used in SetInitialDefaults, etc.

	public void SetIfInvalid(TimeType value) {
	    if (!IsValid) {
		myValue = value.myValue;
		myState = value.myState;
	    }
	}
    }
}
