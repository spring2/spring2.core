using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

using Spring2.Core.Xml;

namespace Spring2.Core.SeleniumLoadTester {
    public class SeleniumLoadTester {

        private List<LoadTestThread> threads = new List<LoadTestThread>();

        public SeleniumLoadTester(string filename) {
            XmlDocument doc = Parse(filename);
            PopulateThreads(doc);
        }

        public void RunThreads() {
            if (threads.Count == 0) {
                throw new InvalidOperationException("No threads successfully defined.");
            }
            foreach (LoadTestThread testThread in threads) {
                Thread thread = new Thread(new ThreadStart(testThread.Run));
                thread.Start();
            }

            Console.WriteLine("Press newline when done");
            Console.ReadLine();

            LoadTestThread.Stop = true;

            foreach (LoadTestThread testThread in threads) {
                int totalSuccess = 0;
                int totalFailure = 0;
                foreach (LoadTestMethod method in testThread.Methods) {
                    totalSuccess += method.NumSuccess;
                    totalFailure += method.NumFailure;
                }
                Console.WriteLine("Thread Total Success: " + totalSuccess.ToString() + ", Total Failure: " + totalFailure.ToString());
                foreach (LoadTestMethod method in testThread.Methods) {
                    Console.WriteLine("   Method " + method.TestClass + "." + method.TestMethod + " Success: " + method.NumSuccess.ToString() + ", Failure: " + method.NumFailure.ToString());
                }
            }
        }

        private XmlDocument Parse(string filename) {
            FileInfo xmlFile = new FileInfo(filename);
            if (!xmlFile.Exists) {
                throw new FileNotFoundException("XML Test configuration file '" + filename + "' not found");
            }

            XmlDocument doc = new XmlDocument();
            Stream fileStream = XIncludeReader.GetStream(filename);
            doc.Load(fileStream);
            fileStream.Close();

            if (doc == null) {
                throw new InvalidOperationException("Could not parse xml configuration file '" + filename + "'");
            }

            return doc;
        }

        private void PopulateThreads(XmlDocument doc) {
            XmlNode root = (XmlNode)(doc.DocumentElement);
            if (!root.Name.Equals("loadtests")) {
                throw new InvalidOperationException("loadtests element not found in the xml document.  Run aborted.");
            }
            AttributeVars rootVars = new AttributeVars();
            rootVars.SetVars(root);

            foreach (XmlNode threadNode in root.ChildNodes) {
                if (!threadNode.Name.Equals("testthread") || threadNode.NodeType == XmlNodeType.Comment) {
                    continue;
                }
                try {
                    AttributeVars threadVars = new AttributeVars(rootVars);
                    threadVars.SetVars(threadNode);
                    List<LoadTestMethod> methods = new List<LoadTestMethod>();
                    foreach (XmlNode methodNode in threadNode.ChildNodes) {
                        if (!methodNode.Name.Equals("loadtest") || methodNode.NodeType == XmlNodeType.Comment) {
                            continue;
                        }
                        try {
                            AttributeVars methodVars = new AttributeVars(threadVars);
                            methodVars.SetVars(methodNode);
                            if (!methodVars.Validate()) {
                                continue;
                            }
                            int numIterations = 1;
                            if (methodNode.Attributes["numiterations"] != null) {
                                numIterations = int.Parse(methodNode.Attributes["numiterations"].Value);
                            }
                            if (methodNode.Attributes["testclass"] == null) {
                                Console.WriteLine("No testclass specified for method.  Method ignored.");
                                continue;
                            }
                            if (methodNode.Attributes["testmethod"] == null) {
                                Console.WriteLine("No testmethod specified for method.  Method ignored.");
                                continue;
                            }
                            LoadTestMethod method = new LoadTestMethod(methodVars.dllPath, methodNode.Attributes["testclass"].Value, methodNode.Attributes["testmethod"].Value, numIterations);
                            methods.Add(method);
                        } catch (Exception ex) {
                            Console.WriteLine("Unexpected error parsing method.  Method ignored : '" + ex.ToString() + "'.");
                        }
                    }

                    if (methods.Count == 0) {
                        Console.WriteLine("No valid methods found for thread.  Thread ignored.");
                        continue;
                    }

                    int numThreads = 1;
                    if (threadNode.Attributes["numthreads"] != null) {
                        numThreads = int.Parse(threadNode.Attributes["numthreads"].Value);
                    }
                    for (int i = 0; i < numThreads; i++) {
                        LoadTestThread thread = new LoadTestThread(threadVars.serverHost, threadVars.serverPort, threadVars.browserString, threadVars.browserUrl, methods);
                        threads.Add(thread);
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Unexpected error parsing thread, thread ignored : '" + ex.ToString() + "'.");
                }
            }
        }
    }

    internal struct AttributeVars {
        public string serverHost;
        public string serverPort;
        public string browserString;
        public string browserUrl;
        public string dllPath;

        public AttributeVars (AttributeVars vars) {
            this.serverHost = vars.serverHost;
            this.serverPort = vars.serverPort;
            this.browserString = vars.browserString;
            this.browserUrl = vars.browserUrl;
            this.dllPath = vars.dllPath;
        }

        public void SetVars (XmlNode node) {
            if (node.Attributes["serverhost"] != null) {
                this.serverHost = node.Attributes["serverhost"].Value;
            }
            if (node.Attributes["serverport"] != null) {
                this.serverPort = node.Attributes["serverport"].Value;
            }
            if (node.Attributes["browserstring"] != null) {
                this.browserString = node.Attributes["browserstring"].Value;
            }
            if (node.Attributes["browserurl"] != null) {
                this.browserUrl = node.Attributes["browserurl"].Value;
            }
            if (node.Attributes["dllpath"] != null) {
                this.dllPath = node.Attributes["dllpath"].Value;
            }
        }

        public bool Validate() {
            bool valid = true;
            if (serverHost == null) {
                Console.WriteLine("No serverhost specified for method.  Method ignored.");
                valid = false;
            }
            if (serverHost == null) {
                Console.WriteLine("No serverport specified for method.  Method ignored.");
                valid = false;
            }
            if (serverHost == null) {
                Console.WriteLine("No browserstring specified for method.  Method ignored.");
                valid = false;
            }
            if (serverHost == null) {
                Console.WriteLine("No browserurl specified for method.  Method ignored.");
                valid = false;
            }
            if (serverHost == null) {
                Console.WriteLine("No dllpath specified for method.  Method ignored.");
                valid = false;
            }

            return valid;
        }
    }
}
