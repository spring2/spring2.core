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
    public class MailAttachmentDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly MailAttachmentDAO DAO = new MailAttachmentDAO();
	private static readonly String VIEW = "vwMailAttachment";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public Int32 MailAttachmentId;
	    public Int32 MailMessageId;
	    public Int32 Filename;
	    public Int32 Text;

	    internal ColumnOrdinals(IDataReader reader) {
		MailAttachmentId = reader.GetOrdinal("MailAttachmentId");
		MailMessageId = reader.GetOrdinal("MailMessageId");
		Filename = reader.GetOrdinal("Filename");
		Text = reader.GetOrdinal("Text");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		MailAttachmentId = reader.GetOrdinal(prefix + "MailAttachmentId");
		MailMessageId = reader.GetOrdinal(prefix + "MailMessageId");
		Filename = reader.GetOrdinal(prefix + "Filename");
		Text = reader.GetOrdinal(prefix + "Text");
	    }
	}

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MailAttachmentDAO() {
	    if (!propertyToSqlMap.Contains("MailAttachmentId")) {
		propertyToSqlMap.Add("MailAttachmentId", @"MailAttachmentId");
	    }
	    if (!propertyToSqlMap.Contains("MailMessageId")) {
		propertyToSqlMap.Add("MailMessageId", @"MailMessageId");
	    }
	    if (!propertyToSqlMap.Contains("Filename")) {
		propertyToSqlMap.Add("Filename", @"Filename");
	    }
	    if (!propertyToSqlMap.Contains("Text")) {
		propertyToSqlMap.Add("Text", @"Text");
	    }
	}

	private MailAttachmentDAO() {
	}

	/// <summary>
	/// Creates a where clause object by mapping the given where clause text.  The text may reference
	/// entity properties which will be mapped to sql code by enclosing the property names in braces.
	/// </summary>
	/// <param name="whereText">Text to be mapped</param>
	/// <returns>SqlFilter object.</returns>
	/// <exception cref="ApplicationException">When property name found in braces is not found in the entity.</exception>
	public static SqlFilter Filter(String whereText) {
	    return new SqlFilter(new SqlLiteralPredicate(ProcessExpression(propertyToSqlMap, whereText)));
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A SqlFilter object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static SqlFilter Filter(String propertyName, String value) {
	    return new SqlFilter(new SqlEqualityPredicate(GetPropertyMapping(propertyToSqlMap, propertyName), EqualityOperatorEnum.Equal, value));
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A SqlFilter object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static SqlFilter Filter(String propertyName, Int32 value) {
	    return new SqlFilter(new SqlEqualityPredicate(GetPropertyMapping(propertyToSqlMap, propertyName), EqualityOperatorEnum.Equal, value));
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A SqlFilter object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static SqlFilter filter(String propertyName, DateTime value) {
	    return new SqlFilter(new SqlEqualityPredicate(GetPropertyMapping(propertyToSqlMap, propertyName), EqualityOperatorEnum.Equal, value));
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all MailAttachment rows.
	/// </summary>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailAttachmentList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailAttachmentList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of MailAttachment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailAttachmentList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailAttachmentList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MailAttachmentList list = new MailAttachmentList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all MailAttachment rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailAttachmentList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailAttachmentList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MailAttachment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailAttachmentList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailAttachmentList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MailAttachmentList list = new MailAttachmentList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a MailAttachment entity using it's primary key.
	/// </summary>
	/// <param name="MailAttachmentId">A key field.</param>
	/// <returns>A MailAttachment object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MailAttachment Load(IdType mailAttachmentId) {
	    String sql = "SELECT * FROM " + VIEW + " WHERE MailAttachmentId=@MailAttachmentId";
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@MailAttachmentId", DbType.Int32, ParameterDirection.Input, mailAttachmentId.IsValid ? mailAttachmentId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private static MailAttachmentList GetList(IDataReader reader) {
	    MailAttachmentList list = new MailAttachmentList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MailAttachment instance) {
	    String sql = "SELECT * FROM " + VIEW + " WHERE MailAttachmentId=@MailAttachmentId";
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@MailAttachmentId", DbType.Int32, ParameterDirection.Input, instance.MailAttachmentId.IsValid ? instance.MailAttachmentId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MailAttachment.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private static MailAttachment GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private static MailAttachment GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MailAttachment data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(MailAttachment data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(MailAttachment data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MailAttachment data = new MailAttachment(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MailAttachment data = new MailAttachment(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    MailAttachment data = new MailAttachment(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailAttachment GetDataObjectFromReader(MailAttachment data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MailAttachmentId"))) {
		data.MailAttachmentId = IdType.UNSET;
	    } else {
		data.MailAttachmentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("MailAttachmentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MailMessageId"))) {
		data.MailMessageId = IdType.UNSET;
	    } else {
		data.MailMessageId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("MailMessageId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Filename"))) {
		data.Filename = StringType.UNSET;
	    } else {
		data.Filename = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Filename")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Text"))) {
		data.Text = null;
	    } else {
 		Byte[] b = new Byte[(dataReader.GetBytes(dataReader.GetOrdinal("Text"), 0, null, 0, int.MaxValue))]; 	
 		dataReader.GetBytes(dataReader.GetOrdinal("Text"), 0, b, 0, b.Length);
 		data.Buffer = b ;
 		data.Text = b;
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MailAttachment data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MailAttachment data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailAttachment_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, data.MailMessageId.IsValid ? data.MailMessageId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Filename", DbType.AnsiString, ParameterDirection.Input, data.Filename.IsValid ? data.Filename.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Text", DbType.Binary, ParameterDirection.Input, data.Text));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}


	/// <summary>
	/// Updates a record in the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MailAttachment data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MailAttachment data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailAttachment_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailAttachmentId", DbType.Int32, ParameterDirection.Input, data.MailAttachmentId.IsValid ? data.MailAttachmentId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, data.MailMessageId.IsValid ? data.MailMessageId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Filename", DbType.AnsiString, ParameterDirection.Input, data.Filename.IsValid ? data.Filename.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Text", DbType.Binary, ParameterDirection.Input, data.Text));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the MailAttachment table by MailAttachmentId.
	/// </summary>
	/// <param name="MailAttachmentId">A key field.</param>
	public void Delete(IdType mailAttachmentId) {
	    Delete(mailAttachmentId, null);
	}

	/// <summary>
	/// Deletes a record from the MailAttachment table by MailAttachmentId.
	/// </summary>
	/// <param name="MailAttachmentId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType mailAttachmentId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailAttachment_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MailAttachmentId", DbType.Int32, ParameterDirection.Input, mailAttachmentId.IsValid ? mailAttachmentId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="MailMessageId">A field value to be matched.</param>
	/// <returns>The list of MailAttachmentDAO objects found.</returns>
	public MailAttachmentList FindByMailMessageId(IdType mailMessageId) {
	    String sql = "SELECT * FROM " + VIEW + " WHERE (MailMessageId=@MailMessageId or (MailMessageId is null and @MailMessageId is null)) ORDER BY MailMessageId";
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, mailMessageId.IsValid ? mailMessageId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

	    return GetList(dataReader);
	}
    }
}
