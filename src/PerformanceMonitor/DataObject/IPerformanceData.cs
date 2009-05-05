using Spring2.Core.BusinessEntity;
using Spring2.Core.PerformanceMonitor.Types;

using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.DataObject {
    public class PerformanceDataFields {
        private PerformanceDataFields() { }
        public static readonly String PERFORMANCEDATAID = "PerformanceDataId";
        public static readonly String MACHINENAME = "MachineName";
        public static readonly String CATEGORYNAME = "CategoryName";
        public static readonly String COUNTERNAME = "CounterName";
        public static readonly String INSTANCENAME = "InstanceName";
        public static readonly String CALCULATIONTYPE = "CalculationType";
        public static readonly String INTERVALINSECONDS = "IntervalInSeconds";
        public static readonly String NUMBEROFSAMPLES = "NumberOfSamples";
        public static readonly String TIMEOFSAMPLE = "TimeOfSample";
        public static readonly String SAMPLEVALUE = "SampleValue";
    }

    public interface IPerformanceData : IBusinessEntity {
        Int32? PerformanceDataId {
            get;
        }

        String MachineName {
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

        PerformanceCounterCalculationTypeEnum CalculationType {
            get;
        }

        Int32? IntervalInSeconds {
            get;
        }

        Int32? NumberOfSamples {
            get;
        }

        DateTime? TimeOfSample {
            get;
        }

        float? SampleValue {
            get;
        }
    }
}
