using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.BusinessLogic {
    public class PerformanceCounterContainer {
        public PerformanceCounterDefinition counterDefinition = null;
        public PerformanceCounter counter = null;
        public float PreAverage = 0F;

        public PerformanceCounterContainer(PerformanceCounterDefinition counterDefinition, PerformanceCounter counter) {
            this.counterDefinition = counterDefinition;
            this.counter = counter;
            PreAverage = 0F;
        }
    }
}
