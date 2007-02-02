using Spring2.Core.Types;
using Spring2.Core.Tax.Vertex;

namespace Spring2.Core.Tax {

    /// <summary>
    /// Common interface expected to be implmented by all tax provider that are avaialable through the Tax Manager.
    /// </summary>
    public interface ITaxProvider {
    	
    	/// <summary>
    	/// Get tax rate information for a specific tax area for a specific date using the tax area id from the tax provider.
    	/// </summary>
    	/// <remarks>
    	/// Tax area ids are not valid across tax providers.  i.e. a tax area id from one tax provider cannot be used with another tax provider.
    	/// </remarks>
    	/// <param name="id"></param>
    	/// <param name="dateType"></param>
    	/// <returns></returns>
    	TaxRateInfo GetTaxRateForArea(IdType id, DateType dateType);
    	
    	/// <summary>
    	/// Commits a sale in the tax provider
    	/// </summary>
    	/// <param name="order"></param>
    	/// <returns></returns>
	TaxResult Commit(TaxOrder order, StringType currencyCode);
    	
    	/// <summary>
    	/// Gets a tax estimate based on an order, for use durring the ordering process.  This is not persisted as a sale in the tax provider.
    	/// </summary>
    	/// <param name="order"></param>
    	/// <returns></returns>
	TaxResult GetQuoteTaxTotal(TaxOrder order, StringType currencyCode);
    	
    	/// <summary>
    	/// Get tax area information for a specific tax area for a specific date using the tax area id from the tax provider.
    	/// </summary>
    	/// <param name="street"></param>
    	/// <param name="city"></param>
    	/// <param name="county"></param>
    	/// <param name="state"></param>
    	/// <param name="postalCode"></param>
    	/// <param name="country"></param>
    	/// <param name="limits"></param>
    	/// <returns></returns>
	TaxAreaData GetTaxAreaForAddress(StringType street, StringType city, StringType county, StringType state, StringType postalCode, StringType country, BooleanType limits);
	
    	/// <summary>
    	/// Resolves possible tax areas for a given address
    	/// </summary>
    	/// <param name="street"></param>
    	/// <param name="city"></param>
    	/// <param name="county"></param>
    	/// <param name="state"></param>
    	/// <param name="postalCode"></param>
    	/// <param name="country"></param>
    	/// <param name="date"></param>
    	/// <param name="booleanType"></param>
    	/// <returns></returns>
	TaxAreaList LookupTaxArea(StringType street, StringType city, StringType county, StringType state, StringType postalCode, StringType country,  DateType date, BooleanType booleanType);

    	/// <summary>
    	/// Commits and records a sale in the tax provider.
    	/// </summary>
    	/// <param name="street"></param>
    	/// <param name="city"></param>
    	/// <param name="county"></param>
    	/// <param name="region"></param>
    	/// <param name="postalCode"></param>
    	/// <param name="country"></param>
    	/// <param name="date"></param>
    	/// <param name="order"></param>
    	/// <returns></returns>
	TaxResult Commit(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order);
    	
    	/// <summary>
    	/// Gets a tax estimate based on an order, for use durring the ordering process.  This is not persisted as a sale in the tax provider.
    	/// </summary>
    	/// <param name="street"></param>
    	/// <param name="city"></param>
    	/// <param name="county"></param>
    	/// <param name="region"></param>
    	/// <param name="postalCode"></param>
    	/// <param name="country"></param>
    	/// <param name="date"></param>
    	/// <param name="order"></param>
    	/// <returns></returns>
	TaxResult Calculate(StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order);

	/// <summary>
	/// Gets a tax estimate based on an order, for use durring the ordering process.  This is not persisted as a sale in the tax provider, but will update a persisted transaction.
	/// </summary>
	/// <param name="transactionId"></param>
	/// <param name="street"></param>
	/// <param name="city"></param>
	/// <param name="county"></param>
	/// <param name="region"></param>
	/// <param name="postalCode"></param>
	/// <param name="country"></param>
	/// <param name="date"></param>
	/// <param name="order"></param>
	/// <returns></returns>
	TaxResult Calculate(StringType transactionId, StringType street, StringType city, StringType county, StringType region, StringType postalCode, StringType country, DateType date, TaxOrder order);
    	
    	
    }
}