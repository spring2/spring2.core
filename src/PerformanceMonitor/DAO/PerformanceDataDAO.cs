using Spring2.Core.DAO;
using Spring2.Core.PerformanceMonitor.BusinessLogic;
using Spring2.Core.PerformanceMonitor.DataObject;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DAO {
    public class PerformanceDataDAO : Spring2.Core.DAO.SqlEntityDAO {
        public static readonly PerformanceDataDAO DAO = new PerformanceDataDAO();

        private static readonly String CONNECTION_STRING_KEY = "ConnectionString";

        private static readonly Int32 COMMAND_TIMEOUT = 15;

        private static readonly String VIEW = "PerformanceData";

        private static ColumnOrdinals columnOrdinals = null;

        /// <summary>
        /// Initializes the static map of property names to sql expressions.
        /// </summary>
        static PerformanceDataDAO() {
            AddPropertyMapping("PerformanceDataId", @"PerformanceDataId");
            AddPropertyMapping("MachineName", @"MachineName");
            AddPropertyMapping("CategoryName", @"CategoryName");
            AddPropertyMapping("CounterName", @"CounterName");
            AddPropertyMapping("InstanceName", @"InstanceName");
            AddPropertyMapping("CalculationType", @"CalculationType");
            AddPropertyMapping("IntervalInSeconds", @"IntervalInSeconds");
            AddPropertyMapping("NumberOfSampes", @"NumberOfSamples");
            AddPropertyMapping("TimeOfSample", @"TimeOfSample");
            AddPropertyMapping("SampleValue", "SampleValue");
        }

        private PerformanceDataDAO() { }

        protected override String ConnectionStringKey {
            get {
                return CONNECTION_STRING_KEY;
            }
        }

        /// <summary>
        /// Returns a list of all PerformanceData rows.
        /// </summary>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceData> GetList() {
            return GetList(null, null);
        }

        /// <summary>
        /// Returns a filtered list of PerformanceData rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceData> GetList(SqlFilter filter) {
            return GetList(filter, null);
        }

        /// <summary>
        /// Returns an ordered list of PerformanceData rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceData> GetList(IOrderBy orderByClause) {
            return GetList(null, orderByClause);
        }

        /// <summary>
        /// Returns an ordered and filtered list of PerformanceData rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceData> GetList(SqlFilter filter, IOrderBy orderByClause) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

            List<PerformanceData> list = new List<PerformanceData>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Returns a list of all PerformanceData rows.
        /// </summary>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceData> GetList(Int32 maxRows) {
            return GetList(null, null, maxRows);
        }

        /// <summary>
        /// Returns a filtered list of PerformanceData rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceData> GetList(SqlFilter filter, Int32 maxRows) {
            return GetList(filter, null, maxRows);
        }

        /// <summary>
        /// Returns an ordered list of PerformanceData rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<PerformanceData> GetList(IOrderBy orderByClause, Int32 maxRows) {
            return GetList(null, orderByClause, maxRows);
        }

        /// <summary>
        /// Returns an ordered and filtered list of PerformanceData rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of PerformanceData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<PerformanceData> GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

            List<PerformanceData> list = new List<PerformanceData>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Finds a PerformanceData entity using it's primary key.
        /// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        /// <returns>A PerformanceData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
        public PerformanceData Load(Int32? performanceCounterDefinitionId) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceDataId", EqualityOperatorEnum.Equal, performanceCounterDefinitionId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);
            return GetDataObject(dataReader);
        }

        /// <summary>
        /// Repopulates an existing business entity instance
        /// </summary>
        public void Reload(PerformanceData instance) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("PerformanceDataId", EqualityOperatorEnum.Equal, instance.PerformanceDataId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);

            if (!dataReader.Read()) {
                dataReader.Close();
                throw new FinderException("Reload found no rows for PerformanceData.");
            }
            GetDataObjectFromReader(instance, dataReader);
            dataReader.Close();
        }

        /// <summary>
        /// Read through the reader and return a data object list
        /// </summary>
        private List<PerformanceData> GetList(IDataReader reader) {
            List<PerformanceData> list = new List<PerformanceData>();
            while (reader.Read()) {
                list.Add(GetDataObjectFromReader(reader));
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private PerformanceData GetDataObject(IDataReader reader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(reader);
            }
            return GetDataObject(reader, columnOrdinals);
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private PerformanceData GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
            if (!reader.Read()) {
                reader.Close();
                throw new FinderException("Reader contained no rows.");
            }
            PerformanceData data = GetDataObjectFromReader(reader, ordinals);
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
        internal static PerformanceData GetDataObjectFromReader(PerformanceData data, IDataReader dataReader, ColumnOrdinals ordinals) {
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceData GetDataObjectFromReader(PerformanceData data, IDataReader dataReader) {
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
        internal static PerformanceData GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
            PerformanceData data = new PerformanceData(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceData GetDataObjectFromReader(IDataReader dataReader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(dataReader);
            }
            PerformanceData data = new PerformanceData(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <param name="prefix"></param>
        /// <param name="ordinals"></param>
        /// <returns>Data object built from current row.</returns>
        internal static PerformanceData GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            PerformanceData data = new PerformanceData(false);
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
        internal static PerformanceData GetDataObjectFromReader(PerformanceData data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            if (dataReader.IsDBNull(ordinals.PerformanceDataId)) {
                data.PerformanceDataId = null;
            } else {
                data.PerformanceDataId = dataReader.GetInt32(ordinals.PerformanceDataId);
            }
            if (dataReader.IsDBNull(ordinals.MachineName)) {
                data.MachineName = null;
            } else {
                data.MachineName = dataReader.GetString(ordinals.MachineName);
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
            if (dataReader.IsDBNull(ordinals.TimeOfSample)) {
                data.TimeOfSample = null;
            } else {
                data.TimeOfSample = dataReader.GetDateTime(ordinals.TimeOfSample);
            }
            if (dataReader.IsDBNull(ordinals.SampleValue)) {
                data.SampleValue = null;
            } else {
                data.SampleValue = (float)dataReader.GetDouble(ordinals.SampleValue);
            }
            return data;
        }

        /// <summary>
        /// Inserts a record into the PerformanceData table.
        /// </summary>
        /// <param name="data"></param>
        public Int32 Insert(PerformanceData data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceData_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(idParam);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@MachineName", DbType.AnsiString, ParameterDirection.Input, data.MachineName));
            cmd.Parameters.Add(CreateDataParameter("@CategoryName", DbType.AnsiString, ParameterDirection.Input, data.CategoryName));
            cmd.Parameters.Add(CreateDataParameter("@CounterName", DbType.AnsiString, ParameterDirection.Input, data.CounterName));
            cmd.Parameters.Add(CreateDataParameter("@InstanceName", DbType.AnsiString, ParameterDirection.Input, data.InstanceName));
            cmd.Parameters.Add(CreateDataParameter("@CalculationType", DbType.AnsiString, ParameterDirection.Input, data.CalculationType.DBValue));
            cmd.Parameters.Add(CreateDataParameter("@IntervalInSeconds", DbType.Int32, ParameterDirection.Input, data.IntervalInSeconds));
            cmd.Parameters.Add(CreateDataParameter("@NumberOfSamples", DbType.Int32, ParameterDirection.Input, data.NumberOfSamples));
            cmd.Parameters.Add(CreateDataParameter("@TimeOfSample", DbType.DateTime, ParameterDirection.Input, data.TimeOfSample));
            cmd.Parameters.Add(CreateDataParameter("@SampleValue", DbType.Double, ParameterDirection.Input, data.SampleValue));

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
        /// Updates a record in the PerformanceData table.
        /// </summary>
        /// <param name="data"></param>
        public void Update(PerformanceData data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceData_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@PerformanceDataId", DbType.Int32, ParameterDirection.Input, data.PerformanceDataId));
            cmd.Parameters.Add(CreateDataParameter("@MachineName", DbType.AnsiString, ParameterDirection.Input, data.MachineName));
            cmd.Parameters.Add(CreateDataParameter("@CategoryName", DbType.AnsiString, ParameterDirection.Input, data.CategoryName));
            cmd.Parameters.Add(CreateDataParameter("@CounterName", DbType.AnsiString, ParameterDirection.Input, data.CounterName));
            cmd.Parameters.Add(CreateDataParameter("@InstanceName", DbType.AnsiString, ParameterDirection.Input, data.InstanceName));
            cmd.Parameters.Add(CreateDataParameter("@CalculationType", DbType.AnsiString, ParameterDirection.Input, data.CalculationType.DBValue));
            cmd.Parameters.Add(CreateDataParameter("@IntervalInSeconds", DbType.Int32, ParameterDirection.Input, data.IntervalInSeconds));
            cmd.Parameters.Add(CreateDataParameter("@NumberOfSamples", DbType.Int32, ParameterDirection.Input, data.NumberOfSamples));
            cmd.Parameters.Add(CreateDataParameter("@TimeOfSample", DbType.DateTime, ParameterDirection.Input, data.TimeOfSample));
            cmd.Parameters.Add(CreateDataParameter("@SampleValue", DbType.Double, ParameterDirection.Input, data.SampleValue));

            // Execute the query
            cmd.ExecuteNonQuery();

            // do not close the connection if it is part of a transaction
            if (DbConnectionScope.Current == null) {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Deletes a record from the PerformanceData table by PerformanceDataId.
        /// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        public void Delete(Int32? performanceCounterDefinitionId) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spPerformanceData_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            // Create and append the parameters
            cmd.Parameters.Add(CreateDataParameter("@PerformanceDataId", DbType.Int32, ParameterDirection.Input, performanceCounterDefinitionId));
            // Execute the query and return the result
            cmd.ExecuteNonQuery();

            // do not close the connection if it is part of a transaction
            if (DbConnectionScope.Current == null) {
                cmd.Connection.Close();
            }
        }

        public sealed class ColumnOrdinals {
            public Int32 PerformanceDataId;
            public Int32 MachineName;
            public Int32 CategoryName;
            public Int32 CounterName;
            public Int32 InstanceName;
            public Int32 CalculationType;
            public Int32 IntervalInSeconds;
            public Int32 NumberOfSamples;
            public Int32 TimeOfSample;
            public Int32 SampleValue;

            internal ColumnOrdinals(IDataReader reader) {
                Init(reader, "");
            }

            internal ColumnOrdinals(IDataReader reader, String prefix) {
                Init(reader, prefix);
            }

            private void Init(IDataReader reader, String prefix) {
                PerformanceDataId = reader.GetOrdinal("PerformanceDataId");
                MachineName = reader.GetOrdinal("MachineName");
                CategoryName = reader.GetOrdinal("CategoryName");
                CounterName = reader.GetOrdinal("CounterName");
                InstanceName = reader.GetOrdinal("InstanceName");
                CalculationType = reader.GetOrdinal("CalculationType");
                IntervalInSeconds = reader.GetOrdinal("IntervalInSeconds");
                NumberOfSamples = reader.GetOrdinal("NumberOfSamples");
                TimeOfSample = reader.GetOrdinal("TimeOfSample");
                SampleValue = reader.GetOrdinal("SampleValue");
            }
        }
    }
}
