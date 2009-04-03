using System;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GecodeData.
    /// </summary>
    public class GeocodeData {

	public GeocodeData() {
	}

	public GeocodeData(String delimitedString) {
	    String[] parts = delimitedString.Split('|');
	    if (parts[0].Length>0) {
		matchCount = IntegerType.Parse(parts[0]);
	    } else {
		matchCount = 0;
	    }
	    matchType = IntegerType.Parse(parts[1]);
	    matchDB = parts[2];
	    stdAddress = parts[3];
	    stdCity = parts[4];
	    stdState = parts[5];
	    stdZipCode = parts[6];
	    stdZipCodePlus4 = parts[7];
	    stdDPBC = parts[8];
	    stdCarrier = parts[9];
	    matchAddress = parts[10];
	    matchCity = parts[11];
	    matchState = parts[12];
	    matchZipCode = parts[13];
	    if (parts[14].Length>0) {
		matchLatitude = DecimalType.Parse(parts[14]);
	    } else {
		matchLatitude = 0;
	    }
	    if (parts[15].Length>0) {
		matchLongitude = DecimalType.Parse(parts[15]);
	    } else {
		matchLongitude = 0;
	    }
	    output = parts[16];
	}

	public String ToDelimitedString() {
	    StringBuilder sb = new StringBuilder();

	    sb.Append(matchCount.Display());
	    sb.Append('|');
	    sb.Append(matchType.Display());
	    sb.Append('|');
	    sb.Append(matchDB.Display());
	    sb.Append('|');
	    sb.Append(stdAddress.Display());
	    sb.Append('|');
	    sb.Append(stdCity.Display());
	    sb.Append('|');
	    sb.Append(stdState.Display());
	    sb.Append('|');
	    sb.Append(stdZipCode.Display());
	    sb.Append('|');
	    sb.Append(StdZipCodePlus4.Display());
	    sb.Append('|');
	    sb.Append(stdDPBC.Display());
	    sb.Append('|');
	    sb.Append(stdCarrier.Display());
	    sb.Append('|');
	    sb.Append(matchAddress.Display());
	    sb.Append('|');
	    sb.Append(matchCity.Display());
	    sb.Append('|');
	    sb.Append(matchState.Display());
	    sb.Append('|');
	    sb.Append(matchZipCode.Display());
	    sb.Append('|');
	    sb.Append(matchLatitude.Display());
	    sb.Append('|');
	    sb.Append(matchLongitude.Display());
	    sb.Append('|');
	    sb.Append(output.Display());

	    return sb.ToString();
	}

	private IntegerType matchCount = IntegerType.DEFAULT;
	private IntegerType matchType = IntegerType.DEFAULT;
	private StringType matchDB = StringType.DEFAULT;
	private StringType stdAddress = StringType.DEFAULT;
	private StringType stdCity = StringType.DEFAULT;
	private StringType stdState = StringType.DEFAULT;
	private StringType stdZipCode = StringType.DEFAULT;
	private StringType stdZipCodePlus4 = StringType.DEFAULT;
	private StringType stdDPBC = StringType.DEFAULT;
	private StringType stdCarrier = StringType.DEFAULT;
	private StringType matchAddress = StringType.DEFAULT;
	private StringType matchCity = StringType.DEFAULT;
	private StringType matchState = StringType.DEFAULT;
	private StringType matchZipCode = StringType.DEFAULT;
	private DecimalType matchLatitude = DecimalType.DEFAULT;
	private DecimalType matchLongitude = DecimalType.DEFAULT;
	
	private StringType output = StringType.EMPTY;

	public StringType OutPut {
	    set {
		output = value;
	    }
	    get {return output;}
	}

	public IntegerType MatchCount {
	    get {
		return matchCount;
	   }
	    set {
		matchCount = value;
	    }
	}

	public IntegerType MatchType {
	    get {
		return matchType;
	    }
	    set {
		matchType = value;
	    }
	}

	public StringType MatchDB {
	    get { 	
		return matchDB;
	    }
	    set {
		matchDB = value;
	    }
	}

	public StringType StdAddress {
	    get { 
		return stdAddress;
	    }
	    set {
		stdAddress = value;
	    }
	}

	public StringType StdCity {
	    get { 
		return stdCity;
	    }
	    set {
		stdCity = value;
	    }
	}

	public StringType StdState {
	    get {
		return stdState;
	    }
	    set {
		stdState = value;
	    }
	}

	public StringType StdZipCode {
	    get { 
		return stdZipCode;
	    }
	    set {
		stdZipCode = value;
	    }
	}

	public StringType StdZipCodePlus4 {
	    get { 
		return stdZipCodePlus4;
	    }
	    set {
		stdZipCodePlus4 = value;
	    }
	}

	public StringType StdDPBC {
	    get { 
		return stdDPBC;
	    }
	    set {
		stdDPBC = value;
	    }
	}

	public StringType StdCarrier {
	    get { 
		return stdCarrier;
	    }
	    set {
		stdCarrier = value;
	    }
	}

	public StringType MatchAddress {
	    get { 
		return matchAddress;
	    }
	    set {
		matchAddress = value;
	    }
	}

	public StringType MatchCity {	    
	    get {
		return matchCity;
	    }
	    set {
		matchCity = value;
	    }
	}

	public StringType MatchState {
	    get { 
		return matchState;
	    }
	    set {
		matchState = value;
	    }
	}

	public StringType MatchZipCode {
	    get {
		return matchZipCode;
	    }
	    set {
		matchZipCode = value;
	    }
	}

	public DecimalType MatchLatitude {
	    get {
		return matchLatitude;
	    }
	    set {
		matchLatitude = value;
	    }
	}

	public DecimalType MatchLongitude {
	    get {
		return matchLongitude;
	    }
	    set {
		matchLongitude = value;
	    }
	}
    }
}
