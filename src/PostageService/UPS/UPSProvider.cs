using System;
using Spring2.Core.Configuration;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;

namespace Spring2.Core.PostageService.UPS {
    public class UPSProvider : IPostageServiceProvider {
	UPSMI.Ship.ShipService shipService;
	UPSModelAssembler assembler;

	string accessLicenseNumber;
	string username;
	string password;
	string shipperNumber;
	string costCenter;

	public UPSProvider() {
	    SetCredentials();
	    InitializeUPSShipService();
	    assembler = new UPSModelAssembler();
	}

	public UPSProvider(string accessLicenseNumber, string username, string password, string shipperNumber, string costCenter, string postageServerUrl) {
	    SetCredentials(accessLicenseNumber, username, password, shipperNumber, costCenter);
	    InitializeUPSShipService(postageServerUrl);
	    assembler = new UPSModelAssembler();
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    UPSMI.Ship.ShipmentRequest request = assembler.ToShipmentRequest(data, shipperNumber, costCenter);
	    UPSMI.Ship.ShipmentResponse response = shipService.ProcessShipment(request);
	    return assembler.ToPostageLabelData(response);
	}

	public RefundRequestData RefundRequest(String trackingNumber, bool isInternational) {
	    throw new NotImplementedException();
	}

	public PostageRateData GetPostageRate(PostageRateInputData data) {
	    // Not implementing in UPS provider at this time
	    throw new NotImplementedException();
	}

	public PostageRatesData GetPostageRates(PostageRateInputData data) {
	    // Not implementing in UPS provider at this time
	    throw new NotImplementedException();
	}

	public PasswordChangedData ChangePassword(ChangePasswordInputData data) {
	    // Not implementing in UPS provider at this time
	    throw new NotImplementedException();
	}

	public PurchasedPostageData BuyPostage(PostagePurchaseInputData data) {
	    // Not implementing in UPS provider at this time
	    throw new NotImplementedException();
	}

	private void SetCredentials() {
	    SetCredentials(ConfigurationProvider.Instance.Settings["PostageService.UPS.AccessLicenseNumber"],
		ConfigurationProvider.Instance.Settings["PostageService.UPS.Username"],
		ConfigurationProvider.Instance.Settings["PostageService.UPS.Password"],
		ConfigurationProvider.Instance.Settings["PostageService.UPS.ShipperNumber"],
		ConfigurationProvider.Instance.Settings["PostageService.UPS.costCenter"]);
	}

	private void SetCredentials(string accessLicenseNumber, string username, string password, string shipperNumber, string costCenter) {
	    this.accessLicenseNumber = accessLicenseNumber;
	    this.username = username;
	    this.password = password;
	    this.shipperNumber = shipperNumber;
	    this.costCenter = costCenter;
	}

	private void InitializeUPSShipService() {
	    InitializeUPSShipService(ConfigurationProvider.Instance.Settings["PostageService.UPS.PostageServerUrl"]);
	}

	private void InitializeUPSShipService(string url) {
	    shipService = new UPSMI.Ship.ShipService() {
		UPSSecurityValue = new UPSMI.Ship.UPSSecurity() {
		    ServiceAccessToken = new UPSMI.Ship.UPSSecurityServiceAccessToken() {
			AccessLicenseNumber = accessLicenseNumber
		    },
		    UsernameToken = new UPSMI.Ship.UPSSecurityUsernameToken() {
			Username = username,
			Password = password
		    }
		},
		Url = url
	    };
	    System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
	}


    }
}
