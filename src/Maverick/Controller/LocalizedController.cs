using System;
using Spring2.Core.Types;

namespace Spring2.Core.Maverick.Controller {
    /// <summary>
    /// Summary description for LocalizedController.
    /// </summary>
    public abstract class LocalizedController : ErrorableController {

	public abstract String LocalizedPerform();

	public override String SafePerform() {
	    return LocalizedPerform();
	}

	public virtual ILocale Locale {
	    get{
		//try to determine the locale of the user from the web request
		String culture = ControllerContext.HttpContext.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
		String locale = culture.Substring(culture.IndexOf("-") + 1, 2);
		LocaleEnum localeEnum = LocaleEnum.GetInstance(locale.ToUpper());
		if (localeEnum.IsValid) {
		    return localeEnum;
		} else {
		    //return system default of US
		    return LocaleEnum.UNITED_STATES;
		}
	    }
	}

	public virtual ILanguage Language {
	    get{
		//try to determine the Language of the user from the web request
		String culture = ControllerContext.HttpContext.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
		String language = culture.Substring(0, culture.IndexOf("-"));
		LanguageEnum languageEnum = LanguageEnum.GetInstance(language.ToLower());
		if (languageEnum.IsValid) {
		    return languageEnum;
		} else {
		    //return system default of english
		    return LanguageEnum.ENGLISH;
		}
	    }
	}
    }
}
