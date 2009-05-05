using Spring2.Core.BusinessEntity;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DataObject {
    public class PerformanceMachineDefinitionFields {
        private PerformanceMachineDefinitionFields() { }
        public static readonly String PERFORMANCEMACHINEDEFINITIONID = "PerformanceMachineDefinitionId";
        public static readonly String MACHINENAME = "MachineName";
        public static readonly String INTERVALINSECONDS = "IntervalInSeconds";
        public static readonly String NUMBEROFSAMPLES = "NumberOfSamples";
    }

    public interface IPerformanceMachineDefinition : IBusinessEntity {
        Int32? PerformanceMachineDefinitionId {
            get;
        }

        String MachineName {
            get;
        }

        Int32? IntervalInSeconds {
            get;
        }

        Int32? NumberOfSamples {
            get;
        }
    }
}
