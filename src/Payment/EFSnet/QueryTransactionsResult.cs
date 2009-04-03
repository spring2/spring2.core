using System;

namespace Spring2.Core.Payment.EFSNet {
    //------------------------------------------------------------------------------
    // <autogenerated>
    //     This code was generated by a tool.
    //     Runtime Version: 1.1.4322.2032
    //
    //     Changes to this file may cause incorrect behavior and will be lost if 
    //     the code is regenerated.
    // </autogenerated>
    //------------------------------------------------------------------------------

    // 
    // This source code was auto-generated by xsd, Version=1.1.4322.2032.
    // 
    using System.Xml.Serialization;


    /// <remarks/>
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class QueryTransactionsResult {
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("Transaction", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public QueryTransactionsResultTransaction[] Transaction;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string SessionCookie;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string MaxCount;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string StoreID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string QueryTime;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlAttributeAttribute()]
	public string QueryDate;
    }

    /// <remarks/>
    public class QueryTransactionsResultTransaction {
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string RequestID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string StoreID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ApplicationID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string HostID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string MerchantID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TerminalID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TerminalType;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ProcessorID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionType;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionStatus;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionDate;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionTime;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ReferenceNumber;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string AccountNumber;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string EntryMode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string CVVPresenceIndicator;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string CardLogo;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string TransactionAmount;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string BillingName;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string BillingAddress;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string BillingPostalCode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ServerIPAddress;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string AuthorizationNumber;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ResponseCode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ResultCode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ResultMessage;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string AVSResponseCode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string CVVResponseCode;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string ApprovalNumber;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string SettlementDate;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string MessageFormat;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string OriginalTransactionID;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string OriginalTransactionAmount;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string SalesTaxAmount;
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
	public string BillingState;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public class NewDataSet {
    
	/// <remarks/>
	[System.Xml.Serialization.XmlElementAttribute("QueryTransactionsResult")]
	public QueryTransactionsResult[] Items;
    }

}