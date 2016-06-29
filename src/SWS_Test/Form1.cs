using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Spring2.Core.PostageService.Stamps;
using Spring2.Core.PostageService.Enums;

namespace Spring2.Core.PostageService.StampsDemo {

    public partial class Form1 : Form {
	private StampsProvider provider;
	private string trackingNumber;
	private List<string> integrationTxIDs;

        public Form1() {
            InitializeComponent();
	    provider = new StampsProvider("36f039b2-d233-457d-86b9-54f81aafbe21", txbPassword.Text, txbUsername.Text, "https://swsim.testing.stamps.com/swsim/SwsimV52.asmx");
	    integrationTxIDs = new List<string>();
	}

        private void button1_Click(object sender, EventArgs e) {
            tbxOut.Clear();
            try {
		provider.AuthenticateUser();
                try {
                    tbxOut.Text += @"Authenticator:" + Environment.NewLine + provider.LastLoginTime 
			+ Environment.NewLine + provider.ClearCredential
			+ Environment.NewLine + provider.LoginBannerText
			+ Environment.NewLine + provider.LoginBannerText
			+ Environment.NewLine + provider.PasswordExpired
			+ Environment.NewLine + Environment.NewLine + "LastLogin: " + provider.LastLoginTime;
                } catch (Exception exception) {
                    tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
                }
            } catch (Exception exception) {
                string message = exception.Message.ToString();
                MessageBox.Show(message);
            }
        }

	private void btnGAI_Click(object sender, EventArgs e) {
	    tbxOut.Clear();
	    try {
		SWSIMV52.GetAccountInfoResponse accountInfo = provider.GetAccountInfo();
		tbxOut.Text += "Account Info" + Environment.NewLine;
		foreach (var property in accountInfo.AccountInfo.GetType().GetProperties()) {
		    var name = property.Name.ToString();
		    var value = property.GetValue(accountInfo.AccountInfo, null);
		    tbxOut.Text += name + ": " + value + Environment.NewLine;
		}
		tbxOut.Text += Environment.NewLine + "Address" + Environment.NewLine;
		foreach (var property in accountInfo.Address.GetType().GetProperties()) {
		    var name = property.Name.ToString();
		    var value = property.GetValue(accountInfo.Address, null);
		    tbxOut.Text += name + ": " + value + Environment.NewLine;
		}

		tbxOut.Text += $"Available Postage: {accountInfo.AccountInfo.PostageBalance.AvailablePostage}" + Environment.NewLine;
		tbxOut.Text += Environment.NewLine + @"Email: " + accountInfo.CustomerEmail + Environment.NewLine;
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

	private void button1_Click_1(Object sender, EventArgs e) {
            tbxOut.Clear();
            try {
                var address = new SWSIMV52.Address();
                address.Address1 = "4682 Serendipity Way";
                address.City = "South Jordan";
                address.State = "UT";
                address.FullName = "Cort Schaefer";
                address.ZIPCode = "84095";

                try {
		    SWSIMV52.CleanseAddressResponse response = provider.CleanseAddress(address, "84070");

                    tbxOut.Text += $"AddressMatch: {response.AddressMatch}" + Environment.NewLine;
                    tbxOut.Text += $"CityStateZipOK: {response.CityStateZipOK}" + Environment.NewLine;
                    tbxOut.Text += $"Residential: {response.ResidentialDeliveryIndicator}" + Environment.NewLine;
                    tbxOut.Text += $"IsPOBox: {response.IsPOBox}" + Environment.NewLine;
                    tbxOut.Text += $"Status: {response.StatusCodes.ReturnCode}" + Environment.NewLine;
                    tbxOut.Text += $"Candidate Addresses: {response.CandidateAddresses.Length}";
                    foreach (var c in response.CandidateAddresses) {
                        tbxOut.Text += $"candidate address: {c.Address1}" + Environment.NewLine;
                        tbxOut.Text += $"candidate address: {c.Address2}" + Environment.NewLine;
                        tbxOut.Text += $"candidate address: {c.Address3}" + Environment.NewLine;
                        tbxOut.Text += $"candidate address: {c.City}" + Environment.NewLine;
                        tbxOut.Text += $"candidate address: {c.State}" + Environment.NewLine;
                        tbxOut.Text += $"candidate address: {c.ZIPCode}" + Environment.NewLine;
                    }
                    tbxOut.Text += $"Rates: {response.Rates.Length}";
                    foreach (var r in response.Rates) {
                        tbxOut.Text += $"rate amount: {r.Amount}" + Environment.NewLine;
                        tbxOut.Text += $"rate service type: {r.ServiceType}" + Environment.NewLine;
                        tbxOut.Text += $"rate package type: {r.PackageType}" + Environment.NewLine;
                        tbxOut.Text += $"rate zone: {r.Zone}" + Environment.NewLine;
                        tbxOut.Text += $"rate max amount: {r.MaxAmount}" + Environment.NewLine;
                        tbxOut.Text += $"rate delivery days: {r.DeliverDays}" + Environment.NewLine;
                        tbxOut.Text += $"rate delivery date: {r.DeliveryDate}" + Environment.NewLine;
                    }
                } catch (Exception exception) {
                    tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
                }
            } catch (Exception exception) {
                string message = exception.Message.ToString();
                MessageBox.Show(message);
            }
        }

	private void button2_Click(Object sender, EventArgs e) {
	    tbxOut.Clear();
	    try {
		PostageLabelInputData input = new PostageLabelInputData() {
		    MailClass = MailClassEnum.FIRST,
		    WeightOz = 8,
		    MailpieceShape = MailpieceShapeEnum.THICKENVELOPE,
		    FromCompany = "Spring2",
		    ReturnAddress1 = "10150 Centennial Parkway",
		    ReturnAddress2 = "Suite 210",
		    FromCity = "Sandy",
		    FromState = "UT",
		    FromPostalCode = "84070",
		    FromPhone = "2125551234", //This is required... 
		    ToName = "Elmer Fudd",
		    ToAddress1 = "10150 Centennial Parkway",
		    ToAddress2 = "Suite 310",
		    ToCity = "Sandy",
		    ToState = "UT",
		    ToPostalCode = "84070",
		    ShipDate = DateTime.Now.ToString("MM/dd/yyyy"),
		    ShipTime = DateTime.Now.ToString("hh:mm tt")
		};

		PostageLabelData response = provider.GetPostageLabel(input);
		trackingNumber = response.TrackingNumber;
		integrationTxIDs.Add(response.ReferenceID);
		Byte[] imageData = Convert.FromBase64String(response.Base64LabelImage);
		tbxOut.Text += $"StampsTxID: {response.ReferenceID}" + Environment.NewLine;
		tbxOut.Text += $"Images: {1}" + Environment.NewLine;

		var image = ByteToImage(imageData);
		pictureBox1.Image = image;
		image.Save("label.png");
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

	private void button3_Click(Object sender, EventArgs e) {
	    tbxOut.Clear();

	    try {
		PostageRateInputData input = new PostageRateInputData() {
		    MailClass = MailClassEnum.FIRST,
		    MailpieceShape = MailpieceShapeEnum.THICKENVELOPE,
		    FromPostalCode = "84070",
		    ToPostalCode = "84095",
		    ShipDate = DateTime.Today.ToString()
		};
		PostageRatesData responseData = provider.GetPostageRates(input);


		tbxOut.Text += $"Rates: {responseData.PostagePrice.Count}" + Environment.NewLine;
		foreach (var r in responseData.PostagePrice) {
		    tbxOut.Text += $"rate amount: {r.TotalAmount}" + Environment.NewLine;
		    tbxOut.Text += $"rate service type: {r.MailClass}" + Environment.NewLine;
		}
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

        public static Bitmap ByteToImage(byte[] blob) {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

	private void button4_Click(Object sender, EventArgs e) {
	    tbxOut.Clear();
	    try {
		PostagePurchaseInputData input = new PostagePurchaseInputData() {
		    RecreditAmount = "100"
		};
		provider.GetAccountInfo();
		PurchasedPostageData responseData = provider.BuyPostage(input);
		
		tbxOut.Text += $"rejectionReason: {responseData.ErrorMessage}" + Environment.NewLine;
		tbxOut.Text += $"transactionId: {responseData.RequestID}" + Environment.NewLine;
		tbxOut.Text += $"Available Postage: {responseData.CertifiedIntermediary.PostageBalance}" + Environment.NewLine;
		tbxOut.Text += $"Control Total: {responseData.CertifiedIntermediary.AscendingBalance}" + Environment.NewLine;
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

	private void button5_Click(Object sender, EventArgs e) {
	    tbxOut.Clear();
	    try {
		if (!String.IsNullOrWhiteSpace(trackingNumber)) {
		    RefundRequestData response = provider.RefundRequest(trackingNumber, false);
		    tbxOut.Text += $"Refund Initiated";
		} else {
		    tbxOut.Text += $"rejectionReason: You must create a label first" + Environment.NewLine;
		}
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

	private void button6_Click(Object sender, EventArgs e) {
	    tbxOut.Clear();
	    try {
		if (integrationTxIDs.Count > 0) {
		    SWSIMV52.Address address = new SWSIMV52.Address() {
			Address1 = "10150 Centennial Parkway",
			Address2 = "Suite 210",
			City = "Sandy",
			Company = "Spring2",
			State = "UT",
			ZIPCode = "84070",
			PhoneNumber = "2125551234"
		    };
		    SWSIMV52.CreateScanFormResponse response = provider.GetScanForm(integrationTxIDs.ToArray(), address);
		    tbxOut.Text += $"SCAN Form ID: {response.ScanFormId}" + Environment.NewLine;
		    tbxOut.Text += $"SCAN Form URL: {response.Url}";
		} else {
		    tbxOut.Text += $"rejectionReason: You must create labels to populate IntegrationTxIDs first" + Environment.NewLine;
		}
	    } catch (Exception exception) {
		tbxOut.Text += @"Error: " + exception.Message.ToString() + Environment.NewLine;
	    }
	}

	private void txbPassword_TextChanged(Object sender, EventArgs e) {

	}
    }
}
