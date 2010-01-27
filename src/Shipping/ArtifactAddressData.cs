using System;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public class ArtifactAddressData {

	private StringType consignee = StringType.DEFAULT;
	private StringType streetNumberLow = StringType.DEFAULT;
	private StringType streetName = StringType.DEFAULT;
	private StringType streetType = StringType.DEFAULT;
	private StringType politicalDivision2 = StringType.DEFAULT;
	private StringType politicalDivision1 = StringType.DEFAULT;
        private StringType postcodePrimaryLow = StringType.DEFAULT;
	private StringType postcodeExtendedLow = StringType.DEFAULT;
	private StringType countryCode = StringType.DEFAULT;

	public StringType Consignee {
	    get { return consignee; }
	    set { consignee = value; }
	}

	public StringType StreetNumberLow {
	    get { return streetNumberLow; }
	    set { streetNumberLow = value; }
	}

        public StringType StreetName {
            get { return streetName; }
            set { streetName = value; }
	}

        public StringType StreetType {
            get { return streetType; }
            set { streetType = value; }
	}

        public StringType PoliticalDivision2 {
            get { return politicalDivision2; }
            set { politicalDivision2 = value; }
	}

        public StringType PoliticalDivision1 {
            get { return politicalDivision1; }
            set { politicalDivision1 = value; }
	}

        public StringType PostcodePrimaryLow {
            get { return postcodePrimaryLow; }
            set { postcodePrimaryLow = value; }
        }

        public StringType PostcodeExtendedLow {
            get { return postcodeExtendedLow; }
            set { postcodeExtendedLow = value; }
        }

        public CountryCodeEnum SetCountryCode {
            get { return CovertedThreeCharCode(countryCode); }
            set { countryCode = ConvertCountryCode(value); }
        }

        public StringType CountryCode {
            get { return countryCode; }
            set { countryCode = value; }
	}

        private CountryCodeEnum CovertedThreeCharCode(StringType twoCharCode) {
            switch (twoCharCode.ToString()) { 
                case "US":
                    return CountryCodeEnum.UNITED_STATES;
                case "CA":
                    return CountryCodeEnum.CANADA;
                default:
                    return CountryCodeEnum.UNITED_STATES;
                
            }
        }

        private StringType ConvertCountryCode(CountryCodeEnum threeCharCountry) {
            switch (threeCharCountry.Code) { 
                case "ABW":
                    return StringType.Parse("AW");
                case "AFG":
                    return StringType.Parse("AF");
                case "AGO":
                    return StringType.Parse("AO");
                case "AIA":
                    return StringType.Parse("AI");
                case "ALA":
                    return StringType.Parse("AX");
                case "ALB":
                    return StringType.Parse("AL");
                case "AND":
                    return StringType.Parse("AD");
                case "ANT":
                    return StringType.Parse("AN");
                case "ARE":
                    return StringType.Parse("AE");
                case "ARG":
                    return StringType.Parse("AR");
                case "ARM":
                    return StringType.Parse("AM");
                case "CAN":
                    return StringType.Parse("CA");
                case "USA":
                    return StringType.Parse("US");
                default:
                    return StringType.Parse("US");
            }
        }
    }
}
