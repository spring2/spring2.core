using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.IoC;
using Spring2.Core.DAO;
using NUnit.Framework;

namespace Spring2.Core.Test.IoC {

    [TestFixture]
    public class ClassRegistryTest {

	[Test]
	public void ShouldResolveRegisteredClass() {

	    ClassRegistry container = new ClassRegistry();
	    //registering dependecies
	    container.Register<IConnectionStringStrategy>(delegate {
		return new DefaultConnectionStringStrategy();
	    });

	    //using the container
	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), container.Create<IConnectionStringStrategy>());
	}
    }
}
