using Spring2.Core.DAO;
using Spring2.Core.PerformanceMonitor.BusinessLogic;
using Spring2.Core.PerformanceMonitor.DataObject;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DAO {
    public class PerformanceCounterDefinitionDAO : Spring2.Core.DAO.SqlEntityDAO {
	public static readonly PerformanceCounterDefinitionDAO DAO = new PerformanceCounterDefinitionDAO();
        
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
        
	private static readonly Int32 COMMAND_TIMEOUT = 15;

        private static readonly String VIEW = "PerformanceCounterDefinition";

	private static ColumnOrdinals columnOrdinals = null;
        
	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static PerformanceCounterDefinitionDAO() {
	    AddPropertyMapping("PerformanceCounterDefinitionId", @"PerformanceCounterDefinitionId");
            AddPropertyMapping("PerformanceMachineDefinitionId", @"PerformanceMachineDefinitionId");
            AddPropertyMapping("CategoryName", @"CategoryName");
            AddPropertyMapping("CounterName", @"CounterName");
            AddPropertyMapping("InstanceName", @"InstanceName");
            AddPropertyMapping("CalculationType", @"CalculationType");
	}

        private PerformanceCounterDefinitionDAO() { }
        
	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}
        
	/// <summary>
	/// Returns a list of all PerformanceCounterDefinition rows.
	/// </summary>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public List<PerformanceCounterDefinition> GetList() {
	    return GetList(null, null);
	}
        
	/// <summary>
        /// Returns a filtered list of PerformanceCounterDefinition rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public List<PerformanceCounterDefinition> GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}
        
	/// <summary>
        /// Returns an ordered list of PerformanceCounterDefinition rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public List<PerformanceCounterDefinition> GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}
        
	/// <summary>
        /// Returns an ordered and filtered list of PerformanceCounterDefinition rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public List<PerformanceCounterDefinition> GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    List<PerformanceCounterDefinition> list = new List<PerformanceCounterDefinition>();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}
        
	/// <summary>
        /// Returns a list of all PerformanceCounterDefinition rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public List<PerformanceCounterDefinition> GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}
        
	/// <summary>
        /// Returns a filtered list of PerformanceCounterDefinition rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public List<PerformanceCounterDefinition> GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}
        
	/// <summary>
        /// Returns an ordered list of PerformanceCounterDefinition rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public List<PerformanceCounterDefinition> GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}
        
	/// <summary>
        /// Returns an ordered and filtered list of PerformanceCounterDefinition rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceCounterDefinition objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public List<PerformanceCounterDefinition> GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    List<PerformanceCounterDefinition> list = new List<PerformanceCounterDefinition>();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}
        
	/// <summary>
        /// Finds a PerformanceCounterDefinition entity using it's primary key.
	/// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        /// <returns>A PerformanceCounterDefinition object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
        public PerformanceCounterDefinition Load(Int32? performanceCounterDefinitionId) {
	    SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceCounterDefinitionId", EqualityOperatorEnum.Equal, performanceCounterDefinitionId));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}
        
	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
        public void Reload(PerformanceCounterDefinition instance) {
	    SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceCounterDefinitionId", EqualityOperatorEnum.Equal, instance.PerformanceCounterDefinitionId));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
                throw new FinderException("Reload found no rows for PerformanceCounterDefinition.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}
        
	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private List<PerformanceCounterDefinition> GetList(IDataReader reader) {
	    List<PerformanceCounterDefinition> list = new List<PerformanceCounterDefinition>();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}
        
	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
        private PerformanceCounterDefinition GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}
        
	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
        private PerformanceCounterDefinition GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
            PerformanceCounterDefinition data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(PerformanceCounterDefinition data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(PerformanceCounterDefinition data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
            PerformanceCounterDefinition data = new PerformanceCounterDefinition(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
            PerformanceCounterDefinition data = new PerformanceCounterDefinition(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            PerformanceCounterDefinition data = new PerformanceCounterDefinition(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="data"></param>
	/// <param name="dataReader">Container for database row.</param>
	/// <param name="prefix"></param>
	/// <param name="ordinals"></param>
	/// <returns>Data object built from current row.</returns>
        internal static PerformanceCounterDefinition GetDataObjectFromReader(PerformanceCounterDefinition data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.PerformanceCounterDefinitionId)) {
                data.PerformanceCounterDefinitionId = null;
	    } else {
		data.PerformanceCounterDefinitionId = dataReader.GetInt32(ordinals.PerformanceCounterDefinitionId);
	    }
            if (dataReader.IsDBNull(ordinals.PerformanceMachineDefinitionId)) {
                data.PerformanceMachineDefinitionId = null;
            } else {
                data.PerformanceMachineDefinitionId = dataReader.GetInt32(ordinals.PerformanceMachineDefinitionId);
            }
            if (dataReader.IsDBNull(ordinals.CategoryName)) {
                data.CategoryName = null;
            } else {
                data.CategoryName = dataReader.GetString(ordinals.CategoryName);
            }
            if (dataReader.IsDBNull(ordinals.CounterName)) {
                data.CounterName = null;
            } else {
                data.CounterName = dataReader.GetString(ordinals.CounterName);
            }
            if (dataReader.IsDBNull(ordinals.InstanceName)) {
                data.InstanceName = null;
            } else {
                data.InstanceName = dataReader.GetString(ordinals.InstanceName);
            }
            if (dataReader.IsDBNull(ordinals.CalculationType)) {
                data.CalculationType = null;
            } else {
                data.CalculationType = PerformanceCounterCalculationTypeEnum.GetInstance(dataReader.GetString(ordinals.CalculationType));
            }
            return data;
	}
        
	/// <summary>
	/// Inserts a record into the PerformanceCounterDefinition table.
	/// </summary>
	/// <param name="data"></param>
        public Int32 Insert(PerformanceCounterDefinition data) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceCounterDefinition_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@PerformanceMachineDefinitionId", DbType.Int32, ParameterDirection.Input, data.PerformanceMachineDefinitionId));
	    cmd.Parameters.Add(CreateDataParameter("@CategoryName", DbType.AnsiString, ParameterDirection.Input, data.CategoryName));
	    cmd.Parameters.Add(CreateDataParameter("@CounterName", DbType.AnsiString, ParameterDirection.Input, data.CounterName));
            cmd.Parameters.Add(CreateDataParameter("@InstanceName", DbType.AnsiString, ParameterDirection.Input, data.InstanceName));
            cmd.Parameters.Add(CreateDataParameter("@CalculationType", DbType.AnsiString, ParameterDirection.Input, data.CalculationType.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return (Int32)idParam.Value;
	}
        
	/// <summary>
        /// Updates a record in the PerformanceCounterDefinition table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(PerformanceCounterDefinition data) {
	    // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceCounterDefinition_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@PerformanceCounterDefinitionId", DbType.Int32, ParameterDirection.Input, data.PerformanceCounterDefinitionId));
            cmd.Parameters.Add(CreateDataParameter("@PerformanceMachineDefinitionId", DbType.Int32, ParameterDirection.Input, data.PerformanceMachineDefinitionId));
            cmd.Parameters.Add(CreateDataParameter("@CategoryName", DbType.AnsiString, ParameterDirection.Input, data.CategoryName));
            cmd.Parameters.Add(CreateDataParameter("@CounterName", DbType.AnsiString, ParameterDirection.Input, data.CounterName));
            cmd.Parameters.Add(CreateDataParameter("@InstanceName", DbType.AnsiString, ParameterDirection.Input, data.InstanceName));
            cmd.Parameters.Add(CreateDataParameter("@CalculationType", DbType.AnsiString, ParameterDirection.Input, data.CalculationType.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}
        
	/// <summary>
        /// Deletes a record from the PerformanceCounterDefinition table by PerformanceCounterDefinitionId.
	/// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        public void Delete(Int32? performanceCounterDefinitionId) {
	    // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceCounterDefinition_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

	    // Create and append the parameters
            cmd.Parameters.Add(CreateDataParameter("@PerformanceCounterDefinitionId", DbType.Int32, ParameterDirection.Input, performanceCounterDefinitionId));
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (DbConnectionScope.Current == null) {
		cmd.Connection.Close();
	    }
	}
        
	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
        /// <param name="performanceMachineDefinitionId">A field value to be matched.</param>
        /// <returns>The list of PerformanceCounterDefinition objects found.</returns>
	public List<PerformanceCounterDefinition> FindByPerformanceMachineDefinitionId(String performanceMachineDefinitionId) {
            OrderByClause sort = new OrderByClause("PerformanceCounterDefinitionId");
	    SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceMachineDefinitionId", EqualityOperatorEnum.Equal, performanceMachineDefinitionId));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    return GetList(dataReader);
	}
        
	public sealed class ColumnOrdinals {
            public Int32 PerformanceCounterDefinitionId;
            public Int32 PerformanceMachineDefinitionId;
            public Int32 CategoryName;
            public Int32 CounterName;
            public Int32 InstanceName;
            public Int32 CalculationType;
            
	    internal ColumnOrdinals(IDataReader reader) {
                Init(reader, "");
	    }
            
	    internal ColumnOrdinals(IDataReader reader, String prefix) {
                Init(reader, prefix);
            }
             
            private void Init(IDataReader reader, String prefix) {
                PerformanceCounterDefinitionId = reader.GetOrdinal("PerformanceCounterDefinitionId");
                PerformanceMachineDefinitionId = reader.GetOrdinal("PerformanceMachineDefinitionId");
                CategoryName = reader.GetOrdinal("CategoryName");
                CounterName = reader.GetOrdinal("CounterName");
                InstanceName = reader.GetOrdinal("InstanceName");
                CalculationType = reader.GetOrdinal("CalculationType");
	    }
	}
    }
}
