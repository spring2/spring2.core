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
    /// Data access class for MenuLinkGroup business entity.
    /// </summary>
    public class MenuLinkGroupDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly MenuLinkGroupDAO DAO = new MenuLinkGroupDAO();
	private static readonly String VIEW = "vwMenuLinkGroup";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 MenuLinkGroupId;
	    public Int32 Name;

	    internal ColumnOrdinals(IDataReader reader) {
		MenuLinkGroupId = reader.GetOrdinal("MenuLinkGroupId");
		Name = reader.GetOrdinal("Name");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		MenuLinkGroupId = reader.GetOrdinal(prefix + "MenuLinkGroupId");
		Name = reader.GetOrdinal(prefix + "Name");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MenuLinkGroupDAO() {
	    AddPropertyMapping("MenuLinkGroupId", @"MenuLinkGroupId");
	    AddPropertyMapping("Name", @"Name");
	}

	private MenuLinkGroupDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all MenuLinkGroup rows.
	/// </summary>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkGroupList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MenuLinkGroup rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkGroupList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of MenuLinkGroup rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkGroupList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLinkGroup rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkGroupList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MenuLinkGroupList list = new MenuLinkGroupList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all MenuLinkGroup rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkGroupList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MenuLinkGroup rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkGroupList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MenuLinkGroup rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkGroupList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLinkGroup rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLinkGroup objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkGroupList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MenuLinkGroupList list = new MenuLinkGroupList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a MenuLinkGroup entity using it's primary key.
	/// </summary>
	/// <param name="MenuLinkGroupId">A key field.</param>
	/// <returns>A MenuLinkGroup object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MenuLinkGroup Load(IdType menuLinkGroupId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkGroupId", EqualityOperatorEnum.Equal, menuLinkGroupId.IsValid ? menuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MenuLinkGroup instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkGroupId", EqualityOperatorEnum.Equal, instance.MenuLinkGroupId.IsValid ? instance.MenuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MenuLinkGroup.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private MenuLinkGroupList GetList(IDataReader reader) {
	    MenuLinkGroupList list = new MenuLinkGroupList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLinkGroup GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLinkGroup GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MenuLinkGroup data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkGroup GetDataObjectFromReader(MenuLinkGroup data, IDataReader dataReader) {
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
	internal MenuLinkGroup GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MenuLinkGroup data = new MenuLinkGroup(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkGroup GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MenuLinkGroup data = new MenuLinkGroup(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLinkGroup GetDataObjectFromReader(MenuLinkGroup data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.MenuLinkGroupId)) {
		data.MenuLinkGroupId = IdType.UNSET;
	    } else {
		data.MenuLinkGroupId = new IdType(dataReader.GetInt32(ordinals.MenuLinkGroupId));
	    }
	    if (dataReader.IsDBNull(ordinals.Name)) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(ordinals.Name));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MenuLinkGroup table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MenuLinkGroup data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the MenuLinkGroup table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MenuLinkGroup data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLinkGroup_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Name", DbType.AnsiString, ParameterDirection.Input, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));

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
	/// Updates a record in the MenuLinkGroup table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MenuLinkGroup data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the MenuLinkGroup table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MenuLinkGroup data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLinkGroup_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkGroupId", DbType.Int32, ParameterDirection.Input, data.MenuLinkGroupId.IsValid ? data.MenuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Name", DbType.AnsiString, ParameterDirection.Input, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));

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
	/// Deletes a record from the MenuLinkGroup table by MenuLinkGroupId.
	/// </summary>
	/// <param name="MenuLinkGroupId">A key field.</param>
	public void Delete(IdType menuLinkGroupId) {
	    Delete(menuLinkGroupId, null);
	}

	/// <summary>
	/// Deletes a record from the MenuLinkGroup table by MenuLinkGroupId.
	/// </summary>
	/// <param name="MenuLinkGroupId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType menuLinkGroupId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLinkGroup_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkGroupId", DbType.Int32, ParameterDirection.Input, menuLinkGroupId.IsValid ? menuLinkGroupId.ToInt32() as Object : DBNull.Value));
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
	/// <param name="Name">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkGroup FindByName(StringType name) {
	    OrderByClause sort = new OrderByClause("Name");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Name", EqualityOperatorEnum.Equal, name.IsValid ? name.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetDataObject(dataReader);
	}

	#region Custom Code
	#endregion
    }
}
