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
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Spring2.Dss.Tax.Vertex {
	/// <remarks/>
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[WebServiceBinding(Name="LookupTaxAreasStringSoapBinding", Namespace="http://elbrus:8080/vertex-ws/services/LookupTaxAreasString")]
	public class LookupTaxAreasWSService : SoapHttpClientProtocol {
		/// <remarks/>
		public LookupTaxAreasWSService() {
			this.Url = "http://localhost:8080/vertex-ws/services/LookupTaxAreasString";
		}

		/// <remarks/>
		[SoapDocumentMethod("", Use=SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Bare)]
		[return : XmlElement("inStringReturn", Namespace="http://axis.webservices.vertexinc.com")]
		public string lookupTaxAreasString([XmlElement(Namespace="http://axis.webservices.vertexinc.com")] string inString) {
			object[] results = this.Invoke("lookupTaxAreasString", new object[] {
				inString
			});
			return ((string) (results[0]));
		}

		/// <remarks/>
		public IAsyncResult BeginlookupTaxAreasString(string inString, AsyncCallback callback, object asyncState) {
			return this.BeginInvoke("lookupTaxAreasString", new object[] {
				inString
			}, callback, asyncState);
		}

		/// <remarks/>
		public string EndlookupTaxAreasString(IAsyncResult asyncResult) {
			object[] results = this.EndInvoke(asyncResult);
			return ((string) (results[0]));
		}
	}
}