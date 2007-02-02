using System;
using System.Text;
using Spring2.Core.Configuration;

namespace Spring2.Dss.Payment.PayflowPro {
    public class PayflowProProviderConfiguration : BaseConfigurationProvider {
 
	// configuration setting keys
	public static readonly String REMOSTHOST_KEY = "PayflowPro.RemoteHost";
	public static readonly String TIMEOUT_KEY = "PayflowPro.Timeout";
	public static readonly String PROXY_KEY = "PayflowPro.Proxy";
	public static readonly String CONNECTIONSTRING_KEY = "PayflowPro.ConnectionString";
	public static readonly String ALLOWPOSTALCODEMISMATCH_KEY = "PayflowPro.AllowPostalCodeMismatch";
	public static readonly String ALLOWADDRESSMISMATCH_KEY = "PayflowPro.AllowAddressMismatch";
	public static readonly String ALLOWCVVMISMATCH_KEY = "PayflowPro.AllowCvvMismatch";

	private string hostAddress = String.Empty;
	private int hostPort = 0;
	private int timeout = 0;
	private string proxyAddress = String.Empty;
	private int proxyPort = 0;
	private string proxyLogon = String.Empty;
	private string proxyPassword = String.Empty;
	private Boolean allowPostalCodeMismatch = false;
	private Boolean allowAddressMismatch = false;
	private Boolean allowCvvMismatch = false;
    	private String connectionString = String.Empty;

	public PayflowProProviderConfiguration() {
	    Init();
	}

	public string HostAddress {
	    get { return hostAddress; }
	}

	public int HostPort {
	    get { return hostPort; }
	}

	public int Timeout {
	    get { return timeout; }
	}

	public string ProxyAddress {
	    get { return proxyAddress; }
	}

	public int ProxyPort {
	    get { return proxyPort; }
	}

	public string ProxyLogon {
	    get { return proxyLogon; }
	}

	public string ProxyPassword {
	    get { return proxyPassword; }
	}

	public bool AllowPostalCodeMismatch {
	    get { return allowPostalCodeMismatch; }
	}

	public bool AllowAddressMismatch {
	    get { return allowAddressMismatch; }
	}

	public bool AllowCvvMismatch {
	    get { return allowCvvMismatch; }
	}

    	public void Init() {
	    CheckConfiguration();
	    string temp = ConfigurationProvider.Instance.Settings[REMOSTHOST_KEY];
	    if (temp != null && temp.Length > 0) {
		string[] host = temp.Split(new char[] {':'}, 2);
		hostAddress = host[0];
		if (host.Length > 1) {
		    hostPort = int.Parse(host[1]);
		}
	    }

	    temp = ConfigurationProvider.Instance.Settings[TIMEOUT_KEY];
	    if (temp != null && temp.Length > 0) {
		timeout = int.Parse(temp);
	    }

	    temp = ConfigurationProvider.Instance.Settings[PROXY_KEY];
	    if (temp != null && temp.Length > 0) {
		string[] host = temp.Split(new char[] {':'}, 4);
		proxyAddress = host[0];
		if (host.Length > 1) {
		    proxyPort = int.Parse(host[1]);
		    if (host.Length > 2) {
			proxyLogon = host[2];
			if (host.Length > 3) {
			    proxyPassword = host[3];
			}
		    }
		}
	    }
	    allowPostalCodeMismatch = GetConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY, "0") == "1";
	    allowAddressMismatch = GetConfigurationValue(ALLOWADDRESSMISMATCH_KEY, "0") == "1";
	    allowCvvMismatch = GetConfigurationValue(ALLOWCVVMISMATCH_KEY, "0") == "1";
	    connectionString = ParseConnectionString();
	}
    		
	private void CheckConfiguration() {
	    CheckConfigurationValue(REMOSTHOST_KEY);
	    CheckConfigurationValue(CONNECTIONSTRING_KEY);
	    CheckConfigurationValue(TIMEOUT_KEY);
	    CheckConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWADDRESSMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWCVVMISMATCH_KEY);
	}
    	
	public String ConnectionString {
	    get {
	    	return this.connectionString;
	    }
	}
    	
    	private String ParseConnectionString() {
	    String connectionString = ConfigurationProvider.Instance.Settings[CONNECTIONSTRING_KEY];
	    String[] connectionParameters = connectionString.Split(';');

	    if (connectionParameters.Length <= 1) {
		throw new PaymentConfigurationException("Invalid format for " + CONNECTIONSTRING_KEY);
	    }

	    StringBuilder connection = new StringBuilder();

	    foreach (String connectionParameter in connectionParameters) {
		string[] parameter = connectionParameter.Split(new char[] {'='}, 2);
		if (parameter.Length != 2) {
		    throw new PaymentConfigurationException("Invalid format for " + CONNECTIONSTRING_KEY);
		}

		if (connection.Length > 0) {
		    connection.Append('&');
		}
		connection.Append(parameter[0].Trim().ToUpper()).Append('=');
		connection.Append(parameter[1].Trim());
	    }
	    	
	    return connection.ToString();
    	}

    }
}