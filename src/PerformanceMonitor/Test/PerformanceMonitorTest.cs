using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;
using Spring2.Core;
using Spring2.Core.PerformanceMonitor.BusinessLogic;
using Spring2.Core.PerformanceMonitor.DAO;
using Spring2.Core.PerformanceMonitor.Types;
using Spring2.Core.Test;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for AddressValidationManagerTest.
    /// </summary>
    [TestFixture]
    public class PerformanceMonitorTest {
        [TearDown]
        public void TearDown() {
	    String connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try {
                String sql = "Truncate Table PerformanceCounterDefinition;Truncate Table PerformanceMachineDefinition;Truncate Table PerformanceData";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            } finally {
                conn.Close();
            }
        }

        [Test]
        public void CreateTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create("abc", 5, 20);
            PerformanceCounterDefinition def = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "_Total");
            PerformanceData data = PerformanceData.Create(machine, def, 10);
            List<PerformanceCounterDefinition> defs = PerformanceCounterDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, defs.Count);
            Assert.AreEqual(PerformanceCounterCalculationTypeEnum.SNAPSHOT, defs[0].CalculationType);
            Assert.AreEqual(machine.PerformanceMachineDefinitionId, defs[0].PerformanceMachineDefinitionId);
            Assert.AreEqual("PhysicalDisk", defs[0].CategoryName);
            Assert.AreEqual("Current Disk Queue Length", defs[0].CounterName);
            Assert.AreEqual("_Total", defs[0].InstanceName);
            Assert.IsNull(defs[0].InstanceMatchName);

            List<PerformanceMachineDefinition> machines = PerformanceMachineDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, machines.Count);
            Assert.AreEqual("abc", machines[0].MachineName);
            Assert.AreEqual(5, machines[0].IntervalInSeconds);
            Assert.AreEqual(20, machines[0].NumberOfSamples);

            List<PerformanceData> samples = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(1, samples.Count);
            Assert.AreEqual(PerformanceCounterCalculationTypeEnum.SNAPSHOT, samples[0].CalculationType);
            Assert.AreEqual("PhysicalDisk", samples[0].CategoryName);
            Assert.AreEqual("Current Disk Queue Length", samples[0].CounterName);
            Assert.AreEqual("_Total", samples[0].InstanceName);
            Assert.AreEqual(5, samples[0].IntervalInSeconds);
            Assert.AreEqual("abc", samples[0].MachineName);
            Assert.AreEqual(20, samples[0].NumberOfSamples);
            Assert.AreEqual(10.0, samples[0].SampleValue);
        }

        [Test]
        public void CreateTestMatchName() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create("abc", 5, 20);
            PerformanceCounterDefinition def = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "All Disk", ".*Tot.*");
            PerformanceData data = PerformanceData.Create(machine, def, 10);
            List<PerformanceCounterDefinition> defs = PerformanceCounterDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, defs.Count);
            Assert.AreEqual(PerformanceCounterCalculationTypeEnum.SNAPSHOT, defs[0].CalculationType);
            Assert.AreEqual(machine.PerformanceMachineDefinitionId, defs[0].PerformanceMachineDefinitionId);
            Assert.AreEqual("PhysicalDisk", defs[0].CategoryName);
            Assert.AreEqual("Current Disk Queue Length", defs[0].CounterName);
            Assert.AreEqual("All Disk", defs[0].InstanceName);
            Assert.AreEqual(".*Tot.*", defs[0].InstanceMatchName);

            List<PerformanceMachineDefinition> machines = PerformanceMachineDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, machines.Count);
            Assert.AreEqual("abc", machines[0].MachineName);
            Assert.AreEqual(5, machines[0].IntervalInSeconds);
            Assert.AreEqual(20, machines[0].NumberOfSamples);

            List<PerformanceData> samples = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(1, samples.Count);
            Assert.AreEqual(PerformanceCounterCalculationTypeEnum.SNAPSHOT, samples[0].CalculationType);
            Assert.AreEqual("PhysicalDisk", samples[0].CategoryName);
            Assert.AreEqual("Current Disk Queue Length", samples[0].CounterName);
            Assert.AreEqual("All Disk", samples[0].InstanceName);
            Assert.AreEqual(5, samples[0].IntervalInSeconds);
            Assert.AreEqual("abc", samples[0].MachineName);
            Assert.AreEqual(20, samples[0].NumberOfSamples);
            Assert.AreEqual(10.0, samples[0].SampleValue);
        }

        [Test]
        public void FindMachineByNameTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create("abc", 5, 20);
            PerformanceCounterDefinition def = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "_Total");

            PerformanceMachineDefinition machine2 = PerformanceMachineDefinitionDAO.DAO.FindByMachineName(machine.MachineName);
            Assert.AreEqual(machine.PerformanceMachineDefinitionId, machine2.PerformanceMachineDefinitionId);

            List<PerformanceCounterDefinition> defs = PerformanceCounterDefinitionDAO.DAO.FindByPerformanceMachineDefinitionId(machine.PerformanceMachineDefinitionId);
            Assert.AreEqual(1, defs.Count);
            Assert.AreEqual(def.PerformanceCounterDefinitionId, defs[0].PerformanceCounterDefinitionId);
        }

        [Test]
        public void MonitorSnapshotOnlyTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create(PerformanceMachineDefinition.CurrentMachineName, 1, 4);
            PerformanceCounterDefinition def = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "_Total");

            machine.Monitor(3);

            List<PerformanceData> dataPoints = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(3, dataPoints.Count);
            foreach (PerformanceData data in dataPoints) {
                Assert.AreEqual(def.CalculationType, data.CalculationType);
                Assert.AreEqual(def.CategoryName, data.CategoryName);
                Assert.AreEqual(def.CounterName, data.CounterName);
                Assert.AreEqual(def.InstanceName, data.InstanceName);
                Assert.AreEqual(machine.IntervalInSeconds, data.IntervalInSeconds);
                Assert.AreEqual(machine.MachineName, data.MachineName);
                Assert.AreEqual(machine.NumberOfSamples, data.NumberOfSamples);
            }
        }

        [Test]
        public void MonitorAverageOnlyTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create(PerformanceMachineDefinition.CurrentMachineName, 1, 4);
            PerformanceCounterDefinition def = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.AVERAGE, "Processor", "% Processor Time", "_Total");

            machine.Monitor(3);

            List<PerformanceData> dataPoints = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(3, dataPoints.Count);
            foreach (PerformanceData data in dataPoints) {
                Assert.AreEqual(def.CalculationType, data.CalculationType);
                Assert.AreEqual(def.CategoryName, data.CategoryName);
                Assert.AreEqual(def.CounterName, data.CounterName);
                Assert.AreEqual(def.InstanceName, data.InstanceName);
                Assert.AreEqual(machine.IntervalInSeconds, data.IntervalInSeconds);
                Assert.AreEqual(machine.MachineName, data.MachineName);
                Assert.AreEqual(machine.NumberOfSamples, data.NumberOfSamples);
            }
        }

        [Test]
        public void MonitorBothTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create(PerformanceMachineDefinition.CurrentMachineName, 1, 4);
            PerformanceCounterDefinition defAverage = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.AVERAGE, "Processor", "% Processor Time", "_Total");
            PerformanceCounterDefinition defSnapshot = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "_Total");

            machine.Monitor(3);

            List<PerformanceData> dataPoints = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(6, dataPoints.Count);
            int numAverage = 0;
            int numSnapshot = 0;
            foreach (PerformanceData data in dataPoints) {
                if (data.CalculationType == defAverage.CalculationType) {
                    numAverage++;
                    Assert.AreEqual(defAverage.CalculationType, data.CalculationType);
                    Assert.AreEqual(defAverage.CategoryName, data.CategoryName);
                    Assert.AreEqual(defAverage.CounterName, data.CounterName);
                    Assert.AreEqual(defAverage.InstanceName, data.InstanceName);
                } else if (data.CalculationType == defSnapshot.CalculationType) {
                    numSnapshot++;
                    Assert.AreEqual(defSnapshot.CalculationType, data.CalculationType);
                    Assert.AreEqual(defSnapshot.CategoryName, data.CategoryName);
                    Assert.AreEqual(defSnapshot.CounterName, data.CounterName);
                    Assert.AreEqual(defSnapshot.InstanceName, data.InstanceName);
                } else {
                    Assert.Fail("Unexpected calculation type: " + data.CalculationType.ToString());
                }
                Assert.AreEqual(machine.IntervalInSeconds, data.IntervalInSeconds);
                Assert.AreEqual(machine.MachineName, data.MachineName);
                Assert.AreEqual(machine.NumberOfSamples, data.NumberOfSamples);
            }
            Assert.AreEqual(3, numAverage);
            Assert.AreEqual(3, numSnapshot);
        }

        [Test]
        public void MonitorBothWithMatchNameTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create(PerformanceMachineDefinition.CurrentMachineName, 1, 4);
            PerformanceCounterDefinition defAverage = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.AVERAGE, "Processor", "% Processor Time", "Total Processor", ".*Tot.*");
            PerformanceCounterDefinition defSnapshot = PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, PerformanceCounterCalculationTypeEnum.SNAPSHOT, "PhysicalDisk", "Current Disk Queue Length", "Total Disk", ".*Tot.*");

            machine.Monitor(3);

            List<PerformanceData> dataPoints = PerformanceDataDAO.DAO.GetList();
            Assert.AreEqual(6, dataPoints.Count);
            int numAverage = 0;
            int numSnapshot = 0;
            foreach (PerformanceData data in dataPoints) {
                if (data.CalculationType == defAverage.CalculationType) {
                    numAverage++;
                    Assert.AreEqual(defAverage.CalculationType, data.CalculationType);
                    Assert.AreEqual(defAverage.CategoryName, data.CategoryName);
                    Assert.AreEqual(defAverage.CounterName, data.CounterName);
                    Assert.AreEqual(defAverage.InstanceName, data.InstanceName);
                } else if (data.CalculationType == defSnapshot.CalculationType) {
                    numSnapshot++;
                    Assert.AreEqual(defSnapshot.CalculationType, data.CalculationType);
                    Assert.AreEqual(defSnapshot.CategoryName, data.CategoryName);
                    Assert.AreEqual(defSnapshot.CounterName, data.CounterName);
                    Assert.AreEqual(defSnapshot.InstanceName, data.InstanceName);
                } else {
                    Assert.Fail("Unexpected calculation type: " + data.CalculationType.ToString());
                }
                Assert.AreEqual(machine.IntervalInSeconds, data.IntervalInSeconds);
                Assert.AreEqual(machine.MachineName, data.MachineName);
                Assert.AreEqual(machine.NumberOfSamples, data.NumberOfSamples);
            }
            Assert.AreEqual(3, numAverage);
            Assert.AreEqual(3, numSnapshot);
        }
    }
}
