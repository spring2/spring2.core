using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.CommunicationSubscription.DataObject;
using Spring2.Core.DAO;
using Spring2.Core.Types;





namespace Spring2.Core.CommunicationSubscription.DataObject {

    public class PublicationTrackingData : Spring2.Core.DataObject.DataObject {

	public static readonly PublicationTrackingData DEFAULT = new PublicationTrackingData();

	private IdType publicationTypeId = IdType.DEFAULT;

	public IdType PublicationTypeId {
	    get { return this.publicationTypeId; }
	    set { this.publicationTypeId = value; }
	}

	public Boolean IsDefault {
	    get {
		return Object.ReferenceEquals(DEFAULT, this);
	    }
        }
    }
}
