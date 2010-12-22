using System;

using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Spring2.Core.Types;


namespace Spring2.Core.Test.Serialization {

    [TestFixture]
    public class DateTypeTest {

	[Serializable]
	public class ValueObject : DataObject.DataObject {
	    private DateType _valid = new DateType(1);
	    private DateType _unset = DateType.UNSET;
	    private DateType _default = DateType.DEFAULT;

	    public DateType Valid {
		get { return _valid; }
		set { _valid = value; }
	    }

	    public DateType Unset {
		get { return _unset; }
		set { _unset = value; }
	    }

	    public DateType Default {
		get { return _default; }
		set { _default = value; }
	    }
	}

	/// <summary>
	/// Utility method to serialize to memory and then deserialize an object
	/// </summary>
	/// <param name="value"></param>
	/// <returns></returns>
	private Object SerializeDeserialze(Object value) {
	    BinaryFormatter binaryFmt = new BinaryFormatter();
	    MemoryStream ms = new MemoryStream();
	    binaryFmt.Serialize(ms, value);

	    // Deserialize.
	    ms.Position = 0;
	    Object value2 = binaryFmt.Deserialize(ms);
	    ms.Close();

	    return value2;
	}

	[Test]
	public void ShouldBinarySerializeWithValue() {
	    DateType s = new DateType(1);
	    DateType s2 = (DateType)SerializeDeserialze(s);

	    Assert.IsTrue(s2.IsValid);
	    Assert.IsTrue(s.Equals(s2));
	}

	[Test]
	public void ShouldBinarySerializeUnset() {
	    DateType s = DateType.UNSET;
	    DateType s2 = (DateType)SerializeDeserialze(s);

	    Assert.IsTrue(s2.IsUnset);
	    Assert.IsTrue(s.Equals(s2));
	}

	[Test]
	public void ShouldBinarySerializeDefault() {
	    DateType s = DateType.DEFAULT;
	    DateType s2 = (DateType)SerializeDeserialze(s);

	    Assert.IsTrue(s2.IsDefault);
	    Assert.IsTrue(s.Equals(s2));
	}

	[Test]
	public void ShouldBinarySerializeInValueObject() {
	    ValueObject vo = new ValueObject();
	    ValueObject vo2 = (ValueObject)SerializeDeserialze(vo);

	    Assert.IsTrue(vo.Equals(vo2));
	    Assert.IsTrue(vo.Valid.IsValid);
	    Assert.IsTrue(vo.Unset.IsUnset);
	    Assert.IsTrue(vo.Default.IsDefault);
	}

    }

}
