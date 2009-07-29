using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spring2.Core.SeleniumLoadTester {
    /// <summary>
    /// Class for logging method execution info.
    /// </summary>
    public class LoadTestLog : Spring2.Core.BusinessEntity.BusinessEntity {
        Int32? loadTestLogId = null;
        String dllPath = null;
        String testClass = null;
        String testMethod = null;
        DateTime? startTime = null;
        DateTime? endTime = null;
        Int32? elapsedInMicroseconds = null;
        bool? success = null;
        String exception = null;

        private LoadTestLog() {
            this.isNew = false;
        }

        internal LoadTestLog(Boolean isNew) {
            this.isNew = IsNew;
        }

        /// <summary>
        /// Generated Id
        /// </summary>
        public Int32? LoadTestLogId {
            get { return loadTestLogId; }
            set { loadTestLogId = value; }
        }

        /// <summary>
        /// Path to dll containing method run.
        /// </summary>
        public String DllPath {
            get { return dllPath; }
            set { dllPath = value; }
        }

        /// <summary>
        /// Class containing method run.
        /// </summary>
        public String TestClass {
            get { return testClass; }
            set { testClass = value; }
        }

        /// <summary>
        /// Name of method run.
        /// </summary>
        public String TestMethod {
            get { return testMethod; }
            set { testMethod = value; }
        }

        /// <summary>
        /// Time method started.
        /// </summary>
        public DateTime? StartTime {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// Time method ended.
        /// </summary>
        public DateTime? EndTime {
            get { return endTime; }
            set { endTime = value; }
        }

        /// <summary>
        /// Interval in seconds defined for this sample.
        /// </summary>
        public Int32? ElapsedInMicroseconds {
            get { return elapsedInMicroseconds; }
            set { elapsedInMicroseconds = value; }
        }

        /// <summary>
        ///  Number of samples for average defined.
        /// </summary>
        public bool? Success {
            get { return success; }
            set { success = value; }
        }

        /// <summary>
        /// Value for the sample.
        /// </summary>
        public String Exception {
            get { return this.exception; }
            set { this.exception = value; }
        }

        /// <summary>
        /// Returns an instance based on the generated id.
        /// </summary>
        /// <param name="LoadTestLogId"></param>
        /// <returns></returns>
        public static LoadTestLog GetInstance(Int32 LoadTestLogId) {
            return LoadTestLogDAO.DAO.Load(LoadTestLogId);
        }

        /// <summary>
        /// Creates a new definition for getting snapshots of a counter.
        /// </summary>
        /// <param name="method"></param>
	/// <param name="startTime"></param>
	/// <param name="endTime"></param>
	/// <param name="elapsedInMicroseconds"></param>
	/// <param name="success"></param>
	/// <param name="exception"></param>
        /// <returns></returns>
        public static LoadTestLog Create(LoadTestMethod method, DateTime startTime, DateTime endtime, int elapsedInMicroseconds, bool success, string exception) {
            LoadTestLog data = new LoadTestLog(true);
            data.DllPath = method.DllPath;
            data.TestClass = method.TestClass;
            data.TestMethod = method.TestMethod;
            data.StartTime = startTime;
            data.EndTime = endtime;
            data.ElapsedInMicroseconds = elapsedInMicroseconds;
            data.Success = success;
            data.Exception = exception;
            data.Store();
            return data;
        }

        private void Store() {
            if (isNew) {
                this.LoadTestLogId = LoadTestLogDAO.DAO.Insert(this);
                isNew = false;
            } else {
                LoadTestLogDAO.DAO.Update(this);
            }
        }

        /// <summary>
        /// Reloads the object from the database.
        /// </summary>
        public void Reload() {
            LoadTestLogDAO.DAO.Reload(this);
        }
    }
}
