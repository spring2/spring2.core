using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Types.Generic;
using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test.Generic {

    [TestFixture]
    public class DataTypeTest {

	[Test]
	public void ShouldCompareEqual() {
	    DataType<Int32> t1 = new DataType<Int32>(1);
	    DataType<Int32> t2 = new DataType<Int32>(1);

	    Assert.AreEqual(t1, t2);
	    Assert.IsTrue(t1 == t2);
	}

	[Test]
	public void ShouldCompareNotEqual() {
	    DataType<Int32> t1 = new DataType<Int32>(1);
	    DataType<Int32> t2 = new DataType<Int32>(2);

	    Assert.AreNotEqual(t1, t2);
	    Assert.IsTrue(t1 != t2);
	}


	[Test]
	public void ShouldCompareLessThan() {
	    DataType<Int32> t1 = new DataType<Int32>(1);
	    Int32 t2 = 2;

	    Assert.IsTrue(t1 < t2);
	}

	[Test]
	public void ShouldCompareGreaterThan() {
	    DataType<Int32> t1 = new DataType<Int32>(1);
	    Int32 t2 = 2;

	    Assert.IsTrue(t2 > t1);
	}

	[Test]
	public void foo() {
	    DataType<Int32> t1 = new DataType<Int32>(1);
	    DataType<Int32> t2 = new DataType<Int32>(2);
	    DataType<Int32> t1_1 = new DataType<Int32>(1);
	    DataType<Int32> _unset = new DataType<Int32>(TypeState.UNSET);
	    DataType<Int32> _default = new DataType<Int32>(TypeState.DEFAULT);


	    Int32 i = t1.Value;
	    Assert.AreEqual(1, i);
	    try {
		var foo = _unset.Value;
	    } catch(InvalidOperationException) {
		// pass
	    }
	    try {
		var foo = _default.Value;
	    } catch (InvalidOperationException) {
		// pass
	    }

	    Assert.IsTrue(_unset.IsUnset);
	    Assert.IsFalse(_unset.IsDefault);
	    Assert.IsFalse(_unset.IsValid);

	    Assert.IsFalse(_default.IsUnset);
	    Assert.IsTrue(_default.IsDefault);
	    Assert.IsFalse(_default.IsValid);

	    Assert.IsFalse(t1.IsUnset);
	    Assert.IsFalse(t1.IsDefault);
	    Assert.IsTrue(t1.IsValid);

	    Assert.IsTrue(t1 < t2);
	    Assert.IsTrue(t2 > t1);
	    Assert.IsTrue(t1 != t2);
	    Assert.IsTrue(t1 == t1_1);

	    Assert.IsTrue(t1 == 1);
	    Assert.IsTrue(t1 != 2);

	    Assert.IsTrue(IntegerType.DEFAULT > IntegerType.UNSET);
	    Assert.IsTrue(IntegerType.ZERO > IntegerType.UNSET);
	    Assert.IsTrue(IntegerType.ZERO > IntegerType.DEFAULT);

	    Assert.IsFalse(IntegerType.DEFAULT < IntegerType.UNSET);
	    Assert.IsFalse(IntegerType.ZERO < IntegerType.UNSET);
	    Assert.IsFalse(IntegerType.ZERO < IntegerType.DEFAULT);

	    Assert.IsTrue(IntegerType.UNSET < IntegerType.ZERO);
	    Assert.IsTrue(IntegerType.DEFAULT < IntegerType.ZERO);
	    Assert.IsTrue(IntegerType.UNSET < IntegerType.DEFAULT);

	    Assert.IsFalse(IntegerType.UNSET > IntegerType.ZERO);
	    Assert.IsFalse(IntegerType.DEFAULT > IntegerType.ZERO);
	    Assert.IsFalse(IntegerType.UNSET > IntegerType.DEFAULT);

	    Assert.IsFalse(_unset.Equals(null));
	    Assert.IsFalse(_unset.Equals(_default));
	    Assert.IsFalse(_default.Equals(_unset));
	    Assert.IsFalse(_unset.Equals(t1));

	    Assert.IsTrue(t1.Equals(t1_1));
	    Assert.IsTrue(_unset.Equals(new DataType<Int32>(TypeState.UNSET)));
	    Assert.IsTrue(_default.Equals(new DataType<Int32>(TypeState.DEFAULT)));

	    Assert.IsFalse(t1.Equals(null));
	    Assert.IsFalse(t1.Equals(_default));
	    Assert.IsFalse(t1.Equals(_unset));


	    Assert.IsFalse(t1.Equals(null));
	    Assert.IsFalse(t1.Equals(new DataType<Int64>(1)));


	    Assert.IsTrue(_default > _unset);
	    Assert.IsTrue(t1 > _unset);
	    Assert.IsTrue(t1 > _default);

	    Assert.IsFalse(_default < _unset);
	    Assert.IsFalse(t1 < _unset);
	    Assert.IsFalse(t1 < _default);

	    Assert.IsTrue(_unset < t1);
	    Assert.IsTrue(_default < t1);
	    Assert.IsTrue(_unset < _default);

	    Assert.IsFalse(_unset > t1);
	    Assert.IsFalse(_default > t1);
	    Assert.IsFalse(_unset > _default);



	    Assert.IsTrue(1 > _unset);
	    Assert.IsTrue(1 > _default);

	    Assert.IsFalse(1 < _unset);
	    Assert.IsFalse(1 < _default);

	    Assert.IsTrue(_unset < 1);
	    Assert.IsTrue(_default < 1);

	    Assert.IsFalse(_unset > 1);
	    Assert.IsFalse(_default > 1);


	    Assert.AreEqual(0, _unset.GetHashCode());
	    Assert.AreEqual(0, _default.GetHashCode());
	    Assert.AreEqual(1.GetHashCode(), t1.GetHashCode());

	    Assert.AreEqual("UNSET", _unset.ToString());
	    Assert.AreEqual("DEFAULT", _default.ToString());
	    Assert.AreEqual("1", t1.ToString());

	    i = (Int32)t2;
	    Assert.AreEqual(2, i);


	    Assert.AreEqual(1, t1.GetValueOrDefault());
	    Assert.AreEqual(1, t1.GetValueOrDefault(2));
	    Assert.AreEqual(0, _unset.GetValueOrDefault());
	    Assert.AreEqual(2, _unset.GetValueOrDefault(2));


	    Assert.IsTrue(_default == _default);
	    Assert.IsTrue(_unset == _unset);
	    Assert.IsTrue(t1 == t1);

	    Object o = t1;
	    Assert.IsTrue(t1.Equals((DataType<Int32>)o));
	}





    }
}
