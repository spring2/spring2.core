using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.IoC;
using Spring2.Core.DAO;
using NUnit.Framework;
using System.Drawing;
using System.ComponentModel;

namespace Spring2.Core.Test.IoC {

    [TestFixture]
    public class ClassRegistryTest {

	[Test]
	public void CanResolveRegisteredDelegate() {
	    ClassRegistry.Register<IConnectionStringStrategy>(() => { return new DefaultConnectionStringStrategy(); });
	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), ClassRegistry.Resolve<IConnectionStringStrategy>());
	}

	[Test]
	public void CanResolveRegisteredInstance() {
	    DefaultConnectionStringStrategy instance = new DefaultConnectionStringStrategy();
	    ClassRegistry.Register<IConnectionStringStrategy>(instance);
	    IConnectionStringStrategy resolvedInstance = ClassRegistry.Resolve<IConnectionStringStrategy>();
	    Assert.AreSame(resolvedInstance, instance);
	}

	[Test]
	public void CanResolveTransientType() {
	    ClassRegistry.Register<DefaultConnectionStringStrategy, DefaultConnectionStringStrategy>();

	    DefaultConnectionStringStrategy instance1 = ClassRegistry.Resolve<DefaultConnectionStringStrategy>();
	    DefaultConnectionStringStrategy instance2 = ClassRegistry.Resolve<DefaultConnectionStringStrategy>();

	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), instance1);
	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), instance2);
	    Assert.IsFalse(ReferenceEquals(instance1, instance2));
	}

	[Test]
	public void CanResolveSingletonType() {
	    ClassRegistry.Register<DefaultConnectionStringStrategy, DefaultConnectionStringStrategy>(true);

	    IConnectionStringStrategy instance1 = ClassRegistry.Resolve<DefaultConnectionStringStrategy>();
	    IConnectionStringStrategy instance2 = ClassRegistry.Resolve<DefaultConnectionStringStrategy>();

	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), instance1);
	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), instance2);
	    Assert.IsTrue(ReferenceEquals(instance1, instance2));
	}

	[Test]
	public void CanResolveIsTrueForRegisteredType() {
	    DefaultConnectionStringStrategy instance = new DefaultConnectionStringStrategy();
	    ClassRegistry.Register<IConnectionStringStrategy>(instance);
	    Assert.IsTrue(ClassRegistry.CanResolve(typeof(IConnectionStringStrategy)));
	}

	[Test]
	public void CanResolveIsFalseForUnregisteredType() {
	    Assert.IsFalse(ClassRegistry.CanResolve(typeof(IComparable)));
	}

	[Test]
	public void CanResolveIsTrueForRegisteredContract() {
	    DefaultConnectionStringStrategy instance = new DefaultConnectionStringStrategy();
	    ClassRegistry.Register<IConnectionStringStrategy>(instance);
	    Assert.IsTrue(ClassRegistry.CanResolve<IConnectionStringStrategy>());
	}

	[Test]
	public void CanResolveIsFalseForUnregisteredContract() {
	    Assert.IsFalse(ClassRegistry.CanResolve<IComparable>());
	}

	[Test]
	public void CanFlushDependencies() {
	    ClassRegistry.Register<IConnectionStringStrategy>(new DefaultConnectionStringStrategy());
	    Assert.IsInstanceOfType(typeof(DefaultConnectionStringStrategy), ClassRegistry.Resolve<IConnectionStringStrategy>());
	    ClassRegistry.Flush();
	    Assert.IsFalse(ClassRegistry.CanResolve<IConnectionStringStrategy>());
	}

	[Test]
	public void CanFlushDependenciesNoDispose() {
	    DisposableClass disposeMe = new DisposableClass();
	    ClassRegistry.Register<DisposableClass>(disposeMe);
	    Assert.AreSame(disposeMe, ClassRegistry.Resolve<DisposableClass>());
	    ClassRegistry.Flush();
	    Assert.IsFalse(ClassRegistry.CanResolve<DisposableClass>());
	    Assert.IsFalse(disposeMe.Disposed);
	}

	[Test]
	public void CanFlushDependenciesDispose() {
	    DisposableClass disposeMe = new DisposableClass();
	    ClassRegistry.Register<DisposableClass>(disposeMe);
	    Assert.AreSame(disposeMe, ClassRegistry.Resolve<DisposableClass>());
	    ClassRegistry.Flush(true);
	    Assert.IsFalse(ClassRegistry.CanResolve<DisposableClass>());
	    Assert.IsTrue(disposeMe.Disposed);
	}

	private class DisposableClass : IDisposable {
	    public bool Disposed { private set; get; }
	    public DisposableClass() { }
	    public void Dispose() { Disposed = true; }
	}
    }
}