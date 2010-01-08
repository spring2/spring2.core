using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Geocode.Types {

    public class CountryCodeEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new CountryCodeEnum DEFAULT = new CountryCodeEnum();
	public static readonly new CountryCodeEnum UNSET = new CountryCodeEnum();

	public static readonly CountryCodeEnum AFGHANISTAN = new CountryCodeEnum("AF", "AFGHANISTAN");
	public static readonly CountryCodeEnum ALAND_ISLANDS = new CountryCodeEnum("AX", "ALAND ISLANDS");
	public static readonly CountryCodeEnum ALBANIA = new CountryCodeEnum("AL", "ALBANIA");
	public static readonly CountryCodeEnum ALGERIA = new CountryCodeEnum("DZ", "ALGERIA");
	public static readonly CountryCodeEnum AMERICAN_SAMOA = new CountryCodeEnum("AS", "AMERICAN SAMOA");
	public static readonly CountryCodeEnum ANDORRA = new CountryCodeEnum("AD", "ANDORRA");
	public static readonly CountryCodeEnum ANGOLA = new CountryCodeEnum("AO", "ANGOLA");
	public static readonly CountryCodeEnum ANGUILLA = new CountryCodeEnum("AI", "ANGUILLA");
	public static readonly CountryCodeEnum ANTARCTICA = new CountryCodeEnum("AQ", "ANTARCTICA");
	public static readonly CountryCodeEnum ANTIGUA_AND_BARBUDA = new CountryCodeEnum("AG", "ANTIGUA AND BARBUDA");
	public static readonly CountryCodeEnum ARGENTINA = new CountryCodeEnum("AR", "ARGENTINA");
	public static readonly CountryCodeEnum ARMENIA = new CountryCodeEnum("AM", "ARMENIA");
	public static readonly CountryCodeEnum ARUBA = new CountryCodeEnum("AW", "ARUBA");
	public static readonly CountryCodeEnum AUSTRALIA = new CountryCodeEnum("AU", "AUSTRALIA");
	public static readonly CountryCodeEnum AUSTRIA = new CountryCodeEnum("AT", "AUSTRIA");
	public static readonly CountryCodeEnum AZERBAIJAN = new CountryCodeEnum("AZ", "AZERBAIJAN");
	public static readonly CountryCodeEnum BAHAMAS = new CountryCodeEnum("BS", "BAHAMAS");
	public static readonly CountryCodeEnum BAHRAIN = new CountryCodeEnum("BH", "BAHRAIN");
	public static readonly CountryCodeEnum BANGLADESH = new CountryCodeEnum("BD", "BANGLADESH");
	public static readonly CountryCodeEnum BARBADOS = new CountryCodeEnum("BB", "BARBADOS");
	public static readonly CountryCodeEnum BELARUS = new CountryCodeEnum("BY", "BELARUS");
	public static readonly CountryCodeEnum BELGIUM = new CountryCodeEnum("BE", "BELGIUM");
	public static readonly CountryCodeEnum BELIZE = new CountryCodeEnum("BZ", "BELIZE");
	public static readonly CountryCodeEnum BENIN = new CountryCodeEnum("BJ", "BENIN");
	public static readonly CountryCodeEnum BERMUDA = new CountryCodeEnum("BM", "BERMUDA");
	public static readonly CountryCodeEnum BHUTAN = new CountryCodeEnum("BT", "BHUTAN");
	public static readonly CountryCodeEnum BOLIVIA_PLURINATIONAL_STATE_OF = new CountryCodeEnum("BO", "BOLIVIA, PLURINATIONAL STATE OF");
	public static readonly CountryCodeEnum BOSNIA_AND_HERZEGOVINA = new CountryCodeEnum("BA", "BOSNIA AND HERZEGOVINA");
	public static readonly CountryCodeEnum BOTSWANA = new CountryCodeEnum("BW", "BOTSWANA");
	public static readonly CountryCodeEnum BOUVET_ISLAND = new CountryCodeEnum("BV", "BOUVET ISLAND");
	public static readonly CountryCodeEnum BRAZIL = new CountryCodeEnum("BR", "BRAZIL");
	public static readonly CountryCodeEnum BRITISH_INDIAN_OCEAN_TERRITORY = new CountryCodeEnum("IO", "BRITISH INDIAN OCEAN TERRITORY");
	public static readonly CountryCodeEnum BRUNEI_DARUSSALAM = new CountryCodeEnum("BN", "BRUNEI DARUSSALAM");
	public static readonly CountryCodeEnum BULGARIA = new CountryCodeEnum("BG", "BULGARIA");
	public static readonly CountryCodeEnum BURKINA_FASO = new CountryCodeEnum("BF", "BURKINA FASO");
	public static readonly CountryCodeEnum BURUNDI = new CountryCodeEnum("BI", "BURUNDI");
	public static readonly CountryCodeEnum CAMBODIA = new CountryCodeEnum("KH", "CAMBODIA");
	public static readonly CountryCodeEnum CAMEROON = new CountryCodeEnum("CM", "CAMEROON");
	public static readonly CountryCodeEnum CANADA = new CountryCodeEnum("CA", "CANADA");
	public static readonly CountryCodeEnum CAPE_VERDE = new CountryCodeEnum("CV", "CAPE VERDE");
	public static readonly CountryCodeEnum CAYMAN_ISLANDS = new CountryCodeEnum("KY", "CAYMAN ISLANDS");
	public static readonly CountryCodeEnum CENTRAL_AFRICAN_REPUBLIC = new CountryCodeEnum("CF", "CENTRAL AFRICAN REPUBLIC");
	public static readonly CountryCodeEnum CHAD = new CountryCodeEnum("TD", "CHAD");
	public static readonly CountryCodeEnum CHILE = new CountryCodeEnum("CL", "CHILE");
	public static readonly CountryCodeEnum CHINA = new CountryCodeEnum("CN", "CHINA");
	public static readonly CountryCodeEnum CHRISTMAS_ISLAND = new CountryCodeEnum("CX", "CHRISTMAS ISLAND");
	public static readonly CountryCodeEnum COCOS_KEELING_ISLANDS = new CountryCodeEnum("CC", "COCOS (KEELING) ISLANDS");
	public static readonly CountryCodeEnum COLOMBIA = new CountryCodeEnum("CO", "COLOMBIA");
	public static readonly CountryCodeEnum COMOROS = new CountryCodeEnum("KM", "COMOROS");
	public static readonly CountryCodeEnum CONGO = new CountryCodeEnum("CG", "CONGO");
	public static readonly CountryCodeEnum CONGO_THE_DEMOCRATIC_REPUBLIC_OF_THE = new CountryCodeEnum("CD", "CONGO, THE DEMOCRATIC REPUBLIC OF THE");
	public static readonly CountryCodeEnum COOK_ISLANDS = new CountryCodeEnum("CK", "COOK ISLANDS");
	public static readonly CountryCodeEnum COSTA_RICA = new CountryCodeEnum("CR", "COSTA RICA");
	public static readonly CountryCodeEnum COTE_DIVOIRE = new CountryCodeEnum("CI", "COTE D'IVOIRE");
	public static readonly CountryCodeEnum CROATIA = new CountryCodeEnum("HR", "CROATIA");
	public static readonly CountryCodeEnum CUBA = new CountryCodeEnum("CU", "CUBA");
	public static readonly CountryCodeEnum CYPRUS = new CountryCodeEnum("CY", "CYPRUS");
	public static readonly CountryCodeEnum CZECH_REPUBLIC = new CountryCodeEnum("CZ", "CZECH REPUBLIC");
	public static readonly CountryCodeEnum DENMARK = new CountryCodeEnum("DK", "DENMARK");
	public static readonly CountryCodeEnum DJIBOUTI = new CountryCodeEnum("DJ", "DJIBOUTI");
	public static readonly CountryCodeEnum DOMINICA = new CountryCodeEnum("DM", "DOMINICA");
	public static readonly CountryCodeEnum DOMINICAN_REPUBLIC = new CountryCodeEnum("DO", "DOMINICAN REPUBLIC");
	public static readonly CountryCodeEnum ECUADOR = new CountryCodeEnum("EC", "ECUADOR");
	public static readonly CountryCodeEnum EGYPT = new CountryCodeEnum("EG", "EGYPT");
	public static readonly CountryCodeEnum EL_SALVADOR = new CountryCodeEnum("SV", "EL SALVADOR");
	public static readonly CountryCodeEnum EQUATORIAL_GUINEA = new CountryCodeEnum("GQ", "EQUATORIAL GUINEA");
	public static readonly CountryCodeEnum ERITREA = new CountryCodeEnum("ER", "ERITREA");
	public static readonly CountryCodeEnum ESTONIA = new CountryCodeEnum("EE", "ESTONIA");
	public static readonly CountryCodeEnum ETHIOPIA = new CountryCodeEnum("ET", "ETHIOPIA");
	public static readonly CountryCodeEnum FALKLAND_ISLANDS_MALVINAS = new CountryCodeEnum("FK", "FALKLAND ISLANDS (MALVINAS)");
	public static readonly CountryCodeEnum FAROE_ISLANDS = new CountryCodeEnum("FO", "FAROE ISLANDS");
	public static readonly CountryCodeEnum FIJI = new CountryCodeEnum("FJ", "FIJI");
	public static readonly CountryCodeEnum FINLAND = new CountryCodeEnum("FI", "FINLAND");
	public static readonly CountryCodeEnum FRANCE = new CountryCodeEnum("FR", "FRANCE");
	public static readonly CountryCodeEnum FRENCH_GUIANA = new CountryCodeEnum("GF", "FRENCH GUIANA");
	public static readonly CountryCodeEnum FRENCH_POLYNESIA = new CountryCodeEnum("PF", "FRENCH POLYNESIA");
	public static readonly CountryCodeEnum FRENCH_SOUTHERN_TERRITORIES = new CountryCodeEnum("TF", "FRENCH SOUTHERN TERRITORIES");
	public static readonly CountryCodeEnum GABON = new CountryCodeEnum("GA", "GABON");
	public static readonly CountryCodeEnum GAMBIA = new CountryCodeEnum("GM", "GAMBIA");
	public static readonly CountryCodeEnum GEORGIA = new CountryCodeEnum("GE", "GEORGIA");
	public static readonly CountryCodeEnum GERMANY = new CountryCodeEnum("DE", "GERMANY");
	public static readonly CountryCodeEnum GHANA = new CountryCodeEnum("GH", "GHANA");
	public static readonly CountryCodeEnum GIBRALTAR = new CountryCodeEnum("GI", "GIBRALTAR");
	public static readonly CountryCodeEnum GREECE = new CountryCodeEnum("GR", "GREECE");
	public static readonly CountryCodeEnum GREENLAND = new CountryCodeEnum("GL", "GREENLAND");
	public static readonly CountryCodeEnum GRENADA = new CountryCodeEnum("GD", "GRENADA");
	public static readonly CountryCodeEnum GUADELOUPE = new CountryCodeEnum("GP", "GUADELOUPE");
	public static readonly CountryCodeEnum GUAM = new CountryCodeEnum("GU", "GUAM");
	public static readonly CountryCodeEnum GUATEMALA = new CountryCodeEnum("GT", "GUATEMALA");
	public static readonly CountryCodeEnum GUERNSEY = new CountryCodeEnum("GG", "GUERNSEY");
	public static readonly CountryCodeEnum GUINEA = new CountryCodeEnum("GN", "GUINEA");
	public static readonly CountryCodeEnum GUINEABISSAU = new CountryCodeEnum("GW", "GUINEA-BISSAU");
	public static readonly CountryCodeEnum GUYANA = new CountryCodeEnum("GY", "GUYANA");
	public static readonly CountryCodeEnum HAITI = new CountryCodeEnum("HT", "HAITI");
	public static readonly CountryCodeEnum HEARD_ISLAND_AND_MCDONALD_ISLANDS = new CountryCodeEnum("HM", "HEARD ISLAND AND MCDONALD ISLANDS");
	public static readonly CountryCodeEnum HOLY_SEE_VATICAN_CITY_STATE = new CountryCodeEnum("VA", "HOLY SEE (VATICAN CITY STATE)");
	public static readonly CountryCodeEnum HONDURAS = new CountryCodeEnum("HN", "HONDURAS");
	public static readonly CountryCodeEnum HONG_KONG = new CountryCodeEnum("HK", "HONG KONG");
	public static readonly CountryCodeEnum HUNGARY = new CountryCodeEnum("HU", "HUNGARY");
	public static readonly CountryCodeEnum ICELAND = new CountryCodeEnum("IS", "ICELAND");
	public static readonly CountryCodeEnum INDIA = new CountryCodeEnum("IN", "INDIA");
	public static readonly CountryCodeEnum INDONESIA = new CountryCodeEnum("ID", "INDONESIA");
	public static readonly CountryCodeEnum IRAN_ISLAMIC_REPUBLIC_OF = new CountryCodeEnum("IR", "IRAN, ISLAMIC REPUBLIC OF");
	public static readonly CountryCodeEnum IRAQ = new CountryCodeEnum("IQ", "IRAQ");
	public static readonly CountryCodeEnum IRELAND = new CountryCodeEnum("IE", "IRELAND");
	public static readonly CountryCodeEnum ISLE_OF_MAN = new CountryCodeEnum("IM", "ISLE OF MAN");
	public static readonly CountryCodeEnum ISRAEL = new CountryCodeEnum("IL", "ISRAEL");
	public static readonly CountryCodeEnum ITALY = new CountryCodeEnum("IT", "ITALY");
	public static readonly CountryCodeEnum JAMAICA = new CountryCodeEnum("JM", "JAMAICA");
	public static readonly CountryCodeEnum JAPAN = new CountryCodeEnum("JP", "JAPAN");
	public static readonly CountryCodeEnum JERSEY = new CountryCodeEnum("JE", "JERSEY");
	public static readonly CountryCodeEnum JORDAN = new CountryCodeEnum("JO", "JORDAN");
	public static readonly CountryCodeEnum KAZAKHSTAN = new CountryCodeEnum("KZ", "KAZAKHSTAN");
	public static readonly CountryCodeEnum KENYA = new CountryCodeEnum("KE", "KENYA");
	public static readonly CountryCodeEnum KIRIBATI = new CountryCodeEnum("KI", "KIRIBATI");
	public static readonly CountryCodeEnum KOREA_DEMOCRATIC_PEOPLES_REPUBLIC_OF = new CountryCodeEnum("KP", "KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF");
	public static readonly CountryCodeEnum KOREA_REPUBLIC_OF = new CountryCodeEnum("KR", "KOREA, REPUBLIC OF");
	public static readonly CountryCodeEnum KUWAIT = new CountryCodeEnum("KW", "KUWAIT");
	public static readonly CountryCodeEnum KYRGYZSTAN = new CountryCodeEnum("KG", "KYRGYZSTAN");
	public static readonly CountryCodeEnum LAO_PEOPLES_DEMOCRATIC_REPUBLIC = new CountryCodeEnum("LA", "LAO PEOPLE'S DEMOCRATIC REPUBLIC");
	public static readonly CountryCodeEnum LATVIA = new CountryCodeEnum("LV", "LATVIA");
	public static readonly CountryCodeEnum LEBANON = new CountryCodeEnum("LB", "LEBANON");
	public static readonly CountryCodeEnum LESOTHO = new CountryCodeEnum("LS", "LESOTHO");
	public static readonly CountryCodeEnum LIBERIA = new CountryCodeEnum("LR", "LIBERIA");
	public static readonly CountryCodeEnum LIBYAN_ARAB_JAMAHIRIYA = new CountryCodeEnum("LY", "LIBYAN ARAB JAMAHIRIYA");
	public static readonly CountryCodeEnum LIECHTENSTEIN = new CountryCodeEnum("LI", "LIECHTENSTEIN");
	public static readonly CountryCodeEnum LITHUANIA = new CountryCodeEnum("LT", "LITHUANIA");
	public static readonly CountryCodeEnum LUXEMBOURG = new CountryCodeEnum("LU", "LUXEMBOURG");
	public static readonly CountryCodeEnum MACAO = new CountryCodeEnum("MO", "MACAO");
	public static readonly CountryCodeEnum MACEDONIA_THE_FORMER_YUGOSLAV_REPUBLIC_OF = new CountryCodeEnum("MK", "MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF");
	public static readonly CountryCodeEnum MADAGASCAR = new CountryCodeEnum("MG", "MADAGASCAR");
	public static readonly CountryCodeEnum MALAWI = new CountryCodeEnum("MW", "MALAWI");
	public static readonly CountryCodeEnum MALAYSIA = new CountryCodeEnum("MY", "MALAYSIA");
	public static readonly CountryCodeEnum MALDIVES = new CountryCodeEnum("MV", "MALDIVES");
	public static readonly CountryCodeEnum MALI = new CountryCodeEnum("ML", "MALI");
	public static readonly CountryCodeEnum MALTA = new CountryCodeEnum("MT", "MALTA");
	public static readonly CountryCodeEnum MARSHALL_ISLANDS = new CountryCodeEnum("MH", "MARSHALL ISLANDS");
	public static readonly CountryCodeEnum MARTINIQUE = new CountryCodeEnum("MQ", "MARTINIQUE");
	public static readonly CountryCodeEnum MAURITANIA = new CountryCodeEnum("MR", "MAURITANIA");
	public static readonly CountryCodeEnum MAURITIUS = new CountryCodeEnum("MU", "MAURITIUS");
	public static readonly CountryCodeEnum MAYOTTE = new CountryCodeEnum("YT", "MAYOTTE");
	public static readonly CountryCodeEnum MEXICO = new CountryCodeEnum("MX", "MEXICO");
	public static readonly CountryCodeEnum MICRONESIA_FEDERATED_STATES_OF = new CountryCodeEnum("FM", "MICRONESIA, FEDERATED STATES OF");
	public static readonly CountryCodeEnum MOLDOVA_REPUBLIC_OF = new CountryCodeEnum("MD", "MOLDOVA, REPUBLIC OF");
	public static readonly CountryCodeEnum MONACO = new CountryCodeEnum("MC", "MONACO");
	public static readonly CountryCodeEnum MONGOLIA = new CountryCodeEnum("MN", "MONGOLIA");
	public static readonly CountryCodeEnum MONTENEGRO = new CountryCodeEnum("ME", "MONTENEGRO");
	public static readonly CountryCodeEnum MONTSERRAT = new CountryCodeEnum("MS", "MONTSERRAT");
	public static readonly CountryCodeEnum MOROCCO = new CountryCodeEnum("MA", "MOROCCO");
	public static readonly CountryCodeEnum MOZAMBIQUE = new CountryCodeEnum("MZ", "MOZAMBIQUE");
	public static readonly CountryCodeEnum MYANMAR = new CountryCodeEnum("MM", "MYANMAR");
	public static readonly CountryCodeEnum NAMIBIA = new CountryCodeEnum("NA", "NAMIBIA");
	public static readonly CountryCodeEnum NAURU = new CountryCodeEnum("NR", "NAURU");
	public static readonly CountryCodeEnum NEPAL = new CountryCodeEnum("NP", "NEPAL");
	public static readonly CountryCodeEnum NETHERLANDS = new CountryCodeEnum("NL", "NETHERLANDS");
	public static readonly CountryCodeEnum NETHERLANDS_ANTILLES = new CountryCodeEnum("AN", "NETHERLANDS ANTILLES");
	public static readonly CountryCodeEnum NEW_CALEDONIA = new CountryCodeEnum("NC", "NEW CALEDONIA");
	public static readonly CountryCodeEnum NEW_ZEALAND = new CountryCodeEnum("NZ", "NEW ZEALAND");
	public static readonly CountryCodeEnum NICARAGUA = new CountryCodeEnum("NI", "NICARAGUA");
	public static readonly CountryCodeEnum NIGER = new CountryCodeEnum("NE", "NIGER");
	public static readonly CountryCodeEnum NIGERIA = new CountryCodeEnum("NG", "NIGERIA");
	public static readonly CountryCodeEnum NIUE = new CountryCodeEnum("NU", "NIUE");
	public static readonly CountryCodeEnum NORFOLK_ISLAND = new CountryCodeEnum("NF", "NORFOLK ISLAND");
	public static readonly CountryCodeEnum NORTHERN_MARIANA_ISLANDS = new CountryCodeEnum("MP", "NORTHERN MARIANA ISLANDS");
	public static readonly CountryCodeEnum NORWAY = new CountryCodeEnum("NO", "NORWAY");
	public static readonly CountryCodeEnum OMAN = new CountryCodeEnum("OM", "OMAN");
	public static readonly CountryCodeEnum PAKISTAN = new CountryCodeEnum("PK", "PAKISTAN");
	public static readonly CountryCodeEnum PALAU = new CountryCodeEnum("PW", "PALAU");
	public static readonly CountryCodeEnum PALESTINIAN_TERRITORY_OCCUPIED = new CountryCodeEnum("PS", "PALESTINIAN TERRITORY, OCCUPIED");
	public static readonly CountryCodeEnum PANAMA = new CountryCodeEnum("PA", "PANAMA");
	public static readonly CountryCodeEnum PAPUA_NEW_GUINEA = new CountryCodeEnum("PG", "PAPUA NEW GUINEA");
	public static readonly CountryCodeEnum PARAGUAY = new CountryCodeEnum("PY", "PARAGUAY");
	public static readonly CountryCodeEnum PERU = new CountryCodeEnum("PE", "PERU");
	public static readonly CountryCodeEnum PHILIPPINES = new CountryCodeEnum("PH", "PHILIPPINES");
	public static readonly CountryCodeEnum PITCAIRN = new CountryCodeEnum("PN", "PITCAIRN");
	public static readonly CountryCodeEnum POLAND = new CountryCodeEnum("PL", "POLAND");
	public static readonly CountryCodeEnum PORTUGAL = new CountryCodeEnum("PT", "PORTUGAL");
	public static readonly CountryCodeEnum PUERTO_RICO = new CountryCodeEnum("PR", "PUERTO RICO");
	public static readonly CountryCodeEnum QATAR = new CountryCodeEnum("QA", "QATAR");
	public static readonly CountryCodeEnum REUNION = new CountryCodeEnum("RE", "REUNION");
	public static readonly CountryCodeEnum ROMANIA = new CountryCodeEnum("RO", "ROMANIA");
	public static readonly CountryCodeEnum RUSSIAN_FEDERATION = new CountryCodeEnum("RU", "RUSSIAN FEDERATION");
	public static readonly CountryCodeEnum RWANDA = new CountryCodeEnum("RW", "RWANDA");
	public static readonly CountryCodeEnum SAINT_BARTHELEMY = new CountryCodeEnum("BL", "SAINT BARTHELEMY");
	public static readonly CountryCodeEnum SAINT_HELENA = new CountryCodeEnum("SH", "SAINT HELENA");
	public static readonly CountryCodeEnum SAINT_KITTS_AND_NEVIS = new CountryCodeEnum("KN", "SAINT KITTS AND NEVIS");
	public static readonly CountryCodeEnum SAINT_LUCIA = new CountryCodeEnum("LC", "SAINT LUCIA");
	public static readonly CountryCodeEnum SAINT_MARTIN = new CountryCodeEnum("MF", "SAINT MARTIN");
	public static readonly CountryCodeEnum SAINT_PIERRE_AND_MIQUELON = new CountryCodeEnum("PM", "SAINT PIERRE AND MIQUELON");
	public static readonly CountryCodeEnum SAINT_VINCENT_AND_THE_GRENADINES = new CountryCodeEnum("VC", "SAINT VINCENT AND THE GRENADINES");
	public static readonly CountryCodeEnum SAMOA = new CountryCodeEnum("WS", "SAMOA");
	public static readonly CountryCodeEnum SAN_MARINO = new CountryCodeEnum("SM", "SAN MARINO");
	public static readonly CountryCodeEnum SAO_TOME_AND_PRINCIPE = new CountryCodeEnum("ST", "SAO TOME AND PRINCIPE");
	public static readonly CountryCodeEnum SAUDI_ARABIA = new CountryCodeEnum("SA", "SAUDI ARABIA");
	public static readonly CountryCodeEnum SENEGAL = new CountryCodeEnum("SN", "SENEGAL");
	public static readonly CountryCodeEnum SERBIA = new CountryCodeEnum("RS", "SERBIA");
	public static readonly CountryCodeEnum SEYCHELLES = new CountryCodeEnum("SC", "SEYCHELLES");
	public static readonly CountryCodeEnum SIERRA_LEONE = new CountryCodeEnum("SL", "SIERRA LEONE");
	public static readonly CountryCodeEnum SINGAPORE = new CountryCodeEnum("SG", "SINGAPORE");
	public static readonly CountryCodeEnum SLOVAKIA = new CountryCodeEnum("SK", "SLOVAKIA");
	public static readonly CountryCodeEnum SLOVENIA = new CountryCodeEnum("SI", "SLOVENIA");
	public static readonly CountryCodeEnum SOLOMON_ISLANDS = new CountryCodeEnum("SB", "SOLOMON ISLANDS");
	public static readonly CountryCodeEnum SOMALIA = new CountryCodeEnum("SO", "SOMALIA");
	public static readonly CountryCodeEnum SOUTH_AFRICA = new CountryCodeEnum("ZA", "SOUTH AFRICA");
	public static readonly CountryCodeEnum SOUTH_GEORGIA_AND_THE_SOUTH_SANDWICH_ISLANDS = new CountryCodeEnum("GS", "SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS");
	public static readonly CountryCodeEnum SPAIN = new CountryCodeEnum("ES", "SPAIN");
	public static readonly CountryCodeEnum SRI_LANKA = new CountryCodeEnum("LK", "SRI LANKA");
	public static readonly CountryCodeEnum SUDAN = new CountryCodeEnum("SD", "SUDAN");
	public static readonly CountryCodeEnum SURINAME = new CountryCodeEnum("SR", "SURINAME");
	public static readonly CountryCodeEnum SVALBARD_AND_JAN_MAYEN = new CountryCodeEnum("SJ", "SVALBARD AND JAN MAYEN");
	public static readonly CountryCodeEnum SWAZILAND = new CountryCodeEnum("SZ", "SWAZILAND");
	public static readonly CountryCodeEnum SWEDEN = new CountryCodeEnum("SE", "SWEDEN");
	public static readonly CountryCodeEnum SWITZERLAND = new CountryCodeEnum("CH", "SWITZERLAND");
	public static readonly CountryCodeEnum SYRIAN_ARAB_REPUBLIC = new CountryCodeEnum("SY", "SYRIAN ARAB REPUBLIC");
	public static readonly CountryCodeEnum TAIWAN_PROVINCE_OF_CHINA = new CountryCodeEnum("TW", "TAIWAN, PROVINCE OF CHINA");
	public static readonly CountryCodeEnum TAJIKISTAN = new CountryCodeEnum("TJ", "TAJIKISTAN");
	public static readonly CountryCodeEnum TANZANIA_UNITED_REPUBLIC_OF = new CountryCodeEnum("TZ", "TANZANIA, UNITED REPUBLIC OF");
	public static readonly CountryCodeEnum THAILAND = new CountryCodeEnum("TH", "THAILAND");
	public static readonly CountryCodeEnum TIMORLESTE = new CountryCodeEnum("TL", "TIMOR-LESTE");
	public static readonly CountryCodeEnum TOGO = new CountryCodeEnum("TG", "TOGO");
	public static readonly CountryCodeEnum TOKELAU = new CountryCodeEnum("TK", "TOKELAU");
	public static readonly CountryCodeEnum TONGA = new CountryCodeEnum("TO", "TONGA");
	public static readonly CountryCodeEnum TRINIDAD_AND_TOBAGO = new CountryCodeEnum("TT", "TRINIDAD AND TOBAGO");
	public static readonly CountryCodeEnum TUNISIA = new CountryCodeEnum("TN", "TUNISIA");
	public static readonly CountryCodeEnum TURKEY = new CountryCodeEnum("TR", "TURKEY");
	public static readonly CountryCodeEnum TURKMENISTAN = new CountryCodeEnum("TM", "TURKMENISTAN");
	public static readonly CountryCodeEnum TURKS_AND_CAICOS_ISLANDS = new CountryCodeEnum("TC", "TURKS AND CAICOS ISLANDS");
	public static readonly CountryCodeEnum TUVALU = new CountryCodeEnum("TV", "TUVALU");
	public static readonly CountryCodeEnum UGANDA = new CountryCodeEnum("UG", "UGANDA");
	public static readonly CountryCodeEnum UKRAINE = new CountryCodeEnum("UA", "UKRAINE");
	public static readonly CountryCodeEnum UNITED_ARAB_EMIRATES = new CountryCodeEnum("AE", "UNITED ARAB EMIRATES");
	public static readonly CountryCodeEnum UNITED_KINGDOM = new CountryCodeEnum("GB", "UNITED KINGDOM");
	public static readonly CountryCodeEnum UNITED_STATES = new CountryCodeEnum("US", "UNITED STATES");
	public static readonly CountryCodeEnum UNITED_STATES_MINOR_OUTLYING_ISLANDS = new CountryCodeEnum("UM", "UNITED STATES MINOR OUTLYING ISLANDS");
	public static readonly CountryCodeEnum URUGUAY = new CountryCodeEnum("UY", "URUGUAY");
	public static readonly CountryCodeEnum UZBEKISTAN = new CountryCodeEnum("UZ", "UZBEKISTAN");
	public static readonly CountryCodeEnum VANUATU = new CountryCodeEnum("VU", "VANUATU");
	public static readonly CountryCodeEnum VENEZUELA_BOLIVARIAN_REPUBLIC_OF = new CountryCodeEnum("VE", "VENEZUELA, BOLIVARIAN REPUBLIC OF");
	public static readonly CountryCodeEnum VIET_NAM = new CountryCodeEnum("VN", "VIET NAM");
	public static readonly CountryCodeEnum VIRGIN_ISLANDS_BRITISH = new CountryCodeEnum("VG", "VIRGIN ISLANDS, BRITISH");
	public static readonly CountryCodeEnum VIRGIN_ISLANDS_US = new CountryCodeEnum("VI", "VIRGIN ISLANDS, U.S.");
	public static readonly CountryCodeEnum WALLIS_AND_FUTUNA = new CountryCodeEnum("WF", "WALLIS AND FUTUNA");
	public static readonly CountryCodeEnum WESTERN_SAHARA = new CountryCodeEnum("EH", "WESTERN SAHARA");
	public static readonly CountryCodeEnum YEMEN = new CountryCodeEnum("YE", "YEMEN");
	public static readonly CountryCodeEnum ZAMBIA = new CountryCodeEnum("ZM", "ZAMBIA");
	public static readonly CountryCodeEnum ZIMBABWE = new CountryCodeEnum("ZW", "ZIMBABWE");

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
