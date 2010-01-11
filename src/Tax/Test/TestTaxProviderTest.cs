using System;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Tax;
using Spring2.Core.Message;

namespace Spring2.Core.Tax.Test {
    [TestFixture()]
    public class TestTaxProviderTest {
	[SetUp()]
	public void Setup() {
	}

	[Test()]
	public void TaxAreaLookup1() {
	    try {
		TestTaxProvider tax = new TestTaxProvider("US");

		TaxRateInfo info = tax.GetTaxRateForArea(IdType.DEFAULT, DateType.DEFAULT);
		Assert.Fail();	//	Should fail in the previous call
	    } catch (MessageListException exc) {
		Assert.IsTrue(exc.Messages.Count > 0);
		Assert.AreEqual(typeof(InvalidTaxAreaIdException), exc.Messages[0].GetType());
	    }
	}

	[Test]
	public void TaxJurisdictionBreakdownShouldMatchTotal() {
	    TestTaxProvider provider = new TestTaxProvider("US");
	    TaxAreaList taxAreaList = provider.LookupTaxArea("6862 S Algonguian Ct", "Aurora", "", "CO", "80016", "USA", DateType.Now, BooleanType.FALSE);

	    CurrencyType total = CurrencyType.ZERO;
	    DecimalType rate = DecimalType.ZERO;
	    foreach (TaxJurisdiction j in taxAreaList[0].TaxJurisdictions) {
		total += j.Amount;
		rate += j.Rate;
	    }

	    Assert.AreEqual(rate, taxAreaList[0].TotalTaxRate);

	}

	[Test()]
	public void GetQuoteTaxTotal() {
	    TaxOrder order = new TaxOrder();
	    order.TaxAreaId = IdType.Parse("84070");

	    TaxOrderLine taxOrderLine;

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "002";
	    order.Lines.Add(taxOrderLine);


	    TestTaxProvider provider = new TestTaxProvider("US");
	    TaxResult result = provider.Calculate("10150 South Centennial Parkway", "Sandy", "", "UT", "84070", "USA", DateType.Now, order);

	    Assert.AreEqual(new CurrencyType(66.00), result.TotalTax);

	    CurrencyType total = CurrencyType.ZERO;
	    DecimalType rate = DecimalType.ZERO;
	    foreach (TaxJurisdiction j in result.TaxJurisdictions) {
		total += j.Amount;
		rate += j.Rate;
	    }

	    Assert.AreEqual(rate, result.TotalTaxRate);
	    Assert.AreEqual(result.TotalTax, total);
	}

    }
}
