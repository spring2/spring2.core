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
	    Assert.AreEqual(new DecimalType(6.250), taxRateInfo.TotalTaxRate);
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
        
	    Assert.AreEqual(new CurrencyType(66.00), result.TotalTax);
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
        
	    Assert.AreEqual(new CurrencyType(66.00), result.TotalTax);
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
        
	    Assert.AreEqual(new CurrencyType(66.00), result.TotalTax);
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
		
	    Assert.AreEqual(new CurrencyType(93.80), result.TotalTax);
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
		
	    Assert.AreEqual(new CurrencyType(66.00), result.TotalTax);
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
		
	    Assert.AreEqual(new CurrencyType(86.80), result.TotalTax);
	}

	[Test()]
	public void LookUpTaxArea() {
	    CertiTaxProvider CertiTaxProvider = new CertiTaxProvider("United States");			
	    TaxAreaList taxAreaList = CertiTaxProvider.LookupTaxArea("1412 W. 1800 N.", "Bountiful", "Salt Lake", "UT", "84664", "USA", DateType.Now, BooleanType.FALSE);
	    Assert.AreEqual(IdType.Parse("840101644"), taxAreaList[0].TaxAreaID);
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
        
	    Assert.AreEqual(new DecimalType(6.60), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.90), result.TotalTax);
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
        
	    Assert.AreEqual(new DecimalType(6.60), result.TotalTaxRate);
	    Assert.AreEqual(new CurrencyType(0.24), result.TotalTax);
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

	    Assert.AreEqual(StringType.Parse("10150 Centennial Pkwy"), taxArea.Street);
	    Assert.AreEqual(StringType.Parse("Sandy"), taxArea.City);
	    Assert.AreEqual(StringType.Parse("UT"), taxArea.Region);
	    Assert.AreEqual(IdType.Parse("840704103"), taxArea.TaxAreaID);
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
	    Assert.AreEqual(47.50, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(4.7500m, jurisidiction.Rate.ToDecimal());
		     
	    jurisidiction = result.TaxJurisdictions[TaxJurisdictionTypeEnum.COUNTY];
	    Assert.AreEqual(TaxJurisdictionTypeEnum.COUNTY, jurisidiction.JurisdictionType);
	    Assert.AreEqual("SALT LAKE", jurisidiction.Description.ToString());
	    Assert.AreEqual(18.50, jurisidiction.Amount.ToDecimal());
	    Assert.AreEqual(1.8500m, jurisidiction.Rate.ToDecimal());
	}
    }
}