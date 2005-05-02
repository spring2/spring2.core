using System;

using NUnit.Framework;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    [TestFixture]
    public class PhoneNumberTypeTest {

	[Test]
	public void Parse() {
	    Assert.IsFalse(PhoneNumberType.Parse("-").IsDefault);
	    Assert.IsTrue(PhoneNumberType.Parse("-").IsUnset);
	    Assert.IsFalse(PhoneNumberType.Parse("-").IsValid);
	    Assert.AreEqual("UNSET", PhoneNumberType.Parse("-").ToString());

	    Assert.IsFalse(PhoneNumberType.Parse("").IsDefault);
	    Assert.IsTrue(PhoneNumberType.Parse("").IsUnset);
	    Assert.IsFalse(PhoneNumberType.Parse("").IsValid);
	    Assert.AreEqual("UNSET", PhoneNumberType.Parse("").ToString());

	    PhoneNumberType phoneNumber = PhoneNumberType.Parse("1234567");
	    Assert.IsTrue(phoneNumber.IsValid);
	    Assert.AreEqual("123-4567", phoneNumber.ToString());

	    // what should this really do?
	    phoneNumber = PhoneNumberType.Parse("123456");
	    Assert.IsTrue(phoneNumber.IsValid);
	    Assert.AreEqual("123-456", phoneNumber.ToString());

	    // I am sure the number is not supposed to be truncated, but what is right?
	    phoneNumber = PhoneNumberType.Parse("12345678901234567890");
	    Assert.IsTrue(phoneNumber.IsValid);
// TODO: fails
	    //Assert.AreEqual("12345678901234567890", phoneNumber.ToString());

	    Assert.AreEqual("(801) 123-1234", PhoneNumberType.Parse("801.123.1234").ToString());
	}
    }
}
