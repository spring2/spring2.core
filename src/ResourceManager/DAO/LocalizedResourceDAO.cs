using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Dao {
    public class LocalizedResourceDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly LocalizedResourceDAO DAO = new LocalizedResourceDAO();
	private static readonly String VIEW = "vwLocalizedResource";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	//These are used to help with population from the DB.
	private ILocaleFactory localeFactory = null;
	private ILanguageFactory languageFactory = null;

	public ILocaleFactory LocaleFactory {
	    set { localeFactory = value; }
	}
	public ILanguageFactory LanguageFactory {
	    set { languageFactory = value; }
	}

	internal sealed class ColumnOrdinals {
	    public Int32 LocalizedResourceId;
	    public Int32 ResourceId;
	    public Int32 Locale;
	    public Int32 Language;
	    public Int32 Content;
	    public Int32 Resource_ResourceId;
	    public Int32 Resource_Context;
	    public Int32 Resource_Field;
	    public Int32 Resource_ContextIdentity;

	    internal ColumnOrdinals(IDataReader reader) {
		LocalizedResourceId = reader.GetOrdinal("LocalizedResourceId");
		ResourceId = reader.GetOrdinal("ResourceId");
		Locale = reader.GetOrdinal("Locale");
		Language = reader.GetOrdinal("Language");
		Content = reader.GetOrdinal("Content");
		Resource_ResourceId = reader.GetOrdinal("Resource_ResourceId");
		Resource_Context = reader.GetOrdinal("Resource_Context");
		Resource_Field = reader.GetOrdinal("Resource_Field");
		Resource_ContextIdentity = reader.GetOrdinal("Resource_ContextIdentity");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		LocalizedResourceId = reader.GetOrdinal(prefix + "LocalizedResourceId");
		ResourceId = reader.GetOrdinal(prefix + "ResourceId");
		Locale = reader.GetOrdinal(prefix + "Locale");
		Language = reader.GetOrdinal(prefix + "Language");
		Content = reader.GetOrdinal(prefix + "Content");
		Resource_ResourceId = reader.GetOrdinal(prefix + "Resource_ResourceId");
		Resource_Context = reader.GetOrdinal(prefix + "Resource_Context");
		Resource_Field = reader.GetOrdinal(prefix + "Resource_Field");
		Resource_ContextIdentity = reader.GetOrdinal(prefix + "Resource_ContextIdentity");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static LocalizedResourceDAO() {
	    AddPropertyMapping("LocalizedResourceId", @"LocalizedResourceId");
	    AddPropertyMapping("ResourceId", @"ResourceId");
	    AddPropertyMapping("Locale", @"Locale");
	    AddPropertyMapping("Language", @"Language");
	    AddPropertyMapping("Content", @"Content");
	    AddPropertyMapping("Resource.ResourceId", @"Resource_ResourceId");
	    AddPropertyMapping("Resource.Context", @"Resource_Context");
	    AddPropertyMapping("Resource.Field", @"Resource_Field");
	    AddPropertyMapping("Resource.Identity", @"Resource_ContextIdentity");
	}

	private LocalizedResourceDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all LocalizedResource rows.
	/// </summary>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of LocalizedResource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    LocalizedResourceList list = new LocalizedResourceList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all LocalizedResource rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of LocalizedResource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResourceList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of LocalizedResource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of LocalizedResource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public LocalizedResourceList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    LocalizedResourceList list = new LocalizedResourceList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a LocalizedResource entity using it's primary key.
	/// </summary>
	/// <param name="LocalizedResourceId">A key field.</param>
	/// <returns>A LocalizedResource object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public LocalizedResource Load(IdType localizedResourceId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("LocalizedResourceId", EqualityOperatorEnum.Equal, localizedResourceId.IsValid ? localizedResourceId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(LocalizedResource instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("LocalizedResourceId", EqualityOperatorEnum.Equal, instance.LocalizedResourceId.IsValid ? instance.LocalizedResourceId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for LocalizedResource.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private LocalizedResourceList GetList(IDataReader reader) {
	    LocalizedResourceList list = new LocalizedResourceList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private LocalizedResource GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private LocalizedResource GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    LocalizedResource data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(LocalizedResource data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(LocalizedResource data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    LocalizedResource data = new LocalizedResource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    LocalizedResource data = new LocalizedResource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    LocalizedResource data = new LocalizedResource(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal LocalizedResource GetDataObjectFromReader(LocalizedResource data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.LocalizedResourceId)) {
		data.LocalizedResourceId = IdType.UNSET;
	    } else {
		data.LocalizedResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("LocalizedResourceId")));
	    }
	    if (dataReader.IsDBNull(ordinals.ResourceId)) {
		data.ResourceId = IdType.UNSET;
	    } else {
		data.ResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ResourceId")));
	    }
	    if (dataReader.IsDBNull(ordinals.Locale)) {
		data.Locale = null;
	    } else {
		data.Locale = this.localeFactory.GetLocale(dataReader.GetString(dataReader.GetOrdinal("Locale")));
	    }
	    if (dataReader.IsDBNull(ordinals.Language)) {
		data.Language = null;
	    } else {
		data.Language = this.languageFactory.GetLanguage(dataReader.GetString(dataReader.GetOrdinal("Language")));
	    }
	    if (dataReader.IsDBNull(ordinals.Content)) {
		data.Content = StringType.UNSET;
	    } else {
		data.Content = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Content")));
	    }
	    if (dataReader.IsDBNull(ordinals.Resource_ResourceId)) {
		data.Resource.ResourceId = IdType.UNSET;
	    } else {
		data.Resource.ResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Resource_ResourceId")));
	    }
	    if (dataReader.IsDBNull(ordinals.Resource_Context)) {
		data.Resource.Context = StringType.UNSET;
	    } else {
		data.Resource.Context = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Resource_Context")));
	    }
	    if (dataReader.IsDBNull(ordinals.Resource_Field)) {
		data.Resource.Field = StringType.UNSET;
	    } else {
		data.Resource.Field = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Resource_Field")));
	    }
	    if (dataReader.IsDBNull(ordinals.Resource_ContextIdentity)) {
		data.Resource.Identity = IdType.UNSET;
	    } else {
		data.Resource.Identity = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Resource_ContextIdentity")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the LocalizedResource table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(LocalizedResource data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the LocalizedResource table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(LocalizedResource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Locale", DbType.AnsiString, ParameterDirection.Input, data.Locale.Code));
	    cmd.Parameters.Add(CreateDataParameter("@Language", DbType.AnsiString, ParameterDirection.Input, data.Language.Code));
	    cmd.Parameters.Add(CreateDataParameter("@Content", DbType.AnsiString, ParameterDirection.Input, data.Content.IsValid ? data.Content.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}


	/// <summary>
	/// Updates a record in the LocalizedResource table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(LocalizedResource data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the LocalizedResource table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(LocalizedResource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@LocalizedResourceId", DbType.Int32, ParameterDirection.Input, data.LocalizedResourceId.IsValid ? data.LocalizedResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Locale", DbType.AnsiString, ParameterDirection.Input, data.Locale.Code));
	    cmd.Parameters.Add(CreateDataParameter("@Language", DbType.AnsiString, ParameterDirection.Input, data.Language.Code));
	    cmd.Parameters.Add(CreateDataParameter("@Content", DbType.AnsiString, ParameterDirection.Input, data.Content.IsValid ? data.Content.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the LocalizedResource table by LocalizedResourceId.
	/// </summary>
	/// <param name="LocalizedResourceId">A key field.</param>
	public void Delete(IdType localizedResourceId) {
	    Delete(localizedResourceId, null);
	}

	/// <summary>
	/// Deletes a record from the LocalizedResource table by LocalizedResourceId.
	/// </summary>
	/// <param name="LocalizedResourceId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType localizedResourceId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLocalizedResource_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@LocalizedResourceId", DbType.Int32, ParameterDirection.Input, localizedResourceId.IsValid ? localizedResourceId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
#if (NET_1_1)
	    if (transaction == null) {
#else
	    if (transaction == null && DbConnectionScope.Current == null) {
#endif
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="ResourceId">A field value to be matched.</param>
	/// <param name="Locale">A field value to be matched.</param>
	/// <param name="Language">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public LocalizedResource FindByResourceIdLocaleAndLanguage(IdType resourceId, ILocale locale, ILanguage language) {
	    OrderByClause sort = new OrderByClause("ResourceId, Locale, Language");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("ResourceId", EqualityOperatorEnum.Equal, resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Locale", EqualityOperatorEnum.Equal, locale.Code));
	    filter.And(new SqlEqualityPredicate("Language", EqualityOperatorEnum.Equal, language.Code));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="Context">A field value to be matched.</param>
	/// <returns>The list of ResourceDAO objects found.</returns>
	public LocalizedResourceList FindByResourceId(IdType resourceId) {
	    OrderByClause sort = new OrderByClause("ResourceId");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("ResourceId", EqualityOperatorEnum.Equal, resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    return GetList(dataReader);
	}
    }
}
