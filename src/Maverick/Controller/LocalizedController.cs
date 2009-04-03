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

	private String GetCultureFromRequest(String defaultCulture) {
	    String culture = defaultCulture;
	    if (ControllerContext.HttpContext.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] != null && ControllerContext.HttpContext.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].Length>0) {
	    	String[] cultures = ControllerContext.HttpContext.Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].Split(new char[] {','});   // need to parse out comma seperated values, then sort by q= value, where q=1 is the default if not specified
	    	if (cultures.Length>0 && cultures[0] != null) {
	    	    culture = cultures[0];
	    	} 
	    }

	    return culture;
	}

	public virtual ILocale Locale {
	    get {
		String culture = GetCultureFromRequest("en-US");
		if (culture.IndexOf("-")>0) {
		    String locale = culture.Substring(culture.IndexOf("-") + 1, 2);
		    LocaleEnum localeEnum = LocaleEnum.GetInstance(locale.ToUpper());
		    if (localeEnum.IsValid) {
			return localeEnum;
		    } else {
			//return system default of US
			return LocaleEnum.UNITED_STATES;
		    }
		} else {
		    //return system default of US
		    return LocaleEnum.UNITED_STATES;
		}
	    }
	}

	public virtual ILanguage Language {
	    get {
		String culture = GetCultureFromRequest("en-US");

		String language;
		if (culture.IndexOf("-")>0) {
		    language = culture.Substring(0, culture.IndexOf("-"));
		} else {
		    language = culture;
		}

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