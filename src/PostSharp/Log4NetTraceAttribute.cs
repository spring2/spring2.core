using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using PostSharp.Laos;
using Spring2.Core.Util;

namespace Spring2.Dss.Facade {

    [Serializable]
    public sealed class Log4NetTraceAttribute : OnMethodBoundaryAspect {

	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	public override void OnEntry(MethodExecutionEventArgs eventArgs) {
	    eventArgs.MethodExecutionTag = new Timer();
	    log.Debug(String.Format("Entering {0}.{1}.", eventArgs.Method.DeclaringType.Name, eventArgs.Method.Name));
	}

	public override void OnExit(MethodExecutionEventArgs eventArgs) {
	    Timer timer = eventArgs.MethodExecutionTag as Timer;
	    timer.Stop();
	    log.Debug(String.Format("Leaving {0}.{1} with execution time of {2} seconds.", eventArgs.Method.DeclaringType.Name, eventArgs.Method.Name, timer.TimeSpan.TotalSeconds.ToString()));
	}

    }
}
