using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;

using Spring2.Core.Publication.DataObject;
using Spring2.Core.Types;



namespace Spring2.Core.Publication.DataObject {

    public class PublicationTrackingData : Spring2.Core.DataObject.DataObject {

	public static readonly PublicationTrackingData DEFAULT = new PublicationTrackingData();

	private IdType publicationPrimaryKeyId = IdType.DEFAULT;
	private IdType publicationTypeId = IdType.DEFAULT;

	public IdType PublicationPrimaryKeyId {
	    get { return this.publicationPrimaryKeyId; }
	    set { this.publicationPrimaryKeyId = value; }
	}

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
