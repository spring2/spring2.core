using System;

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// Summary description for DateLabelFormat.
    /// </summary>
    public class DateLabelFormat : LabelFormat {

	protected DateTime zeroDate = DateTime.MinValue;
	
	public DateLabelFormat() {
	}

	/// <summary>
	/// Create an instance setting which date represents 0
	/// </summary>
	/// <param name="zeroDate"></param>
	public DateLabelFormat(DateTime zeroDate) {
	    this.zeroDate = zeroDate;
	}

	public override String GetLabel(float value) {
	    DateTime date = zeroDate.AddDays((double)Decimal.Round((Decimal)value, 0));
	    return date.ToShortDateString();
	}

    }
}
