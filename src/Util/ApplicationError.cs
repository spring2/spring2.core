using System;

namespace Spring2.Core.Util {

    /// <summary>
    /// Represents an error caused by user input.
    /// </summary>
    public abstract class ApplicationError {

	private String propertyName;
	private String message;

	/// <summary>
	/// Creates an application error from the given property name.
	/// </summary>
	/// <param name="propertyName">the name of the property associated with the error.</param>
	public ApplicationError(String propertyName) {
	    if (propertyName == null) {
		this.propertyName = String.Empty;
	    } else {
		this.propertyName = propertyName;
	    }
	}

	/// <summary>
	/// Read-only property for accessing the property name of the error.
	/// </summary>
	public String PropertyName {
	    get { return propertyName; }
	}

	/// <summary>
	/// Property for getting and setting a unique message associated with the error.
	/// </summary>
	public String Message {
	    get { return message; }
	    set { message = value; }
	}

	public abstract String FormatMessage(String displayName);
    }
}
