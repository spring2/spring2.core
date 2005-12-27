using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;

namespace Spring2.Core.ResourceManager.Dao {
    public class LocalizedResourceDAO : Spring2.Core.DAO.SqlEntityDAO {


	public static readonly LocalizedResourceDAO DAO = new LocalizedResourceDAO(); 
	private static readonly String VIEW = "vwLocalizedResource";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static Hashtable propertyToSqlMap = new Hashtable();

	static LocalizedResourceDAO() {
	    if (!propertyToSqlMap.Contains("LocalizedResourceId")) {
		propertyToSqlMap.Add("LocalizedResourceId",@"LocalizedResourceId");
	    }
	    if (!propertyToSqlMap.Contains("ResourceId")) {
		propertyToSqlMap.Add("ResourceId",@"ResourceId");
	    }
	    if (!propertyToSqlMap.Contains("Locale")) {
		propertyToSqlMap.Add("Locale",@"Locale");
	    }
	    if (!propertyToSqlMap.Contains("Language")) {
		propertyToSqlMap.Add("Language",@"Language");
	    }
	    if (!propertyToSqlMap.Contains("Content")) {
		propertyToSqlMap.Add("Content",@"Content");
	    }
	}

	private LocalizedResourceDAO() {}
	
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
	/// Returns a list of all LocalizedResource rows.
	/// </summary>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList() { 
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(IWhere whereClause) { 
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of LocalizedResource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(IOrderBy orderByClause) { 
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(IWhere whereClause, IOrderBy orderByClause) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); 

	    LocalizedResourceList list = new LocalizedResourceList(); 
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Returns a list of all LocalizedResource rows.
	/// </summary>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(Int32 maxRows) { 
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(IWhere whereClause, Int32 maxRows) { 
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of LocalizedResource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(IOrderBy orderByClause, Int32 maxRows) { 
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows); 

	    LocalizedResourceList list = new LocalizedResourceList();
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Finds a LocalizedResource entity using it's primary key.
	/// </summary>
	/// <param name="LocalizedResourceId">A key field.</param>
	/// <returns>A LocalizedResource object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public LocalizedResource Load(IdType localizedResourceId) {
	    WhereClause w = new WhereClause();
	    w.And("LocalizedResourceId", localizedResourceId.IsValid ? localizedResourceId.ToInt32() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for LocalizedResource.");
	    }
	    LocalizedResource data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(LocalizedResource instance) {
	    WhereClause w = new WhereClause();
	    w.And("LocalizedResourceId", instance.LocalizedResourceId);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for LocalizedResource.");
	    }
	    LocalizedResource data = GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static LocalizedResource GetDataObjectFromReader(LocalizedResource data, IDataReader dataReader) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static LocalizedResource GetDataObjectFromReader(IDataReader dataReader) {
	    LocalizedResource data = new LocalizedResource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static LocalizedResource GetDataObjectFromReader(IDataReader dataReader, String prefix) {
	    LocalizedResource data = new LocalizedResource(false);
	    return GetDataObjectFromReader(data, dataReader, prefix);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static LocalizedResource GetDataObjectFromReader(LocalizedResource data, IDataReader dataReader, String prefix) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalizedResourceId"))) { 
		data.LocalizedResourceId = IdType.UNSET;
	    } else {
		data.LocalizedResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("LocalizedResourceId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ResourceId"))) { 
		data.ResourceId = IdType.UNSET;
	    } else {
		data.ResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ResourceId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Locale"))) { 
		data.Locale = StringType.UNSET;
	    } else {
		data.Locale = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Locale")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Language"))) { 
		data.Language = StringType.UNSET;
	    } else {
		data.Language = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Language")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Content"))) { 
		data.Content = StringType.UNSET;
	    } else {
		data.Content = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Content")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the LocalizedResource table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(LocalizedResource data) {
	    return Insert(data, null);
	}
	
	/// <summary>
	/// Inserts a record into the LocalizedResource table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(LocalizedResource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Locale", DbType.AnsiString, ParameterDirection.Input, data.Locale.IsValid ? data.Locale.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Language", DbType.AnsiString, ParameterDirection.Input, data.Language.IsValid ? data.Language.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Content", DbType.AnsiString, ParameterDirection.Input, data.Content.IsValid ? data.Content.ToString() as Object : DBNull.Value));

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
	/// Updates a record in the LocalizedResource table.
	/// </summary>
	/// <param name=""></param>
	public void Update(LocalizedResource data) {
	    Update(data, null);
	}
	
	/// <summary>
	/// Updates a record in the LocalizedResource table.
	/// </summary>
	/// <param name=""></param>
	public void Update(LocalizedResource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@LocalizedResourceId", DbType.Int32, ParameterDirection.Input, data.LocalizedResourceId.IsValid ? data.LocalizedResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Locale", DbType.AnsiString, ParameterDirection.Input, data.Locale.IsValid ? data.Locale.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Language", DbType.AnsiString, ParameterDirection.Input, data.Language.IsValid ? data.Language.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Content", DbType.AnsiString, ParameterDirection.Input, data.Content.IsValid ? data.Content.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the LocalizedResource table by LocalizedResourceId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType localizedResourceId) {
	    Delete(localizedResourceId, null);
	}
	
	/// <summary>
	/// Deletes a record from the LocalizedResource table by LocalizedResourceId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType localizedResourceId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@LocalizedResourceId", DbType.Int32, ParameterDirection.Input, localizedResourceId.IsValid ? localizedResourceId.ToInt32() as Object : DBNull.Value));

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="ResourceId">A field value to be matched.</param>
	/// <param name="Locale">A field value to be matched.</param>
	/// <param name="Language">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResource FindByResourceIdAndLocaleAndLanguage(IdType resourceId, StringType locale, StringType language) {
	    OrderByClause sort = new OrderByClause("ResourceId, Locale, Language");
	    WhereClause filter = new WhereClause();
	    filter.And("ResourceId", resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value);
	    filter.And("Locale", locale.IsValid ? locale.ToString() as Object : DBNull.Value);
	    filter.And("Language", language.IsValid ? language.ToString() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("LocalizedResource.FindResourceIdAndLocaleAndLanguage found no rows.");
	    }
	    LocalizedResource data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}
    }
}
