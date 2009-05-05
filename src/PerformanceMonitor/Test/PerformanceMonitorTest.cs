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
            String connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try {
                String sql = "Truncate Table PerformanceCounterDefinition;Truncate Table PerformanceMachineDefinition";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            } finally {
                conn.Close();
            }
        }

        [Test]
        public void CreateTest() {
            PerformanceMachineDefinition machine = PerformanceMachineDefinition.Create("abc", 5, 20);
            PerformanceCounterDefinition.Create(machine.PerformanceMachineDefinitionId, "PhysicalDisk", "Current Disk Queue Length", "_Total");
            List<PerformanceCounterDefinition> defs = PerformanceCounterDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, defs.Count);
            Assert.AreEqual(PerformanceCounterCalculationTypeEnum.SNAPSHOT, defs[0].CalculationType);
            Assert.AreEqual(machine.PerformanceMachineDefinitionId, defs[0].PerformanceMachineDefinitionId);
            Assert.AreEqual("PhysicalDisk", defs[0].CategoryName);
            Assert.AreEqual("Current Disk Queue Length", defs[0].CounterName);
            Assert.AreEqual("_Total", defs[0].InstanceName);

            List<PerformanceMachineDefinition> machines = PerformanceMachineDefinitionDAO.DAO.GetList();
            Assert.AreEqual(1, machines.Count);
            Assert.AreEqual("abc", machines[0].MachineName);
            Assert.AreEqual(5, machines[0].IntervalInSeconds);
            Assert.AreEqual(20, machines[0].NumberOfSamples);
        }
    }
}
