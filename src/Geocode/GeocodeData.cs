using System;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GeocodeData.
    /// </summary>
    public class GeocodeData {

	private String output = String.Empty;

	public string OutPut {
	    set {
		output = value;
	    }
	    get {return output;}
	}

	public IntegerType MatchCount {
	    get {
		return new IntegerType(Convert.ToInt32(output.Substring(15,1)));
	    }
	}

	public IntegerType MatchType {
	    get {
		try{
		    if(output.Length<630){
			//Bad output, string should be 700 chars long at least
			// changed this to 630 in light of a 636 length string returned for an exact match on 75 East 55th Street
			//	here is sample valid output for this match:  "Match Count  < 1>\r\nMatch Type   <1>\r\nMatch DB     <P>\r\n\r\nStd. Addr    <>\r\nStd. City    <>\r\nStd. State   <>\r\nStd. Zip     <>\r\nStd. P4      <>\r\nStd. DPBC    <>\r\nStd. Carrier <>\r\n\r\nMatch Addr   <75 E 55TH ST                                                >\r\nMatch City   <NEW YORK                    >\r\nMatch State  <NY>\r\nMatch Zip    <10022>\r\nMatch Lat    <40.760635>\r\nMatch Lon    <-073.972809>\r\n\r\nPop Dens     < >\r\nFIPS County  <NY061>\r\n\r\nCensus Block <NY0610102001>\r\nCensus MCD   <        >\r\nCensus Place <      >\r\nCensus Tract <NY061010200>\r\nCensus UAC   <    >\r\n\r\nFIPS MCD     <NY44919>\r\nFIPS Place   <NY51000>\r\nMSA          <5600>\r\n"
			//Return a value for bad address
			return new IntegerType(13);
		    }else{
			return new IntegerType(Convert.ToInt32(output.Substring(33,1)));
		    }
		}catch(Exception){
		    return new IntegerType(13);
		}
	    }
	}

	public string MatchDB {
	    get { return output.Substring(51,1);}
	}

	public string StdAddress {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. Addr");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{
		    return output.Substring(71,60);
		}
	    }
	}

	public string StdCity {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. City");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{return output.Substring(148,28);}
	    }
	}

	public string StdState {
	    get {
		if(output.Length<700){
		    int index = output.IndexOf("Std. State");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		} else {
		    return output.Substring(193,2);
		}
	    }
	}

	public string StdZipCode {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. Zip");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		} else { 
		    return output.Substring(212,5);
		}
	    }
	}

	public string StdZipCodePlus4 {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. P4");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{
		    return output.Substring(234,4);
		}
	    }
	}

	public string StdDPBC {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. DPBC");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{
		    return output.Substring(255,2);
		}
	    }
	}

	public string StdCarrier {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Std. Carrier");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{
		    return output.Substring(274,4);
		}
	    }
	}

	public string MatchAddress {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Match Addr");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{return output.Substring(297,60);}
	    }
	}

	public string MatchCity {
	    
	    get {
		if(output.Length<700){
		    int index = output.IndexOf("Match City");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{
		    return output.Substring(374,28);
		}
	    }
	}

	public string MatchState {
	    get { 
		if(output.Length<700){
		    int index = output.IndexOf("Match State");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		}else{		
		    return output.Substring(419,2);
		}
	    }
	}

	public string MatchZipCode {
	    get {
		if(output.Length<700) {
		    int index = output.IndexOf("Match Zip");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    return output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		} else {
		    return output.Substring(438,5);
		}
	    }
	}

	public DecimalType MatchLatitude {
	    get {
		String latString;
		if(output.Length<700){
		    int index = output.IndexOf("Match Lat");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    latString = output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		} else {
		    latString = output.Substring(460,9);
		}
		try{
		    return new DecimalType(Convert.ToDecimal(latString));
		} catch (Exception) {
		    return new DecimalType(0);
		}
	    }
	}

	public DecimalType MatchLongitude {
	    get {
		String latString;
		if(output.Length<700){
		    int index = output.IndexOf("Match Lon");
		    index = output.IndexOf("<",index);
		    int stopIndex = output.IndexOf(">",index);
		    latString = output.Substring(index+1,(stopIndex-index)-1).TrimEnd(' ');
		} else {
		    latString = output.Substring(486,11);
		}
		try{
		    return new DecimalType(Convert.ToDecimal(latString));
		} catch (Exception) {
		    return new DecimalType(0);
		}
	    }
	}		

    }
}
