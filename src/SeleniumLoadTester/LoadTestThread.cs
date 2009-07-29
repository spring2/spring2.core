using Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Spring2.Core.SeleniumLoadTester {

    public class LoadTestThread {
        private string serverHost = null;
        private int serverPort = -1;
        private string browserString = null;
        private string browserUrl = null;
        private List<LoadTestMethod> methods = null;
        private static bool stop = false;
        private static readonly object stopLock = new object();

        public static bool Stop {
            get {
                lock (stopLock) {
                    return stop;
                }
            }
            set {
                lock (stopLock) {
                    stop = value;
                }
            }
        }

        public List<LoadTestMethod> Methods {
            get { return methods; }
        }

        public LoadTestThread(string serverHost, string serverPort, string browserString, string browserUrl, List<LoadTestMethod> methods) {
            this.serverHost = serverHost;
            this.serverPort = int.Parse(serverPort);
            this.browserString = browserString;
            this.browserUrl = browserUrl;
            this.methods = new List<LoadTestMethod>();
            foreach (LoadTestMethod method in methods) {
                LoadTestMethod m = new LoadTestMethod(method);
                this.methods.Add(m);
            }
        }

        public void Run() {
            ISelenium selenium = new DefaultSelenium(serverHost, serverPort, browserString, browserUrl);
            selenium.Start();
            foreach (LoadTestMethod m in methods) {
                m.CreateObject(selenium);
            }
            Thread.Sleep(30000);
            while (!Stop) {
                foreach (LoadTestMethod m in methods) {
                    m.Run();
                }
            }
            try {
                selenium.Stop();
            } catch (Exception) {
                // ignore exception closing browser.
            }
        }
    }
}
