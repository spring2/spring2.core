using System;
using Spring2.Core.Types;

namespace Spring2.Core.AddressValidation {
    public class AddressValidationResult {
	private ResponseTypeEnum responseType = ResponseTypeEnum.DEFAULT;
	private AddressList addresses = AddressList.DEFAULT;
	private StringType responseXml = StringType.EMPTY;
	
	public ResponseTypeEnum ResponseType {
	    get { return responseType; }
	    set { responseType = value; }
	}

	public AddressList Addresses {
	    get { return addresses; }
	    set { addresses = value; }
	}

	public StringType ResponseXml {
	    get { return responseXml; }
	    set { responseXml = value; }
	}
    }
}
