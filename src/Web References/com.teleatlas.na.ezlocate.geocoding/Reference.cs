﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
// 
#pragma warning disable 1591

namespace Spring2.Core.com.teleatlas.na.ezlocate.geocoding {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="GeocodingBinding", Namespace="http://ezlocate.na.teleatlas.com/Geocoding.wsdl")]
    public partial class Geocoding : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getServicesOperationCompleted;
        
        private System.Threading.SendOrPostCallback getServiceDescriptionOperationCompleted;
        
        private System.Threading.SendOrPostCallback findAddressOperationCompleted;
        
        private System.Threading.SendOrPostCallback findMultiAddressOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Geocoding() {
            this.Url = "http://mmezl.teleatlas.com/axis/services/Geocoding";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getServicesCompletedEventHandler getServicesCompleted;
        
        /// <remarks/>
        public event getServiceDescriptionCompletedEventHandler getServiceDescriptionCompleted;
        
        /// <remarks/>
        public event findAddressCompletedEventHandler findAddressCompleted;
        
        /// <remarks/>
        public event findMultiAddressCompletedEventHandler findMultiAddressCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Geocoding:GeocodingPortType#getServices", RequestNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", ResponseNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("resultCode", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int getServices([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int identity, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("nv", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out NameValue[] services) {
            object[] results = this.Invoke("getServices", new object[] {
                        identity});
            services = ((NameValue[])(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetServices(int identity, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getServices", new object[] {
                        identity}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndgetServices(System.IAsyncResult asyncResult, out NameValue[] services) {
            object[] results = this.EndInvoke(asyncResult);
            services = ((NameValue[])(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getServicesAsync(int identity) {
            this.getServicesAsync(identity, null);
        }
        
        /// <remarks/>
        public void getServicesAsync(int identity, object userState) {
            if ((this.getServicesOperationCompleted == null)) {
                this.getServicesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetServicesOperationCompleted);
            }
            this.InvokeAsync("getServices", new object[] {
                        identity}, this.getServicesOperationCompleted, userState);
        }
        
        private void OngetServicesOperationCompleted(object arg) {
            if ((this.getServicesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getServicesCompleted(this, new getServicesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Geocoding:GeocodingPortType#getServiceDescription", RequestNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", ResponseNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("resultCode", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int getServiceDescription([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string service, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int identity, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out string description, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out string countryCode, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("nv", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out NameValue[] inputs, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("fields", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out OutputField[] outputs, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("types", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out MatchType[] matchTypes, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out string matchTypeName) {
            object[] results = this.Invoke("getServiceDescription", new object[] {
                        service,
                        identity});
            description = ((string)(results[1]));
            countryCode = ((string)(results[2]));
            inputs = ((NameValue[])(results[3]));
            outputs = ((OutputField[])(results[4]));
            matchTypes = ((MatchType[])(results[5]));
            matchTypeName = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BegingetServiceDescription(string service, int identity, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("getServiceDescription", new object[] {
                        service,
                        identity}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndgetServiceDescription(System.IAsyncResult asyncResult, out string description, out string countryCode, out NameValue[] inputs, out OutputField[] outputs, out MatchType[] matchTypes, out string matchTypeName) {
            object[] results = this.EndInvoke(asyncResult);
            description = ((string)(results[1]));
            countryCode = ((string)(results[2]));
            inputs = ((NameValue[])(results[3]));
            outputs = ((OutputField[])(results[4]));
            matchTypes = ((MatchType[])(results[5]));
            matchTypeName = ((string)(results[6]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getServiceDescriptionAsync(string service, int identity) {
            this.getServiceDescriptionAsync(service, identity, null);
        }
        
        /// <remarks/>
        public void getServiceDescriptionAsync(string service, int identity, object userState) {
            if ((this.getServiceDescriptionOperationCompleted == null)) {
                this.getServiceDescriptionOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetServiceDescriptionOperationCompleted);
            }
            this.InvokeAsync("getServiceDescription", new object[] {
                        service,
                        identity}, this.getServiceDescriptionOperationCompleted, userState);
        }
        
        private void OngetServiceDescriptionOperationCompleted(object arg) {
            if ((this.getServiceDescriptionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getServiceDescriptionCompleted(this, new getServiceDescriptionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Geocoding:GeocodingPortType#findAddress", RequestNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", ResponseNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("resultCode", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int findAddress([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string service, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("nv", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] NameValue[] input, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int identity, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out Geocode result) {
            object[] results = this.Invoke("findAddress", new object[] {
                        service,
                        input,
                        identity});
            result = ((Geocode)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginfindAddress(string service, NameValue[] input, int identity, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("findAddress", new object[] {
                        service,
                        input,
                        identity}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndfindAddress(System.IAsyncResult asyncResult, out Geocode result) {
            object[] results = this.EndInvoke(asyncResult);
            result = ((Geocode)(results[1]));
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void findAddressAsync(string service, NameValue[] input, int identity) {
            this.findAddressAsync(service, input, identity, null);
        }
        
        /// <remarks/>
        public void findAddressAsync(string service, NameValue[] input, int identity, object userState) {
            if ((this.findAddressOperationCompleted == null)) {
                this.findAddressOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindAddressOperationCompleted);
            }
            this.InvokeAsync("findAddress", new object[] {
                        service,
                        input,
                        identity}, this.findAddressOperationCompleted, userState);
        }
        
        private void OnfindAddressOperationCompleted(object arg) {
            if ((this.findAddressCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findAddressCompleted(this, new findAddressCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("Geocoding:GeocodingPortType#findMultiAddress", RequestNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", ResponseNamespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("resultCode", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int findMultiAddress([System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] string service, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("record", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("nv", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, NestingLevel=1)] NameValue[][] inputs, [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] int identity, [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] [System.Xml.Serialization.XmlArrayItemAttribute("sequence", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)] out Geocode[] results) {
            object[] results1 = this.Invoke("findMultiAddress", new object[] {
                        service,
                        inputs,
                        identity});
            results = ((Geocode[])(results1[1]));
            return ((int)(results1[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginfindMultiAddress(string service, NameValue[][] inputs, int identity, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("findMultiAddress", new object[] {
                        service,
                        inputs,
                        identity}, callback, asyncState);
        }
        
        /// <remarks/>
        public int EndfindMultiAddress(System.IAsyncResult asyncResult, out Geocode[] results) {
            object[] results1 = this.EndInvoke(asyncResult);
            results = ((Geocode[])(results1[1]));
            return ((int)(results1[0]));
        }
        
        /// <remarks/>
        public void findMultiAddressAsync(string service, NameValue[][] inputs, int identity) {
            this.findMultiAddressAsync(service, inputs, identity, null);
        }
        
        /// <remarks/>
        public void findMultiAddressAsync(string service, NameValue[][] inputs, int identity, object userState) {
            if ((this.findMultiAddressOperationCompleted == null)) {
                this.findMultiAddressOperationCompleted = new System.Threading.SendOrPostCallback(this.OnfindMultiAddressOperationCompleted);
            }
            this.InvokeAsync("findMultiAddress", new object[] {
                        service,
                        inputs,
                        identity}, this.findMultiAddressOperationCompleted, userState);
        }
        
        private void OnfindMultiAddressOperationCompleted(object arg) {
            if ((this.findMultiAddressCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.findMultiAddressCompleted(this, new findMultiAddressCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1")]
    public partial class NameValue {
        
        private string nameField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1")]
    public partial class Geocode {
        
        private int resultCodeField;
        
        private NameValue[] mAttributesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int resultCode {
            get {
                return this.resultCodeField;
            }
            set {
                this.resultCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("nv", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public NameValue[] mAttributes {
            get {
                return this.mAttributesField;
            }
            set {
                this.mAttributesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1")]
    public partial class MatchType {
        
        private string nameField;
        
        private string descriptionField;
        
        private int idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ezlocate.na.teleatlas.com/Geocoding.xsd1")]
    public partial class OutputField {
        
        private string nameField;
        
        private string descriptionField;
        
        private int typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public string description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getServicesCompletedEventHandler(object sender, getServicesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getServicesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getServicesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public NameValue[] services {
            get {
                this.RaiseExceptionIfNecessary();
                return ((NameValue[])(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getServiceDescriptionCompletedEventHandler(object sender, getServiceDescriptionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getServiceDescriptionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getServiceDescriptionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string description {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string countryCode {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
        
        /// <remarks/>
        public NameValue[] inputs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((NameValue[])(this.results[3]));
            }
        }
        
        /// <remarks/>
        public OutputField[] outputs {
            get {
                this.RaiseExceptionIfNecessary();
                return ((OutputField[])(this.results[4]));
            }
        }
        
        /// <remarks/>
        public MatchType[] matchTypes {
            get {
                this.RaiseExceptionIfNecessary();
                return ((MatchType[])(this.results[5]));
            }
        }
        
        /// <remarks/>
        public string matchTypeName {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[6]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void findAddressCompletedEventHandler(object sender, findAddressCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findAddressCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal findAddressCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public Geocode result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Geocode)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void findMultiAddressCompletedEventHandler(object sender, findMultiAddressCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class findMultiAddressCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results1;
        
        internal findMultiAddressCompletedEventArgs(object[] results1, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results1 = results1;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results1[0]));
            }
        }
        
        /// <remarks/>
        public Geocode[] results {
            get {
                this.RaiseExceptionIfNecessary();
                return ((Geocode[])(this.results1[1]));
            }
        }
    }
}

#pragma warning restore 1591