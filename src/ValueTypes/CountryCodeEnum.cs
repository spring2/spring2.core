using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Types {

    public class CountryCodeEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new CountryCodeEnum DEFAULT = new CountryCodeEnum();
	public static readonly new CountryCodeEnum UNSET = new CountryCodeEnum();

	public static readonly CountryCodeEnum ARUBA = new CountryCodeEnum("ABW", "Aruba");
	public static readonly CountryCodeEnum AFGHANISTAN = new CountryCodeEnum("AFG", "Afghanistan");
	public static readonly CountryCodeEnum ANGOLA = new CountryCodeEnum("AGO", "Angola");
	public static readonly CountryCodeEnum ANGUILLA = new CountryCodeEnum("AIA", "Anguilla");
	public static readonly CountryCodeEnum ALAND_ISLANDS = new CountryCodeEnum("ALA", "Aland Islands");
	public static readonly CountryCodeEnum ALBANIA = new CountryCodeEnum("ALB", "Albania");
	public static readonly CountryCodeEnum ANDORRA = new CountryCodeEnum("AND", "Andorra");
	public static readonly CountryCodeEnum NETHERLANDS_ANTILLES = new CountryCodeEnum("ANT", "Netherlands Antilles");
	public static readonly CountryCodeEnum UNITED_ARAB_EMIRATES = new CountryCodeEnum("ARE", "United Arab Emirates");
	public static readonly CountryCodeEnum ARGENTINA = new CountryCodeEnum("ARG", "Argentina");
	public static readonly CountryCodeEnum ARMENIA = new CountryCodeEnum("ARM", "Armenia");
	public static readonly CountryCodeEnum AMERICAN_SAMOA = new CountryCodeEnum("ASM", "American Samoa");
	public static readonly CountryCodeEnum ANTARCTICA = new CountryCodeEnum("ATA", "Antarctica");
	public static readonly CountryCodeEnum FRENCH_SOUTHERN_TERRITORIES = new CountryCodeEnum("ATF", "French Southern Territories");
	public static readonly CountryCodeEnum ANTIGUA_AND_BARBUDA = new CountryCodeEnum("ATG", "Antigua and Barbuda");
	public static readonly CountryCodeEnum AUSTRALIA = new CountryCodeEnum("AUS", "Australia");
	public static readonly CountryCodeEnum AUSTRIA = new CountryCodeEnum("AUT", "Austria");
	public static readonly CountryCodeEnum AZERBAIJAN = new CountryCodeEnum("AZE", "Azerbaijan");
	public static readonly CountryCodeEnum BURUNDI = new CountryCodeEnum("BDI", "Burundi");
	public static readonly CountryCodeEnum BELGIUM = new CountryCodeEnum("BEL", "Belgium");
	public static readonly CountryCodeEnum BENIN = new CountryCodeEnum("BEN", "Benin");
	public static readonly CountryCodeEnum BURKINA_FASO = new CountryCodeEnum("BFA", "Burkina Faso");
	public static readonly CountryCodeEnum BANGLADESH = new CountryCodeEnum("BGD", "Bangladesh");
	public static readonly CountryCodeEnum BULGARIA = new CountryCodeEnum("BGR", "Bulgaria");
	public static readonly CountryCodeEnum BAHRAIN = new CountryCodeEnum("BHR", "Bahrain");
	public static readonly CountryCodeEnum BAHAMAS = new CountryCodeEnum("BHS", "Bahamas");
	public static readonly CountryCodeEnum BOSNIA_AND_HERZEGOVINA = new CountryCodeEnum("BIH", "Bosnia and Herzegovina");
	public static readonly CountryCodeEnum SAINT_BARTHELEMY = new CountryCodeEnum("BLM", "Saint Barthelemy");
	public static readonly CountryCodeEnum BELARUS = new CountryCodeEnum("BLR", "Belarus");
	public static readonly CountryCodeEnum BELIZE = new CountryCodeEnum("BLZ", "Belize");
	public static readonly CountryCodeEnum BERMUDA = new CountryCodeEnum("BMU", "Bermuda");
	public static readonly CountryCodeEnum BOLIVIA_PLURINATIONAL_STATE_OF = new CountryCodeEnum("BOL", "Bolivia, Plurinational State of");
	public static readonly CountryCodeEnum BRAZIL = new CountryCodeEnum("BRA", "Brazil");
	public static readonly CountryCodeEnum BARBADOS = new CountryCodeEnum("BRB", "Barbados");
	public static readonly CountryCodeEnum BRUNEI_DARUSSALAM = new CountryCodeEnum("BRN", "Brunei Darussalam");
	public static readonly CountryCodeEnum BHUTAN = new CountryCodeEnum("BTN", "Bhutan");
	public static readonly CountryCodeEnum BOUVET_ISLAND = new CountryCodeEnum("BVT", "Bouvet Island");
	public static readonly CountryCodeEnum BOTSWANA = new CountryCodeEnum("BWA", "Botswana");
	public static readonly CountryCodeEnum CENTRAL_AFRICAN_REPUBLIC = new CountryCodeEnum("CAF", "Central African Republic");
	public static readonly CountryCodeEnum CANADA = new CountryCodeEnum("CAN", "Canada");
	public static readonly CountryCodeEnum COCOS_KEELING_ISLANDS = new CountryCodeEnum("CCK", "Cocos (Keeling) Islands");
	public static readonly CountryCodeEnum SWITZERLAND = new CountryCodeEnum("CHE", "Switzerland");
	public static readonly CountryCodeEnum CHILE = new CountryCodeEnum("CHL", "Chile");
	public static readonly CountryCodeEnum CHINA = new CountryCodeEnum("CHN", "China");
	public static readonly CountryCodeEnum COTE_DIVOIRE = new CountryCodeEnum("CIV", "Cote d'Ivoire");
	public static readonly CountryCodeEnum CAMEROON = new CountryCodeEnum("CMR", "Cameroon");
	public static readonly CountryCodeEnum CONGO_THE_DEMOCRATIC_REPUBLIC_OF_THE = new CountryCodeEnum("COD", "Congo, the Democratic Republic of the");
	public static readonly CountryCodeEnum CONGO = new CountryCodeEnum("COG", "Congo");
	public static readonly CountryCodeEnum COOK_ISLANDS = new CountryCodeEnum("COK", "Cook Islands");
	public static readonly CountryCodeEnum COLOMBIA = new CountryCodeEnum("COL", "Colombia");
	public static readonly CountryCodeEnum COMOROS = new CountryCodeEnum("COM", "Comoros");
	public static readonly CountryCodeEnum CAPE_VERDE = new CountryCodeEnum("CPV", "Cape Verde");
	public static readonly CountryCodeEnum COSTA_RICA = new CountryCodeEnum("CRI", "Costa Rica");
	public static readonly CountryCodeEnum CUBA = new CountryCodeEnum("CUB", "Cuba");
	public static readonly CountryCodeEnum CHRISTMAS_ISLAND = new CountryCodeEnum("CXR", "Christmas Island");
	public static readonly CountryCodeEnum CAYMAN_ISLANDS = new CountryCodeEnum("CYM", "Cayman Islands");
	public static readonly CountryCodeEnum CYPRUS = new CountryCodeEnum("CYP", "Cyprus");
	public static readonly CountryCodeEnum CZECH_REPUBLIC = new CountryCodeEnum("CZE", "Czech Republic");
	public static readonly CountryCodeEnum GERMANY = new CountryCodeEnum("DEU", "Germany");
	public static readonly CountryCodeEnum DJIBOUTI = new CountryCodeEnum("DJI", "Djibouti");
	public static readonly CountryCodeEnum DOMINICA = new CountryCodeEnum("DMA", "Dominica");
	public static readonly CountryCodeEnum DENMARK = new CountryCodeEnum("DNK", "Denmark");
	public static readonly CountryCodeEnum DOMINICAN_REPUBLIC = new CountryCodeEnum("DOM", "Dominican Republic");
	public static readonly CountryCodeEnum ALGERIA = new CountryCodeEnum("DZA", "Algeria");
	public static readonly CountryCodeEnum ECUADOR = new CountryCodeEnum("ECU", "Ecuador");
	public static readonly CountryCodeEnum EGYPT = new CountryCodeEnum("EGY", "Egypt");
	public static readonly CountryCodeEnum ERITREA = new CountryCodeEnum("ERI", "Eritrea");
	public static readonly CountryCodeEnum WESTERN_SAHARA = new CountryCodeEnum("ESH", "Western Sahara");
	public static readonly CountryCodeEnum SPAIN = new CountryCodeEnum("ESP", "Spain");
	public static readonly CountryCodeEnum ESTONIA = new CountryCodeEnum("EST", "Estonia");
	public static readonly CountryCodeEnum ETHIOPIA = new CountryCodeEnum("ETH", "Ethiopia");
	public static readonly CountryCodeEnum FINLAND = new CountryCodeEnum("FIN", "Finland");
	public static readonly CountryCodeEnum FIJI = new CountryCodeEnum("FJI", "Fiji");
	public static readonly CountryCodeEnum FALKLAND_ISLANDS_MALVINAS = new CountryCodeEnum("FLK", "Falkland Islands (Malvinas)");
	public static readonly CountryCodeEnum FRANCE = new CountryCodeEnum("FRA", "France");
	public static readonly CountryCodeEnum FAROE_ISLANDS = new CountryCodeEnum("FRO", "Faroe Islands");
	public static readonly CountryCodeEnum MICRONESIA_FEDERATED_STATES_OF = new CountryCodeEnum("FSM", "Micronesia, Federated States of");
	public static readonly CountryCodeEnum GABON = new CountryCodeEnum("GAB", "Gabon");
	public static readonly CountryCodeEnum UNITED_KINGDOM = new CountryCodeEnum("GBR", "United Kingdom");
	public static readonly CountryCodeEnum GEORGIA = new CountryCodeEnum("GEO", "Georgia");
	public static readonly CountryCodeEnum GUERNSEY = new CountryCodeEnum("GGY", "Guernsey");
	public static readonly CountryCodeEnum GHANA = new CountryCodeEnum("GHA", "Ghana");
	public static readonly CountryCodeEnum GIBRALTAR = new CountryCodeEnum("GIB", "Gibraltar");
	public static readonly CountryCodeEnum GUINEA = new CountryCodeEnum("GIN", "Guinea");
	public static readonly CountryCodeEnum GUADELOUPE = new CountryCodeEnum("GLP", "Guadeloupe");
	public static readonly CountryCodeEnum GAMBIA = new CountryCodeEnum("GMB", "Gambia");
	public static readonly CountryCodeEnum GUINEABISSAU = new CountryCodeEnum("GNB", "Guinea-Bissau");
	public static readonly CountryCodeEnum EQUATORIAL_GUINEA = new CountryCodeEnum("GNQ", "Equatorial Guinea");
	public static readonly CountryCodeEnum GREECE = new CountryCodeEnum("GRC", "Greece");
	public static readonly CountryCodeEnum GRENADA = new CountryCodeEnum("GRD", "Grenada");
	public static readonly CountryCodeEnum GREENLAND = new CountryCodeEnum("GRL", "Greenland");
	public static readonly CountryCodeEnum GUATEMALA = new CountryCodeEnum("GTM", "Guatemala");
	public static readonly CountryCodeEnum FRENCH_GUIANA = new CountryCodeEnum("GUF", "French Guiana");
	public static readonly CountryCodeEnum GUAM = new CountryCodeEnum("GUM", "Guam");
	public static readonly CountryCodeEnum GUYANA = new CountryCodeEnum("GUY", "Guyana");
	public static readonly CountryCodeEnum HONG_KONG = new CountryCodeEnum("HKG", "Hong Kong");
	public static readonly CountryCodeEnum HEARD_ISLAND_AND_MCDONALD_ISLANDS = new CountryCodeEnum("HMD", "Heard Island and McDonald Islands");
	public static readonly CountryCodeEnum HONDURAS = new CountryCodeEnum("HND", "Honduras");
	public static readonly CountryCodeEnum CROATIA = new CountryCodeEnum("HRV", "Croatia");
	public static readonly CountryCodeEnum HAITI = new CountryCodeEnum("HTI", "Haiti");
	public static readonly CountryCodeEnum HUNGARY = new CountryCodeEnum("HUN", "Hungary");
	public static readonly CountryCodeEnum INDONESIA = new CountryCodeEnum("IDN", "Indonesia");
	public static readonly CountryCodeEnum ISLE_OF_MAN = new CountryCodeEnum("IMN", "Isle of Man");
	public static readonly CountryCodeEnum INDIA = new CountryCodeEnum("IND", "India");
	public static readonly CountryCodeEnum BRITISH_INDIAN_OCEAN_TERRITORY = new CountryCodeEnum("IOT", "British Indian Ocean Territory");
	public static readonly CountryCodeEnum IRELAND = new CountryCodeEnum("IRL", "Ireland");
	public static readonly CountryCodeEnum IRAN_ISLAMIC_REPUBLIC_OF = new CountryCodeEnum("IRN", "Iran, Islamic Republic of");
	public static readonly CountryCodeEnum IRAQ = new CountryCodeEnum("IRQ", "Iraq");
	public static readonly CountryCodeEnum ICELAND = new CountryCodeEnum("ISL", "Iceland");
	public static readonly CountryCodeEnum ISRAEL = new CountryCodeEnum("ISR", "Israel");
	public static readonly CountryCodeEnum ITALY = new CountryCodeEnum("ITA", "Italy");
	public static readonly CountryCodeEnum JAMAICA = new CountryCodeEnum("JAM", "Jamaica");
	public static readonly CountryCodeEnum JERSEY = new CountryCodeEnum("JEY", "Jersey");
	public static readonly CountryCodeEnum JORDAN = new CountryCodeEnum("JOR", "Jordan");
	public static readonly CountryCodeEnum JAPAN = new CountryCodeEnum("JPN", "Japan");
	public static readonly CountryCodeEnum KAZAKHSTAN = new CountryCodeEnum("KAZ", "Kazakhstan");
	public static readonly CountryCodeEnum KENYA = new CountryCodeEnum("KEN", "Kenya");
	public static readonly CountryCodeEnum KYRGYZSTAN = new CountryCodeEnum("KGZ", "Kyrgyzstan");
	public static readonly CountryCodeEnum CAMBODIA = new CountryCodeEnum("KHM", "Cambodia");
	public static readonly CountryCodeEnum KIRIBATI = new CountryCodeEnum("KIR", "Kiribati");
	public static readonly CountryCodeEnum SAINT_KITTS_AND_NEVIS = new CountryCodeEnum("KNA", "Saint Kitts and Nevis");
	public static readonly CountryCodeEnum KOREA_REPUBLIC_OF = new CountryCodeEnum("KOR", "Korea, Republic of");
	public static readonly CountryCodeEnum KUWAIT = new CountryCodeEnum("KWT", "Kuwait");
	public static readonly CountryCodeEnum LAO_PEOPLES_DEMOCRATIC_REPUBLIC = new CountryCodeEnum("LAO", "Lao People's Democratic Republic");
	public static readonly CountryCodeEnum LEBANON = new CountryCodeEnum("LBN", "Lebanon");
	public static readonly CountryCodeEnum LIBERIA = new CountryCodeEnum("LBR", "Liberia");
	public static readonly CountryCodeEnum LIBYAN_ARAB_JAMAHIRIYA = new CountryCodeEnum("LBY", "Libyan Arab Jamahiriya");
	public static readonly CountryCodeEnum SAINT_LUCIA = new CountryCodeEnum("LCA", "Saint Lucia");
	public static readonly CountryCodeEnum LIECHTENSTEIN = new CountryCodeEnum("LIE", "Liechtenstein");
	public static readonly CountryCodeEnum SRI_LANKA = new CountryCodeEnum("LKA", "Sri Lanka");
	public static readonly CountryCodeEnum LESOTHO = new CountryCodeEnum("LSO", "Lesotho");
	public static readonly CountryCodeEnum LITHUANIA = new CountryCodeEnum("LTU", "Lithuania");
	public static readonly CountryCodeEnum LUXEMBOURG = new CountryCodeEnum("LUX", "Luxembourg");
	public static readonly CountryCodeEnum LATVIA = new CountryCodeEnum("LVA", "Latvia");
	public static readonly CountryCodeEnum MACAO = new CountryCodeEnum("MAC", "Macao");
	public static readonly CountryCodeEnum SAINT_MARTIN_FRENCH_PART = new CountryCodeEnum("MAF", "Saint Martin (French part)");
	public static readonly CountryCodeEnum MOROCCO = new CountryCodeEnum("MAR", "Morocco");
	public static readonly CountryCodeEnum MONACO = new CountryCodeEnum("MCO", "Monaco");
	public static readonly CountryCodeEnum MOLDOVA_REPUBLIC_OF = new CountryCodeEnum("MDA", "Moldova, Republic of");
	public static readonly CountryCodeEnum MADAGASCAR = new CountryCodeEnum("MDG", "Madagascar");
	public static readonly CountryCodeEnum MALDIVES = new CountryCodeEnum("MDV", "Maldives");
	public static readonly CountryCodeEnum MEXICO = new CountryCodeEnum("MEX", "Mexico");
	public static readonly CountryCodeEnum MARSHALL_ISLANDS = new CountryCodeEnum("MHL", "Marshall Islands");
	public static readonly CountryCodeEnum MACEDONIA_THE_FORMER_YUGOSLAV_REPUBLIC_OF = new CountryCodeEnum("MKD", "Macedonia, the former Yugoslav Republic of");
	public static readonly CountryCodeEnum MALI = new CountryCodeEnum("MLI", "Mali");
	public static readonly CountryCodeEnum MALTA = new CountryCodeEnum("MLT", "Malta");
	public static readonly CountryCodeEnum MYANMAR = new CountryCodeEnum("MMR", "Myanmar");
	public static readonly CountryCodeEnum MONTENEGRO = new CountryCodeEnum("MNE", "Montenegro");
	public static readonly CountryCodeEnum MONGOLIA = new CountryCodeEnum("MNG", "Mongolia");
	public static readonly CountryCodeEnum NORTHERN_MARIANA_ISLANDS = new CountryCodeEnum("MNP", "Northern Mariana Islands");
	public static readonly CountryCodeEnum MOZAMBIQUE = new CountryCodeEnum("MOZ", "Mozambique");
	public static readonly CountryCodeEnum MAURITANIA = new CountryCodeEnum("MRT", "Mauritania");
	public static readonly CountryCodeEnum MONTSERRAT = new CountryCodeEnum("MSR", "Montserrat");
	public static readonly CountryCodeEnum MARTINIQUE = new CountryCodeEnum("MTQ", "Martinique");
	public static readonly CountryCodeEnum MAURITIUS = new CountryCodeEnum("MUS", "Mauritius");
	public static readonly CountryCodeEnum MALAWI = new CountryCodeEnum("MWI", "Malawi");
	public static readonly CountryCodeEnum MALAYSIA = new CountryCodeEnum("MYS", "Malaysia");
	public static readonly CountryCodeEnum MAYOTTE = new CountryCodeEnum("MYT", "Mayotte");
	public static readonly CountryCodeEnum NAMIBIA = new CountryCodeEnum("NAM", "Namibia");
	public static readonly CountryCodeEnum NEW_CALEDONIA = new CountryCodeEnum("NCL", "New Caledonia");
	public static readonly CountryCodeEnum NIGER = new CountryCodeEnum("NER", "Niger");
	public static readonly CountryCodeEnum NORFOLK_ISLAND = new CountryCodeEnum("NFK", "Norfolk Island");
	public static readonly CountryCodeEnum NIGERIA = new CountryCodeEnum("NGA", "Nigeria");
	public static readonly CountryCodeEnum NICARAGUA = new CountryCodeEnum("NIC", "Nicaragua");
	public static readonly CountryCodeEnum _NIUE = new CountryCodeEnum(" NI", " Niue");
	public static readonly CountryCodeEnum NETHERLANDS = new CountryCodeEnum("NLD", "Netherlands");
	public static readonly CountryCodeEnum NORWAY = new CountryCodeEnum("NOR", "Norway");
	public static readonly CountryCodeEnum NEPAL = new CountryCodeEnum("NPL", "Nepal");
	public static readonly CountryCodeEnum NAURU = new CountryCodeEnum("NRU", "Nauru");
	public static readonly CountryCodeEnum NEW_ZEALAND = new CountryCodeEnum("NZL", "New Zealand");
	public static readonly CountryCodeEnum OMAN = new CountryCodeEnum("OMN", "Oman");
	public static readonly CountryCodeEnum PAKISTAN = new CountryCodeEnum("PAK", "Pakistan");
	public static readonly CountryCodeEnum PANAMA = new CountryCodeEnum("PAN", "Panama");
	public static readonly CountryCodeEnum PITCAIRN = new CountryCodeEnum("PCN", "Pitcairn");
	public static readonly CountryCodeEnum PERU = new CountryCodeEnum("PER", "Peru");
	public static readonly CountryCodeEnum PHILIPPINES = new CountryCodeEnum("PHL", "Philippines");
	public static readonly CountryCodeEnum PALAU = new CountryCodeEnum("PLW", "Palau");
	public static readonly CountryCodeEnum PAPUA_NEW_GUINEA = new CountryCodeEnum("PNG", "Papua New Guinea");
	public static readonly CountryCodeEnum POLAND = new CountryCodeEnum("POL", "Poland");
	public static readonly CountryCodeEnum PUERTO_RICO = new CountryCodeEnum("PRI", "Puerto Rico");
	public static readonly CountryCodeEnum KOREA_DEMOCRATIC_PEOPLES_REPUBLIC_OF = new CountryCodeEnum("PRK", "Korea, Democratic People's Republic of");
	public static readonly CountryCodeEnum PORTUGAL = new CountryCodeEnum("PRT", "Portugal");
	public static readonly CountryCodeEnum PARAGUAY = new CountryCodeEnum("PRY", "Paraguay");
	public static readonly CountryCodeEnum PALESTINIAN_TERRITORY_OCCUPIED = new CountryCodeEnum("PSE", "Palestinian Territory, Occupied");
	public static readonly CountryCodeEnum FRENCH_POLYNESIA = new CountryCodeEnum("PYF", "French Polynesia");
	public static readonly CountryCodeEnum QATAR = new CountryCodeEnum("QAT", "Qatar");
	public static readonly CountryCodeEnum REUNION = new CountryCodeEnum("REU", "Reunion");
	public static readonly CountryCodeEnum ROMANIA = new CountryCodeEnum("ROU", "Romania");
	public static readonly CountryCodeEnum RUSSIAN_FEDERATION = new CountryCodeEnum("RUS", "Russian Federation");
	public static readonly CountryCodeEnum RWANDA = new CountryCodeEnum("RWA", "Rwanda");
	public static readonly CountryCodeEnum SAUDI_ARABIA = new CountryCodeEnum("SAU", "Saudi Arabia");
	public static readonly CountryCodeEnum SUDAN = new CountryCodeEnum("SDN", "Sudan");
	public static readonly CountryCodeEnum SENEGAL = new CountryCodeEnum("SEN", "Senegal");
	public static readonly CountryCodeEnum SINGAPORE = new CountryCodeEnum("SGP", "Singapore");
	public static readonly CountryCodeEnum SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS = new CountryCodeEnum("SGS", "South Georgia and the South Sandwich Islands");
	public static readonly CountryCodeEnum SAINT_HELENA = new CountryCodeEnum("SHN", "Saint Helena");
	public static readonly CountryCodeEnum SVALBARD_AND_JAN_MAYEN = new CountryCodeEnum("SJM", "Svalbard and Jan Mayen");
	public static readonly CountryCodeEnum SOLOMON_ISLANDS = new CountryCodeEnum("SLB", "Solomon Islands");
	public static readonly CountryCodeEnum SIERRA_LEONE = new CountryCodeEnum("SLE", "Sierra Leone");
	public static readonly CountryCodeEnum EL_SALVADOR = new CountryCodeEnum("SLV", "El Salvador");
	public static readonly CountryCodeEnum SAN_MARINO = new CountryCodeEnum("SMR", "San Marino");
	public static readonly CountryCodeEnum SOMALIA = new CountryCodeEnum("SOM", "Somalia");
	public static readonly CountryCodeEnum SAINT_PIERRE_AND_MIQUELON = new CountryCodeEnum("SPM", "Saint Pierre and Miquelon");
	public static readonly CountryCodeEnum SERBIA = new CountryCodeEnum("SRB", "Serbia");
	public static readonly CountryCodeEnum SAO_TOME_AND_PRINCIPE = new CountryCodeEnum("STP", "Sao Tome and Principe");
	public static readonly CountryCodeEnum SURINAME = new CountryCodeEnum("SUR", "Suriname");
	public static readonly CountryCodeEnum SLOVAKIA = new CountryCodeEnum("SVK", "Slovakia");
	public static readonly CountryCodeEnum SLOVENIA = new CountryCodeEnum("SVN", "Slovenia");
	public static readonly CountryCodeEnum SWEDEN = new CountryCodeEnum("SWE", "Sweden");
	public static readonly CountryCodeEnum SWAZILAND = new CountryCodeEnum("SWZ", "Swaziland");
	public static readonly CountryCodeEnum SEYCHELLES = new CountryCodeEnum("SYC", "Seychelles");
	public static readonly CountryCodeEnum SYRIAN_ARAB_REPUBLIC = new CountryCodeEnum("SYR", "Syrian Arab Republic");
	public static readonly CountryCodeEnum TURKS_AND_CAICOS_ISLANDS = new CountryCodeEnum("TCA", "Turks and Caicos Islands");
	public static readonly CountryCodeEnum CHAD = new CountryCodeEnum("TCD", "Chad");
	public static readonly CountryCodeEnum TOGO = new CountryCodeEnum("TGO", "Togo");
	public static readonly CountryCodeEnum THAILAND = new CountryCodeEnum("THA", "Thailand");
	public static readonly CountryCodeEnum TAJIKISTAN = new CountryCodeEnum("TJK", "Tajikistan");
	public static readonly CountryCodeEnum TOKELAU = new CountryCodeEnum("TKL", "Tokelau");
	public static readonly CountryCodeEnum TURKMENISTAN = new CountryCodeEnum("TKM", "Turkmenistan");
	public static readonly CountryCodeEnum TIMORLESTE = new CountryCodeEnum("TLS", "Timor-Leste");
	public static readonly CountryCodeEnum TONGA = new CountryCodeEnum("TON", "Tonga");
	public static readonly CountryCodeEnum TRINIDAD_AND_TOBAGO = new CountryCodeEnum("TTO", "Trinidad and Tobago");
	public static readonly CountryCodeEnum TUNISIA = new CountryCodeEnum("TUN", "Tunisia");
	public static readonly CountryCodeEnum TURKEY = new CountryCodeEnum("TUR", "Turkey");
	public static readonly CountryCodeEnum TUVALU = new CountryCodeEnum("TUV", "Tuvalu");
	public static readonly CountryCodeEnum TAIWAN_PROVINCE_OF_CHINA = new CountryCodeEnum("TWN", "Taiwan, Province of China");
	public static readonly CountryCodeEnum TANZANIA_UNITED_REPUBLIC_OF = new CountryCodeEnum("TZA", "Tanzania, United Republic of");
	public static readonly CountryCodeEnum UGANDA = new CountryCodeEnum("UGA", "Uganda");
	public static readonly CountryCodeEnum UKRAINE = new CountryCodeEnum("UKR", "Ukraine");
	public static readonly CountryCodeEnum UNITED_STATES_MINOR_OUTLYING_ISLANDS = new CountryCodeEnum("UMI", "United States Minor Outlying Islands");
	public static readonly CountryCodeEnum URUGUAY = new CountryCodeEnum("URY", "Uruguay");
	public static readonly CountryCodeEnum UNITED_STATES = new CountryCodeEnum("USA", "United States");
	public static readonly CountryCodeEnum UZBEKISTAN = new CountryCodeEnum("UZB", "Uzbekistan");
	public static readonly CountryCodeEnum HOLY_SEE_VATICAN_CITY_STATE = new CountryCodeEnum("VAT", "Holy See (Vatican City State)");
	public static readonly CountryCodeEnum SAINT_VINCENT_AND_THE_GRENADINES = new CountryCodeEnum("VCT", "Saint Vincent and the Grenadines");
	public static readonly CountryCodeEnum VENEZUELA_BOLIVARIAN_REPUBLIC_OF = new CountryCodeEnum("VEN", "Venezuela, Bolivarian Republic of");
	public static readonly CountryCodeEnum VIRGIN_ISLANDS_BRITISH = new CountryCodeEnum("VGB", "Virgin Islands, British");
	public static readonly CountryCodeEnum VIRGIN_ISLANDS_US = new CountryCodeEnum("VIR", "Virgin Islands, U.S.");
	public static readonly CountryCodeEnum VIET_NAM = new CountryCodeEnum("VNM", "Viet Nam");
	public static readonly CountryCodeEnum VANUATU = new CountryCodeEnum("VUT", "Vanuatu");
	public static readonly CountryCodeEnum WALLIS_AND_FUTUNA = new CountryCodeEnum("WLF", "Wallis and Futuna");
	public static readonly CountryCodeEnum SAMOA = new CountryCodeEnum("WSM", "Samoa");
	public static readonly CountryCodeEnum YEMEN = new CountryCodeEnum("YEM", "Yemen");
	public static readonly CountryCodeEnum SOUTH_AFRICA = new CountryCodeEnum("ZAF", "South Africa");
	public static readonly CountryCodeEnum ZAMBIA = new CountryCodeEnum("ZMB", "Zambia");
	public static readonly CountryCodeEnum ZIMBABWE = new CountryCodeEnum("ZWE", "Zimbabwe");

	public static CountryCodeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (CountryCodeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private CountryCodeEnum() {}

	private CountryCodeEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static EnumDataTypeList Options {
	    get { return OPTIONS; }
	}
    }
}
