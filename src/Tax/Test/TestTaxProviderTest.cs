using System;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Tax;
using Spring2.Core.Message;

namespace Spring2.Core.Tax.Test
{
	[TestFixture()]
	public class TestTaxProviderTest
	{
		[SetUp()]
		public void Setup() 
		{
		}

		[Test()]
		public void TaxAreaLookup1() 
		{
			try
			{
				TestTaxProvider tax = new TestTaxProvider("US");

				TaxRateInfo info = tax.GetTaxRateForArea(IdType.DEFAULT, DateType.DEFAULT);
				Assert.Fail();	//	Should fail in the previous call
			}
			catch(MessageListException exc)
			{
				Assert.IsTrue(exc.Messages.Count > 0);
				Assert.AreEqual(typeof(InvalidTaxAreaIdException), exc.Messages[0].GetType());
			}
		}

	}
}
