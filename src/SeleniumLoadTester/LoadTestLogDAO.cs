using Spring2.Core.DAO;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Spring2.Core.SeleniumLoadTester {
    public class LoadTestLogDAO : Spring2.Core.DAO.SqlEntityDAO {
        public static readonly LoadTestLogDAO DAO = new LoadTestLogDAO();

        private static readonly String CONNECTION_STRING_KEY = "ConnectionString";

        private static readonly Int32 COMMAND_TIMEOUT = 15;

        private static readonly String VIEW = "LoadTestLog";

        private static ColumnOrdinals columnOrdinals = null;

        /// <summary>
        /// Initializes the static map of property names to sql expressions.
        /// </summary>
        static LoadTestLogDAO() {
            AddPropertyMapping("LoadTestLogId", @"LoadTestLogId");
	    AddPropertyMapping("DllPath", @"DllPath");
	    AddPropertyMapping("TestClass", @"TestClass");
	    AddPropertyMapping("TestMethod", @"TestMethod");
	    AddPropertyMapping("StartTime", @"StartTime");
	    AddPropertyMapping("EndTime", @"EndTime");
	    AddPropertyMapping("ElapsedInMicroseconds", @"ElapsedInMicroseconds");
	    AddPropertyMapping("Success", @"Success");
            AddPropertyMapping("Exception", @"Exception");
        }

        private LoadTestLogDAO() { }

        protected override String ConnectionStringKey {
            get {
                return CONNECTION_STRING_KEY;
            }
        }

        /// <summary>
        /// Returns a list of all LoadTestLog rows.
        /// </summary>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<LoadTestLog> GetList() {
            return GetList(null, null);
        }

        /// <summary>
        /// Returns a filtered list of LoadTestLog rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<LoadTestLog> GetList(SqlFilter filter) {
            return GetList(filter, null);
        }

        /// <summary>
        /// Returns an ordered list of LoadTestLog rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<LoadTestLog> GetList(IOrderBy orderByClause) {
            return GetList(null, orderByClause);
        }

        /// <summary>
        /// Returns an ordered and filtered list of LoadTestLog rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<LoadTestLog> GetList(SqlFilter filter, IOrderBy orderByClause) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

            List<LoadTestLog> list = new List<LoadTestLog>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Returns a list of all LoadTestLog rows.
        /// </summary>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<LoadTestLog> GetList(Int32 maxRows) {
            return GetList(null, null, maxRows);
        }

        /// <summary>
        /// Returns a filtered list of LoadTestLog rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<LoadTestLog> GetList(SqlFilter filter, Int32 maxRows) {
            return GetList(filter, null, maxRows);
        }

        /// <summary>
        /// Returns an ordered list of LoadTestLog rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        public List<LoadTestLog> GetList(IOrderBy orderByClause, Int32 maxRows) {
            return GetList(null, orderByClause, maxRows);
        }

        /// <summary>
        /// Returns an ordered and filtered list of LoadTestLog rows.
        /// </summary>
        /// <param name="filter">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
        /// <returns>List of LoadTestLog objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        public List<LoadTestLog> GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

            List<LoadTestLog> list = new List<LoadTestLog>();
            while (dataReader.Read()) {
                list.Add(GetDataObjectFromReader(dataReader));
            }
            dataReader.Close();
            return list;
        }

        /// <summary>
        /// Finds a LoadTestLog entity using it's primary key.
        /// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        /// <returns>A LoadTestLog object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
        public LoadTestLog Load(Int32? loadTestLogId) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("LoadTestLogId", EqualityOperatorEnum.Equal, loadTestLogId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);
            return GetDataObject(dataReader);
        }

        /// <summary>
        /// Repopulates an existing business entity instance
        /// </summary>
        public void Reload(LoadTestLog instance) {
            SqlFilter filter = new SqlFilter();
            filter.And(new SqlEqualityPredicate("LoadTestLogId", EqualityOperatorEnum.Equal, instance.LoadTestLogId));
            IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);

            if (!dataReader.Read()) {
                dataReader.Close();
                throw new FinderException("Reload found no rows for LoadTestLog.");
            }
            GetDataObjectFromReader(instance, dataReader);
            dataReader.Close();
        }

        /// <summary>
        /// Read through the reader and return a data object list
        /// </summary>
        private List<LoadTestLog> GetList(IDataReader reader) {
            List<LoadTestLog> list = new List<LoadTestLog>();
            while (reader.Read()) {
                list.Add(GetDataObjectFromReader(reader));
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private LoadTestLog GetDataObject(IDataReader reader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(reader);
            }
            return GetDataObject(reader, columnOrdinals);
        }

        /// <summary>
        /// Read from reader and return a single data object
        /// </summary>
        private LoadTestLog GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
            if (!reader.Read()) {
                reader.Close();
                throw new FinderException("Reader contained no rows.");
            }
            LoadTestLog data = GetDataObjectFromReader(reader, ordinals);
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
        internal static LoadTestLog GetDataObjectFromReader(LoadTestLog data, IDataReader dataReader, ColumnOrdinals ordinals) {
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static LoadTestLog GetDataObjectFromReader(LoadTestLog data, IDataReader dataReader) {
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
        internal static LoadTestLog GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
            LoadTestLog data = new LoadTestLog(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        internal static LoadTestLog GetDataObjectFromReader(IDataReader dataReader) {
            if (columnOrdinals == null) {
                columnOrdinals = new ColumnOrdinals(dataReader);
            }
            LoadTestLog data = new LoadTestLog(false);
            return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
        }

        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <param name="prefix"></param>
        /// <param name="ordinals"></param>
        /// <returns>Data object built from current row.</returns>
        internal static LoadTestLog GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            LoadTestLog data = new LoadTestLog(false);
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
        internal static LoadTestLog GetDataObjectFromReader(LoadTestLog data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
            if (dataReader.IsDBNull(ordinals.LoadTestLogId)) {
                data.LoadTestLogId = null;
            } else {
                data.LoadTestLogId = dataReader.GetInt32(ordinals.LoadTestLogId);
            }
            if (dataReader.IsDBNull(ordinals.DllPath)) {
                data.DllPath = null;
            } else {
                data.DllPath = dataReader.GetString(ordinals.DllPath);
            }
            if (dataReader.IsDBNull(ordinals.TestClass)) {
                data.TestClass = null;
            } else {
                data.TestClass = dataReader.GetString(ordinals.TestClass);
            }
            if (dataReader.IsDBNull(ordinals.StartTime)) {
                data.StartTime = null;
            } else {
                data.StartTime = dataReader.GetDateTime(ordinals.StartTime);
            }
            if (dataReader.IsDBNull(ordinals.EndTime)) {
                data.EndTime = null;
            } else {
                data.EndTime = dataReader.GetDateTime(ordinals.EndTime);
            }
            if (dataReader.IsDBNull(ordinals.ElapsedInMicroseconds)) {
                data.ElapsedInMicroseconds = null;
            } else {
                data.ElapsedInMicroseconds = dataReader.GetInt32(ordinals.ElapsedInMicroseconds);
            }
            if (dataReader.IsDBNull(ordinals.Success)) {
                data.Success = false;
            } else {
                data.Success = dataReader.GetBoolean(ordinals.Success);
            }
            if (dataReader.IsDBNull(ordinals.Exception)) {
                data.Exception = null;
            } else {
                data.Exception = dataReader.GetString(ordinals.Exception);
            }
            return data;
        }

        /// <summary>
        /// Inserts a record into the LoadTestLog table.
        /// </summary>
        /// <param name="data"></param>
        public Int32 Insert(LoadTestLog data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLoadTestLog_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
            cmd.Parameters.Add(idParam);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@DllPath", DbType.AnsiString, ParameterDirection.Input, data.DllPath));
            cmd.Parameters.Add(CreateDataParameter("@TestClass", DbType.AnsiString, ParameterDirection.Input, data.TestClass));
            cmd.Parameters.Add(CreateDataParameter("@TestMethod", DbType.AnsiString, ParameterDirection.Input, data.TestMethod));
            cmd.Parameters.Add(CreateDataParameter("@StartTime", DbType.DateTime, ParameterDirection.Input, data.StartTime));
            cmd.Parameters.Add(CreateDataParameter("@EndTime", DbType.DateTime, ParameterDirection.Input, data.EndTime));
            cmd.Parameters.Add(CreateDataParameter("@ElapsedInMicroseconds", DbType.Int32, ParameterDirection.Input, data.ElapsedInMicroseconds));
            cmd.Parameters.Add(CreateDataParameter("@Success", DbType.Boolean, ParameterDirection.Input, data.Success));
            cmd.Parameters.Add(CreateDataParameter("@Exception", DbType.AnsiString, ParameterDirection.Input, data.Exception));

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
        /// Updates a record in the LoadTestLog table.
        /// </summary>
        /// <param name="data"></param>
        public void Update(LoadTestLog data) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLoadTestLog_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            //Create the parameters and append them to the command object
            cmd.Parameters.Add(CreateDataParameter("@LoadTestLogId", DbType.Int32, ParameterDirection.Input, data.LoadTestLogId));
            cmd.Parameters.Add(CreateDataParameter("@TestClass", DbType.AnsiString, ParameterDirection.Input, data.TestClass));
            cmd.Parameters.Add(CreateDataParameter("@TestMethod", DbType.AnsiString, ParameterDirection.Input, data.TestMethod));
            cmd.Parameters.Add(CreateDataParameter("@StartTime", DbType.DateTime, ParameterDirection.Input, data.StartTime));
            cmd.Parameters.Add(CreateDataParameter("@EndTime", DbType.DateTime, ParameterDirection.Input, data.EndTime));
            cmd.Parameters.Add(CreateDataParameter("@ElapsedInMicroseconds", DbType.Int32, ParameterDirection.Input, data.ElapsedInMicroseconds));
            cmd.Parameters.Add(CreateDataParameter("@Success", DbType.Boolean, ParameterDirection.Input, data.Success));
            cmd.Parameters.Add(CreateDataParameter("@Exception", DbType.AnsiString, ParameterDirection.Input, data.Exception));

            // Execute the query
            cmd.ExecuteNonQuery();

            // do not close the connection if it is part of a transaction
            if (DbConnectionScope.Current == null) {
                cmd.Connection.Close();
            }
        }

        /// <summary>
        /// Deletes a record from the LoadTestLog table by LoadTestLogId.
        /// </summary>
        /// <param name="performanceCounterDefinitionId">A key field.</param>
        public void Delete(Int32? performanceCounterDefinitionId) {
            // Create and execute the command
            IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spLoadTestLog_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, null);

            // Create and append the parameters
            cmd.Parameters.Add(CreateDataParameter("@LoadTestLogId", DbType.Int32, ParameterDirection.Input, performanceCounterDefinitionId));
            // Execute the query and return the result
            cmd.ExecuteNonQuery();

            // do not close the connection if it is part of a transaction
            if (DbConnectionScope.Current == null) {
                cmd.Connection.Close();
            }
        }

        public sealed class ColumnOrdinals {
            public Int32 LoadTestLogId;
	    public Int32 DllPath;
	    public Int32 TestClass;
	    public Int32 TestMethod;
	    public Int32 StartTime;
	    public Int32 EndTime;
	    public Int32 ElapsedInMicroseconds;
	    public Int32 Success;
            public Int32 Exception;

            internal ColumnOrdinals(IDataReader reader) {
                Init(reader, "");
            }

            internal ColumnOrdinals(IDataReader reader, String prefix) {
                Init(reader, prefix);
            }

            private void Init(IDataReader reader, String prefix) {
                LoadTestLogId = reader.GetOrdinal("LoadTestLogId");
                DllPath = reader.GetOrdinal("DllPath");
                TestClass = reader.GetOrdinal("TestClass");
                TestMethod = reader.GetOrdinal("TestMethod");
                StartTime = reader.GetOrdinal("StartTime");
                EndTime = reader.GetOrdinal("EndTime");
                ElapsedInMicroseconds = reader.GetOrdinal("ElapsedInMicroseconds");
                Success = reader.GetOrdinal("Success");
                Exception = reader.GetOrdinal("Exception");
            }
        }
    }
}
