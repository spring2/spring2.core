using System;

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// Summary description for NumberLabelFormat.
    /// </summary>
    public class NumberLabelFormat : LabelFormat {
	
	private Int32 decimals = 2;

	public NumberLabelFormat() {
	}

	public NumberLabelFormat(Int32 decimals) {
	    this.decimals = decimals;
	}

	public override String GetLabel(float value) {
	    return Decimal.Round((decimal)value, this.decimals).ToString();
	}

    }
}
