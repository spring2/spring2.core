using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Dao {
    public class ResourceDAO : Spring2.Core.DAO.SqlEntityDAO {

	public static readonly ResourceDAO DAO = new ResourceDAO();
	private static readonly String VIEW = "vwResource";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static ColumnOrdinals columnOrdinals = null;

	internal sealed class ColumnOrdinals {
	    public Int32 ResourceId;
	    public Int32 Context;
	    public Int32 Field;
	    public Int32 ContextIdentity;

	    internal ColumnOrdinals(IDataReader reader) {
		ResourceId = reader.GetOrdinal("ResourceId");
		Context = reader.GetOrdinal("Context");
		Field = reader.GetOrdinal("Field");
		ContextIdentity = reader.GetOrdinal("ContextIdentity");
	    }

	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		ResourceId = reader.GetOrdinal(prefix + "ResourceId");
		Context = reader.GetOrdinal(prefix + "Context");
		Field = reader.GetOrdinal(prefix + "Field");
		ContextIdentity = reader.GetOrdinal(prefix + "ContextIdentity");
	    }
	}

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static ResourceDAO() {
	    AddPropertyMapping("ResourceId", @"ResourceId");
	    AddPropertyMapping("Context", @"Context");
	    AddPropertyMapping("Field", @"Field");
	    AddPropertyMapping("Identity", @"ContextIdentity");
	}

	private ResourceDAO() {
	}

	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}

	/// <summary>
	/// Returns a list of all Resource rows.
	/// </summary>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Resource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}

	/// <summary>
	/// Returns an ordered list of Resource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Resource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    ResourceList list = new ResourceList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all Resource rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Resource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Resource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Resource rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    ResourceList list = new ResourceList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a Resource entity using it's primary key.
	/// </summary>
	/// <param name="ResourceId">A key field.</param>
	/// <returns>A Resource object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public Resource Load(IdType resourceId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("ResourceId", EqualityOperatorEnum.Equal, resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(Resource instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("ResourceId", EqualityOperatorEnum.Equal, instance.ResourceId.IsValid ? instance.ResourceId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for Resource.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private ResourceList GetList(IDataReader reader) {
	    ResourceList list = new ResourceList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private Resource GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}

	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private Resource GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    Resource data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(Resource data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(Resource data, IDataReader dataReader) {
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
	internal static Resource GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    Resource data = new Resource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    Resource data = new Resource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    Resource data = new Resource(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(Resource data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.ResourceId)) {
		data.ResourceId = IdType.UNSET;
	    } else {
		data.ResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ResourceId")));
	    }
	    if (dataReader.IsDBNull(ordinals.Context)) {
		data.Context = StringType.UNSET;
	    } else {
		data.Context = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Context")));
	    }
	    if (dataReader.IsDBNull(ordinals.Field)) {
		data.Field = StringType.UNSET;
	    } else {
		data.Field = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Field")));
	    }
	    if (dataReader.IsDBNull(ordinals.ContextIdentity)) {
		data.Identity = IdType.UNSET;
	    } else {
		data.Identity = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ContextIdentity")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the Resource table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(Resource data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Resource table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(Resource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.AnsiString, ParameterDirection.Input, data.Context.IsValid ? data.Context.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Field", DbType.AnsiString, ParameterDirection.Input, data.Field.IsValid ? data.Field.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ContextIdentity", DbType.Int32, ParameterDirection.Input, data.Identity.IsValid ? data.Identity.ToInt32() as Object : DBNull.Value));

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
	/// Updates a record in the Resource table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(Resource data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Resource table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(Resource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.AnsiString, ParameterDirection.Input, data.Context.IsValid ? data.Context.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Field", DbType.AnsiString, ParameterDirection.Input, data.Field.IsValid ? data.Field.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ContextIdentity", DbType.Int32, ParameterDirection.Input, data.Identity.IsValid ? data.Identity.ToInt32() as Object : DBNull.Value));

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
	/// Deletes a record from the Resource table by ResourceId.
	/// </summary>
	/// <param name="ResourceId">A key field.</param>
	public void Delete(IdType resourceId) {
	    Delete(resourceId, null);
	}

	/// <summary>
	/// Deletes a record from the Resource table by ResourceId.
	/// </summary>
	/// <param name="ResourceId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType resourceId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value));
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
	/// <param name="Context">A field value to be matched.</param>
	/// <param name="Field">A field value to be matched.</param>
	/// <param name="ContextIdentity">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public Resource FindByContextFieldAndIdentity(StringType context, StringType field, IdType identity) {
	    OrderByClause sort = new OrderByClause("Context, Field, ContextIdentity");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Context", EqualityOperatorEnum.Equal, context.IsValid ? context.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Field", EqualityOperatorEnum.Equal, field.IsValid ? field.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("ContextIdentity", EqualityOperatorEnum.Equal, identity.IsValid ? identity.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetDataObject(dataReader);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="Context">A field value to be matched.</param>
	/// <returns>The list of ResourceDAO objects found.</returns>
	public ResourceList FindByContext(StringType context) {
	    OrderByClause sort = new OrderByClause("Context");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Context", EqualityOperatorEnum.Equal, context.IsValid ? context.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="Context">A field value to be matched.</param>
	/// <param name="Field">A field value to be matched.</param>
	/// <returns>The list of ResourceDAO objects found.</returns>
	public ResourceList FindListByContextAndField(StringType context, StringType field) {
	    OrderByClause sort = new OrderByClause("Context, Field");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("Context", EqualityOperatorEnum.Equal, context.IsValid ? context.ToString() as Object : DBNull.Value));
	    filter.And(new SqlEqualityPredicate("Field", EqualityOperatorEnum.Equal, field.IsValid ? field.ToString() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);	

	    return GetList(dataReader);
	}

	#region Custom Code
	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="Context">A field value to be matched.</param>
	/// <param name="Field">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public Resource FindByContextAndField(StringType context, StringType field) {
	    return FindByContextFieldAndIdentity(context, field, IdType.UNSET);
	}

	/// <summary>
	/// Returns 
	/// </summary>
	/// <returns></returns>
	public StringTypeList FindUniqueContexts() {
	    String sql = "select distinct Context from " + VIEW;
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
#if (NET_1_1)
		IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
#else
		IDataReader dataReader = null;
	    if (DbConnectionScope.Current == null) {
			dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		} else {
			dataReader = cmd.ExecuteReader();
		}
#endif
	    StringTypeList list = new StringTypeList();
	    while(dataReader.Read()) {
		if(!dataReader.IsDBNull(dataReader.GetOrdinal("Context"))) {
		    list.Add(StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Context"))));
		}
	    }
	    return list;
	}

	/// <summary>
	/// Returns 
	/// </summary>
	/// <returns></returns>
	public StringTypeList FindUniqueFields(StringType context) {
	    String sql = "select distinct Field from " + VIEW + " where Context = @Context";
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.String, ParameterDirection.Input, context.Display()));
#if (NET_1_1)
		IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
#else
		IDataReader dataReader = null;
	    if (DbConnectionScope.Current == null) {
			dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		} else {
			dataReader = cmd.ExecuteReader();
		}
#endif
	    StringTypeList list = new StringTypeList();
	    while(dataReader.Read()) {
		if(!dataReader.IsDBNull(dataReader.GetOrdinal("Field"))) {
		    list.Add(StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Field"))));
		}
	    }
	    return list;
	}

	/// <summary>
	/// Returns 
	/// </summary>
	/// <returns></returns>
	public IdTypeList FindUniqueIdentities(StringType context, StringType field) {
	    String sql = "select distinct ContextIdentity from " + VIEW + " where Context = @Context and field = @Field";
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.String, ParameterDirection.Input, context.Display()));
	    cmd.Parameters.Add(CreateDataParameter("@Field", DbType.String, ParameterDirection.Input, field.Display()));
#if (NET_1_1)
		IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
#else
		IDataReader dataReader = null;
	    if (DbConnectionScope.Current == null) {
			dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
		} else {
			dataReader = cmd.ExecuteReader();
		}
#endif
	    IdTypeList list = new IdTypeList();
	    while(dataReader.Read()) {
		if(!dataReader.IsDBNull(dataReader.GetOrdinal("ContextIdentity"))) {
		    list.Add(new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ContextIdentity"))));
		}
	    }
	    return list;
	}

	#endregion
    }
}
