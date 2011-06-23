using System; 
using System.IO;
using System.Text; 
using System.Diagnostics; 
using System.Threading; 
using Spring2.Core.Configuration;
using Spring2.Core.Types;
using Spring2.Core.Geocode.DataObject;
using Spring2.Core.Geocode.Dao;
using Spring2.Core.Geocode.BusinessLogic;
using Spring2.Core.Geocode.Types;

namespace Spring2.Core.Geocode {
    /// <summary>
    /// Summary description for GecodeWrapper.
    /// </summary>
    public class GeocodeWrapper {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	protected StringType street;
	protected StringType city;
	protected StringType state;
	protected StringType zipCode;
	protected StringType password;
	protected StringType userName;
	protected StringType serviceName;

	// Variables used to store property values (prefix: underscore "_")
	private string _exeDirectory = "C:\\data\\work\\goldcanyon\\dss\\lib";
	private int processTimeOutMilliseconds = 20000; // 20 seconds
	private int _exitcode = 0;
	string inputText = string.Empty;

	// Variables used for monitoring external process and threads
	private Process _processObject;
	private string _outputString;
	private string _errorString;

	private string _inputfile = String.Empty;
	private string _outputfile = String.Empty;

	
	/// <summary>
	/// name of the home directory (where keyrings AND gpg.exe are located)
	/// </summary>
	public string exeDirectory {
	    set { _exeDirectory = value; }
	}

	public GeocodeData GetCityAndStateOfZipCode(StringType postalCode){
	    return DoGeocode(StringType.EMPTY,StringType.EMPTY, StringType.EMPTY, postalCode,StringType.DEFAULT, CountryCodeEnum.UNSET);
	}

	public Int32 GetGeocodeCount() {
	    this.password = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodePassword"]);
	    this.userName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeUserName"]);

	    //Account: EZL0017833; Current total: 18; Limit: 100; Expires: 08/28/07 12:56:00
	    string result = ExecuteGeocodeCountCommand();
	    int limit = 0;
	    int used = 0;
	    int start = 0;
	    int end = 0;

	    string startText = "current total: ";
	    start = result.ToLower().IndexOf(startText);
	    end = result.IndexOf(";", start);
	    used = int.Parse(result.Substring(start + startText.Length, end - (start + startText.Length)));

	    start = 0;
	    end = 0;
	    startText = "limit: ";
	    start = result.ToLower().IndexOf(startText);
	    end = result.IndexOf(";", start);
	    limit = int.Parse(result.Substring(start + startText.Length, end - (start + startText.Length)));

	    return limit - used;
	}

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode){
	    TeleAtlasGeocodeData teleAtlasgeocodeData = new TeleAtlasGeocodeData();
	    GeocodeData geocodeData = new GeocodeData();
	    AddressCacheList list = null;

	    if(postalCode.ToString().Length > 0){
		list = AddressCacheDAO.DAO.FindAddressByStreetAndPostalCode(street,postalCode);		
	    }else {
		list = AddressCacheDAO.DAO.FindAddressByStreetAndCityAndState(street,city,state);
	    }

	    foreach (IAddressCache cache in list) {
		if((cache.Status.Equals(GeocodeStatusEnum.VALID))){
		    teleAtlasgeocodeData.OutPut = cache.Result.ToString();

		    geocodeData.OutPut = StringType.Parse(teleAtlasgeocodeData.OutPut);
		    geocodeData.MatchCount = teleAtlasgeocodeData.MatchCount;
		    geocodeData.MatchType = teleAtlasgeocodeData.MatchType;
		    geocodeData.MatchDB = StringType.Parse( teleAtlasgeocodeData.MatchDB );
		    geocodeData.StdAddress = StringType.Parse( teleAtlasgeocodeData.StdAddress );
		    geocodeData.StdCity = StringType.Parse( teleAtlasgeocodeData.StdCity );
		    geocodeData.StdState = StringType.Parse( teleAtlasgeocodeData.StdState );
		    geocodeData.StdZipCode = StringType.Parse( teleAtlasgeocodeData.StdZipCode );
		    geocodeData.StdZipCodePlus4 = StringType.Parse( teleAtlasgeocodeData.StdZipCodePlus4 );
		    geocodeData.StdDPBC = StringType.Parse( teleAtlasgeocodeData.StdDPBC );
		    geocodeData.StdCarrier = StringType.Parse( teleAtlasgeocodeData.StdCarrier );
		    geocodeData.MatchAddress = StringType.Parse( teleAtlasgeocodeData.MatchAddress );
		    geocodeData.MatchCity = StringType.Parse( teleAtlasgeocodeData.MatchAddress );
		    geocodeData.MatchState = StringType.Parse( teleAtlasgeocodeData.MatchState );
		    geocodeData.MatchZipCode = StringType.Parse( teleAtlasgeocodeData.MatchZipCode );
		    geocodeData.MatchLatitude = teleAtlasgeocodeData.MatchLatitude;
		    geocodeData.MatchLongitude = teleAtlasgeocodeData.MatchLongitude;

		    return geocodeData;
		}
	    }

	    this.street = street;
	    this.city = city;
	    this.state = state;
	    this.zipCode = postalCode;
	    this.password = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodePassword"]);
	    this.userName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeUserName"]);
	    DirectoryInfo directory = new DirectoryInfo(ConfigurationProvider.Instance.Settings["GeocodePath"]);

	    if (path.Length > 0 && System.IO.File.Exists((path.ToString() + "\\rie.exe"))) {
		exeDirectory = path.ToString();
	    } else if (System.IO.File.Exists((directory.FullName + "\\rie.exe"))) {
		exeDirectory = directory.FullName;
	    } else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "rie.exe"))) {
		exeDirectory = Directory.GetCurrentDirectory();
	    } else {
		throw new Exception(String.Format("{0} could not be found.", directory.FullName + "\\rie.exe"));
	    }

	    teleAtlasgeocodeData = ExecuteCommand();
	    if (teleAtlasgeocodeData.OutPut.Trim() == "bad result <9>") {
		throw new Exception("Out of geocodes.");
	    }

	    /*
		Match Types:
		0 = Non-match
		1 = Street Segment Exact Match
		2 = Street Segment Near Match (+/- 100 housenumbers)
		3 = ZIP+2 Centroid
		4 = 5-Digit ZIP Centroid
		5 = 3-Digit ZIP (SCF) Centroid
		6 = Ambiguous Segment Match (aggregate of more than 1 segment which
			equally match the address)
	    */

	    AddressCacheData addressCacheData = new AddressCacheData(); 

	    addressCacheData.Address1 = street;
	    addressCacheData.City = city;
	    addressCacheData.Region = state;
	    addressCacheData.PostalCode = postalCode;
	    addressCacheData.Result = teleAtlasgeocodeData.OutPut;
	    addressCacheData.Latitude = teleAtlasgeocodeData.MatchLatitude;
	    addressCacheData.Longitude = teleAtlasgeocodeData.MatchLongitude;

	    AddressCache addressCache = new AddressCache();

	    if(teleAtlasgeocodeData.MatchType.Equals(new IntegerType(0)) || teleAtlasgeocodeData.MatchType.Equals(new IntegerType(6))){
		addressCacheData.Status = GeocodeStatusEnum.INVALID; 
		addressCache.Update(addressCacheData);
		//throw new GeocodeException();
	    }else{
		addressCacheData.Status = GeocodeStatusEnum.VALID;
		addressCache.Update(addressCacheData);
	    }	    

	    geocodeData.OutPut = StringType.Parse(teleAtlasgeocodeData.OutPut);
	    geocodeData.MatchCount = teleAtlasgeocodeData.MatchCount;
	    geocodeData.MatchType = teleAtlasgeocodeData.MatchType;
	    geocodeData.MatchDB = StringType.Parse(teleAtlasgeocodeData.MatchDB);
	    geocodeData.StdAddress = StringType.Parse( teleAtlasgeocodeData.StdAddress);
	    geocodeData.StdCity = StringType.Parse( teleAtlasgeocodeData.StdCity);
	    geocodeData.StdState = StringType.Parse( teleAtlasgeocodeData.StdState);
	    geocodeData.StdZipCode = StringType.Parse( teleAtlasgeocodeData.StdZipCode);
	    geocodeData.StdZipCodePlus4 = StringType.Parse( teleAtlasgeocodeData.StdZipCodePlus4);
	    geocodeData.StdDPBC = StringType.Parse( teleAtlasgeocodeData.StdDPBC);
	    geocodeData.StdCarrier = StringType.Parse( teleAtlasgeocodeData.StdCarrier);
	    geocodeData.MatchAddress = StringType.Parse( teleAtlasgeocodeData.MatchAddress);
	    geocodeData.MatchCity = StringType.Parse( teleAtlasgeocodeData.MatchAddress);
	    geocodeData.MatchState = StringType.Parse( teleAtlasgeocodeData.MatchState);
	    geocodeData.MatchZipCode = StringType.Parse( teleAtlasgeocodeData.MatchZipCode);
	    geocodeData.MatchLatitude = teleAtlasgeocodeData.MatchLatitude;
	    geocodeData.MatchLongitude = teleAtlasgeocodeData.MatchLongitude;

	    return geocodeData;
	}

	/// <summary>
	/// </summary>
	private TeleAtlasGeocodeData ExecuteCommand() {
	    //"-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    //inputText = "-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    string outputText = "";
	    TeleAtlasGeocodeData data = new TeleAtlasGeocodeData();

	    string gpgExecutable = _exeDirectory + "\\rie.exe";
	    MakeGeocodeParamString();

	    // Create startinfo object
	    log.Info("Geocode executable: " + gpgExecutable);
	    log.Info("Geocode input: " + inputText);
	    ProcessStartInfo pInfo = new ProcessStartInfo(gpgExecutable, inputText);
	    pInfo.WorkingDirectory = _exeDirectory;
	    pInfo.CreateNoWindow = true;
	    pInfo.UseShellExecute = false;
	    // Redirect everything:
	    //stdout to get message, stderr in case of errors...
	    pInfo.RedirectStandardInput = true;
	    pInfo.RedirectStandardOutput = true;
	    pInfo.RedirectStandardError = true;
	    _processObject = Process.Start(pInfo);

	    _outputString = "";
	    _errorString = "";

	    // Create two threads to read both output/error streams without creating a deadlock
	    ThreadStart outputEntry = new ThreadStart(StandardOutputReader);
	    Thread outputThread = new Thread(outputEntry);
	    outputThread.Start();
	    ThreadStart errorEntry = new ThreadStart(StandardErrorReader);
	    Thread errorThread = new Thread(errorEntry);
	    errorThread.Start();

	    if (_processObject.WaitForExit(processTimeOutMilliseconds)) {
		// Process exited before timeout...
		// Wait for the threads to complete reading output/error (but use a timeout!)
		if (!outputThread.Join(processTimeOutMilliseconds/2)) {
		    outputThread.Abort();
		}
		if (!errorThread.Join(processTimeOutMilliseconds/2)) {
		    errorThread.Abort();
		}
	    } else {
		// Process timeout: Geocde hung somewhere... kill it (as well as the threads!)
		_outputString = "";
		_errorString = "Timed out after " + processTimeOutMilliseconds.ToString() + " milliseconds";
		_processObject.Kill();
		if (outputThread.IsAlive) {
		    outputThread.Abort();
		}
		if (errorThread.IsAlive) {
		    errorThread.Abort();
		}
		log.Info(_errorString);
	    }

	    // Check results and prepare output
	    _exitcode = _processObject.ExitCode;
	    if (_exitcode == 0) {
		outputText = _outputString;
		log.Info(_outputString);
		data.OutPut = outputText;
	    } else {
		if (_errorString == "") {
		    _errorString = "Geocode: [" + _processObject.ExitCode.ToString() + "]: Unknown error";
		}
		log.Info(_errorString);
		throw new GeocodeException(_errorString, 0);
	    }
	    return data;
	}

	private string ExecuteGeocodeCountCommand() {
	    //"-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    //inputText = "-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    string outputText = "";

	    string gpgExecutable = _exeDirectory + "\\rie.exe";
	    MakeGeocodeParamStringToGetGeocodeCount();

	    // Create startinfo object
	    log.Info("Geocode executable: " + gpgExecutable);
	    log.Info("Geocode input: " + inputText);
	    ProcessStartInfo pInfo = new ProcessStartInfo(gpgExecutable, inputText);
	    pInfo.WorkingDirectory = _exeDirectory;
	    pInfo.CreateNoWindow = true;
	    pInfo.UseShellExecute = false;
	    // Redirect everything:
	    //stdout to get message, stderr in case of errors...
	    pInfo.RedirectStandardInput = true;
	    pInfo.RedirectStandardOutput = true;
	    pInfo.RedirectStandardError = true;
	    _processObject = Process.Start(pInfo);

	    _outputString = "";
	    _errorString = "";

	    // Create two threads to read both output/error streams without creating a deadlock
	    ThreadStart outputEntry = new ThreadStart(StandardOutputReader);
	    Thread outputThread = new Thread(outputEntry);
	    outputThread.Start();
	    ThreadStart errorEntry = new ThreadStart(StandardErrorReader);
	    Thread errorThread = new Thread(errorEntry);
	    errorThread.Start();

	    if (_processObject.WaitForExit(processTimeOutMilliseconds)) {
		// Process exited before timeout...
		// Wait for the threads to complete reading output/error (but use a timeout!)
		if (!outputThread.Join(processTimeOutMilliseconds/2)) {
		    outputThread.Abort();
		}
		if (!errorThread.Join(processTimeOutMilliseconds/2)) {
		    errorThread.Abort();
		}
	    } else {
		// Process timeout: Geocde hung somewhere... kill it (as well as the threads!)
		_outputString = "";
		_errorString = "Timed out after " + processTimeOutMilliseconds.ToString() + " milliseconds";
		_processObject.Kill();
		if (outputThread.IsAlive) {
		    outputThread.Abort();
		}
		if (errorThread.IsAlive) {
		    errorThread.Abort();
		}
		log.Info(_errorString);
	    }

	    // Check results and prepare output
	    _exitcode = _processObject.ExitCode;
	    if (_exitcode == 0) {
		outputText = _outputString;
		log.Info(_outputString);
	    } else {
		if (_errorString == "") {
		    _errorString = "Geocode: [" + _processObject.ExitCode.ToString() + "]: Unknown error";
		}
		log.Info(_errorString);
		throw new GeocodeException(_errorString, 0);
	    }
	    return outputText;
	}

	private void MakeGeocodeParamString(){
	    //"-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI"
	    inputText = "-g ";
	    inputText += "\"" + street.ToString() + "|" + city.ToString() + "|" + state.ToString() + "|" + zipCode.ToString() + "\"";
	    inputText += " -u " + userName.ToString() + " -p " + password.ToString();
	    //inputText += " -u " + "EZL0012953" + " -p " + "WurbI";
	    
	}

	private void MakeGeocodeParamStringToGetGeocodeCount(){
	    //"-I -u EZL0012953 -p WurbI"
	    inputText = "-I ";
	    inputText += " -u " + userName.ToString() + " -p " + password.ToString();
	    
	}

	/// <summary>
	/// Reader thread for standard output
	///
	/// <p/>Updates the private variable _outputString (locks it first)
	/// </summary>
	public void StandardOutputReader() {
	    string output = _processObject.StandardOutput.ReadToEnd();
	    lock(this) {
		_outputString = output;
	    }
	}

	/// <summary>
	/// Reader thread for standard error
	///
	/// <p/>Updates the private variable _errorString (locks it first)
	/// </summary>
	public void StandardErrorReader() {
	    string error = _processObject.StandardError.ReadToEnd();
	    lock(this) {
		_errorString = error;
	    }
	}

    }
}
