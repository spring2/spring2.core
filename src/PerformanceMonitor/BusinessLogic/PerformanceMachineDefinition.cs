using log4net;

using Spring2.Core.Message;
using Spring2.Core.PerformanceMonitor.DAO;
using Spring2.Core.PerformanceMonitor.DataObject;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.BusinessLogic {
    /// <summary>
    /// Class for handling monitoring performance for a machine
    /// </summary>
    public class PerformanceMachineDefinition : Spring2.Core.BusinessEntity.BusinessEntity, IPerformanceMachineDefinition {
        private Int32? performanceMachineDefinitionId = null;
        private String machineName = null;
        private Int32? intervalInSeconds = null;
        private Int32? numberOfSamples = null;
        private bool stopNow = false;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PerformanceMachineDefinition() {
            this.isNew = false;
        }

        internal PerformanceMachineDefinition(Boolean isNew) {
            this.isNew = IsNew;
        }

        /// <summary>
        /// Returns the machine name of the machine we are running on.
        /// </summary>
        public static String CurrentMachineName {
            get {
                return Environment.MachineName;
            }
        }

        /// <summary>
        /// Setting this to true will cause monitor to stop.
        /// </summary>
        public bool StopNow {
            get { return stopNow; }
            set { stopNow = value; }
        }

        /// <summary>
        /// Generated Id
        /// </summary>
        public Int32? PerformanceMachineDefinitionId {
            get { return performanceMachineDefinitionId; }
            set { performanceMachineDefinitionId = value; }
        }

        /// <summary>
        /// Id of machine to be monitored.
        /// </summary>
        public String MachineName {
            get { return machineName; }
            set { machineName = value; }
        }

        /// <summary>
        /// Number of seconds between measurements and persistence of the counter.
        /// </summary>
        public Int32? IntervalInSeconds {
            get { return intervalInSeconds; }
            set { intervalInSeconds = value; }
        }

        /// <summary>
        /// Only used if CalculationType is average.  Number of samples to take during the interval for the average.
        /// </summary>
        public Int32? NumberOfSamples {
            get { return numberOfSamples; }
            set { numberOfSamples = value; }
        }

        /// <summary>
        /// Returns an instance based on the generated id.
        /// </summary>
        /// <param name="PerformanceMachineDefinitionId"></param>
        /// <returns></returns>
        public static PerformanceMachineDefinition GetInstance(Int32 PerformanceMachineDefinitionId) {
            return PerformanceMachineDefinitionDAO.DAO.Load(PerformanceMachineDefinitionId);
        }

        /// <summary>
        /// Creates a new definition for getting snapshots of a counter.
        /// </summary>
        /// <param name="machineName"></param>
        /// <param name="intervalInSeconds"></param>
        /// <param name="numberOfSamples"></param>
        /// <returns></returns>
        public static PerformanceMachineDefinition Create(String machineName, Int32 intervalInSeconds, Int32 numberOfSamples) {
            PerformanceMachineDefinition def = new PerformanceMachineDefinition(true);
            def.MachineName = machineName;
            def.IntervalInSeconds = intervalInSeconds;
            def.NumberOfSamples = numberOfSamples;
            def.Store();
            return def;
        }

        private void Store() {
            MessageList errors = Validate();

            if (errors.Count > 0) {
                if (!isNew) {
                    this.Reload();
                }
                throw new MessageListException(errors);
            }

            if (isNew) {
                this.PerformanceMachineDefinitionId = PerformanceMachineDefinitionDAO.DAO.Insert(this);
                isNew = false;
            } else {
                PerformanceMachineDefinitionDAO.DAO.Update(this);
            }
        }

        /// <summary>
        /// Validates the object is correct before persistance.
        /// </summary>
        /// <returns></returns>
        public MessageList Validate() {
            MessageList errors = new MessageList();

            return errors;
        }

        /// <summary>
        /// Reloads the object from the database.
        /// </summary>
        public void Reload() {
            PerformanceMachineDefinitionDAO.DAO.Reload(this);
        }

        /// <summary>
        /// Monitors defined counters until killed
        /// </summary>
        public void Monitor() {
            Monitor(0);
        }

        /// <summary>
        /// Monitors defined couters for indicated number of interations
        /// </summary>
        /// <param name="numberOfIterations">Number of iterations to go. If < 1 then go forever.</param>
        public void Monitor(Int32 numberOfIterations) {
            // Uncomment following line to allow for attach for debugging.
            //System.Threading.Thread.Sleep(30000);
            List<PerformanceCounterDefinition> counterDefinitions = PerformanceCounterDefinitionDAO.DAO.FindByPerformanceMachineDefinitionId(PerformanceMachineDefinitionId);

            List<PerformanceCounterContainer> averageCounters = new List<PerformanceCounterContainer>();
            List<PerformanceCounterContainer> snapshotCounters = new List<PerformanceCounterContainer>();
            foreach (PerformanceCounterDefinition counterDefinition in counterDefinitions) {
                PerformanceCounter c = new PerformanceCounter(counterDefinition.CategoryName, counterDefinition.CounterName, counterDefinition.InstanceName, true);
                PerformanceCounterContainer container = new PerformanceCounterContainer(counterDefinition, c);
                if (counterDefinition.CalculationType == PerformanceCounterCalculationTypeEnum.AVERAGE) {
                    averageCounters.Add(container);
                } else if (counterDefinition.CalculationType == PerformanceCounterCalculationTypeEnum.SNAPSHOT) {
                    snapshotCounters.Add(container);
                } else {
                    LogMessage("Unexpected calculation type '" + counterDefinition.CalculationType.ToString() + "' found for machine '" + MachineName + "', Category '" + counterDefinition.CategoryName + "', counter '" + counterDefinition.CounterName + "', instance '" + counterDefinition.InstanceName);
                }
                // Use up value in case it has been a long time (mainly for delta)
                container.counter.NextValue();
            }
            if (snapshotCounters.Count == 0 && averageCounters.Count == 0) {
                LogMessage("No counter definitions found for machine '" + MachineName + "'.");
                return;
            }

            int counter = 0;
            int iterationCount = 0;
            int sleepAmount = (((int)IntervalInSeconds) * 1000) / (int)NumberOfSamples;
            while (!StopNow && (numberOfIterations < 1 || iterationCount < numberOfIterations)) {
                try {
                    iterationCount++;
                    if (averageCounters.Count > 0) {
                        counter = 0;

                        while (counter < NumberOfSamples) {
                            foreach (PerformanceCounterContainer c in averageCounters) {
                                c.PreAverage += c.counter.NextValue();
                            }
                            System.Threading.Thread.Sleep(sleepAmount);
                            counter++;
                        }

                        foreach (PerformanceCounterContainer c in averageCounters) {
                            PerformanceData.Create(this, c.counterDefinition, c.PreAverage / ((float)NumberOfSamples));
                            c.PreAverage = 0F;
                        }
                    } else {
                        System.Threading.Thread.Sleep((int)IntervalInSeconds * 1000);
                    }

                    foreach (PerformanceCounterContainer c in snapshotCounters) {
                        PerformanceData.Create(this, c.counterDefinition, c.counter.NextValue());
                    }
                    GC.Collect();
                } catch (Exception ex) {
                    LogMessage("Exception in notification class", ex);
                    foreach (PerformanceCounterContainer c in averageCounters) {
                        c.PreAverage = 0F;
                    }
                }
            }
        }

        private void LogMessage(string message) {
            log.Error(message);
            EventLog.WriteEntry("Spring2 Performance Monitor", message, EventLogEntryType.Error);
        }

        private void LogMessage(string message, Exception ex) {
            log.Error(message, ex);
            EventLog.WriteEntry("Spring2 Performance Monitor", message + ": " + ex.Message, EventLogEntryType.Error);
        }
    }
}
