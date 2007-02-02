//------------------------------------------------------------------------------
// this class was hand modified after generation to add support for logging request and response
//------------------------------------------------------------------------------

using Spring2.Core.Soap;

namespace Spring2.Core.net.esalestax.webservices1 {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CertiCalcSoap", Namespace="http://webservices.esalestax.net/CertiTAX.NET")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(MarshalByRefObject))]
    public class CertiCalc : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public CertiCalc() {
            this.Url = "https://webservices.esalestax.net/CertiTAX.NET/CertiCalc.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Abandon_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Abandon(TaxTransaction TaxTransaction) {
            this.Invoke("Abandon", new object[] {
                        TaxTransaction});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAbandon(TaxTransaction TaxTransaction, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Abandon", new object[] {
                        TaxTransaction}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAbandon(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Abandon1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Abandon_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Abandon(string CertiTAXTransactionId, string SerialNumber) {
            this.Invoke("Abandon1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAbandon1(string CertiTAXTransactionId, string SerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Abandon1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAbandon1(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Abandon2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Abandon", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Abandon(string CertiTAXTransactionId, string SerialNumber, string ReferredId) {
            this.Invoke("Abandon2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAbandon2(string CertiTAXTransactionId, string SerialNumber, string ReferredId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Abandon2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndAbandon2(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("Calculate", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate1", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate1(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate1", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate1(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NORIDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("Calculate2", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate2(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate2", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate3")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NORID_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NORID_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate3(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate3(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate4")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NOTEResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("Calculate4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate4(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate4(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate5")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NOTE_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate5", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate5(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate5", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate5(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate6")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NORID_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NORID_NOTEResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("Calculate6", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate6(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate6", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate6(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate7")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_NORID_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_NORID_NOTE_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate7(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate7(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate8")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_RecalcResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string PreviousCertiTAXTransactionId, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("Calculate8", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        PreviousCertiTAXTransactionId,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate8(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string PreviousCertiTAXTransactionId, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate8", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        PreviousCertiTAXTransactionId,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate8(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate9")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string PreviousCertiTAXTransactionId, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate9", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        PreviousCertiTAXTransactionId,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate9(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string PreviousCertiTAXTransactionId, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate9", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        PreviousCertiTAXTransactionId,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate9(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate10")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NORIDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("Calculate10", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate10(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate10", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate10(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate11")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NORID_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NORID_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate11", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate11(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate11", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate11(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate12")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NOTEResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("Calculate12", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate12(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate12", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate12(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate13")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NOTE_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate13", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate13(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate13", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate13(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate14")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NORID_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NORID_NOTEResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("Calculate14", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate14(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate14", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate14(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate15")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_Recalc_NORID_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_Recalc_NORID_NOTE_EDResult")]
        public TaxTransaction Calculate(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate15", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate15(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    string PreviousCertiTAXTransactionId, 
                    bool CalculateTax, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("Calculate15", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        PreviousCertiTAXTransactionId,
                        CalculateTax,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate15(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate16")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_DSResult")]
        [Log4NetExtension("CertiCalc")]
        public TaxTransaction Calculate(Order Order) {
            object[] results = this.Invoke("Calculate16", new object[] {
                        Order});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate16(Order Order, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Calculate16", new object[] {
                        Order}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate16(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Calculate17")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Calculate_DS_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("Calculate_DS_EDResult")]
        public TaxTransaction Calculate(Order Order, System.DateTime EffectiveDate) {
            object[] results = this.Invoke("Calculate17", new object[] {
                        Order,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCalculate17(Order Order, System.DateTime EffectiveDate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Calculate17", new object[] {
                        Order,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndCalculate17(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Cancel_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Cancel(TaxTransaction TaxTransaction) {
            this.Invoke("Cancel", new object[] {
                        TaxTransaction});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCancel(TaxTransaction TaxTransaction, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Cancel", new object[] {
                        TaxTransaction}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCancel(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Cancel1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Cancel_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Cancel(string CertiTAXTransactionId, string SerialNumber) {
            this.Invoke("Cancel1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCancel1(string CertiTAXTransactionId, string SerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Cancel1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCancel1(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Cancel2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Cancel", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Cancel(string CertiTAXTransactionId, string SerialNumber, string ReferredId) {
            this.Invoke("Cancel2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCancel2(string CertiTAXTransactionId, string SerialNumber, string ReferredId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Cancel2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCancel2(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Commit_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Commit(TaxTransaction TaxTransaction) {
            this.Invoke("Commit", new object[] {
                        TaxTransaction});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCommit(TaxTransaction TaxTransaction, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Commit", new object[] {
                        TaxTransaction}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCommit(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Commit1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Commit_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [Log4NetExtension("CertiCalc")]
        public void Commit(string CertiTAXTransactionId, string SerialNumber) {
            this.Invoke("Commit1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCommit1(string CertiTAXTransactionId, string SerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Commit1", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCommit1(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="Commit2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/Commit", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public void Commit(string CertiTAXTransactionId, string SerialNumber, string ReferredId) {
            this.Invoke("Commit2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCommit2(string CertiTAXTransactionId, string SerialNumber, string ReferredId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Commit2", new object[] {
                        CertiTAXTransactionId,
                        SerialNumber,
                        ReferredId}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndCommit2(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_TRANSACTION", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_TRANSACTIONResult")]
        public TaxTransaction ProcessAttributedLoss(TaxTransaction CommittedTaxTransaction) {
            object[] results = this.Invoke("ProcessAttributedLoss", new object[] {
                        CommittedTaxTransaction});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss(TaxTransaction CommittedTaxTransaction, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss", new object[] {
                        CommittedTaxTransaction}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_TID_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_TID_NORIDResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string CertiTAXTransactionId) {
            object[] results = this.Invoke("ProcessAttributedLoss1", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss1(string SerialNumber, string CertiTAXTransactionId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss1", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss1(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_NORID_NOTOTAL" +
"", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_NORID_NOTOTALResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge) {
            object[] results = this.Invoke("ProcessAttributedLoss2", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss2(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss2", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss3")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_NORIDResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedLoss3", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss3(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss3", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss3(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss4")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_NORID_OT", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_NORID_OTResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string CertiTAXTransactionId, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedLoss4", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss4(string SerialNumber, string CertiTAXTransactionId, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss4", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss4(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss5")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_TID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_TIDResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string ReferredId, string CertiTAXTransactionId) {
            object[] results = this.Invoke("ProcessAttributedLoss5", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss5(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss5", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss5(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss6")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_NOTOTAL", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_NOTOTALResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge) {
            object[] results = this.Invoke("ProcessAttributedLoss6", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss6(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss6", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss6(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss7")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLossResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedLoss7", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss7(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss7", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss7(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss8")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_OT", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_OTResult")]
        public TaxTransaction ProcessAttributedLoss(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedLoss8", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss8(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss8", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss8(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedLoss9")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedLoss_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedLoss_DSResult")]
        public TaxTransaction ProcessAttributedLoss(Order OrderChanges) {
            object[] results = this.Invoke("ProcessAttributedLoss9", new object[] {
                        OrderChanges});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedLoss9(Order OrderChanges, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedLoss9", new object[] {
                        OrderChanges}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedLoss9(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_TRANSACTION" +
"", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_TRANSACTIONResult")]
        public TaxTransaction ProcessAttributedReturn(TaxTransaction CommittedTaxTransaction) {
            object[] results = this.Invoke("ProcessAttributedReturn", new object[] {
                        CommittedTaxTransaction});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn(TaxTransaction CommittedTaxTransaction, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn", new object[] {
                        CommittedTaxTransaction}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_TID_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_TID_NORIDResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string CertiTAXTransactionId) {
            object[] results = this.Invoke("ProcessAttributedReturn1", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn1(string SerialNumber, string CertiTAXTransactionId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn1", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn1(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_NORID_NOTOT" +
"AL", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_NORID_NOTOTALResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge) {
            object[] results = this.Invoke("ProcessAttributedReturn2", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn2(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn2", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn3")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_NORIDResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedReturn3", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn3(string SerialNumber, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn3", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn3(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn4")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_NORID_OT", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_NORID_OTResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string CertiTAXTransactionId, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedReturn4", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn4(string SerialNumber, string CertiTAXTransactionId, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn4", new object[] {
                        SerialNumber,
                        CertiTAXTransactionId,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn4(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn5")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_TID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_TIDResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string ReferredId, string CertiTAXTransactionId) {
            object[] results = this.Invoke("ProcessAttributedReturn5", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn5(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn5", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn5(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn6")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_NOTOTAL", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_NOTOTALResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge) {
            object[] results = this.Invoke("ProcessAttributedReturn6", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn6(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn6", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn6(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn7")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturnResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedReturn7", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn7(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal ShippingCharge, System.Decimal HandlingCharge, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn7", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        ShippingCharge,
                        HandlingCharge,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn7(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn8")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_OT", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_OTResult")]
        public TaxTransaction ProcessAttributedReturn(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal Total) {
            object[] results = this.Invoke("ProcessAttributedReturn8", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        Total});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn8(string SerialNumber, string ReferredId, string CertiTAXTransactionId, System.Decimal Total, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn8", new object[] {
                        SerialNumber,
                        ReferredId,
                        CertiTAXTransactionId,
                        Total}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn8(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessAttributedReturn9")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessAttributedReturn_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessAttributedReturn_DSResult")]
        public TaxTransaction ProcessAttributedReturn(Order OrderChanges) {
            object[] results = this.Invoke("ProcessAttributedReturn9", new object[] {
                        OrderChanges});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAttributedReturn9(Order OrderChanges, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAttributedReturn9", new object[] {
                        OrderChanges}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessAttributedReturn9(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("ProcessUnattributedLoss", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NORIDResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("ProcessUnattributedLoss1", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss1(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss1", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss1(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NOTEResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("ProcessUnattributedLoss2", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss2(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss2", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss3")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NORID_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NORID_NOTEResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("ProcessUnattributedLoss3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss3(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss3(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss4")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_EDResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedLoss4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss4(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss4(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss5")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NORID_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NORID_EDResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedLoss5", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss5(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss5", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss5(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss6")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NOTE_EDResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedLoss6", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss6(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss6", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss6(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss7")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_NORID_NOTE_" +
"ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_NORID_NOTE_EDResult")]
        public TaxTransaction ProcessUnattributedLoss(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedLoss7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss7(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss7(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss8")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_DSResult")]
        public TaxTransaction ProcessUnattributedLoss(Order Order) {
            object[] results = this.Invoke("ProcessUnattributedLoss8", new object[] {
                        Order});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss8(Order Order, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss8", new object[] {
                        Order}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss8(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedLoss9")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedLoss_DS_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedLoss_DS_EDResult")]
        public TaxTransaction ProcessUnattributedLoss(Order Order, System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedLoss9", new object[] {
                        Order,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedLoss9(Order Order, System.DateTime EffectiveDate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessUnattributedLoss9", new object[] {
                        Order,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedLoss9(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("ProcessUnattributedReturn", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn1")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NORID", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NORIDResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason) {
            object[] results = this.Invoke("ProcessUnattributedReturn1", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn1(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn1", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn1(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn2")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NOTE", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NOTEResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("ProcessUnattributedReturn2", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn2(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn2", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn2(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn3")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NORID_NOT" +
"E", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NORID_NOTEResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode) {
            object[] results = this.Invoke("ProcessUnattributedReturn3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn3(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn3", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn3(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn4")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_EDResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedReturn4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn4(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn4", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn4(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn5")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NORID_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NORID_EDResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedReturn5", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn5(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    string TaxExemptCertificate, 
                    string TaxExemptIssuer, 
                    string TaxExemptReason, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn5", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        TaxExemptCertificate,
                        TaxExemptIssuer,
                        TaxExemptReason,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn5(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn6")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NOTE_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NOTE_EDResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedReturn6", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn6(
                    string SerialNumber, 
                    string ReferredId, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn6", new object[] {
                        SerialNumber,
                        ReferredId,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn6(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn7")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_NORID_NOT" +
"E_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_NORID_NOTE_EDResult")]
        public TaxTransaction ProcessUnattributedReturn(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedReturn7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn7(
                    string SerialNumber, 
                    string Location, 
                    string MerchantTransactionId, 
                    string Nexus, 
                    string Name, 
                    string Street1, 
                    string Street2, 
                    string City, 
                    string County, 
                    string State, 
                    string PostalCode, 
                    string Nation, 
                    System.Decimal ShippingCharge, 
                    System.Decimal HandlingCharge, 
                    System.Decimal Total, 
                    bool ConfirmAddress, 
                    int DefaultProductCode, 
                    System.DateTime EffectiveDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn7", new object[] {
                        SerialNumber,
                        Location,
                        MerchantTransactionId,
                        Nexus,
                        Name,
                        Street1,
                        Street2,
                        City,
                        County,
                        State,
                        PostalCode,
                        Nation,
                        ShippingCharge,
                        HandlingCharge,
                        Total,
                        ConfirmAddress,
                        DefaultProductCode,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn7(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn8")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_DS", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_DSResult")]
        public TaxTransaction ProcessUnattributedReturn(Order Order) {
            object[] results = this.Invoke("ProcessUnattributedReturn8", new object[] {
                        Order});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn8(Order Order, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn8", new object[] {
                        Order}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn8(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute(MessageName="ProcessUnattributedReturn9")]
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://webservices.esalestax.net/CertiTAX.NET/ProcessUnattributedReturn_DS_ED", RequestNamespace="http://webservices.esalestax.net/CertiTAX.NET", ResponseNamespace="http://webservices.esalestax.net/CertiTAX.NET")]
        [return: System.Xml.Serialization.SoapElementAttribute("ProcessUnattributedReturn_DS_EDResult")]
        public TaxTransaction ProcessUnattributedReturn(Order Order, System.DateTime EffectiveDate) {
            object[] results = this.Invoke("ProcessUnattributedReturn9", new object[] {
                        Order,
                        EffectiveDate});
            return ((TaxTransaction)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessUnattributedReturn9(Order Order, System.DateTime EffectiveDate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessUnattributedReturn9", new object[] {
                        Order,
                        EffectiveDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public TaxTransaction EndProcessUnattributedReturn9(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((TaxTransaction)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("TaxTransaction", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class TaxTransaction : MarshalByRefObject {
        
        /// <remarks/>
        public CorrectedAddress CorrectedAddress;
        
        /// <remarks/>
        public string CertiTAXTransactionId;
        
        /// <remarks/>
        public System.Decimal CityTax;
        
        /// <remarks/>
        public string CityTaxAuthority;
        
        /// <remarks/>
        public System.Decimal CountyTax;
        
        /// <remarks/>
        public string CountyTaxAuthority;
        
        /// <remarks/>
        public TaxTransactionLineItem[] LineItems;
        
        /// <remarks/>
        public System.Decimal LocalTax;
        
        /// <remarks/>
        public string LocalTaxAuthority;
        
        /// <remarks/>
        public int MerchantId;
        
        /// <remarks/>
        public System.Decimal NationalTax;
        
        /// <remarks/>
        public string NationalTaxAuthority;
        
        /// <remarks/>
        public System.Decimal OtherTax;
        
        /// <remarks/>
        public string OtherTaxAuthority;
        
        /// <remarks/>
        public string ReferredId;
        
        /// <remarks/>
        public string SerialNumber;
        
        /// <remarks/>
        public string Server;
        
        /// <remarks/>
        public System.Decimal StateTax;
        
        /// <remarks/>
        public string StateTaxAuthority;
        
        /// <remarks/>
        public System.Decimal TotalTax;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("CorrectedAddress", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class CorrectedAddress : MarshalByRefObject {
        
        /// <remarks/>
        public string City;
        
        /// <remarks/>
        public string GeoCode;
        
        /// <remarks/>
        public string Name;
        
        /// <remarks/>
        public string PostalCode;
        
        /// <remarks/>
        public string State;
        
        /// <remarks/>
        public string Street1;
        
        /// <remarks/>
        public string Street2;
        
        /// <remarks/>
        public string NationISO2Abbreviation;
        
        /// <remarks/>
        public string NationISO3Abbreviation;
        
        /// <remarks/>
        public string NationName;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("MarshalByRefObject", "http://webservices.esalestax.net/CertiTAX.NET")]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(OrderLineItem))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Address))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(Order))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(TaxTransactionLineItem))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(CorrectedAddress))]
    [System.Xml.Serialization.SoapIncludeAttribute(typeof(TaxTransaction))]
    public abstract class MarshalByRefObject {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("OrderLineItem", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class OrderLineItem : MarshalByRefObject {
        
        /// <remarks/>
        public string ItemId;
        
        /// <remarks/>
        public string StockingUnit;
        
        /// <remarks/>
        public int Quantity;
        
        /// <remarks/>
        public System.Decimal ExtendedPrice;
        
        /// <remarks/>
        public int ProductCode;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("Address", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class Address : MarshalByRefObject {
        
        /// <remarks/>
        public string City;
        
        /// <remarks/>
        public string County;
        
        /// <remarks/>
        public string Name;
        
        /// <remarks/>
        public string Nation;
        
        /// <remarks/>
        public string PostalCode;
        
        /// <remarks/>
        public string State;
        
        /// <remarks/>
        public string Street1;
        
        /// <remarks/>
        public string Street2;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("Order", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class Order : MarshalByRefObject {
        
        /// <remarks/>
        public Address Address;
        
        /// <remarks/>
        public bool CalculateTax;
        
        /// <remarks/>
        public string CertiTAXTransactionId;
        
        /// <remarks/>
        public bool ConfirmAddress;
        
        /// <remarks/>
        public int DefaultProductCode;
        
        /// <remarks/>
        public System.Decimal HandlingCharge;
        
        /// <remarks/>
        public OrderLineItem[] LineItems;
        
        /// <remarks/>
        public string Location;
        
        /// <remarks/>
        public string MerchantTransactionId;
        
        /// <remarks/>
        public string Nexus;
        
        /// <remarks/>
        public string ReferredId;
        
        /// <remarks/>
        public string SerialNumber;
        
        /// <remarks/>
        public System.Decimal ShippingCharge;
        
        /// <remarks/>
        public string TaxExemptCertificate;
        
        /// <remarks/>
        public string TaxExemptIssuer;
        
        /// <remarks/>
        public string TaxExemptReason;
        
        /// <remarks/>
        public System.Decimal Total;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.SoapTypeAttribute("TaxTransactionLineItem", "http://webservices.esalestax.net/CertiTAX.NET")]
    public class TaxTransactionLineItem : MarshalByRefObject {
        
        /// <remarks/>
        public string ItemId;
        
        /// <remarks/>
        public bool CityTaxApplied;
        
        /// <remarks/>
        public bool CountyTaxApplied;
        
        /// <remarks/>
        public bool LocalTaxApplied;
        
        /// <remarks/>
        public bool NationalTaxApplied;
        
        /// <remarks/>
        public bool OtherTaxApplied;
        
        /// <remarks/>
        public bool StateTaxApplied;
    }
}
