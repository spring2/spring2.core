using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Types;


using Spring2.Core.Mail.Types;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.BusinessEntity;

using Spring2.DataTierGenerator.Attribute;


namespace Spring2.Core.Mail.Dao {
    /// <summary>
    /// Data access interface for MailAttachment business entity.
    /// </summary>
    public interface IMailAttachmentDAO {

	/// <summary>
	/// Returns a list of all MailAttachment rows.
	/// </summary>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailAttachmentList GetList();

	/// <summary>
	/// Returns a filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailAttachmentList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of MailAttachment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailAttachmentList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailAttachmentList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all MailAttachment rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailAttachmentList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailAttachmentList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of MailAttachment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailAttachmentList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of MailAttachment rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailAttachment objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailAttachmentList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a MailAttachment entity using it's primary key.
	/// </summary>
	/// <param name="mailAttachmentId">A key field.</param>
	/// <returns>A MailAttachment object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	MailAttachment Load(IdType mailAttachmentId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(MailAttachment instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailAttachment GetDataObjectFromReader(MailAttachment data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailAttachment GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailAttachment GetDataObjectFromReader(IDataReader dataReader, MailAttachmentDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailAttachment GetDataObjectFromReader(MailAttachment data, IDataReader dataReader, MailAttachmentDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(MailAttachment data);


	/// <summary>
	/// Updates a record in the MailAttachment table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(MailAttachment data);

	/// <summary>
	/// Deletes a record from the MailAttachment table by MailAttachmentId.
	/// </summary>
	/// <param name="mailAttachmentId">A key field.</param>
	[Generate]
	void Delete(IdType mailAttachmentId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="mailMessageId">A field value to be matched.</param>
	/// <returns>The list of MailAttachmentDAO objects found.</returns>
	[Generate]
	MailAttachmentList FindByMailMessageId(IdType mailMessageId);

	//}
    }
}
