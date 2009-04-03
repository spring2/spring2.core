using System;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.Tax {
    /// <summary>
    /// Html rendering utilities for Vertex tax area results
    /// </summary>
    public class TaxHtmlTool {

	public String RenderSelect(String selectName, String selectedValue, TaxAreaList list, bool enabled, String cssClass) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<select ");
	    sb.Append("class=\"" + cssClass + "\"" + " name=\"" + selectName + "\"");
	    sb.Append(IsDisabled(enabled));
	    sb.Append(">");
	    foreach (TaxAreaData key in list) {
		TaxRateInfo data2 = new TaxRateInfo();
		data2 = TaxManager.GetProvider(key.Country).GetTaxRateForArea(key.TaxAreaID, new DateType(DateTime.Now));

		sb.Append("<option ");
		if (selectedValue == key.City.ToString()) {
		    sb.Append("selected ");
		}
		sb.Append("value=\"" + key.TaxAreaID.ToInt32() + "|" + IsDefault(key.Region.ToString(), false, true) + IsDefault(key.County.ToString(), false, false) + IsDefault(key.City.ToString(), false, false) + IsDefault((data2.TotalTaxRate.ToString("N2") + "%"), true, false) + "\">" + IsDefault(key.Region.ToString(), true, true) + IsDefault(key.County.ToString(), true, false) + IsDefault(key.City.ToString(), true, false) + IsDefault((data2.TotalTaxRate.ToString("N3") + "%"), true, false));
		sb.Append("</option>");
	    }
	    sb.Append("</select>");
	    return sb.ToString();
	}

	public String IsDisabled(bool disabled) {
	    if (!disabled) {
		return "";
	    } else {
		return " disabled ";
	    }
	}

	public String IsDefault(string area, bool isForDisplay, bool isFirst) {
	    if (area.Equals("DEFAULT")) {
		return "";
	    } else {
		if (isForDisplay) {
		    if (!isFirst) {
			area = ", " + area;
		    }
		    return area;
		} else {
		    if (!isFirst) {
			area = " " + area;
		    }
		    return area;
		}
	    }
	}
    }
}