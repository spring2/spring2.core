using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.PostageService {
    public class Fees {
	public decimal CertificateOfMailing { set; get; }

	public decimal CertifiedMail { set; get; }

	public decimal CollectOnDelivery { set; get; }

	public decimal DeliveryConfirmation { set; get; }

	public decimal ElectronicReturnReceipt { set; get; }

	public decimal InsuredMail { set; get; }

	public decimal RegisteredMail { set; get; }

	public decimal RestrictedDelivery { set; get; }

	public decimal ReturnReceipt { set; get; }

	public decimal ReturnReceiptForMerchandise { set; get; }

	public decimal SignatureConfirmation { set; get; }

	public decimal SpecialHandling { set; get; }

	public decimal CriticalMail { set; get; }

	public decimal MerchandiseReturn { set; get; }

	public decimal OpenAndDistribute { set; get; }

	public decimal AdultSignature { set; get; }

	public decimal AdultSignatureRestrictedDelivery { set; get; }

	public decimal TotalAmount { set; get; }
    }
}
