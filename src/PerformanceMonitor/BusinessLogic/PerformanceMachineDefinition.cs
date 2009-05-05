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
        Int32? performanceMachineDefinitionId = null;
        String machineName = null;
        Int32? intervalInSeconds = null;
        Int32? numberOfSamples = null;

        private PerformanceMachineDefinition() {
            this.isNew = false;
        }

        internal PerformanceMachineDefinition(Boolean isNew) {
            this.isNew = IsNew;
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
        /*
                public void Monitor(Int32 interval) {
                    PerformanceData performanceData = new PerformanceData();
                    //TODO:  Ability to monitor any performance object, counter, and instance
                    PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
                    PerformanceCounter currentDiskQueue = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total", true);
                    performanceData.Interval = StringType.Parse(interval.ToString());
                    performanceData.MachineName = StringType.Parse(cpu.MachineName.ToString());
                    performanceData.Instance = StringType.Parse(cpu.InstanceName.ToString());
                    cpu.NextValue();  // mark the start of the interval
                    int adjustedInterval = interval * 4;
                    int counter = 0;
                    float periodValue = 0;
                    float maxValue = 0;
                    float maxDiskQueue = 0;
                    float preAvg = 0;
                    float periodDiskQueue = 0;

                    while (!stopNow) {
                        try {
                            preAvg = 0;
                            counter = 0;

                            while (counter < adjustedInterval) {
                                preAvg += currentDiskQueue.NextValue();
                                System.Threading.Thread.Sleep(250); //sleep for 1/4 sec.
                                counter++;
                            }
                            GC.Collect();
                            periodValue = cpu.NextValue();
                            periodDiskQueue = preAvg / (adjustedInterval);

                            // log to performance table
                            performanceData.EventTime = new DateType(DateTime.Now);
                            performanceData.AverageDbCPU = new DecimalType(periodValue);
                            performanceData.AverageDataDriveQueueLength = new DecimalType(Convert.ToDecimal(periodDiskQueue));
                            PerformanceDAO.Insert(performanceData);

                            if (periodValue > maxValue) {
                                maxValue = periodValue;
                            }
                            if (periodDiskQueue > maxDiskQueue) {
                                maxDiskQueue = periodDiskQueue;
                            }
                        } catch (Exception ex) {
                            LogMessage("Exception in notification class:" + ex.ToString());
                        }
                    }
                }*/
    }
}
