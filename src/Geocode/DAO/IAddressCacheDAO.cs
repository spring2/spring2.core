using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Types;
using Spring2.Core.Types;


using Spring2.Core.Geocode.BusinessLogic;

using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Geocode.Dao {
    /// <summary>
    /// Data access interface for AddressCache business entity.
    /// </summary>
    public interface IAddressCacheDAO {
	/// <summary>
	/// Sets the singleton DAO instance of IAddressCacheDAO
	/// </summary>
	[Generate]
	void SetInstance(IAddressCacheDAO instance);

	/// <summary>
	/// Returns a list of all AddressCache rows.
	/// </summary>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	AddressCacheList GetList();

	/// <summary>
	/// Returns a filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	AddressCacheList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of AddressCache rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	AddressCacheList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	AddressCacheList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all AddressCache rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	AddressCacheList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	AddressCacheList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of AddressCache rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	AddressCacheList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of AddressCache rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of AddressCache objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	AddressCacheList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a AddressCache entity using it's primary key.
	/// </summary>
	/// <param name="addressId">A key field.</param>
	/// <returns>A AddressCache object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	AddressCache Load(IdType addressId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(AddressCache instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	AddressCache GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	AddressCache GetDataObjectFromReader(IDataReader dataReader, AddressCacheDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	AddressCache GetDataObjectFromReader(AddressCache data, IDataReader dataReader, AddressCacheDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(AddressCache data);


	/// <summary>
	/// Updates a record in the AddressCache table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(AddressCache data);

	/// <summary>
	/// Deletes a record from the AddressCache table by AddressId.
	/// </summary>
	/// <param name="addressId">A key field.</param>
	[Generate]
	void Delete(IdType addressId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="address1">A field value to be matched.</param>
	/// <param name="postalCode">A field value to be matched.</param>
	/// <returns>The list of AddressCacheDAO objects found.</returns>
	[Generate]
	AddressCacheList FindAddressByStreetAndPostalCode(StringType address1, StringType postalCode);

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="address1">A field value to be matched.</param>
	/// <param name="city">A field value to be matched.</param>
	/// <param name="region">A field value to be matched.</param>
	/// <returns>The list of AddressCacheDAO objects found.</returns>
	[Generate]
	AddressCacheList FindAddressByStreetAndCityAndState(StringType address1, StringType city, StringType region);

	//}
    }
}
