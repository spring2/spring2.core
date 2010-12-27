using PostSharp.Aspects;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Spring2.Core.PostSharp {
    [Serializable]
    public abstract class PerformanceCounterAttribute : OnMethodBoundaryAspect {

        protected string categoryName = null;
        protected string counterName = null;
        protected string instanceName = null;
        protected string instanceParameterName = null;
        protected string instanceParameterPropertyName = null;
        protected bool isDisabled = false;
        [NonSerialized]
        protected PropertyInfo instanceProperty = null;
        protected int instancePropertyIndex = -1;
        protected static List<PerformanceCounter> counters = new List<PerformanceCounter>();
        private enum InstanceType { Literal, Parameter, Return, ConfigurationSetting };
        [NonSerialized]
        private InstanceType instanceType = InstanceType.Literal;

        private PerformanceCounter FindCounter(string counterName, string instanceName) {
            foreach (PerformanceCounter c in counters) {
                if (c.CategoryName.Equals(this.categoryName) && c.CounterName.Equals(counterName) && c.InstanceName.Equals(instanceName)) {
                    return c;
                }
            }

            return null;
        }

        protected PerformanceCounter GetCounter(string instanceName) {
            return GetCounter(counterName, instanceName);
        }

        protected PerformanceCounter GetCounter(string counterName, string instanceName) {
            PerformanceCounter c = FindCounter(counterName, instanceName);
            if (c == null) {
                if (PerformanceCounterCategory.Exists(categoryName) && PerformanceCounterCategory.CounterExists(counterName, categoryName)) {
                    lock (counters) {
                        c = FindCounter(counterName, instanceName);
                        if (c == null) {
                            if (instanceName != null) {
                                c = new PerformanceCounter(categoryName, counterName, instanceName, false);
                                counters.Add(c);
                            }
                        }
                    }
                }
            }

            return c;
        }

        public override void RuntimeInitialize(MethodBase method) {
            instanceType = InstanceType.Literal;
            if (this.instanceName == null) {
                if (this.instanceParameterName == null) {
                    this.isDisabled = true;
                    return;
                }
                if (this.instanceParameterName.ToUpper().Equals("CONFIGURATIONSETTING")) {
                    instanceType = InstanceType.ConfigurationSetting;
                    if (this.instanceParameterPropertyName == null || this.instanceParameterPropertyName.Length == 0) {
                        this.isDisabled = true;
                        return;
                    }
                } else if (this.instanceParameterName.ToUpper().Equals("RETURN")) {
                    instanceType = InstanceType.Return;
                    if (this.instanceParameterPropertyName != null || this.instanceParameterPropertyName.Length > 0) {
                        if (!(method is MethodInfo)) {
                            this.isDisabled = true;
                            return;
                        }
                        MethodInfo meth = (MethodInfo)method;
                        Type t = meth.ReturnType;
                        this.instanceProperty = GetProperty(t, this.instanceParameterPropertyName);
                        if (this.instanceProperty == null) {
                            this.isDisabled = true;
                            return;
                        }
                    }
                } else {
                    instanceType = InstanceType.Parameter;
                    ParameterInfo[] parms = method.GetParameters();
                    for (int i = 0; i < parms.Length; i++) {
                        if (parms[i].Name.Equals(this.instanceParameterName)) {
                            this.instancePropertyIndex = i;
                            if (this.instanceParameterPropertyName != null && this.instanceParameterPropertyName.Length > 0) {
                                this.instanceProperty = GetProperty(parms[i].ParameterType, this.instanceParameterPropertyName);
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
                        return;
                    }
                }
            }
        }

        private PropertyInfo GetProperty(Type t, string property) {
            try {
                PropertyInfo p = t.GetProperty(property);
                if (p == null) {
                    Type[] interfaces = t.GetInterfaces();
                    foreach (Type intrfc in interfaces) {
                        p = intrfc.GetProperty(property);
                        if (p != null) {
                            break;
                        }
                    }
                }
                return p;
            } catch (AmbiguousMatchException) {
                return null;
            }
        }

        protected string GetInstanceName(MethodExecutionArgs eventArgs) {
            string localInstanceName = this.instanceName;
            if (localInstanceName == null) {
                if (instanceType == InstanceType.ConfigurationSetting) {
		    localInstanceName = (string)ConfigurationManager.AppSettings[instanceParameterPropertyName];
                } else {
                    object instanceObject = null;
                    if (instanceType == InstanceType.Return) {
                        instanceObject = eventArgs.ReturnValue;
                    } else {
			object[] arguments = eventArgs.Arguments.ToArray();  // was .GetReadOnlyArgumentArray()
                        instanceObject = arguments[this.instancePropertyIndex];
                    }

                    if (instanceObject == null) {
                        return null;
                    }

                    if (this.instanceProperty == null) {
                        localInstanceName = instanceObject.ToString();
                    } else {
                        localInstanceName = this.instanceProperty.GetValue(instanceObject, null).ToString();
                    }
                }
            }

            return localInstanceName;
        }
    }
}
