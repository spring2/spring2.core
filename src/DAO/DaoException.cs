using System;

namespace Spring2.Core.DAO {

    /// <summary>
    /// Base exception for DAO exceptions
    /// </summary>
    public class DaoException : System.ApplicationException {
	public DaoException() {}

	public DaoException (String message) : base(message) {}
    }
}
