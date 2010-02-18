using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;

using Spring2.Core.Publication.DataObject;
using Spring2.Core.Types;


using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Publication.Dao {
    /// <summary>
    /// Data access interface for PublicationTracking business entity.
    /// </summary>
    public interface IPublicationTrackingDAO {
	/// <summary>
	/// Sets the singleton DAO instance of IPublicationTrackingDAO
	/// </summary>
	[Generate]
	void SetInstance(IPublicationTrackingDAO instance);

	/// <summary>
	/// Returns a list of all PublicationTracking rows.
	/// </summary>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTrackingList GetList();

	/// <summary>
	/// Returns a filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTrackingList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of PublicationTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTrackingList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTrackingList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all PublicationTracking rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTrackingList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTrackingList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of PublicationTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTrackingList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of PublicationTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTrackingList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a PublicationTracking entity using it's primary key.
	/// </summary>
	/// <param name="publicationTrackingId">A key field.</param>
	/// <returns>A PublicationTracking object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	PublicationTracking Load(IdType publicationTrackingId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(PublicationTracking instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationTracking GetDataObjectFromReader(PublicationTracking data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationTracking GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationTracking GetDataObjectFromReader(IDataReader dataReader, PublicationTrackingDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationTracking GetDataObjectFromReader(PublicationTracking data, IDataReader dataReader, PublicationTrackingDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(PublicationTracking data);


	/// <summary>
	/// Updates a record in the PublicationTracking table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(PublicationTracking data);

	/// <summary>
	/// Deletes a record from the PublicationTracking table by PublicationTrackingId.
	/// </summary>
	/// <param name="publicationTrackingId">A key field.</param>
	[Generate]
	void Delete(IdType publicationTrackingId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="publicationTypeId">A field value to be matched.</param>
	/// <returns>The list of PublicationTrackingDAO objects found.</returns>
	[Generate]
	PublicationTrackingList FindByPublicationTypeId(IdType publicationTypeId);

	//}
    }
}
