﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=1.1.4322.573.
// 
using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;

namespace Spring2.Core.Tax.Vertex {

    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CalculateTaxStringSoapBinding", Namespace="http://elbrus:8080/vertex-ws/services/CalculateTaxString")]
    public class CalculateTaxWSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
    
	/// <remarks/>
	public CalculateTaxWSService() {
	    this.Url = "http://localhost:8080/vertex-ws/services/CalculateTaxString";
	}
    
	/// <remarks/>
	[System.Web.Services.Protocols.SoapDocumentMethodAttribute("", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
	[return: System.Xml.Serialization.XmlElementAttribute("inStringReturn", Namespace="http://axis.webservices.vertexinc.com")]
	[Spring2.Core.Soap.Log4NetExtension("CalculateTax")]
	public string calculateTaxString([System.Xml.Serialization.XmlElementAttribute(Namespace="http://axis.webservices.vertexinc.com")] string inString) {
	    object[] results = this.Invoke("calculateTaxString", new object[] {
										  inString});
	    return ((string)(results[0]));
	}
    
	/// <remarks/>
	public System.IAsyncResult BegincalculateTaxString(string inString, System.AsyncCallback callback, object asyncState) {
	    return this.BeginInvoke("calculateTaxString", new object[] {
									   inString}, callback, asyncState);
	}
    
	/// <remarks/>
	public string EndcalculateTaxString(System.IAsyncResult asyncResult) {
	    object[] results = this.EndInvoke(asyncResult);
	    return ((string)(results[0]));
	}
    }
}