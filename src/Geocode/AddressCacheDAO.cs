using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;


namespace Spring2.Core.Geocode {
    public class AddressCacheDAO : Spring2.Core.DAO.SqlEntityDAO {


	public static readonly AddressCacheDAO DAO = new AddressCacheDAO(); 
	private static readonly String VIEW = "vwAddressCache";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static Hashtable propertyToSqlMap = new Hashtable();

	static AddressCacheDAO() {
	    if (!propertyToSqlMap.Contains("AddressId")) {
		propertyToSqlMap.Add("AddressId",@"AddressId");
	    }
	    if (!propertyToSqlMap.Contains("Address1")) {
		propertyToSqlMap.Add("Address1",@"Address1");
	    }
	    if (!propertyToSqlMap.Contains("City")) {
		propertyToSqlMap.Add("City",@"City");
	    }
	    if (!propertyToSqlMap.Contains("Region")) {
		propertyToSqlMap.Add("Region",@"Region");
	    }
	    if (!propertyToSqlMap.Contains("PostalCode")) {
		propertyToSqlMap.Add("PostalCode",@"PostalCode");
	    }
	    if (!propertyToSqlMap.Contains("Latitude")) {
		propertyToSqlMap.Add("Latitude",@"Latitude");
	    }
	    if (!propertyToSqlMap.Contains("Longitude")) {
		propertyToSqlMap.Add("Longitude",@"Longitude");
	    }
	    if (!propertyToSqlMap.Contains("Result")) {
		propertyToSqlMap.Add("Result",@"Result");
	    }
	    if (!propertyToSqlMap.Contains("Status")) {
		propertyToSqlMap.Add("Status",@"Status");
	    }
	}

	private AddressCacheDAO() {}
	
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
	/// Returns a list of all AddressCache rows.
	/// </summary>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList() { 
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of AddressCache rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(IWhere whereClause) { 
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of AddressCache rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList(IOrderBy orderByClause) { 
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of AddressCache rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(IWhere whereClause, IOrderBy orderByClause) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); 

	    AddressCacheList list = new AddressCacheList(); 
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Returns a list of all AddressCache rows.
	/// </summary>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList(Int32 maxRows) { 
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of AddressCache rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(IWhere whereClause, Int32 maxRows) { 
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of AddressCache rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList(IOrderBy orderByClause, Int32 maxRows) { 
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of AddressCache rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows); 

	    AddressCacheList list = new AddressCacheList();
	    while (dataReader.Read()) { 
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list; 
	}

	/// <summary>
	/// Finds a AddressCache entity using it's primary key.
	/// </summary>
	/// <param name="AddressId">A key field.</param>
	/// <returns>A AddressCache object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public AddressCache Load(IdType addressId) {
	    WhereClause w = new WhereClause();
	    w.And("AddressId", addressId.IsValid ? addressId.ToInt32() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for AddressCache.");
	    }
	    AddressCache data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(AddressCache instance) {
	    WhereClause w = new WhereClause();
	    w.And("AddressId", instance.AddressId);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for AddressCache.");
	    }
	    AddressCache data = GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static AddressCache GetDataObjectFromReader(IDataReader dataReader) {
	    AddressCache data = new AddressCache(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static AddressCache GetDataObjectFromReader(IDataReader dataReader, String prefix) {
	    AddressCache data = new AddressCache(false);
	    return GetDataObjectFromReader(data, dataReader, prefix);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader, String prefix) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AddressId"))) { 
		data.AddressId = IdType.UNSET;
	    } else {
		data.AddressId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("AddressId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address1"))) { 
		data.Address1 = StringType.UNSET;
	    } else {
		data.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("City"))) { 
		data.City = StringType.UNSET;
	    } else {
		data.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Region"))) { 
		data.Region = StringType.UNSET;
	    } else {
		data.Region = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Region")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PostalCode"))) { 
		data.PostalCode = StringType.UNSET;
	    } else {
		data.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("PostalCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Latitude"))) { 
		data.Latitude = DecimalType.UNSET;
	    } else {
		data.Latitude = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("Latitude")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Longitude"))) { 
		data.Longitude = DecimalType.UNSET;
	    } else {
		data.Longitude = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("Longitude")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Result"))) { 
		data.Result = StringType.UNSET;
	    } else {
		data.Result = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Result")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Status"))) { 
		data.Status = GeocodeStatusEnum.UNSET;
	    } else {
		data.Status = GeocodeStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Status")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the AddressCache table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(AddressCache data) {
	    return Insert(data, null);
	}
	
	/// <summary>
	/// Inserts a record into the AddressCache table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(AddressCache data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spAddressCache_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Address1", DbType.AnsiString, ParameterDirection.Input, data.Address1.IsValid ? data.Address1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@City", DbType.AnsiString, ParameterDirection.Input, data.City.IsValid ? data.City.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Region", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.Region.IsValid ? data.Region.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@PostalCode", DbType.AnsiString, ParameterDirection.Input, data.PostalCode.IsValid ? data.PostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Latitude", DbType.Decimal, ParameterDirection.Input, data.Latitude.IsValid ? data.Latitude.ToDecimal() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Longitude", DbType.Decimal, ParameterDirection.Input, data.Longitude.IsValid ? data.Longitude.ToDecimal() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Result", DbType.AnsiString, ParameterDirection.Input, data.Result.IsValid ? data.Result.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Status", DbType.AnsiString, ParameterDirection.Input, data.Status.DBValue));

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
	/// Updates a record in the AddressCache table.
	/// </summary>
	/// <param name=""></param>
	public void Update(AddressCache data) {
	    Update(data, null);
	}
	
	/// <summary>
	/// Updates a record in the AddressCache table.
	/// </summary>
	/// <param name=""></param>
	public void Update(AddressCache data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spAddressCache_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@AddressId", DbType.Int32, ParameterDirection.Input, data.AddressId.IsValid ? data.AddressId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Address1", DbType.AnsiString, ParameterDirection.Input, data.Address1.IsValid ? data.Address1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@City", DbType.AnsiString, ParameterDirection.Input, data.City.IsValid ? data.City.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Region", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.Region.IsValid ? data.Region.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@PostalCode", DbType.AnsiString, ParameterDirection.Input, data.PostalCode.IsValid ? data.PostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Latitude", DbType.Decimal, ParameterDirection.Input, data.Latitude.IsValid ? data.Latitude.ToDecimal() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Longitude", DbType.Decimal, ParameterDirection.Input, data.Longitude.IsValid ? data.Longitude.ToDecimal() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Result", DbType.AnsiString, ParameterDirection.Input, data.Result.IsValid ? data.Result.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Status", DbType.AnsiString, ParameterDirection.Input, data.Status.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the AddressCache table by AddressId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType addressId) {
	    Delete(addressId, null);
	}
	
	/// <summary>
	/// Deletes a record from the AddressCache table by AddressId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType addressId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spAddressCache_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@AddressId", DbType.Int32, ParameterDirection.Input, addressId.IsValid ? addressId.ToInt32() as Object : DBNull.Value));

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
	/// <param name="Address1">A field value to be matched.</param>
	/// <param name="PostalCode">A field value to be matched.</param>
	/// <returns>The list of AddressCacheDAO objects found.</returns>
	public AddressCacheList FindAddressByStreetAndPostalCode(StringType address1, StringType postalCode) {
	    OrderByClause sort = new OrderByClause("Address1, PostalCode");
	    WhereClause filter = new WhereClause("Address1=@Address1 and PostalCode=@PostalCode");
	    String sql = "Select * from " + VIEW + filter.FormatSql() + sort.FormatSql();
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@Address1", DbType.AnsiString, ParameterDirection.Input, address1.IsValid ? address1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@PostalCode", DbType.AnsiString, ParameterDirection.Input, postalCode.IsValid ? postalCode.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
	    AddressCacheList list = new AddressCacheList(); 

	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader)); 
	    }
	    dataReader.Close();
	    return list;
	}
    }
}
