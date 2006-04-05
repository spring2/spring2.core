using System;
using System.Globalization;

namespace Spring2.Core.Types {

    public struct IdType : IDataType {

	private IntegerType myValue;

	public static readonly IdType DEFAULT = new IdType(TypeState.DEFAULT);
	public static readonly IdType UNSET   = new IdType(TypeState.UNSET);

	#region State management
	public bool IsValid {
	    get {return myValue.IsValid;}
	}

	public void SetValid() {
	    myValue.SetValid();
	}

	public bool IsDefault {
	    get {return myValue.IsDefault;}
	}

	public void SetDefault() {
	    myValue.SetDefault();
	}

	public bool IsUnset {
	    get {return myValue.IsUnset;}
	}

	public void SetUnset() {
	    myValue.SetUnset();
	}

	public TypeState State {
	    get {return myValue.State;}
	    set {myValue.State = value;}
	}
	#endregion

	#region Constructors
	private IdType(TypeState state) {
	    myValue = 0;
	    myValue.State = state;
	}

	public IdType(IntegerType value) {
	    myValue = value;
	}

	public IdType(Int32 value) {
	    myValue = value;
	}
	#endregion

	public static IdType Parse(String value) {
	    return new IdType(IntegerType.Parse(value));
	}

	public override System.String ToString() {
	    return myValue.ToString();
	}


	public System.String Display() {
	    return myValue.Display();
	}

	public int ToInt32() {
	    if (!IsValid) {
		throw new InvalidStateException(myValue.State);
	    }

	    return myValue.ToInt32();
	}

	public static implicit operator IdType(int castFrom) {
	    IdType returnType = new IdType(castFrom);

	    return returnType;
	}

    }
}
