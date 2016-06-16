using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

using Spring2.Core.Xml;

namespace Spring2.Core.SeleniumLoadTester {
    public class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                throw new FileNotFoundException("No XML test configuration file specified.");
            }

            SeleniumLoadTester tester = new SeleniumLoadTester(args[0]);

            tester.RunThreads();
        }
    }
}
