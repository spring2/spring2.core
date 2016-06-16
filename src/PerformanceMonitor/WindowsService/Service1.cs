using Spring2.Core.PerformanceMonitor.BusinessLogic;
using Spring2.Core.PerformanceMonitor.DAO;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace PerformanceMonitor.WindowsService {
    public partial class Spring2CorePerformanceMonitor : ServiceBase {
        private PerformanceMachineDefinition machine = null;
        Thread workerThread = null;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Spring2CorePerformanceMonitor() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            string machineName = PerformanceMachineDefinition.CurrentMachineName;
            try {
                machine = PerformanceMachineDefinitionDAO.DAO.FindByMachineName(machineName);
                ThreadStart st = new ThreadStart(machine.Monitor);
                workerThread = new Thread(st);
                workerThread.Start();
            } catch (Exception ex) {
                log.Error("Error starting performance monitor on machine '" + machineName + "'.", ex);
                EventLog.WriteEntry("Error starting performance monitor on machine '" + machineName + "': " + ex.Message, EventLogEntryType.Error);
                throw;
            }
        }

        protected override void OnStop() {
            if (machine != null) {
                machine.StopNow = true;
                workerThread.Join(120000);
            }
        }
    }
}
