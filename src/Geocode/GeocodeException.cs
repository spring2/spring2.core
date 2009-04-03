using System;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GeocodeException.
    /// </summary>
    public class GeocodeException : Exception {

	private Int32 matchType = 0;
 
	public Int32 MatchType {
	    get {return matchType;}
	}

	public GeocodeException() {
	}

	public GeocodeException(String message,Int32 matchType) : base(message) {
	}
    }
}
