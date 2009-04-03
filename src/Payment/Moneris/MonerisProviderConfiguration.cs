using System;
using System.Text;
using Spring2.Core.Configuration;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.Moneris {
    public class MonerisProviderConfiguration : BaseConfigurationProvider {
 
	// configuration setting keys
	public static readonly String HOST_KEY = "Moneris.Host";
	public static readonly String STOREID_KEY = "Moneris.StoreId";
	public static readonly String APITOKEN_KEY = "Moneris.ApiToken";
	public static readonly String CRYPT_KEY = "Moneris.Crypt";
	public static readonly String ALLOWPOSTALCODEMISMATCH_KEY = "Moneris.AllowPostalCodeMismatch";
	public static readonly String ALLOWADDRESSMISMATCH_KEY = "Moneris.AllowAddressMismatch";
	public static readonly String ALLOWCVVMISMATCH_KEY = "Moneris.AllowCvvMismatch";
	public static readonly String TIMEOUT_KEY = "Moneris.Timeout";

	private String host = String.Empty;
	private String storeId = String.Empty;
	private String apiToken = String.Empty;
    	private String crypt = String.Empty;
	private Boolean allowPostalCodeMismatch = false;
	private Boolean allowAddressMismatch = false;
	private Boolean allowCvvMismatch = false;
	private int timeout = 100000;
    	
	public MonerisProviderConfiguration() {
	    Init();
	}

	public string Host {
	    get { return host; }
	}

	public string StoreId {
	    get { return storeId; }
	}

	public string ApiToken {
	    get { return apiToken; }
	}

	public string Crypt {
	    get { return crypt; }
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

	public int Timeout {
	    get { return timeout; }
	}

	public void Init() {
	    CheckConfiguration();
	    host = ConfigurationProvider.Instance.Settings[HOST_KEY];
	    storeId = ConfigurationProvider.Instance.Settings[STOREID_KEY];
	    apiToken = ConfigurationProvider.Instance.Settings[APITOKEN_KEY];
	    crypt = ConfigurationProvider.Instance.Settings[CRYPT_KEY];
	    allowPostalCodeMismatch = GetConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY, "0") == "1";
	    allowAddressMismatch = GetConfigurationValue(ALLOWADDRESSMISMATCH_KEY, "0") == "1";
	    allowCvvMismatch = GetConfigurationValue(ALLOWCVVMISMATCH_KEY, "0") == "1";
	    timeout = Int32.Parse(GetConfigurationValue(TIMEOUT_KEY, "100000"));
	}
    		
	private void CheckConfiguration() {
	    CheckConfigurationValue(HOST_KEY);
	    CheckConfigurationValue(STOREID_KEY);
	    CheckConfigurationValue(APITOKEN_KEY);
	    CheckConfigurationValue(CRYPT_KEY);
	    CheckConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWADDRESSMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWCVVMISMATCH_KEY);
	}
    	    	
    }
}