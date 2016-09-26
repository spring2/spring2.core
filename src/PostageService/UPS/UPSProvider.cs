using System;
using Spring2.Core.Configuration;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;

namespace Spring2.Core.PostageService.UPS {
    public class UPSProvider : IPostageServiceProvider {
	UPSWS.Ship.ShipService shipService;
	UPSModelAssembler assembler;

	string accessLicenseNumber;
	string username;
	string password;
	string shipperNumber;
	string shipperName;
	string costCenter;

	public UPSProvider() {
	    SetCredentials();
	    InitializeUPSShipService();
	    assembler = new UPSModelAssembler();
	}

	public UPSProvider(string accessLicenseNumber, string username, string password, string shipperNumber, string shipperName, string costCenter, string postageServerUrl) {
	    SetCredentials(accessLicenseNumber, username, password, shipperNumber, shipperName, costCenter);
	    InitializeUPSShipService(postageServerUrl);
	    assembler = new UPSModelAssembler();
	}

	public PostageLabelData GetPostageLabel(PostageLabelInputData data) {
	    try {
		UPSWS.Ship.ShipmentRequest request = assembler.ToShipmentRequest(data, shipperNumber, shipperName, costCenter);
		UPSWS.Ship.ShipmentResponse response = shipService.ProcessShipment(request);
		return assembler.ToPostageLabelData(response);
	    } catch (System.Web.Services.Protocols.SoapException ex) {
		StringBuilder error = new StringBuilder();
		error.Append("---------Ship Web Service returns error----------------");
		error.Append(Environment.NewLine);
		error.Append("---------\"Hard\" is user error \"Transient\" is system error----------------");
		error.Append(Environment.NewLine);
		error.Append("SoapException Message= " + ex.Message);
		error.Append(Environment.NewLine);
		error.Append("SoapException Category:Code:Message= " + ex.Detail.LastChild.InnerText);
		error.Append(Environment.NewLine);
		error.Append("SoapException XML String for all= " + ex.Detail.LastChild.OuterXml);
		error.Append(Environment.NewLine);
		error.Append("SoapException StackTrace= " + ex.StackTrace);
		return assembler.ToPostageLabelErrorData(error.ToString());
	    } catch (System.ServiceModel.CommunicationException ex) {
		StringBuilder error = new StringBuilder();
		error.Append("CommunicationException= " + ex.Message);
		error.Append(Environment.NewLine);
		error.Append("CommunicationException-StackTrace= " + ex.StackTrace);
		return assembler.ToPostageLabelErrorData(error.ToString());
	    } catch (Exception ex) {
		StringBuilder error = new StringBuilder();
		error.Append(" General Exception= " + ex.Message);
		error.Append(Environment.NewLine);
		error.Append(" General Exception-StackTrace= " + ex.StackTrace);
		return assembler.ToPostageLabelErrorData(error.ToString());
	    }	    
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
		ConfigurationProvider.Instance.Settings["PostageService.UPS.ShipperName"],
		ConfigurationProvider.Instance.Settings["PostageService.UPS.costCenter"]);
	}

	private void SetCredentials(string accessLicenseNumber, string username, string password, string shipperNumber, string shipperName, string costCenter) {
	    this.accessLicenseNumber = accessLicenseNumber;
	    this.username = username;
	    this.password = password;
	    this.shipperNumber = shipperNumber;
	    this.shipperName = shipperName;
	    this.costCenter = costCenter;
	}

	private void InitializeUPSShipService() {
	    InitializeUPSShipService(ConfigurationProvider.Instance.Settings["PostageService.UPS.PostageServerUrl"]);
	}

	private void InitializeUPSShipService(string url) {
	    shipService = new UPSWS.Ship.ShipService();
	    shipService.UPSSecurityValue = new UPSWS.Ship.UPSSecurity() {
		ServiceAccessToken = new UPSWS.Ship.UPSSecurityServiceAccessToken() {
		    AccessLicenseNumber = accessLicenseNumber
		},
		UsernameToken = new UPSWS.Ship.UPSSecurityUsernameToken() {
		    Username = username,
		    Password = password
		}
	    };
	    shipService.Url = url;
	}
    }
}
