using PostSharp.Laos;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spring2.Core.PostSharp {
    [Serializable]
    public class PerformanceCounterResponseTimeAttribute : PerformanceCounterAttribute {
        protected string baseCounterName = null;
        [NonSerialized]
        protected Stopwatch stopWatch = null;

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter.  This constructor create an instance that will maintain a counter for the average response time of the method decorated.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="baseCounterName"></param>
        /// <param name="instanceName"></param>
        public PerformanceCounterResponseTimeAttribute(string categoryName, string counterName, string baseCounterName, string instanceName) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.baseCounterName = baseCounterName;
            this.instanceName = instanceName;
        }

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter.  This constructor create an instance that will maintain a counter for the average response time of the method decorated.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="baseCounterName"></param>
        /// <param name="instanceParameterName"></param>
        /// <param name="instanceParameterPropertyName"></param>
        public PerformanceCounterResponseTimeAttribute(string categoryName, string counterName, string baseCounterName, string instanceParameterName, string instanceParameterPropertyName) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.baseCounterName = baseCounterName;
            this.instanceParameterName = instanceParameterName;
            this.instanceParameterPropertyName = instanceParameterPropertyName;
        }

        public override void RuntimeInitialize(MethodBase method) {
            stopWatch = new Stopwatch();
            base.RuntimeInitialize(method);
        }

        private PerformanceCounter GetBaseCounter(string instanceName) {
            return GetCounter(baseCounterName, instanceName);
        }

        public override void OnEntry(MethodExecutionEventArgs eventArgs) {
            stopWatch.Reset();
            stopWatch.Start();
        }

        public override void OnSuccess(MethodExecutionEventArgs eventArgs) {
            stopWatch.Stop();
            if (this.isDisabled) {
                return;
            }
            string localInstanceName = GetInstanceName(eventArgs);
            PerformanceCounter c = GetCounter(localInstanceName);
            PerformanceCounter b = GetBaseCounter(localInstanceName);
            if (c != null && b != null) {
                c.IncrementBy(stopWatch.ElapsedTicks);
                b.Increment();
            }
        }
    }
}
