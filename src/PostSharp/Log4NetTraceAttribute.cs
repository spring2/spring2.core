using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using log4net;
using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

using Spring2.Core.Util;

namespace Spring2.Core.PostSharp {

    [Serializable]
    //[AspectTypeDependency( AspectDependencyAction.None, AspectDependencyPosition.Any, typeof(Log4NetTraceAttribute))]
    //[AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Any, "foo")]
    [WaiveAspectEffect()]
    public sealed class Log4NetTraceAttribute : OnMethodBoundaryAspect {

	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


	/// <summary>
	/// Level of messages logged when a method is entered.
	/// </summary>
	private LogLevel entryLevel = LogLevel.None;

	/// <summary>
	/// Message to log when a method is entered.
	/// </summary>
	private string entryText = "Entering method: {0}.{1}.";

	/// <summary>
	/// Level of messages logged when a method is exited normally (i.e. without throwing exception).
	/// </summary>
	private LogLevel exitLevel = LogLevel.None;

	/// <summary>
	/// Message to log when a method is exited normally (i.e. without throwing exception).
	/// </summary>
	private string exitText = "Exiting method: {0}.{1} with execution time of {2} seconds.";

	/// <summary>
	/// Level of messages logged when an exception is thrown from a method.
	/// </summary>
	private LogLevel exceptionLevel = LogLevel.None;

	/// <summary>
	/// Message to log when an exception is thrown from a method.
	/// </summary>
	private string exceptionText = "Exception thrown from method: {0}.{1} with execution time of {2} seconds.";

	/// <summary>
	/// Gets or sets the level of messages logged when a method is entered.
	/// </summary>
	/// <remarks>
	/// <para>Default value of this proprerty is <see cref="LogLevel.None"/>.</para>
	/// </remarks>
	public LogLevel EntryLevel {
	    get { return this.entryLevel; }
	    set { this.entryLevel = value; }
	}

	/// <summary>
	/// Gets or sets the message to log when a method is entered.
	/// </summary>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	/// <remarks>
	/// <para>Default value of this proprerty is the following string: "Entering method: {signature}.".</para>
	/// <para>Please refer to the class documentation for more information.</para>
	/// </remarks>
	public string EntryText {
	    get { return this.entryText; }
	    set {
		if (value == null) {
		    throw new ArgumentNullException("value");
		}

		this.entryText = value;
	    }
	}

	/// <summary>
	/// Gets or sets the level of messages logged when a method is exited normally (i.e. without throwing exception).
	/// </summary>
	/// <remarks>
	/// <para>Default value of this proprerty is <see cref="LogLevel.None"/>.</para>
	/// </remarks>
	public LogLevel ExitLevel {
	    get { return this.exitLevel; }
	    set { this.exitLevel = value; }
	}

	/// <summary>
	/// Gets or sets the message to log when a method is exited normally (i.e. without throwing exception).
	/// </summary>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	/// <remarks>
	/// <para>Default value of this proprerty is the following string: "Exiting method: {signature}.".</para>
	/// <para>Please refer to the class documentation for more information.</para>
	/// </remarks>
	public string ExitText {
	    get { return this.exitText; }
	    set {
		if (value == null) {
		    throw new ArgumentNullException("value");
		}

		this.exitText = value;
	    }
	}

	/// <summary>
	/// Gets or sets the level of messages logged when an exception is thrown from a method.
	/// </summary>
	/// <remarks>
	/// <para>Default value of this proprerty is <see cref="LogLevel.None"/>.</para>
	/// </remarks>
	public LogLevel ExceptionLevel {
	    get { return this.exceptionLevel; }
	    set { this.exceptionLevel = value; }
	}

	/// <summary>
	/// Gets or sets the message to log when an exception is thrown from a method.
	/// </summary>
	/// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
	/// <remarks>
	/// <para>Default value of this proprerty is the following string: "Exception thrown from method: {signature}.".</para>
	/// <para>Please refer to the class documentation for more information.</para>
	/// </remarks>
	public string ExceptionText {
	    get { return this.exceptionText; }
	    set {
		if (value == null) {
		    throw new ArgumentNullException("value");
		}

		this.exceptionText = value;
	    }
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Log4NetTraceAttribute"/> class.
	/// </summary>
	public Log4NetTraceAttribute() {
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Log4NetTraceAttribute"/> class with the specified entry
	/// message details.
	/// </summary>
	/// <param name="entryLevel">Level of the message that will be logged when a method is entered.</param>
	/// <param name="entryText">Message to log when a method is entered.</param>
	/// <exception cref="ArgumentNullException"><paramref name="entryText"/> is <see langword="null"/>.</exception>
	/// <remarks>
	/// <para>This constructor also sets <see cref="ExceptionLevel"/> to <see cref="LogLevel.Error"/>.</para>
	/// <para>Please refer to the class documentation for more information.</para>
	/// </remarks>
	public Log4NetTraceAttribute(LogLevel entryLevel, string entryText) {
	    if (entryText == null) {
		throw new ArgumentNullException("entryText");
	    }

	    this.entryLevel = entryLevel;
	    this.entryText = entryText;
	    this.exceptionLevel = LogLevel.Error;
	}

	public override void OnEntry(MethodExecutionArgs eventArgs) {
	    eventArgs.MethodExecutionTag = new Timer();
	    String message = String.Format(EntryText, eventArgs.Method.DeclaringType.Name, eventArgs.Method.Name);
	    Log(EntryLevel, message);
	}

	public override void OnExit(MethodExecutionArgs eventArgs) {
	    Timer timer = eventArgs.MethodExecutionTag as Timer;
	    timer.Stop();
	    String message = String.Format(ExitText, eventArgs.Method.DeclaringType.Name, eventArgs.Method.Name, timer.TimeSpan.TotalSeconds.ToString());
	    Log(ExitLevel, message);
	}

	public override void OnException(MethodExecutionArgs eventArgs) {
	    Timer timer = eventArgs.MethodExecutionTag as Timer;
	    timer.Stop();
	    String message = String.Format(ExceptionText, eventArgs.Method.DeclaringType.Name, eventArgs.Method.Name, timer.TimeSpan.TotalSeconds.ToString());
	    Log(ExceptionLevel, message);
	}

	private void Log(LogLevel level, String message) {
	    switch (level) {
		case LogLevel.Debug:
		    log.Debug(message);
		    break;
		case LogLevel.Info:
		    log.Info(message);
		    break;
		case LogLevel.Warn:
		    log.Warn(message);
		    break;
		case LogLevel.Error:
		    log.Error(message);
		    break;
		case LogLevel.Fatal:
		    log.Fatal(message);
		    break;
		default:
		    // do nothing, no logging
		    break;
	    }
	}

    }
}
