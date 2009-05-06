using Spring2.Core.DAO;
using Spring2.Core.PerformanceMonitor.BusinessLogic;
using Spring2.Core.PerformanceMonitor.DataObject;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DAO {
    public class PerformanceMachineDefinitionDAO : Spring2.Core.DAO.SqlEntityDAO {
        public static readonly PerformanceMachineDefinitionDAO DAO = new PerformanceMachineDefinitionDAO();

        private static readonly String CONNECTION_STRING_KEY = "ConnectionString";

        private static readonly Int32 COMMAND_TIMEOUT = 15;

        private static readonly String VIEW = "PerformanceMachineDefinition";

        private static ColumnOrdinals columnOrdinals = null;

        /// <summary>
        /// Initializes the static map of property names to sql expressions.
        /// </summary>
        static PerformanceMachineDefinitionDAO() {
            AddPropertyMapping("PerformanceMachineDefinitionId", @"PerformanceMachineDefinitionId");
            AddPropertyMapping("MachineName", @"MachineName");
            AddPropertyMapping("IntervalInSeconds", @"IntervalInSeconds");
            AddPropertyMapping("NumberOfSamples", @"NumberOfSamples");
        }

        private PerformanceMachineDefinitionDAO() { }

        protected override String ConnectionStringKey {
            get {
                return CONNECTION_STRING_KEY;
            }
        }

        /// <summary>
        /// Returns a list of all PerformanceMachineDefinition rows.
        /// </summary>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceMachineDefinition> GetList() {
            return GetList(null, null);
        }

        /// <summary>
        /// Returns a filtered list of PerformanceMachineDefinition rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceMachineDefinition> GetList(SqlFilter filter) {
            return GetList(filter, null);
        }

        /// <summary>
        /// Returns an ordered list of PerformanceMachineDefinition rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceMachineDefinition> GetList(IOrderBy orderByClause) {
            return GetList(null, orderByClause);
        }

        /// <summary>
        /// Returns an ordered and filtered list of PerformanceMachineDefinition rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceMachineDefinition> GetList(SqlFilter filter, IOrderBy orderByClause) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

            List<PerformanceMachineDefinition> list = new List<PerformanceMachineDefinition>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Returns a list of all PerformanceMachineDefinition rows.
        /// </summary>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceMachineDefinition> GetList(Int32 maxRows) {
            return GetList(null, null, maxRows);
        }

        /// <summary>
        /// Returns a filtered list of PerformanceMachineDefinition rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceMachineDefinition> GetList(SqlFilter filter, Int32 maxRows) {
            return GetList(filter, null, maxRows);
        }

        /// <summary>
        /// Returns an ordered list of PerformanceMachineDefinition rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceMachineDefinition> GetList(IOrderBy orderByClause, Int32 maxRows) {
            return GetList(null, orderByClause, maxRows);
        }

        /// <summary>
        /// Returns an ordered and filtered list of PerformanceMachineDefinition rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceMachineDefinition objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceMachineDefinition> GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

            List<PerformanceMachineDefinition> list = new List<PerformanceMachineDefinition>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Finds a PerformanceMachineDefinition entity using it's primary key.
        /// </summary>
        /// <param name="PerformanceMachineDefinitionId">A key field.</param>
        /// <returns>A PerformanceMachineDefinition object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
        public PerformanceMachineDefinition Load(Int32? PerformanceMachineDefinitionId) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceMachineDefinitionId", EqualityOperatorEnum.Equal, PerformanceMachineDefinitionId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);
            return GetDataObject(dataReader);
        }

        /// <summary>
        /// Repopulates an existing business entity instance
        /// </summary>
        public void Reload(PerformanceMachineDefinition instance) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceMachineDefinitionId", EqualityOperatorEnum.Equal, instance.PerformanceMachineDefinitionId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);

            if (!dataReader.Read()) {
                dataReader.Close();
                throw new FinderException("Reload found no rows for PerformanceMachineDefinition.");
            }
            GetDataObjectFromReader(instance, dataReader);
            dataReader.Close();
        }

        /// <summary>
        /// Read through the reader and return a data object list
        /// </summary>
        private List<PerformanceMachineDefinition> GetList(IDataReader reader) {
            List<PerformanceMachineDefinition> list = new List<PerformanceMachineDefinition>();
            while (reader.Read()) {
                list.Add(GetDataObjectFromReader(reader));
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private PerformanceMachineDefinition GetDataObject(IDataReader reader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(reader);
            }
            return GetDataObject(reader, columnOrdinals);
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private PerformanceMachineDefinition GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
            if (!reader.Read()) {
                reader.Close();
                throw new FinderException("Reader contained no rows.");
            }
            PerformanceMachineDefinition data = GetDataObjectFromReader(reader, ordinals);
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
        internal static PerformanceMachineDefinition GetDataObjectFromReader(PerformanceMachineDefinition data, IDataReader dataReader, ColumnOrdinals ordinals) {
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceMachineDefinition GetDataObjectFromReader(PerformanceMachineDefinition data, IDataReader dataReader) {
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
        internal static PerformanceMachineDefinition GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
            PerformanceMachineDefinition data = new PerformanceMachineDefinition(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceMachineDefinition GetDataObjectFromReader(IDataReader dataReader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(dataReader);
            }
            PerformanceMachineDefinition data = new PerformanceMachineDefinition(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <param name="prefix"></param>
        /// <param name="ordinals"></param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceMachineDefinition GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            PerformanceMachineDefinition data = new PerformanceMachineDefinition(false);
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
        internal static PerformanceMachineDefinition GetDataObjectFromReader(PerformanceMachineDefinition data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            if (dataReader.IsDBNull(ordinals.PerformanceMachineDefinitionId)) {
                data.PerformanceMachineDefinitionId = null;
            } else {
                data.PerformanceMachineDefinitionId = dataReader.GetInt32(ordinals.PerformanceMachineDefinitionId);
            }
            if (dataReader.IsDBNull(ordinals.MachineName)) {
                data.MachineName = null;
            } else {
                data.MachineName = dataReader.GetString(ordinals.MachineName);
            }
            if (dataReader.IsDBNull(ordinals.IntervalInSeconds)) {
                data.IntervalInSeconds = null;
            } else {
                data.IntervalInSeconds = dataReader.GetInt32(ordinals.IntervalInSeconds);
            }
            if (dataReader.IsDBNull(ordinals.NumberOfSamples)) {
                data.NumberOfSamples = null;
            } else {
                data.NumberOfSamples = dataReader.GetInt32(ordinals.NumberOfSamples);
            }
            return data;
        }

        /// <summary>
        /// Inserts a record into the PerformanceMachineDefinition table.
        /// </summary>
        /// <param name="data"></param>
        public Int32 Insert(PerformanceMachineDefinition data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceMachineDefinition_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(idParam);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@MachineName", DbType.AnsiString, ParameterDirection.Input, data.MachineName));
            cmd.Parameters.Add(CreateDataParameter("@IntervalInSeconds", DbType.Int32, ParameterDirection.Input, data.IntervalInSeconds));
            cmd.Parameters.Add(CreateDataParameter("@NumberOfSamples", DbType.Int32, ParameterDirection.Input, data.NumberOfSamples));

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
        /// Updates a record in the PerformanceMachineDefinition table.
        /// </summary>
        /// <param name="data"></param>
        public void Update(PerformanceMachineDefinition data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceMachineDefinition_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@PerformanceMachineDefinitionId", DbType.Int32, ParameterDirection.Input, data.PerformanceMachineDefinitionId));
            cmd.Parameters.Add(CreateDataParameter("@MachineName", DbType.AnsiString, ParameterDirection.Input, data.MachineName));
            cmd.Parameters.Add(CreateDataParameter("@IntervalInSeconds", DbType.Int32, ParameterDirection.Input, data.IntervalInSeconds));
            cmd.Parameters.Add(CreateDataParameter("@NumberOfSamples", DbType.Int32, ParameterDirection.Input, data.NumberOfSamples));

            // Execute the query
            cmd.ExecuteNonQuery();

            // do not close the connection if it is part of a transaction
            if (DbConnectionScope.Current == null) {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Deletes a record from the PerformanceMachineDefinition table by PerformanceMachineDefinitionId.
        /// </summary>
        /// <param name="PerformanceMachineDefinitionId">A key field.</param>
        public void Delete(Int32? PerformanceMachineDefinitionId) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceMachineDefinition_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            // Create and append the parameters
            cmd.Parameters.Add(CreateDataParameter("@PerformanceMachineDefinitionId", DbType.Int32, ParameterDirection.Input, PerformanceMachineDefinitionId));
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
        /// <param name="machineName">A field value to be matched.</param>
        /// <returns>The list of PerformanceMachineDefinition objects found.</returns>
        public PerformanceMachineDefinition FindByMachineName(String machineName) {
            OrderByClause sort = new OrderByClause("PerformanceMachineDefinitionId");
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("MachineName", EqualityOperatorEnum.Equal, machineName));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);
            try {
                return GetDataObject(dataReader);
            } finally {
                dataReader.Close();
            }
        }

        public sealed class ColumnOrdinals {
            public Int32 PerformanceMachineDefinitionId;
            public Int32 MachineName;
            public Int32 IntervalInSeconds;
            public Int32 NumberOfSamples;

            internal ColumnOrdinals(IDataReader reader) {
                Init(reader, "");
            }

            internal ColumnOrdinals(IDataReader reader, String prefix) {
                Init(reader, prefix);
            }

            private void Init(IDataReader reader, String prefix) {
                PerformanceMachineDefinitionId = reader.GetOrdinal("PerformanceMachineDefinitionId");
                MachineName = reader.GetOrdinal("MachineName");
                IntervalInSeconds = reader.GetOrdinal("IntervalInSeconds");
                NumberOfSamples = reader.GetOrdinal("NumberOfSamples");
            }
        }
    }
}
