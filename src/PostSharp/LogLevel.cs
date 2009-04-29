using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostSharp {
    /// <summary>
    /// Specifies severity level of a logged message.
    /// </summary>
    public enum LogLevel {
	/// <summary>
	/// No message should be logged.
	/// </summary>
	None,

	/// <summary>
	/// Message should be logged using Debug() method.
	/// </summary>
	Debug,

	/// <summary>
	/// Message should be logged using Info() method.
	/// </summary>
	Info,

	/// <summary>
	/// Message should be logged using Warn() method.
	/// </summary>
	Warn,

	/// <summary>
	/// Message should be logged using Error() method.
	/// </summary>
	Error,

	/// <summary>
	/// Message should be logged using Fatal() method.
	/// </summary>
	Fatal
    }
}
