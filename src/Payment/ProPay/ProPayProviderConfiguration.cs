using System;
using System.Text;
using Spring2.Core.Payment;
using Spring2.Core.Configuration;
//using Spring2.Dss.Payment;

namespace Spring2.Core.Payment.ProPay {
    public class ProPayProviderConfiguration : BaseConfigurationProvider {

	// configuration setting keys
	public static readonly String HOST_URL_KEY = "ProPay.URL";
	public static readonly String TIMEOUT_KEY = "ProPay.Timeout";
	public static readonly String SIGNUP_CERTSTRING_KEY = "ProPay.SignupAccess.CertString";
	public static readonly String CARDPROCESSING_CERTSTRING_KEY = "ProPay.RealTimeCCProcessing.CertString";
	public static readonly String COMMISSIONPROCESSING_CERTSTRING_KEY = "ProPay.CommissionProcessing.CertString"; 
	//public static readonly String BUSINESSCLASS_KEY = "ProPay.BusinessClass"; (legacy ProPay value)
	private static readonly String BUSINESSCLASS = "partner";
	public static readonly String XMLENCODING_KEY = "ProPay.XMLEncoding";
	public static readonly String CORPORATE_ACCOUNT_NUMBER = "ProPay.CorporateAccountNumber";
	public static readonly String CORPORATE_ACCOUNT_EMAIL = "ProPay.CorporateAccountEmail";
	public static readonly String MINUMUM_CHARGE_FEE = "ProPay.MinimumFeeForCharge";
	public static readonly String CHARGE_FEES = "ProPay.ChargeFee";
	public static readonly String AFFILIATION = "ProPay.AffiliationString";

	private String hostAddress = String.Empty;
	private Int32 timeout = 0;
	private String signupCertString = String.Empty;
	private String cardProcessingCertString = String.Empty;
	private String commissionProcessingCertString = String.Empty;
	private String businessClass = String.Empty;
	private Encoding xmlEncoding = System.Text.Encoding.Unicode;
	private String corporateAccountNumber = String.Empty;
	private String corporateAccountEmail = String.Empty;
	private Decimal minimumChargeFee = 0.00M;
	private String chargeFees = String.Empty;
	private String affiliation = String.Empty;

	public ProPayProviderConfiguration() {
	    Init();
	}

	public string HostAddress {
	    get { return hostAddress; }
	}

	public int Timeout {
	    get { return timeout; }
	}

	public String SignupCertString {
	    get { return signupCertString; }
	}

	public String CardProcessingCertString {
	    get { return cardProcessingCertString; }
	}

	public String CommissionProcessingCertString {
	    get { return commissionProcessingCertString; }
	}

	public String CorporateAccountNumber {
	    get { return corporateAccountNumber; }
	}

	public String CorporateAccountEmail {
	    get { return corporateAccountEmail; }
	}

	public String BusinessClass {
	    get { return businessClass; }
	}

	public Encoding XMLEncoding {
	    get { return xmlEncoding; }
	}

	public Decimal MinimumChargeFee {
	    get { return minimumChargeFee; }
	}

	public String ChargeFees {
	    get { return chargeFees; }
	}

	public String Affiliation {
	    get { return affiliation; }
	}

	public void Init() {
	    CheckConfiguration();
	    String hostAddressFromConfig = ConfigurationProvider.Instance.Settings[HOST_URL_KEY];
	    if (hostAddressFromConfig != null && hostAddressFromConfig.Length > 0) {
		hostAddress = hostAddressFromConfig;
	    }

	    String timeoutFromConfig = ConfigurationProvider.Instance.Settings[TIMEOUT_KEY];
	    if (timeoutFromConfig != null && timeoutFromConfig.Length > 0) {
		timeout = int.Parse(timeoutFromConfig);
	    }

	    String signupCertStringFromConfig = ConfigurationProvider.Instance.Settings[SIGNUP_CERTSTRING_KEY];
	    if (signupCertStringFromConfig != null && signupCertStringFromConfig.Length > 0) {
		signupCertString = signupCertStringFromConfig;
	    }

	    String cardProcessingCertStringFromConfig = ConfigurationProvider.Instance.Settings[CARDPROCESSING_CERTSTRING_KEY];
	    if (cardProcessingCertStringFromConfig != null && cardProcessingCertStringFromConfig.Length > 0) {
		cardProcessingCertString = cardProcessingCertStringFromConfig;
	    }

	    String corporateAccountNumberFromConfig = ConfigurationProvider.Instance.Settings[CORPORATE_ACCOUNT_NUMBER];
	    if (corporateAccountNumberFromConfig != null && corporateAccountNumberFromConfig.Length > 0) {
		corporateAccountNumber = corporateAccountNumberFromConfig;
	    }

	    String corporateAccountEmailFromConfig = ConfigurationProvider.Instance.Settings[CORPORATE_ACCOUNT_EMAIL];
	    if (corporateAccountEmailFromConfig != null && corporateAccountEmailFromConfig.Length > 0) {
		corporateAccountEmail = corporateAccountEmailFromConfig;
	    }

	    String commissionProcessingCertStringFromConfig = ConfigurationProvider.Instance.Settings[COMMISSIONPROCESSING_CERTSTRING_KEY];
	    if (commissionProcessingCertStringFromConfig != null && commissionProcessingCertStringFromConfig.Length > 0) {
		commissionProcessingCertString = commissionProcessingCertStringFromConfig;
	    }

	    String businessClassFromConfig = BUSINESSCLASS; // This is a legacy value from ProPay, who has told us to just always use "partner"
	    if (businessClassFromConfig != null && businessClassFromConfig.Length > 0) {
		businessClass = businessClassFromConfig;
	    }

	    String encodingFromConfig = ConfigurationProvider.Instance.Settings[XMLENCODING_KEY];
	    switch(encodingFromConfig.ToLower()) {
		case "unicode":
		case "utf-16":
		    xmlEncoding = System.Text.Encoding.Unicode;
		    break;
		case "utf-8":
		    xmlEncoding = System.Text.Encoding.UTF8;
		    break;
		case "utf-7":
		    xmlEncoding = System.Text.Encoding.UTF7;
		    break;
		// utf-32 is not supported in early .NET versions
		//case "utf-32":
		//    xmlEncoding = System.Text.Encoding.UTF32;
		//    break;
	    }

	    String minimumChargeFeeFromConfig = ConfigurationProvider.Instance.Settings[MINUMUM_CHARGE_FEE];
	    if (minimumChargeFeeFromConfig != null && minimumChargeFeeFromConfig.Length > 0) {
		minimumChargeFee = Decimal.Parse(minimumChargeFeeFromConfig);
	    }

	    String feeRulesFromConfig = ConfigurationProvider.Instance.Settings[CHARGE_FEES];
	    if (feeRulesFromConfig != null && feeRulesFromConfig.Length > 0) {
		chargeFees = feeRulesFromConfig;
	    }

	    String affiliationFromConfig = ConfigurationProvider.Instance.Settings[AFFILIATION];
	    if (affiliationFromConfig != null && affiliationFromConfig.Length > 0) {
		affiliation = affiliationFromConfig;
	    }

	}

	private void CheckConfiguration() {
	    CheckConfigurationValue(HOST_URL_KEY);
	    CheckConfigurationValue(TIMEOUT_KEY);
	    CheckConfigurationValue(SIGNUP_CERTSTRING_KEY);
	    CheckConfigurationValue(CARDPROCESSING_CERTSTRING_KEY);
	    CheckConfigurationValue(COMMISSIONPROCESSING_CERTSTRING_KEY);
	    //CheckConfigurationValue(BUSINESSCLASS_KEY); ProPay legacy value, always use "partner"
	    CheckConfigurationValue(XMLENCODING_KEY);
	    CheckConfigurationValue(CORPORATE_ACCOUNT_NUMBER);
	    CheckConfigurationValue(CORPORATE_ACCOUNT_EMAIL);
	    CheckConfigurationValue(MINUMUM_CHARGE_FEE);
	    CheckConfigurationValue(CHARGE_FEES);
	    CheckConfigurationValue(AFFILIATION);
	}


    }
}