using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.WebService {
    /// <summary>
    /// Summary description for TeleAtlasGeocodeWebServiceData.
    /// </summary>
    public class TeleAtlasGeocodeWebServiceData {

	#region Member Variables

	private System.Collections.Hashtable output = new System.Collections.Hashtable();
	private StringType dbOutput;
	private StringType matAddress;
	private StringType matCity;
	private StringType matState;
	private StringType matZip;
	private DecimalType matLatitude;
	private DecimalType matLongitude;
	private StringType stdAddress;
	private StringType stdCity;
	private StringType stdState;
	private StringType stdZip;
	private StringType stdPlus4;
	private IntegerType matchType;

	#endregion

	#region Properties

	public System.Collections.Hashtable OutPut {
	    set {
		output = value;
	    }
	    get {return output;}
	}

	public StringType DBOutPut {
	    set {
		dbOutput = value;
		if (dbOutput.StartsWith("Match Count")) {
		    ParseDBOutput();  // convert
		}
	    }
	    get {return dbOutput;}
	}

	public IntegerType MatchType {
	    get {return matchType;}
	}

	// Should use StringType
	public string StdAddress {
	    get {return stdAddress.Display();}
	}

	public string StdCity {
	    get {return stdCity.Display();}
	}

	public string StdState {
	    get {return stdState.Display();}
	}

	public string StdZipCode {
	    get {return stdZip.Display();}
	}

	public string StdZipCodePlus4 {
	    get {return stdPlus4.Display();}
	}

	public string MatchAddress {
	    get {return matAddress;}
	}

	public string MatchCity {
	    
	    get {return matCity;}
	}

	public string MatchState {
	    get {return matState.Display();}
	}

	public string MatchZipCode {
	    get {return matZip.Display();}
	}

	public DecimalType MatchLatitude {
	    get {return matLatitude;}
	}

	public DecimalType MatchLongitude {
	    get {return matLongitude;}
	}		

	#endregion
	
	#region Returned Results Handler Methods

	public void ParseData() {
	    ParseOutput();
	    CreateDBOutput();
	}

	/// <summary>
	/// Parses the hash and extracts the data
	/// </summary>
	private void ParseOutput() {
	    stdAddress = ParseStringType("STD_ADDR");
	    stdCity = ParseStringType("STD_CITY");
	    stdState = ParseStringType("STD_ST");
	    stdZip = ParseStringType("STD_ZIP");
	    stdPlus4 = ParseStringType("STD_P4");

	    matAddress = ParseStringType("MAT_ADDR");
	    matCity = ParseStringType("MAT_CITY");
	    if (output.ContainsKey("MAT_ST")) {
		matState = ParseStringType("MAT_ST");
	    } else  {
		matState = ParseStringType("MAT_PROV");
	    }

	    if (output.ContainsKey("MAT_ZIP")) {
		matZip = ParseStringType("MAT_ZIP");
	    } else {
		matZip = ParseStringType("MAT_FSA");
	    }

	    matLatitude = DecimalType.Parse(output["MAT_LAT"].ToString());
	    matLongitude = DecimalType.Parse(output["MAT_LON"].ToString());

	    matchType = IntegerType.Parse(output["MAT_TYPE"].ToString());
	}

	private StringType ParseStringType(String key) {
	    if (output.ContainsKey(key)) {
		return StringType.Parse(output[key].ToString());
	    } else {
		return StringType.EMPTY;
	    }
	}

	/// <summary>
	/// converts the name/value result object returned by the webservice to a string format that is saved
	/// to the database in the Result column of AddressCache
	/// </summary>
	private void CreateDBOutput() {
	    System.Text.StringBuilder sb = new System.Text.StringBuilder();

	    sb.Append("STD_ADDR=" + stdAddress.Display() + ",");
	    sb.Append("STD_CITY=" + stdCity.Display() + ",");
	    sb.Append("STD_ST=" + stdState.Display() + ",");
	    sb.Append("STD_ZIP=" + stdZip.Display() + ",");
	    sb.Append("STD_P4=" + stdPlus4.Display() + ",");
	    sb.Append("MAT_ADDR=" + matAddress.Display() + ",");
	    sb.Append("MAT_CITY=" + matCity.Display() + ",");
	    sb.Append("MAT_ST=" + matState.Display() + ",");
	    sb.Append("MAT_ZIP=" + matZip.Display() + ",");
	    sb.Append("MAT_LAT=" + matLatitude.Display() + ",");
	    sb.Append("MAT_LON=" + matLongitude.Display() + ",");
	    sb.Append("MAT_TYPE=" + matchType.Display());

	    this.dbOutput = sb.ToString();
	}

	#endregion

	#region Conversion Methods

	private void ParseDBOutput() {
	    if (dbOutput.StartsWith("Match Count")) {
		DoConversion();
	    }

	    StringType[] values = dbOutput.Split(new char[] {','});

	    foreach (string val in values) {
		string name = val.Substring(0,val.IndexOf("="));
		string data = val.Substring(val.IndexOf("=") + 1);

		switch (name) {
		    case "STD_ADDR":
			stdAddress = data;
			break;
		    case "STD_CITY":
			stdCity = data;
			break;
		    case "STD_ST":
			stdState = data;
			break;
		    case "STD_ZIP":
			stdZip = data;
			break;
		    case "STD_P4":
			stdPlus4 = data;
			break;
		    case "MAT_ADDR":
			matAddress = data;
			break;
		    case "MAT_CITY":
			matCity = data;
			break;
		    case "MAT_PROV":
		    case "MAT_ST":
			matState = data;
			break;
		    case "MAT_FSA":
		    case "MAT_ZIP":
			matZip = data;
			break;
		    case "MAT_LAT":
			matLatitude = new DecimalType(decimal.Parse(data));
			break;
		    case "MAT_LON":
			matLongitude = new DecimalType(decimal.Parse(data));
			break;
		    case "MAT_TYPE":
			matchType = new IntegerType(int.Parse(data));
			break;
		}
	    }
	}

	/// <summary>
	/// Converts the legacy Result string returned by the Tele Atlas application to a new format
	/// </summary>
	private void DoConversion() {
	    int index;
	    int stopIndex;
	    System.Text.StringBuilder sb = new System.Text.StringBuilder();
	    string output = dbOutput;

	    //StdAddress 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Std. Addr");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("STD_ADDR=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else{
		    sb.Append("STD_ADDR=" + output.Substring(71, 60).Trim());
		}
	    } catch (Exception) {
		sb.Append("STD_ADDR=" + string.Empty);
	    }
	    sb.Append(",");

	    //StdCity
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Std. City");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">",index);
		    sb.Append("STD_CITY=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else {
		    sb.Append("STD_CITY=" + output.Substring(148, 28).Trim());
		}
	    } catch (Exception) {
		sb.Append("STD_CITY=" + string.Empty);
	    }
	    sb.Append(",");

	    //StdState
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Std. State");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("STD_ST=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		} else {
		    sb.Append("STD_ST=" + output.Substring(193, 2).Trim());
		}
	    } catch (Exception) {
		sb.Append("STD_ST=" + string.Empty);
	    }
	    sb.Append(",");

	    //StdZipCode
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Std. Zip");
		    index = output.IndexOf("<",index);
		    stopIndex = output.IndexOf(">",index);
		    sb.Append("STD_ZIP=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		} else { 
		    sb.Append("STD_ZIP=" + output.Substring(212, 5).Trim());
		}
	    } catch (Exception) {
		sb.Append("STD_ZIP=" + string.Empty);
	    }
	    sb.Append(",");

	    //StdZipCodePlus4 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Std. P4");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("STD_P4=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else{
		    sb.Append("STD_P4=" + output.Substring(234, 4).Trim());
		}
	    } catch (Exception) {
		sb.Append("STD_P4=" + string.Empty);
	    }
	    sb.Append(",");

	    //MatchAddress 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Match Addr");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("MAT_ADDR=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else {
		    sb.Append("MAT_ADDR=" + output.Substring(297, 60).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_ADDR=" + string.Empty);
	    }
	    sb.Append(",");

	    //MatchCity 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Match City");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("MAT_CITY=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else{
		    sb.Append("MAT_CITY=" + output.Substring(374, 28).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_CITY=" + string.Empty);
	    }
	    sb.Append(",");

	    //MatchState 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Match State");
		    index = output.IndexOf("<",index);
		    stopIndex = output.IndexOf(">",index);
		    sb.Append("MAT_ST=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		}else{		
		    sb.Append("MAT_ST=" + output.Substring(419, 2).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_ST=" + string.Empty);
	    }
	    sb.Append(",");

	    //MatchZipCode 
	    try {
		if(output.Length < 700) {
		    index = output.IndexOf("Match Zip");
		    index = output.IndexOf("<",index);
		    stopIndex = output.IndexOf(">",index);
		    sb.Append("MAT_ZIP=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		} else {
		    sb.Append("MAT_ZIP=" + output.Substring(438, 5).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_ZIP=" + string.Empty);
	    }
	    sb.Append(",");

	    //MatchLatitude 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Match Lat");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("MAT_LAT=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		} else {
		    sb.Append("MAT_LAT=" + output.Substring(460,9).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_LAT=0");
	    }
	    sb.Append(",");

	    //MatchLongitude 
	    try {
		if(output.Length < 700){
		    index = output.IndexOf("Match Lon");
		    index = output.IndexOf("<", index);
		    stopIndex = output.IndexOf(">", index);
		    sb.Append("MAT_LON=" + output.Substring(index + 1,(stopIndex - index) - 1).Trim());
		} else {
		    sb.Append("MAT_LON=" + output.Substring(486,11).Trim());
		}
	    } catch (Exception) {
		sb.Append("MAT_LON=0");
	    }
	    sb.Append(",");

	    //MatchType
	    try{
		if(output.Length < 630){
		    //Bad output, string should be 700 chars long at least
		    // changed this to 630 in light of a 636 length string returned for an exact match on 75 East 55th Street
		    //	here is sample valid output for this match:  "Match Count  < 1>\r\nMatch Type   <1>\r\nMatch DB     <P>\r\n\r\nStd. Addr    <>\r\nStd. City    <>\r\nStd. State   <>\r\nStd. Zip     <>\r\nStd. P4      <>\r\nStd. DPBC    <>\r\nStd. Carrier <>\r\n\r\nMatch Addr   <75 E 55TH ST                                                >\r\nMatch City   <NEW YORK                    >\r\nMatch State  <NY>\r\nMatch Zip    <10022>\r\nMatch Lat    <40.760635>\r\nMatch Lon    <-073.972809>\r\n\r\nPop Dens     < >\r\nFIPS County  <NY061>\r\n\r\nCensus Block <NY0610102001>\r\nCensus MCD   <        >\r\nCensus Place <      >\r\nCensus Tract <NY061010200>\r\nCensus UAC   <    >\r\n\r\nFIPS MCD     <NY44919>\r\nFIPS Place   <NY51000>\r\nMSA          <5600>\r\n"
		    //Return a value for bad address
		    sb.Append("MAT_TYPE=12");
		}else{
		    sb.Append("MAT_TYPE=" + output.Substring(33,1).Trim());
		}
	    }catch(Exception){
		sb.Append("MAT_TYPE=12");
	    }

	    this.dbOutput = sb.ToString();
	}

	#endregion
    }
}