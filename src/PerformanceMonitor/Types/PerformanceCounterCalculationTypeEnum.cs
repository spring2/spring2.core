using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.PerformanceMonitor.Types {
    public class PerformanceCounterCalculationTypeEnum {
	private static readonly List<PerformanceCounterCalculationTypeEnum> OPTIONS = new List<PerformanceCounterCalculationTypeEnum>();

	public static readonly PerformanceCounterCalculationTypeEnum SNAPSHOT = new PerformanceCounterCalculationTypeEnum("Snapshot", "Snapshot");
	public static readonly PerformanceCounterCalculationTypeEnum AVERAGE = new PerformanceCounterCalculationTypeEnum("Average", "Average");

        private string code = null;
        private string name = null;

	public static PerformanceCounterCalculationTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (PerformanceCounterCalculationTypeEnum t in OPTIONS) {
		    if (t.DBValue.Equals(value)) {
			return t;
		    }
		}
	    }

	    return null;
	}

	private PerformanceCounterCalculationTypeEnum() {}

        private PerformanceCounterCalculationTypeEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public static List<PerformanceCounterCalculationTypeEnum> Options {
	    get { return OPTIONS; }
	}

        /// <summary>
        /// Value for storing in the database
        /// </summary>
        public String DBValue {
            get { return code; }
        }
    }
}
