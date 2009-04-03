using System;
using System.Text;
using Spring2.Core.Configuration;
using Spring2.Core.Types;

namespace Spring2.Core.Payment.EFSNet {
    public class EFSNetProviderConfiguration : BaseConfigurationProvider {
 
	// configuration setting keys
	public static readonly String WEBSERVICEURL_KEY = "EFSNet.WebServiceUrl";
	public static readonly String STOREID_KEY = "EFSNet.StoreId";
	public static readonly String STOREKEY_KEY = "EFSNet.StoreKey";
	public static readonly String APPLICATIONID_KEY = "EFSNet.ApplicationId";
	public static readonly String ALLOWPOSTALCODEMISMATCH_KEY = "EFSNet.AllowPostalCodeMismatch";
	public static readonly String ALLOWADDRESSMISMATCH_KEY = "EFSNet.AllowAddressMismatch";
	public static readonly String ALLOWCVVMISMATCH_KEY = "EFSNet.AllowCvvMismatch";
	public static readonly String TIMEOUT_KEY = "EFSNet.Timeout";

	private String webSeviceUrl = String.Empty;
	private string storeId = String.Empty;
	private String storeKey = String.Empty;
	private String applicationId = String.Empty;
	private Boolean allowPostalCodeMismatch = false;
	private Boolean allowAddressMismatch = false;
	private Boolean allowCvvMismatch = false;
	private int timeout = 100000;
    	
	public EFSNetProviderConfiguration() {
	    Init();
	}

	public string WebSeviceUrl {
	    get { return webSeviceUrl; }
	}

	public string StoreId {
	    get { return storeId; }
	}

	public string StoreKey {
	    get { return storeKey; }
	}

	public string ApplicationId {
	    get { return applicationId; }
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
	    webSeviceUrl = ConfigurationProvider.Instance.Settings[WEBSERVICEURL_KEY];
	    storeId = ConfigurationProvider.Instance.Settings[STOREID_KEY];
	    storeKey = ConfigurationProvider.Instance.Settings[STOREKEY_KEY];
	    applicationId = ConfigurationProvider.Instance.Settings[APPLICATIONID_KEY];
	    allowPostalCodeMismatch = GetConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY, "0") == "1";
	    allowAddressMismatch = GetConfigurationValue(ALLOWADDRESSMISMATCH_KEY, "0") == "1";
	    allowCvvMismatch = GetConfigurationValue(ALLOWCVVMISMATCH_KEY, "0") == "1";
	    timeout = Int32.Parse(GetConfigurationValue(TIMEOUT_KEY, "100000"));
	}
    		
	private void CheckConfiguration() {
	    CheckConfigurationValue(WEBSERVICEURL_KEY);
	    CheckConfigurationValue(STOREID_KEY);
	    CheckConfigurationValue(STOREKEY_KEY);
	    CheckConfigurationValue(APPLICATIONID_KEY);
	    CheckConfigurationValue(ALLOWPOSTALCODEMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWADDRESSMISMATCH_KEY);
	    CheckConfigurationValue(ALLOWCVVMISMATCH_KEY);
	}
    	    	
    }
}