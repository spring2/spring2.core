using System;
using System.Collections;
using Spring2.Core.Types;

namespace Spring2.Core.Types {

    public class LanguageEnum : Spring2.Core.Types.EnumDataType, ILanguage {

	private static readonly EnumDataTypeList OPTIONS = new EnumDataTypeList();

	public static readonly new LanguageEnum DEFAULT = new LanguageEnum();
	public static readonly new LanguageEnum UNSET = new LanguageEnum();

	public static readonly LanguageEnum AFRIKAANS = new LanguageEnum("af", "Afrikaans");
	public static readonly LanguageEnum ALBANIAN = new LanguageEnum("sq", "Albanian");
	public static readonly LanguageEnum ARABIC = new LanguageEnum("ar", "Arabic");
	public static readonly LanguageEnum ARMENIAN = new LanguageEnum("hy", "Armenian");
	public static readonly LanguageEnum AZERI = new LanguageEnum("az", "Azeri");
	public static readonly LanguageEnum BASQUE = new LanguageEnum("eu", "Basque");
	public static readonly LanguageEnum BELARUSIAN = new LanguageEnum("be", "Belarusian");
	public static readonly LanguageEnum BULGARIAN = new LanguageEnum("bg", "Bulgarian");
	public static readonly LanguageEnum CATALAN = new LanguageEnum("ca", "Catalan");
	public static readonly LanguageEnum CHINESE = new LanguageEnum("zh", "Chinese");
	public static readonly LanguageEnum CROATIAN = new LanguageEnum("hr", "Croatian");
	public static readonly LanguageEnum CZECH = new LanguageEnum("cs", "Czech");
	public static readonly LanguageEnum DANISH = new LanguageEnum("da", "Danish");
	public static readonly LanguageEnum DHIVEHI = new LanguageEnum("div", "Dhivehi");
	public static readonly LanguageEnum DUTCH = new LanguageEnum("nl", "Dutch");
	public static readonly LanguageEnum ENGLISH = new LanguageEnum("en", "English");
	public static readonly LanguageEnum ESTONIAN = new LanguageEnum("et", "Estonian");
	public static readonly LanguageEnum FAROESE = new LanguageEnum("fo", "Faroese");
	public static readonly LanguageEnum FARSI = new LanguageEnum("fa", "Farsi");
	public static readonly LanguageEnum FINNISH = new LanguageEnum("fi", "Finnish");
	public static readonly LanguageEnum FRENCH = new LanguageEnum("fr", "French");
	public static readonly LanguageEnum GALICIAN = new LanguageEnum("gl", "Galician");
	public static readonly LanguageEnum GEORGIAN = new LanguageEnum("ka", "Georgian");
	public static readonly LanguageEnum GERMAN = new LanguageEnum("de", "German");
	public static readonly LanguageEnum GREEK = new LanguageEnum("el", "Greek");
	public static readonly LanguageEnum GUJARATI = new LanguageEnum("gu", "Gujarati");
	public static readonly LanguageEnum HEBREW = new LanguageEnum("he", "Hebrew");
	public static readonly LanguageEnum HINDI = new LanguageEnum("hi", "Hindi");
	public static readonly LanguageEnum HUNGARIAN = new LanguageEnum("hu", "Hungarian");
	public static readonly LanguageEnum ICELANDIC = new LanguageEnum("is", "Icelandic");
	public static readonly LanguageEnum INDONESIAN = new LanguageEnum("id", "Indonesian");
	public static readonly LanguageEnum ITALIAN = new LanguageEnum("it", "Italian");
	public static readonly LanguageEnum JAPANESE = new LanguageEnum("ja", "Japanese");
	public static readonly LanguageEnum KANNADA = new LanguageEnum("kn", "Kannada");
	public static readonly LanguageEnum KAZAKH = new LanguageEnum("kk", "Kazakh");
	public static readonly LanguageEnum KONKANI = new LanguageEnum("kok", "Konkani");
	public static readonly LanguageEnum KOREAN = new LanguageEnum("ko", "Korean");
	public static readonly LanguageEnum KYRGYZ = new LanguageEnum("ky", "Kyrgyz");
	public static readonly LanguageEnum LATVIAN = new LanguageEnum("lv", "Latvian");
	public static readonly LanguageEnum LITHUANIAN = new LanguageEnum("lt", "Lithuanian");
	public static readonly LanguageEnum MACEDONIAN = new LanguageEnum("mk", "Macedonian");
	public static readonly LanguageEnum MALAY = new LanguageEnum("ms", "Malay");
	public static readonly LanguageEnum MARATHI = new LanguageEnum("mr", "Marathi");
	public static readonly LanguageEnum MONGOLIAN = new LanguageEnum("mn", "Mongolian");
	public static readonly LanguageEnum NORWEGIAN = new LanguageEnum("no", "Norwegian");
	public static readonly LanguageEnum POLISH = new LanguageEnum("pl", "Polish");
	public static readonly LanguageEnum PORTUGUESE = new LanguageEnum("pt", "Portuguese");
	public static readonly LanguageEnum PUNJABI = new LanguageEnum("pa", "Punjabi");
	public static readonly LanguageEnum ROMANIAN = new LanguageEnum("ro", "Romanian");
	public static readonly LanguageEnum RUSSIAN = new LanguageEnum("ru", "Russian");
	public static readonly LanguageEnum SANSKRIT = new LanguageEnum("sa", "Sanskrit");
	public static readonly LanguageEnum SERBIAN = new LanguageEnum("sr", "Serbian");
	public static readonly LanguageEnum SLOVAK = new LanguageEnum("sk", "Slovak");
	public static readonly LanguageEnum SLOVENIAN = new LanguageEnum("sl", "Slovenian");
	public static readonly LanguageEnum SPANISH = new LanguageEnum("es", "Spanish");
	public static readonly LanguageEnum SWAHILI = new LanguageEnum("sw", "Swahili");
	public static readonly LanguageEnum SWEDISH = new LanguageEnum("sv", "Swedish");
	public static readonly LanguageEnum SYRIAC = new LanguageEnum("syr", "Syriac");
	public static readonly LanguageEnum TAMIL = new LanguageEnum("ta", "Tamil");
	public static readonly LanguageEnum TATAR = new LanguageEnum("tt", "Tatar");
	public static readonly LanguageEnum TELUGU = new LanguageEnum("te", "Telugu");
	public static readonly LanguageEnum THAI = new LanguageEnum("th", "Thai");
	public static readonly LanguageEnum TURKISH = new LanguageEnum("tr", "Turkish");
	public static readonly LanguageEnum UKRAINIAN = new LanguageEnum("uk", "Ukrainian");
	public static readonly LanguageEnum URDU = new LanguageEnum("ur", "Urdu");
	public static readonly LanguageEnum UZBEK = new LanguageEnum("uz", "Uzbek");
	public static readonly LanguageEnum VIETNAMESE = new LanguageEnum("vi", "Vietnamese");

	public static LanguageEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (LanguageEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	public ILanguage GetInstanceNonStatic(Object value) {
	    if (value is String) {
		foreach (LanguageEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private LanguageEnum() {}

	private LanguageEnum(String code, String name) {
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

	public void SetValue(Object newValue){
	    LanguageEnum instance = LanguageEnum.GetInstance(newValue);
	    code = instance.Code;
	    name = instance.Name;
	
	}
    }
}
