using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Spring2.Core.IoC;

namespace Spring2.Core.RhinoMocks {
    public class TestMockUtility : MockRepository {
	/// <summary>
	/// Gets the list of clean up Action delegates.
	/// </summary>
	/// <value>
	/// The clean up delegates.
	/// </value>
	public IList<Action> CleanUps { protected set; get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="TestMockUtility"/> class.
	/// </summary>
	public TestMockUtility() {
	    CleanUps = new List<Action>();
	}

	/// <summary>
	/// Clears the Rhino Mocks repository and runs tear downs.
	/// </summary>
	public void TearDown() {
	    foreach (Action cleanUp in CleanUps) {
		cleanUp();
	    }
	    CleanUps.Clear();
	    CleanUps = null;
	    ClassRegistry.Flush(true);
	}

	/// <summary>
	/// Resets the expectations on a mock or stub.
	/// </summary>
	/// <param name="mock">The mock or stub which is to have its expectations reset.</param>
	/// <param name="options">The reset options.</param>
	public void ResetExpectations(object mock, BackToRecordOptions options) {
	    mock.BackToRecord(options);
	    mock.Replay();
	}

	/// <summary>
	/// Resets the expectations on all mocks and stubs held by this instance of TestMockUtility.
	/// </summary>
	/// <param name="options">The reset options.</param>
	public void ResetExpectationsAll(BackToRecordOptions options) {
	    BackToRecordAll(options);
	    ReplayAll();
	}

	#region Strict Mocks
	/// <summary>
	/// Creates a strict mock object of type T.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object.</returns>
	public new T StrictMock<T>(params object[] arguments) {
	    T theType = base.StrictMock<T>(arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a strict mock object of type T, and any other interfaces it may implement.
	/// </summary>
	/// <param name="extraTypes">The extra types.</param>
	/// <returns>The mocked object.</returns>
	public new T StrictMultiMock<T>(params Type[] extraTypes) {
	    T theType = base.StrictMultiMock<T>(extraTypes);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a strict mock object of type T, and any other interfaces it may implement.
	/// </summary>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object.</returns>
	public new T StrictMultiMock<T>(Type[] extraTypes, params object[] arguments) {
	    T theType = base.StrictMultiMock<T>(extraTypes, arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a strict mock object of type T, and registers it in the ClassRegistry.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T StrictMockAndRegister<T>(params object[] arguments) {
	    T theType = base.StrictMock<T>(arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a strict mock object of type T, and any other interfaces it may implement. It is also registered into the ClassRegistry.
	/// </summary>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T StrictMultiMockAndRegister<T>(Type[] extraTypes, params object[] arguments) {
	    T theType = base.StrictMultiMock<T>(extraTypes, arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}
	#endregion

	#region Partial Mocks
	/// <summary>
	/// Creates a Parial mock object of type T, where T is a class.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="arguments">The arguments.</param>
	/// <returns>The mocked class object.</returns>
	public new T PartialMock<T>(params object[] arguments) where T : class {
	    T theType = base.PartialMock<T>(arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a Parial mock object of type T, where T is a class, and any other interfaces it may implement.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="extraTypes">The extra types.</param>
	/// <returns>The mocked class object.</returns>
	public new T PartialMultiMock<T>(params Type[] extraTypes) where T : class {
	    T theType = base.PartialMultiMock<T>(extraTypes);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a Parial mock object of type T, where T is a class, and any other interfaces it may implement.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The contructor arguments.</param>
	/// <returns>The mocked class object.</returns>
	public new T PartialMultiMock<T>(Type[] extraTypes, params object[] arguments) where T : class {
	    T theType = base.PartialMultiMock<T>(extraTypes, arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a partial mock object of type T, and registers it in the ClassRegistry.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T PartialMockAndRegister<T>(params object[] arguments) where T : class {
	    T theType = base.PartialMock<T>(arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a partial mock object of type T, and any other interfaces it may implement. It is also registered into the ClassRegistry.
	/// </summary>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T PartialMultiMockAndRegister<T>(Type[] extraTypes, params object[] arguments) {
	    T theType = base.PartialMultiMock<T>(extraTypes, arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}
	#endregion

	#region Dynamic Mocks
	/// <summary>
	/// Creates a dynamic mock object of type T, where T is a class.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="arguments">The arguments.</param>
	/// <returns>The mocked class object.</returns>
	public new T DynamicMock<T>(params object[] arguments) where T : class {
	    T theType = base.DynamicMock<T>(arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a dynamic mock object of type T, where T is a class, and any other interfaces it may implement.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="extraTypes">The extra types.</param>
	/// <returns>The mocked class object.</returns>
	public new T DynamicMultiMock<T>(params Type[] extraTypes) where T : class {
	    T theType = base.DynamicMultiMock<T>(extraTypes);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a dynamic mock object of type T, where T is a class, and any other interfaces it may implement.
	/// </summary>
	/// <typeparam name="T">Where T is a class.</typeparam>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The contructor arguments.</param>
	/// <returns>The mocked class object.</returns>
	public new T DynamicMultiMock<T>(Type[] extraTypes, params object[] arguments) where T : class {
	    T theType = base.DynamicMultiMock<T>(extraTypes, arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a dynamic mock object of type T, and registers it in the ClassRegistry.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T DynamicMockAndRegister<T>(params object[] arguments) where T : class {
	    T theType = base.DynamicMock<T>(arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Creates a dynamic mock object of type T, and any other interfaces it may implement. It is also registered into the ClassRegistry.
	/// </summary>
	/// <param name="extraTypes">The extra types.</param>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The mocked object that has been registered with the ClassRegistry.</returns>
	public T DynamicMultiMockAndRegister<T>(Type[] extraTypes, params object[] arguments) where T : class {
	    T theType = base.DynamicMultiMock<T>(extraTypes, arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}
	#endregion

	#region Stubs
	/// <summary>
	/// Stubs the specified arguments.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The stubbed object.</returns>
	public new T Stub<T>(params object[] arguments) {
	    T theType = base.Stub<T>(arguments);
	    theType.Replay();
	    return theType;
	}

	/// <summary>
	/// Stubs an object of type T and registers it in the ClassRegistry.
	/// </summary>
	/// <param name="arguments">The constructor arguments.</param>
	/// <returns>The stubbed object that has been registered with the ClassRegistry.</returns>
	public T StubAndRegister<T>(params object[] arguments) where T : class {
	    T theType = base.Stub<T>(arguments);
	    ClassRegistry.Register<T>(theType);
	    theType.Replay();
	    return theType;
	}
	#endregion
    }
}