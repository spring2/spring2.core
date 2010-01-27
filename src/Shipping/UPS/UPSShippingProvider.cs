using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Spring2.Core.Types;
using Spring2.Core.Configuration;

namespace Spring2.Core.Shipping.UPS {
    class UPSShippingProvider : IShippingProvider {

            private string UPSAccessKey {
                get {
                return ConfigurationProvider.Instance.Settings["UPS.AccessKey"]; 
                }
            }

            private string UPSUserId {
                get {
                    return ConfigurationProvider.Instance.Settings["UPS.UserId"]; 
                    }
            }

            private string UPSPassword {
                get {
                    return ConfigurationProvider.Instance.Settings["UPS.Password"];
                }
            }

            private string UPSServer {
                get {
                    return ConfigurationProvider.Instance.Settings["UPS.TNTServer"]; //"https://wwwcie.ups.com/ups.app/xml/TimeInTransit"; 
                }
            }

            public ITimeInTransitResponse TimeInTransit(ArtifactAddressData fromLocation, ArtifactAddressData toLocation, DateTimeType pickupDateTime, DecimalType shipmentWeight, UnitOfMeasurementEnum unitOfMeasurment, CurrencyCodeEnum currencyCode, CurrencyType monetaryValue) {
                
                string response = string.Empty;
                UPSTimeInTransitRequest timeInTransit = new UPSTimeInTransitRequest(fromLocation, toLocation, pickupDateTime, shipmentWeight, unitOfMeasurment, currencyCode, monetaryValue);
                string request = UPSAccessRequest.AccessRequestXml(UPSAccessKey, UPSUserId, UPSPassword).ToString() + Environment.NewLine + timeInTransit.GetRequestXml();

                // Set encoding & get content Length
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(request);

                // Prepare post request
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(UPSServer);
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = data.Length;
                Stream requestStream = webRequest.GetRequestStream();
                // Send the data
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                // get the response
                WebResponse webResponse = null;
                try {
                    webResponse = webRequest.GetResponse();
                    using (StreamReader sr = new StreamReader(webResponse.GetResponseStream())) {
                        response = sr.ReadToEnd();
                        sr.Close();
                    }
                    UPSTimeInTransitResponse resp = new UPSTimeInTransitResponse(StringType.Parse(response));
                    resp.ParseTimeInTransitResponse();
                    return resp;
                } finally {
                    if (webResponse != null) {
                        webResponse.Close();
                    }
                }
            }

            private bool AddressIsBlockRange(string street) {
                return System.Text.RegularExpressions.Regex.IsMatch(street, @"^\d+-\d+");
            }
    
    }
}
