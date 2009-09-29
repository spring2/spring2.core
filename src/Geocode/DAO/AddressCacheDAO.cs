using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Geocode.BusinessLogic;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode.Dao {

    /// <summary>
    /// Data access class for AddressCache business entity.
    /// </summary>
    public class AddressCacheDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly AddressCacheDAO DAO = new AddressCacheDAO();
	private static readonly String VIEW = "vwAddressCache";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 AddressId;
	    public Int32 Address1;
	    public Int32 City;
	    public Int32 Region;
	    public Int32 PostalCode;
	    public Int32 Latitude;
	    public Int32 Longitude;
	    public Int32 Result;
	    public Int32 Status;
	    public Int32 StdAddress1;
	    public Int32 StdCity;
	    public Int32 StdRegion;
	    public Int32 StdPostalCode;
	    public Int32 StdPlus4;
	    public Int32 MatAddress1;
	    public Int32 MatCity;
	    public Int32 MatRegion;
	    public Int32 MatPostalCode;
	    public Int32 MatchType;

	    internal ColumnOrdinals(IDataReader reader) {
		AddressId = reader.GetOrdinal("AddressId");
		Address1 = reader.GetOrdinal("Address1");
		City = reader.GetOrdinal("City");
		Region = reader.GetOrdinal("Region");
		PostalCode = reader.GetOrdinal("PostalCode");
		Latitude = reader.GetOrdinal("Latitude");
		Longitude = reader.GetOrdinal("Longitude");
		Result = reader.GetOrdinal("Result");
		Status = reader.GetOrdinal("Status");
		StdAddress1 = reader.GetOrdinal("StdAddress1");
		StdCity = reader.GetOrdinal("StdCity");
		StdRegion = reader.GetOrdinal("StdRegion");
		StdPostalCode = reader.GetOrdinal("StdPostalCode");
		StdPlus4 = reader.GetOrdinal("StdPlus4");
		MatAddress1 = reader.GetOrdinal("MatAddress1");
		MatCity = reader.GetOrdinal("MatCity");
		MatRegion = reader.GetOrdinal("MatRegion");
		MatPostalCode = reader.GetOrdinal("MatPostalCode");
		MatchType = reader.GetOrdinal("MatchType");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		AddressId = reader.GetOrdinal(prefix + "AddressId");
		Address1 = reader.GetOrdinal(prefix + "Address1");
		City = reader.GetOrdinal(prefix + "City");
		Region = reader.GetOrdinal(prefix + "Region");
		PostalCode = reader.GetOrdinal(prefix + "PostalCode");
		Latitude = reader.GetOrdinal(prefix + "Latitude");
		Longitude = reader.GetOrdinal(prefix + "Longitude");
		Result = reader.GetOrdinal(prefix + "Result");
		Status = reader.GetOrdinal(prefix + "Status");
		StdAddress1 = reader.GetOrdinal(prefix + "StdAddress1");
		StdCity = reader.GetOrdinal(prefix + "StdCity");
		StdRegion = reader.GetOrdinal(prefix + "StdRegion");
		StdPostalCode = reader.GetOrdinal(prefix + "StdPostalCode");
		StdPlus4 = reader.GetOrdinal(prefix + "StdPlus4");
		MatAddress1 = reader.GetOrdinal(prefix + "MatAddress1");
		MatCity = reader.GetOrdinal(prefix + "MatCity");
		MatRegion = reader.GetOrdinal(prefix + "MatRegion");
		MatPostalCode = reader.GetOrdinal(prefix + "MatPostalCode");
		MatchType = reader.GetOrdinal(prefix + "MatchType");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static AddressCacheDAO() {
	    AddPropertyMapping("AddressId", @"AddressId");
	    AddPropertyMapping("Address1", @"Address1");
	    AddPropertyMapping("City", @"City");
	    AddPropertyMapping("Region", @"Region");
	    AddPropertyMapping("PostalCode", @"PostalCode");
	    AddPropertyMapping("Latitude", @"Latitude");
	    AddPropertyMapping("Longitude", @"Longitude");
	    AddPropertyMapping("Result", @"Result");
	    AddPropertyMapping("Status", @"Status");
	    AddPropertyMapping("StdAddress1", @"StdAddress1");
	    AddPropertyMapping("StdCity", @"StdCity");
	    AddPropertyMapping("StdRegion", @"StdRegion");
	    AddPropertyMapping("StdPostalCode", @"StdPostalCode");
	    AddPropertyMapping("StdPlus4", @"StdPlus4");
	    AddPropertyMapping("MatAddress1", @"MatAddress1");
	    AddPropertyMapping("MatCity", @"MatCity");
	    AddPropertyMapping("MatRegion", @"MatRegion");
	    AddPropertyMapping("MatPostalCode", @"MatPostalCode");
	    AddPropertyMapping("MatchType", @"MatchType");
	}

	private AddressCacheDAO() {
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
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(SqlFilter filter) {
	    return GetList(filter, null);
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
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

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
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of AddressCache rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public AddressCacheList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public AddressCacheList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

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
	/// <param name="addressId">A key field.</param>
	/// <returns>A AddressCache object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public AddressCache Load(IdType addressId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("AddressId", EqualityOperatorEnum.Equal, addressId.IsValid ? addressId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(AddressCache instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("AddressId", EqualityOperatorEnum.Equal, instance.AddressId.IsValid ? instance.AddressId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for AddressCache.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private AddressCacheList GetList(IDataReader reader) {
	    AddressCacheList list = new AddressCacheList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private AddressCache GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private AddressCache GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    AddressCache data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader) {
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
	internal AddressCache GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    AddressCache data = new AddressCache(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal AddressCache GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    AddressCache data = new AddressCache(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.AddressId)) {
		data.AddressId = IdType.UNSET;
	    } else {
		data.AddressId = new IdType(dataReader.GetInt32(ordinals.AddressId));
	    }
	    if (dataReader.IsDBNull(ordinals.Address1)) {
		data.Address1 = StringType.UNSET;
	    } else {
		data.Address1 = StringType.Parse(dataReader.GetString(ordinals.Address1));
	    }
	    if (dataReader.IsDBNull(ordinals.City)) {
		data.City = StringType.UNSET;
	    } else {
		data.City = StringType.Parse(dataReader.GetString(ordinals.City));
	    }
	    if (dataReader.IsDBNull(ordinals.Region)) {
		data.Region = StringType.UNSET;
	    } else {
		data.Region = StringType.Parse(dataReader.GetString(ordinals.Region));
	    }
	    if (dataReader.IsDBNull(ordinals.PostalCode)) {
		data.PostalCode = StringType.UNSET;
	    } else {
		data.PostalCode = StringType.Parse(dataReader.GetString(ordinals.PostalCode));
	    }
	    if (dataReader.IsDBNull(ordinals.Latitude)) {
		data.Latitude = DecimalType.UNSET;
	    } else {
		data.Latitude = new DecimalType(dataReader.GetDecimal(ordinals.Latitude));
	    }
	    if (dataReader.IsDBNull(ordinals.Longitude)) {
		data.Longitude = DecimalType.UNSET;
	    } else {
		data.Longitude = new DecimalType(dataReader.GetDecimal(ordinals.Longitude));
	    }
	    if (dataReader.IsDBNull(ordinals.Result)) {
		data.Result = StringType.UNSET;
	    } else {
		data.Result = StringType.Parse(dataReader.GetString(ordinals.Result));
	    }
	    if (dataReader.IsDBNull(ordinals.Status)) {
		data.Status = GeocodeStatusEnum.UNSET;
	    } else {
		data.Status = GeocodeStatusEnum.GetInstance(dataReader.GetString(ordinals.Status));
	    }
	    if (dataReader.IsDBNull(ordinals.StdAddress1)) {
		data.StdAddress1 = StringType.UNSET;
	    } else {
		data.StdAddress1 = StringType.Parse(dataReader.GetString(ordinals.StdAddress1));
	    }
	    if (dataReader.IsDBNull(ordinals.StdCity)) {
		data.StdCity = StringType.UNSET;
	    } else {
		data.StdCity = StringType.Parse(dataReader.GetString(ordinals.StdCity));
	    }
	    if (dataReader.IsDBNull(ordinals.StdRegion)) {
		data.StdRegion = StringType.UNSET;
	    } else {
		data.StdRegion = StringType.Parse(dataReader.GetString(ordinals.StdRegion));
	    }
	    if (dataReader.IsDBNull(ordinals.StdPostalCode)) {
		data.StdPostalCode = StringType.UNSET;
	    } else {
		data.StdPostalCode = StringType.Parse(dataReader.GetString(ordinals.StdPostalCode));
	    }
	    if (dataReader.IsDBNull(ordinals.StdPlus4)) {
		data.StdPlus4 = StringType.UNSET;
	    } else {
		data.StdPlus4 = StringType.Parse(dataReader.GetString(ordinals.StdPlus4));
	    }
	    if (dataReader.IsDBNull(ordinals.MatAddress1)) {
		data.MatAddress1 = StringType.UNSET;
	    } else {
		data.MatAddress1 = StringType.Parse(dataReader.GetString(ordinals.MatAddress1));
	    }
	    if (dataReader.IsDBNull(ordinals.MatCity)) {
		data.MatCity = StringType.UNSET;
	    } else {
		data.MatCity = StringType.Parse(dataReader.GetString(ordinals.MatCity));
	    }
	    if (dataReader.IsDBNull(ordinals.MatRegion)) {
		data.MatRegion = StringType.UNSET;
	    } else {
		data.MatRegion = StringType.Parse(dataReader.GetString(ordinals.MatRegion));
	    }
	    if (dataReader.IsDBNull(ordinals.MatPostalCode)) {
		data.MatPostalCode = StringType.UNSET;
	    } else {
		data.MatPostalCode = StringType.Parse(dataReader.GetString(ordinals.MatPostalCode));
	    }
	    if (dataReader.IsDBNull(ordinals.MatchType)) {
		data.MatchType = IntegerType.UNSET;
	    } else {
		data.MatchType = new IntegerType(dataReader.GetInt32(ordinals.MatchType));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(AddressCache data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
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
	    cmd.Parameters.Add(CreateDataParameter("@StdAddress1", DbType.AnsiString, ParameterDirection.Input, data.StdAddress1.IsValid ? data.StdAddress1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdCity", DbType.AnsiString, ParameterDirection.Input, data.StdCity.IsValid ? data.StdCity.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdRegion", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.StdRegion.IsValid ? data.StdRegion.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdPostalCode", DbType.AnsiString, ParameterDirection.Input, data.StdPostalCode.IsValid ? data.StdPostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdPlus4", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.StdPlus4.IsValid ? data.StdPlus4.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatAddress1", DbType.AnsiString, ParameterDirection.Input, data.MatAddress1.IsValid ? data.MatAddress1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatCity", DbType.AnsiString, ParameterDirection.Input, data.MatCity.IsValid ? data.MatCity.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatRegion", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.MatRegion.IsValid ? data.MatRegion.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatPostalCode", DbType.AnsiString, ParameterDirection.Input, data.MatPostalCode.IsValid ? data.MatPostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatchType", DbType.Int32, ParameterDirection.Input, data.MatchType.IsValid ? data.MatchType.ToInt32() as Object : DBNull.Value));

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
	/// Updates a record in the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(AddressCache data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
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
	    cmd.Parameters.Add(CreateDataParameter("@StdAddress1", DbType.AnsiString, ParameterDirection.Input, data.StdAddress1.IsValid ? data.StdAddress1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdCity", DbType.AnsiString, ParameterDirection.Input, data.StdCity.IsValid ? data.StdCity.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdRegion", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.StdRegion.IsValid ? data.StdRegion.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdPostalCode", DbType.AnsiString, ParameterDirection.Input, data.StdPostalCode.IsValid ? data.StdPostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@StdPlus4", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.StdPlus4.IsValid ? data.StdPlus4.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatAddress1", DbType.AnsiString, ParameterDirection.Input, data.MatAddress1.IsValid ? data.MatAddress1.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatCity", DbType.AnsiString, ParameterDirection.Input, data.MatCity.IsValid ? data.MatCity.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatRegion", DbType.AnsiStringFixedLength, ParameterDirection.Input, data.MatRegion.IsValid ? data.MatRegion.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatPostalCode", DbType.AnsiString, ParameterDirection.Input, data.MatPostalCode.IsValid ? data.MatPostalCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MatchType", DbType.Int32, ParameterDirection.Input, data.MatchType.IsValid ? data.MatchType.ToInt32() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the AddressCache table by AddressId.
	/// </summary>
	/// <param name="addressId">A key field.</param>
	public void Delete(IdType addressId) {
	    Delete(addressId, null);
	}

	/// <summary>
	/// Deletes a record from the AddressCache table by AddressId.
	/// </summary>
	/// <param name="addressId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType addressId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spAddressCache_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@AddressId", DbType.Int32, ParameterDirection.Input, addressId.IsValid ? addressId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="address1">A field value to be matched.</param>
	/// <param name="postalCode">A field value to be matched.</param>
	/// <returns>The list of AddressCacheDAO objects found.</returns>
	public AddressCacheList FindAddressByStreetAndPostalCode(StringType address1, StringType postalCode) {
	    OrderByClause sort = new OrderByClause("Address1, PostalCode");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Address1", EqualityOperatorEnum.Equal, address1.IsValid ? address1.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("PostalCode", EqualityOperatorEnum.Equal, postalCode.IsValid ? postalCode.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="address1">A field value to be matched.</param>
	/// <param name="city">A field value to be matched.</param>
	/// <param name="region">A field value to be matched.</param>
	/// <returns>The list of AddressCacheDAO objects found.</returns>
	public AddressCacheList FindAddressByStreetAndCityAndState(StringType address1, StringType city, StringType region) {
	    OrderByClause sort = new OrderByClause("Address1, City, Region");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Address1", EqualityOperatorEnum.Equal, address1.IsValid ? address1.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("City", EqualityOperatorEnum.Equal, city.IsValid ? city.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Region", EqualityOperatorEnum.Equal, region.IsValid ? region.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}
    }
}
