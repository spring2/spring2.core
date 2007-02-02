using System;
using Spring2.Core.Types;

namespace Spring2.Dss.AddressValidation {
    public class AddressData {
	private StringType street1 = StringType.DEFAULT;
	private StringType street2 = StringType.DEFAULT;
	private StringType street3 = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType state = StringType.DEFAULT;
	private StringType postalCode = StringType.DEFAULT;
	private StringType country = StringType.DEFAULT;
	private BooleanType blockRange = BooleanType.DEFAULT;

	public StringType Street1 {
	    get { return street1; }
	    set { street1 = value; }
	}

	public StringType Street2 {
	    get { return street2; }
	    set { street2 = value; }
	}

	public StringType Street3 {
	    get { return street3; }
	    set { street3 = value; }
	}

	public StringType City {
	    get { return city; }
	    set { city = value; }
	}

	public StringType State {
	    get { return state; }
	    set { state = value; }
	}

	public StringType PostalCode {
	    get { return postalCode; }
	    set { postalCode = value; }
	}

	public StringType Country {
	    get { return country; }
	    set { country = value; }
	}

	public BooleanType BlockRange {
	    get { return blockRange; }
	    set { blockRange = value; }
	}

	public StringType FormattedAddress {
	    get {
		System.Text.StringBuilder buffer = new System.Text.StringBuilder();

		if (!this.Street1.IsEmpty) {
		    buffer.Append(this.Street1.ToString() + Environment.NewLine);
		}

		if (!this.Street2.IsEmpty) {
		    buffer.Append(this.Street2.ToString() + Environment.NewLine);
		}

		if (!this.Street3.IsEmpty) {
		    buffer.Append(this.Street3.ToString() + Environment.NewLine);
		}

		if (!this.City.IsEmpty) {
		    buffer.Append(this.City.ToString());
		    if (this.State.IsValid) {
			buffer.AppendFormat(" {0}", this.State.ToString());
		    }
		    if (this.PostalCode.IsValid && !this.PostalCode.IsEmpty) {
			buffer.AppendFormat("  {0}", this.PostalCode.ToString());
		    }
		} else {
		    if (!this.State.IsEmpty) {
			buffer.AppendFormat("{0}", this.State.ToString());
			if (this.PostalCode.IsValid && !this.PostalCode.IsEmpty) {
			    buffer.AppendFormat("  {0}", this.PostalCode.ToString());
			}
		    } else {
			if (this.PostalCode.IsValid && !this.PostalCode.IsEmpty) {
			    buffer.AppendFormat("{0}", this.PostalCode.ToString());
			}
		    }
		}

		return buffer.ToString();
	    }
	}
    }
}
