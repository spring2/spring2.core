using Selenium;
using Spring2.Core.SeleniumLoadTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Spring2.Core.SeleniumLoadTester {
//    <loadtest serverhost="localhost" serverport="4444" browserstrring="*firefox" browserurl="http://localhost:4444" dllpath="xxxx.dll" testclass="Spring2.Dss.Loadtest" testmethod="AddCustomDesignToShoppingCart" numiterations="1" /> 

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
            assembly = Assembly.LoadFile(dllPath);
            type = assembly.GetType(testClass);
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
                for (int i = 0; i < numIterations; i++) {
                    try {
                        method.Invoke(obj, null);
                        numSuccess++;
                    } catch (Exception ex) {
                        Console.WriteLine("Method " + testClass + "." + testMethod + " failure: " + ex.ToString());
                        numFailure++;
                    }
                }
            } else {
                throw new InvalidOperationException("obj not initialized");
            }
        }
    }
}
