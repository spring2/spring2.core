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

namespace Spring2.Core.Geocode.TeleAtlasWS {
    /// <summary>
    /// Summary description for GecodeWrapper.
    /// </summary>
    public class TeleAtlasCmdWrapper {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	private static String EXE = "Spring2.Core.Geocode.TeleAtlasExe.exe";

	protected StringType street;
	protected StringType city;
	protected StringType state;
	protected StringType zipCode;
	protected StringType password;
	protected StringType userName;

	// Variables used to store property values (prefix: underscore "_")
	private string _exeDirectory = @".\";
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

	public GeocodeData DoGeocode(StringType street, StringType city, StringType state, StringType postalCode, StringType path, CountryCodeEnum ccode){
	    this.street = street;
	    this.city = city;
	    this.state = state;
	    this.zipCode = postalCode;
	    this.password = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodePassword"]);
	    this.userName = StringType.Parse(ConfigurationProvider.Instance.Settings["GeocodeUserName"]);
	    DirectoryInfo directory = new DirectoryInfo(ConfigurationProvider.Instance.Settings["GeocodePath"]);

	    if (path.Length > 0 && System.IO.File.Exists((path.ToString() + "\\" + EXE))) {
		exeDirectory = path.ToString();
	    } else if (System.IO.File.Exists((directory.FullName + "\\" + EXE))) {
		exeDirectory = directory.FullName;
	    } else if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), EXE))) {
		exeDirectory = Directory.GetCurrentDirectory();
	    } else {
		throw new Exception(String.Format("{0} could not be found.", directory.FullName + "\\" + EXE));
	    }

	    inputText = String.Format("\"{0}|{1}|{2}|{3}|\"", street, city, state, postalCode);
	    GeocodeData result = ExecuteCommand();
	    return result;
	}

	/// <summary>
	/// </summary>
	private GeocodeData ExecuteCommand() {
	    //"-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    //inputText = "-g \"232 Madison Ave|New York City|New York|10016\" -u EZL0012953 -p WurbI";
	    string outputText = "";

	    string gpgExecutable = _exeDirectory + "\\" + EXE;

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
	    GeocodeData data;
	    if (_exitcode == 0) {
		outputText = _outputString;
		log.Info(_outputString);
		data = new GeocodeData(outputText);
	    } else {
		if (_errorString == "") {
		    _errorString = "Geocode: [" + _processObject.ExitCode.ToString() + "]: Unknown error";
		}
		log.Info(_errorString);
		throw new GeocodeException(_errorString, 0);
	    }
	    return data;
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
