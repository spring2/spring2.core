using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PostSharp.Aspects;
using Spring2.Core.PostSharp;

namespace Spring2.Core.PostSharp.Test {
    
    [TestFixture]
    public class MultipleAspectsTest {

	[Test]
	[Log4NetTrace(EntryLevel = LogLevel.Debug, ExitLevel = LogLevel.Info, ExceptionLevel = LogLevel.Error)]
	[DbConnectionScope()]
	public void ShouldBeAbleToHaveMultipleAspects() {
	}

    }
}
