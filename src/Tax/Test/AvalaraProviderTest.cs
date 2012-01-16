using System;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Tax;
using Spring2.Core.Tax.AvalaraTax;

namespace Spring2.Core.Test {
    
    /// <summary>
    /// Provides unit tests for AvalaraProvider class.
    /// </summary>
    [TestFixture()]
    public class AvalaraProviderTest {

	private TaxOrder NewTaxOrder() {
	    TaxOrder order = new TaxOrder();
	    order.OrderId = (Int32)(new DateTime(2012, 1, 1).Ticks - DateTime.Now.Ticks);
	    order.CustomerId = order.OrderId;

	    return order;
	}

	[Test()]
	[ExpectedException("System.NotImplementedException")]
	public void GetTaxRateForArea() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");

	    AvalaraProvider.GetTaxRateForArea(84664, DateType.Today);
	}

	[Test()]
	[Ignore("not address correction")]
	public void GetTaxAreaForAddress() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxAreaData taxArea = AvalaraProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("10150 Centennial Pkwy"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Sandy"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("UT"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("840704103"), taxArea.TaxAreaID);
	}

	[Test()]
	[Ignore("not address correction")]
	public void GetTaxAreaForAddress1() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxAreaData taxArea = AvalaraProvider.GetTaxAreaForAddress("200 W. State St.", "Irene", "", "SD", "57037", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("200 W State St"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Irene"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("SD"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("570372122"), taxArea.TaxAreaID);
	}

	[Test()]
	public void Commit() {
	    TaxOrder order = NewTaxOrder();
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

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Commit("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CalculateWithMultipleOfSameItem() {
	    TaxOrder order = NewTaxOrder();
	    order.TaxAreaId = IdType.Parse("84070");
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);
                
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 2;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 120;
	    taxOrderLine.ItemNumber = "SHIPPING";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CommitWithMultipleOfSameItem() {
	    TaxOrder order = NewTaxOrder();
	    order.TaxAreaId = IdType.Parse("84070");
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);
                
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 2;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 120;
	    taxOrderLine.ItemNumber = "SHIPPING";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Commit("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CommitWithShippingTax() {
	    TaxOrder order = NewTaxOrder();
	    order.TaxAreaId = IdType.Parse("10002");
		
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

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 120;
	    taxOrderLine.ItemNumber = "SHIPPING";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Commit("190 Stanton St", "New York", "", "NY", "10002", "USA", DateType.Now, order);

	    Assert.AreEqual(new DecimalType(8.87500), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(99.41), result.TotalTax);
	}

	[Test()]
	public void GetQuoteTaxTotal() {
	    TaxOrder order = NewTaxOrder();
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
	    

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
		
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void GetQuoteTaxTotalWithShippingTax() {
	    TaxOrder order = NewTaxOrder();
	    order.TaxAreaId = IdType.Parse("92276");
	    
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

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 120;
	    taxOrderLine.ItemNumber = "SHIPPING";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("32025 Monterey Ave", "Thousand Palms", "", "CA", "92276", "USA", DateType.Now, order);

	    Assert.AreEqual(new DecimalType(7.25), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(81.20), result.TotalTax);
	}

	[Test()]
	[Ignore("not address correction")]
	public void LookUpTaxArea() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");			
	    TaxAreaList taxAreaList = AvalaraProvider.LookupTaxArea("1412 W. 1800 N.", "Bountiful", "Salt Lake", "UT", "84664", "USA", DateType.Now, BooleanType.FALSE);
	    Assert.AreEqual(IdType.Parse("840101644"), taxAreaList[0].TaxAreaID);
	}

	[Test()]
	[Ignore("not address correction")]
	public void LookUpTaxArea2() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");			
	    TaxAreaList taxAreaList = AvalaraProvider.LookupTaxArea("6862 S Algonguian Ct", "Aurora", "", "CO", "80016", "USA", DateType.Now, BooleanType.FALSE);
	    Assert.AreEqual(4, taxAreaList[0].TaxJurisdictions.Count);
	}

	[Test()]
	public void CalculateWithTaxRate() {
	    TaxOrder order = NewTaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 13.65M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.85), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.93), result.TotalTax);
	}

	[Test()]
	public void CalculateTaxRateWithDiscount() {
	    TaxOrder order = NewTaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 13.65M;
	    taxOrderLine.DiscountAmount = 10.00M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.85), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.25), result.TotalTax);
	}

	[Test()]
	public void CalculateCTWithShipping() {
	    TaxOrder order = NewTaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 2;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "SHIPPING";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("69 Chester","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.35), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(12.70), result.TotalTax);
	}

	[Test()]
	public void IsShippingTaxable() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");

	    // as of April 20, 2006 UT does not charge tax on shipping
	    TaxAreaData taxArea = AvalaraProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.FALSE, taxArea.IsShippingTaxable);

	    // as of April 20, 2006 CT does charge tax on shipping
	    taxArea = AvalaraProvider.GetTaxAreaForAddress("10 Robbie Road", "Tolland", "", "CT", "06084", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.TRUE, taxArea.IsShippingTaxable);
		
	    // as of August 22, 2006 CO does charge tax on shipping that is not optional
	    taxArea = AvalaraProvider.GetTaxAreaForAddress("11640 Couer d Alene Drive", "Parker", "", "CO", "80138", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.TRUE, taxArea.IsShippingTaxable);
	    
	}
    	
	[Test()]
	public void CalculateWithZeroQuantityItem() {
	    TaxOrder order = NewTaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 0;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 2;
	    taxOrderLine.Quantity = 0;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("69 Chester","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.35), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.0), result.TotalTax);
	}

	[Test()]
	public void ShouldBeAbleToTaxOnValidDataIfStreetDoesNotValidate() {
	    TaxOrder order = NewTaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 0;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 2;
	    taxOrderLine.Quantity = 0;
	    taxOrderLine.Price = 100M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("this is bad data","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.35), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.0), result.TotalTax);
	}
	
	[Test()]
	[Ignore("not address correction")]
	public void GetTaxAreaForAddress2() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxAreaData taxArea = AvalaraProvider.GetTaxAreaForAddress("6578 Lynx Cove", "Littleton", "", "CO", "80124", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("6578 Lynx Cv"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Littleton"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("CO"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("801249535"), taxArea.TaxAreaID);
	}

	[Test()]
	public void GetTaxJurisidictionsWithTaxResult() {
	    TaxOrder order = NewTaxOrder();
	    order.TaxAreaId = IdType.Parse("84070");
		     		
	    TaxOrderLine taxOrderLine;
		     		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);
		     
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxResult result = AvalaraProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
		     
	    Assert.AreEqual(3, result.TaxJurisdictions.Count);
		     	    
	    TaxJurisdiction jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.STATE, jurisidiction.JurisdictionType);
	    Assert.AreEqual("UTAH", jurisidiction.Description.ToString());
	    Assert.AreEqual(4.700m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(23.500m, jurisidiction.Amount.ToDecimal());
		     
	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE", jurisidiction.Description.ToString());
	    Assert.AreEqual(1.3500m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(6.750m, jurisidiction.Amount.ToDecimal());

	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.OTHER];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.OTHER, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE CO TR", jurisidiction.Description.ToString());
	    Assert.AreEqual(0.800m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(4.000m, jurisidiction.Amount.ToDecimal());

	}

	[Test()]
	public void GetTaxJurisidictionsWithTaxAreaData() {
	    AvalaraProvider AvalaraProvider = new AvalaraProvider("United States");
	    TaxAreaData result = AvalaraProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);

	    Assert.AreEqual(3, result.TaxJurisdictions.Count);

	    TaxJurisdiction jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.STATE, jurisidiction.JurisdictionType);
	    Assert.AreEqual("UTAH", jurisidiction.Description.ToString());
	    Assert.AreEqual(4.700m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(47.000m, jurisidiction.Amount.ToDecimal());

	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE", jurisidiction.Description.ToString());
	    Assert.AreEqual(1.3500m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(13.50m, jurisidiction.Amount.ToDecimal());

	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.OTHER];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.OTHER, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE CO TR", jurisidiction.Description.ToString());
	    Assert.AreEqual(0.800m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(8.000m, jurisidiction.Amount.ToDecimal());
	}


	public void GilbertShouldHaveJurisdictions() {
	    AvalaraProvider provider = new AvalaraProvider("United States");
	    //TaxAreaList taxareas = provider.LookupTaxArea("3622 E Leslie Dr", "Gilbert", "", "AZ", "85296", "USA", DateType.Now, BooleanType.TRUE);
	    //foreach (TaxAreaData area in taxareas) {
	    //    Console.Out.WriteLine(area.ToString());
	    //}

	    TaxOrder data = GetDummyTaxOrder("FOOD");
	    TaxResult taxResult = provider.Calculate("3622 E Leslie Dr", "Gilbert", "", "AZ", "85296", "USA", DateType.Now, data);
	    Console.Out.WriteLine(taxResult.ToString());

	    data = GetDummyTaxOrderWithShipping("FOOD");
	    taxResult = provider.Calculate("3622 E Leslie Dr", "Gilbert", "", "AZ", "85296", "USA", DateType.Now, data);
	    Console.Out.WriteLine(taxResult.ToString());

	    data = GetDummyTaxOrderWithShipping("SHIPPING");
	    taxResult = provider.Calculate("3622 E Leslie Dr", "Gilbert", "", "AZ", "85296", "USA", DateType.Now, data);
	    Console.Out.WriteLine(taxResult.ToString());

	    data = GetDummyTaxOrderWithShipping("NON-FOOD");
	    taxResult = provider.Calculate("3622 E Leslie Dr", "Gilbert", "", "AZ", "85296", "USA", DateType.Now, data);
	    Console.Out.WriteLine(taxResult.ToString());
	}

	public void ShouldBeAbleToGetBothPSTAndGST() {
	    //5400 Robinson St
	    //Niagara Falls Ontario
	    //Canada
	    //L2G 2A6 

	    AvalaraProvider provider = new AvalaraProvider("Canada");
	    TaxOrder data = GetDummyTaxOrder("FOOD");
	    TaxResult taxResult = provider.Calculate("5400 Robinson St", "Niagara Falls", "", "ON", "L2G 2A6", "Canada", DateType.Now, data);

	    Assert.AreEqual(2, taxResult.TaxJurisdictions.Count);

	    TaxJurisdiction jurisidiction = taxResult.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.STATE, jurisidiction.JurisdictionType);
	    Assert.AreEqual("ONTARIO", jurisidiction.Description.ToString());
	    Assert.AreEqual(80m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(8m, jurisidiction.Rate.ToDecimal());

	    jurisidiction = taxResult.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTRY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTRY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("CAN", jurisidiction.Description.ToString());
	    Assert.AreEqual(50m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(5m, jurisidiction.Rate.ToDecimal());
	}

	public void ShouldBeAbleToGetHST() {
	    //575 Broadway Boulvard
	    //Grand Falls, N.B.
	    //E3Z 2L2

	    AvalaraProvider provider = new AvalaraProvider("Canada");
	    TaxOrder data = GetDummyTaxOrder("FOOD");
	    TaxResult taxResult = provider.Calculate("575 Broadway Boulvard", "Grand Falls", "", "NB", "E3Z 2L2", "Canada", DateType.Now, data);

	    Assert.AreEqual(1, taxResult.TaxJurisdictions.Count);

	    TaxJurisdiction jurisidiction = taxResult.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTRY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTRY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("CAN", jurisidiction.Description.ToString());
	    Assert.AreEqual(130m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(13m, jurisidiction.Rate.ToDecimal());
	}

	public void ShouldBeAbleToGetGSTOnly() {
	    //5011 - 50 St | Barrhead, AB | T7N 1A2
	    AvalaraProvider provider = new AvalaraProvider("Canada");
	    TaxOrder data = GetDummyTaxOrder("FOOD");
	    TaxResult taxResult = provider.Calculate("5011 - 50 St", "Barrhead", "", "AB", "T7N 1A2", "Canada", DateType.Now, data);

	    Assert.AreEqual(1, taxResult.TaxJurisdictions.Count);

	    TaxJurisdiction jurisidiction = taxResult.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTRY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTRY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("CAN", jurisidiction.Description.ToString());
	    Assert.AreEqual(50m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(5m, jurisidiction.Rate.ToDecimal());
	}


	private TaxOrder GetDummyTaxOrder(String itemNumber) {
	    TaxOrder data = new TaxOrder();
	    data.OrderId = new IdType(1);
	    data.OrderType = "Customer";
	    data.IsTaxExempt = BooleanType.FALSE;
	    data.OrderStatus = "New";
	    data.CompleteDate = DateTimeType.Now;
	    data.DiscountRate = 0.0;
	    data.ShipTotal = 0.0;
	    data.CustomerId = new IdType(1);

	    data.Lines = new TaxOrderLineList();

	    // create the line for the item passed in
	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = new IdType(0);
	    line.DiscountAmount = 0;
	    line.Price = 1000;
	    line.Quantity = 1;
	    line.ExtendedPrice = 1000;
	    line.ItemNumber = itemNumber;
	    data.Lines.Add(line);

	    return data;
	}

	private TaxOrder GetDummyTaxOrderWithShipping(String itemNumber) {
	    TaxOrder order = GetDummyTaxOrder(itemNumber);

	    TaxOrderLine line = new TaxOrderLine();
	    line.OrderLineId = new IdType(0);
	    line.DiscountAmount = 0;
	    line.Price = 1000;
	    line.Quantity = 1;
	    line.ExtendedPrice = 1000;
	    line.ItemNumber = "SHIPPING";
	    order.Lines.Add(line);

	    return order;
	}
    }
}