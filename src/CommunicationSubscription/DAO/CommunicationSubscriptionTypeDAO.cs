using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.CommunicationSubscription.BusinessLogic;
using Spring2.Core.CommunicationSubscription.DataObject;


namespace Spring2.Core.CommunicationSubscription.Dao {

    /// <summary>
    /// Data access class for CommunicationSubscriptionType business entity.
    /// </summary>
    public class CommunicationSubscriptionTypeDAO : Spring2.Core.DAO.SqlEntityDAO, ICommunicationSubscriptionTypeDAO {
	private static ICommunicationSubscriptionTypeDAO instance = new CommunicationSubscriptionTypeDAO();
	public static ICommunicationSubscriptionTypeDAO DAO{
	    get{ return instance; }
	}
	
	/// <summary>
	/// Sets the singleton DAO instance of ICommunicationSubscriptionTypeDAO
	/// </summary>
	public void SetInstance(ICommunicationSubscriptionTypeDAO dao) {
	    if(dao != null){
			instance = dao;
	    }else{
			instance = new CommunicationSubscriptionTypeDAO();
	    }
	}
	
	private static readonly String VIEW = "vwCommunicationSubscriptionType";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	public sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 CommunicationSubscriptionTypeId;
	    public Int32 Name;
	    public Int32 EmailSubject;
	    public Int32 EmailBody;
	    public Int32 EmailBodyType;
	    public Int32 MailMessageType;
	    public Int32 LastSentDate;
	    public Int32 FrequencyInMinutes;
	    public Int32 ProviderName;
	    public Int32 AllowSubscription;
	    public Int32 AutoSubscribe;
	    public Int32 EffectiveDate;
	    public Int32 ExpirationDate;
	    public Int32 CreateDate;
	    public Int32 CreateUserId;
	    public Int32 LastModifiedDate;
	    public Int32 LastModifiedUserId;

	    internal ColumnOrdinals(IDataReader reader) {
		CommunicationSubscriptionTypeId = reader.GetOrdinal("CommunicationSubscriptionTypeId");
		Name = reader.GetOrdinal("Name");
		EmailSubject = reader.GetOrdinal("EmailSubject");
		EmailBody = reader.GetOrdinal("EmailBody");
		EmailBodyType = reader.GetOrdinal("EmailBodyType");
		MailMessageType = reader.GetOrdinal("MailMessageType");
		LastSentDate = reader.GetOrdinal("LastSentDate");
		FrequencyInMinutes = reader.GetOrdinal("FrequencyInMinutes");
		ProviderName = reader.GetOrdinal("ProviderName");
		AllowSubscription = reader.GetOrdinal("AllowSubscription");
		AutoSubscribe = reader.GetOrdinal("AutoSubscribe");
		EffectiveDate = reader.GetOrdinal("EffectiveDate");
		ExpirationDate = reader.GetOrdinal("ExpirationDate");
		CreateDate = reader.GetOrdinal("CreateDate");
		CreateUserId = reader.GetOrdinal("CreateUserId");
		LastModifiedDate = reader.GetOrdinal("LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal("LastModifiedUserId");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		CommunicationSubscriptionTypeId = reader.GetOrdinal(prefix + "CommunicationSubscriptionTypeId");
		Name = reader.GetOrdinal(prefix + "Name");
		EmailSubject = reader.GetOrdinal(prefix + "EmailSubject");
		EmailBody = reader.GetOrdinal(prefix + "EmailBody");
		EmailBodyType = reader.GetOrdinal(prefix + "EmailBodyType");
		MailMessageType = reader.GetOrdinal(prefix + "MailMessageType");
		LastSentDate = reader.GetOrdinal(prefix + "LastSentDate");
		FrequencyInMinutes = reader.GetOrdinal(prefix + "FrequencyInMinutes");
		ProviderName = reader.GetOrdinal(prefix + "ProviderName");
		AllowSubscription = reader.GetOrdinal(prefix + "AllowSubscription");
		AutoSubscribe = reader.GetOrdinal(prefix + "AutoSubscribe");
		EffectiveDate = reader.GetOrdinal(prefix + "EffectiveDate");
		ExpirationDate = reader.GetOrdinal(prefix + "ExpirationDate");
		CreateDate = reader.GetOrdinal(prefix + "CreateDate");
		CreateUserId = reader.GetOrdinal(prefix + "CreateUserId");
		LastModifiedDate = reader.GetOrdinal(prefix + "LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal(prefix + "LastModifiedUserId");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static CommunicationSubscriptionTypeDAO() {
	    AddPropertyMapping("CommunicationSubscriptionTypeId", @"CommunicationSubscriptionTypeId");
	    AddPropertyMapping("Name", @"Name");
	    AddPropertyMapping("EmailSubject", @"EmailSubject");
	    AddPropertyMapping("EmailBody", @"EmailBody");
	    AddPropertyMapping("EmailBodyType", @"EmailBodyType");
	    AddPropertyMapping("MailMessageType", @"MailMessageType");
	    AddPropertyMapping("LastSentDate", @"LastSentDate");
	    AddPropertyMapping("FrequencyInMinutes", @"FrequencyInMinutes");
	    AddPropertyMapping("ProviderName", @"ProviderName");
	    AddPropertyMapping("AllowSubscription", @"AllowSubscription");
	    AddPropertyMapping("AutoSubscribe", @"AutoSubscribe");
	    AddPropertyMapping("EffectiveDate", @"EffectiveDate");
	    AddPropertyMapping("ExpirationDate", @"ExpirationDate");
	    AddPropertyMapping("CreateDate", @"CreateDate");
	    AddPropertyMapping("CreateUserId", @"CreateUserId");
	    AddPropertyMapping("LastModifiedUserId", @"LastModifiedUserId");
	    AddPropertyMapping("LastModifiedDate", @"LastModifiedDate");
	}

	private CommunicationSubscriptionTypeDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionType rows.
	/// </summary>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTypeList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTypeList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTypeList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTypeList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    CommunicationSubscriptionTypeList list = new CommunicationSubscriptionTypeList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTypeList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTypeList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTypeList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTypeList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    CommunicationSubscriptionTypeList list = new CommunicationSubscriptionTypeList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a CommunicationSubscriptionType entity using it's primary key.
	/// </summary>
	/// <param name="communicationSubscriptionTypeId">A key field.</param>
	/// <returns>A CommunicationSubscriptionType object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public CommunicationSubscriptionType Load(IdType communicationSubscriptionTypeId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CommunicationSubscriptionTypeFields.COMMUNICATIONSUBSCRIPTIONTYPEID, EqualityOperatorEnum.Equal, communicationSubscriptionTypeId.IsValid ? communicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(CommunicationSubscriptionType instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CommunicationSubscriptionTypeFields.COMMUNICATIONSUBSCRIPTIONTYPEID, EqualityOperatorEnum.Equal, instance.CommunicationSubscriptionTypeId.IsValid ? instance.CommunicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for CommunicationSubscriptionType.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private CommunicationSubscriptionTypeList GetList(IDataReader reader) {
	    CommunicationSubscriptionTypeList list = new CommunicationSubscriptionTypeList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CommunicationSubscriptionType GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CommunicationSubscriptionType GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    CommunicationSubscriptionType data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionType GetDataObjectFromReader(CommunicationSubscriptionType data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionType GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    CommunicationSubscriptionType data = new CommunicationSubscriptionType(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionType GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    CommunicationSubscriptionType data = new CommunicationSubscriptionType(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionType GetDataObjectFromReader(CommunicationSubscriptionType data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.CommunicationSubscriptionTypeId)) {
		data.CommunicationSubscriptionTypeId = IdType.UNSET;
	    } else {
		data.CommunicationSubscriptionTypeId = new IdType(dataReader.GetInt32(ordinals.CommunicationSubscriptionTypeId));
	    }
	    if (dataReader.IsDBNull(ordinals.Name)) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(ordinals.Name));
	    }
	    if (dataReader.IsDBNull(ordinals.EmailSubject)) {
		data.EmailSubject = StringType.UNSET;
	    } else {
		data.EmailSubject = StringType.Parse(dataReader.GetString(ordinals.EmailSubject));
	    }
	    if (dataReader.IsDBNull(ordinals.EmailBody)) {
		data.EmailBody = StringType.UNSET;
	    } else {
		data.EmailBody = StringType.Parse(dataReader.GetString(ordinals.EmailBody));
	    }
	    if (dataReader.IsDBNull(ordinals.EmailBodyType)) {
		data.EmailBodyType = StringType.UNSET;
	    } else {
		data.EmailBodyType = StringType.Parse(dataReader.GetString(ordinals.EmailBodyType));
	    }
	    if (dataReader.IsDBNull(ordinals.MailMessageType)) {
		data.MailMessageType = StringType.UNSET;
	    } else {
		data.MailMessageType = StringType.Parse(dataReader.GetString(ordinals.MailMessageType));
	    }
	    if (dataReader.IsDBNull(ordinals.LastSentDate)) {
		data.LastSentDate = DateTimeType.UNSET;
	    } else {
		data.LastSentDate = new DateTimeType(dataReader.GetDateTime(ordinals.LastSentDate));
	    }
	    if (dataReader.IsDBNull(ordinals.FrequencyInMinutes)) {
		data.FrequencyInMinutes = IntegerType.UNSET;
	    } else {
		data.FrequencyInMinutes = new IntegerType(dataReader.GetInt32(ordinals.FrequencyInMinutes));
	    }
	    if (dataReader.IsDBNull(ordinals.ProviderName)) {
		data.ProviderName = StringType.UNSET;
	    } else {
		data.ProviderName = StringType.Parse(dataReader.GetString(ordinals.ProviderName));
	    }
	    if (dataReader.IsDBNull(ordinals.AllowSubscription)) {
		data.AllowSubscription = BooleanType.UNSET;
	    } else {
		data.AllowSubscription = BooleanType.GetInstance(dataReader.GetBoolean(ordinals.AllowSubscription));
	    }
	    if (dataReader.IsDBNull(ordinals.AutoSubscribe)) {
		data.AutoSubscribe = BooleanType.UNSET;
	    } else {
		data.AutoSubscribe = BooleanType.GetInstance(dataReader.GetBoolean(ordinals.AutoSubscribe));
	    }
	    if (dataReader.IsDBNull(ordinals.EffectiveDate)) {
		data.EffectiveDate = DateTimeType.UNSET;
	    } else {
		data.EffectiveDate = new DateTimeType(dataReader.GetDateTime(ordinals.EffectiveDate));
	    }
	    if (dataReader.IsDBNull(ordinals.ExpirationDate)) {
		data.ExpirationDate = DateTimeType.UNSET;
	    } else {
		data.ExpirationDate = new DateTimeType(dataReader.GetDateTime(ordinals.ExpirationDate));
	    }
	    if (dataReader.IsDBNull(ordinals.CreateDate)) {
		data.CreateDate = DateTimeType.UNSET;
	    } else {
		data.CreateDate = new DateTimeType(dataReader.GetDateTime(ordinals.CreateDate));
	    }
	    if (dataReader.IsDBNull(ordinals.CreateUserId)) {
		data.CreateUserId = IdType.UNSET;
	    } else {
		data.CreateUserId = new IdType(dataReader.GetInt32(ordinals.CreateUserId));
	    }
	    if (dataReader.IsDBNull(ordinals.LastModifiedUserId)) {
		data.LastModifiedUserId = IdType.UNSET;
	    } else {
		data.LastModifiedUserId = new IdType(dataReader.GetInt32(ordinals.LastModifiedUserId));
	    }
	    if (dataReader.IsDBNull(ordinals.LastModifiedDate)) {
		data.LastModifiedDate = DateTimeType.UNSET;
	    } else {
		data.LastModifiedDate = new DateTimeType(dataReader.GetDateTime(ordinals.LastModifiedDate));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(CommunicationSubscriptionType data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(CommunicationSubscriptionType data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionType_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.NAME, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILSUBJECT, data.EmailSubject.IsValid ? data.EmailSubject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILBODY, data.EmailBody.IsValid ? data.EmailBody.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILBODYTYPE, data.EmailBodyType.IsValid ? data.EmailBodyType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.MAILMESSAGETYPE, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTSENTDATE, data.LastSentDate.IsValid ? data.LastSentDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.FREQUENCYINMINUTES, data.FrequencyInMinutes.IsValid ? data.FrequencyInMinutes.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.PROVIDERNAME, data.ProviderName.IsValid ? data.ProviderName.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.ALLOWSUBSCRIPTION, data.AllowSubscription.IsValid ? data.AllowSubscription.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.AUTOSUBSCRIBE, data.AutoSubscribe.IsValid ? data.AutoSubscribe.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EFFECTIVEDATE, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EXPIRATIONDATE, data.ExpirationDate.IsValid ? data.ExpirationDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}


	/// <summary>
	/// Updates a record in the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(CommunicationSubscriptionType data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(CommunicationSubscriptionType data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionType_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.COMMUNICATIONSUBSCRIPTIONTYPEID, data.CommunicationSubscriptionTypeId.IsValid ? data.CommunicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.NAME, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILSUBJECT, data.EmailSubject.IsValid ? data.EmailSubject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILBODY, data.EmailBody.IsValid ? data.EmailBody.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EMAILBODYTYPE, data.EmailBodyType.IsValid ? data.EmailBodyType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.MAILMESSAGETYPE, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTSENTDATE, data.LastSentDate.IsValid ? data.LastSentDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.FREQUENCYINMINUTES, data.FrequencyInMinutes.IsValid ? data.FrequencyInMinutes.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.PROVIDERNAME, data.ProviderName.IsValid ? data.ProviderName.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.ALLOWSUBSCRIPTION, data.AllowSubscription.IsValid ? data.AllowSubscription.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.AUTOSUBSCRIBE, data.AutoSubscribe.IsValid ? data.AutoSubscribe.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EFFECTIVEDATE, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.EXPIRATIONDATE, data.ExpirationDate.IsValid ? data.ExpirationDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionType table by CommunicationSubscriptionTypeId.
	/// </summary>
	/// <param name="communicationSubscriptionTypeId">A key field.</param>
	public void Delete(IdType communicationSubscriptionTypeId) {
	    Delete(communicationSubscriptionTypeId, null);
	}

	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionType table by CommunicationSubscriptionTypeId.
	/// </summary>
	/// <param name="communicationSubscriptionTypeId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType communicationSubscriptionTypeId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionType_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTypeFields.COMMUNICATIONSUBSCRIPTIONTYPEID, communicationSubscriptionTypeId.IsValid ? communicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}
    }
}
