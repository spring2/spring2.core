using System;

namespace Spring2.Core.DAO {

    /// <summary>
    /// Exception created when a finder returns no rows.
    /// </summary>
    public class FinderException : System.ApplicationException {
	public FinderException() {}

	public FinderException (String message) : base(message) {}
    }
}
