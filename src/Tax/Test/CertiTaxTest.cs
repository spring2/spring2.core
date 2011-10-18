using System;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Tax;
using Spring2.Core.Tax.CertiTax;

namespace Spring2.Core.Test {
    
    /// <summary>
    /// Provides unit tests for CertiTaxProvider class.
    /// </summary>
    [TestFixture()]
    public class CertiTaxTest {

	[Test()]
	public void GetTaxRateForArea() {
	    TaxRateInfo taxRateInfo = new TaxRateInfo();
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");

	    taxRateInfo = CertiTaxProvider.GetTaxRateForArea(84664, DateType.Today);
	    Assert.AreEqual(new DecimalType(6.750), taxRateInfo.TotalTaxRate);
	}

	[Test()]
	public void GetTaxAreaForAddress() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxAreaData taxArea = CertiTaxProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("10150 Centennial Pkwy"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Sandy"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("UT"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("840704103"), taxArea.TaxAreaID);
	}

	[Test()]
	public void GetTaxAreaForAddress1() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxAreaData taxArea = CertiTaxProvider.GetTaxAreaForAddress("200 W. State St.", "Irene", "", "SD", "57037", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("200 W State St"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Irene"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("SD"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("570372122"), taxArea.TaxAreaID);
	}

	[Test()]
	public void Commit() {
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Commit("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CalculateWithMultipleOfSameItem() {
	    TaxOrder order = new TaxOrder();
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CommitWithMultipleOfSameItem() {
	    TaxOrder order = new TaxOrder();
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Commit("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void CommitWithShippingTax() {
	    TaxOrder order = new TaxOrder();
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Commit("190 Stanton St", "New York", "", "NY", "10002", "USA", DateType.Now, order);

	    Assert.AreEqual(new DecimalType(8.87500), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(99.40), result.TotalTax);
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
	    

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
		
	    Assert.AreEqual(new CurrencyType(68.50), result.TotalTax);
	}

	[Test()]
	public void GetQuoteTaxTotalWithShippingTax() {
	    TaxOrder order = new TaxOrder();
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("32025 Monterey Ave", "Thousand Palms", "", "CA", "92276", "USA", DateType.Now, order);

	    Assert.AreEqual(new DecimalType(8.75), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(98.00), result.TotalTax);
	}

	[Test()]
	public void LookUpTaxArea() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");			
	    TaxAreaList taxAreaList = CertiTaxProvider.LookupTaxArea("1412 W. 1800 N.", "Bountiful", "Salt Lake", "UT", "84664", "USA", DateType.Now, BooleanType.FALSE);
	    Assert.AreEqual(IdType.Parse("840101644"), taxAreaList[0].TaxAreaID);
	}

	[Test()]
	public void LookUpTaxArea2() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");			
	    TaxAreaList taxAreaList = CertiTaxProvider.LookupTaxArea("6862 S Algonguian Ct", "Aurora", "", "CO", "80016", "USA", DateType.Now, BooleanType.FALSE);
	    Assert.AreEqual(4, taxAreaList[0].TaxJurisdictions.Count);
	}

	[Test()]
	public void CalculateWithTaxRate() {
	    TaxOrder order = new TaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 13.65M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.85), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.93), result.TotalTax);
	}

	[Test()]
	public void CalculateWithLocation() {
	    TaxOrder order = new TaxOrder();

	    StringType location = "test location";

	    TaxOrderLine taxOrderLine;

	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 13.65M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway", "Sandy", "", "UT", "84070", "USA", DateType.Now, order, location);

	    Assert.AreEqual(location, result.Location);
	}

	[Test()]
	public void CalculateTaxRateWithDiscount() {
	    TaxOrder order = new TaxOrder();
		
	    TaxOrderLine taxOrderLine;
		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.OrderLineId = 1;
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 13.65M;
	    taxOrderLine.DiscountAmount = 10.00M;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.85), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.25), result.TotalTax);
	}

	[Test()]
	public void CalculateCTWithShipping() {
	    TaxOrder order = new TaxOrder();
		
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("69 Chester","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.0), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(12.0), result.TotalTax);
	}

	[Test()]
	public void IsShippingTaxable() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");

	    // as of April 20, 2006 UT does not charge tax on shipping
	    TaxAreaData taxArea = CertiTaxProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.FALSE, taxArea.IsShippingTaxable);

	    // as of April 20, 2006 CT does charge tax on shipping
	    taxArea = CertiTaxProvider.GetTaxAreaForAddress("10 Robbie Road", "Tolland", "", "CT", "06084", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.TRUE, taxArea.IsShippingTaxable);
		
	    // as of August 22, 2006 CO does charge tax on shipping that is not optional
	    taxArea = CertiTaxProvider.GetTaxAreaForAddress("11640 Couer d Alene Drive", "Parker", "", "CO", "80138", "USA", BooleanType.FALSE);
	    Assert.AreEqual(BooleanType.TRUE, taxArea.IsShippingTaxable);
	    
	}
    	
	[Test()]
	public void CalculateWithZeroQuantityItem() {
	    TaxOrder order = new TaxOrder();
		
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("69 Chester","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.0), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.0), result.TotalTax);
	}

	[Test()]
	public void ShouldBeAbleToTaxOnValidDataIfStreetDoesNotValidate() {
	    TaxOrder order = new TaxOrder();
		
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

	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("this is bad data","Plainville", "", "CT", "06062", "USA", DateType.Now, order);
        
	    Assert.AreEqual(new DecimalType(6.0), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.0), result.TotalTax);
	}
	
	[Test()]
	public void GetTaxAreaForAddress2() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxAreaData taxArea = CertiTaxProvider.GetTaxAreaForAddress("6578 Lynx Cove", "Littleton", "", "CO", "80124", "USA", BooleanType.FALSE);

	    Assert.AreEqual(StringType.Parse("6578 Lynx Cv"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Littleton"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("CO"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("801249535"), taxArea.TaxAreaID);
	}

	[Test()]
	public void GetTaxJurisidictionsWithTaxResult() {
	    TaxOrder order = new TaxOrder();
	    order.TaxAreaId = IdType.Parse("84070");
		     		
	    TaxOrderLine taxOrderLine;
		     		
	    taxOrderLine = new TaxOrderLine();
	    taxOrderLine.Quantity = 1;
	    taxOrderLine.Price = 500;
	    taxOrderLine.ItemNumber = "001";
	    order.Lines.Add(taxOrderLine);
		     
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxResult result = CertiTaxProvider.Calculate("10150 South Centennial Parkway","Sandy", "", "UT", "84070", "USA", DateType.Now, order);
		     
	    Assert.AreEqual(2, result.TaxJurisdictions.Count);
		     	    
	    TaxJurisdiction jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.STATE, jurisidiction.JurisdictionType);
	    Assert.AreEqual("UTAH", jurisidiction.Description.ToString());
	    Assert.AreEqual(4.700m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(23.500m, jurisidiction.Amount.ToDecimal());
		     
	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE", jurisidiction.Description.ToString());
	    Assert.AreEqual(2.1500m, jurisidiction.Rate.ToDecimal());
	    Assert.AreEqual(10.750m, jurisidiction.Amount.ToDecimal());
	}

	[Test()]
	public void GetTaxJurisidictionsWithTaxAreaData() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");
	    TaxAreaData taxArea = CertiTaxProvider.GetTaxAreaForAddress("10150 S. Centennial Parkway", "Sandy", "Salt Lake", "UT", "84070", "USA", BooleanType.FALSE);

	    Assert.AreEqual(2, taxArea.TaxJurisdictions.Count);
		     	    
	    TaxJurisdiction jurisidiction = taxArea.TaxJurisdictions[TaxJurisdictionTypeEnum.STATE];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.STATE, jurisidiction.JurisdictionType);
	    Assert.AreEqual("UTAH", jurisidiction.Description.ToString());
	    Assert.AreEqual(47.000m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(4.7000m, jurisidiction.Rate.ToDecimal());
		     
	    jurisidiction = taxArea.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE", jurisidiction.Description.ToString());
	    Assert.AreEqual(21.500m, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(2.1500m, jurisidiction.Rate.ToDecimal());
	}


	public void GilbertShouldHaveJurisdictions() {
	    CertiTaxProvider provider = new CertiTaxProvider("United States");
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

	    CertiTaxProvider provider = new CertiTaxProvider("Canada");
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

	    CertiTaxProvider provider = new CertiTaxProvider("Canada");
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
	    CertiTaxProvider provider = new CertiTaxProvider("Canada");
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