using Selenium;
using Spring2.Core.SeleniumLoadTest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spring2.Core.SeleniumLoadTester {
    public class LoadTestMethod {
        private string dllPath = "";
        private string testClass = "";
        private string testMethod = "";
        private int numIterations = 1;
        private Assembly assembly = null;
        private Type type = null;
        private MethodInfo method = null;
        private Object obj = null;
        private int numSuccess = 0;
        private int numFailure = 0;

        public string DllPath {
            get { return dllPath; }
        }

        public string TestClass {
            get { return testClass; }
        }

        public string TestMethod {
            get { return testMethod; }
        }

        public int NumSuccess {
            get { return numSuccess; }
        }

        public int NumFailure {
            get { return numFailure; }
        }

        public LoadTestMethod(string dllPath, string testClass, string testMethod, int numIterations) {
            this.dllPath = dllPath;
            this.testClass = testClass;
            this.testMethod = testMethod;
            this.numIterations = numIterations;
            InitMethodInfo();
        }

        public LoadTestMethod(LoadTestMethod testMethod) {
            this.dllPath = testMethod.dllPath;
            this.testClass = testMethod.testClass;
            this.testMethod = testMethod.testMethod;
            this.numIterations = testMethod.numIterations;
            InitMethodInfo();
        }

        private void InitMethodInfo() {
            FileInfo dllFile = new FileInfo(dllPath);
            if (!dllFile.Exists) {
                throw new InvalidOperationException("Dll file '" + dllPath + "' not found.");
            }
            assembly = Assembly.LoadFile(dllFile.FullName);
            type = assembly.GetType(testClass);
            if (type == null) {
                throw new InvalidOperationException("Test class '" + testClass + "' not found in assembly '" + dllPath + "'");
            }
            method = type.GetMethod(testMethod);
            if (method.GetParameters().Length > 0) {
                throw new InvalidOperationException("Method '" + testClass + "." + testMethod + "' has parameters.  Method Ignored.");
            }
            object obj = Activator.CreateInstance(type);
            if (!(obj is ISeleniumLoadTest)) {
                throw new InvalidOperationException("Class '" + testClass + "' does not implement ISeleniumLoadTest.  Method ignored.");
            }
        }

        public void CreateObject(ISelenium selenium) {
            obj = Activator.CreateInstance(type);
            ((ISeleniumLoadTest)obj).SetSelenium(selenium);
            numSuccess = 0;
            numFailure = 0;
        }

        public void Run() {
            if (obj != null) {
                Stopwatch stopWatch = new Stopwatch();
                for (int i = 0; i < numIterations; i++) {
                    bool success = false;
                    string exception = null;
                    DateTime startTime = DateTime.Now;
                    DateTime endTime = DateTime.Now;

                    try {
                        startTime = DateTime.Now;
                        stopWatch.Reset();
                        stopWatch.Start();
                        method.Invoke(obj, null);
                        stopWatch.Stop();
                        endTime = DateTime.Now;
                        numSuccess++;
                        success = true;
                    } catch (Exception ex) {
                        stopWatch.Stop();
                        Console.WriteLine("Method " + testClass + "." + testMethod + " failure: " + ex.ToString());
                        numFailure++;
                        success = false;
                        exception = ex.ToString();
                    }

                    if (ConfigurationSettings.AppSettings["ConnectionString"] != null) {
                        try {
                            LoadTestLog.Create(this, startTime, endTime, (int)(stopWatch.ElapsedMilliseconds), success, exception);
                        } catch (Exception ex) {
                            Console.WriteLine("Unable to log data: " + ex.ToString());
                        }
                    }
                }
            } else {
                throw new InvalidOperationException("obj not initialized");
            }
        }
    }
}
