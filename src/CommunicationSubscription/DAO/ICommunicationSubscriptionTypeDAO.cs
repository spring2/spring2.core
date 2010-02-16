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
    /// Data access interface for CommunicationSubscriptionType business entity.
    /// </summary>
    public interface ICommunicationSubscriptionTypeDAO {
	/// <summary>
	/// Sets the singleton DAO instance of ICommunicationSubscriptionTypeDAO
	/// </summary>
	[Generate]
	void SetInstance(ICommunicationSubscriptionTypeDAO instance);

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionType rows.
	/// </summary>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList();

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of CommunicationSubscriptionType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of CommunicationSubscriptionType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of CommunicationSubscriptionType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	CommunicationSubscriptionTypeList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a CommunicationSubscriptionType entity using it's primary key.
	/// </summary>
	/// <param name="communicationSubscriptionTypeId">A key field.</param>
	/// <returns>A CommunicationSubscriptionType object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	CommunicationSubscriptionType Load(IdType communicationSubscriptionTypeId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(CommunicationSubscriptionType instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionType GetDataObjectFromReader(CommunicationSubscriptionType data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionType GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionType GetDataObjectFromReader(IDataReader dataReader, CommunicationSubscriptionTypeDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	CommunicationSubscriptionType GetDataObjectFromReader(CommunicationSubscriptionType data, IDataReader dataReader, CommunicationSubscriptionTypeDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(CommunicationSubscriptionType data);


	/// <summary>
	/// Updates a record in the CommunicationSubscriptionType table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(CommunicationSubscriptionType data);

	/// <summary>
	/// Deletes a record from the CommunicationSubscriptionType table by CommunicationSubscriptionTypeId.
	/// </summary>
	/// <param name="communicationSubscriptionTypeId">A key field.</param>
	[Generate]
	void Delete(IdType communicationSubscriptionTypeId);


	//}
    }
}
