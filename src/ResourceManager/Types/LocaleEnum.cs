using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.ResourceManager.Types {

    public class LocaleEnum : Spring2.Core.Types.EnumDataType {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new LocaleEnum DEFAULT = new LocaleEnum();
	public static readonly new LocaleEnum UNSET = new LocaleEnum();

	public static readonly LocaleEnum ALBANIA = new LocaleEnum("AL", "Albania");
	public static readonly LocaleEnum ALGERIA = new LocaleEnum("DZ", "Algeria");
	public static readonly LocaleEnum ARGENTINA = new LocaleEnum("AR", "Argentina");
	public static readonly LocaleEnum ARMENIA = new LocaleEnum("AM", "Armenia");
	public static readonly LocaleEnum AUSTRALIA = new LocaleEnum("AU", "Australia");
	public static readonly LocaleEnum AUSTRIA = new LocaleEnum("AT", "Austria");
	public static readonly LocaleEnum AZERBAIJAN = new LocaleEnum("AZ", "Azerbaijan");
	public static readonly LocaleEnum BAHRAIN = new LocaleEnum("BH", "Bahrain");
	public static readonly LocaleEnum BELARUS = new LocaleEnum("BY", "Belarus");
	public static readonly LocaleEnum BELGIUM = new LocaleEnum("BE", "Belgium");
	public static readonly LocaleEnum BELIZE = new LocaleEnum("BZ", "Belize");
	public static readonly LocaleEnum BOLIVIA = new LocaleEnum("BO", "Bolivia");
	public static readonly LocaleEnum BRAZIL = new LocaleEnum("BR", "Brazil");
	public static readonly LocaleEnum BRUNEI = new LocaleEnum("BN", "Brunei");
	public static readonly LocaleEnum BULGARIA = new LocaleEnum("BG", "Bulgaria");
	public static readonly LocaleEnum CANADA = new LocaleEnum("CA", "Canada");
	public static readonly LocaleEnum CARIBBEAN = new LocaleEnum("CB", "Caribbean");
	public static readonly LocaleEnum CHILE = new LocaleEnum("CL", "Chile");
	public static readonly LocaleEnum CHINA = new LocaleEnum("CN", "China");
	public static readonly LocaleEnum COLOMBIA = new LocaleEnum("CO", "Colombia");
	public static readonly LocaleEnum COSTA_RICA = new LocaleEnum("CR", "Costa Rica");
	public static readonly LocaleEnum CROATIA = new LocaleEnum("HR", "Croatia");
	public static readonly LocaleEnum CZECH_REPUBLIC = new LocaleEnum("CZ", "Czech Republic");
	public static readonly LocaleEnum DENMARK = new LocaleEnum("DK", "Denmark");
	public static readonly LocaleEnum DOMINICAN_REPUBLIC = new LocaleEnum("DO", "Dominican Republic");
	public static readonly LocaleEnum ECUADOR = new LocaleEnum("EC", "Ecuador");
	public static readonly LocaleEnum EGYPT = new LocaleEnum("EG", "Egypt");
	public static readonly LocaleEnum EL_SALVADOR = new LocaleEnum("SV", "El Salvador");
	public static readonly LocaleEnum ESTONIA = new LocaleEnum("EE", "Estonia");
	public static readonly LocaleEnum FAROE_ISLANDS = new LocaleEnum("FO", "Faroe Islands");
	public static readonly LocaleEnum FINLAND = new LocaleEnum("FI", "Finland");
	public static readonly LocaleEnum FRANCE = new LocaleEnum("FR", "France");
	public static readonly LocaleEnum FYROM = new LocaleEnum("MK", "FYROM");
	public static readonly LocaleEnum GEORGIA = new LocaleEnum("GE", "Georgia");
	public static readonly LocaleEnum GERMANY = new LocaleEnum("DE", "Germany");
	public static readonly LocaleEnum UNITED_KINGDOM = new LocaleEnum("GB", "United Kingdom");
	public static readonly LocaleEnum GREECE = new LocaleEnum("GR", "Greece");
	public static readonly LocaleEnum GUATEMALA = new LocaleEnum("GT", "Guatemala");
	public static readonly LocaleEnum HONDURAS = new LocaleEnum("HN", "Honduras");
	public static readonly LocaleEnum HONG_KONG = new LocaleEnum("HK", "Hong Kong");
	public static readonly LocaleEnum HUNGARY = new LocaleEnum("HU", "Hungary");
	public static readonly LocaleEnum ICELAND = new LocaleEnum("IS", "Iceland");
	public static readonly LocaleEnum INDIA = new LocaleEnum("IN", "India");
	public static readonly LocaleEnum INDONESIA = new LocaleEnum("ID", "Indonesia");
	public static readonly LocaleEnum IRAN = new LocaleEnum("IR", "Iran");
	public static readonly LocaleEnum IRAQ = new LocaleEnum("IQ", "Iraq");
	public static readonly LocaleEnum IRELAND = new LocaleEnum("IE", "Ireland");
	public static readonly LocaleEnum ISRAEL = new LocaleEnum("IL", "Israel");
	public static readonly LocaleEnum ITALY = new LocaleEnum("IT", "Italy");
	public static readonly LocaleEnum JAMAICA = new LocaleEnum("JM", "Jamaica");
	public static readonly LocaleEnum JAPAN = new LocaleEnum("JP", "Japan");
	public static readonly LocaleEnum JORDAN = new LocaleEnum("JO", "Jordan");
	public static readonly LocaleEnum KAZAKHSTAN = new LocaleEnum("KZ", "Kazakhstan");
	public static readonly LocaleEnum KENYA = new LocaleEnum("KE", "Kenya");
	public static readonly LocaleEnum KOREA = new LocaleEnum("KR", "Korea");
	public static readonly LocaleEnum KUWAIT = new LocaleEnum("KW", "Kuwait");
	public static readonly LocaleEnum LATVIA = new LocaleEnum("LV", "Latvia");
	public static readonly LocaleEnum LEBANON = new LocaleEnum("LB", "Lebanon");
	public static readonly LocaleEnum LIBYA = new LocaleEnum("LY", "Libya");
	public static readonly LocaleEnum LIECHTENSTEIN = new LocaleEnum("LI", "Liechtenstein");
	public static readonly LocaleEnum LITHUANIA = new LocaleEnum("LT", "Lithuania");
	public static readonly LocaleEnum LUXEMBOURG = new LocaleEnum("LU", "Luxembourg");
	public static readonly LocaleEnum MACAU = new LocaleEnum("MO", "Macau");
	public static readonly LocaleEnum MALAYSIA = new LocaleEnum("MY", "Malaysia");
	public static readonly LocaleEnum MALDIVES = new LocaleEnum("MV", "Maldives");
	public static readonly LocaleEnum MEXICO = new LocaleEnum("MX", "Mexico");
	public static readonly LocaleEnum MONACO = new LocaleEnum("MC", "Monaco");
	public static readonly LocaleEnum MONGOLIA = new LocaleEnum("MN", "Mongolia");
	public static readonly LocaleEnum MOROCCO = new LocaleEnum("MA", "Morocco");
	public static readonly LocaleEnum NEW_ZEALAND = new LocaleEnum("NZ", "New Zealand");
	public static readonly LocaleEnum NICARAGUA = new LocaleEnum("NI", "Nicaragua");
	public static readonly LocaleEnum NORWAY = new LocaleEnum("NO", "Norway");
	public static readonly LocaleEnum OMAN = new LocaleEnum("OM", "Oman");
	public static readonly LocaleEnum PAKISTAN = new LocaleEnum("PK", "Pakistan");
	public static readonly LocaleEnum PANAMA = new LocaleEnum("PA", "Panama");
	public static readonly LocaleEnum PARAGUAY = new LocaleEnum("PY", "Paraguay");
	public static readonly LocaleEnum PERU = new LocaleEnum("PE", "Peru");
	public static readonly LocaleEnum PHILIPPINES = new LocaleEnum("PH", "Philippines");
	public static readonly LocaleEnum POLAND = new LocaleEnum("PL", "Poland");
	public static readonly LocaleEnum PORTUGAL = new LocaleEnum("PT", "Portugal");
	public static readonly LocaleEnum PUERTO_RICO = new LocaleEnum("PR", "Puerto Rico");
	public static readonly LocaleEnum QATAR = new LocaleEnum("QA", "Qatar");
	public static readonly LocaleEnum ROMANIA = new LocaleEnum("RO", "Romania");
	public static readonly LocaleEnum RUSSIA = new LocaleEnum("RU", "Russia");
	public static readonly LocaleEnum SAUDI_ARABIA = new LocaleEnum("SA", "Saudi Arabia");
	public static readonly LocaleEnum SERBIA = new LocaleEnum("SP", "Serbia");
	public static readonly LocaleEnum SINGAPORE = new LocaleEnum("SG", "Singapore");
	public static readonly LocaleEnum SLOVAKIA = new LocaleEnum("SK", "Slovakia");
	public static readonly LocaleEnum SLOVENIA = new LocaleEnum("SI", "Slovenia");
	public static readonly LocaleEnum SOUTH_AFRICA = new LocaleEnum("ZA ", "South Africa");
	public static readonly LocaleEnum SPAIN = new LocaleEnum("ES", "Spain");
	public static readonly LocaleEnum SWEDEN = new LocaleEnum("SE", "Sweden");
	public static readonly LocaleEnum SYRIA = new LocaleEnum("SY", "Syria");
	public static readonly LocaleEnum TAIWAN = new LocaleEnum("TW", "Taiwan");
	public static readonly LocaleEnum THAILAND = new LocaleEnum("TH", "Thailand");
	public static readonly LocaleEnum THE_NETHERLANDS = new LocaleEnum("NL", "The Netherlands");
	public static readonly LocaleEnum TRINIDAD_AND_TOBAGO = new LocaleEnum("TT", "Trinidad and Tobago");
	public static readonly LocaleEnum TUNISIA = new LocaleEnum("TN", "Tunisia");
	public static readonly LocaleEnum TURKEY = new LocaleEnum("TR", "Turkey");
	public static readonly LocaleEnum UKRAINE = new LocaleEnum("UA", "Ukraine");
	public static readonly LocaleEnum UNITED_ARAB_EMIRATES = new LocaleEnum("AE", "United Arab Emirates");
	public static readonly LocaleEnum UNITED_STATES = new LocaleEnum("US", "United States");
	public static readonly LocaleEnum URUGUAY = new LocaleEnum("UY", "Uruguay");
	public static readonly LocaleEnum UZBEKISTAN = new LocaleEnum("UZ", "Uzbekistan");
	public static readonly LocaleEnum VENEZUELA = new LocaleEnum("VE", "Venezuela");
	public static readonly LocaleEnum VIETNAM = new LocaleEnum("VN", "Vietnam");
	public static readonly LocaleEnum YEMEN = new LocaleEnum("YE", "Yemen");
	public static readonly LocaleEnum ZIMBABWE = new LocaleEnum("ZW", "Zimbabwe");

	public static LocaleEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (LocaleEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private LocaleEnum() {}

	private LocaleEnum(String code, String name) {
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
