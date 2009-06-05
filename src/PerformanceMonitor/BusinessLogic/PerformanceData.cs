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
    /// Class for a sample when monitoring performance
    /// </summary>
    public class PerformanceData : Spring2.Core.BusinessEntity.BusinessEntity, IPerformanceData {
        Int32? performanceDataId = null;
        String machineName = null;
        String categoryName = null;
        String counterName = null;
        String instanceName = null;
        PerformanceCounterCalculationTypeEnum calculationType = null;
        Int32? intervalInSeconds = null;
        Int32? numberOfSamples = null;
        DateTime? timeOfSample = null;
        float? sampleValue = null;

        private PerformanceData() {
            this.isNew = false;
        }

        internal PerformanceData(Boolean isNew) {
            this.isNew = IsNew;
        }

        /// <summary>
        /// Generated Id
        /// </summary>
        public Int32? PerformanceDataId {
            get { return performanceDataId; }
            set { performanceDataId = value; }
        }

        /// <summary>
        /// Machine sampled.
        /// </summary>
        public String MachineName {
            get { return machineName; }
            set { machineName = value; }
        }

        /// <summary>
        /// Category of counter sampled.
        /// </summary>
        public String CategoryName {
            get { return categoryName; }
            set { categoryName = value; }
        }

        /// <summary>
        /// Name of counter sampled.
        /// </summary>
        public String CounterName {
            get { return counterName; }
            set { counterName = value; }
        }

        /// <summary>
        /// Instance value (if any) of the counter sampled.
        /// </summary>
        public String InstanceName {
            get { return instanceName; }
            set { instanceName = value; }
        }

        /// <summary>
        /// Indicates if we are monitoring a snapshot of the counter or an average over the interval.
        /// </summary>
        public PerformanceCounterCalculationTypeEnum CalculationType {
            get { return calculationType; }
            set { calculationType = value; }
        }

        /// <summary>
        /// Interval in seconds defined for this sample.
        /// </summary>
        public Int32? IntervalInSeconds {
            get { return intervalInSeconds; }
            set { intervalInSeconds = value; }
        }

        /// <summary>
        ///  Number of samples for average defined.
        /// </summary>
        public Int32? NumberOfSamples {
            get { return numberOfSamples; }
            set { numberOfSamples = value; }
        }

        /// <summary>
        /// Time sample was created.
        /// </summary>
        public DateTime? TimeOfSample {
            get { return timeOfSample; }
            set { timeOfSample = value; }
        }

        /// <summary>
        /// Value for the sample.
        /// </summary>
        public float? SampleValue {
            get { return this.sampleValue; }
            set { this.sampleValue = value; }
        }

        /// <summary>
        /// Returns an instance based on the generated id.
        /// </summary>
        /// <param name="performanceDataId"></param>
        /// <returns></returns>
        public static PerformanceData GetInstance(Int32 performanceDataId) {
            return PerformanceDataDAO.DAO.Load(performanceDataId);
        }

        /// <summary>
        /// Creates a new definition for getting snapshots of a counter.
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="counter"></param>
        /// <param name="sampleValue"></param>
        /// <returns></returns>
        public static PerformanceData Create(PerformanceMachineDefinition machine, PerformanceCounterDefinition counter, float sampleValue) {
            PerformanceData def = new PerformanceData(true);
            def.CalculationType = counter.CalculationType;
            def.CategoryName = counter.CategoryName;
            def.CounterName = counter.CounterName;
            def.InstanceName = counter.InstanceName;
            def.IntervalInSeconds = machine.IntervalInSeconds;
            def.MachineName = machine.MachineName;
            def.NumberOfSamples = machine.NumberOfSamples;
            def.TimeOfSample = DateTime.Now;
            def.SampleValue = sampleValue;
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
                this.PerformanceDataId = PerformanceDataDAO.DAO.Insert(this);
                isNew = false;
            } else {
                PerformanceDataDAO.DAO.Update(this);
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
            PerformanceDataDAO.DAO.Reload(this);
        }
    }
}
