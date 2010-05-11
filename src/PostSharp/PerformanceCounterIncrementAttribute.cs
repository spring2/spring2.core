using PostSharp.Aspects;
using PostSharp.Aspects.Dependencies;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spring2.Core.PostSharp {
    [Serializable]
    [AspectTypeDependency(AspectDependencyAction.Commute, typeof(PerformanceCounterIncrementAttribute))]
    public sealed class PerformanceCounterIncrementAttribute : PerformanceCounterAttribute {
        private PerformanceCounterIncrementAttribute() { }

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter.  This constructor create an instance that will increment the performance counter by one each time the decorated method is called.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceName"></param>
        public PerformanceCounterIncrementAttribute(string categoryName, string counterName, string instanceName) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.instanceName = instanceName;
        }

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter.  This constructor create an instance that will increment the performance counter by one each time the decorated method is called.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceParameterName"></param>
        /// <param name="instanceParameterPropertyName"></param>
        public PerformanceCounterIncrementAttribute(string categoryName, string counterName, string instanceParameterName, string instanceParameterPropertyName) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.instanceParameterName = instanceParameterName;
            this.instanceParameterPropertyName = instanceParameterPropertyName;
        }

	public override void OnSuccess(MethodExecutionArgs eventArgs) {
            if (this.isDisabled) {
                return;
            }

            string localInstanceName = GetInstanceName(eventArgs);
            PerformanceCounter c = GetCounter(localInstanceName);
            if (c != null) {
                c.Increment();
            }
        }
    }
}
