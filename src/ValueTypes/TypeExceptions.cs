using System;

namespace Spring2.Types
{
      public class TypesException : System.Exception
      {
         public TypesException()
         {
         }

         public TypesException(string message) 
            : base(message)
         {
         }
      }

      public class InvalidValueException : TypesException
      {
         public InvalidValueException(TypeState state) 
            : base(string.Format("State is {0} should be VALID", state.ToString()))
         {
         }

         public InvalidValueException(TypeState leftState, TypeState rightState) 
            : base(string.Format("Invalid state. left and right should be VALID but are ({0}, {1})", leftState.ToString(), rightState.ToString()))
         {
         }

         public InvalidValueException(TypeState stateOne, TypeState stateTwo, TypeState stateThree) 
            : base(string.Format("Invalid state. states should be VALID but are ({0}, {1}, {2})", stateOne.ToString(), stateTwo.ToString(), stateThree.ToString()))
         {
         }

	  public InvalidValueException(string message) 
            : base(message)
         {
         }
      }

      public class InvalidStateException : TypesException
      {
         public InvalidStateException(TypeState state) 
            : base(string.Format("State is {0} should be VALID", state.ToString()))
         {
         }

         public InvalidStateException(TypeState leftState, TypeState rightState) 
            : base(string.Format("Invalid state. left and right should be VALID but are ({0}, {1})", leftState.ToString(), rightState.ToString()))
         {
         }

         public InvalidStateException(TypeState stateOne, TypeState stateTwo, TypeState stateThree) 
            : base(string.Format("Invalid state. states should be VALID but are ({0}, {1}, {2})", stateOne.ToString(), stateTwo.ToString(), stateThree.ToString()))
         {
         }

	  public InvalidStateException(string message) 
            : base(message)
         {
         }
      }

      public class InvalidArgumentException : TypesException
      {
         public InvalidArgumentException(string argumentName)
            : base("Invalid argument '" + argumentName + "'")
         {
         }
      }

      public class InvalidTypeException : TypesException
      {
         public InvalidTypeException(string argumentName)
            : base("Invalid type. expected '" + argumentName + "'")
         {
         }
      }

      public class InvalidConversionException : TypesException
      {
         public InvalidConversionException(string fromType, string toType)
            : base("Invalid type when converting from '" + fromType + "' to '" + toType + "'")
         {
         }
      }

      public class InvalidCastException : TypesException
      {
         public InvalidCastException(string fromType, string toType)
            : base("Invalid type when casting from '" + fromType + "' to '" + toType + "'")
         {
         }

	 public InvalidCastException(string message)
	      : base("message")
	 {
	 }
      }

      public class NotImplementedException : TypesException
      {
         public NotImplementedException(string methodName)
            : base("Unimplemented method '" + methodName + "'")
         {
         }
      }

      class InvalidFormatException : TypesException
      {
         public InvalidFormatException(string typeName)
            : base("Invalid format for type '" + typeName + "'")
         {
         }
      }

      public class ValueOverflowException : TypesException
      {
         public ValueOverflowException(string valueName)
            : base(String.Format("Value overflow for value '{0}'", valueName))
         {
         }

         public ValueOverflowException(string from, string to)
            : base(String.Format("Value overflow converting from type '{0}' to type '{1}'", from, to))
         {
         }
      }
}
