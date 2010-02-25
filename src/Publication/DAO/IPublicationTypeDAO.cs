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
    /// Data access interface for PublicationType business entity.
    /// </summary>
    public interface IPublicationTypeDAO {

	/// <summary>
	/// Returns a list of all PublicationType rows.
	/// </summary>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTypeList GetList();

	/// <summary>
	/// Returns a filtered list of PublicationType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTypeList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of PublicationType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTypeList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of PublicationType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTypeList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all PublicationType rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTypeList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of PublicationType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTypeList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of PublicationType rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationTypeList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of PublicationType rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of PublicationType objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	PublicationTypeList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a PublicationType entity using it's primary key.
	/// </summary>
	/// <param name="publicationTypeId">A key field.</param>
	/// <returns>A PublicationType object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	PublicationType Load(IdType publicationTypeId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(PublicationType instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationType GetDataObjectFromReader(PublicationType data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationType GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationType GetDataObjectFromReader(IDataReader dataReader, PublicationTypeDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	PublicationType GetDataObjectFromReader(PublicationType data, IDataReader dataReader, PublicationTypeDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the PublicationType table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(PublicationType data);


	/// <summary>
	/// Updates a record in the PublicationType table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(PublicationType data);

	/// <summary>
	/// Deletes a record from the PublicationType table by PublicationTypeId.
	/// </summary>
	/// <param name="publicationTypeId">A key field.</param>
	[Generate]
	void Delete(IdType publicationTypeId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="effectiveDate">A field value to be matched.</param>
	/// <returns>The list of PublicationTypeDAO objects found.</returns>
	[Generate]
	PublicationTypeList FindActivePublicationTypeForProcessingByDate(DateTimeType effectiveDate);

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="effectiveDate">A field value to be matched.</param>
	/// <returns>The list of PublicationTypeDAO objects found.</returns>
	[Generate]
	PublicationTypeList FindActiveSubscribablePublicationTypeByDate(DateTimeType effectiveDate);

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="effectiveDate">A field value to be matched.</param>
	/// <returns>The list of PublicationTypeDAO objects found.</returns>
	[Generate]
	PublicationTypeList FindActiveAutoSubscribablePublicationTypeByDate(DateTimeType effectiveDate);

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="name">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	PublicationType GetByName(StringType name);

	//}
    }
}
