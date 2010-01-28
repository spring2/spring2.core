using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.Currency.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;


using Spring2.Core.Currency.BusinessLogic;
using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Currency.Dao {
    /// <summary>
    /// Data access interface for CurrencyExchange business entity.
    /// </summary>
    public interface ICurrencyExchangeDAO {
	/// <summary>
	/// Sets the singleton DAO instance of ICurrencyExchangeDAO
	/// </summary>
	[Generate]
	void SetInstance(ICurrencyExchangeDAO instance);

	/// <summary>
	/// Returns a list of all CurrencyExchange rows.
	/// </summary>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CurrencyExchangeList GetList();

	/// <summary>
	/// Returns a filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CurrencyExchangeList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of CurrencyExchange rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CurrencyExchangeList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CurrencyExchangeList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all CurrencyExchange rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CurrencyExchangeList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CurrencyExchangeList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of CurrencyExchange rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CurrencyExchangeList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of CurrencyExchange rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CurrencyExchange objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CurrencyExchangeList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a CurrencyExchange entity using it's primary key.
	/// </summary>
	/// <param name="currencyExchangeId">A key field.</param>
	/// <returns>A CurrencyExchange object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	CurrencyExchange Load(IdType currencyExchangeId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(CurrencyExchange instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CurrencyExchange GetDataObjectFromReader(CurrencyExchange data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CurrencyExchange GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CurrencyExchange GetDataObjectFromReader(IDataReader dataReader, CurrencyExchangeDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CurrencyExchange GetDataObjectFromReader(CurrencyExchange data, IDataReader dataReader, CurrencyExchangeDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(CurrencyExchange data);


	/// <summary>
	/// Updates a record in the CurrencyExchange table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(CurrencyExchange data);

	/// <summary>
	/// Deletes a record from the CurrencyExchange table by CurrencyExchangeId.
	/// </summary>
	/// <param name="currencyExchangeId">A key field.</param>
	[Generate]
	void Delete(IdType currencyExchangeId);


	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="currencyCode">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CurrencyExchange FindEffectiveRateByCode(StringType currencyCode);

	#region
	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="currencyCode">A field value to be matched.</param>
	/// <param name="data">A field to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	CurrencyExchange FindRateByCodeAndDate(StringType currencyCode, DateTimeType date);
	#endregion
    }
}
