using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.Navigation.BusinessLogic;
using Spring2.Core.Navigation.DataObject;

namespace Spring2.Core.Navigation.Dao {

    /// <summary>
    /// Data access class for MenuLinkKey business entity.
    /// </summary>
    public class MenuLinkKeyDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly MenuLinkKeyDAO DAO = new MenuLinkKeyDAO();
	private static readonly String VIEW = "vwMenuLinkKey";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 MenuLinkId;
	    public Int32 Key;

	    internal ColumnOrdinals(IDataReader reader) {
		MenuLinkId = reader.GetOrdinal("MenuLinkId");
		Key = reader.GetOrdinal("Key");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		MenuLinkId = reader.GetOrdinal(prefix + "MenuLinkId");
		Key = reader.GetOrdinal(prefix + "Key");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MenuLinkKeyDAO() {
	    AddPropertyMapping("MenuLinkId", @"MenuLinkId");
	    AddPropertyMapping("Key", @"Key");
	}

	private MenuLinkKeyDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all MenuLinkKey rows.
	/// </summary>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkKeyList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MenuLinkKey rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkKeyList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of MenuLinkKey rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkKeyList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLinkKey rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkKeyList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MenuLinkKeyList list = new MenuLinkKeyList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all MenuLinkKey rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkKeyList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MenuLinkKey rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkKeyList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MenuLinkKey rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkKeyList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLinkKey rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkKey objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkKeyList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MenuLinkKeyList list = new MenuLinkKeyList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a MenuLinkKey entity using it's primary key.
	/// </summary>
	/// <param name="MenuLinkId">A key field.</param>
	/// <param name="Key">A key field.</param>
	/// <returns>A MenuLinkKey object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MenuLinkKey Load(IdType menuLinkId, StringType key) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkId", EqualityOperatorEnum.Equal, menuLinkId.IsValid ? menuLinkId.ToInt32() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Key", EqualityOperatorEnum.Equal, key.IsValid ? key.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MenuLinkKey instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkId", EqualityOperatorEnum.Equal, instance.MenuLinkId.IsValid ? instance.MenuLinkId.ToInt32() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Key", EqualityOperatorEnum.Equal, instance.Key.IsValid ? instance.Key.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MenuLinkKey.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private MenuLinkKeyList GetList(IDataReader reader) {
	    MenuLinkKeyList list = new MenuLinkKeyList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLinkKey GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLinkKey GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MenuLinkKey data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkKey GetDataObjectFromReader(MenuLinkKey data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkKey GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MenuLinkKey data = new MenuLinkKey(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkKey GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MenuLinkKey data = new MenuLinkKey(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkKey GetDataObjectFromReader(MenuLinkKey data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.MenuLinkId)) {
		data.MenuLinkId = IdType.UNSET;
	    } else {
		data.MenuLinkId = new IdType(dataReader.GetInt32(ordinals.MenuLinkId));
	    }
	    if (dataReader.IsDBNull(ordinals.Key)) {
		data.Key = StringType.UNSET;
	    } else {
		data.Key = StringType.Parse(dataReader.GetString(ordinals.Key));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MenuLinkKey table.
	/// </summary>
	/// <param name="data"></param>
	public void Insert(MenuLinkKey data) {
	    Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the MenuLinkKey table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Insert(MenuLinkKey data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLinkKey_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);


	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkId", DbType.Int32, ParameterDirection.Input, data.MenuLinkId.IsValid ? data.MenuLinkId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Key", DbType.AnsiString, ParameterDirection.Input, data.Key.IsValid ? data.Key.ToString() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	}



	/// <summary>
	/// Deletes a record from the MenuLinkKey table by a composite primary key.
	/// </summary>
	/// <param name="MenuLinkId">A key field.</param>
	/// <param name="Key">A key field.</param>
	public void Delete(IdType menuLinkId, StringType key) {
	    Delete(menuLinkId, key, null);
	}

	/// <summary>
	/// Deletes a record from the MenuLinkKey table by a composite primary key.
	/// </summary>
	/// <param name="MenuLinkId">A key field.</param>
	/// <param name="Key">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType menuLinkId, StringType key, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLinkKey_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkId", DbType.Int32, ParameterDirection.Input, menuLinkId.IsValid ? menuLinkId.ToInt32() as Object : DBNull.Value));	    cmd.Parameters.Add(CreateDataParameter("@Key", DbType.AnsiString, ParameterDirection.Input, key.IsValid ? key.ToString() as Object : DBNull.Value));	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="MenuLinkId">A field value to be matched.</param>
	/// <returns>The list of MenuLinkKeyDAO objects found.</returns>
	public MenuLinkKeyList FindByMenuLinkId(IdType menuLinkId) {
	    OrderByClause sort = new OrderByClause("MenuLinkId");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkId", EqualityOperatorEnum.Equal, menuLinkId.IsValid ? menuLinkId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}
    }
}
