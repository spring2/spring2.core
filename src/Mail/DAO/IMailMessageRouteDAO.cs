using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;
using Spring2.Core.Types;


using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Dao {
    /// <summary>
    /// Data access interface for MailMessageRoute business entity.
    /// </summary>
    public interface IMailMessageRouteDAO {

	/// <summary>
	/// Returns a list of all MailMessageRoute rows.
	/// </summary>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageRouteList GetList();

	/// <summary>
	/// Returns a filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageRouteList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of MailMessageRoute rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageRouteList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageRouteList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all MailMessageRoute rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageRouteList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageRouteList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of MailMessageRoute rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageRouteList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of MailMessageRoute rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessageRoute objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageRouteList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a MailMessageRoute entity using it's primary key.
	/// </summary>
	/// <param name="mailMessageRouteId">A key field.</param>
	/// <returns>A MailMessageRoute object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	MailMessageRoute Load(IdType mailMessageRouteId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(MailMessageRoute instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessageRoute GetDataObjectFromReader(MailMessageRoute data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessageRoute GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessageRoute GetDataObjectFromReader(IDataReader dataReader, MailMessageRouteDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessageRoute GetDataObjectFromReader(MailMessageRoute data, IDataReader dataReader, MailMessageRouteDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(MailMessageRoute data);


	/// <summary>
	/// Updates a record in the MailMessageRoute table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(MailMessageRoute data);

	/// <summary>
	/// Deletes a record from the MailMessageRoute table by MailMessageRouteId.
	/// </summary>
	/// <param name="mailMessageRouteId">A key field.</param>
	[Generate]
	void Delete(IdType mailMessageRouteId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="mailMessage">A field value to be matched.</param>
	/// <returns>The list of MailMessageRouteDAO objects found.</returns>
	[Generate]
	MailMessageRouteList FindByMailMessage(StringType mailMessage);

	//}
    }
}
