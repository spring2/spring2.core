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
    /// Data access class for MenuLink business entity.
    /// </summary>
    public class MenuLinkDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly MenuLinkDAO DAO = new MenuLinkDAO();
	private static readonly String VIEW = "vwMenuLink";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public String Prefix = String.Empty;
	    public Int32 MenuLinkId;
	    public Int32 Name;
	    public Int32 Target;
	    public Int32 Active;
	    public Int32 MenuLinkGroupId;
	    public Int32 ParentMenuLinkId;
	    public Int32 EffectiveDate;
	    public Int32 ExpirationDate;

	    internal ColumnOrdinals(IDataReader reader) {
		MenuLinkId = reader.GetOrdinal("MenuLinkId");
		Name = reader.GetOrdinal("Name");
		Target = reader.GetOrdinal("Target");
		Active = reader.GetOrdinal("Active");
		MenuLinkGroupId = reader.GetOrdinal("MenuLinkGroupId");
		ParentMenuLinkId = reader.GetOrdinal("ParentMenuLinkId");
		EffectiveDate = reader.GetOrdinal("EffectiveDate");
		ExpirationDate = reader.GetOrdinal("ExpirationDate");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		Prefix = prefix;
		MenuLinkId = reader.GetOrdinal(prefix + "MenuLinkId");
		Name = reader.GetOrdinal(prefix + "Name");
		Target = reader.GetOrdinal(prefix + "Target");
		Active = reader.GetOrdinal(prefix + "Active");
		MenuLinkGroupId = reader.GetOrdinal(prefix + "MenuLinkGroupId");
		ParentMenuLinkId = reader.GetOrdinal(prefix + "ParentMenuLinkId");
		EffectiveDate = reader.GetOrdinal(prefix + "EffectiveDate");
		ExpirationDate = reader.GetOrdinal(prefix + "ExpirationDate");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MenuLinkDAO() {
	    AddPropertyMapping("MenuLinkId", @"MenuLinkId");
	    AddPropertyMapping("Name", @"Name");
	    AddPropertyMapping("Target", @"Target");
	    AddPropertyMapping("Active", @"Active");
	    AddPropertyMapping("MenuLinkGroupId", @"MenuLinkGroupId");
	    AddPropertyMapping("ParentMenuLinkId", @"ParentMenuLinkId");
	    AddPropertyMapping("EffectiveDate", @"EffectiveDate");
	    AddPropertyMapping("ExpirationDate", @"ExpirationDate");
	}

	private MenuLinkDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all MenuLink rows.
	/// </summary>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of MenuLink rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of MenuLink rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLink rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MenuLinkList list = new MenuLinkList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all MenuLink rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of MenuLink rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of MenuLink rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MenuLinkList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of MenuLink rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MenuLink objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MenuLinkList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MenuLinkList list = new MenuLinkList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a MenuLink entity using it's primary key.
	/// </summary>
	/// <param name="menuLinkId">A key field.</param>
	/// <returns>A MenuLink object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MenuLink Load(IdType menuLinkId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkId", EqualityOperatorEnum.Equal, menuLinkId.IsValid ? menuLinkId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MenuLink instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkId", EqualityOperatorEnum.Equal, instance.MenuLinkId.IsValid ? instance.MenuLinkId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MenuLink.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private MenuLinkList GetList(IDataReader reader) {
	    MenuLinkList list = new MenuLinkList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLink GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MenuLink GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MenuLink data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLink GetDataObjectFromReader(MenuLink data, IDataReader dataReader) {
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
	internal MenuLink GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MenuLink data = new MenuLink(false);
	    return GetDataObjectFromReader(data, dataReader, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLink GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MenuLink data = new MenuLink(false);
	    return GetDataObjectFromReader(data, dataReader, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data">Entity to be populated from data reader</param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals">An instance of ColumnOrdinals initialized for this data reader</param>
	/// <returns>Data object built from current row.</returns>
	internal MenuLink GetDataObjectFromReader(MenuLink data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.MenuLinkId)) {
		data.MenuLinkId = IdType.UNSET;
	    } else {
		data.MenuLinkId = new IdType(dataReader.GetInt32(ordinals.MenuLinkId));
	    }
	    if (dataReader.IsDBNull(ordinals.Name)) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(ordinals.Name));
	    }
	    if (dataReader.IsDBNull(ordinals.Target)) {
		data.Target = StringType.UNSET;
	    } else {
		data.Target = StringType.Parse(dataReader.GetString(ordinals.Target));
	    }
	    if (dataReader.IsDBNull(ordinals.Active)) {
		data.Active = BooleanType.UNSET;
	    } else {
		data.Active = BooleanType.GetInstance(dataReader.GetBoolean(ordinals.Active));
	    }
	    if (dataReader.IsDBNull(ordinals.MenuLinkGroupId)) {
		data.MenuLinkGroupId = IdType.UNSET;
	    } else {
		data.MenuLinkGroupId = new IdType(dataReader.GetInt32(ordinals.MenuLinkGroupId));
	    }
	    if (dataReader.IsDBNull(ordinals.ParentMenuLinkId)) {
		data.ParentMenuLinkId = IdType.UNSET;
	    } else {
		data.ParentMenuLinkId = new IdType(dataReader.GetInt32(ordinals.ParentMenuLinkId));
	    }
	    if (dataReader.IsDBNull(ordinals.EffectiveDate)) {
		data.EffectiveDate = DateTimeType.UNSET;
	    } else {
		data.EffectiveDate = new DateTimeType(dataReader.GetDateTime(ordinals.EffectiveDate));
	    }
	    if (dataReader.IsDBNull(ordinals.ExpirationDate)) {
		data.ExpirationDate = DateTimeType.UNSET;
	    } else {
		data.ExpirationDate = new DateTimeType(dataReader.GetDateTime(ordinals.ExpirationDate));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the MenuLink table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MenuLink data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the MenuLink table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MenuLink data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLink_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Name", DbType.AnsiString, ParameterDirection.Input, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Target", DbType.AnsiString, ParameterDirection.Input, data.Target.IsValid ? data.Target.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Active", DbType.Boolean, ParameterDirection.Input, data.Active.IsValid ? data.Active.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkGroupId", DbType.Int32, ParameterDirection.Input, data.MenuLinkGroupId.IsValid ? data.MenuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ParentMenuLinkId", DbType.Int32, ParameterDirection.Input, data.ParentMenuLinkId.IsValid ? data.ParentMenuLinkId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@EffectiveDate", DbType.DateTime, ParameterDirection.Input, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ExpirationDate", DbType.DateTime, ParameterDirection.Input, data.ExpirationDate.IsValid ? data.ExpirationDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}


	/// <summary>
	/// Updates a record in the MenuLink table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MenuLink data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the MenuLink table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MenuLink data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLink_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkId", DbType.Int32, ParameterDirection.Input, data.MenuLinkId.IsValid ? data.MenuLinkId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Name", DbType.AnsiString, ParameterDirection.Input, data.Name.IsValid ? data.Name.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Target", DbType.AnsiString, ParameterDirection.Input, data.Target.IsValid ? data.Target.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Active", DbType.Boolean, ParameterDirection.Input, data.Active.IsValid ? data.Active.ToBoolean() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkGroupId", DbType.Int32, ParameterDirection.Input, data.MenuLinkGroupId.IsValid ? data.MenuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ParentMenuLinkId", DbType.Int32, ParameterDirection.Input, data.ParentMenuLinkId.IsValid ? data.ParentMenuLinkId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@EffectiveDate", DbType.DateTime, ParameterDirection.Input, data.EffectiveDate.IsValid ? data.EffectiveDate.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ExpirationDate", DbType.DateTime, ParameterDirection.Input, data.ExpirationDate.IsValid ? data.ExpirationDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the MenuLink table by MenuLinkId.
	/// </summary>
	/// <param name="menuLinkId">A key field.</param>
	public void Delete(IdType menuLinkId) {
	    Delete(menuLinkId, null);
	}

	/// <summary>
	/// Deletes a record from the MenuLink table by MenuLinkId.
	/// </summary>
	/// <param name="menuLinkId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType menuLinkId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMenuLink_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MenuLinkId", DbType.Int32, ParameterDirection.Input, menuLinkId.IsValid ? menuLinkId.ToInt32() as Object : DBNull.Value));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="menuLinkGroupId">A field value to be matched.</param>
	/// <returns>The list of MenuLinkDAO objects found.</returns>
	public MenuLinkList FindByMenuLinkGroup(IdType menuLinkGroupId) {
	    OrderByClause sort = new OrderByClause("MenuLinkGroupId");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MenuLinkGroupId", EqualityOperatorEnum.Equal, menuLinkGroupId.IsValid ? menuLinkGroupId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="parentMenuLinkId">A field value to be matched.</param>
	/// <returns>The list of MenuLinkDAO objects found.</returns>
	public MenuLinkList FindByParentMenuLink(IdType parentMenuLinkId) {
	    OrderByClause sort = new OrderByClause("ParentMenuLinkId");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("ParentMenuLinkId", EqualityOperatorEnum.Equal, parentMenuLinkId.IsValid ? parentMenuLinkId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}
    }
}
