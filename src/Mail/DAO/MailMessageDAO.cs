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
    public class MailMessageDAO : Spring2.Core.DAO.SqlEntityDAO {


	public static readonly MailMessageDAO DAO = new MailMessageDAO(); 
	private static readonly String VIEW = "vwMailMessage";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static Hashtable propertyToSqlMap = new Hashtable();

	static MailMessageDAO() {
	    if (!propertyToSqlMap.Contains("MailMessageId")) {
		propertyToSqlMap.Add("MailMessageId",@"MailMessageId");
	    }
	    if (!propertyToSqlMap.Contains("ScheduleTime")) {
		propertyToSqlMap.Add("ScheduleTime",@"ScheduleTime");
	    }
	    if (!propertyToSqlMap.Contains("ProcessedTime")) {
		propertyToSqlMap.Add("ProcessedTime",@"ProcessedTime");
	    }
	    if (!propertyToSqlMap.Contains("Priority")) {
		propertyToSqlMap.Add("Priority",@"Priority");
	    }
	    if (!propertyToSqlMap.Contains("From")) {
		propertyToSqlMap.Add("From",@"From");
	    }
	    if (!propertyToSqlMap.Contains("To")) {
		propertyToSqlMap.Add("To",@"To");
	    }
	    if (!propertyToSqlMap.Contains("Cc")) {
		propertyToSqlMap.Add("Cc",@"Cc");
	    }
	    if (!propertyToSqlMap.Contains("Bcc")) {
		propertyToSqlMap.Add("Bcc",@"Bcc");
	    }
	    if (!propertyToSqlMap.Contains("Subject")) {
		propertyToSqlMap.Add("Subject",@"Subject");
	    }
	    if (!propertyToSqlMap.Contains("BodyFormat")) {
		propertyToSqlMap.Add("BodyFormat",@"BodyFormat");
	    }
	    if (!propertyToSqlMap.Contains("Body")) {
		propertyToSqlMap.Add("Body",@"Body");
	    }
	    if (!propertyToSqlMap.Contains("MailMessageStatus")) {
		propertyToSqlMap.Add("MailMessageStatus",@"MailMessageStatus");
	    }
	    if (!propertyToSqlMap.Contains("ReleasedByUserId")) {
		propertyToSqlMap.Add("ReleasedByUserId",@"ReleasedByUserId");
	    }
	    if (!propertyToSqlMap.Contains("MailMessageType")) {
		propertyToSqlMap.Add("MailMessageType",@"MailMessageType");
	    }
	    if (!propertyToSqlMap.Contains("NumberOfAttempts")) {
		propertyToSqlMap.Add("NumberOfAttempts",@"NumberOfAttempts");
	    }
	    if (!propertyToSqlMap.Contains("MessageQueueDate")) {
		propertyToSqlMap.Add("MessageQueueDate",@"MessageQueueDate");
	    }
	}

	private MailMessageDAO() {}
	
	/// <summary>
	/// Creates a where clause object by mapping the given where clause text.  The text may reference
	/// entity properties which will be mapped to sql code by enclosing the property names in braces.
	/// </summary>
	/// <param name="whereText">Text to be mapped</param>
	/// <returns>WhereClause object.</returns>
	/// <exception cref="ApplicationException">When property name found in braces is not found in the entity.</exception>
	public static IWhere Where(String whereText) {
	    return new WhereClause(ProcessExpression(propertyToSqlMap, whereText));
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, String value) {
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, Int32 value) {
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, DateTime value)	{
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
  	    }
	}

	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList() { 
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(IWhere whereClause) { 
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(IOrderBy orderByClause) { 
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(IWhere whereClause, IOrderBy orderByClause) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); 

	    MailMessageList list = new MailMessageList(); 
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(Int32 maxRows) { 
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="maxRows"></param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(IWhere whereClause, Int32 maxRows) { 
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows"></param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(IOrderBy orderByClause, Int32 maxRows) { 
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows"></param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows); 

	    MailMessageList list = new MailMessageList();
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Finds a MailMessage entity using it's primary key.
	/// </summary>
	/// <param name="mailMessageId">A key field.</param>
	/// <returns>A MailMessage object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MailMessage Load(IdType mailMessageId) {
	    WhereClause w = new WhereClause();
	    w.And("MailMessageId", mailMessageId.IsValid ? mailMessageId.ToInt32() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for MailMessage.");
	    }
	    MailMessage data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MailMessage instance) {
	    WhereClause w = new WhereClause();
	    w.And("MailMessageId", instance.MailMessageId);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MailMessage.");
	    }
	    MailMessage data = GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(IDataReader dataReader) {
	    MailMessage data = new MailMessage(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(IDataReader dataReader, String prefix) {
	    MailMessage data = new MailMessage(false);
	    return GetDataObjectFromReader(data, dataReader, prefix);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader, String prefix) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MailMessageId"))) { 
		data.MailMessageId = IdType.UNSET;
	    } else {
		data.MailMessageId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("MailMessageId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ScheduleTime"))) { 
		data.ScheduleTime = DateTimeType.UNSET;
	    } else {
		data.ScheduleTime = new DateTimeType(dataReader.GetDateTime(dataReader.GetOrdinal("ScheduleTime")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ProcessedTime"))) { 
		data.ProcessedTime = DateTimeType.UNSET;
	    } else {
		data.ProcessedTime = new DateTimeType(dataReader.GetDateTime(dataReader.GetOrdinal("ProcessedTime")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Priority"))) { 
		data.Priority = MailPriorityEnum.UNSET;
	    } else {
		data.Priority = MailPriorityEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Priority")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("From"))) { 
		data.From = StringType.UNSET;
	    } else {
		data.From = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("From")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("To"))) { 
		data.To = StringType.UNSET;
	    } else {
		data.To = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("To")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Cc"))) { 
		data.Cc = StringType.UNSET;
	    } else {
		data.Cc = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Cc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Bcc"))) { 
		data.Bcc = StringType.UNSET;
	    } else {
		data.Bcc = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Bcc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Subject"))) { 
		data.Subject = StringType.UNSET;
	    } else {
		data.Subject = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Subject")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("BodyFormat"))) { 
		data.BodyFormat = MailBodyFormatEnum.UNSET;
	    } else {
		data.BodyFormat = MailBodyFormatEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("BodyFormat")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Body"))) { 
		data.Body = StringType.UNSET;
	    } else {
		data.Body = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Body")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MailMessageStatus"))) { 
		data.MailMessageStatus = MailMessageStatusEnum.UNSET;
	    } else {
		data.MailMessageStatus = MailMessageStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("MailMessageStatus")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ReleasedByUserId"))) { 
		data.ReleasedByUserId = IdType.UNSET;
	    } else {
		data.ReleasedByUserId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ReleasedByUserId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MailMessageType"))) { 
		data.MailMessageType = StringType.UNSET;
	    } else {
		data.MailMessageType = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("MailMessageType")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NumberOfAttempts"))) { 
		data.NumberOfAttempts = IntegerType.UNSET;
	    } else {
		data.NumberOfAttempts = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("NumberOfAttempts")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MessageQueueDate"))) { 
		data.MessageQueueDate = DateTimeType.UNSET;
	    } else {
		data.MessageQueueDate = new DateTimeType(dataReader.GetDateTime(dataReader.GetOrdinal("MessageQueueDate")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MailMessage data) {
	    return Insert(data, null);
	}
	
	/// <summary>
	/// Inserts a record into the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MailMessage data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ScheduleTime", DbType.DateTime, ParameterDirection.Input, data.ScheduleTime.IsValid ? data.ScheduleTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ProcessedTime", DbType.DateTime, ParameterDirection.Input, data.ProcessedTime.IsValid ? data.ProcessedTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Priority", DbType.AnsiString, ParameterDirection.Input, data.Priority.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@From", DbType.AnsiString, ParameterDirection.Input, data.From.IsValid ? data.From.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@To", DbType.AnsiString, ParameterDirection.Input, data.To.IsValid ? data.To.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Cc", DbType.AnsiString, ParameterDirection.Input, data.Cc.IsValid ? data.Cc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Bcc", DbType.AnsiString, ParameterDirection.Input, data.Bcc.IsValid ? data.Bcc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Subject", DbType.AnsiString, ParameterDirection.Input, data.Subject.IsValid ? data.Subject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@BodyFormat", DbType.AnsiString, ParameterDirection.Input, data.BodyFormat.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Body", DbType.AnsiString, ParameterDirection.Input, data.Body.IsValid ? data.Body.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageStatus", DbType.AnsiString, ParameterDirection.Input, data.MailMessageStatus.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@ReleasedByUserId", DbType.Int32, ParameterDirection.Input, data.ReleasedByUserId.IsValid ? data.ReleasedByUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageType", DbType.AnsiString, ParameterDirection.Input, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@NumberOfAttempts", DbType.Int32, ParameterDirection.Input, data.NumberOfAttempts.IsValid ? data.NumberOfAttempts.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MessageQueueDate", DbType.DateTime, ParameterDirection.Input, data.MessageQueueDate.IsValid ? data.MessageQueueDate.ToDateTime() as Object : DBNull.Value));

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
	/// Updates a record in the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MailMessage data) {
	    Update(data, null);
	}
	
	/// <summary>
	/// Updates a record in the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MailMessage data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, data.MailMessageId.IsValid ? data.MailMessageId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ScheduleTime", DbType.DateTime, ParameterDirection.Input, data.ScheduleTime.IsValid ? data.ScheduleTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ProcessedTime", DbType.DateTime, ParameterDirection.Input, data.ProcessedTime.IsValid ? data.ProcessedTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Priority", DbType.AnsiString, ParameterDirection.Input, data.Priority.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@From", DbType.AnsiString, ParameterDirection.Input, data.From.IsValid ? data.From.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@To", DbType.AnsiString, ParameterDirection.Input, data.To.IsValid ? data.To.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Cc", DbType.AnsiString, ParameterDirection.Input, data.Cc.IsValid ? data.Cc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Bcc", DbType.AnsiString, ParameterDirection.Input, data.Bcc.IsValid ? data.Bcc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Subject", DbType.AnsiString, ParameterDirection.Input, data.Subject.IsValid ? data.Subject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@BodyFormat", DbType.AnsiString, ParameterDirection.Input, data.BodyFormat.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Body", DbType.AnsiString, ParameterDirection.Input, data.Body.IsValid ? data.Body.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageStatus", DbType.AnsiString, ParameterDirection.Input, data.MailMessageStatus.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@ReleasedByUserId", DbType.Int32, ParameterDirection.Input, data.ReleasedByUserId.IsValid ? data.ReleasedByUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageType", DbType.AnsiString, ParameterDirection.Input, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@NumberOfAttempts", DbType.Int32, ParameterDirection.Input, data.NumberOfAttempts.IsValid ? data.NumberOfAttempts.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MessageQueueDate", DbType.DateTime, ParameterDirection.Input, data.MessageQueueDate.IsValid ? data.MessageQueueDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the MailMessage table by MailMessageId.
	/// </summary>
	/// <param name="mailMessageId"></param>
	public void Delete(IdType mailMessageId) {
	    Delete(mailMessageId, null);
	}
	
	/// <summary>
	/// Deletes a record from the MailMessage table by MailMessageId.
	/// </summary>
	/// <param name="mailMessageId"></param>
	/// <param name="transaction"></param>
	public void Delete(IdType mailMessageId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, mailMessageId.IsValid ? mailMessageId.ToInt32() as Object : DBNull.Value));

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
	/// <param name="mailMessageStatus">A field value to be matched.</param>
	/// <returns>The list of MailMessageDAO objects found.</returns>
	public MailMessageList FindByStatus(MailMessageStatusEnum mailMessageStatus) {
	    OrderByClause sort = new OrderByClause("MailMessageStatus");
	    WhereClause filter = new WhereClause();
	    filter.And("MailMessageStatus", mailMessageStatus.DBValue);

	    return GetList(filter, sort);
	}
    }
}
