using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.CommunicationSubscription.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;


using Spring2.Core.CommunicationSubscription.BusinessLogic;
using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.CommunicationSubscription.Dao {
    /// <summary>
    /// Data access interface for CommunicationSubscriptionTracking business entity.
    /// </summary>
    public interface ICommunicationSubscriptionTrackingDAO {
	/// <summary>
	/// Sets the singleton DAO instance of ICommunicationSubscriptionTrackingDAO
	/// </summary>
	[Generate]
	void SetInstance(ICommunicationSubscriptionTrackingDAO instance);

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList();

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionTracking rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionTracking rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionTracking objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTrackingList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a CommunicationSubscriptionTracking entity using it's primary key.
	/// </summary>
	/// <param name="communicationPrimaryKeyId">A key field.</param>
	/// <returns>A CommunicationSubscriptionTracking object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	CommunicationSubscriptionTracking Load(IdType communicationPrimaryKeyId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(CommunicationSubscriptionTracking instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionTracking GetDataObjectFromReader(CommunicationSubscriptionTracking data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionTracking GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionTracking GetDataObjectFromReader(IDataReader dataReader, CommunicationSubscriptionTrackingDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionTracking GetDataObjectFromReader(CommunicationSubscriptionTracking data, IDataReader dataReader, CommunicationSubscriptionTrackingDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(CommunicationSubscriptionTracking data);


	/// <summary>
	/// Updates a record in the CommunicationSubscriptionTracking table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(CommunicationSubscriptionTracking data);

	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionTracking table by CommunicationPrimaryKeyId.
	/// </summary>
	/// <param name="communicationPrimaryKeyId">A key field.</param>
	[Generate]
	void Delete(IdType communicationPrimaryKeyId);


	//}
    }
}
