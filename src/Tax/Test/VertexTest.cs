using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Tax;
using Spring2.Core.Tax.Vertex;

namespace Spring2.Core.Test {
    
    /// <summary>
    /// Summary description for CountryTest.
    /// </summary>
    [TestFixture()]
    public class VertexTest  {
        
	internal static StringType CITY = StringType.Parse("TAYLORSVILLE");
	internal static StringType POSTALCODE = StringType.Parse("84123");
	internal static StringType COUNTY = StringType.Parse("SALT LAKE");
	internal static StringType COUNTRY = StringType.Parse("US");
	internal static StringType REGION = StringType.Parse(USStateCodeEnum.UTAH.ToString());
        
	[SetUp()]
	public void Setup() {
	}

	[Test()]
	public void TaxAreaLookup1() {
	    VertexProvider tax = new VertexProvider("US");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT",  "84070", "United States", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("SANDY"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("450350280"), taxArea.TaxAreaID);
	}

	[Test()]
	public void TaxAreaLookup2() {
	    VertexProvider tax = new VertexProvider("US");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Aztec", "", "NM", "87410", "United States", BooleanType.FALSE);
	    	
	    Assert.AreEqual(StringType.Parse("AZTEC"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("320450040"), taxArea.TaxAreaID);
	}

	[Test()]
	public void TaxAreaLookup3() {
	    VertexProvider tax = new VertexProvider("US");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Dubuque", "", "IA", "52003", "United States", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("DUBUQUE"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("160610330"), taxArea.TaxAreaID);
	}

	[Test()]
	public void TaxAreaLookup4() {
	    VertexProvider tax = new VertexProvider("US");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Manteca", "", "CA", "95336", "United States", BooleanType.FALSE);
	
	    Assert.AreEqual(StringType.Parse("MANTECA"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("50771960"), taxArea.TaxAreaID);
	}
	
	[Test()]
	public void TaxAreaLookup5() {
	    VertexProvider tax = new VertexProvider("US");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Woodstock", "", "IL", "60098", "United States", BooleanType.FALSE);
	
	    Assert.AreEqual(StringType.Parse("WOODSTOCK"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("141113240"), taxArea.TaxAreaID);
	}

	[Test()]
	public void TaxAreaLookup6() {
	    VertexProvider tax = new VertexProvider("CA");

	    TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Toronto", "", "ON", "M6J 1V5", "Canada", BooleanType.FALSE);
	
	    Assert.AreEqual(StringType.Parse("TORONTO"), taxArea.TaxArea);
	    Assert.AreEqual(IdType.Parse("700151420"), taxArea.TaxAreaID);
	}

	[Test()]
	[Ignore("")]
	public void TaxAreaLookup7() {
	    try {
		VertexProvider tax = new VertexProvider("CA");

		TaxAreaData taxArea = tax.GetTaxAreaForAddress("", "Montréal", "", "QC", "H3B 2T6", "Canada", BooleanType.FALSE);
	
		Assert.AreEqual(StringType.Parse("TORONTO"), taxArea.TaxArea);
		Assert.AreEqual(IdType.Parse("700151420"), taxArea.TaxAreaID);
	    } catch (Spring2.Core.Message.MessageListException ex) {
		foreach (Spring2.Core.Message.Message message in ex.Messages) {
		    System.Console.WriteLine(message.Message);
		}
	    }
	}

	[Test()]
	[Ignore("This fails, there is an open request with Vertex on this.")]
	public void TaxRateLookup1() {
	    VertexProvider tax = new VertexProvider("US");

	    DecimalType taxRate = tax.GetTaxRateForArea(new IdType(170690000), DateType.Now).TotalTaxRate;
	    Assert.AreEqual(6.45, taxRate.ToDecimal());
	}

	[Test()]
	//[Ignore("This fails, there is an open request with Vertex on this.")]
	public void TaxRateLookup2() {
	    VertexProvider tax = new VertexProvider("US");

	    DecimalType taxRate = tax.GetTaxRateForArea(new IdType(50391940), DateType.Now).TotalTaxRate;
	    Assert.AreEqual(7.25, taxRate.ToDecimal());
	}

	//Toronto
	[Test()]
	[Ignore("Test server does not have canadian data")]
	public void TaxRateLookup3() {
	    VertexProvider tax = new VertexProvider("CA");

	    DecimalType taxRate = tax.GetTaxRateForArea(new IdType(700151420), DateType.Now).TotalTaxRate;
	    Assert.AreEqual(15.00M, taxRate.ToDecimal());
	}

	[Test]
	public void TaxQuote() {
	    VertexProvider provider = new VertexProvider("US");

	    TaxOrder order = new TaxOrder();

	    TaxOrderLineList list = new TaxOrderLineList();
	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = 1;
	    line.DiscountAmount = 0;
	    line.Price = 100;
	    line.Quantity = 1;
	    line.ExtendedPrice = 100;
	    line.ItemNumber = "123";
	    list.Add(line);

	    line = new TaxOrderLine();
	    line.OrderLineId = 2;
	    line.DiscountAmount = 0;
	    line.Price = 50;
	    line.Quantity = 1;
	    line.ExtendedPrice = 50;
	    line.ItemNumber = "234";
	    list.Add(line);

	    order.Lines = list;
	    order.OrderId = 123456;
	    order.CompleteDate = DateTimeType.Now;
	    order.TaxAreaId = 320450040;
	    order.ShipTotal = 0;

	    TaxResult result = provider.GetQuoteTaxTotal(order, "USD");

	    Assert.AreEqual(new CurrencyType(11.44), result.TotalTax);
	    Assert.AreEqual(2, result.Lines.Count);
	    Assert.AreEqual(new CurrencyType(7.62), result.Lines[0].TaxAmount);
	    Assert.AreEqual(new CurrencyType(3.82), result.Lines[1].TaxAmount);

	    Assert.AreEqual(result.TotalTax, result.Lines[0].TaxAmount + result.Lines[1].TaxAmount);

	}

	[Test]
	public void TaxInvoice() {
	    VertexProvider provider = new VertexProvider("US");

	    TaxOrder order = new TaxOrder();

	    TaxOrderLineList list = new TaxOrderLineList();
	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = 1;
	    line.DiscountAmount = 0;
	    line.Price = 100;
	    line.Quantity = 1;
	    line.ExtendedPrice = 100;
	    line.ItemNumber = "123";
	    list.Add(line);

	    line = new TaxOrderLine();
	    line.OrderLineId = 2;
	    line.DiscountAmount = 0;
	    line.Price = 50;
	    line.Quantity = 1;
	    line.ExtendedPrice = 50;
	    line.ItemNumber = "234";
	    list.Add(line);

	    line = new TaxOrderLine();
	    line.OrderLineId = 3;
	    line.DiscountAmount = 0;
	    line.Price = 50;
	    line.Quantity = 1;
	    line.ExtendedPrice = 50;
	    line.ItemNumber = "SHIPPING";
	    list.Add(line);

	    order.Lines = list;
	    order.OrderId = 123456;
	    order.CompleteDate = DateTimeType.Now;
	    order.TaxAreaId = 320450040;
	    order.ShipTotal = 0;

	    TaxResult result = provider.Commit(order, "USD");

	    Assert.AreEqual(new CurrencyType(15.25), result.TotalTax);
	    Assert.AreEqual(3, result.Lines.Count);
	    Assert.AreEqual(new CurrencyType(7.61), result.Lines[0].TaxAmount);
	    Assert.AreEqual(new CurrencyType(3.82), result.Lines[1].TaxAmount);
	    Assert.AreEqual(new CurrencyType(3.82), result.Lines[2].TaxAmount);

	    Assert.AreEqual(result.TotalTax, result.Lines[0].TaxAmount + result.Lines[1].TaxAmount + result.Lines[2].TaxAmount);
	}

	[Test]
	public void TaxQuoteWithZeroQuantityLine() {
	    VertexProvider provider = new VertexProvider("US");

	    TaxOrder order = new TaxOrder();

	    TaxOrderLineList list = new TaxOrderLineList();
	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = 1;
	    line.DiscountAmount = 0;
	    line.Price = 100;
	    line.Quantity = 0;
	    line.ExtendedPrice = 100;
	    line.ItemNumber = "123";
	    list.Add(line);

	    line = new TaxOrderLine();
	    line.OrderLineId = 2;
	    line.DiscountAmount = 0;
	    line.Price = 50;
	    line.Quantity = 1;
	    line.ExtendedPrice = 50;
	    line.ItemNumber = "234";
	    list.Add(line);

	    order.Lines = list;
	    order.OrderId = 123456;
	    order.CompleteDate = DateTimeType.Now;
	    order.TaxAreaId = 320450040;
	    order.ShipTotal = 0;

	    TaxResult result = provider.GetQuoteTaxTotal(order, "USD");

	    Assert.AreEqual(1, result.Lines.Count);
	}

	[Test]
	public void TaxQuoteWithZeroPriceLine() {
	    VertexProvider provider = new VertexProvider("US");

	    TaxOrder order = new TaxOrder();

	    TaxOrderLineList list = new TaxOrderLineList();
	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = 1;
	    line.DiscountAmount = 0;
	    line.Price = 0;
	    line.Quantity = 1;
	    line.ExtendedPrice = 0;
	    line.ItemNumber = "123";
	    list.Add(line);

	    line = new TaxOrderLine();
	    line.OrderLineId = 2;
	    line.DiscountAmount = 0;
	    line.Price = 50;
	    line.Quantity = 1;
	    line.ExtendedPrice = 50;
	    line.ItemNumber = "234";
	    list.Add(line);

	    order.Lines = list;
	    order.OrderId = 123456;
	    order.CompleteDate = DateTimeType.Now;
	    order.TaxAreaId = 320450040;
	    order.ShipTotal = 0;

	    TaxResult result = provider.GetQuoteTaxTotal(order, "USD");

	    Assert.AreEqual(2, result.Lines.Count);
	}
    }
}