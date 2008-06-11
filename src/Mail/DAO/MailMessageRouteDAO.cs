using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.Dao {
    public class MailMessageRouteDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly MailMessageRouteDAO DAO = new MailMessageRouteDAO();
	private static readonly String VIEW = "vwMailMessageRoute";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public Int32 MailMessageRouteId;
	    public Int32 MailMessage;
	    public Int32 RoutingType;
	    public Int32 Status;
	    public Int32 EmailAddress;

	    internal ColumnOrdinals(IDataReader reader) {
		MailMessageRouteId = reader.GetOrdinal("MailMessageRouteId");
		MailMessage = reader.GetOrdinal("MailMessage");
		RoutingType = reader.GetOrdinal("RoutingType");
		Status = reader.GetOrdinal("Status");
		EmailAddress = reader.GetOrdinal("EmailAddress");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		MailMessageRouteId = reader.GetOrdinal(prefix + "MailMessageRouteId");
		MailMessage = reader.GetOrdinal(prefix + "MailMessage");
		RoutingType = reader.GetOrdinal(prefix + "RoutingType");
		Status = reader.GetOrdinal(prefix + "Status");
		EmailAddress = reader.GetOrdinal(prefix + "EmailAddress");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MailMessageRouteDAO() {
	    AddPropertyMapping("MailMessageRouteId", @"MailMessageRouteId");
	    AddPropertyMapping("MailMessage", @"MailMessage");
	    AddPropertyMapping("RoutingType", @"RoutingType");
	    AddPropertyMapping("Status", @"Status");
	    AddPropertyMapping("EmailAddress", @"EmailAddress");
	}

	private MailMessageRouteDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all MailMessageRoute rows.
	/// </summary>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageRouteList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageRouteList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of MailMessageRoute rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageRouteList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageRouteList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MailMessageRouteList list = new MailMessageRouteList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all MailMessageRoute rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageRouteList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageRouteList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MailMessageRoute rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageRouteList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageRouteList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MailMessageRouteList list = new MailMessageRouteList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a MailMessageRoute entity using it's primary key.
	/// </summary>
	/// <param name="mailMessageRouteId">A key field.</param>
	/// <returns>A MailMessageRoute object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MailMessageRoute Load(IdType mailMessageRouteId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MailMessageRouteId", EqualityOperatorEnum.Equal, mailMessageRouteId.IsValid ? mailMessageRouteId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MailMessageRoute instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MailMessageRouteId", EqualityOperatorEnum.Equal, instance.MailMessageRouteId.IsValid ? instance.MailMessageRouteId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MailMessageRoute.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private MailMessageRouteList GetList(IDataReader reader) {
	    MailMessageRouteList list = new MailMessageRouteList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MailMessageRoute GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MailMessageRoute GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MailMessageRoute data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(MailMessageRoute data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(MailMessageRoute data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MailMessageRoute data = new MailMessageRoute(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MailMessageRoute data = new MailMessageRoute(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    MailMessageRoute data = new MailMessageRoute(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessageRoute GetDataObjectFromReader(MailMessageRoute data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.MailMessageRouteId)) {
		data.MailMessageRouteId = IdType.UNSET;
	    } else {
		data.MailMessageRouteId = new IdType(dataReader.GetInt32(ordinals.MailMessageRouteId));
	    }
	    if (dataReader.IsDBNull(ordinals.MailMessage)) {
		data.MailMessage = StringType.UNSET;
	    } else {
		data.MailMessage = StringType.Parse(dataReader.GetString(ordinals.MailMessage));
	    }
	    if (dataReader.IsDBNull(ordinals.RoutingType)) {
		data.RoutingType = RoutingTypeEnum.UNSET;
	    } else {
		data.RoutingType = RoutingTypeEnum.GetInstance(dataReader.GetString(ordinals.RoutingType));
	    }
	    if (dataReader.IsDBNull(ordinals.Status)) {
		data.Status = ActiveStatusEnum.UNSET;
	    } else {
		data.Status = ActiveStatusEnum.GetInstance(dataReader.GetString(ordinals.Status));
	    }
	    if (dataReader.IsDBNull(ordinals.EmailAddress)) {
		data.EmailAddress = StringType.UNSET;
	    } else {
		data.EmailAddress = StringType.Parse(dataReader.GetString(ordinals.EmailAddress));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MailMessageRoute data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MailMessageRoute data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessageRoute_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailMessage", DbType.AnsiString, ParameterDirection.Input, data.MailMessage.IsValid ? data.MailMessage.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@RoutingType", DbType.AnsiString, ParameterDirection.Input, data.RoutingType.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Status", DbType.AnsiString, ParameterDirection.Input, data.Status.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@EmailAddress", DbType.AnsiString, ParameterDirection.Input, data.EmailAddress.IsValid ? data.EmailAddress.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}


	/// <summary>
	/// Updates a record in the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MailMessageRoute data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MailMessageRoute data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessageRoute_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageRouteId", DbType.Int32, ParameterDirection.Input, data.MailMessageRouteId.IsValid ? data.MailMessageRouteId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessage", DbType.AnsiString, ParameterDirection.Input, data.MailMessage.IsValid ? data.MailMessage.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@RoutingType", DbType.AnsiString, ParameterDirection.Input, data.RoutingType.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Status", DbType.AnsiString, ParameterDirection.Input, data.Status.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@EmailAddress", DbType.AnsiString, ParameterDirection.Input, data.EmailAddress.IsValid ? data.EmailAddress.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the MailMessageRoute table by MailMessageRouteId.
	/// </summary>
	/// <param name="mailMessageRouteId">A key field.</param>
	public void Delete(IdType mailMessageRouteId) {
	    Delete(mailMessageRouteId, null);
	}

	/// <summary>
	/// Deletes a record from the MailMessageRoute table by MailMessageRouteId.
	/// </summary>
	/// <param name="mailMessageRouteId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType mailMessageRouteId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessageRoute_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageRouteId", DbType.Int32, ParameterDirection.Input, mailMessageRouteId.IsValid ? mailMessageRouteId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="mailMessage">A field value to be matched.</param>
	/// <returns>The list of MailMessageRouteDAO objects found.</returns>
	public MailMessageRouteList FindByMailMessage(StringType mailMessage) {
	    OrderByClause sort = new OrderByClause("MailMessage");
	    SqlFilter filter = new SqlFilter(new SqlLiteralPredicate(" MailMessage = @MailMessage"));
	    String sql = "SELECT * from " + VIEW + filter.Statement + sort.FormatSql();
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@MailMessage", DbType.AnsiString, ParameterDirection.Input, mailMessage.IsValid ? mailMessage.ToString() as Object : DBNull.Value));
#if (NET_1_1)
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
#else
		IDataReader dataReader = null;
		if (DbConnectionScope.Current == null) {
			dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		} else {
			dataReader = cmd.ExecuteReader();
		}
#endif
	    return GetList(dataReader);
	}
    }
}
