using System;
using System.Data.SqlClient;

namespace Spring2.Core.DAO {

    /// <summary>
    /// Wrapper of SqlTransaction to ensure that the connection associated with a transaction is closed when Commit()
    /// or Rollback() is called.
    /// </summary>
    public class DaoTransaction {

	private SqlTransaction transaction;

	/// <summary>
	/// Initialize a new instance of DaoTransaction class with an existing SqlTransaction.
	/// </summary>
	/// <param name="transaction"></param>
	public DaoTransaction(SqlTransaction transaction) {
	    this.transaction = transaction;
	}

	/// <summary>
	/// Commit the transaction
	/// </summary>
	public void Commit() {
	    SqlConnection conn = transaction.Connection;

	    try {
		transaction.Commit();
	    } finally {
		conn.Close();
	    }
	}

	/// <summary>
	/// Rollback the transaction
	/// </summary>
	public void Rollback() {
	    SqlConnection conn = transaction.Connection;

	    try {
		transaction.Rollback();
	    } finally {
		conn.Close();
	    }
	}

	/// <summary>
	/// Rollback the transaction to a named point
	/// </summary>
	/// <param name="transactionName"></param>
	public void Rollback(String transactionName) {
	    SqlConnection conn = transaction.Connection;
	    
	    try {
		transaction.Rollback(transactionName);
	    } finally {
		conn.Close();
	    }
	}

	/// <summary>
	/// Expose the wrapped internal SqlTransaction.
	/// </summary>
	public SqlTransaction SqlTransaction {
	    get { return this.transaction; }
	}

    }
}
