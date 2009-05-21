using PostSharp.Laos;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Spring2.Core.PostSharp {
    [Serializable]
    public sealed class PerformanceCounterAttribute : OnMethodBoundaryAspect {

        private string categoryName = null;
        private string counterName = null;
        private string instanceName = null;
        private string counterType = null;
        private string instanceParameterName = null;
        private string instanceParameterPropertyName = null;
        private bool isDisabled = false;
        [NonSerialized]
        private PropertyInfo instanceProperty = null;
        private int instancePropertyIndex = -1;
        private static List<PerformanceCounter> counters = new List<PerformanceCounter>();

        private PerformanceCounterAttribute() { }

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceName"></param>
        /// <param name="counterType">"INCREMENT" means increment the counter by one for each invocation.</param>
        public PerformanceCounterAttribute(string categoryName, string counterName, string instanceName, string counterType) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.instanceName = instanceName;
            this.counterType = counterType.ToUpper();
        }

        /// <summary>
        /// Attribute to indicate a method invocation is used to modify a performance counter
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instanceParameterName"></param>
        /// <param name="instanceParameterPropertyName"></param>
        /// <param name="counterType">"INCREMENT" means increment the counter by one for each invocation.</param>
        public PerformanceCounterAttribute(string categoryName, string counterName, string instanceParameterName, string instanceParameterPropertyName, string counterType) {
            this.categoryName = categoryName;
            this.counterName = counterName;
            this.instanceParameterName = instanceParameterName;
            this.instanceParameterPropertyName = instanceParameterPropertyName;
            this.counterType = counterType.ToUpper();
        }

        private PerformanceCounter FindCounter(string instanceName) {
            foreach (PerformanceCounter c in counters) {
                if (c.CategoryName.Equals(this.categoryName) && c.CounterName.Equals(this.counterName) && c.InstanceName.Equals(instanceName)) {
                    return c;
                }
            }

            return null;
        }

        private PerformanceCounter GetCounter(string instanceName) {
            PerformanceCounter c = FindCounter(instanceName);
            if (c == null) {
                if (PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName)) {
                    lock (counters) {
                        c = FindCounter(instanceName);
                        if (c == null) {
                            c = new PerformanceCounter(categoryName, counterName, instanceName, false);
                            counters.Add(c);
                        }
                    }
                }
            }

            return c;
        }

        public override void RuntimeInitialize(MethodBase method) {
            if (this.instanceName == null) {
                if (this.instanceParameterName == null) {
                    this.isDisabled = true;
                    return;
                }
                if (this.instanceParameterName.ToUpper().Equals("RETURN")) {
                    if (this.instanceParameterPropertyName == null || this.instanceParameterPropertyName.Length == 0) {
                        return;
                    }
                    if (!(method is MethodInfo)) {
                        this.isDisabled = true;
                        return;
                    }
                    MethodInfo meth = (MethodInfo)method;
                    Type t = meth.ReturnType;
                    this.instanceProperty = t.GetProperty(this.instanceParameterPropertyName);
                    if (this.instanceProperty == null) {
                        this.isDisabled = true;
                        return;
                    }
                } else {
                    ParameterInfo[] parms = method.GetParameters();
                    for (int i = 0; i < parms.Length; i++) {
                        if (parms[i].Name.Equals(this.instanceParameterName)) {
                            this.instancePropertyIndex = i;
                            if (this.instanceParameterPropertyName != null && this.instanceParameterPropertyName.Length > 0) {
                                this.instanceProperty = parms[i].ParameterType.GetProperty(this.instanceParameterPropertyName);
                                if (this.instanceProperty == null) {
                                    this.isDisabled = true;
                                    return;
                                }
                            }
                            break;
                        }
                    }
                    if (this.instancePropertyIndex == -1) {
                        this.isDisabled = true;
                    }
                }
            }
        }

        public override void OnSuccess(MethodExecutionEventArgs eventArgs) {
            if (this.isDisabled) {
                return;
            }
            string localInstanceName = this.instanceName;
            if (localInstanceName == null) {
                object instanceObject = null;
                if (this.instanceParameterName.ToUpper().Equals("RETURN")) {
                    instanceObject = eventArgs.ReturnValue;
                } else {
                    object[] arguments = eventArgs.GetReadOnlyArgumentArray();
                    instanceObject = arguments[this.instancePropertyIndex];
                }

                if (instanceObject == null) {
                    return;
                }

                if (this.instanceProperty == null) {
                    localInstanceName = instanceObject.ToString();
                } else {
                    localInstanceName = this.instanceProperty.GetValue(instanceObject, null).ToString();
                }
            }
            if (counterType.Equals("INCREMENT")) {
                PerformanceCounter c = GetCounter(localInstanceName);
                if (c != null) {
                    c.Increment();
                }
            }
        }
    }
}
