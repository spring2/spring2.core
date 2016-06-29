using System.Collections.Generic;
using Spring2.Core.PostageService.Stamps;
using Spring2.Core.Types;
using System.Text;

namespace Spring2.Core.AddressValidation.Stamps {
    public class StampsAddressValidationProvider : IAddressValidationProvider {
	protected string integrationId;
	protected string password;
	protected string username;
	protected string postageServerUrl;

	protected string fullName;

	protected string address1 = null;
	protected string address2 = null;
	protected string address3 = null;
	protected string city = null;
	protected string state = null;
	protected string postalCode = null;
	protected string country = null;


	public AddressValidationResult Validate(StringType street, StringType city, StringType state, StringType postalCode, StringType country) {
	    this.address1 = street;
	    this.city = city;
	    this.state = state;
	    this.postalCode = postalCode;
	    this.country = country;

	    return Execute();
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType city, StringType state, StringType postalCode, StringType country) {
	    this.address1 = street1;
	    this.address2 = street2;
	    this.city = city;
	    this.state = state;
	    this.postalCode = postalCode;
	    this.country = country;

	    return Execute();
	}

	public AddressValidationResult Validate(StringType street1, StringType street2, StringType street3, StringType city, StringType state, StringType postalCode, StringType country) {
	    this.address1 = street1;
	    this.address2 = street2;
	    this.address3 = street3;
	    this.city = city;
	    this.state = state;
	    this.postalCode = postalCode;
	    this.country = country;

	    return Execute();
	}
	public void SetCredentials(StringType integrationId, StringType password, StringType username, StringType postageServerUrl, StringType fullName) {
	    this.integrationId = integrationId;
	    this.password = password;
	    this.username = username;
	    this.postageServerUrl = postageServerUrl;
	    this.fullName = fullName;
	}

	private AddressValidationResult Execute() {
	    AddressValidationResult result = new AddressValidationResult();
	    StampsProvider provider = new StampsProvider(integrationId, password, username, postageServerUrl);
	    SWSIMV52.Address address = new SWSIMV52.Address() {
		Address1 = address1,
		Address2 = address2,
		Address3 = address3,
		City = city,
		State = state,
		ZIPCode = postalCode,
		Country = country,
		FullName = fullName
	    };
	    SWSIMV52.CleanseAddressResponse response = provider.CleanseAddress(address, null);
	    AddressList addressList = new AddressList();
	    if (response.Address != null) {
		addressList.Add(convertStampsAddress(response.Address));
	    } else {
		foreach (SWSIMV52.Address stampsAddress in response.CandidateAddresses) {
		    addressList.Add(convertStampsAddress(stampsAddress));
		}
	    }
	    result.Addresses = addressList;
	    if (response.AddressMatch) {
		result.ResponseType = ResponseTypeEnum.VALID;
	    } else if (response.CityStateZipOK) {
		result.ResponseType = ResponseTypeEnum.VALID_CITYSTATEZIP;
	    } else {
		result.ResponseType = ResponseTypeEnum.INVALID;
	    }
	    StringBuilder status = new StringBuilder();
	    status.Append("Return Code: ");
	    status.Append(returnCodeStatus[response.StatusCodes.ReturnCode]);
	    if (response.StatusCodes.Footnotes.Length > 0) {
		foreach(SWSIMV52.Footnote footnote in response.StatusCodes.Footnotes) {
		    status.Append(", Footnote: ");
		    status.Append(footnoteStatus[footnote.Value]);
		}
	    }
	    result.Status = status.ToString();
	    return result;
	}

	private AddressData convertStampsAddress(SWSIMV52.Address stampsAddress) {
	    return new AddressData() {
		City = stampsAddress.City,
		Country = stampsAddress.Country,
		PostalCode = stampsAddress.ZIPCode,
		State = stampsAddress.State,
		Street1 = stampsAddress.Address1,
		Street2 = stampsAddress.Address2,
		Street3 = stampsAddress.Address3
	    };
	}

	// The following dictionaries taken from USPS https://ribbs.usps.gov/amsapi/documents/tech_guides/AMSAPIUG.PDF for which the codes correspond
	private static Dictionary<IntegerType, StringType> returnCodeStatus = new Dictionary<IntegerType, StringType>() {
	    { 10, "Information presented could not be processed in current format. Corrective action is needed. Be sure that the address line components are correct. For example, the input address line may contain more than one delivery address. " },
	    { 11, "The ZIP Code in the submitted address could not be found because neither a valid city, state, nor valid 5digit ZIP Code was present. Corrective action is needed. It is also recommended that the requestor check the submitted address for accuracy. " },
	    { 12, "The state in the submitted address is invalid. Corrective action is needed. It is also recommended that the requestor check the submitted address for accuracy. " },
	    { 13, "The city in the submitted address is invalid. Corrective action is needed. It is also recommended that the requestor check the submitted address for accuracy. " },
	    { 21, "The address, exactly as submitted, could not be found in the national ZIP+4 file. It is recommended that the requestor check the submitted address for accuracy. For example, the street address line may be abbreviated excessively and may not be fully recognizable. " },
	    { 22, "More than one ZIP+4 Code was found to satisfy the address submitted. The submitted address did not contain sufficiently complete or correct data to determine a single ZIP+4 Code. It is recommended that the requestor check the address for accuracy and completeness. Address elements may be missing " },
	    { 31, "Single response based on input information. No corrective action is needed since an exact match was found in the national ZIP+4 file. " },
	    { 32, "A match was made to a default record in the national ZIP+4 file. A more specific match may be available if a secondary number (i.e., apartment, suite, etc.) exists. " }
	};

	private static Dictionary<StringType, StringType> footnoteStatus = new Dictionary<StringType, StringType>() {
	    { "A", "The address was found to have a different 5-digit ZIP Code than given in the submitted list. The correct ZIP Code is shown in the output address. " },
	    { "B", "The spelling of the city name and/or state abbreviation in the submitted address was found to be different than the standard spelling. The standard spelling of the city name and state abbreviation are shown in the output address. " },
	    { "C", "The ZIP Code in the submitted address could not be found because neither a valid city, state, nor valid 5digit ZIP Code was present. It is also recommended that the requestor check the submitted address for accuracy. " },
	    { "D", "This is a record listed by the United States Postal Service on the national ZIP+4 file as a non-deliverable location. It is recommended that the requestor verify the accuracy of the submitted address. "  },
	    { "E", "Multiple records were returned, but each shares the same 5-digit ZIP Code. " },
	    { "F", "The address, exactly as submitted, could not be found in the city, state, or ZIP Code provided. It is also recommended that the requestor check the submitted address for accuracy. For example, the street address line may be abbreviated excessively and may not be fully recognizable. " },
	    { "G", "Information in the firm line was determined to be a part of the address. It was moved out of the firm line and incorporated into the address line. " },
	    { "H", "ZIP+4 information indicates this address is a building. The address as submitted does not contain an apartment/suite number. It is recommended that the requestor check the submitted address and add the missing apartment or suite number to ensure the correct Delivery Point Barcode (DPBC)." },
	    { "I", "More than one ZIP+4 Code was found to satisfy the address as submitted. The submitted address did not contain sufficiently complete or correct data to determine a single ZIP+4 Code. It is recommended that the requestor check the address for accuracy and completeness. For example, firm name, or institution name, doctor�s name, suite number, apartment number, box number, floor number, etc. may be missing or incorrect. Also pre-directional or post-directional indicators (North = N, South = S, East = E, West = W, etc.) and/or street suffixes (Street = ST, Avenue = AVE, Road = RD, Circle = CIR, etc.) may be missing or incorrect. " },
	    { "J", "The input contained two addresses.  For example:   123 MAIN ST PO BOX 99. " },
	    { "K", "CASS rule does not allow a match when the cardinal point of a directional changes more than 90%." },
	    { "L", "An address component (i.e., directional or suffix only) was added, changed, or deleted in order to achieve a match. " },
	    { "M", "The spelling of the street name was changed in order to achieve a match." },
	    { "N", "The delivery address was standardized. For example, if STREET was in the delivery address, the system will return ST as its standard spelling. " },
	    { "O", "More than one ZIP+4 Code was found to satisfy the address as submitted.  The lowest ZIP +4 addon may  be used to break the tie between the records. " },
	    { "P", "The delivery address is matchable, but is known by another (preferred) name. For example, in New York, NY, AVENUE OF THE AMERICAS is also known as 6TH AVE. An inquiry using a delivery address of  55 AVE OF THE AMERICAS would be flagged with a Footnote Flag P. " },
	    { "Q", "Match to an address with a unique ZIP Code." },
	    { "R", "The delivery address is matchable, but the EWS file indicates that an exact match will be available soon." },
	    { "S", "The secondary information (i.e., floor, suite, apartment, or box number) does not match that on the national  ZIP+4 file. This secondary information, although present on the input address, was not valid in the range  found on the national ZIP+4 file. " },
	    { "T", "The search resulted in a single response; however, the record matched was flagged as having magnet street  syndrome. �Whenever an input address has a single suffix word or a single directional word as the street  name, or whenever the ZIP+4 File records being matched to have a single suffix word or a single  directional word as the street name field, then an exact match between the street, suffix and/or post- directional and the same components on the ZIP+4 File must occur before a match can be made. Adding,  changing or deleting a component from the input address to obtain a match to a ZIP+4 record will be  considered incorrect.� Instead of returning a �no match� in this situation a multiple response is returned to  allow access the candidate record. " },
	    { "U", "The city or post office name in the submitted address is not recognized by the United States Postal Service  as an official last line name (preferred city name), and is not acceptable as an alternate name. This does  denote an error and the preferred city name will be provided as output." },
	    { "V", "The city and state in the submitted address could not be verified as corresponding to the given 5-digit ZIP  Code. This comment does not necessarily denote an error; however, it is recommended that the requestor  check the city and state in the submitted address for accuracy. " },
	    { "W", "The input address record contains a delivery address other than a PO BOX, General Delivery, or  Postmaster with a 5-digit ZIP Code that is identified as a �small town default.� The United States Postal  Service does not provide street delivery for this ZIP Code. The United States Postal Service requires use of  a PO BOX, General Delivery, or Postmaster for delivery within this ZIP Code." },
	    { "X", "Default match inside a unique ZIP Code. " },
	    { "Y", "Match made to a record with a military ZIP Code. " },
	    { "Z", "The ZIPMOVE product shows which ZIP + 4 records have moved from one ZIP Code to another.  If an  input address matches to a ZIP + 4 record which the ZIPMOVE product indicates as having moved, the  search is performed again in the new ZIP Code. " }
	};
    }
}
