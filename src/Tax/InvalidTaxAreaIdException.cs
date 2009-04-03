using Spring2.Core.Message;

namespace Spring2.Core.Tax
{
	public class InvalidTaxAreaIdException : TaxException 
	{
		public InvalidTaxAreaIdException() : base("Invalid TaxAreaId Supplied") 
		{
		}

	}
}
