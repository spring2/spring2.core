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

        public StringType CountryCode {
            get { return countryCode; }
            set { countryCode = value; }
	}

    }
}
