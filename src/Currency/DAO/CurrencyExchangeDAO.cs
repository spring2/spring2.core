using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Currency.BusinessLogic;
using Spring2.Core.Currency.DataObject;

namespace Spring2.Core.Currency.Dao {

    /// <summary>
    /// Data access class for CurrencyExchange business entity.
    /// </summary>
    public class CurrencyExchangeDAO : Spring2.Core.DAO.SqlEntityDAO, ICurrencyExchangeDAO {
	private static ICurrencyExchangeDAO instance = new CurrencyExchangeDAO();
	public static ICurrencyExchangeDAO DAO{
	    get{ return instance; }
	}
	
	/// <summary>
	/// Sets the singleton DAO instance of ICurrencyExchangeDAO
	/// </summary>
	public void SetInstance(ICurrencyExchangeDAO dao) {
	    if(dao != null){
			instance = dao;
	    }else{
			instance = new CurrencyExchangeDAO();
	    }
	}
	
	private static readonly String VIEW = "vwCurrencyExchange";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	public sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 CurrencyExchangeId;
	    public Int32 CurrencyCode;
	    public Int32 EffectiveDate;
	    public Int32 Rate;

	    internal ColumnOrdinals(IDataReader reader) {
		CurrencyExchangeId = reader.GetOrdinal("CurrencyExchangeId");
		CurrencyCode = reader.GetOrdinal("CurrencyCode");
		EffectiveDate = reader.GetOrdinal("EffectiveDate");
		Rate = reader.GetOrdinal("Rate");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		CurrencyExchangeId = reader.GetOrdinal(prefix + "CurrencyExchangeId");
		CurrencyCode = reader.GetOrdinal(prefix + "CurrencyCode");
		EffectiveDate = reader.GetOrdinal(prefix + "EffectiveDate");
		Rate = reader.GetOrdinal(prefix + "Rate");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static CurrencyExchangeDAO() {
	    AddPropertyMapping("CurrencyExchangeId", @"CurrencyExchangeId");
	    AddPropertyMapping("CurrencyCode", @"CurrencyCode");
	    AddPropertyMapping("EffectiveDate", @"EffectiveDate");
	    AddPropertyMapping("Rate", @"Rate");
	}

	private CurrencyExchangeDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all CurrencyExchange rows.
	/// </summary>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchangeList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CurrencyExchangeList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of CurrencyExchange rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchangeList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CurrencyExchangeList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    CurrencyExchangeList list = new CurrencyExchangeList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all CurrencyExchange rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchangeList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CurrencyExchangeList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of CurrencyExchange rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchangeList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public CurrencyExchangeList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    CurrencyExchangeList list = new CurrencyExchangeList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a CurrencyExchange entity using it's primary key.
	/// </summary>
	/// <param name="currencyExchangeId">A key field.</param>
	/// <returns>A CurrencyExchange object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public CurrencyExchange Load(IdType currencyExchangeId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CurrencyExchangeFields.CURRENCYEXCHANGEID, EqualityOperatorEnum.Equal, currencyExchangeId.IsValid ? currencyExchangeId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(CurrencyExchange instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(CreateEqualityPredicate(CurrencyExchangeFields.CURRENCYEXCHANGEID, EqualityOperatorEnum.Equal, instance.CurrencyExchangeId.IsValid ? instance.CurrencyExchangeId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for CurrencyExchange.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private CurrencyExchangeList GetList(IDataReader reader) {
	    CurrencyExchangeList list = new CurrencyExchangeList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CurrencyExchange GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private CurrencyExchange GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    CurrencyExchange data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	public CurrencyExchange GetDataObjectFromReader(CurrencyExchange data, IDataReader dataReader) {
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
	public CurrencyExchange GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    CurrencyExchange data = new CurrencyExchange(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CurrencyExchange GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    CurrencyExchange data = new CurrencyExchange(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	public CurrencyExchange GetDataObjectFromReader(CurrencyExchange data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.CurrencyExchangeId)) {
		data.CurrencyExchangeId = IdType.UNSET;
	    } else {
		data.CurrencyExchangeId = new IdType(dataReader.GetInt32(ordinals.CurrencyExchangeId));
	    }
	    if (dataReader.IsDBNull(ordinals.CurrencyCode)) {
		data.CurrencyCode = StringType.UNSET;
	    } else {
		data.CurrencyCode = StringType.Parse(dataReader.GetString(ordinals.CurrencyCode));
	    }
	    if (dataReader.IsDBNull(ordinals.EffectiveDate)) {
		data.EffectiveDate = DateTimeType.UNSET;
	    } else {
		data.EffectiveDate = new DateTimeType(dataReader.GetDateTime(ordinals.EffectiveDate));
	    }
	    if (dataReader.IsDBNull(ordinals.Rate)) {
		data.Rate = DecimalType.UNSET;
	    } else {
		data.Rate = new DecimalType(dataReader.GetDecimal(ordinals.Rate));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(CurrencyExchange data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(CurrencyExchange data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCurrencyExchange_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.CURRENCYCODE, data.CurrencyCode.IsValid ? data.CurrencyCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.EFFECTIVEDATE, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.RATE, data.Rate.IsValid ? data.Rate.ToDecimal() as Object : DBNull.Value));

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
	/// Updates a record in the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(CurrencyExchange data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(CurrencyExchange data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCurrencyExchange_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.CURRENCYEXCHANGEID, data.CurrencyExchangeId.IsValid ? data.CurrencyExchangeId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.CURRENCYCODE, data.CurrencyCode.IsValid ? data.CurrencyCode.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.EFFECTIVEDATE, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.RATE, data.Rate.IsValid ? data.Rate.ToDecimal() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the CurrencyExchange table by CurrencyExchangeId.
	/// </summary>
	/// <param name="currencyExchangeId">A key field.</param>
	public void Delete(IdType currencyExchangeId) {
	    Delete(currencyExchangeId, null);
	}

	/// <summary>
	/// Deletes a record from the CurrencyExchange table by CurrencyExchangeId.
	/// </summary>
	/// <param name="currencyExchangeId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType currencyExchangeId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spCurrencyExchange_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter(CurrencyExchangeFields.CURRENCYEXCHANGEID, currencyExchangeId.IsValid ? currencyExchangeId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null && DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="currencyCode">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchange FindEffectiveRateByCode(StringType currencyCode) {
	    OrderByClause sort = new OrderByClause("EffectiveDate DESC");
	    SqlFilter filter = new SqlFilter(new SqlLiteralPredicate("CurrencyCode = @CurrencyCode AND EffectiveDate <= getdate()"));
	    String sql = "SELECT * from " + VIEW + filter.Statement + sort.FormatSql();
		IDataParameterCollection parameters = new SqlParameterList();
	    parameters.Add(CreateDataParameter("@CurrencyCode", CurrencyExchangeFields.CURRENCYCODE, currencyCode.IsValid ? currencyCode.ToString() as Object : DBNull.Value));
		IDataReader dataReader = ExecuteReader(CONNECTION_STRING_KEY, sql, parameters, COMMAND_TIMEOUT);
	    return GetDataObject(dataReader);
	}

	#region
	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="currencyCode">A field value to be matched.</param>
	/// <param name="data">A field to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public CurrencyExchange FindRateByCodeAndDate(StringType currencyCode, DateTimeType date) {
	    OrderByClause sort = new OrderByClause("EffectiveDate DESC");
	    SqlFilter filter = new SqlFilter(new SqlLiteralPredicate("CurrencyCode = @CurrencyCode AND EffectiveDate <= @EffectiveDate"));
	    String sql = "SELECT top 1 * from " + VIEW + filter.Statement + sort.FormatSql();
	    IDataParameterCollection parameters = new SqlParameterList();
	    parameters.Add(CreateDataParameter("@CurrencyCode", CurrencyExchangeFields.CURRENCYCODE, currencyCode.IsValid ? currencyCode.ToString() as Object : DBNull.Value));
	    parameters.Add(CreateDataParameter("@EffectiveDate", CurrencyExchangeFields.EFFECTIVEDATE, date.IsValid ? date.ToDateTime() as Object : DBNull.Value));
	    IDataReader dataReader = ExecuteReader(CONNECTION_STRING_KEY, sql, parameters, COMMAND_TIMEOUT);
	    return GetDataObject(dataReader);
	}
	#endregion
    }
}
