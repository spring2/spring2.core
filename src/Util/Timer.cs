using System;

namespace Spring2.Core.Util {
    /// <summary>
    /// Util class to aide in performance timings.  Internally maintains time in ticks.  Start and Stop properties are expressed as ticks.
    /// </summary>
    public class Timer {

	private long start;
	private long stop;

	public Timer() {
	    Start();
	}

	public void Start() {
	    start = DateTime.Now.Ticks;
	    stop = -1;
	}

	public void Stop() {
	    stop = DateTime.Now.Ticks;
	    if (start==-1)
		start = stop;
	}

	public TimeSpan TimeSpan  {
	    get {
		if (start == -1)
		    return new TimeSpan(0);

		if (stop == -1)
		    return new TimeSpan(DateTime.Now.Ticks - start);

		if (start!=-1 && stop!=-1)
		    return new TimeSpan(stop - start);

		return new TimeSpan(0);
	    }
	}

	public void Reset() {
	    start = -1;
	    stop = -1;
	}
    }
}

