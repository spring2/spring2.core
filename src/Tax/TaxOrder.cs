using System;
using Spring2.Core.Types;

namespace Spring2.Dss.Tax {
	/// <summary>
	/// Summary description for VertexOrderData.
	/// </summary>
	public class TaxOrder {
		public static readonly TaxOrder DEFAULT = new TaxOrder();


		private IdType orderId = IdType.DEFAULT;
		private DecimalType salesTaxRate = DecimalType.DEFAULT;
		private TaxOrderLineList taxOrderLineList = new TaxOrderLineList();
		private DateTimeType completeDate = DateTimeType.DEFAULT;
		private CurrencyType shipTotal = CurrencyType.DEFAULT;
		private CurrencyType salesTaxTotal = CurrencyType.DEFAULT;
		private DecimalType discountRate = DecimalType.DEFAULT;
		private IdType taxAreaId = IdType.DEFAULT;
		private StringType taxArea = StringType.DEFAULT;
		private StringType orderType = StringType.DEFAULT;
		private StringType orderStatus = StringType.DEFAULT;
		private IdType customerId = IdType.DEFAULT;
		private BooleanType isTaxExempt = BooleanType.DEFAULT;

		public IdType OrderId {
			get { return this.orderId; }
			set { this.orderId = value; }
		}

		public IdType CustomerId {
			get { return this.customerId; }
			set { this.customerId = value; }
		}

		public BooleanType IsTaxExempt {
			get { return this.isTaxExempt; }
			set { this.isTaxExempt = value; }
		}

		public IdType TaxAreaId {
			get { return this.taxAreaId; }
			set { this.taxAreaId = value; }
		}

		public StringType TaxArea {
			get { return this.taxArea; }
			set { this.taxArea = value; }
		}

		public StringType OrderType {
			get { return this.orderType; }
			set { this.orderType = value; }
		}

		public StringType OrderStatus {
			get { return this.orderStatus; }
			set { this.orderStatus = value; }
		}

		public DecimalType SalesTaxRate {
			get { return this.salesTaxRate; }
			set { this.salesTaxRate = value; }
		}

		public CurrencyType SalesTaxTotal {
			get { return this.salesTaxTotal; }
			set { this.salesTaxTotal = value; }
		}

		public TaxOrderLineList Lines {
			get { return this.taxOrderLineList; }
			set { this.taxOrderLineList = value; }
		}

		public DateTimeType CompleteDate {
			get { return this.completeDate; }
			set { this.completeDate = value; }
		}

		public CurrencyType ShipTotal {
			get { return this.shipTotal; }
			set { this.shipTotal = value; }
		}

		public DecimalType DiscountRate {
			get { return this.discountRate; }
			set { this.discountRate = value; }
		}

		public Boolean IsDefault {
			get { return Object.ReferenceEquals(DEFAULT, this); }
		}

		public CurrencyType MerchandiseTotal {
			get {
				CurrencyType total = CurrencyType.ZERO;

				foreach (TaxOrderLine line in Lines) {
					total += line.ExtendedPrice;
				}

				return total;
			}
		}
	}
}