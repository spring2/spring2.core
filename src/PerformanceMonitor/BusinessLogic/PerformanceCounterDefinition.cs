using Spring2.Core.Message;
using Spring2.Core.PerformanceMonitor.DAO;
using Spring2.Core.PerformanceMonitor.DataObject;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Spring2.Core.PerformanceMonitor.BusinessLogic {
    /// <summary>
    /// Class for handling monitoring performance of a specific performance counter
    /// </summary>
    public class PerformanceCounterDefinition : Spring2.Core.BusinessEntity.BusinessEntity, IPerformanceCounterDefinition {
        Int32? performanceCounterDefinitionId = null;
        Int32? performanceMachineDefinitionId = null;
        String categoryName = null;
        String counterName = null;
        String instanceName = null;
        String instanceMatchName = null;
        String actualInstanceName = null;
        PerformanceCounterCalculationTypeEnum calculationType = null;

        private PerformanceCounterDefinition() {
            this.isNew = false;
        }

        internal PerformanceCounterDefinition(Boolean isNew) {
            this.isNew = IsNew;
        }

        /// <summary>
        /// Generated Id
        /// </summary>
        public Int32? PerformanceCounterDefinitionId {
            get { return performanceCounterDefinitionId; }
            set { performanceCounterDefinitionId = value; }
        }

        /// <summary>
        /// Id of machine to be monitored.
        /// </summary>
        public Int32? PerformanceMachineDefinitionId {
            get { return performanceMachineDefinitionId; }
            set { performanceMachineDefinitionId = value; }
        }

        /// <summary>
        /// Category of counter to be monitored.
        /// </summary>
        public String CategoryName {
            get { return categoryName; }
            set { categoryName = value; }
        }

        /// <summary>
        /// Name of counter to be monitored.
        /// </summary>
        public String CounterName {
            get { return counterName; }
            set { counterName = value; }
        }

        /// <summary>
        /// Instance value (if any) of the counter to be monitored.
        /// </summary>
        public String InstanceName {
            get { return instanceName; }
            set { instanceName = value; }
        }

        /// <summary>
        /// Regular expression to be used for matching the instance name in cases where full InstanceName is not deterministic (ADO.Net connection counters).
        /// If this is specified (i.e. not null) then InstanceName will be put in PerformanceData but not used to match the actual performance counter instance name.
        /// </summary>
        public String InstanceMatchName {
            get { return instanceMatchName; }
            set { instanceMatchName = value; }
        }

        /// <summary>
        ///  The actual performance counter instance name.  This is different from from InstanceName if InstanceMatchName is specified.
        /// </summary>
        public String ActualInstanceName {
            get {
                if (actualInstanceName == null) {
                    // InstanceExists populates actualInstanceName as a side effectg
                    if (!InstanceExists) {
                        return null;
                    }
                }
                return actualInstanceName;
            }
        }

        /// <summary>
        /// Indicates if the instance exists on the current machine.
        /// </summary>
        public Boolean InstanceExists {
            get {
                if (InstanceMatchName == null || InstanceMatchName.Length == 0) {
                    actualInstanceName = instanceName;
                    return PerformanceCounterCategory.InstanceExists(instanceName, CategoryName);
                } else {
                    PerformanceCounterCategory cat = new PerformanceCounterCategory(CategoryName);
                    foreach (String name in cat.GetInstanceNames()) {
                        if (Regex.IsMatch(name, InstanceMatchName)) {
                            actualInstanceName = name;
                            return true;
                        }
                    }
                    return false;
                }   
            }
        }

        /// <summary>
        /// Indicates if we are monitoring a snapshot of the counter or an average over the interval.
        /// </summary>
        public PerformanceCounterCalculationTypeEnum CalculationType {
            get { return calculationType; }
            set { calculationType = value; }
        }

        /// <summary>
        /// Returns an instance based on the generated id.
        /// </summary>
        /// <param name="performanceCounterDefinitionId"></param>
        /// <returns></returns>
        public static PerformanceCounterDefinition GetInstance(Int32 performanceCounterDefinitionId) {
            return PerformanceCounterDefinitionDAO.DAO.Load(performanceCounterDefinitionId);
        }

        /// <summary>
        /// Creates a new definition for getting snapshots of a counter.
        /// </summary>
        /// <param name="performanceMachineDefinitionId"></param>
        /// <param name="calculationType"></param>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceName"></param>
        /// <returns></returns>
        public static PerformanceCounterDefinition Create(Int32? performanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum calculationType, String categoryName, String counterName, String instanceName) {
            PerformanceCounterDefinition def = new PerformanceCounterDefinition(true);
            def.PerformanceMachineDefinitionId = performanceMachineDefinitionId;
            def.CategoryName = categoryName;
            def.CounterName = counterName;
            def.CalculationType = calculationType;
            def.InstanceName = instanceName;

            def.Store();
            return def;
        }

        /// <summary>
        /// Creates a new definition for getting snapshots of a counter.
        /// </summary>
        /// <param name="performanceMachineDefinitionId"></param>
        /// <param name="calculationType"></param>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceName"></param>
        /// <param name="instanceMatchName"></param>
        /// <returns></returns>
        public static PerformanceCounterDefinition Create(Int32? performanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum calculationType, String categoryName, String counterName, String instanceName, String instanceMatchName) {
            PerformanceCounterDefinition def = new PerformanceCounterDefinition(true);
            def.PerformanceMachineDefinitionId = performanceMachineDefinitionId;
            def.CategoryName = categoryName;
            def.CounterName = counterName;
            def.CalculationType = calculationType;
            def.InstanceName = instanceName;
            def.instanceMatchName = instanceMatchName;

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
                this.PerformanceCounterDefinitionId = PerformanceCounterDefinitionDAO.DAO.Insert(this);
                isNew = false;
            } else {
                PerformanceCounterDefinitionDAO.DAO.Update(this);
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
            PerformanceCounterDefinitionDAO.DAO.Reload(this);
        }
    }
}
