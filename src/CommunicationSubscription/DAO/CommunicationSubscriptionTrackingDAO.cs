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
    /// Data access class for CommunicationSubscriptionTracking business entity.
    /// </summary>
    public class CommunicationSubscriptionTrackingDAO : Spring2.Core.DAO.SqlEntityDAO, ICommunicationSubscriptionTrackingDAO {
	private static ICommunicationSubscriptionTrackingDAO instance = new CommunicationSubscriptionTrackingDAO();
	public static ICommunicationSubscriptionTrackingDAO DAO{
	    get{ return instance; }
	}
	
	/// <summary>
	/// Sets the singleton DAO instance of ICommunicationSubscriptionTrackingDAO
	/// </summary>
	public void SetInstance(ICommunicationSubscriptionTrackingDAO dao) {
	    if(dao != null){
			instance = dao;
	    }else{
			instance = new CommunicationSubscriptionTrackingDAO();
	    }
	}
	
	private static readonly String VIEW = "vwCommunicationSubscriptionTracking";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	public sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 CommunicationPrimaryKeyId;
	    public Int32 CommunicationSubscriptionTypeId;
	    public Int32 CreateDate;
	    public Int32 CreateUserId;
	    public Int32 LastModifiedDate;
	    public Int32 LastModifiedUserId;

	    internal ColumnOrdinals(IDataReader reader) {
		CommunicationPrimaryKeyId = reader.GetOrdinal("CommunicationPrimaryKeyId");
		CommunicationSubscriptionTypeId = reader.GetOrdinal("CommunicationSubscriptionTypeId");
		CreateDate = reader.GetOrdinal("CreateDate");
		CreateUserId = reader.GetOrdinal("CreateUserId");
		LastModifiedDate = reader.GetOrdinal("LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal("LastModifiedUserId");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		CommunicationPrimaryKeyId = reader.GetOrdinal(prefix + "CommunicationPrimaryKeyId");
		CommunicationSubscriptionTypeId = reader.GetOrdinal(prefix + "CommunicationSubscriptionTypeId");
		CreateDate = reader.GetOrdinal(prefix + "CreateDate");
		CreateUserId = reader.GetOrdinal(prefix + "CreateUserId");
		LastModifiedDate = reader.GetOrdinal(prefix + "LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal(prefix + "LastModifiedUserId");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static CommunicationSubscriptionTrackingDAO() {
	    AddPropertyMapping("CommunicationPrimaryKeyId", @"CommunicationPrimaryKeyId");
	    AddPropertyMapping("CommunicationSubscriptionTypeId", @"CommunicationSubscriptionTypeId");
	    AddPropertyMapping("CreateDate", @"CreateDate");
	    AddPropertyMapping("CreateUserId", @"CreateUserId");
	    AddPropertyMapping("LastModifiedUserId", @"LastModifiedUserId");
	    AddPropertyMapping("LastModifiedDate", @"LastModifiedDate");
	}

	private CommunicationSubscriptionTrackingDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTrackingList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTrackingList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTrackingList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTrackingList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    CommunicationSubscriptionTrackingList list = new CommunicationSubscriptionTrackingList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTrackingList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTrackingList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CommunicationSubscriptionTrackingList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CommunicationSubscriptionTrackingList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    CommunicationSubscriptionTrackingList list = new CommunicationSubscriptionTrackingList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a CommunicationSubscriptionTracking entity using it's primary key.
	/// </summary>
	/// <param name="communicationPrimaryKeyId">A key field.</param>
	/// <returns>A CommunicationSubscriptionTracking object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public CommunicationSubscriptionTracking Load(IdType communicationPrimaryKeyId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CommunicationSubscriptionTrackingFields.COMMUNICATIONPRIMARYKEYID, EqualityOperatorEnum.Equal, communicationPrimaryKeyId.IsValid ? communicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(CommunicationSubscriptionTracking instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CommunicationSubscriptionTrackingFields.COMMUNICATIONPRIMARYKEYID, EqualityOperatorEnum.Equal, instance.CommunicationPrimaryKeyId.IsValid ? instance.CommunicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for CommunicationSubscriptionTracking.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private CommunicationSubscriptionTrackingList GetList(IDataReader reader) {
	    CommunicationSubscriptionTrackingList list = new CommunicationSubscriptionTrackingList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CommunicationSubscriptionTracking GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CommunicationSubscriptionTracking GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    CommunicationSubscriptionTracking data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionTracking GetDataObjectFromReader(CommunicationSubscriptionTracking data, IDataReader dataReader) {
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
	public CommunicationSubscriptionTracking GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    CommunicationSubscriptionTracking data = new CommunicationSubscriptionTracking(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionTracking GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    CommunicationSubscriptionTracking data = new CommunicationSubscriptionTracking(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CommunicationSubscriptionTracking GetDataObjectFromReader(CommunicationSubscriptionTracking data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.CommunicationPrimaryKeyId)) {
		data.CommunicationPrimaryKeyId = IdType.UNSET;
	    } else {
		data.CommunicationPrimaryKeyId = new IdType(dataReader.GetInt32(ordinals.CommunicationPrimaryKeyId));
	    }
	    if (dataReader.IsDBNull(ordinals.CommunicationSubscriptionTypeId)) {
		data.CommunicationSubscriptionTypeId = IdType.UNSET;
	    } else {
		data.CommunicationSubscriptionTypeId = new IdType(dataReader.GetInt32(ordinals.CommunicationSubscriptionTypeId));
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
	/// Inserts a record into the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(CommunicationSubscriptionTracking data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(CommunicationSubscriptionTracking data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionTracking_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.COMMUNICATIONSUBSCRIPTIONTYPEID, data.CommunicationSubscriptionTypeId.IsValid ? data.CommunicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

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
	/// Updates a record in the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(CommunicationSubscriptionTracking data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(CommunicationSubscriptionTracking data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionTracking_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.COMMUNICATIONPRIMARYKEYID, data.CommunicationPrimaryKeyId.IsValid ? data.CommunicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.COMMUNICATIONSUBSCRIPTIONTYPEID, data.CommunicationSubscriptionTypeId.IsValid ? data.CommunicationSubscriptionTypeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionTracking table by CommunicationPrimaryKeyId.
	/// </summary>
	/// <param name="communicationPrimaryKeyId">A key field.</param>
	public void Delete(IdType communicationPrimaryKeyId) {
	    Delete(communicationPrimaryKeyId, null);
	}

	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionTracking table by CommunicationPrimaryKeyId.
	/// </summary>
	/// <param name="communicationPrimaryKeyId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType communicationPrimaryKeyId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCommunicationSubscriptionTracking_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter(CommunicationSubscriptionTrackingFields.COMMUNICATIONPRIMARYKEYID, communicationPrimaryKeyId.IsValid ? communicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}
    }
}
