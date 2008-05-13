using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

namespace Spring2.Core.Configuration {
    public class ConfigurationSettingDAO : Spring2.Core.DAO.SqlEntityDAO {


	public static readonly ConfigurationSettingDAO DAO = new ConfigurationSettingDAO(); 
	private static readonly String VIEW = "vwConfigurationSetting";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static Hashtable propertyToSqlMap = new Hashtable();

	static ConfigurationSettingDAO() {
	    if (!propertyToSqlMap.Contains("ConfigurationSettingId")) {
		propertyToSqlMap.Add("ConfigurationSettingId",@"ConfigurationSettingId");
	    }
	    if (!propertyToSqlMap.Contains("Key")) {
		propertyToSqlMap.Add("Key",@"Key");
	    }
	    if (!propertyToSqlMap.Contains("Value")) {
		propertyToSqlMap.Add("Value",@"Value");
	    }
	    if (!propertyToSqlMap.Contains("LastModifiedDate")) {
		propertyToSqlMap.Add("LastModifiedDate",@"LastModifiedDate");
	    }
	    if (!propertyToSqlMap.Contains("LastModifiedUserId")) {
		propertyToSqlMap.Add("LastModifiedUserId",@"LastModifiedUserId");
	    }
            if (!propertyToSqlMap.Contains("EffectiveDate")) {
                propertyToSqlMap.Add("EffectiveDate", @"EffectiveDate");
            }
        }

	private ConfigurationSettingDAO() {}
	
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
	/// Returns a list of all ConfigurationSetting rows.
	/// </summary>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ConfigurationSettingList GetList() { 
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of ConfigurationSetting rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ConfigurationSettingList GetList(IWhere whereClause) { 
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of ConfigurationSetting rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ConfigurationSettingList GetList(IOrderBy orderByClause) { 
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of ConfigurationSetting rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ConfigurationSettingList GetList(IWhere whereClause, IOrderBy orderByClause) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); 

	    ConfigurationSettingList list = new ConfigurationSettingList(); 
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Returns a list of all ConfigurationSetting rows.
	/// </summary>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ConfigurationSettingList GetList(Int32 maxRows) { 
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of ConfigurationSetting rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="maxRows">Number of rows to return</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ConfigurationSettingList GetList(IWhere whereClause, Int32 maxRows) { 
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of ConfigurationSetting rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Number of rows to return</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ConfigurationSettingList GetList(IOrderBy orderByClause, Int32 maxRows) { 
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of ConfigurationSetting rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Number of rows to return</param>
	/// <returns>List of ConfigurationSetting objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ConfigurationSettingList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows); 

	    ConfigurationSettingList list = new ConfigurationSettingList();
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Finds a ConfigurationSetting entity using it's primary key.
	/// </summary>
	/// <param name="configurationSettingId">A key field.</param>
	/// <returns>A ConfigurationSetting object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public ConfigurationSetting Load(IdType configurationSettingId) {
	    WhereClause w = new WhereClause();
	    w.And("ConfigurationSettingId", configurationSettingId.IsValid ? configurationSettingId.ToInt32() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for ConfigurationSetting.");
	    }
	    ConfigurationSetting data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(ConfigurationSetting instance) {
	    WhereClause w = new WhereClause();
	    w.And("ConfigurationSettingId", instance.ConfigurationSettingId);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for ConfigurationSetting.");
	    }
	    ConfigurationSetting data = GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static ConfigurationSetting GetDataObjectFromReader(ConfigurationSetting data, IDataReader dataReader) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static ConfigurationSetting GetDataObjectFromReader(IDataReader dataReader) {
	    ConfigurationSetting data = new ConfigurationSetting(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <returns>Data object built from current row.</returns>
	internal static ConfigurationSetting GetDataObjectFromReader(IDataReader dataReader, String prefix) {
	    ConfigurationSetting data = new ConfigurationSetting(false);
	    return GetDataObjectFromReader(data, dataReader, prefix);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <returns>Data object built from current row.</returns>
	internal static ConfigurationSetting GetDataObjectFromReader(ConfigurationSetting data, IDataReader dataReader, String prefix) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ConfigurationSettingId"))) { 
		data.ConfigurationSettingId = IdType.UNSET;
	    } else {
		data.ConfigurationSettingId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ConfigurationSettingId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Key"))) { 
		data.Key = StringType.UNSET;
	    } else {
		data.Key = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Key")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Value"))) { 
		data.Value = StringType.UNSET;
	    } else {
		data.Value = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Value")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedDate"))) { 
		data.LastModifiedDate = DateTimeType.UNSET;
	    } else {
		data.LastModifiedDate = new DateTimeType(dataReader.GetDateTime(dataReader.GetOrdinal("LastModifiedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedUserId"))) { 
		data.LastModifiedUserId = IdType.UNSET;
	    } else {
		data.LastModifiedUserId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("LastModifiedUserId")));
	    }
            if (dataReader.IsDBNull(dataReader.GetOrdinal("EffectiveDate"))) {
                data.EffectiveDate = DateTimeType.UNSET;
            } else {
                data.EffectiveDate = new DateTimeType(dataReader.GetDateTime(dataReader.GetOrdinal("EffectiveDate")));
            }
            return data;
	}


	/// <summary>
	/// Inserts a record into the ConfigurationSetting table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(ConfigurationSetting data) {
	    return Insert(data, null);
	}
	
	/// <summary>
	/// Inserts a record into the ConfigurationSetting table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(ConfigurationSetting data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spConfigurationSetting_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Key", DbType.AnsiString, ParameterDirection.Input, data.Key.IsValid ? data.Key.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Value", DbType.AnsiString, ParameterDirection.Input, data.Value.IsValid ? data.Value.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@LastModifiedDate", DbType.DateTime, ParameterDirection.Input, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@LastModifiedUserId", DbType.Int32, ParameterDirection.Input, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
            cmd.Parameters.Add(CreateDataParameter("@EffectiveDate", DbType.DateTime, ParameterDirection.Input, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));

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
	/// Updates a record in the ConfigurationSetting table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(ConfigurationSetting data) {
	    Update(data, null);
	}
	
	/// <summary>
	/// Updates a record in the ConfigurationSetting table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(ConfigurationSetting data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spConfigurationSetting_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ConfigurationSettingId", DbType.Int32, ParameterDirection.Input, data.ConfigurationSettingId.IsValid ? data.ConfigurationSettingId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Key", DbType.AnsiString, ParameterDirection.Input, data.Key.IsValid ? data.Key.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Value", DbType.AnsiString, ParameterDirection.Input, data.Value.IsValid ? data.Value.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@LastModifiedDate", DbType.DateTime, ParameterDirection.Input, data.LastModifiedDate.IsValid ? data.LastModifiedDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@LastModifiedUserId", DbType.Int32, ParameterDirection.Input, data.LastModifiedUserId.IsValid ? data.LastModifiedUserId.ToInt32() as Object : DBNull.Value));
            cmd.Parameters.Add(CreateDataParameter("@EffectiveDate", DbType.DateTime, ParameterDirection.Input, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the ConfigurationSetting table by ConfigurationSettingId.
	/// </summary>
	/// <param name="configurationSettingId"></param>
	public void Delete(IdType configurationSettingId) {
	    Delete(configurationSettingId, null);
	}
	
	/// <summary>
	/// Deletes a record from the ConfigurationSetting table by ConfigurationSettingId.
	/// </summary>
	/// <param name="configurationSettingId"></param>
	/// <param name="transaction"></param>
	public void Delete(IdType configurationSettingId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spConfigurationSetting_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@ConfigurationSettingId", DbType.Int32, ParameterDirection.Input, configurationSettingId.IsValid ? configurationSettingId.ToInt32() as Object : DBNull.Value));

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
	/// <param name="lastModifiedDate">A field value to be matched.</param>
	/// <returns>The list of ConfigurationSettingDAO objects found.</returns>
	public ConfigurationSettingList FindModifications(DateTimeType lastModifiedDate) {
	    OrderByClause sort = new OrderByClause("LastModifiedDate");
	    WhereClause filter = new WhereClause("LastModifiedDate >= @LastModifiedDate");
	    String sql = "Select * from " + VIEW + filter.FormatSql() + sort.FormatSql();
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@LastModifiedDate", DbType.DateTime, ParameterDirection.Input, lastModifiedDate.IsValid ? lastModifiedDate.ToDateTime() as Object : DBNull.Value));
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
	    ConfigurationSettingList list = new ConfigurationSettingList(); 

	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list;
	}
    }
}
