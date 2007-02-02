// 
// This source code was hand modified after generation from WSDL
// 
using System.Net;
using System.Xml;

using Spring2.Core.Soap;

namespace Spring2.Core.com.concordebiz.efsnet {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EFSnetSoapBinding", Namespace="http://tempuri.org/wsdl/")]
    public class EFSnet2 : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public EFSnet2() {
            //this.Url = "";
        }
    	

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryIdentityChekAuditSummary", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
        public int QueryIdentityChekAuditSummary(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string InquiryNumber, 
                    string BranchID, 
                    string MultiBank, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string BusinessName, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryIdentityChekAuditSummary", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        InquiryNumber,
                        BranchID,
                        MultiBank,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        BusinessName});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryIdentityChekAuditSummary(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string InquiryNumber, 
                    string BranchID, 
                    string MultiBank, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string BusinessName, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryIdentityChekAuditSummary", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        InquiryNumber,
                        BranchID,
                        MultiBank,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        BusinessName}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryIdentityChekAuditSummary(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryIdentityChekAuditRecords", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryIdentityChekAuditRecords(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string InquiryNumber, 
                    string BranchID, 
                    string MultiBank, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string BusinessName, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryIdentityChekAuditRecords", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        InquiryNumber,
                        BranchID,
                        MultiBank,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        BusinessName});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryIdentityChekAuditRecords(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string InquiryNumber, 
                    string BranchID, 
                    string MultiBank, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string BusinessName, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryIdentityChekAuditRecords", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        InquiryNumber,
                        BranchID,
                        MultiBank,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        BusinessName}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryIdentityChekAuditRecords(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryIdentityChekTransactionTotals", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryIdentityChekTransactionTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string InquiryNumber, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryIdentityChekTransactionTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        InquiryNumber});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryIdentityChekTransactionTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string InquiryNumber, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryIdentityChekTransactionTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        InquiryNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryIdentityChekTransactionTotals(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryIdentityChekTransactions", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryIdentityChekTransactions(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string InquiryNumber, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryIdentityChekTransactions", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        InquiryNumber});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryIdentityChekTransactions(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string InquiryNumber, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryIdentityChekTransactions", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        InquiryNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryIdentityChekTransactions(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryBatchTotals", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryBatchTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string BatchID, 
                    string BatchStatus, 
                    string BatchSubmitDateBegin, 
                    string BatchSubmitDateEnd, 
                    string BatchSubmitTimeBegin, 
                    string BatchSubmitTimeEnd, 
                    string BatchCompleteDateBegin, 
                    string BatchCompleteDateEnd, 
                    string BatchCompleteTimeBegin, 
                    string BatchCompleteTimeEnd, 
                    string ReferenceNumber, 
                    string Comments, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryBatchTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        BatchID,
                        BatchStatus,
                        BatchSubmitDateBegin,
                        BatchSubmitDateEnd,
                        BatchSubmitTimeBegin,
                        BatchSubmitTimeEnd,
                        BatchCompleteDateBegin,
                        BatchCompleteDateEnd,
                        BatchCompleteTimeBegin,
                        BatchCompleteTimeEnd,
                        ReferenceNumber,
                        Comments});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryBatchTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string BatchID, 
                    string BatchStatus, 
                    string BatchSubmitDateBegin, 
                    string BatchSubmitDateEnd, 
                    string BatchSubmitTimeBegin, 
                    string BatchSubmitTimeEnd, 
                    string BatchCompleteDateBegin, 
                    string BatchCompleteDateEnd, 
                    string BatchCompleteTimeBegin, 
                    string BatchCompleteTimeEnd, 
                    string ReferenceNumber, 
                    string Comments, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryBatchTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        BatchID,
                        BatchStatus,
                        BatchSubmitDateBegin,
                        BatchSubmitDateEnd,
                        BatchSubmitTimeBegin,
                        BatchSubmitTimeEnd,
                        BatchCompleteDateBegin,
                        BatchCompleteDateEnd,
                        BatchCompleteTimeBegin,
                        BatchCompleteTimeEnd,
                        ReferenceNumber,
                        Comments}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryBatchTotals(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryBatches", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryBatches(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string BatchID, 
                    string BatchStatus, 
                    string BatchSubmitDateBegin, 
                    string BatchSubmitDateEnd, 
                    string BatchSubmitTimeBegin, 
                    string BatchSubmitTimeEnd, 
                    string BatchCompleteDateBegin, 
                    string BatchCompleteDateEnd, 
                    string BatchCompleteTimeBegin, 
                    string BatchCompleteTimeEnd, 
                    string ReferenceNumber, 
                    string Comments, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryBatches", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        BatchID,
                        BatchStatus,
                        BatchSubmitDateBegin,
                        BatchSubmitDateEnd,
                        BatchSubmitTimeBegin,
                        BatchSubmitTimeEnd,
                        BatchCompleteDateBegin,
                        BatchCompleteDateEnd,
                        BatchCompleteTimeBegin,
                        BatchCompleteTimeEnd,
                        ReferenceNumber,
                        Comments});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryBatches(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string BatchID, 
                    string BatchStatus, 
                    string BatchSubmitDateBegin, 
                    string BatchSubmitDateEnd, 
                    string BatchSubmitTimeBegin, 
                    string BatchSubmitTimeEnd, 
                    string BatchCompleteDateBegin, 
                    string BatchCompleteDateEnd, 
                    string BatchCompleteTimeBegin, 
                    string BatchCompleteTimeEnd, 
                    string ReferenceNumber, 
                    string Comments, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryBatches", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        BatchID,
                        BatchStatus,
                        BatchSubmitDateBegin,
                        BatchSubmitDateEnd,
                        BatchSubmitTimeBegin,
                        BatchSubmitTimeEnd,
                        BatchCompleteDateBegin,
                        BatchCompleteDateEnd,
                        BatchCompleteTimeBegin,
                        BatchCompleteTimeEnd,
                        ReferenceNumber,
                        Comments}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryBatches(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryTransactionTotals", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryTransactionTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TerminalID, 
                    string TransactionType, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string SettlementDateBegin, 
                    string SettlementDateEnd, 
                    string ParentTransactionID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ApprovalNumber, 
                    string ClientIPAddress, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryTransactionTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TerminalID,
                        TransactionType,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        SettlementDateBegin,
                        SettlementDateEnd,
                        ParentTransactionID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        RoutingNumber,
                        CheckNumber,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ApprovalNumber,
                        ClientIPAddress,
                        OriginalTransactionID,
                        OriginalTransactionAmount});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryTransactionTotals(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TerminalID, 
                    string TransactionType, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string SettlementDateBegin, 
                    string SettlementDateEnd, 
                    string ParentTransactionID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ApprovalNumber, 
                    string ClientIPAddress, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryTransactionTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TerminalID,
                        TransactionType,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        SettlementDateBegin,
                        SettlementDateEnd,
                        ParentTransactionID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        RoutingNumber,
                        CheckNumber,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ApprovalNumber,
                        ClientIPAddress,
                        OriginalTransactionID,
                        OriginalTransactionAmount}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryTransactionTotals(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.QueryTransactions", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int QueryTransactions(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TerminalID, 
                    string TransactionType, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string SettlementDateBegin, 
                    string SettlementDateEnd, 
                    string ParentTransactionID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ApprovalNumber, 
                    string ClientIPAddress, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    out string ItemCount, 
                    out string QueryDataSize, 
                    out string QueryData) {
            object[] results = this.Invoke("QueryTransactions", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TerminalID,
                        TransactionType,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        SettlementDateBegin,
                        SettlementDateEnd,
                        ParentTransactionID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        RoutingNumber,
                        CheckNumber,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ApprovalNumber,
                        ClientIPAddress,
                        OriginalTransactionID,
                        OriginalTransactionAmount});
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginQueryTransactions(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string DeliveryMethod, 
                    string TransactionID, 
                    string TerminalID, 
                    string TransactionType, 
                    string TransactionStatus, 
                    string TransactionDateBegin, 
                    string TransactionDateEnd, 
                    string TransactionTimeBegin, 
                    string TransactionTimeEnd, 
                    string SettlementDateBegin, 
                    string SettlementDateEnd, 
                    string ParentTransactionID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ApprovalNumber, 
                    string ClientIPAddress, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("QueryTransactions", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        DeliveryMethod,
                        TransactionID,
                        TerminalID,
                        TransactionType,
                        TransactionStatus,
                        TransactionDateBegin,
                        TransactionDateEnd,
                        TransactionTimeBegin,
                        TransactionTimeEnd,
                        SettlementDateBegin,
                        SettlementDateEnd,
                        ParentTransactionID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        RoutingNumber,
                        CheckNumber,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ApprovalNumber,
                        ClientIPAddress,
                        OriginalTransactionID,
                        OriginalTransactionAmount}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndQueryTransactions(System.IAsyncResult asyncResult, out string ItemCount, out string QueryDataSize, out string QueryData) {
            object[] results = this.EndInvoke(asyncResult);
            ItemCount = ((string)(results[1]));
            QueryDataSize = ((string)(results[2]));
            QueryData = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GetSettlementTotals", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GetSettlementTotals(string StoreID, string StoreKey, string ApplicationID, string SettlementDate, out string ResultCode, out string ResultMessage, out string SettledAmount, out string DetailedTotals) {
            object[] results = this.Invoke("GetSettlementTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        SettlementDate});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            SettledAmount = ((string)(results[3]));
            DetailedTotals = ((string)(results[4]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSettlementTotals(string StoreID, string StoreKey, string ApplicationID, string SettlementDate, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSettlementTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        SettlementDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetSettlementTotals(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string SettledAmount, out string DetailedTotals) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            SettledAmount = ((string)(results[3]));
            DetailedTotals = ((string)(results[4]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GetProcessorTotals", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GetProcessorTotals(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ActivityPeriod, out string ResultCode, out string ResultMessage, out string ProcessedAmount, out string DetailedTotals) {
            object[] results = this.Invoke("GetProcessorTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ActivityPeriod});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            ProcessedAmount = ((string)(results[3]));
            DetailedTotals = ((string)(results[4]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetProcessorTotals(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ActivityPeriod, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetProcessorTotals", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ActivityPeriod}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGetProcessorTotals(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string ProcessedAmount, out string DetailedTotals) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            ProcessedAmount = ((string)(results[3]));
            DetailedTotals = ((string)(results[4]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.TimeOutReversal", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int TimeOutReversal(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string TransactionAmount, out string ResultCode, out string ResultMessage) {
            object[] results = this.Invoke("TimeOutReversal", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        TransactionAmount});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTimeOutReversal(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string TransactionAmount, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("TimeOutReversal", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        TransactionAmount}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTimeOutReversal(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.VoidTransaction", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int VoidTransaction(string StoreID, string StoreKey, string ApplicationID, string TransactionID, out string ResultCode, out string ResultMessage) {
            object[] results = this.Invoke("VoidTransaction", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TransactionID});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginVoidTransaction(string StoreID, string StoreKey, string ApplicationID, string TransactionID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("VoidTransaction", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TransactionID}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndVoidTransaction(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.IdentityChek", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int IdentityChek(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string OriginalInquiryNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string InquiryNumber, 
                    out string TaxIDNumberStartDate, 
                    out string TaxIDNumberEndDate, 
                    out string IdentificationStartDate, 
                    out string IdentificationEndDate, 
                    out string VerifyParameters, 
                    out string VerifyAlerts, 
                    out string ResendParameters, 
                    out string ResendTries, 
                    out string MaxTriesExceeded, 
                    out string WarningFlags) {
            object[] results = this.Invoke("IdentityChek", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        OriginalInquiryNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            InquiryNumber = ((string)(results[6]));
            TaxIDNumberStartDate = ((string)(results[7]));
            TaxIDNumberEndDate = ((string)(results[8]));
            IdentificationStartDate = ((string)(results[9]));
            IdentificationEndDate = ((string)(results[10]));
            VerifyParameters = ((string)(results[11]));
            VerifyAlerts = ((string)(results[12]));
            ResendParameters = ((string)(results[13]));
            ResendTries = ((string)(results[14]));
            MaxTriesExceeded = ((string)(results[15]));
            WarningFlags = ((string)(results[16]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginIdentityChek(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string ClerkNumber, 
                    string FirstName, 
                    string MiddleName, 
                    string LastName, 
                    string NameSuffix, 
                    string BusinessName, 
                    string FirstAddress, 
                    string FirstCity, 
                    string FirstState, 
                    string FirstPostalCode, 
                    string FirstCountry, 
                    string SecondAddress, 
                    string SecondCity, 
                    string SecondState, 
                    string SecondPostalCode, 
                    string SecondCountry, 
                    string HomePhoneNumber, 
                    string WorkPhoneNumber, 
                    string TaxIDNumber, 
                    string AlienRegistrationNumber, 
                    string CountryOfCitizenship, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string BirthDate, 
                    string BirthCity, 
                    string BirthState, 
                    string BirthCountry, 
                    string Gender, 
                    string OriginalInquiryNumber, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("IdentityChek", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        ClerkNumber,
                        FirstName,
                        MiddleName,
                        LastName,
                        NameSuffix,
                        BusinessName,
                        FirstAddress,
                        FirstCity,
                        FirstState,
                        FirstPostalCode,
                        FirstCountry,
                        SecondAddress,
                        SecondCity,
                        SecondState,
                        SecondPostalCode,
                        SecondCountry,
                        HomePhoneNumber,
                        WorkPhoneNumber,
                        TaxIDNumber,
                        AlienRegistrationNumber,
                        CountryOfCitizenship,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        BirthDate,
                        BirthCity,
                        BirthState,
                        BirthCountry,
                        Gender,
                        OriginalInquiryNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndIdentityChek(
                    System.IAsyncResult asyncResult, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string InquiryNumber, 
                    out string TaxIDNumberStartDate, 
                    out string TaxIDNumberEndDate, 
                    out string IdentificationStartDate, 
                    out string IdentificationEndDate, 
                    out string VerifyParameters, 
                    out string VerifyAlerts, 
                    out string ResendParameters, 
                    out string ResendTries, 
                    out string MaxTriesExceeded, 
                    out string WarningFlags) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            InquiryNumber = ((string)(results[6]));
            TaxIDNumberStartDate = ((string)(results[7]));
            TaxIDNumberEndDate = ((string)(results[8]));
            IdentificationStartDate = ((string)(results[9]));
            IdentificationEndDate = ((string)(results[10]));
            VerifyParameters = ((string)(results[11]));
            VerifyAlerts = ((string)(results[12]));
            ResendParameters = ((string)(results[13]));
            ResendTries = ((string)(results[14]));
            MaxTriesExceeded = ((string)(results[15]));
            WarningFlags = ((string)(results[16]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.TeleCheckICARefund", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int TeleCheckICARefund(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.Invoke("TeleCheckICARefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTeleCheckICARefund(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("TeleCheckICARefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTeleCheckICARefund(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.TeleCheckICASettle", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int TeleCheckICASettle(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.Invoke("TeleCheckICASettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTeleCheckICASettle(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("TeleCheckICASettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTeleCheckICASettle(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.TeleCheckICAAuthorize", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int TeleCheckICAAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    string CheckType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string TaxIDNumber, 
                    string AccountBusinessName, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string ExtendedResultMessage) {
            object[] results = this.Invoke("TeleCheckICAAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR,
                        CheckType,
                        IdentificationNumber,
                        IdentificationState,
                        TaxIDNumber,
                        AccountBusinessName,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            ExtendedResultMessage = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTeleCheckICAAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    string CheckType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string TaxIDNumber, 
                    string AccountBusinessName, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("TeleCheckICAAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR,
                        CheckType,
                        IdentificationNumber,
                        IdentificationState,
                        TaxIDNumber,
                        AccountBusinessName,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTeleCheckICAAuthorize(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string ExtendedResultMessage) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            ExtendedResultMessage = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.TeleCheckRiskManagement", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int TeleCheckRiskManagement(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string BirthDate, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("TeleCheckRiskManagement", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR,
                        IdentificationNumber,
                        IdentificationState,
                        BirthDate});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginTeleCheckRiskManagement(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string BirthDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("TeleCheckRiskManagement", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR,
                        IdentificationNumber,
                        IdentificationState,
                        BirthDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndTeleCheckRiskManagement(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.STARChekDirectDebit", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int STARChekDirectDebit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("STARChekDirectDebit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSTARChekDirectDebit(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string Currency, string AccountNumber, string RoutingNumber, string CheckNumber, string MICR, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("STARChekDirectDebit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndSTARChekDirectDebit(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.STARChekDirectVerify", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int STARChekDirectVerify(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("STARChekDirectVerify", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSTARChekDirectVerify(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string Currency, string AccountNumber, string RoutingNumber, string CheckNumber, string MICR, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("STARChekDirectVerify", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndSTARChekDirectVerify(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.FIAccountVerification", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int FIAccountVerification(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string PCTranCode, 
                    string AccountFirstName, 
                    string AccountMiddleName, 
                    string AccountLastName, 
                    string AccountBusinessName, 
                    string AccountAddress, 
                    string AccountCity, 
                    string AccountState, 
                    string AccountPostalCode, 
                    string AccountCountry, 
                    string AccountEmail, 
                    string AccountHomePhone, 
                    string AccountWorkPhone, 
                    string AccountTaxIDNumber, 
                    string AccountBirthDate, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string IdentificationIssueDate, 
                    string IdentificationExpirationDate, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string NonParticipantFlag, 
                    out string AccountStatusCodes, 
                    out string AccountVerificationFlags) {
            object[] results = this.Invoke("FIAccountVerification", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        PCTranCode,
                        AccountFirstName,
                        AccountMiddleName,
                        AccountLastName,
                        AccountBusinessName,
                        AccountAddress,
                        AccountCity,
                        AccountState,
                        AccountPostalCode,
                        AccountCountry,
                        AccountEmail,
                        AccountHomePhone,
                        AccountWorkPhone,
                        AccountTaxIDNumber,
                        AccountBirthDate,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        IdentificationIssueDate,
                        IdentificationExpirationDate});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            NonParticipantFlag = ((string)(results[6]));
            AccountStatusCodes = ((string)(results[7]));
            AccountVerificationFlags = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginFIAccountVerification(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string PCTranCode, 
                    string AccountFirstName, 
                    string AccountMiddleName, 
                    string AccountLastName, 
                    string AccountBusinessName, 
                    string AccountAddress, 
                    string AccountCity, 
                    string AccountState, 
                    string AccountPostalCode, 
                    string AccountCountry, 
                    string AccountEmail, 
                    string AccountHomePhone, 
                    string AccountWorkPhone, 
                    string AccountTaxIDNumber, 
                    string AccountBirthDate, 
                    string IdentificationType, 
                    string IdentificationNumber, 
                    string IdentificationState, 
                    string IdentificationCountry, 
                    string IdentificationIssueDate, 
                    string IdentificationExpirationDate, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("FIAccountVerification", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        PCTranCode,
                        AccountFirstName,
                        AccountMiddleName,
                        AccountLastName,
                        AccountBusinessName,
                        AccountAddress,
                        AccountCity,
                        AccountState,
                        AccountPostalCode,
                        AccountCountry,
                        AccountEmail,
                        AccountHomePhone,
                        AccountWorkPhone,
                        AccountTaxIDNumber,
                        AccountBirthDate,
                        IdentificationType,
                        IdentificationNumber,
                        IdentificationState,
                        IdentificationCountry,
                        IdentificationIssueDate,
                        IdentificationExpirationDate}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndFIAccountVerification(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string NonParticipantFlag, out string AccountStatusCodes, out string AccountVerificationFlags) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            NonParticipantFlag = ((string)(results[6]));
            AccountStatusCodes = ((string)(results[7]));
            AccountVerificationFlags = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.STARChek", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int STARChek(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string RoutingNumber, 
                    string CheckNumber, 
                    string MICR, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("STARChek", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSTARChek(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string Currency, string AccountNumber, string RoutingNumber, string CheckNumber, string MICR, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("STARChek", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        Currency,
                        AccountNumber,
                        RoutingNumber,
                        CheckNumber,
                        MICR}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndSTARChek(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardDeactivate", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardDeactivate(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardDeactivate", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardDeactivate(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track1, string Track2, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardDeactivate", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardDeactivate(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardActivate", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardActivate(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardActivate", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardActivate(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track1, string Track2, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardActivate", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardActivate(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardBalanceIncrease", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardBalanceIncrease(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardBalanceIncrease", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardBalanceIncrease(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track1, string Track2, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardBalanceIncrease", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardBalanceIncrease(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardBalanceInquiry", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardBalanceInquiry(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string CashierNumber, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardBalanceInquiry", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        CashierNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardBalanceInquiry(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string CashierNumber, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track1, string Track2, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardBalanceInquiry", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        CashierNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardBalanceInquiry(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardCredit", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("GiftCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardCredit(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardRefund", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardRefund(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardRefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardRefund(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string SalesTaxAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardRefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardRefund(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardCapture", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("GiftCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardCapture(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardSettle", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardSettle(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardSettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardSettle(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string SalesTaxAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GiftCardSettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardSettle(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardAuthorize", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount, 
                    out string ApprovedAmount) {
            object[] results = this.Invoke("GiftCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            BalanceAmount = ((string)(results[8]));
            ApprovedAmount = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("GiftCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardAuthorize(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime, out string BalanceAmount, out string ApprovedAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            BalanceAmount = ((string)(results[8]));
            ApprovedAmount = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.GiftCardCharge", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int GiftCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string BalanceAmount) {
            object[] results = this.Invoke("GiftCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGiftCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("GiftCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndGiftCardCharge(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string BalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            BalanceAmount = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.BenefitsBalanceInquiry", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int BenefitsBalanceInquiry(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string CashierNumber, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string FoodStampBalanceAmount, 
                    out string CashBenefitBalanceAmount) {
            object[] results = this.Invoke("BenefitsBalanceInquiry", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        CashierNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            FoodStampBalanceAmount = ((string)(results[6]));
            CashBenefitBalanceAmount = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginBenefitsBalanceInquiry(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string CashierNumber, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track2, string PINData, string KeySerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("BenefitsBalanceInquiry", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        CashierNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndBenefitsBalanceInquiry(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime, out string FoodStampBalanceAmount, out string CashBenefitBalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            FoodStampBalanceAmount = ((string)(results[6]));
            CashBenefitBalanceAmount = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CashBenefitCharge", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CashBenefitCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string CashBackAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string FoodStampBalanceAmount, 
                    out string CashBenefitBalanceAmount) {
            object[] results = this.Invoke("CashBenefitCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        CashBackAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCashBenefitCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string CashBackAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CashBenefitCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        CashBackAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCashBenefitCharge(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime, out string FoodStampBalanceAmount, out string CashBenefitBalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.FoodStampVoucher", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
        public int FoodStampVoucher(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string ApprovalNumber, 
                    string VoucherNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("FoodStampVoucher", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        ApprovalNumber,
                        VoucherNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginFoodStampVoucher(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string ApprovalNumber, string VoucherNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("FoodStampVoucher", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        ApprovalNumber,
                        VoucherNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndFoodStampVoucher(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.FoodStampCredit", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int FoodStampCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string FoodStampBalanceAmount, 
                    out string CashBenefitBalanceAmount) {
            object[] results = this.Invoke("FoodStampCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginFoodStampCredit(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track2, string PINData, string KeySerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("FoodStampCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndFoodStampCredit(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime, out string FoodStampBalanceAmount, out string CashBenefitBalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.FoodStampCharge", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int FoodStampCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string FoodStampBalanceAmount, 
                    out string CashBenefitBalanceAmount) {
            object[] results = this.Invoke("FoodStampCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginFoodStampCharge(string StoreID, string StoreKey, string ApplicationID, string TerminalID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string AccountNumber, string ExpirationMonth, string ExpirationYear, string Track2, string PINData, string KeySerialNumber, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("FoodStampCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track2,
                        PINData,
                        KeySerialNumber}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndFoodStampCharge(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime, out string FoodStampBalanceAmount, out string CashBenefitBalanceAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            FoodStampBalanceAmount = ((string)(results[7]));
            CashBenefitBalanceAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitSessionKeyChange", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitSessionKeyChange(string StoreID, string StoreKey, string ApplicationID, string TerminalID, out string ResultCode, out string ResultMessage, out string SessionKey) {
            object[] results = this.Invoke("DebitSessionKeyChange", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            SessionKey = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitSessionKeyChange(string StoreID, string StoreKey, string ApplicationID, string TerminalID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("DebitSessionKeyChange", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitSessionKeyChange(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string SessionKey) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            SessionKey = ((string)(results[3]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitCardCredit", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("DebitCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("DebitCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitCardCredit(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitCardCapture", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("DebitCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("DebitCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitCardCapture(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitCardAuthorize", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string ApprovedAmount) {
            object[] results = this.Invoke("DebitCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            ApprovedAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("DebitCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitCardAuthorize(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime, out string ApprovedAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            ApprovedAmount = ((string)(results[8]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitCardChargePINless", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitCardChargePINless(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("DebitCardChargePINless", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitCardChargePINless(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("DebitCardChargePINless", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitCardChargePINless(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.DebitCardCharge", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int DebitCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("DebitCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginDebitCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string CashBackAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string Track1, 
                    string Track2, 
                    string PINData, 
                    string KeySerialNumber, 
                    string SignatureData, 
                    string SignatureAlgorithm, 
                    string PrivateData, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("DebitCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        CashBackAmount,
                        Currency,
                        AccountNumber,
                        Track1,
                        Track2,
                        PINData,
                        KeySerialNumber,
                        SignatureData,
                        SignatureAlgorithm,
                        PrivateData,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndDebitCardCharge(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            AuthorizationNumber = ((string)(results[5]));
            TransactionDate = ((string)(results[6]));
            TransactionTime = ((string)(results[7]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardAddressVerify", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardAddressVerify(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string AVSResponseCode, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardAddressVerify", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardAddressVerify(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardAddressVerify", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardAddressVerify(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string AVSResponseCode, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.ProcessAuthenticationResponse", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int ProcessAuthenticationResponse(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TransactionID, 
                    string AuthenticationResponseToken, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string AVSResponseCode, 
                    out string CVVResponseCode, 
                    out string AuthenticationResponseCode, 
                    out string AuthenticationMethodName, 
                    out string AuthenticationMethodVersion, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime, 
                    out string ReferenceNumber, 
                    out string AccountNumber, 
                    out string TransactionAmount) {
            object[] results = this.Invoke("ProcessAuthenticationResponse", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TransactionID,
                        AuthenticationResponseToken});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            AVSResponseCode = ((string)(results[3]));
            CVVResponseCode = ((string)(results[4]));
            AuthenticationResponseCode = ((string)(results[5]));
            AuthenticationMethodName = ((string)(results[6]));
            AuthenticationMethodVersion = ((string)(results[7]));
            ApprovalNumber = ((string)(results[8]));
            AuthorizationNumber = ((string)(results[9]));
            TransactionDate = ((string)(results[10]));
            TransactionTime = ((string)(results[11]));
            ReferenceNumber = ((string)(results[12]));
            AccountNumber = ((string)(results[13]));
            TransactionAmount = ((string)(results[14]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginProcessAuthenticationResponse(string StoreID, string StoreKey, string ApplicationID, string TransactionID, string AuthenticationResponseToken, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ProcessAuthenticationResponse", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TransactionID,
                        AuthenticationResponseToken}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndProcessAuthenticationResponse(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string AVSResponseCode, out string CVVResponseCode, out string AuthenticationResponseCode, out string AuthenticationMethodName, out string AuthenticationMethodVersion, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime, out string ReferenceNumber, out string AccountNumber, out string TransactionAmount) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            AVSResponseCode = ((string)(results[3]));
            CVVResponseCode = ((string)(results[4]));
            AuthenticationResponseCode = ((string)(results[5]));
            AuthenticationMethodName = ((string)(results[6]));
            AuthenticationMethodVersion = ((string)(results[7]));
            ApprovalNumber = ((string)(results[8]));
            AuthorizationNumber = ((string)(results[9]));
            TransactionDate = ((string)(results[10]));
            TransactionTime = ((string)(results[11]));
            ReferenceNumber = ((string)(results[12]));
            AccountNumber = ((string)(results[13]));
            TransactionAmount = ((string)(results[14]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardAuthorizeWithAuthentication", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardAuthorizeWithAuthentication(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string AuthenticationResponseToken, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string AVSResponseCode, 
                    out string CVVResponseCode, 
                    out string AuthenticationResponseCode, 
                    out string AuthenticationMethodName, 
                    out string AuthenticationMethodVersion, 
                    out string AuthenticationRequestToken, 
                    out string AuthenticationServerURL, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardAuthorizeWithAuthentication", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        AuthenticationResponseToken,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            AuthenticationResponseCode = ((string)(results[6]));
            AuthenticationMethodName = ((string)(results[7]));
            AuthenticationMethodVersion = ((string)(results[8]));
            AuthenticationRequestToken = ((string)(results[9]));
            AuthenticationServerURL = ((string)(results[10]));
            ApprovalNumber = ((string)(results[11]));
            AuthorizationNumber = ((string)(results[12]));
            TransactionDate = ((string)(results[13]));
            TransactionTime = ((string)(results[14]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardAuthorizeWithAuthentication(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string AuthenticationResponseToken, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardAuthorizeWithAuthentication", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        AuthenticationResponseToken,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardAuthorizeWithAuthentication(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string AVSResponseCode, out string CVVResponseCode, out string AuthenticationResponseCode, out string AuthenticationMethodName, out string AuthenticationMethodVersion, out string AuthenticationRequestToken, out string AuthenticationServerURL, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            AuthenticationResponseCode = ((string)(results[6]));
            AuthenticationMethodName = ((string)(results[7]));
            AuthenticationMethodVersion = ((string)(results[8]));
            AuthenticationRequestToken = ((string)(results[9]));
            AuthenticationServerURL = ((string)(results[10]));
            ApprovalNumber = ((string)(results[11]));
            AuthorizationNumber = ((string)(results[12]));
            TransactionDate = ((string)(results[13]));
            TransactionTime = ((string)(results[14]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardVoiceAuthorize", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardVoiceAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardVoiceAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardVoiceAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardVoiceAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardVoiceAuthorize(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardCredit", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardCredit(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardCredit", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardCredit(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardRefund", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardRefund(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardRefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardRefund(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string SalesTaxAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("CreditCardRefund", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardRefund(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardCapture", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
        [Log4NetExtension("EFSnet2")]
        public int CreditCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardCapture(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string AuthorizationNumber, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardCapture", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        AuthorizationNumber,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardCapture(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            TransactionDate = ((string)(results[4]));
            TransactionTime = ((string)(results[5]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardSettle", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardSettle(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string OriginalTransactionID, 
                    string OriginalTransactionAmount, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string ApprovalNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardSettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardSettle(string StoreID, string StoreKey, string ApplicationID, string ReferenceNumber, string CashierNumber, string TransactionAmount, string SalesTaxAmount, string OriginalTransactionID, string OriginalTransactionAmount, string ClientIPAddress, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("CreditCardSettle", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        OriginalTransactionID,
                        OriginalTransactionAmount,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardSettle(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string ApprovalNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            ApprovalNumber = ((string)(results[4]));
            TransactionDate = ((string)(results[5]));
            TransactionTime = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardCharge", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
        public int CreditCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string AVSResponseCode, 
                    out string CVVResponseCode, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            ApprovalNumber = ((string)(results[6]));
            AuthorizationNumber = ((string)(results[7]));
            TransactionDate = ((string)(results[8]));
            TransactionTime = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardCharge(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardCharge", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardCharge(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string AVSResponseCode, out string CVVResponseCode, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            ApprovalNumber = ((string)(results[6]));
            AuthorizationNumber = ((string)(results[7]));
            TransactionDate = ((string)(results[8]));
            TransactionTime = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.CreditCardAuthorize", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int CreditCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    out string ResultCode, 
                    out string ResultMessage, 
                    out string TransactionID, 
                    out string AVSResponseCode, 
                    out string CVVResponseCode, 
                    out string ApprovalNumber, 
                    out string AuthorizationNumber, 
                    out string TransactionDate, 
                    out string TransactionTime) {
            object[] results = this.Invoke("CreditCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            ApprovalNumber = ((string)(results[6]));
            AuthorizationNumber = ((string)(results[7]));
            TransactionDate = ((string)(results[8]));
            TransactionTime = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginCreditCardAuthorize(
                    string StoreID, 
                    string StoreKey, 
                    string ApplicationID, 
                    string TerminalID, 
                    string ReferenceNumber, 
                    string CashierNumber, 
                    string TransactionAmount, 
                    string SalesTaxAmount, 
                    string Currency, 
                    string AccountNumber, 
                    string ExpirationMonth, 
                    string ExpirationYear, 
                    string CardVerificationValue, 
                    string Track1, 
                    string Track2, 
                    string BillingName, 
                    string BillingAddress, 
                    string BillingCity, 
                    string BillingState, 
                    string BillingPostalCode, 
                    string BillingCountry, 
                    string BillingPhone, 
                    string BillingEmail, 
                    string ShippingName, 
                    string ShippingAddress, 
                    string ShippingCity, 
                    string ShippingState, 
                    string ShippingPostalCode, 
                    string ShippingCountry, 
                    string ShippingPhone, 
                    string ShippingEmail, 
                    string ClientIPAddress, 
                    System.AsyncCallback callback, 
                    object asyncState) {
            return this.BeginInvoke("CreditCardAuthorize", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        TerminalID,
                        ReferenceNumber,
                        CashierNumber,
                        TransactionAmount,
                        SalesTaxAmount,
                        Currency,
                        AccountNumber,
                        ExpirationMonth,
                        ExpirationYear,
                        CardVerificationValue,
                        Track1,
                        Track2,
                        BillingName,
                        BillingAddress,
                        BillingCity,
                        BillingState,
                        BillingPostalCode,
                        BillingCountry,
                        BillingPhone,
                        BillingEmail,
                        ShippingName,
                        ShippingAddress,
                        ShippingCity,
                        ShippingState,
                        ShippingPostalCode,
                        ShippingCountry,
                        ShippingPhone,
                        ShippingEmail,
                        ClientIPAddress}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndCreditCardAuthorize(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage, out string TransactionID, out string AVSResponseCode, out string CVVResponseCode, out string ApprovalNumber, out string AuthorizationNumber, out string TransactionDate, out string TransactionTime) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            TransactionID = ((string)(results[3]));
            AVSResponseCode = ((string)(results[4]));
            CVVResponseCode = ((string)(results[5]));
            ApprovalNumber = ((string)(results[6]));
            AuthorizationNumber = ((string)(results[7]));
            TransactionDate = ((string)(results[8]));
            TransactionTime = ((string)(results[9]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.RequestPassThrough", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int RequestPassThrough(string StoreID, string StoreKey, string ApplicationID, string RequestMessage, out string TransactionID, out string ReplyMessage) {
            object[] results = this.Invoke("RequestPassThrough", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        RequestMessage});
            TransactionID = ((string)(results[1]));
            ReplyMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginRequestPassThrough(string StoreID, string StoreKey, string ApplicationID, string RequestMessage, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("RequestPassThrough", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID,
                        RequestMessage}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndRequestPassThrough(System.IAsyncResult asyncResult, out string TransactionID, out string ReplyMessage) {
            object[] results = this.EndInvoke(asyncResult);
            TransactionID = ((string)(results[1]));
            ReplyMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("http://tempuri.org/action/EFSnet.SystemCheck", RequestNamespace="http://tempuri.org/message/", ResponseNamespace="http://tempuri.org/message/")]
        [return: System.Xml.Serialization.SoapElementAttribute("Result")]
	[Log4NetExtension("EFSnet2")]
	public int SystemCheck(string StoreID, string StoreKey, string ApplicationID, out string ResultCode, out string ResultMessage) {
            object[] results = this.Invoke("SystemCheck", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID});
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSystemCheck(string StoreID, string StoreKey, string ApplicationID, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SystemCheck", new object[] {
                        StoreID,
                        StoreKey,
                        ApplicationID}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndSystemCheck(System.IAsyncResult asyncResult, out string ResultCode, out string ResultMessage) {
            object[] results = this.EndInvoke(asyncResult);
            ResultCode = ((string)(results[1]));
            ResultMessage = ((string)(results[2]));
            return ((int)(results[0]));
        }
    }
}
