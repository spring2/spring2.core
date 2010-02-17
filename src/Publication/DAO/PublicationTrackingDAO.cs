using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Publication.DataObject;


namespace Spring2.Core.Publication.Dao {

    /// <summary>
    /// Data access class for PublicationTracking business entity.
    /// </summary>
    public class PublicationTrackingDAO : Spring2.Core.DAO.SqlEntityDAO, IPublicationTrackingDAO {
	private static IPublicationTrackingDAO instance = new PublicationTrackingDAO();
	public static IPublicationTrackingDAO DAO{
	    get{ return instance; }
	}
	
	/// <summary>
	/// Sets the singleton DAO instance of IPublicationTrackingDAO
	/// </summary>
	public void SetInstance(IPublicationTrackingDAO dao) {
	    if(dao != null){
			instance = dao;
	    }else{
			instance = new PublicationTrackingDAO();
	    }
	}
	
	private static readonly String VIEW = "vwPublicationTracking";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	public sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 PublicationTrackingId;
	    public Int32 PublicationPrimaryKeyId;
	    public Int32 PublicationTypeId;
	    public Int32 CreateDate;
	    public Int32 CreateUserId;
	    public Int32 LastModifiedDate;
	    public Int32 LastModifiedUserId;

	    internal ColumnOrdinals(IDataReader reader) {
		PublicationTrackingId = reader.GetOrdinal("PublicationTrackingId");
		PublicationPrimaryKeyId = reader.GetOrdinal("PublicationPrimaryKeyId");
		PublicationTypeId = reader.GetOrdinal("PublicationTypeId");
		CreateDate = reader.GetOrdinal("CreateDate");
		CreateUserId = reader.GetOrdinal("CreateUserId");
		LastModifiedDate = reader.GetOrdinal("LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal("LastModifiedUserId");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		PublicationTrackingId = reader.GetOrdinal(prefix + "PublicationTrackingId");
		PublicationPrimaryKeyId = reader.GetOrdinal(prefix + "PublicationPrimaryKeyId");
		PublicationTypeId = reader.GetOrdinal(prefix + "PublicationTypeId");
		CreateDate = reader.GetOrdinal(prefix + "CreateDate");
		CreateUserId = reader.GetOrdinal(prefix + "CreateUserId");
		LastModifiedDate = reader.GetOrdinal(prefix + "LastModifiedDate");
		LastModifiedUserId = reader.GetOrdinal(prefix + "LastModifiedUserId");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static PublicationTrackingDAO() {
	    AddPropertyMapping("PublicationTrackingId", @"PublicationTrackingId");
	    AddPropertyMapping("PublicationPrimaryKeyId", @"PublicationPrimaryKeyId");
	    AddPropertyMapping("PublicationTypeId", @"PublicationTypeId");
	    AddPropertyMapping("CreateDate", @"CreateDate");
	    AddPropertyMapping("CreateUserId", @"CreateUserId");
	    AddPropertyMapping("LastModifiedUserId", @"LastModifiedUserId");
	    AddPropertyMapping("LastModifiedDate", @"LastModifiedDate");
	}

	private PublicationTrackingDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all PublicationTracking rows.
	/// </summary>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public PublicationTrackingList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public PublicationTrackingList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of PublicationTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public PublicationTrackingList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public PublicationTrackingList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    PublicationTrackingList list = new PublicationTrackingList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all PublicationTracking rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public PublicationTrackingList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public PublicationTrackingList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of PublicationTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public PublicationTrackingList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public PublicationTrackingList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    PublicationTrackingList list = new PublicationTrackingList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a PublicationTracking entity using it's primary key.
	/// </summary>
	/// <param name="publicationTrackingId">A key field.</param>
	/// <returns>A PublicationTracking object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public PublicationTracking Load(IdType publicationTrackingId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(PublicationTrackingFields.PUBLICATIONTRACKINGID, EqualityOperatorEnum.Equal, publicationTrackingId.IsValid ? publicationTrackingId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(PublicationTracking instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(PublicationTrackingFields.PUBLICATIONTRACKINGID, EqualityOperatorEnum.Equal, instance.PublicationTrackingId.IsValid ? instance.PublicationTrackingId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for PublicationTracking.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private PublicationTrackingList GetList(IDataReader reader) {
	    PublicationTrackingList list = new PublicationTrackingList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private PublicationTracking GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private PublicationTracking GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    PublicationTracking data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	public PublicationTracking GetDataObjectFromReader(PublicationTracking data, IDataReader dataReader) {
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
	public PublicationTracking GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    PublicationTracking data = new PublicationTracking(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public PublicationTracking GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    PublicationTracking data = new PublicationTracking(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public PublicationTracking GetDataObjectFromReader(PublicationTracking data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.PublicationTrackingId)) {
		data.PublicationTrackingId = IdType.UNSET;
	    } else {
		data.PublicationTrackingId = new IdType(dataReader.GetInt32(ordinals.PublicationTrackingId));
	    }
	    if (dataReader.IsDBNull(ordinals.PublicationPrimaryKeyId)) {
		data.PublicationPrimaryKeyId = IdType.UNSET;
	    } else {
		data.PublicationPrimaryKeyId = new IdType(dataReader.GetInt32(ordinals.PublicationPrimaryKeyId));
	    }
	    if (dataReader.IsDBNull(ordinals.PublicationTypeId)) {
		data.PublicationTypeId = IdType.UNSET;
	    } else {
		data.PublicationTypeId = new IdType(dataReader.GetInt32(ordinals.PublicationTypeId));
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
	/// Inserts a record into the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(PublicationTracking data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(PublicationTracking data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPublicationTracking_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONPRIMARYKEYID, data.PublicationPrimaryKeyId.IsValid ? data.PublicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONTYPEID, data.PublicationTypeId.IsValid ? data.PublicationTypeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

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
	/// Updates a record in the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(PublicationTracking data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(PublicationTracking data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPublicationTracking_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONTRACKINGID, data.PublicationTrackingId.IsValid ? data.PublicationTrackingId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONPRIMARYKEYID, data.PublicationPrimaryKeyId.IsValid ? data.PublicationPrimaryKeyId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONTYPEID, data.PublicationTypeId.IsValid ? data.PublicationTypeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.CREATEDATE, data.CreateDate.IsValid ? data.CreateDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.CREATEUSERID, data.CreateUserId.IsValid ? data.CreateUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.LASTMODIFIEDUSERID, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.LASTMODIFIEDDATE, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the PublicationTracking table by PublicationTrackingId.
	/// </summary>
	/// <param name="publicationTrackingId">A key field.</param>
	public void Delete(IdType publicationTrackingId) {
	    Delete(publicationTrackingId, null);
	}

	/// <summary>
	/// Deletes a record from the PublicationTracking table by PublicationTrackingId.
	/// </summary>
	/// <param name="publicationTrackingId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType publicationTrackingId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPublicationTracking_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter(PublicationTrackingFields.PUBLICATIONTRACKINGID, publicationTrackingId.IsValid ? publicationTrackingId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}
    }
}
