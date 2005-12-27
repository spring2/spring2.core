using System;
using System.Collections;
using System.Configuration;
using System.Data;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;

namespace Spring2.Core.ResourceManager.Dao {
    public class ResourceDAO : Spring2.Core.DAO.SqlEntityDAO {


	public static readonly ResourceDAO DAO = new ResourceDAO(); 
	private static readonly String VIEW = "vwResource";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;
	private static Hashtable propertyToSqlMap = new Hashtable();

	static ResourceDAO() {
	    if (!propertyToSqlMap.Contains("ResourceId")) {
		propertyToSqlMap.Add("ResourceId",@"ResourceId");
	    }
	    if (!propertyToSqlMap.Contains("Context")) {
		propertyToSqlMap.Add("Context",@"Context");
	    }
	    if (!propertyToSqlMap.Contains("Field")) {
		propertyToSqlMap.Add("Field",@"Field");
	    }
	    if (!propertyToSqlMap.Contains("Identity")) {
		propertyToSqlMap.Add("Identity",@"Identity");
	    }
	}

	private ResourceDAO() {}
	
	/// <summary>
	/// Creates a where clause object by mapping the given where clause text.  The text may reference
	/// entity properties which will be mapped to sql code by enclosing the property names in braces.
	/// </summary>
	/// <param name="whereText">Text to be mapped</param>
	/// <returns>WhereClause object.</returns>
	/// <exception cref="ApplicationException">When property name found in braces is not found in the entity.</exception>
	public static IWhere Where(String whereText) {
	    return new WhereClause(ProcessExpression(propertyToSqlMap, whereText));
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, String value) {
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, Int32 value) {
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	/// <summary>
	/// Creates a where clause object that can be used to create sql to find objects whose entity property value
	/// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped 
	/// to the appropriate sql.
	/// </summary>
	/// <param name="propertyName">Entity property to be matched.</param>
	/// <param name="value">Value to match the property with</param>
	/// <returns>A WhereClause object.</returns>
	/// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
	public static IWhere Where(String propertyName, DateTime value)	{
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
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
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(IWhere whereClause) { 
	    return GetList(whereClause, null);
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
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(IWhere whereClause, IOrderBy orderByClause) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); 

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
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList(Int32 maxRows) { 
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Resource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(IWhere whereClause, Int32 maxRows) { 
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Resource rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public ResourceList GetList(IOrderBy orderByClause, Int32 maxRows) { 
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Resource rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Resource objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public ResourceList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) { 
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows); 

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
	    WhereClause w = new WhereClause();
	    w.And("ResourceId", resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Resource.");
	    }
	    Resource data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(Resource instance) {
	    WhereClause w = new WhereClause();
	    w.And("ResourceId", instance.ResourceId);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for Resource.");
	    }
	    Resource data = GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(Resource data, IDataReader dataReader) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(IDataReader dataReader) {
	    Resource data = new Resource(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(IDataReader dataReader, String prefix) {
	    Resource data = new Resource(false);
	    return GetDataObjectFromReader(data, dataReader, prefix);
	}
	
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static Resource GetDataObjectFromReader(Resource data, IDataReader dataReader, String prefix) {
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ResourceId"))) { 
		data.ResourceId = IdType.UNSET;
	    } else {
		data.ResourceId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ResourceId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Context"))) { 
		data.Context = StringType.UNSET;
	    } else {
		data.Context = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Context")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Field"))) { 
		data.Field = StringType.UNSET;
	    } else {
		data.Field = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Field")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Identity"))) { 
		data.Identity = IdType.UNSET;
	    } else {
		data.Identity = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Identity")));
	    }
	    return data;
	}


	/// <summary>
	/// Inserts a record into the Resource table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(Resource data) {
	    return Insert(data, null);
	}
	
	/// <summary>
	/// Inserts a record into the Resource table.
	/// </summary>
	/// <param name=""></param>
	public IdType Insert(Resource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.AnsiString, ParameterDirection.Input, data.Context.IsValid ? data.Context.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Field", DbType.AnsiString, ParameterDirection.Input, data.Field.IsValid ? data.Field.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Identity", DbType.Int32, ParameterDirection.Input, data.Identity.IsValid ? data.Identity.ToInt32() as Object : DBNull.Value));

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
	/// Updates a record in the Resource table.
	/// </summary>
	/// <param name=""></param>
	public void Update(Resource data) {
	    Update(data, null);
	}
	
	/// <summary>
	/// Updates a record in the Resource table.
	/// </summary>
	/// <param name=""></param>
	public void Update(Resource data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, data.ResourceId.IsValid ? data.ResourceId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Context", DbType.AnsiString, ParameterDirection.Input, data.Context.IsValid ? data.Context.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Field", DbType.AnsiString, ParameterDirection.Input, data.Field.IsValid ? data.Field.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Identity", DbType.Int32, ParameterDirection.Input, data.Identity.IsValid ? data.Identity.ToInt32() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}


	/// <summary>
	/// Deletes a record from the Resource table by ResourceId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType resourceId) {
	    Delete(resourceId, null);
	}
	
	/// <summary>
	/// Deletes a record from the Resource table by ResourceId.
	/// </summary>
	/// <param name=""></param>
	public void Delete(IdType resourceId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spResource_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@ResourceId", DbType.Int32, ParameterDirection.Input, resourceId.IsValid ? resourceId.ToInt32() as Object : DBNull.Value));

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
	    
	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="Context">A field value to be matched.</param>
	/// <param name="Field">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public Resource FindByContextAndField(StringType context, StringType field) {
	    OrderByClause sort = new OrderByClause("Context, Field");
	    WhereClause filter = new WhereClause();
	    filter.And("Context", context.IsValid ? context.ToString() as Object : DBNull.Value);
	    filter.And("Field", field.IsValid ? field.ToString() as Object : DBNull.Value);
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Resource.FindByContextAndField found no rows.");
	    }
	    Resource data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}
    }
}
