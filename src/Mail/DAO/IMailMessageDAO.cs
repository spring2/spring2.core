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
    /// Data access interface for MailMessage business entity.
    /// </summary>
    public interface IMailMessageDAO {

	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageList GetList();

	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageList GetList(SqlFilter filter);

	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageList GetList(IOrderBy orderByClause);

	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageList GetList(SqlFilter filter, IOrderBy orderByClause);

	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageList GetList(Int32 maxRows);

	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageList GetList(SqlFilter filter, Int32 maxRows);

	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessageList GetList(IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	[Generate]
	MailMessageList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows);

	/// <summary>
	/// Finds a MailMessage entity using it's primary key.
	/// </summary>
	/// <param name="mailMessageId">A key field.</param>
	/// <returns>A MailMessage object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	[Generate]
	MailMessage Load(IdType mailMessageId);

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	[Generate]
	void Reload(MailMessage instance);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessage GetDataObjectFromReader(IDataReader dataReader);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessage GetDataObjectFromReader(IDataReader dataReader, MailMessageDAO.ColumnOrdinals ordinals);

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	[Generate]
	MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader, MailMessageDAO.ColumnOrdinals ordinals);


	/// <summary>
	/// Inserts a record into the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	IdType Insert(MailMessage data);


	/// <summary>
	/// Updates a record in the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	[Generate]
	void Update(MailMessage data);

	/// <summary>
	/// Deletes a record from the MailMessage table by MailMessageId.
	/// </summary>
	/// <param name="mailMessageId">A key field.</param>
	[Generate]
	void Delete(IdType mailMessageId);


	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="mailMessageStatus">A field value to be matched.</param>
	/// <returns>The list of MailMessageDAO objects found.</returns>
	[Generate]
	MailMessageList FindByStatus(MailMessageStatusEnum mailMessageStatus);

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="uniqueKey">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	[Generate]
	MailMessage FindByUniqueKey(StringType uniqueKey);

	//}
    }
}
