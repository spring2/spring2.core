using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Types;
using System.Security.Principal;
using System.Threading;

namespace Spring2.Core.Security {
    public class UserPrincipal : IUserPrincipal {
	IdType userId;
	WindowsPrincipal windowsPrincipal;

	public UserPrincipal() { }
	public UserPrincipal(IdType userId) {
	    this.userId = userId;
	    windowsPrincipal = Thread.CurrentPrincipal as WindowsPrincipal;
	}

	public ILocale Locale {
	    get {
		return LocaleEnum.UNITED_STATES.GetInstanceNonStatic(LocaleEnum.UNITED_STATES.Code);
	    }
	}

	public ILanguage Language {
	    get {
		return LanguageEnum.ENGLISH.GetInstanceNonStatic(LanguageEnum.ENGLISH.Code);
	    }
	}

	public IdType UserId {
	    get {
		return userId;
	    }
	}


	public IIdentity Identity {
	    get {
		return windowsPrincipal.Identity;
	    }
	}

	public bool IsInRole(string role) {
	    throw new System.NotImplementedException();
	}
    }
}
