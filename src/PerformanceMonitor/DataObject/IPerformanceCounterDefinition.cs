using Spring2.Core.BusinessEntity;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DataObject {
    public class PerformanceCounterDefinitionFields {
        private PerformanceCounterDefinitionFields() { }
        public static readonly String PERFORMANCECOUNTERDEFINITIONID = "PerformanceCounterDefinitionId";
        public static readonly String PERFORMANCEMACHINEDEFINITIONID = "PerformanceMachineDefinitionId";
        public static readonly String CATEGORYNAME = "CategoryName";
        public static readonly String COUNTERNAME = "CounterName";
        public static readonly String INSTANCENAME = "InstanceName";
        public static readonly String INSTANCEMATCHNAME = "InstanceMatchName";
        public static readonly String CALCULATIONTYPE = "CalculationType";
    }

    public interface IPerformanceCounterDefinition : IBusinessEntity {
        Int32? PerformanceCounterDefinitionId {
            get;
        }

        Int32? PerformanceMachineDefinitionId {
            get;
        }

        String CategoryName {
            get;
        }

        String CounterName {
            get;
        }

        String InstanceName {
            get;
        }

        String InstanceMatchName {
            get;
        }

        PerformanceCounterCalculationTypeEnum CalculationType {
            get;
        }
    }
}
